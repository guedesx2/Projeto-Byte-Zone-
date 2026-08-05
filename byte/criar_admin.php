<?php
require_once __DIR__ . '/includes/functions.php';
try {
    $admins = (int) db()->query("SELECT COUNT(*) FROM tbl_Usuarios WHERE LOWER(TipoUsuario) = 'admin'")->fetchColumn();
} catch (Throwable $e) {
    $admins = 0;
}
if ($admins > 0) {
    flash('error', 'Já existe um administrador. Por segurança, remova este arquivo do servidor.');
    redirect('login.php');
}
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $name = sanitize_text($_POST['nome'] ?? '', 100);
    $login = sanitize_text($_POST['login'] ?? '', 50);
    $password = trim((string) ($_POST['senha'] ?? ''));
    if (!is_valid_name($name) || $login === '') {
        flash('error', 'Use um nome válido e um usuário válido para criar o administrador.');
    } elseif (strlen($password) < 8 || !is_strong_password($password)) {
        flash('error', 'A senha precisa ter no mínimo 8 caracteres, incluindo letra maiúscula, minúscula, número e símbolo.');
    } else {
        try {
            $passwordHash = hash_password($password);
            $stmt = db()->prepare('INSERT INTO tbl_Usuarios (NomeUsuario, SenhaHash, TipoUsuario) VALUES (?,?,?)');
            $stmt->execute([$login, $passwordHash, 'admin']);
            flash('success', 'Administrador criado. Exclua agora o arquivo criar_admin.php por segurança e entre na conta.');
            redirect('login.php');
        } catch (PDOException $e) {
            flash('error', 'Esse login já está em uso.');
        }
    }
}
$pageTitle = 'Criar administrador';
require __DIR__ . '/includes/header.php'; ?>
<section class="auth-wrap">
    <form class="auth-card" method="post">
        <div class="eyebrow">CONFIGURAÇÃO INICIAL</div>
        <h1>Criar administrador.</h1>
        <p>Esta tela só funciona enquanto não existe nenhum administrador. Depois de criar a conta, apague
            <b>criar_admin.php</b> do servidor.</p>
        <div class="form-group"><label>Nome</label><input name="nome" required></div>
        <div class="form-group"><label>Login</label><input name="login" required></div>
        <div class="form-group"><label>Senha</label><input type="password" name="senha" minlength="6" required></div>
        <button class="btn btn-primary">Criar administrador →</button>
    </form>
</section><?php require __DIR__ . '/includes/footer.php'; ?>