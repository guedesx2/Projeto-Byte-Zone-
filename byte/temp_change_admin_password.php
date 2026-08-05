<?php
require 'config/database.php';
$pdo = db();
$hash = password_hash('adm123', PASSWORD_DEFAULT);
$stmt = $pdo->prepare('UPDATE tbl_Usuarios SET SenhaHash = ? WHERE NomeUsuario = ? AND LOWER(TipoUsuario) = LOWER(?)');
$stmt->execute([$hash, 'admin', 'admin']);
$affected = $stmt->rowCount();
echo $affected . PHP_EOL;
