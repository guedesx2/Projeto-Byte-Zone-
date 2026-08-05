<?php
require 'config/database.php';
$pdo = db();
$pdo->beginTransaction();

$customer = (int) $pdo->query('SELECT ID_Cliente FROM tbl_Clientes ORDER BY ID_Cliente LIMIT 1')->fetchColumn();
$saleUserId = (int) $pdo->query('SELECT ID_Usuario FROM tbl_Usuarios WHERE LOWER(TipoUsuario) = LOWER("admin") ORDER BY ID_Usuario LIMIT 1')->fetchColumn();
$payment = (int) $pdo->query('SELECT ID_Forma FROM tbl_FormaPagamento ORDER BY ID_Forma LIMIT 1')->fetchColumn();
$address = (int) $pdo->query('SELECT ID_Endereco FROM tbl_Enderecos WHERE ID_Cliente = ' . $customer . ' ORDER BY ID_Endereco DESC LIMIT 1')->fetchColumn();
if (!$address) {
    $pdo->prepare('INSERT INTO tbl_Enderecos (ID_Cliente, Rua_Cliente, Num_Cliente, Comple_Cliente, Bairro_Cliente, Cidade_Cliente, Est_Cliente, CEP_Cliente) VALUES (?, ?, ?, ?, ?, ?, ?, ?)')
        ->execute([$customer, 'Rua Teste', '123', 'Casa', 'Centro', 'Cidade', 'SP', '01000000']);
    $address = (int) $pdo->lastInsertId();
}
$total = (float) $pdo->query('SELECT COALESCE(SUM(Valor_Preco),0) FROM tbl_Produtos LIMIT 1')->fetchColumn();
$insert = $pdo->prepare("INSERT INTO tbl_Vendas (ID_Cliente,ID_Usuario,ID_Forma,ID_Endereco,StatusVenda,ValorTotal) VALUES (?,?,?,?, 'Pendente',?)");
$insert->execute([$customer, $saleUserId, $payment, $address, $total]);
$pdo->rollBack();

echo 'CUSTOMER=' . $customer . PHP_EOL;
echo 'SALE_USER=' . $saleUserId . PHP_EOL;
echo 'PAYMENT=' . $payment . PHP_EOL;
echo 'ADDRESS=' . $address . PHP_EOL;
echo 'TOTAL=' . $total . PHP_EOL;
echo 'CHECKOUT_OK' . PHP_EOL;
