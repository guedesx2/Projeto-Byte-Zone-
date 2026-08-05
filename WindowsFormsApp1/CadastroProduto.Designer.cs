namespace WindowsFormsApp1
{
    partial class CadastroProduto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.Label lblNomeProd;
        private System.Windows.Forms.TextBox txtNomeProd;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.ComboBox cmbMarca;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label lblValorPreco;
        private System.Windows.Forms.TextBox txtValorPreco;
        private System.Windows.Forms.Label lblValorPromocional;
        private System.Windows.Forms.TextBox txtValorPromocional;
        private System.Windows.Forms.Label lblQtdEstoque;
        private System.Windows.Forms.TextBox txtQtdEstoque;
        private System.Windows.Forms.Label lblPesoKG;
        private System.Windows.Forms.TextBox txtPesoKG;
        private System.Windows.Forms.Label lblSlug;
        private System.Windows.Forms.TextBox txtSlug;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblUrlImagem;
        private System.Windows.Forms.TextBox txtUrlImagem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblValidationMessage;
        private System.Windows.Forms.NumericUpDown numGarantia;
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroProduto));
            this.lblNomeProd = new System.Windows.Forms.Label();
            this.txtNomeProd = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.cmbMarca = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.lblValorPreco = new System.Windows.Forms.Label();
            this.txtValorPreco = new System.Windows.Forms.TextBox();
            this.lblValorPromocional = new System.Windows.Forms.Label();
            this.txtValorPromocional = new System.Windows.Forms.TextBox();
            this.lblQtdEstoque = new System.Windows.Forms.Label();
            this.txtQtdEstoque = new System.Windows.Forms.TextBox();
            this.lblPesoKG = new System.Windows.Forms.Label();
            this.txtPesoKG = new System.Windows.Forms.TextBox();
            this.lblSlug = new System.Windows.Forms.Label();
            this.txtSlug = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblUrlImagem = new System.Windows.Forms.Label();
            this.txtUrlImagem = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.lblValidationMessage = new System.Windows.Forms.Label();
            this.numGarantia = new System.Windows.Forms.NumericUpDown();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lblEspecificacoes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEspecificacoes = new System.Windows.Forms.TextBox();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGarantia)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNomeProd
            // 
            this.lblNomeProd.AutoSize = true;
            this.lblNomeProd.Location = new System.Drawing.Point(20, 20);
            this.lblNomeProd.Name = "lblNomeProd";
            this.lblNomeProd.Size = new System.Drawing.Size(90, 13);
            this.lblNomeProd.TabIndex = 0;
            this.lblNomeProd.Text = "Nome do Produto";
            // 
            // txtNomeProd
            // 
            this.txtNomeProd.Location = new System.Drawing.Point(20, 36);
            this.txtNomeProd.Name = "txtNomeProd";
            this.txtNomeProd.Size = new System.Drawing.Size(360, 20);
            this.txtNomeProd.TabIndex = 1;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(400, 20);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(37, 13);
            this.lblMarca.TabIndex = 2;
            this.lblMarca.Text = "Marca";
            // 
            // cmbMarca
            // 
            this.cmbMarca.Location = new System.Drawing.Point(400, 36);
            this.cmbMarca.Name = "cmbMarca";
            this.cmbMarca.Size = new System.Drawing.Size(200, 21);
            this.cmbMarca.TabIndex = 3;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(20, 70);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(52, 13);
            this.lblCategoria.TabIndex = 4;
            this.lblCategoria.Text = "Categoria";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Location = new System.Drawing.Point(20, 86);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(200, 21);
            this.cmbCategoria.TabIndex = 5;
            // 
            // lblValorPreco
            // 
            this.lblValorPreco.AutoSize = true;
            this.lblValorPreco.Location = new System.Drawing.Point(240, 70);
            this.lblValorPreco.Name = "lblValorPreco";
            this.lblValorPreco.Size = new System.Drawing.Size(35, 13);
            this.lblValorPreco.TabIndex = 6;
            this.lblValorPreco.Text = "Preço";
            // 
            // txtValorPreco
            // 
            this.txtValorPreco.Location = new System.Drawing.Point(240, 86);
            this.txtValorPreco.Name = "txtValorPreco";
            this.txtValorPreco.Size = new System.Drawing.Size(100, 20);
            this.txtValorPreco.TabIndex = 7;
            // 
            // lblValorPromocional
            // 
            this.lblValorPromocional.AutoSize = true;
            this.lblValorPromocional.Location = new System.Drawing.Point(360, 70);
            this.lblValorPromocional.Name = "lblValorPromocional";
            this.lblValorPromocional.Size = new System.Drawing.Size(86, 13);
            this.lblValorPromocional.TabIndex = 8;
            this.lblValorPromocional.Text = "Preço Promoção";
            // 
            // txtValorPromocional
            // 
            this.txtValorPromocional.Location = new System.Drawing.Point(360, 86);
            this.txtValorPromocional.Name = "txtValorPromocional";
            this.txtValorPromocional.Size = new System.Drawing.Size(100, 20);
            this.txtValorPromocional.TabIndex = 9;
            // 
            // lblQtdEstoque
            // 
            this.lblQtdEstoque.AutoSize = true;
            this.lblQtdEstoque.Location = new System.Drawing.Point(480, 70);
            this.lblQtdEstoque.Name = "lblQtdEstoque";
            this.lblQtdEstoque.Size = new System.Drawing.Size(46, 13);
            this.lblQtdEstoque.TabIndex = 10;
            this.lblQtdEstoque.Text = "Estoque";
            // 
            // txtQtdEstoque
            // 
            this.txtQtdEstoque.Location = new System.Drawing.Point(480, 86);
            this.txtQtdEstoque.Name = "txtQtdEstoque";
            this.txtQtdEstoque.Size = new System.Drawing.Size(80, 20);
            this.txtQtdEstoque.TabIndex = 11;
            // 
            // lblPesoKG
            // 
            this.lblPesoKG.AutoSize = true;
            this.lblPesoKG.Location = new System.Drawing.Point(580, 70);
            this.lblPesoKG.Name = "lblPesoKG";
            this.lblPesoKG.Size = new System.Drawing.Size(31, 13);
            this.lblPesoKG.TabIndex = 12;
            this.lblPesoKG.Text = "Peso";
            // 
            // txtPesoKG
            // 
            this.txtPesoKG.Location = new System.Drawing.Point(580, 86);
            this.txtPesoKG.Name = "txtPesoKG";
            this.txtPesoKG.Size = new System.Drawing.Size(80, 20);
            this.txtPesoKG.TabIndex = 13;
            // 
            // lblSlug
            // 
            this.lblSlug.AutoSize = true;
            this.lblSlug.Location = new System.Drawing.Point(20, 120);
            this.lblSlug.Name = "lblSlug";
            this.lblSlug.Size = new System.Drawing.Size(28, 13);
            this.lblSlug.TabIndex = 14;
            this.lblSlug.Text = "Slug";
            // 
            // txtSlug
            // 
            this.txtSlug.Location = new System.Drawing.Point(20, 136);
            this.txtSlug.Name = "txtSlug";
            this.txtSlug.Size = new System.Drawing.Size(200, 20);
            this.txtSlug.TabIndex = 15;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(240, 120);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Items.AddRange(new object[] {
            "Disponível",
            "Esgotado"});
            this.cmbStatus.Location = new System.Drawing.Point(240, 136);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 21);
            this.cmbStatus.TabIndex = 17;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblUrlImagem
            // 
            this.lblUrlImagem.AutoSize = true;
            this.lblUrlImagem.Location = new System.Drawing.Point(20, 170);
            this.lblUrlImagem.Name = "lblUrlImagem";
            this.lblUrlImagem.Size = new System.Drawing.Size(84, 13);
            this.lblUrlImagem.TabIndex = 18;
            this.lblUrlImagem.Text = "URL da Imagem";
            // 
            // txtUrlImagem
            // 
            this.txtUrlImagem.Location = new System.Drawing.Point(20, 186);
            this.txtUrlImagem.Name = "txtUrlImagem";
            this.txtUrlImagem.Size = new System.Drawing.Size(540, 20);
            this.txtUrlImagem.TabIndex = 19;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(680, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Salvar";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(760, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancelar";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(483, 156);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(80, 24);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(20, 233);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(260, 180);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 22;
            this.picPreview.TabStop = false;
            // 
            // lblValidationMessage
            // 
            this.lblValidationMessage.AutoSize = true;
            this.lblValidationMessage.ForeColor = System.Drawing.Color.Red;
            this.lblValidationMessage.Location = new System.Drawing.Point(20, 400);
            this.lblValidationMessage.Name = "lblValidationMessage";
            this.lblValidationMessage.Size = new System.Drawing.Size(0, 13);
            this.lblValidationMessage.TabIndex = 25;
            this.lblValidationMessage.Visible = false;
            // 
            // numGarantia
            // 
            this.numGarantia.Location = new System.Drawing.Point(594, 392);
            this.numGarantia.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numGarantia.Name = "numGarantia";
            this.numGarantia.Size = new System.Drawing.Size(80, 20);
            this.numGarantia.TabIndex = 31;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(596, 154);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(55, 13);
            this.lblDescricao.TabIndex = 26;
            this.lblDescricao.Text = "Descrição";
            // 
            // lblEspecificacoes
            // 
            this.lblEspecificacoes.AutoSize = true;
            this.lblEspecificacoes.Location = new System.Drawing.Point(316, 217);
            this.lblEspecificacoes.Name = "lblEspecificacoes";
            this.lblEspecificacoes.Size = new System.Drawing.Size(79, 13);
            this.lblEspecificacoes.TabIndex = 27;
            this.lblEspecificacoes.Text = "Especificações";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Preview de imagem";
            // 
            // txtEspecificacoes
            // 
            this.txtEspecificacoes.Location = new System.Drawing.Point(319, 233);
            this.txtEspecificacoes.Multiline = true;
            this.txtEspecificacoes.Name = "txtEspecificacoes";
            this.txtEspecificacoes.Size = new System.Drawing.Size(241, 179);
            this.txtEspecificacoes.TabIndex = 29;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(594, 170);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(241, 161);
            this.txtDescricao.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(596, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Garantia (Mês)";
            // 
            // CadastroProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.imagem;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(860, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.txtEspecificacoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEspecificacoes);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.lblNomeProd);
            this.Controls.Add(this.txtNomeProd);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.cmbMarca);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.lblValorPreco);
            this.Controls.Add(this.txtValorPreco);
            this.Controls.Add(this.lblValorPromocional);
            this.Controls.Add(this.txtValorPromocional);
            this.Controls.Add(this.lblQtdEstoque);
            this.Controls.Add(this.txtQtdEstoque);
            this.Controls.Add(this.lblPesoKG);
            this.Controls.Add(this.txtPesoKG);
            this.Controls.Add(this.lblSlug);
            this.Controls.Add(this.txtSlug);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblUrlImagem);
            this.Controls.Add(this.txtUrlImagem);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblValidationMessage);
            this.Controls.Add(this.numGarantia);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CadastroProduto";
            this.Text = "Cadastro de Produto";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGarantia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Label lblEspecificacoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEspecificacoes;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label2;
    }
}