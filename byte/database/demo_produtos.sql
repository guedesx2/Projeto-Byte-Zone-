USE db_Tec;
INSERT INTO tbl_Produtos (Nome_Prod,ID_Marca,ID_Categoria,Valor_Preco,ValorPromocional,QtdEstoque,PesoKG,Slug,StatusProduto) VALUES
('Byte Zone Phantom RTX 5070',4,2,8999.90,8299.90,8,12.50,'phantom-rtx-5070','Disponível'),
('Byte Zone Core i7 Creator',3,3,6499.90,NULL,12,9.80,'creator-i7','Disponível'),
('ASUS ROG Strix G16',4,1,10999.90,9999.90,5,2.50,'asus-rog-g16','Disponível'),
('Lenovo Legion 5',2,1,7199.90,6799.90,9,2.40,'lenovo-legion-5','Disponível');
INSERT INTO tbl_DescricaoProduto (ID_Produto,Descricao,Especificacoes,GarantiaMeses) VALUES
(1,'PC Gamer de alto desempenho para jogos competitivos e criação de conteúdo.','Processador: Intel Core i7\nGPU: RTX 5070\nMemória: 32GB DDR5\nArmazenamento: SSD NVMe 1TB',12),
(2,'Desktop pensado para produtividade, edição e projetos criativos.','Processador: Intel Core i7\nMemória: 32GB DDR5\nArmazenamento: SSD NVMe 1TB',12),
(3,'Notebook gamer com tela rápida e construção premium.','Tela: 16 polegadas 165Hz\nGPU: RTX 4060\nMemória: 16GB RAM\nArmazenamento: SSD 1TB',12),
(4,'Notebook para jogar, estudar e trabalhar com potência.','Tela: 15,6 polegadas 144Hz\nGPU: RTX 4050\nMemória: 16GB RAM\nArmazenamento: SSD 512GB',12);
INSERT INTO tbl_ProdutoImagem (ID_Produto,UrlImagem,Principal) VALUES
(1,'https://images.unsplash.com/photo-1587202372775-e229f172b9d7?auto=format&fit=crop&w=900&q=85',1),
(2,'https://images.unsplash.com/photo-1593640408182-31c70c8268f5?auto=format&fit=crop&w=900&q=85',1),
(3,'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=900&q=85',1),
(4,'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?auto=format&fit=crop&w=900&q=85',1);
