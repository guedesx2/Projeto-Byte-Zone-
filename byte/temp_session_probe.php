<?php
session_start();
require 'config/database.php';
$login = 'admin';
$senha = 'Admin@123';
$q = db()->prepare('SELECT * FROM tbl_Usuarios WHERE LOWER(NomeUsuario) = LOWER(?) AND LOWER(TipoUsuario) = LOWER(?) LIMIT 1');
$q->execute([$login, 'admin']);
$user = $q->fetch();
if ($user && password_verify($senha, $user['SenhaHash'])) {
    $_SESSION['user'] = ['id' => (int)$user['ID_Usuario'], 'name' => $user['NomeUsuario'], 'login' => $user['NomeUsuario'], 'type' => 'Admin'];
    echo $_SESSION['user']['type'] . PHP_EOL;
    echo $_SESSION['user']['login'] . PHP_EOL;
} else {
    echo 'FAIL';
}
