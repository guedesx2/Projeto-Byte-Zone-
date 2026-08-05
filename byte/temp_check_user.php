<?php
require 'config/database.php';
$pdo = new PDO('mysql:host=' . DB_HOST . ';dbname=' . DB_NAME . ';charset=utf8mb4', DB_USER, DB_PASS, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES => false,
]);
foreach ($pdo->query('SELECT ID_Usuario, NomeUsuario, TipoUsuario, SenhaHash FROM tbl_Usuarios LIMIT 10') as $row) {
    echo $row['ID_Usuario'] . ' | ' . $row['NomeUsuario'] . ' | ' . $row['TipoUsuario'] . ' | ' . substr($row['SenhaHash'],0,10) . PHP_EOL;
}
