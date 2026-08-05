namespace WindowsFormsApp1
{
    partial class CadastroUsuario
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroUsuario));
            this.txtCadNomeUsuario = new System.Windows.Forms.TextBox();
            this.lblNomeUsuario = new System.Windows.Forms.Label();
            this.txtCadCPF = new System.Windows.Forms.TextBox();
            this.lblCadCPF = new System.Windows.Forms.Label();
            this.txtCadEmail = new System.Windows.Forms.TextBox();
            this.lblCadEmail = new System.Windows.Forms.Label();
            this.btnCadUsuario = new System.Windows.Forms.Button();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtSobrenome = new System.Windows.Forms.TextBox();
            this.lblSobrenome = new System.Windows.Forms.Label();
            this.txtConfirmSenha = new System.Windows.Forms.TextBox();
            this.lblConfirmarSenha = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConfirmSenhaAPP = new System.Windows.Forms.TextBox();
            this.lblConfirmSenhaAPP = new System.Windows.Forms.Label();
            this.txtSenhaAPP = new System.Windows.Forms.TextBox();
            this.lblSenhaAPP = new System.Windows.Forms.Label();
            this.btnCadUserAPP = new System.Windows.Forms.Button();
            this.txtNomeUsuarioAPP = new System.Windows.Forms.TextBox();
            this.lblNomeUsuarioAPP = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtDataNascimento = new System.Windows.Forms.MaskedTextBox();
            this.lblNascimento = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCadNomeUsuario
            // 
            this.txtCadNomeUsuario.Location = new System.Drawing.Point(12, 40);
            this.txtCadNomeUsuario.Name = "txtCadNomeUsuario";
            this.txtCadNomeUsuario.Size = new System.Drawing.Size(199, 20);
            this.txtCadNomeUsuario.TabIndex = 17;
            // 
            // lblNomeUsuario
            // 
            this.lblNomeUsuario.AutoSize = true;
            this.lblNomeUsuario.Location = new System.Drawing.Point(12, 24);
            this.lblNomeUsuario.Name = "lblNomeUsuario";
            this.lblNomeUsuario.Size = new System.Drawing.Size(74, 13);
            this.lblNomeUsuario.TabIndex = 16;
            this.lblNomeUsuario.Text = "Digite o Nome";
            // 
            // txtCadCPF
            // 
            this.txtCadCPF.Location = new System.Drawing.Point(13, 119);
            this.txtCadCPF.Name = "txtCadCPF";
            this.txtCadCPF.Size = new System.Drawing.Size(201, 20);
            this.txtCadCPF.TabIndex = 15;
            // 
            // lblCadCPF
            // 
            this.lblCadCPF.AutoSize = true;
            this.lblCadCPF.Location = new System.Drawing.Point(12, 102);
            this.lblCadCPF.Name = "lblCadCPF";
            this.lblCadCPF.Size = new System.Drawing.Size(66, 13);
            this.lblCadCPF.TabIndex = 14;
            this.lblCadCPF.Text = "Digite o CPF";
            // 
            // txtCadEmail
            // 
            this.txtCadEmail.Location = new System.Drawing.Point(12, 198);
            this.txtCadEmail.Name = "txtCadEmail";
            this.txtCadEmail.Size = new System.Drawing.Size(201, 20);
            this.txtCadEmail.TabIndex = 13;
            // 
            // lblCadEmail
            // 
            this.lblCadEmail.AutoSize = true;
            this.lblCadEmail.Location = new System.Drawing.Point(12, 181);
            this.lblCadEmail.Name = "lblCadEmail";
            this.lblCadEmail.Size = new System.Drawing.Size(71, 13);
            this.lblCadEmail.TabIndex = 12;
            this.lblCadEmail.Text = "Digite o Email";
            this.lblCadEmail.Click += new System.EventHandler(this.lblCadEmail_Click);
            // 
            // btnCadUsuario
            // 
            this.btnCadUsuario.Location = new System.Drawing.Point(12, 339);
            this.btnCadUsuario.Name = "btnCadUsuario";
            this.btnCadUsuario.Size = new System.Drawing.Size(199, 45);
            this.btnCadUsuario.TabIndex = 23;
            this.btnCadUsuario.Text = "Cadastrar Usuário";
            this.btnCadUsuario.UseVisualStyleBackColor = true;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(12, 277);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(199, 20);
            this.txtSenha.TabIndex = 25;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(12, 261);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(91, 13);
            this.lblSenha.TabIndex = 24;
            this.lblSenha.Text = "Digite uma Senha";
            // 
            // txtSobrenome
            // 
            this.txtSobrenome.Location = new System.Drawing.Point(12, 79);
            this.txtSobrenome.Name = "txtSobrenome";
            this.txtSobrenome.Size = new System.Drawing.Size(199, 20);
            this.txtSobrenome.TabIndex = 27;
            // 
            // lblSobrenome
            // 
            this.lblSobrenome.AutoSize = true;
            this.lblSobrenome.Location = new System.Drawing.Point(12, 63);
            this.lblSobrenome.Name = "lblSobrenome";
            this.lblSobrenome.Size = new System.Drawing.Size(100, 13);
            this.lblSobrenome.TabIndex = 26;
            this.lblSobrenome.Text = "Digite o Sobrenome";
            // 
            // txtConfirmSenha
            // 
            this.txtConfirmSenha.Location = new System.Drawing.Point(12, 316);
            this.txtConfirmSenha.Name = "txtConfirmSenha";
            this.txtConfirmSenha.Size = new System.Drawing.Size(199, 20);
            this.txtConfirmSenha.TabIndex = 29;
            // 
            // lblConfirmarSenha
            // 
            this.lblConfirmarSenha.AutoSize = true;
            this.lblConfirmarSenha.Location = new System.Drawing.Point(12, 300);
            this.lblConfirmarSenha.Name = "lblConfirmarSenha";
            this.lblConfirmarSenha.Size = new System.Drawing.Size(91, 13);
            this.lblConfirmarSenha.TabIndex = 28;
            this.lblConfirmarSenha.Text = "Confirme a Senha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "CADASTRO DO SITE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(474, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "CADASTRO DO APLICATIVO";
            // 
            // txtConfirmSenhaAPP
            // 
            this.txtConfirmSenhaAPP.Location = new System.Drawing.Point(474, 308);
            this.txtConfirmSenhaAPP.Name = "txtConfirmSenhaAPP";
            this.txtConfirmSenhaAPP.Size = new System.Drawing.Size(199, 20);
            this.txtConfirmSenhaAPP.TabIndex = 43;
            // 
            // lblConfirmSenhaAPP
            // 
            this.lblConfirmSenhaAPP.AutoSize = true;
            this.lblConfirmSenhaAPP.Location = new System.Drawing.Point(474, 292);
            this.lblConfirmSenhaAPP.Name = "lblConfirmSenhaAPP";
            this.lblConfirmSenhaAPP.Size = new System.Drawing.Size(91, 13);
            this.lblConfirmSenhaAPP.TabIndex = 42;
            this.lblConfirmSenhaAPP.Text = "Confirme a Senha";
            // 
            // txtSenhaAPP
            // 
            this.txtSenhaAPP.Location = new System.Drawing.Point(474, 269);
            this.txtSenhaAPP.Name = "txtSenhaAPP";
            this.txtSenhaAPP.Size = new System.Drawing.Size(199, 20);
            this.txtSenhaAPP.TabIndex = 39;
            // 
            // lblSenhaAPP
            // 
            this.lblSenhaAPP.AutoSize = true;
            this.lblSenhaAPP.Location = new System.Drawing.Point(474, 253);
            this.lblSenhaAPP.Name = "lblSenhaAPP";
            this.lblSenhaAPP.Size = new System.Drawing.Size(91, 13);
            this.lblSenhaAPP.TabIndex = 38;
            this.lblSenhaAPP.Text = "Digite uma Senha";
            // 
            // btnCadUserAPP
            // 
            this.btnCadUserAPP.Location = new System.Drawing.Point(474, 331);
            this.btnCadUserAPP.Name = "btnCadUserAPP";
            this.btnCadUserAPP.Size = new System.Drawing.Size(199, 45);
            this.btnCadUserAPP.TabIndex = 37;
            this.btnCadUserAPP.Text = "Cadastrar Usuário";
            this.btnCadUserAPP.UseVisualStyleBackColor = true;
            // 
            // txtNomeUsuarioAPP
            // 
            this.txtNomeUsuarioAPP.Location = new System.Drawing.Point(474, 230);
            this.txtNomeUsuarioAPP.Name = "txtNomeUsuarioAPP";
            this.txtNomeUsuarioAPP.Size = new System.Drawing.Size(199, 20);
            this.txtNomeUsuarioAPP.TabIndex = 36;
            // 
            // lblNomeUsuarioAPP
            // 
            this.lblNomeUsuarioAPP.AutoSize = true;
            this.lblNomeUsuarioAPP.Location = new System.Drawing.Point(474, 214);
            this.lblNomeUsuarioAPP.Name = "lblNomeUsuarioAPP";
            this.lblNomeUsuarioAPP.Size = new System.Drawing.Size(128, 13);
            this.lblNomeUsuarioAPP.TabIndex = 35;
            this.lblNomeUsuarioAPP.Text = "Digite o Nome de Usuário";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(12, 238);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(201, 20);
            this.txtTelefone.TabIndex = 46;
            // 
            // lblTelefone
            // 
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Location = new System.Drawing.Point(12, 221);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(88, 13);
            this.lblTelefone.TabIndex = 45;
            this.lblTelefone.Text = "Digite o Telefone";
            // 
            // txtDataNascimento
            // 
            this.txtDataNascimento.BackColor = System.Drawing.Color.White;
            this.txtDataNascimento.ForeColor = System.Drawing.Color.Black;
            this.txtDataNascimento.Location = new System.Drawing.Point(12, 159);
            this.txtDataNascimento.Mask = "00/00/0000";
            this.txtDataNascimento.Name = "txtDataNascimento";
            this.txtDataNascimento.Size = new System.Drawing.Size(201, 20);
            this.txtDataNascimento.TabIndex = 48;
            this.txtDataNascimento.ValidatingType = typeof(System.DateTime);
            // 
            // lblNascimento
            // 
            this.lblNascimento.AutoSize = true;
            this.lblNascimento.Location = new System.Drawing.Point(11, 142);
            this.lblNascimento.Name = "lblNascimento";
            this.lblNascimento.Size = new System.Drawing.Size(104, 13);
            this.lblNascimento.TabIndex = 47;
            this.lblNascimento.Text = "Data de Nascimento";
            // 
            // CadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.imagem;
            this.ClientSize = new System.Drawing.Size(685, 385);
            this.Controls.Add(this.txtDataNascimento);
            this.Controls.Add(this.lblNascimento);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConfirmSenhaAPP);
            this.Controls.Add(this.lblConfirmSenhaAPP);
            this.Controls.Add(this.txtSenhaAPP);
            this.Controls.Add(this.lblSenhaAPP);
            this.Controls.Add(this.btnCadUserAPP);
            this.Controls.Add(this.txtNomeUsuarioAPP);
            this.Controls.Add(this.lblNomeUsuarioAPP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConfirmSenha);
            this.Controls.Add(this.lblConfirmarSenha);
            this.Controls.Add(this.txtSobrenome);
            this.Controls.Add(this.lblSobrenome);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.btnCadUsuario);
            this.Controls.Add(this.txtCadNomeUsuario);
            this.Controls.Add(this.lblNomeUsuario);
            this.Controls.Add(this.txtCadCPF);
            this.Controls.Add(this.lblCadCPF);
            this.Controls.Add(this.txtCadEmail);
            this.Controls.Add(this.lblCadEmail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CadastroUsuario";
            this.Text = "CadastroUsuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCadNomeUsuario;
        private System.Windows.Forms.Label lblNomeUsuario;
        private System.Windows.Forms.TextBox txtCadCPF;
        private System.Windows.Forms.Label lblCadCPF;
        private System.Windows.Forms.TextBox txtCadEmail;
        private System.Windows.Forms.Label lblCadEmail;
        private System.Windows.Forms.Button btnCadUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtSobrenome;
        private System.Windows.Forms.Label lblSobrenome;
        private System.Windows.Forms.TextBox txtConfirmSenha;
        private System.Windows.Forms.Label lblConfirmarSenha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConfirmSenhaAPP;
        private System.Windows.Forms.Label lblConfirmSenhaAPP;
        private System.Windows.Forms.TextBox txtSenhaAPP;
        private System.Windows.Forms.Label lblSenhaAPP;
        private System.Windows.Forms.Button btnCadUserAPP;
        private System.Windows.Forms.TextBox txtNomeUsuarioAPP;
        private System.Windows.Forms.Label lblNomeUsuarioAPP;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.MaskedTextBox txtDataNascimento;
        private System.Windows.Forms.Label lblNascimento;
    }
}