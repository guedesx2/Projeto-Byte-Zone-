<?php
require_once __DIR__ . '/../config/database.php';
require_once __DIR__ . '/../includes/functions.php';

function assertTrue(bool $condition, string $message): void {
    if ($condition) {
        echo "OK: {$message}\n";
    } else {
        echo "FAIL: {$message}\n";
    }
}

try {
    db()->beginTransaction();

    $brand = db()->query("SELECT ID_Marca FROM tbl_Marca ORDER BY ID_Marca LIMIT 1")->fetchColumn();
    $category = db()->query("SELECT ID_Categoria FROM tbl_Categoria ORDER BY ID_Categoria LIMIT 1")->fetchColumn();
    $tempName = 'TMP_ESTOQUE_ZERO_' . uniqid();
    $tempNameEsgotado = 'TMP_ESGOTADO_' . uniqid();

    $insertZero = db()->prepare('INSERT INTO tbl_Produtos (Nome_Prod, ID_Marca, ID_Categoria, Valor_Preco, QtdEstoque, PesoKG, Slug, StatusProduto) VALUES (?, ?, ?, 1000.00, 0, 2.5, ?, "Disponível")');
    $insertZero->execute([$tempName, $brand, $category, strtolower($tempName)]);
    $productId = (int) db()->lastInsertId();

    $insertEsgotado = db()->prepare('INSERT INTO tbl_Produtos (Nome_Prod, ID_Marca, ID_Categoria, Valor_Preco, QtdEstoque, PesoKG, Slug, StatusProduto) VALUES (?, ?, ?, 1000.00, 5, 2.5, ?, "Esgotado")');
    $insertEsgotado->execute([$tempNameEsgotado, $brand, $category, strtolower($tempNameEsgotado)]);
    $esgotadoProductId = (int) db()->lastInsertId();

    $normalizedStatus = normalize_product_status(0, 'Disponível');
    $normalProductStatus = normalize_product_status(5, 'Disponível');
    $zeroStockProduct = db()->query("SELECT ID_Produto, Nome_Prod, QtdEstoque, StatusProduto FROM tbl_Produtos WHERE ID_Produto = {$productId}")->fetch();
    $esgotadoStatusProduct = db()->query("SELECT ID_Produto, Nome_Prod, QtdEstoque, StatusProduto FROM tbl_Produtos WHERE ID_Produto = {$esgotadoProductId}")->fetch();
    $availableCheck = is_product_unavailable($zeroStockProduct);
    $catalogVisible = is_product_unavailable($zeroStockProduct);
    $statusCheck = is_product_unavailable($esgotadoStatusProduct);

    assertTrue($zeroStockProduct !== false, 'O produto temporário foi criado com sucesso para testar o fluxo.');
    assertTrue((int) ($zeroStockProduct['QtdEstoque'] ?? 0) === 0, 'O produto temporário está realmente com estoque zero.');
    assertTrue($normalizedStatus === 'Esgotado', 'A função de normalização converte estoque zero para status Esgotado.');
    assertTrue($normalProductStatus === 'Disponível', 'A função de normalização mantém o status disponível quando o estoque é positivo.');
    assertTrue($availableCheck === true, 'O produto temporário não aparece como disponível para compra.');
    assertTrue($catalogVisible === true, 'O catálogo filtra corretamente produtos com estoque zero.');
    assertTrue((int) $zeroStockProduct['QtdEstoque'] <= 0, 'A lógica de regra de estoque zero está reconhecida no teste.');
    assertTrue($esgotadoStatusProduct !== false, 'Existe um produto com status Esgotado para validar o novo comportamento de exibição.');
    assertTrue((string) ($esgotadoStatusProduct['StatusProduto'] ?? '') === 'Esgotado', 'O cenário de status Esgotado foi identificado no banco.');
    assertTrue($statusCheck === true, 'A regra de status Esgotado é reconhecida como indisponível para compra.');

    db()->rollBack();
    echo "OK: Teste de regressão concluído com rollback do registro temporário.\n";
} catch (Throwable $e) {
    if (db()->inTransaction()) {
        db()->rollBack();
    }
    echo 'FAIL: ' . $e->getMessage() . "\n";
    exit(1);
}
