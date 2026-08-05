using System;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class AlterarProduto : Form
    {
        private class SimpleItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        private async Task LoadImagePreviewAsync()
        {
            var url = (Convert.ToString(txtURLImagem.Text) ?? string.Empty).Trim();
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
                        if (picImagemSite.Image != null) { var old = picImagemSite.Image; picImagemSite.Image = null; old.Dispose(); }
                        picImagemSite.Image = new System.Drawing.Bitmap(img);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao carregar imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal ParseDecimalInput(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input)) return 0m;
                var s = input.Trim();
                if (s.Contains(".") && s.Contains(","))
                {
                    s = s.Replace(".", string.Empty);
                    s = s.Replace(",", ".");
                }
                else if (s.Contains(",") && !s.Contains("."))
                {
                    s = s.Replace(",", ".");
                }
                if (decimal.TryParse(s, System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out var v)) return v;
                if (decimal.TryParse(input, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out v)) return v;
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
                if (uiStatus == "Disponível" && allowed.Contains("Ativo")) return "Ativo";
                if (uiStatus == "Esgotado" && allowed.Contains("Inativo")) return "Inativo";
                if (uiStatus == "Ativo" && allowed.Contains("Disponível")) return "Disponível";
                if (uiStatus == "Inativo" && allowed.Contains("Esgotado")) return "Esgotado";
                return allowed.FirstOrDefault() ?? uiStatus;
            }
            catch
            {
                return uiStatus;
            }
        }
        private int originalMarcaId = 0;
        private int originalCategoriaId = 0;
        private int currentDescricaoId = 0;

        public AlterarProduto()
        {
            InitializeComponent();
            btnSalvar.Click += BtnSalvar_Click;
            btnCancelar.Click += (s, e) => this.Close();

            cmbStatusProduto.Items.Clear();
            cmbStatusProduto.Items.Add("Disponível");
            cmbStatusProduto.Items.Add("Esgotado");
            cmbStatusProduto.DropDownStyle = ComboBoxStyle.DropDownList;
            // registrar combobox de produtos e carregar lista
            cmbProdutos.SelectedIndexChanged += CmbProdutos_SelectedIndexChanged;
            // preview de imagem (comportamento igual ao CadastroProduto)
            try
            {
                button1.Click += async (s, e) => await LoadImagePreviewAsync();
                // permitir pressionar Enter dentro da caixa de URL para abrir preview
                txtURLImagem.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await LoadImagePreviewAsync(); } };
            }
            catch { }
            // carregar marcas e categorias
            LoadMarcasCategorias();
            LoadProductsList();
        }

        private void ShowValidationMessage(string message, bool success = false)
        {
            try
            {
                if (lblValidationMessage == null)
                {
                    // tentar obter controle do Designer (caso tenha sido adicionado lá)
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

        // Construtor adicional para abrir o formulário já carregado pelo ID do produto
        public AlterarProduto(int id) : this()
        {
            LoadProductById(id);
        }

        private void LoadMarcasCategorias()
        {
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    // marcas
                    string sqlM = "SELECT ID_Marca, NomeMarca FROM tbl_marca ORDER BY NomeMarca";
                    using (var cmd = new MySqlCommand(sqlM, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var id = dr["ID_Marca"] != DBNull.Value ? Convert.ToInt32(dr["ID_Marca"]) : 0;
                            var name = dr["NomeMarca"]?.ToString() ?? string.Empty;
                            cmbIDMarca.Items.Add(new SimpleItem { Id = id, Name = name });
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
                            cmbIDCategoria.Items.Add(new SimpleItem { Id = id, Name = name });
                        }
                    }
                }
            }
            catch { }
            if (cmbIDMarca.Items.Count > 0) cmbIDMarca.SelectedIndex = 0;
            if (cmbIDCategoria.Items.Count > 0) cmbIDCategoria.SelectedIndex = 0;
        }

        private class ProductListItem
        {
            public int Id { get; set; }
            public string Display { get; set; }
            public override string ToString() => Display;
        }

        private void LoadProductsList()
        {
            try
            {
                cmbProdutos.Items.Clear();

                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();

                    string sql = "SELECT ID_Produto, Nome_Prod FROM tbl_produtos ORDER BY ID_Produto, Nome_Prod";

                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string nome = dr["Nome_Prod"]?.ToString() ?? "";
                            int id = dr["ID_Produto"] != DBNull.Value
                                ? Convert.ToInt32(dr["ID_Produto"])
                                : 0;

                            cmbProdutos.Items.Add(new ProductListItem
                            {
                                Id = id,
                                Display = nome
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao carregar lista de produtos: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CmbProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProdutos.SelectedItem is ProductListItem it && it.Id > 0)
            {
                LoadProductById(it.Id);
            }
        }

        private void LoadProductById(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();

                    // primeiro buscar os dados principais do produto e fechar o reader antes de executar outras queries
                    string sql = "SELECT Nome_Prod,ID_Marca, ID_Categoria, Valor_Preco, ValorPromocional, QtdEstoque, PesoKG, StatusProduto FROM tbl_produtos WHERE ID_Produto = @ID_Produto LIMIT 1";
                    string nome = string.Empty;
                    int idMarca = 0;
                    int idCategoria = 0;
                    string valorPreco = string.Empty;
                    string valorProm = string.Empty;
                    string qtd = string.Empty;
                    string peso = string.Empty;
                    string dbStatus = null;

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Produto", id);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                nome = dr["Nome_Prod"]?.ToString() ?? dr["Nome_Produto"]?.ToString() ?? string.Empty;
                                idMarca = dr["ID_Marca"] != DBNull.Value ? Convert.ToInt32(dr["ID_Marca"]) : 0;
                                idCategoria = dr["ID_Categoria"] != DBNull.Value ? Convert.ToInt32(dr["ID_Categoria"]) : 0;
                                valorPreco = dr["Valor_Preco"]?.ToString() ?? string.Empty;
                                valorProm = dr["ValorPromocional"]?.ToString() ?? string.Empty;
                                qtd = dr["QtdEstoque"]?.ToString() ?? string.Empty;
                                peso = dr["PesoKG"]?.ToString() ?? string.Empty;
                                dbStatus = dr["StatusProduto"]?.ToString();
                            }
                        }
                    }

                    // aplicar aos controles após reader fechado
                    txtNomeProd.Text = nome;
                    originalMarcaId = idMarca;
                    originalCategoriaId = idCategoria;
                    for (int i = 0; i < cmbIDMarca.Items.Count; i++)
                    {
                        if (cmbIDMarca.Items[i] is SimpleItem si && si.Id == idMarca) { cmbIDMarca.SelectedIndex = i; break; }
                    }
                    for (int i = 0; i < cmbIDCategoria.Items.Count; i++)
                    {
                        if (cmbIDCategoria.Items[i] is SimpleItem ci && ci.Id == idCategoria) { cmbIDCategoria.SelectedIndex = i; break; }
                    }
                    txtValorPreco.Text = valorPreco;
                    txtValorPromocional.Text = valorProm;
                    txtQtdEstoque.Text = qtd;
                    txtPesoKG.Text = peso;

                    var uiStatus = dbStatus == "Inativo" ? "Esgotado" : "Disponível";
                    if (cmbStatusProduto.Items.Contains(uiStatus)) cmbStatusProduto.SelectedItem = uiStatus;
                    else if (cmbStatusProduto.Items.Count > 0) cmbStatusProduto.SelectedIndex = 0;

                    // agora buscar descrição (em comando separado, reader anterior já fechado)
                    try
                    {
                        using (var dcmd = new MySqlCommand("SELECT ID_Descricao, Descricao, Especificacoes, GarantiaMeses FROM tbl_descricaoproduto WHERE ID_Produto = @id_prod LIMIT 1", conn))
                        {
                            dcmd.Parameters.AddWithValue("@id_prod", id);
                            using (var ddr = dcmd.ExecuteReader())
                            {
                                if (ddr.Read())
                                {
                                    currentDescricaoId = ddr["ID_Descricao"] != DBNull.Value ? Convert.ToInt32(ddr["ID_Descricao"]) : 0;
                                    txtDescricao.Text = ddr["Descricao"]?.ToString() ?? string.Empty;
                                    txtEspecificacoes.Text = ddr["Especificacoes"]?.ToString() ?? string.Empty;
                                    if (decimal.TryParse(ddr["GarantiaMeses"]?.ToString(), out decimal g)) numGarantia.Value = g; else numGarantia.Value = 0;
                                }
                                else
                                {
                                    currentDescricaoId = 0;
                                    txtDescricao.Text = string.Empty;
                                    txtEspecificacoes.Text = string.Empty;
                                    numGarantia.Value = 0;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar descrição do produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // buscar imagem principal
                    try
                    {
                        using (var icmd = new MySqlCommand("SELECT UrlImagem FROM tbl_produtoimagem WHERE ID_Produto = @ID_Produto AND Principal = 1 LIMIT 1", conn))
                        {
                            icmd.Parameters.AddWithValue("@ID_Produto", id);
                            var urlObj = icmd.ExecuteScalar();
                            var urlImg = urlObj != null && urlObj != DBNull.Value ? urlObj.ToString() : string.Empty;
                            txtURLImagem.Text = urlImg ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(txtURLImagem.Text)) { _ = LoadImagePreviewAsync(); }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar URL de imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // checagens auxiliares não-blocking
                    try
                    {
                        using (var cimg = new MySqlCommand("SELECT COUNT(*) FROM tbl_produtoimagem WHERE ID_Produto = @ID_Produto", conn))
                        {
                            cimg.Parameters.AddWithValue("@ID_Produto", id);
                            var cnt = cimg.ExecuteScalar();
                            var n = cnt != null && cnt != DBNull.Value ? Convert.ToInt32(cnt) : 0;
                            if (n > 0 && string.IsNullOrWhiteSpace(txtURLImagem.Text))
                            {
                                MessageBox.Show($"Encontradas {n} imagem(ns) para produto ID={id}, mas nenhuma marcada como Principal. Verifique tbl_produtoimagem.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 /*       private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string sql = "SELECT Nome_Prod, ID_Marca, ID_Categoria, Valor_Preco, ValorPromocional, QtdEstoque, PesoKG, Slug, StatusProduto";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtNomeProd.Text = dr["Nome_Prod"]?.ToString();
                                txtIDMarca.Text = dr["ID_Marca"]?.ToString();
                                txtIDCategoria.Text = dr["ID_Categoria"]?.ToString();
                                txtValorPreco.Text = dr["Valor_Preco"]?.ToString();
                                txtValorPromocional.Text = dr["ValorPromocional"]?.ToString();
                                txtQtdEstoque.Text = dr["QtdEstoque"]?.ToString();
                                txtPesoKG.Text = dr["PesoKG"]?.ToString();
                                txtSlug.Text = dr["Slug"]?.ToString();
                                var dbStatus = dr["StatusProduto"]?.ToString();
                                var uiStatus = dbStatus == "Inativo" ? "Esgotado" : "Disponível";
                                cmbStatusProduto.SelectedItem = uiStatus;
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 */

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            // leitura e validação simples
            var nome = txtNomeProd.Text.Trim();
            int idMarca = 0;
            int idCategoria = 0;
            if (cmbIDMarca.SelectedItem is SimpleItem sm) idMarca = sm.Id; else idMarca = originalMarcaId;
            if (cmbIDCategoria.SelectedItem is SimpleItem sc) idCategoria = sc.Id; else idCategoria = originalCategoriaId;
            var status = cmbStatusProduto.SelectedItem?.ToString() ?? "Disponível";
            // map UI label to DB value
            var dbStatus = status == "Disponível" ? "Ativo" : "Inativo";

            // validação inline: usar label em vez de MessageBox
            ClearValidationMessage();
            if (string.IsNullOrWhiteSpace(nome)) { ShowValidationMessage("Nome é obrigatório."); return; }

            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Nome é Obrigatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // normalizar e parsear valores numéricos
            decimal valorPreco = ParseDecimalInput(txtValorPreco.Text);
            decimal.TryParse(txtValorPromocional.Text.Trim(), out decimal valorPromo);
            int.TryParse(txtQtdEstoque.Text.Trim(), out int qtdEstoque);
            decimal pesoKG = ParseDecimalInput(txtPesoKG.Text);

            // validações adicionais de negócio
            if (valorPreco <= 0) { ShowValidationMessage("Valor_Preco deve ser maior que zero."); return; }
            if (qtdEstoque < 0) { ShowValidationMessage("QtdEstoque não pode ser negativo."); return; }

            // validar seleção de produto
            var selected = cmbProdutos.SelectedItem as ProductListItem;
            if (selected == null || selected.Id <= 0)
            {
                MessageBox.Show("Selecione um produto para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // validar marca e categoria (não podem ser nulas)
            if ((cmbIDMarca.SelectedItem is SimpleItem mi && mi.Id == 0) || !(cmbIDMarca.SelectedItem is SimpleItem))
            {
                MessageBox.Show("Selecione uma marca válida antes de salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((cmbIDCategoria.SelectedItem is SimpleItem ci2 && ci2.Id == 0) || !(cmbIDCategoria.SelectedItem is SimpleItem))
            {
                MessageBox.Show("Selecione uma categoria válida antes de salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // validar descrição e especificações (não podem ser vazias)
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("Descrição do produto é obrigatória.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEspecificacoes.Text))
            {
                MessageBox.Show("Especificações do produto são obrigatórias.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // validar URL de imagem, se informada
            var urlImagem = (Convert.ToString(txtURLImagem.Text) ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(urlImagem))
            {
                if (!Uri.TryCreate(urlImagem, UriKind.Absolute, out var u) || (u.Scheme != Uri.UriSchemeHttp && u.Scheme != Uri.UriSchemeHttps))
                {
                    MessageBox.Show("URL da imagem inválida. Informe um endereço HTTP/HTTPS válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                using (var conn = new MySqlConnection(Variaveis.strConn))
                {
                    conn.Open();
                    string updateSql = @"
                        UPDATE tbl_produtos
                        SET Nome_Prod = @Nome_Prod,
                            ID_Marca = @ID_Marca,
                            ID_Categoria = @ID_Categoria,
                            Valor_Preco = @Valor_Preco,
                            ValorPromocional = @ValorPromocional,
                            QtdEstoque = @QtdEstoque,
                            PesoKG = @PesoKG,
                            StatusProduto = @StatusProduto
                        WHERE ID_Produto = @ID_Produto";
                    using (var tran = conn.BeginTransaction())
                    {
                        using (var cmd = new MySqlCommand(updateSql, conn, tran))
                        {
                            cmd.Parameters.Add("@Nome_Prod", MySqlDbType.VarChar, 255).Value = nome;
                            cmd.Parameters.Add("@ID_Marca", MySqlDbType.Int32).Value = idMarca;
                            cmd.Parameters.Add("@ID_Categoria", MySqlDbType.Int32).Value = idCategoria;
                            cmd.Parameters.Add("@Valor_Preco", MySqlDbType.Decimal).Value = valorPreco;
                            cmd.Parameters.Add("@ValorPromocional", MySqlDbType.Decimal).Value = valorPromo;
                            cmd.Parameters.Add("@QtdEstoque", MySqlDbType.Int32).Value = qtdEstoque;
                            cmd.Parameters.Add("@PesoKG", MySqlDbType.Decimal).Value = pesoKG;
                            // map UI status label to DB value
                            cmd.Parameters.Add("@StatusProduto", MySqlDbType.VarChar, 20).Value = MapUiStatusToDb(conn, tran, status);
                            cmd.Parameters.Add("@ID_Produto", MySqlDbType.Int32).Value = selected.Id;
                            cmd.Prepare();
                            var rows = cmd.ExecuteNonQuery();

                            // garantir que exista no máximo uma descrição: bloquear possíveis registros e decidir update/insert
                            int descId = 0;
                            using (var checkCmd = new MySqlCommand("SELECT ID_Descricao FROM tbl_descricaoproduto WHERE ID_Produto = @ID_Produto LIMIT 1 FOR UPDATE", conn, tran))
                            {
                                checkCmd.Parameters.Add("@ID_Produto", MySqlDbType.Int32).Value = selected.Id;
                                using (var rdr = checkCmd.ExecuteReader())
                                {
                                    if (rdr.Read()) descId = rdr["ID_Descricao"] != DBNull.Value ? Convert.ToInt32(rdr["ID_Descricao"]) : 0;
                                }

                            // atualizar/insir imagem principal na tabela tbl_produtoimagem
                            try
                            {
                                if (!string.IsNullOrEmpty(urlImagem))
                                {
                                    string updImg = "UPDATE tbl_produtoimagem SET UrlImagem=@UrlImagem WHERE ID_Produto=@ID_Produto AND Principal=1";
                                    using (var ucmd = new MySqlCommand(updImg, conn, tran))
                                    {
                                        ucmd.Parameters.Add("@UrlImagem", MySqlDbType.VarChar, 1000).Value = urlImagem;
                                        ucmd.Parameters.Add("@ID_Produto", MySqlDbType.Int32).Value = selected.Id;
                                        var affected = ucmd.ExecuteNonQuery();
                                        if (affected == 0)
                                        {
                                            string insImg = "INSERT INTO tbl_produtoimagem (ID_Produto, UrlImagem, Principal) VALUES (@ID_Produto, @UrlImagem, 1)";
                                            using (var icmd = new MySqlCommand(insImg, conn, tran))
                                            {
                                                icmd.Parameters.Add("@ID_Produto", MySqlDbType.Int32).Value = selected.Id;
                                                icmd.Parameters.Add("@UrlImagem", MySqlDbType.VarChar, 1000).Value = urlImagem;
                                                icmd.Prepare();
                                                icmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            catch { }
                            }

                            if (descId > 0)
                            {
                                string upDesc = "UPDATE tbl_descricaoproduto SET Descricao=@Descricao, Especificacoes=@Especificacoes, GarantiaMeses=@Garantia WHERE ID_Descricao=@ID_Descricao";
                                using (var dcmd = new MySqlCommand(upDesc, conn, tran))
                                {
                                    dcmd.Parameters.Add("@Descricao", MySqlDbType.Text).Value = txtDescricao.Text ?? string.Empty;
                                    dcmd.Parameters.Add("@Especificacoes", MySqlDbType.Text).Value = txtEspecificacoes.Text ?? string.Empty;
                                    dcmd.Parameters.Add("@Garantia", MySqlDbType.Int32).Value = Convert.ToInt32(numGarantia.Value);
                                    dcmd.Parameters.Add("@ID_Descricao", MySqlDbType.Int32).Value = descId;
                                    dcmd.Prepare();
                                    dcmd.ExecuteNonQuery();
                                    currentDescricaoId = descId;
                                }
                            }
                            else
                            {
                                string insDesc = "INSERT INTO tbl_descricaoproduto (ID_Produto, Descricao, Especificacoes, GarantiaMeses) VALUES (@ID_Produto, @Descricao, @Especificacoes, @Garantia)";
                                using (var dcmd = new MySqlCommand(insDesc, conn, tran))
                                {
                                    dcmd.Parameters.Add("@ID_Produto", MySqlDbType.Int32).Value = selected.Id;
                                    dcmd.Parameters.Add("@Descricao", MySqlDbType.Text).Value = txtDescricao.Text ?? string.Empty;
                                    dcmd.Parameters.Add("@Especificacoes", MySqlDbType.Text).Value = txtEspecificacoes.Text ?? string.Empty;
                                    dcmd.Parameters.Add("@Garantia", MySqlDbType.Int32).Value = Convert.ToInt32(numGarantia.Value);
                                    dcmd.Prepare();
                                    dcmd.ExecuteNonQuery();
                                }
                                // obter id inserido (LAST_INSERT_ID) dentro da mesma transação
                                using (var lcmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn, tran))
                                {
                                    var res = lcmd.ExecuteScalar();
                                    currentDescricaoId = res != null ? Convert.ToInt32(res) : 0;
                                }
                            }

                            tran.Commit();

                            if (rows > 0)
                            {
                                ShowValidationMessage("Produto atualizado com sucesso.", success: true);
                                MessageBox.Show("Produto atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ShowValidationMessage("Nenhuma alteração aplicada.", success: true);
                                MessageBox.Show("Nenhuma alteração aplicada.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
