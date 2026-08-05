<?php
require_once __DIR__ . '/includes/functions.php';
require_login();
$customer = current_customer_id();
$itemsQ = db()->prepare("SELECT ci.*,p.Valor_Preco,p.ValorPromocional,p.QtdEstoque,p.Nome_Prod FROM tbl_CarrinhoItens ci JOIN tbl_Carrinho c ON c.ID_Carrinho=ci.ID_Carrinho JOIN tbl_Produtos p ON p.ID_Produto=ci.ID_Produto WHERE c.ID_Cliente=?");
$itemsQ->execute([$customer]);
$items = $itemsQ->fetchAll();
if (!$items) {
    flash('error', 'Seu carrinho está vazio.');
    redirect('carrinho.php');
}
foreach ($items as $item) {
    if ((int) $item['QtdEstoque'] <= 0 || (int) $item['Quantidade'] > (int) $item['QtdEstoque']) {
        db()->prepare('DELETE ci FROM tbl_CarrinhoItens ci JOIN tbl_Carrinho c ON c.ID_Carrinho=ci.ID_Carrinho WHERE ci.ID_Produto=? AND c.ID_Cliente=?')->execute([$item['ID_Produto'], $customer]);
        flash('error', 'Um ou mais itens do seu carrinho ficaram indisponíveis e foram removidos.');
        redirect('carrinho.php');
    }
}
$forms = db()->query('SELECT * FROM tbl_FormaPagamento')->fetchAll();
$address = db()->prepare('SELECT * FROM tbl_Enderecos WHERE ID_Cliente=? ORDER BY ID_Endereco DESC');
$address->execute([$customer]);
$addresses = $address->fetchAll();
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    try {
        db()->beginTransaction();
        $addressId = sanitize_int($_POST['endereco'] ?? 0, 0);
        if (!$addressId) {
            $fields = ['rua', 'numero', 'complemento', 'bairro', 'cidade', 'estado', 'cep'];
            foreach (['rua', 'numero', 'cep'] as $f)
                if (empty($_POST[$f]))
                    throw new Exception('Preencha rua, número e CEP.');
            $rua = sanitize_text($_POST['rua'] ?? '', 150);
            $numero = sanitize_text($_POST['numero'] ?? '', 10);
            $complemento = sanitize_text($_POST['complemento'] ?? '', 50);
            $bairro = sanitize_text($_POST['bairro'] ?? '', 80);
            $cidade = sanitize_text($_POST['cidade'] ?? '', 80);
            $estado = strtoupper(sanitize_text($_POST['estado'] ?? '', 2));
            $cep = sanitize_text($_POST['cep'] ?? '', 10);
            if ($rua === '' || $numero === '' || $bairro === '' || $cidade === '' || $estado === '' || !is_valid_cep($cep)) {
                throw new Exception('Preencha um endereço válido com CEP de 8 dígitos.');
            }
            db()->prepare('INSERT INTO tbl_Enderecos (ID_Cliente,Rua_Cliente,Num_Cliente,Comple_Cliente,Bairro_Cliente,Cidade_Cliente,Est_Cliente,CEP_Cliente) VALUES (?,?,?,?,?,?,?,?)')->execute([$customer, $rua, $numero, $complemento, $bairro, $cidade, $estado, $cep]);
            $addressId = (int) db()->lastInsertId();
        }
        $total = 0;
        foreach ($items as $i)
            $total += (float) ($i['ValorPromocional'] ?: $i['Valor_Preco']) * $i['Quantidade'];
        $forma = sanitize_int($_POST['forma'] ?? 0, 1);
        $saleUserId = current_sale_user_id();
        if (!$saleUserId) {
            throw new Exception('Não existe um usuário administrativo disponível para registrar o pedido.');
        }
        db()->prepare("INSERT INTO tbl_Vendas (ID_Cliente,ID_Usuario,ID_Forma,ID_Endereco,StatusVenda,ValorTotal) VALUES (?,?,?,?, 'Pendente',?)")->execute([$customer, $saleUserId, $forma, $addressId, $total]);
        $cart = db()->prepare('SELECT ID_Carrinho FROM tbl_Carrinho WHERE ID_Cliente=?');
        $cart->execute([$customer]);
        $cartId = $cart->fetchColumn();
        db()->prepare('DELETE FROM tbl_CarrinhoItens WHERE ID_Carrinho=?')->execute([$cartId]);
        db()->commit();
        flash('success', 'Pedido realizado com sucesso! Em breve você receberá as próximas etapas.');
        redirect('minha_conta.php');
    } catch (Throwable $e) {
        if (db()->inTransaction())
            db()->rollBack();
        flash('error', $e->getMessage() ?: 'Não foi possível concluir o pedido.');
    }
}
$pageTitle = 'Finalizar pedido';
require __DIR__ . '/includes/header.php'; ?>
<section class="cart-page">
    <div class="eyebrow">CHECKOUT SEGURO</div>
    <h1>Finalizar pedido.</h1>
    <form class="admin-form" method="post">
        <h3>Endereço de entrega</h3><?php if ($addresses): ?>
            <div class="form-group"><label>Usar endereço salvo</label><select name="endereco">
                    <option value="">Cadastrar novo endereço</option><?php foreach ($addresses as $a): ?>
                        <option value="<?= $a['ID_Endereco'] ?>">
                            <?= e($a['Rua_Cliente'] . ', ' . $a['Num_Cliente'] . ' — ' . $a['Cidade_Cliente'] . '/' . $a['Est_Cliente']) ?>
                        </option><?php endforeach; ?>
                </select></div><?php endif; ?>
        <div class="form-row">
            <div class="form-group"><label>Rua</label><input name="rua"></div>
            <div class="form-group"><label>Número</label><input name="numero"></div>
        </div>
        <div class="form-row">
            <div class="form-group"><label>Complemento</label><input name="complemento"></div>
            <div class="form-group"><label>Bairro</label><input name="bairro"></div>
        </div>
        <div class="form-row">
            <div class="form-group"><label>Cidade</label><input name="cidade"></div>
            <div class="form-group"><label>Estado</label><input name="estado" maxlength="2"></div>
        </div>
        <div class="form-group"><label>CEP</label><input name="cep"></div>
        <div class="form-group"><label>Forma de pagamento</label><select name="forma"
                required><?php foreach ($forms as $f): ?>
                    <option value="<?= $f['ID_Forma'] ?>"><?= e($f['NomeForma']) ?></option><?php endforeach; ?>
            </select></div><button class="btn btn-primary" type="submit">Confirmar pedido →</button>
    </form>
</section><?php require __DIR__ . '/includes/footer.php'; ?>