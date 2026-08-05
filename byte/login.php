<?php
require_once __DIR__ . '/includes/functions.php';
if (is_logged())
    redirect(is_admin() ? 'admin/index.php' : 'minha_conta.php');
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $login = sanitize_text((string) ($_POST['login'] ?? ''), 50);
    $senha = trim((string) ($_POST['senha'] ?? ''));

    try {
        $adminQ = db()->prepare('SELECT * FROM tbl_Usuarios WHERE LOWER(NomeUsuario) = LOWER(?) AND LOWER(TipoUsuario) = LOWER(?) LIMIT 1');
        $adminQ->execute([$login, 'admin']);
        $adminUser = $adminQ->fetch();
        if ($adminUser && verify_password($senha, (string) ($adminUser['SenhaHash'] ?? ''))) {
            $_SESSION['user'] = ['id' => (int) $adminUser['ID_Usuario'], 'name' => $adminUser['NomeUsuario'], 'login' => $adminUser['NomeUsuario'], 'type' => 'Admin'];
            flash('success', 'Bem-vindo(a) de volta ao painel administrativo.');
            redirect('admin/index.php');
        }
    } catch (Throwable $e) {
        // Ignora e usa o fluxo de cliente quando a tabela de usuários admin não existir.
    }

    $customerLogin = sanitize_email($login);
    $q = db()->prepare('SELECT * FROM tbl_Clientes WHERE LOWER(Email_Cliente) = LOWER(?) LIMIT 1');
    $q->execute([$customerLogin]);
    $u = $q->fetch();
    if ($u && verify_password($senha, (string) ($u['Senha_Hash'] ?? ''))) {
        $fullName = trim(($u['Nome_Cliente'] ?? '') . ' ' . ($u['Sobr_Cliente'] ?? ''));
        $_SESSION['user'] = ['id' => (int) $u['ID_Cliente'], 'name' => $fullName ?: ($u['Nome_Cliente'] ?? $login), 'login' => $u['Email_Cliente'] ?? $login, 'type' => 'Cliente'];
        flash('success', 'Bem-vindo(a) de volta à Byte Zone!');
        redirect('minha_conta.php');
    }
    flash('error', 'Login ou senha inválidos.');
}
$pageTitle = 'Entrar';
require __DIR__ . '/includes/header.php'; ?>
<section class="auth-wrap">
    <form class="auth-card" method="post">
        <div class="eyebrow">ACESSO BYTE ZONE</div>
        <h1>Bem-vindo de volta.</h1>
        <p>Entre para acompanhar seus pedidos e salvar os seus equipamentos favoritos.</p>
        <div class="form-group"><label>E-mail</label><input name="login" required autocomplete="username"
                placeholder="seu@email.com"></div>
        <div class="form-group"><label>Senha</label><input type="password" name="senha" required
                autocomplete="current-password" placeholder="••••••••"></div><button class="btn btn-primary"
            type="submit">Entrar na minha conta →</button>
        <p class="form-note">Ainda não tem conta? <a href="cadastro.php">Cadastre-se agora</a></p>
    </form>
</section><?php require __DIR__ . '/includes/footer.php'; ?>