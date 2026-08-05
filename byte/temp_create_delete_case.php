<?php
require 'config/database.php';
$pdo = db();
$brand = (int) $pdo->query('SELECT ID_Marca FROM tbl_Marca ORDER BY ID_Marca LIMIT 1')->fetchColumn();
$category = (int) $pdo->query('SELECT ID_Categoria FROM tbl_Categoria ORDER BY ID_Categoria LIMIT 1')->fetchColumn();
$customer = (int) $pdo->query('SELECT ID_Cliente FROM tbl_Clientes ORDER BY ID_Cliente LIMIT 1')->fetchColumn();
$tempName = 'TMP_TEST_DELETE_' . uniqid();
$status = 'Disponível';
$insertProduct = $pdo->prepare('INSERT INTO tbl_Produtos (Nome_Prod, ID_Marca, ID_Categoria, Valor_Preco, QtdEstoque, PesoKG, Slug, StatusProduto) VALUES (?, ?, ?, 1000.00, 5, 2.5, ?, ?)');
$insertProduct->execute([$tempName, $brand, $category, strtolower($tempName), $status]);
$productId = (int) $pdo->lastInsertId();
$insertCart = $pdo->prepare('INSERT INTO tbl_Carrinho (ID_Cliente) VALUES (?)');
$insertCart->execute([$customer]);
$cartId = (int) $pdo->lastInsertId();
$insertItem = $pdo->prepare('INSERT INTO tbl_CarrinhoItens (ID_Carrinho, ID_Produto, Quantidade) VALUES (?, ?, ?)');
$insertItem->execute([$cartId, $productId, 1]);
echo $productId . PHP_EOL;
