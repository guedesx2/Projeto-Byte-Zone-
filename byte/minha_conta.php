<?php require_once __DIR__ . '/includes/functions.php';
require_login();
$customer = current_customer_id();
$q = db()->prepare('SELECT v.*,f.NomeForma FROM tbl_Vendas v LEFT JOIN tbl_FormaPagamento f ON f.ID_Forma=v.ID_Forma WHERE v.ID_Cliente=? ORDER BY v.DataVenda DESC');
$q->execute([$customer]);
$orders = $q->fetchAll();
$pageTitle = 'Minha conta';
require __DIR__ . '/includes/header.php'; ?>
<section class="account-page">
    <div class="eyebrow">MINHA ÁREA</div>
    <h1>Olá, <?= e($_SESSION['user']['name']) ?>.</h1>
    <div class="account-card">
        <h3>Meus pedidos</h3><?php if ($orders): ?>
            <table class="data-table">
                <tr>
                    <th>Pedido</th>
                    <th>Data</th>
                    <th>Status</th>
                    <th>Total</th>
                </tr><?php foreach ($orders as $o): ?>
                    <tr>
                        <td>#<?= $o['ID_Venda'] ?></td>
                        <td><?= date('d/m/Y', strtotime($o['DataVenda'])) ?></td>
                        <td><?= e($o['StatusVenda']) ?></td>
                        <td><?= money((float) $o['ValorTotal']) ?></td>
                    </tr><?php endforeach; ?>
            </table><?php else: ?>
            <p>Você ainda não fez pedidos. Explore uma máquina que combina com você.</p><a class="btn btn-primary btn-small"
                href="catalogo.php">Ver computadores</a><?php endif; ?>
    </div>
</section><?php require __DIR__ . '/includes/footer.php'; ?>