<?php
require_once __DIR__ . '/includes/functions.php';
$id = sanitize_int($_GET['id'] ?? 0, 1);
$stmt = db()->prepare("SELECT p.*,m.NomeMarca,c.NomeCategoria,d.Descricao,d.Especificacoes,d.GarantiaMeses,(SELECT UrlImagem FROM tbl_ProdutoImagem pi WHERE pi.ID_Produto=p.ID_Produto ORDER BY Principal DESC,ID_Imagem ASC LIMIT 1) UrlImagem FROM tbl_Produtos p JOIN tbl_Marca m ON m.ID_Marca=p.ID_Marca JOIN tbl_Categoria c ON c.ID_Categoria=p.ID_Categoria LEFT JOIN tbl_DescricaoProduto d ON d.ID_Produto=p.ID_Produto WHERE p.ID_Produto=?");
$stmt->execute([$id]);
$p = $stmt->fetch();
if (!$p) {
    flash('error', 'Produto não encontrado.');
    redirect('catalogo.php');
}
$unavailable = is_product_unavailable($p);
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    require_login();
    if ($unavailable) {
        flash('error', 'Este produto está indisponível no momento.');
        redirect('produto.php?id=' . $id);
    }
    $customer = current_customer_id();
    if (!$customer) {
        flash('error', 'Não encontramos seu cadastro de cliente.');
        redirect('minha_conta.php');
    }
    $qty = max(1, sanitize_int($_POST['quantidade'] ?? 1, 1, 100));
    if ($qty > (int) $p['QtdEstoque']) {
        flash('error', 'A quantidade solicitada excede o estoque disponível.');
        redirect('produto.php?id=' . $id);
    }
    $cart = db()->prepare('SELECT ID_Carrinho FROM tbl_Carrinho WHERE ID_Cliente=? LIMIT 1');
    $cart->execute([$customer]);
    $cartId = $cart->fetchColumn();
    if (!$cartId) {
        db()->prepare('INSERT INTO tbl_Carrinho (ID_Cliente) VALUES (?)')->execute([$customer]);
        $cartId = db()->lastInsertId();
    }
    $existing = db()->prepare('SELECT ID_ItemCarrinho, Quantidade FROM tbl_CarrinhoItens WHERE ID_Carrinho=? AND ID_Produto=?');
    $existing->execute([$cartId, $id]);
    $item = $existing->fetch();
    $currentQty = (int) ($item['Quantidade'] ?? 0);
    if (($currentQty + $qty) > (int) $p['QtdEstoque']) {
        flash('error', 'Não há estoque suficiente para adicionar essa quantidade ao carrinho.');
        redirect('produto.php?id=' . $id);
    }
    if ($item)
        db()->prepare('UPDATE tbl_CarrinhoItens SET Quantidade=Quantidade+? WHERE ID_ItemCarrinho=?')->execute([$qty, $item['ID_ItemCarrinho']]);
    else
        db()->prepare('INSERT INTO tbl_CarrinhoItens (ID_Carrinho,ID_Produto,Quantidade) VALUES (?,?,?)')->execute([$cartId, $id, $qty]);
    flash('success', 'Produto adicionado ao carrinho.');
    redirect('carrinho.php');
}
$pageTitle = $p['Nome_Prod'];
require __DIR__ . '/includes/header.php'; ?>
<div class="product-detail">
    <div>
        <div class="detail-image"><img src="<?= e(product_image($p)) ?>" alt="<?= e($p['Nome_Prod']) ?>"></div>
    </div>
    <div class="detail-info">
        <div class="breadcrumb">BYTE ZONE / <?= e(strtoupper($p['NomeCategoria'])) ?> /
            <?= e(strtoupper($p['NomeMarca'])) ?></div><span class="tag"
            style="position:static"><?= e($p['NomeCategoria']) ?></span>
        <h1><?= e($p['Nome_Prod']) ?></h1>
        <p><?= nl2br(e($p['Descricao'] ?: 'Equipamento selecionado pela Byte Zone para entregar alto desempenho, confiabilidade e design.')) ?>
        </p><?php if ($p['ValorPromocional']): ?><span
                class="old-price"><?= money((float) $p['Valor_Preco']) ?></span><?php endif; ?>
        <div class="price-lg"><?= money((float) ($p['ValorPromocional'] ?: $p['Valor_Preco'])) ?></div><span
            class="stock">●
            <?= $unavailable ? 'INDISPONÍVEL / ESGOTADO' : 'DISPONÍVEL EM ESTOQUE' ?></span><?php if (!$unavailable): ?>
            <form class="buy-box" method="post"><input type="number" name="quantidade" min="1" max="<?= $p['QtdEstoque'] ?>"
                    value="1"><button class="btn btn-primary" type="submit">Adicionar ao carrinho →</button></form>
        <?php else: ?>
            <p class="form-note">Este item está temporariamente indisponível para compra.</p>
        <?php endif; ?>
        <div class="specs">
            <h3>Especificações</h3>
            <pre><?= e($p['Especificacoes'] ?: 'Peso: ' . $p['PesoKG'] . ' kg' . ($p['GarantiaMeses'] ? "\nGarantia: " . $p['GarantiaMeses'] . ' meses' : '')) ?></pre>
        </div>
    </div>
</div><?php require __DIR__ . '/includes/footer.php'; ?>