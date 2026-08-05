namespace WindowsFormsApp1
{
    partial class AlterarProduto
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblProdutos;
        private System.Windows.Forms.ComboBox cmbProdutos;
        private System.Windows.Forms.Label lblNomeProd;
        private System.Windows.Forms.TextBox txtNomeProd;
        private System.Windows.Forms.Label lblIDMarca;
        private System.Windows.Forms.ComboBox cmbIDMarca;
        private System.Windows.Forms.Label lblIDCategoria;
        private System.Windows.Forms.ComboBox cmbIDCategoria;
        private System.Windows.Forms.Label lblValorPreco;
        private System.Windows.Forms.TextBox txtValorPreco;
        private System.Windows.Forms.Label lblValorPromocional;
        private System.Windows.Forms.TextBox txtValorPromocional;
        private System.Windows.Forms.Label lblQtdEstoque;
        private System.Windows.Forms.TextBox txtQtdEstoque;
        private System.Windows.Forms.Label lblPesoKG;
        private System.Windows.Forms.TextBox txtPesoKG;
        private System.Windows.Forms.Label lblStatusProduto;
        private System.Windows.Forms.ComboBox cmbStatusProduto;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.TextBox txtEspecificacoes;
        private System.Windows.Forms.NumericUpDown numGarantia;
        private System.Windows.Forms.Label lblValidationMessage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlterarProduto));
            this.lblProdutos = new System.Windows.Forms.Label();
            this.cmbProdutos = new System.Windows.Forms.ComboBox();
            this.lblNomeProd = new System.Windows.Forms.Label();
            this.txtNomeProd = new System.Windows.Forms.TextBox();
            this.lblIDMarca = new System.Windows.Forms.Label();
            this.cmbIDMarca = new System.Windows.Forms.ComboBox();
            this.lblIDCategoria = new System.Windows.Forms.Label();
            this.cmbIDCategoria = new System.Windows.Forms.ComboBox();
            this.lblValorPreco = new System.Windows.Forms.Label();
            this.txtValorPreco = new System.Windows.Forms.TextBox();
            this.lblValorPromocional = new System.Windows.Forms.Label();
            this.txtValorPromocional = new System.Windows.Forms.TextBox();
            this.lblQtdEstoque = new System.Windows.Forms.Label();
            this.txtQtdEstoque = new System.Windows.Forms.TextBox();
            this.lblPesoKG = new System.Windows.Forms.Label();
            this.txtPesoKG = new System.Windows.Forms.TextBox();
            this.lblStatusProduto = new System.Windows.Forms.Label();
            this.cmbStatusProduto = new System.Windows.Forms.ComboBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDescricao = new System.Windows.Forms.TabPage();
            this.lblEspecificacoes = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.txtEspecificacoes = new System.Windows.Forms.TextBox();
            this.numGarantia = new System.Windows.Forms.NumericUpDown();
            this.lblValidationMessage = new System.Windows.Forms.Label();
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURLImagem = new System.Windows.Forms.TextBox();
            this.lblGarantia = new System.Windows.Forms.Label();
            this.lblPreviewImagem = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.picImagemSite = new System.Windows.Forms.PictureBox();
            this.tabControl.SuspendLayout();
            this.tabDescricao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGarantia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagemSite)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProdutos
            // 
            this.lblProdutos.AutoSize = true;
            this.lblProdutos.Location = new System.Drawing.Point(15, 5);
            this.lblProdutos.Name = "lblProdutos";
            this.lblProdutos.Size = new System.Drawing.Size(49, 13);
            this.lblProdutos.TabIndex = 25;
            this.lblProdutos.Text = "Produtos";
            // 
            // cmbProdutos
            // 
            this.cmbProdutos.FormattingEnabled = true;
            this.cmbProdutos.Location = new System.Drawing.Point(15, 21);
            this.cmbProdutos.Name = "cmbProdutos";
            this.cmbProdutos.Size = new System.Drawing.Size(281, 21);
            this.cmbProdutos.TabIndex = 26;
            // 
            // lblNomeProd
            // 
            this.lblNomeProd.AutoSize = true;
            this.lblNomeProd.Location = new System.Drawing.Point(15, 95);
            this.lblNomeProd.Name = "lblNomeProd";
            this.lblNomeProd.Size = new System.Drawing.Size(90, 13);
            this.lblNomeProd.TabIndex = 3;
            this.lblNomeProd.Text = "Nome do Produto";
            // 
            // txtNomeProd
            // 
            this.txtNomeProd.Location = new System.Drawing.Point(15, 111);
            this.txtNomeProd.Name = "txtNomeProd";
            this.txtNomeProd.Size = new System.Drawing.Size(281, 20);
            this.txtNomeProd.TabIndex = 4;
            // 
            // lblIDMarca
            // 
            this.lblIDMarca.AutoSize = true;
            this.lblIDMarca.Location = new System.Drawing.Point(12, 185);
            this.lblIDMarca.Name = "lblIDMarca";
            this.lblIDMarca.Size = new System.Drawing.Size(37, 13);
            this.lblIDMarca.TabIndex = 7;
            this.lblIDMarca.Text = "Marca";
            // 
            // cmbIDMarca
            // 
            this.cmbIDMarca.FormattingEnabled = true;
            this.cmbIDMarca.Location = new System.Drawing.Point(12, 201);
            this.cmbIDMarca.Name = "cmbIDMarca";
            this.cmbIDMarca.Size = new System.Drawing.Size(103, 21);
            this.cmbIDMarca.TabIndex = 8;
            // 
            // lblIDCategoria
            // 
            this.lblIDCategoria.AutoSize = true;
            this.lblIDCategoria.Location = new System.Drawing.Point(134, 185);
            this.lblIDCategoria.Name = "lblIDCategoria";
            this.lblIDCategoria.Size = new System.Drawing.Size(52, 13);
            this.lblIDCategoria.TabIndex = 9;
            this.lblIDCategoria.Text = "Categoria";
            // 
            // cmbIDCategoria
            // 
            this.cmbIDCategoria.FormattingEnabled = true;
            this.cmbIDCategoria.Location = new System.Drawing.Point(134, 201);
            this.cmbIDCategoria.Name = "cmbIDCategoria";
            this.cmbIDCategoria.Size = new System.Drawing.Size(134, 21);
            this.cmbIDCategoria.TabIndex = 10;
            // 
            // lblValorPreco
            // 
            this.lblValorPreco.AutoSize = true;
            this.lblValorPreco.Location = new System.Drawing.Point(15, 230);
            this.lblValorPreco.Name = "lblValorPreco";
            this.lblValorPreco.Size = new System.Drawing.Size(35, 13);
            this.lblValorPreco.TabIndex = 11;
            this.lblValorPreco.Text = "Preço";
            // 
            // txtValorPreco
            // 
            this.txtValorPreco.Location = new System.Drawing.Point(15, 246);
            this.txtValorPreco.Name = "txtValorPreco";
            this.txtValorPreco.Size = new System.Drawing.Size(100, 20);
            this.txtValorPreco.TabIndex = 12;
            // 
            // lblValorPromocional
            // 
            this.lblValorPromocional.AutoSize = true;
            this.lblValorPromocional.Location = new System.Drawing.Point(134, 230);
            this.lblValorPromocional.Name = "lblValorPromocional";
            this.lblValorPromocional.Size = new System.Drawing.Size(86, 13);
            this.lblValorPromocional.TabIndex = 13;
            this.lblValorPromocional.Text = "Preço Promoção";
            // 
            // txtValorPromocional
            // 
            this.txtValorPromocional.Location = new System.Drawing.Point(134, 246);
            this.txtValorPromocional.Name = "txtValorPromocional";
            this.txtValorPromocional.Size = new System.Drawing.Size(100, 20);
            this.txtValorPromocional.TabIndex = 14;
            // 
            // lblQtdEstoque
            // 
            this.lblQtdEstoque.AutoSize = true;
            this.lblQtdEstoque.Location = new System.Drawing.Point(15, 275);
            this.lblQtdEstoque.Name = "lblQtdEstoque";
            this.lblQtdEstoque.Size = new System.Drawing.Size(46, 13);
            this.lblQtdEstoque.TabIndex = 15;
            this.lblQtdEstoque.Text = "Estoque";
            // 
            // txtQtdEstoque
            // 
            this.txtQtdEstoque.Location = new System.Drawing.Point(15, 291);
            this.txtQtdEstoque.Name = "txtQtdEstoque";
            this.txtQtdEstoque.Size = new System.Drawing.Size(100, 20);
            this.txtQtdEstoque.TabIndex = 16;
            // 
            // lblPesoKG
            // 
            this.lblPesoKG.AutoSize = true;
            this.lblPesoKG.Location = new System.Drawing.Point(134, 275);
            this.lblPesoKG.Name = "lblPesoKG";
            this.lblPesoKG.Size = new System.Drawing.Size(31, 13);
            this.lblPesoKG.TabIndex = 17;
            this.lblPesoKG.Text = "Peso";
            // 
            // txtPesoKG
            // 
            this.txtPesoKG.Location = new System.Drawing.Point(134, 291);
            this.txtPesoKG.Name = "txtPesoKG";
            this.txtPesoKG.Size = new System.Drawing.Size(100, 20);
            this.txtPesoKG.TabIndex = 18;
            // 
            // lblStatusProduto
            // 
            this.lblStatusProduto.AutoSize = true;
            this.lblStatusProduto.Location = new System.Drawing.Point(15, 365);
            this.lblStatusProduto.Name = "lblStatusProduto";
            this.lblStatusProduto.Size = new System.Drawing.Size(37, 13);
            this.lblStatusProduto.TabIndex = 21;
            this.lblStatusProduto.Text = "Status";
            // 
            // cmbStatusProduto
            // 
            this.cmbStatusProduto.FormattingEnabled = true;
            this.cmbStatusProduto.Location = new System.Drawing.Point(15, 381);
            this.cmbStatusProduto.Name = "cmbStatusProduto";
            this.cmbStatusProduto.Size = new System.Drawing.Size(121, 21);
            this.cmbStatusProduto.TabIndex = 22;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(15, 420);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.TabIndex = 23;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(121, 420);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 24;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDescricao);
            this.tabControl.Location = new System.Drawing.Point(320, 20);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(420, 257);
            this.tabControl.TabIndex = 30;
            // 
            // tabDescricao
            // 
            this.tabDescricao.Controls.Add(this.lblEspecificacoes);
            this.tabDescricao.Controls.Add(this.txtDescricao);
            this.tabDescricao.Controls.Add(this.txtEspecificacoes);
            this.tabDescricao.Location = new System.Drawing.Point(4, 22);
            this.tabDescricao.Name = "tabDescricao";
            this.tabDescricao.Padding = new System.Windows.Forms.Padding(3);
            this.tabDescricao.Size = new System.Drawing.Size(412, 231);
            this.tabDescricao.TabIndex = 0;
            this.tabDescricao.Text = "Descrição";
            this.tabDescricao.UseVisualStyleBackColor = true;
            // 
            // lblEspecificacoes
            // 
            this.lblEspecificacoes.AutoSize = true;
            this.lblEspecificacoes.Location = new System.Drawing.Point(6, 92);
            this.lblEspecificacoes.Name = "lblEspecificacoes";
            this.lblEspecificacoes.Size = new System.Drawing.Size(79, 13);
            this.lblEspecificacoes.TabIndex = 31;
            this.lblEspecificacoes.Text = "Especificações";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(8, 6);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(392, 83);
            this.txtDescricao.TabIndex = 0;
            // 
            // txtEspecificacoes
            // 
            this.txtEspecificacoes.Location = new System.Drawing.Point(9, 108);
            this.txtEspecificacoes.Multiline = true;
            this.txtEspecificacoes.Name = "txtEspecificacoes";
            this.txtEspecificacoes.Size = new System.Drawing.Size(392, 114);
            this.txtEspecificacoes.TabIndex = 1;
            // 
            // numGarantia
            // 
            this.numGarantia.Location = new System.Drawing.Point(230, 436);
            this.numGarantia.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numGarantia.Name = "numGarantia";
            this.numGarantia.Size = new System.Drawing.Size(80, 20);
            this.numGarantia.TabIndex = 2;
            // 
            // lblValidationMessage
            // 
            this.lblValidationMessage.Location = new System.Drawing.Point(0, 0);
            this.lblValidationMessage.Name = "lblValidationMessage";
            this.lblValidationMessage.Size = new System.Drawing.Size(100, 23);
            this.lblValidationMessage.TabIndex = 0;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(321, 280);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(69, 13);
            this.lblURL.TabIndex = 33;
            this.lblURL.Text = "URL Imagem";
            // 
            // txtURLImagem
            // 
            this.txtURLImagem.Location = new System.Drawing.Point(320, 296);
            this.txtURLImagem.Multiline = true;
            this.txtURLImagem.Name = "txtURLImagem";
            this.txtURLImagem.Size = new System.Drawing.Size(392, 23);
            this.txtURLImagem.TabIndex = 32;
            // 
            // lblGarantia
            // 
            this.lblGarantia.AutoSize = true;
            this.lblGarantia.Location = new System.Drawing.Point(227, 420);
            this.lblGarantia.Name = "lblGarantia";
            this.lblGarantia.Size = new System.Drawing.Size(76, 13);
            this.lblGarantia.TabIndex = 34;
            this.lblGarantia.Text = "Garantia (Mês)";
            // 
            // lblPreviewImagem
            // 
            this.lblPreviewImagem.AutoSize = true;
            this.lblPreviewImagem.Location = new System.Drawing.Point(321, 322);
            this.lblPreviewImagem.Name = "lblPreviewImagem";
            this.lblPreviewImagem.Size = new System.Drawing.Size(68, 13);
            this.lblPreviewImagem.TabIndex = 35;
            this.lblPreviewImagem.Text = "Preview IMG";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "Preview";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // picImagemSite
            // 
            this.picImagemSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImagemSite.Location = new System.Drawing.Point(396, 322);
            this.picImagemSite.Name = "picImagemSite";
            this.picImagemSite.Size = new System.Drawing.Size(316, 143);
            this.picImagemSite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImagemSite.TabIndex = 37;
            this.picImagemSite.TabStop = false;
            // 
            // AlterarProduto
            // 
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.imagem;
            this.ClientSize = new System.Drawing.Size(753, 477);
            this.Controls.Add(this.picImagemSite);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPreviewImagem);
            this.Controls.Add(this.lblGarantia);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtURLImagem);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.numGarantia);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.cmbStatusProduto);
            this.Controls.Add(this.lblStatusProduto);
            this.Controls.Add(this.txtPesoKG);
            this.Controls.Add(this.lblPesoKG);
            this.Controls.Add(this.txtQtdEstoque);
            this.Controls.Add(this.lblQtdEstoque);
            this.Controls.Add(this.txtValorPromocional);
            this.Controls.Add(this.lblValorPromocional);
            this.Controls.Add(this.txtValorPreco);
            this.Controls.Add(this.lblValorPreco);
            this.Controls.Add(this.cmbIDCategoria);
            this.Controls.Add(this.lblIDCategoria);
            this.Controls.Add(this.cmbIDMarca);
            this.Controls.Add(this.lblIDMarca);
            this.Controls.Add(this.txtNomeProd);
            this.Controls.Add(this.lblNomeProd);
            this.Controls.Add(this.cmbProdutos);
            this.Controls.Add(this.lblProdutos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AlterarProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alterar Produto";
            this.tabControl.ResumeLayout(false);
            this.tabDescricao.ResumeLayout(false);
            this.tabDescricao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGarantia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagemSite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblEspecificacoes;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURLImagem;
        private System.Windows.Forms.Label lblGarantia;
        private System.Windows.Forms.Label lblPreviewImagem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox picImagemSite;
    }
}
