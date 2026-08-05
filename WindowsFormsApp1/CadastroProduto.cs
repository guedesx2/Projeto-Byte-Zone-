using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Net.Http;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class CadastroProduto : Form
    {
        private class SimpleItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        private decimal ParseDecimalInput(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input)) return 0m;
                var s = input.Trim();
                // se contém ambos '.' e ',' assume '.' como thousands e ',' como decimal
                if (s.Contains(".") && s.Contains(","))
                {
                    s = s.Replace(".", string.Empty);
                    s = s.Replace(",", ".");
                }
                else if (s.Contains(",") && !s.Contains("."))
                {
                    // apenas vírgula -> decimal separator
                    s = s.Replace(",", ".");
                }
                // agora parse com Invariant
                if (decimal.TryParse(s, NumberStyles.Number | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var v)) return v;
                // fallback tentar parse com CurrentCulture
                if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out v)) return v;
            }
            catch { }
            return 0m;
        }

        private string MapUiStatusToDb(MySqlConnection conn, MySqlTransaction tran, string uiStatus)
        {
            try
            {
                string columnType;
                using (var cmd = new MySqlCommand("SELECT COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME='tbl_produtos' AND COLUMN_NAME='StatusProduto'", conn, tran))
                {
                    columnType = Convert.ToString(cmd.ExecuteScalar() ?? string.Empty);
                }
                if (string.IsNullOrEmpty(columnType)) return uiStatus;
                if (!columnType.StartsWith("enum(")) return uiStatus;
                var matches = Regex.Matches(columnType, "'([^']*)'");
                var allowed = matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
                if (allowed.Contains(uiStatus)) return uiStatus;
                // common mappings
                if (uiStatus == "Disponível" && allowed.Contains("Ativo")) return "Ativo";
                if (uiStatus == "Esgotado" && allowed.Contains("Inativo")) return "Inativo";
                if (uiStatus == "Ativo" && allowed.Contains("Disponível")) return "Disponível";
                if (uiStatus == "Inativo" && allowed.Contains("Esgotado")) return "Esgotado";
                // fallback to first allowed
                return allowed.FirstOrDefault() ?? uiStatus;
            }
            catch
            {
                return uiStatus;
            }
        }
        public CadastroProduto()
        {
            InitializeComponent();
            // eventos
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
            btnPreview.Click += async (s, e) => await LoadImagePreviewAsync();
            LoadMarcasCategorias();
            // preencher campos de descrição se já foram setados em Variaveis
            try
            {
                if (!string.IsNullOrEmpty(Variaveis.CaixaTxtDescricaoProd)) txtDescricao.Text = Variaveis.CaixaTxtDescricaoProd;
                if (!string.IsNullOrEmpty(Variaveis.CaixaTxtEspecificacoesProd)) txtEspecificacoes.Text = Variaveis.CaixaTxtEspecificacoesProd;
            }
            catch { }
        }

        private void LoadMarcasCategorias()
        {
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    // marcas (usar tabela/colunas conforme padrão do projeto)
                    string sqlM = "SELECT ID_Marca, NomeMarca FROM tbl_marca ORDER BY NomeMarca";
                    using (var cmd = new MySqlCommand(sqlM, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var id = dr["ID_Marca"] != DBNull.Value ? Convert.ToInt32(dr["ID_Marca"]) : 0;
                            var name = dr["NomeMarca"]?.ToString() ?? string.Empty;
                            cmbMarca.Items.Add(new SimpleItem { Id = id, Name = name });
                        }
                    }
                    // categorias
                    string sqlC = "SELECT ID_Categoria, NomeCategoria FROM tbl_categoria ORDER BY NomeCategoria";
                    using (var cmd = new MySqlCommand(sqlC, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var id = dr["ID_Categoria"] != DBNull.Value ? Convert.ToInt32(dr["ID_Categoria"]) : 0;
                            var name = dr["NomeCategoria"]?.ToString() ?? string.Empty;
                            cmbCategoria.Items.Add(new SimpleItem { Id = id, Name = name });
                        }
                    }
                }
            }
            catch { }
            if (cmbMarca.Items.Count > 0) cmbMarca.SelectedIndex = 0;
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var nome = (Convert.ToString(txtNomeProd.Text) ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(nome)) { MessageBox.Show("Nome do produto obrigatório."); return; }

            // converter valores numéricos (normalizar entradas com separador de milhar '.' e decimal ',')
            decimal valor = 0, valorPromo = 0, peso = 0;
            int qtd = 0;
            valor = ParseDecimalInput(txtValorPreco.Text);
            valorPromo = ParseDecimalInput(txtValorPromocional.Text);
            peso = ParseDecimalInput(txtPesoKG.Text);
            Int32.TryParse(Convert.ToString(txtQtdEstoque.Text) ?? string.Empty, out qtd);

            int idMarca = 0, idCategoria = 0;
            if (cmbMarca.SelectedItem is SimpleItem mi) idMarca = mi.Id;
            if (cmbCategoria.SelectedItem is SimpleItem ci) idCategoria = ci.Id;

            // validação: marca e categoria obrigatórias
            ClearValidationMessage();
            if (cmbMarca.SelectedItem == null || idMarca == 0)
            {
                ShowValidationMessage("Selecione uma marca antes de salvar.");
                return;
            }
            if (cmbCategoria.SelectedItem == null || idCategoria == 0)
            {
                ShowValidationMessage("Selecione uma categoria antes de salvar.");
                return;
            }

            var slug = (Convert.ToString(txtSlug.Text) ?? string.Empty).Trim();
            var status = cmbStatus.SelectedItem?.ToString() ?? "Disponível";
            // map UI label to DB value
            var dbStatus = status == "Disponível" ? "Ativo" : "Inativo";
            var urlImagem = (Convert.ToString(txtUrlImagem.Text) ?? string.Empty).Trim();
            // valida URL, se informada
            if (!string.IsNullOrEmpty(urlImagem))
            {
                if (!Uri.TryCreate(urlImagem, UriKind.Absolute, out var u) || (u.Scheme != Uri.UriSchemeHttp && u.Scheme != Uri.UriSchemeHttps))
                {
                    MessageBox.Show("URL da imagem inválida. Informe um endereço HTTP/HTTPS válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // sempre tratar a imagem como principal (apenas uma imagem usada no site)
            var principal = 1;

            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        // inserir produto
                        string insertProd = @"INSERT INTO tbl_produtos (Nome_Prod, ID_Marca, ID_Categoria, Valor_Preco, ValorPromocional, QtdEstoque, PesoKG, Slug, StatusProduto)
VALUES (@Nome_Prod, @ID_Marca, @ID_Categoria, @Valor_Preco, @ValorPromocional, @QtdEstoque, @PesoKG, @Slug, @StatusProduto)";
                        using (var cmd = new MySqlCommand(insertProd, conn, tran))
                        {
                            cmd.Parameters.Add("@Nome_Prod", MySqlDbType.VarChar, 255).Value = nome;
                            cmd.Parameters.Add("@ID_Marca", MySqlDbType.Int32).Value = idMarca;
                            cmd.Parameters.Add("@ID_Categoria", MySqlDbType.Int32).Value = idCategoria;
                            cmd.Parameters.Add("@Valor_Preco", MySqlDbType.Decimal).Value = valor;
                            cmd.Parameters.Add("@ValorPromocional", MySqlDbType.Decimal).Value = valorPromo;
                            cmd.Parameters.Add("@QtdEstoque", MySqlDbType.Int32).Value = qtd;
                            cmd.Parameters.Add("@PesoKG", MySqlDbType.Decimal).Value = peso;
                            cmd.Parameters.Add("@Slug", MySqlDbType.VarChar, 200).Value = slug;
                            // map UI status to DB allowed value
                            cmd.Parameters.Add("@StatusProduto", MySqlDbType.VarChar, 50).Value = MapUiStatusToDb(conn, tran, status);
                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            // obter id inserido com comando separado (compatível com MySqlConnector settings)
                            int insertedId = 0;
                            using (var idCmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn, tran))
                            {
                                var idObj = idCmd.ExecuteScalar();
                                insertedId = idObj != null ? Convert.ToInt32(idObj) : 0;
                            }

                            // inserir imagem relacionada, se informado
                            if (!string.IsNullOrEmpty(urlImagem))
                            {
                                string insertImg = "INSERT INTO tbl_produtoimagem (ID_Produto, UrlImagem, Principal) VALUES (@ID_Produto, @UrlImagem, @Principal)";
                                using (var icmd = new MySqlCommand(insertImg, conn, tran))
                                {
                                    icmd.Parameters.Add("@ID_Produto", MySqlDbType.Int32).Value = insertedId;
                                    icmd.Parameters.Add("@UrlImagem", MySqlDbType.VarChar, 1000).Value = urlImagem;
                                    icmd.Parameters.Add("@Principal", MySqlDbType.Bit).Value = principal;
                                    icmd.Prepare();
                                    icmd.ExecuteNonQuery();
                                }
                            }

                            // inserir descrição do produto (uma por produto)
                            try
                            {
                                var descText = (Convert.ToString(txtDescricao.Text) ?? string.Empty).Trim();
                                var espec = (Convert.ToString(txtEspecificacoes.Text) ?? string.Empty).Trim();
                                var garantia = Convert.ToInt32(numGarantia.Value);
                                // sempre inserir registro de descrição (mesmo que campos vazios) para manter consistência
                                try
                                {
                                    string insDesc = "INSERT INTO tbl_descricaoproduto (ID_Produto, Descricao, Especificacoes, GarantiaMeses) VALUES (@ID_Produto, @Descricao, @Especificacoes, @Garantia)";
                                    using (var dcmd = new MySqlCommand(insDesc, conn, tran))
                                    {
                                        dcmd.Parameters.Add("@ID_Produto", MySqlDbType.Int32).Value = insertedId;
                                        dcmd.Parameters.Add("@Descricao", MySqlDbType.Text).Value = descText;
                                        dcmd.Parameters.Add("@Especificacoes", MySqlDbType.Text).Value = espec;
                                        dcmd.Parameters.Add("@Garantia", MySqlDbType.Int32).Value = garantia;
                                        dcmd.Prepare();
                                        dcmd.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Erro ao inserir descrição do produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch { }

                            tran.Commit();
                            ShowValidationMessage("Produto cadastrado com sucesso.", success: true);
                            // manter MessageBox para confirmação final e fechar formulário
                            MessageBox.Show("Produto cadastrado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowValidationMessage(string message, bool success = false)
        {
            try
            {
                if (lblValidationMessage == null)
                {
                    lblValidationMessage = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblValidationMessage");
                }
                if (lblValidationMessage != null)
                {
                    lblValidationMessage.Text = message;
                    lblValidationMessage.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                    lblValidationMessage.Visible = true;
                }
                else
                {
                    MessageBox.Show(message, success ? "Sucesso" : "Aviso", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void ClearValidationMessage()
        {
            try
            {
                if (lblValidationMessage == null)
                {
                    lblValidationMessage = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblValidationMessage");
                }
                if (lblValidationMessage != null) { lblValidationMessage.Text = string.Empty; lblValidationMessage.Visible = false; }
            }
            catch { }
        }

        private async Task LoadImagePreviewAsync()
        {
            var url = (Convert.ToString(txtUrlImagem.Text) ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Informe a URL da imagem para visualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Uri.TryCreate(url, UriKind.Absolute, out var u) || (u.Scheme != Uri.UriSchemeHttp && u.Scheme != Uri.UriSchemeHttps))
            {
                MessageBox.Show("URL inválida. Use HTTP ou HTTPS.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(u);
                    using (var ms = new MemoryStream(bytes))
                    {
                        var img = Image.FromStream(ms);
                        // descartar imagem anterior
                        if (picPreview.Image != null) { var old = picPreview.Image; picPreview.Image = null; old.Dispose(); }
                        picPreview.Image = new System.Drawing.Bitmap(img);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao carregar imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
