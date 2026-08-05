CREATE DATABASE IF NOT EXISTS db_Tec CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE db_Tec;

CREATE TABLE tbl_Clientes (
  ID_Cliente INT AUTO_INCREMENT PRIMARY KEY,
  Nome_Cliente VARCHAR(100) NOT NULL,
  Sobr_Cliente VARCHAR(60),
  Email_Cliente VARCHAR(100) NOT NULL UNIQUE,
  Tel_Cliente VARCHAR(20),
  CPF_Cliente VARCHAR(14) UNIQUE,
  Senha_Hash VARCHAR(255) NOT NULL,
  Data_nascimento DATE,
  DataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE IF NOT EXISTS tbl_Usuarios (
  ID_Usuario INT AUTO_INCREMENT PRIMARY KEY,
  NomeUsuario VARCHAR(50) NOT NULL UNIQUE,
  SenhaHash VARCHAR(255) NOT NULL,
  TipoUsuario VARCHAR(20) NOT NULL DEFAULT 'admin',
  CHECK (TipoUsuario IN ('admin','Admin'))
);
CREATE TABLE tbl_Enderecos (ID_Endereco INT AUTO_INCREMENT PRIMARY KEY, ID_Cliente INT NOT NULL, Rua_Cliente VARCHAR(150) NOT NULL, Num_Cliente VARCHAR(10) NOT NULL, Comple_Cliente VARCHAR(50), Bairro_Cliente VARCHAR(80), Cidade_Cliente VARCHAR(80), Est_Cliente CHAR(2), CEP_Cliente VARCHAR(10) NOT NULL, FOREIGN KEY (ID_Cliente) REFERENCES tbl_Clientes(ID_Cliente) ON DELETE CASCADE);
CREATE TABLE tbl_Categoria (ID_Categoria SMALLINT AUTO_INCREMENT PRIMARY KEY, NomeCategoria VARCHAR(50) NOT NULL UNIQUE);
INSERT INTO tbl_Categoria (NomeCategoria) VALUES ('Notebook'),('PC Gamer'),('Desktop');
CREATE TABLE tbl_Marca (ID_Marca SMALLINT AUTO_INCREMENT PRIMARY KEY, NomeMarca VARCHAR(50) NOT NULL UNIQUE);
CREATE TABLE tbl_Produtos (ID_Produto SMALLINT AUTO_INCREMENT PRIMARY KEY, Nome_Prod VARCHAR(150) NOT NULL, ID_Marca SMALLINT NOT NULL, ID_Categoria SMALLINT NOT NULL, Valor_Preco DECIMAL(10,2) NOT NULL, ValorPromocional DECIMAL(10,2), QtdEstoque SMALLINT NOT NULL, PesoKG DECIMAL(5,2) NOT NULL, Slug VARCHAR(200), StatusProduto VARCHAR(20) DEFAULT 'Disponível', DataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP, CHECK (QtdEstoque >= 0), CHECK (StatusProduto IN ('Disponível','Esgotado')), FOREIGN KEY (ID_Marca) REFERENCES tbl_Marca(ID_Marca), FOREIGN KEY (ID_Categoria) REFERENCES tbl_Categoria(ID_Categoria));
CREATE TABLE tbl_DescricaoProduto (ID_Descricao INT AUTO_INCREMENT PRIMARY KEY, ID_Produto SMALLINT NOT NULL UNIQUE, Descricao TEXT NOT NULL, Especificacoes TEXT, GarantiaMeses SMALLINT, FOREIGN KEY (ID_Produto) REFERENCES tbl_Produtos(ID_Produto) ON DELETE CASCADE);
CREATE TABLE tbl_ProdutoImagem (ID_Imagem INT AUTO_INCREMENT PRIMARY KEY, ID_Produto SMALLINT NOT NULL, UrlImagem TEXT NOT NULL, Principal BOOLEAN DEFAULT 0, FOREIGN KEY (ID_Produto) REFERENCES tbl_Produtos(ID_Produto) ON DELETE CASCADE);
CREATE TABLE tbl_FormaPagamento (ID_Forma SMALLINT AUTO_INCREMENT PRIMARY KEY, NomeForma VARCHAR(30) NOT NULL);
INSERT INTO tbl_FormaPagamento (NomeForma) VALUES ('PIX'),('Cartão de Crédito'),('Cartão de Débito'),('Boleto');
CREATE TABLE tbl_Carrinho (ID_Carrinho INT AUTO_INCREMENT PRIMARY KEY, ID_Cliente INT NOT NULL, FOREIGN KEY (ID_Cliente) REFERENCES tbl_Clientes(ID_Cliente) ON DELETE CASCADE);
CREATE TABLE tbl_CarrinhoItens (ID_ItemCarrinho INT AUTO_INCREMENT PRIMARY KEY, ID_Carrinho INT NOT NULL, ID_Produto SMALLINT NOT NULL, Quantidade SMALLINT NOT NULL, CHECK (Quantidade > 0), FOREIGN KEY (ID_Carrinho) REFERENCES tbl_Carrinho(ID_Carrinho) ON DELETE CASCADE, FOREIGN KEY (ID_Produto) REFERENCES tbl_Produtos(ID_Produto));
CREATE TABLE tbl_Vendas (ID_Venda SMALLINT AUTO_INCREMENT PRIMARY KEY, ID_Cliente INT NOT NULL, ID_Usuario INT NOT NULL, ID_Forma SMALLINT, ID_Endereco INT, DataVenda DATETIME DEFAULT CURRENT_TIMESTAMP, StatusVenda VARCHAR(20) DEFAULT 'Pendente', ValorTotal DECIMAL(10,2), CHECK (StatusVenda IN ('Pendente','Pago','Enviado','Entregue','Cancelado')), FOREIGN KEY (ID_Cliente) REFERENCES tbl_Clientes(ID_Cliente) ON DELETE CASCADE, FOREIGN KEY (ID_Forma) REFERENCES tbl_FormaPagamento(ID_Forma), FOREIGN KEY (ID_Endereco) REFERENCES tbl_Enderecos(ID_Endereco));
INSERT INTO tbl_Marca (NomeMarca) VALUES ('Apple'),('Lenovo'),('Dell'),('ASUS'),('Gigabyte'),('Acer'),('Sony'),('Xiaomi'),('VAIO'),('Samsung'),('HP'),('Positivo');
