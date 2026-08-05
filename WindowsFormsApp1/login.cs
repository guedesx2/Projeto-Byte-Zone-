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
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            // validações básicas
            var usuario = txtUser.Text.Trim();
            var senha = txtSenha.Text;
            // Limites e validação básica do formato do usuário para reduzir superfície de ataque
            const int MaxUserLength = 50;
            if (usuario.Length > MaxUserLength)
            {
                MessageBox.Show("Usuário muito longo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(usuario, "^[a-zA-Z0-9_.-]+$"))
            {
                MessageBox.Show("Usuário contém caracteres inválidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha usuário e senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conexao = new MySql.Data.MySqlClient.MySqlConnection(Variaveis.strConn))
                {
                    conexao.Open();

                    // Ajuste o nome da tabela/colunas conforme seu banco de dados
                    // Calcula hash SHA512 da senha informada para comparar com o campo SenhaHash
                    string hashedSenha;
                    using (var sha = SHA512.Create())
                    {
                        var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
                        var sb = new StringBuilder();
                        foreach (var b in hashBytes)
                            sb.Append(b.ToString("x2"));
                        hashedSenha = sb.ToString(); // hex minúsculo
                    }

                    // Detecta se a coluna SenhaHash existe; se não, usa Senha para compatibilidade
                    // detecta a primeira coluna de senha disponível: SenhaHash, Senha_Hash ou Senha
                    string senhaColumn = null;
                    string[] candidates = new[] { "SenhaHash", "Senha_Hash", "Senha" };
                    foreach (var candidate in candidates)
                    {
                        string checkSql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tbl_usuarios' AND COLUMN_NAME = '" + candidate + "'";
                        using (var checkCmd = new MySqlCommand(checkSql, conexao))
                        {
                            var cntObj = checkCmd.ExecuteScalar();
                            var cnt = cntObj != null ? Convert.ToInt32(cntObj) : 0;
                            if (cnt > 0) { senhaColumn = candidate; break; }
                        }
                    }
                    if (string.IsNullOrEmpty(senhaColumn)) senhaColumn = "SenhaHash";

                    string sql = $"SELECT COUNT(1) FROM tbl_usuarios WHERE NomeUsuario = @NomeUsuario AND {senhaColumn} = @Senha"; //o select count ja faz a contagem de registros que atendem a condição, retornando 1 se existir e 0 se não existir
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conexao))
                    {
                        // Use parâmetros tipados em vez de AddWithValue para evitar inferência incorreta de tipos
                        cmd.Parameters.Add("@NomeUsuario", MySqlDbType.VarChar, 100).Value = usuario;
                        cmd.Parameters.Add("@Senha", MySqlDbType.VarChar, 128).Value = hashedSenha;
                        cmd.Prepare();

                        var resultado = Convert.ToInt32(cmd.ExecuteScalar());
                        if (resultado > 0)
                        {
                            // login ok
                            Variaveis.UsuarioLogado = usuario;
                            this.Hide();
                            var principal = new LojaTech();
                            principal.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Usuário ou senha inválidos.", "Erro de login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao banco: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
