<?php
require 'config/database.php';
$pdo = db();
$pass = 'Admin@123';
$hash = password_hash($pass, PASSWORD_DEFAULT);
$stmt = $pdo->prepare("UPDATE tbl_Usuarios SET SenhaHash = ? WHERE NomeUsuario = ?");
$stmt->execute([$hash, 'admin']);
$login = 'admin';
$senha = 'Admin@123';
$q = $pdo->prepare('SELECT * FROM tbl_Usuarios WHERE LOWER(NomeUsuario) = LOWER(?) AND LOWER(TipoUsuario) = LOWER(?) LIMIT 1');
$q->execute([$login, 'admin']);
$user = $q->fetch();
echo ($user ? 'FOUND' : 'NOT_FOUND') . PHP_EOL;
echo (password_verify($senha, $user['SenhaHash']) ? 'VERIFY_OK' : 'VERIFY_FAIL') . PHP_EOL;
