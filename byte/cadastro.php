<?php
require_once __DIR__ . '/includes/functions.php';
if (is_logged())
    redirect('minha_conta.php');
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $nome = sanitize_text($_POST['nome'] ?? '', 100);
    $email = sanitize_email($_POST['email'] ?? '');
    $senha = trim((string) ($_POST['senha'] ?? ''));
    $confirmarSenha = trim((string) ($_POST['confirmar_senha'] ?? ''));
    $tel = sanitize_text($_POST['telefone'] ?? '', 20);
    $cpf = normalize_cpf($_POST['cpf'] ?? '');
    $dataNascimento = sanitize_text($_POST['data_nascimento'] ?? '', 10);
    if (!is_valid_name($nome)) {
        flash('error', 'Informe um nome válido com apenas letras, espaços ou hífens.');
    } elseif (!is_valid_email($email)) {
        flash('error', 'Informe um e-mail válido.');
    } elseif (!is_strong_password($senha)) {
        flash('error', 'A senha deve ter no mínimo 8 caracteres, incluir letra minúscula, maiúscula, número e símbolo especial.');
    } elseif ($senha !== $confirmarSenha) {
        flash('error', 'As senhas não conferem.');
    } elseif ($cpf !== '' && !is_valid_cpf($cpf)) {
        flash('error', 'Informe um CPF válido.');
    } elseif ($dataNascimento === '' || !preg_match('/^\d{4}-\d{2}-\d{2}$/', $dataNascimento) || !is_over_16($dataNascimento)) {
        flash('error', 'Você precisa ter pelo menos 16 anos para criar uma conta.');
    } else {
        try {
            db()->beginTransaction();
            $parts = preg_split('/\s+/', $nome, 2);
            $passwordHash = hash_password($senha);
            $stmt = db()->prepare('INSERT INTO tbl_Clientes (Nome_Cliente,Sobr_Cliente,Email_Cliente,Tel_Cliente,CPF_Cliente,Senha_Hash,Data_nascimento) VALUES (?,?,?,?,?,?,?)');
            $stmt->execute([$parts[0], $parts[1] ?? '', $email, $tel, $cpf !== '' ? $cpf : null, $passwordHash, $dataNascimento]);
            db()->commit();
            flash('success', 'Conta criada! Agora entre para continuar.');
            redirect('login.php');
        } catch (PDOException $e) {
            if (db()->inTransaction())
                db()->rollBack();
            flash('error', 'Este e-mail já possui uma conta.');
        }
    }
}
$pageTitle = 'Criar conta';
require __DIR__ . '/includes/header.php'; ?>
<section class="auth-wrap">
    <form class="auth-card" method="post">
        <div class="eyebrow">NOVA CONTA</div>
        <h1>Entre na zona.</h1>
        <p>Crie sua conta para comprar, acompanhar e aproveitar a experiência Byte Zone.</p>
        <div class="form-group"><label>Nome completo</label><input name="nome" required
                value="<?= e($_POST['nome'] ?? '') ?>"></div>
        <div class="form-group"><label>E-mail</label><input type="email" name="email" required
                value="<?= e($_POST['email'] ?? '') ?>"></div>
        <div class="form-group"><label>CPF</label><input name="cpf" inputmode="numeric" maxlength="14"
                value="<?= e($_POST['cpf'] ?? '') ?>" placeholder="000.000.000-00" required></div>
        <div class="form-group"><label>Data de nascimento</label><input type="date" name="data_nascimento" required
                value="<?= e($_POST['data_nascimento'] ?? '') ?>"></div>
        <div class="form-group"><label>Telefone</label><input name="telefone" value="<?= e($_POST['telefone'] ?? '') ?>"
                placeholder="(00) 00000-0000"></div>
        <div class="form-group"><label>Senha</label><input type="password" name="senha" required minlength="8"
                placeholder="Mínimo de 8 caracteres"></div>
        <div class="form-group"><label>Confirmar senha</label><input type="password" name="confirmar_senha" required
                placeholder="Repita a senha"></div>
        <button class="btn btn-primary" type="submit">Criar minha
            conta →</button>
        <p class="form-note">Já tem uma conta? <a href="login.php">Entrar</a></p>
    </form>
</section>
<script>
(function(){
  const cpfInput = document.querySelector('input[name="cpf"]');
  const dobInput = document.querySelector('input[name="data_nascimento"]');
  const passwordInput = document.querySelector('input[name="senha"]');
  const confirmInput = document.querySelector('input[name="confirmar_senha"]');

  const validatePassword = () => {
    const value = passwordInput ? passwordInput.value : '';
    const strong = value.length >= 8 && /[a-z]/.test(value) && /[A-Z]/.test(value) && /\d/.test(value) && /[^A-Za-z0-9]/.test(value);
    if (passwordInput) passwordInput.setCustomValidity(strong ? '' : 'A senha deve ter 8+ caracteres, letra minúscula, maiúscula, número e símbolo.');
    if (confirmInput) {
      confirmInput.setCustomValidity(confirmInput.value && confirmInput.value === value ? '' : 'As senhas não conferem.');
    }
  };

  if (cpfInput) {
    cpfInput.addEventListener('input', function () {
      const value = this.value.replace(/\D/g, '').slice(0, 11);
      this.value = value.replace(/(\d{3})(\d)/, '$1.$2').replace(/(\d{3})(\d)/, '$1.$2').replace(/(\d{3})(\d{1,2})$/, '$1-$2');
    });
  }
  if (dobInput) {
    dobInput.addEventListener('change', function () {
      if (!this.value) return;
      const birth = new Date(this.value);
      const today = new Date();
      let age = today.getFullYear() - birth.getFullYear();
      const m = today.getMonth() - birth.getMonth();
      if (m < 0 || (m === 0 && today.getDate() < birth.getDate())) age--;
      this.setCustomValidity(age < 16 ? 'Você precisa ter pelo menos 16 anos.' : '');
    });
  }
  if (passwordInput) {
    passwordInput.addEventListener('input', validatePassword);
  }
  if (confirmInput) {
    confirmInput.addEventListener('input', validatePassword);
  }
})();
</script>
<?php require __DIR__ . '/includes/footer.php'; ?>