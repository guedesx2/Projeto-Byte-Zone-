<?php
require 'config/database.php';
require 'includes/functions.php';
$pdo = db();
$pdo->beginTransaction();

$brand = (int) $pdo->query('SELECT ID_Marca FROM tbl_Marca ORDER BY ID_Marca LIMIT 1')->fetchColumn();
$category = (int) $pdo->query('SELECT ID_Categoria FROM tbl_Categoria ORDER BY ID_Categoria LIMIT 1')->fetchColumn();
$customer = (int) $pdo->query('SELECT ID_Cliente FROM tbl_Clientes ORDER BY ID_Cliente LIMIT 1')->fetchColumn();
$tempName = 'TMP_DEL_' . uniqid();

$insertProduct = $pdo->prepare('INSERT INTO tbl_Produtos (Nome_Prod, ID_Marca, ID_Categoria, Valor_Preco, QtdEstoque, PesoKG, Slug, StatusProduto) VALUES (?, ?, ?, 1000.00, 5, 2.5, ?, "Disponível")');
$insertProduct->execute([$tempName, $brand, $category, strtolower($tempName)]);
$productId = (int) $pdo->lastInsertId();

$cart = $pdo->prepare('INSERT INTO tbl_Carrinho (ID_Cliente) VALUES (?)');
$cart->execute([$customer]);
$cartId = (int) $pdo->lastInsertId();

$item = $pdo->prepare('INSERT INTO tbl_CarrinhoItens (ID_Carrinho, ID_Produto, Quantidade) VALUES (?, ?, ?)');
$item->execute([$cartId, $productId, 1]);

$deleteItems = $pdo->prepare('DELETE FROM tbl_CarrinhoItens WHERE ID_Produto = ?');
$deleteItems->execute([$productId]);
$deleteProduct = $pdo->prepare('DELETE FROM tbl_Produtos WHERE ID_Produto = ?');
$deleteProduct->execute([$productId]);

$remainingProduct = (int) $pdo->query("SELECT COUNT(*) FROM tbl_Produtos WHERE ID_Produto = {$productId}")->fetchColumn();
$remainingItem = (int) $pdo->query("SELECT COUNT(*) FROM tbl_CarrinhoItens WHERE ID_Produto = {$productId}")->fetchColumn();

echo 'PRODUCT=' . $remainingProduct . PHP_EOL;
echo 'ITEM=' . $remainingItem . PHP_EOL;
$pdo->rollBack();
