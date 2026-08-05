<?php
require_once __DIR__ . '/includes/functions.php';
require_login();
$customer = current_customer_id();
if (!$customer) {
    flash('error', 'Complete o cadastro da sua conta.');
    redirect('minha_conta.php');
}
if (isset($_GET['remover'])) {
    $item = sanitize_int($_GET['remover'] ?? 0, 1);
    db()->prepare('DELETE ci FROM tbl_CarrinhoItens ci JOIN tbl_Carrinho c ON c.ID_Carrinho=ci.ID_Carrinho WHERE ci.ID_ItemCarrinho=? AND c.ID_Cliente=?')->execute([$item, $customer]);
    flash('success', 'Item removido do carrinho.');
    redirect('carrinho.php');
}
$q = db()->prepare("SELECT ci.*,p.Nome_Prod,p.Valor_Preco,p.ValorPromocional,p.QtdEstoque,(SELECT UrlImagem FROM tbl_ProdutoImagem pi WHERE pi.ID_Produto=p.ID_Produto ORDER BY Principal DESC,ID_Imagem ASC LIMIT 1) UrlImagem,cg.NomeCategoria FROM tbl_CarrinhoItens ci JOIN tbl_Carrinho c ON c.ID_Carrinho=ci.ID_Carrinho JOIN tbl_Produtos p ON p.ID_Produto=ci.ID_Produto JOIN tbl_Categoria cg ON cg.ID_Categoria=p.ID_Categoria WHERE c.ID_Cliente=?");
$q->execute([$customer]);
$items = $q->fetchAll();
$hasUnavailableItems = false;
$total = 0;
foreach ($items as $i) {
    if ((int) $i['QtdEstoque'] <= 0 || (int) $i['Quantidade'] > (int) $i['QtdEstoque']) {
        $hasUnavailableItems = true;
    }
    $total += (float) ($i['ValorPromocional'] ?: $i['Valor_Preco']) * $i['Quantidade'];
}
$pageTitle = 'Meu carrinho';
require __DIR__ . '/includes/header.php'; ?>
<section class="cart-page">
    <div class="eyebrow">SUA SELEÇÃO</div>
    <h1>Meu carrinho.</h1><?php if ($items): ?><?php if ($hasUnavailableItems): ?><div class="flash error">Alguns itens do carrinho ficaram indisponíveis e não podem ser comprados.</div><?php endif; ?>
        <div class="cart-grid">
            <div><?php foreach ($items as $i): ?>
                    <article class="cart-item"><img src="<?= e(product_image($i)) ?>" alt="">
                        <div>
                            <h3><?= e($i['Nome_Prod']) ?></h3><small><?= e($i['NomeCategoria']) ?> · Quantidade:
                                <?= $i['Quantidade'] ?></small><br><?php if ((int) $i['QtdEstoque'] <= 0 || (int) $i['Quantidade'] > (int) $i['QtdEstoque']): ?><span class="stock" style="color:#b91c1c">INDISPONÍVEL</span><?php else: ?><span class="stock">● EM ESTOQUE</span><?php endif; ?><br><a class="remove"
                                href="carrinho.php?remover=<?= $i['ID_ItemCarrinho'] ?>">Remover</a>
                        </div><b
                            class="price"><?= money((float) ($i['ValorPromocional'] ?: $i['Valor_Preco']) * $i['Quantidade']) ?></b>
                    </article><?php endforeach; ?>
            </div>
            <aside class="cart-summary">
                <h3>Resumo do pedido</h3>
                <div class="summary-line"><span>Produtos</span><span><?= money($total) ?></span></div>
                <div class="summary-line"><span>Frete</span><span>A calcular</span></div>
                <div class="summary-line summary-total"><span>Total</span><span><?= money($total) ?></span></div><?php if (!$hasUnavailableItems): ?><a
                    class="btn btn-primary" href="checkout.php">Ir para pagamento →</a><?php else: ?><button class="btn btn-primary" type="button" disabled>Ir para pagamento →</button><?php endif; ?><a class="btn btn-outline"
                    style="margin:12px 0 0;width:100%" href="catalogo.php">Continuar comprando</a>
            </aside>
        </div><?php else: ?>
        <div class="empty">
            <h3>Seu carrinho está vazio.</h3>
            <p>Que tal encontrar uma máquina à altura do seu próximo desafio?</p><a class="btn btn-primary"
                href="catalogo.php">Explorar catálogo</a>
        </div><?php endif; ?>
</section><?php require __DIR__ . '/includes/footer.php'; ?>