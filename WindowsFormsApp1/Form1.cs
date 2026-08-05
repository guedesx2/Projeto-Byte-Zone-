using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LojaTech : Form
    {
        public LojaTech()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Conexão será aberta quando necessária nos formulários
            // Associar ações adicionais do menu
            this.alteraçãoToolStripMenuItem.Click += alteraçãoToolStripMenuItem_Click;
        }

        private void usuáriosAdminsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void alteraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir listagem de produtos para seleção/alteração

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sair = new login();
            sair.Show();
            this.Close();
        }

        private void sobreNósToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sobreNos = new sobrenos();
            sobreNos.Show();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AlterarProduto();
            form.ShowDialog();

        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                var form = new EditarUsuarios();
                form.ShowDialog();
            }
        }
        private void alteraçãoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroUsuario cadastroUsuario = new CadastroUsuario();
            cadastroUsuario.ShowDialog();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroProduto cadastroProduto = new CadastroProduto();
            cadastroProduto.ShowDialog();
        }
    }
}
