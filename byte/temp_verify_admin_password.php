<?php
require 'config/database.php';
$pdo = db();
$q = $pdo->prepare('SELECT NomeUsuario, TipoUsuario, SenhaHash FROM tbl_Usuarios WHERE NomeUsuario = ? LIMIT 1');
$q->execute(['admin']);
$user = $q->fetch();
echo $user['NomeUsuario'] . ' | ' . $user['TipoUsuario'] . ' | ' . (password_verify('adm123', $user['SenhaHash']) ? 'OK' : 'FAIL') . PHP_EOL;
