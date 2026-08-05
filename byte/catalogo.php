<?php
require_once __DIR__ . '/includes/functions.php';
$categories = db()->query('SELECT * FROM tbl_Categoria ORDER BY NomeCategoria')->fetchAll();
$category = sanitize_text($_GET['categoria'] ?? '', 50);
$search = sanitize_text($_GET['busca'] ?? '', 100);
$where = ["p.StatusProduto IN ('Disponível','Esgotado')"];
$params = [];
if ($category !== '') {
    $where[] = 'c.NomeCategoria=?';
    $params[] = $category;
}
if ($search !== '') {
    $where[] = '(p.Nome_Prod LIKE ? OR m.NomeMarca LIKE ?)';
    $params[] = '%' . $search . '%';
    $params[] = '%' . $search . '%';
}
$sql = "SELECT p.*,m.NomeMarca,c.NomeCategoria,(SELECT UrlImagem FROM tbl_ProdutoImagem pi WHERE pi.ID_Produto=p.ID_Produto ORDER BY Principal DESC,ID_Imagem ASC LIMIT 1) UrlImagem FROM tbl_Produtos p JOIN tbl_Marca m ON m.ID_Marca=p.ID_Marca JOIN tbl_Categoria c ON c.ID_Categoria=p.ID_Categoria WHERE " . implode(' AND ', $where) . ' ORDER BY p.DataCadastro DESC';
$stmt = db()->prepare($sql);
$stmt->execute($params);
$products = $stmt->fetchAll();
$pageTitle = 'Catálogo';
require __DIR__ . '/includes/header.php'; ?>
<section class="page-hero">
    <div class="eyebrow">BYTE ZONE / CATÁLOGO</div>
    <h1>Encontre a sua potência.</h1>
    <p>PC Gamer, desktop e notebook com a configuração certa para o seu próximo nível.</p>
</section>
<div class="catalog-layout">
    <aside class="filters">
        <h3>Filtre sua zona</h3>
        <form method="get"><input class="filter-search" name="busca" value="<?= e($search) ?>"
                placeholder="Buscar produto"><label><input type="radio" name="categoria" value=""
                    <?= $category === '' ? 'checked' : '' ?> onchange="this.form.submit()"> Todas as
                categorias</label><?php foreach ($categories as $c): ?><label><input type="radio" name="categoria"
                        value="<?= e($c['NomeCategoria']) ?>" <?= $category === $c['NomeCategoria'] ? 'checked' : '' ?>
                        onchange="this.form.submit()"> <?= e($c['NomeCategoria']) ?></label><?php endforeach; ?><button
                class="btn btn-primary btn-small" type="submit">Buscar</button></form>
    </aside>
    <section>
        <div class="catalog-top"><span><?= count($products) ?> produto(s)
                encontrado(s)</span><span><?= $category ? e($category) : 'Todos os produtos' ?></span></div>
        <?php if ($products): ?>
            <div class="products"><?php foreach ($products as $p): ?>
                    <article class="product-card<?= is_product_unavailable($p) ? ' is-out' : '' ?>"><a href="produto.php?id=<?= $p['ID_Produto'] ?>"><span
                                class="tag"><?= e($p['NomeCategoria']) ?></span><?php if (is_product_unavailable($p)): ?><span class="out-badge">ESGOTADO</span><?php endif; ?>
                            <div class="product-image"><img src="<?= e(product_image($p)) ?>" alt="<?= e($p['Nome_Prod']) ?>">
                            </div>
                            <div class="product-info"><span class="product-brand"><?= e($p['NomeMarca']) ?></span>
                                <h3><?= e($p['Nome_Prod']) ?></h3><?php if ($p['ValorPromocional']): ?><span
                                        class="old-price"><?= money((float) $p['Valor_Preco']) ?></span><?php endif; ?><b
                                    class="price"><?= money((float) ($p['ValorPromocional'] ?: $p['Valor_Preco'])) ?></b><span
                                    class="stock"><?= is_product_unavailable($p) ? '● INDISPONÍVEL' : '● EM ESTOQUE' ?></span>
                            </div>
                        </a></article><?php endforeach; ?>
            </div><?php else: ?>
            <div class="empty">Nenhum computador encontrado. Ajuste os filtros ou cadastre produtos no painel.</div>
        <?php endif; ?>
    </section>
</div><?php require __DIR__ . '/includes/footer.php'; ?>