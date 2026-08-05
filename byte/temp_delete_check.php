<?php
require 'config/database.php';
$pdo = db();
$pdo->beginTransaction();

$productId = (int) $pdo->query('SELECT ID_Produto FROM tbl_Produtos ORDER BY ID_Produto LIMIT 1')->fetchColumn();
$before = (int) $pdo->query('SELECT COUNT(*) FROM tbl_Produtos WHERE ID_Produto = ' . $productId)->fetchColumn();
$pdo->prepare('DELETE FROM tbl_CarrinhoItens WHERE ID_Produto = ?')->execute([$productId]);
$pdo->prepare('DELETE FROM tbl_Produtos WHERE ID_Produto = ?')->execute([$productId]);
$after = (int) $pdo->query('SELECT COUNT(*) FROM tbl_Produtos WHERE ID_Produto = ' . $productId)->fetchColumn();

echo 'BEFORE=' . $before . PHP_EOL;
echo 'AFTER=' . $after . PHP_EOL;
$pdo->rollBack();
