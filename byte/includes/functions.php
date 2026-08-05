<?php
require_once __DIR__ . '/../config/database.php';
if (session_status() === PHP_SESSION_NONE) session_start();
if (!ob_get_level()) ob_start();

function e(?string $value): string { return htmlspecialchars((string)$value, ENT_QUOTES, 'UTF-8'); }
function sanitize_text(?string $value, int $maxLength = 255): string {
    $text = trim((string) ($value ?? ''));
    $text = strip_tags($text);
    $text = preg_replace('/\s+/', ' ', $text) ?? $text;
    if ($maxLength > 0 && mb_strlen($text) > $maxLength) {
        $text = mb_substr($text, 0, $maxLength);
    }
    return $text;
}
function sanitize_email(?string $value): string {
    return mb_strtolower(trim((string) ($value ?? '')));
}
function is_valid_email(string $email): bool {
    return filter_var($email, FILTER_VALIDATE_EMAIL) !== false && preg_match('/@.+\./', $email);
}
function is_valid_phone(string $phone): bool {
    $phone = preg_replace('/\D/', '', $phone) ?? '';
    return strlen($phone) >= 10 && strlen($phone) <= 11;
}
function is_valid_cep(string $cep): bool {
    $cep = preg_replace('/\D/', '', $cep) ?? '';
    return strlen($cep) === 8;
}
function is_valid_name(string $name): bool {
    return preg_match('/^[\pL\s\-\']{2,100}$/u', $name) === 1;
}
function sanitize_int($value, int $min = 0, int $max = PHP_INT_MAX): int {
    $int = filter_var($value, FILTER_VALIDATE_INT);
    if ($int === false) return $min;
    if ($int < $min) return $min;
    if ($int > $max) return $max;
    return $int;
}
function sanitize_float($value): float {
    $float = filter_var($value, FILTER_VALIDATE_FLOAT);
    return $float === false ? 0.0 : (float) $float;
}
function money(float $value): string { return 'R$ ' . number_format($value, 2, ',', '.'); }
function redirect(string $url): never { header('Location: ' . $url); exit; }
function flash(string $type, ?string $message = null): ?array {
    if ($message !== null) { $_SESSION['flash'] = [$type, $message]; return null; }
    $item = $_SESSION['flash'] ?? null; unset($_SESSION['flash']); return $item;
}
function is_logged(): bool { return isset($_SESSION['user']); }
function is_admin(): bool {
    if (!is_logged()) return false;
    $type = strtolower(trim((string) ($_SESSION['user']['type'] ?? '')));
    return $type === 'admin';
}
function require_login(): void { if (!is_logged()) { flash('error', 'Entre na sua conta para continuar.'); redirect('login.php'); } }
function require_admin(): void { if (!is_admin()) { flash('error', 'Área exclusiva para administradores.'); redirect('../login.php'); } }
function product_image(array $product): string {
    if (!empty($product['UrlImagem'])) return $product['UrlImagem'];
    $category = strtolower($product['NomeCategoria'] ?? 'computador');
    return 'https://images.unsplash.com/photo-'.($category === 'notebook' ? '1496181133206-80ce9b88a853' : '1587202372775-e229f172b9d7').'?auto=format&fit=crop&w=900&q=85';
}
function current_customer_id(): ?int {
    if (!is_logged()) return null;
    $stmt = db()->prepare('SELECT ID_Cliente FROM tbl_Clientes WHERE LOWER(Email_Cliente) = LOWER(?) LIMIT 1');
    $stmt->execute([$_SESSION['user']['login']]);
    return ($id = $stmt->fetchColumn()) ? (int)$id : null;
}
function current_sale_user_id(): ?int {
    $stmt = db()->prepare('SELECT ID_Usuario FROM tbl_Usuarios WHERE LOWER(TipoUsuario) = LOWER(?) ORDER BY ID_Usuario LIMIT 1');
    $stmt->execute(['admin']);
    $id = $stmt->fetchColumn();
    return $id !== false ? (int) $id : null;
}
function normalize_cpf(string $cpf): string {
    return preg_replace('/\D/', '', $cpf) ?? '';
}
function is_valid_cpf(string $cpf): bool {
    $cpf = normalize_cpf($cpf);
    if (strlen($cpf) !== 11 || preg_match('/^(\d)\1{10}$/', $cpf)) return false;
    $sum = 0;
    for ($i = 0; $i < 9; $i++) $sum += (int)$cpf[$i] * (10 - $i);
    $digit1 = ($sum * 10) % 11;
    $digit1 = $digit1 === 10 ? 0 : $digit1;
    if ($digit1 !== (int)$cpf[9]) return false;
    $sum = 0;
    for ($i = 0; $i < 10; $i++) $sum += (int)$cpf[$i] * (11 - $i);
    $digit2 = ($sum * 10) % 11;
    $digit2 = $digit2 === 10 ? 0 : $digit2;
    return $digit2 === (int)$cpf[10];
}
function is_over_16(string $birthDate): bool {
    $date = DateTimeImmutable::createFromFormat('Y-m-d', $birthDate);
    if (!$date) return false;
    $limit = (new DateTimeImmutable('today'))->sub(new DateInterval('P16Y'));
    return $date <= $limit;
}
function normalize_product_status(int $stock, string $status): string {
    $status = trim($status);
    if ($stock <= 0) {
        return 'Esgotado';
    }
    return $status === '' ? 'Disponível' : $status;
}
function is_product_unavailable(array $product): bool {
    $status = strtoupper(trim((string) ($product['StatusProduto'] ?? '')));
    return $status === 'ESGOTADO' || (int) ($product['QtdEstoque'] ?? 0) <= 0;
}
function is_strong_password(string $password): bool {
    return strlen($password) >= 8
        && preg_match('/[a-z]/', $password)
        && preg_match('/[A-Z]/', $password)
        && preg_match('/\d/', $password)
        && preg_match('/[^A-Za-z0-9]/', $password);
}
function hash_password(string $password): string {
    return hash('sha512', trim($password));
}
function verify_password(string $password, string $storedHash): bool {
    if (str_starts_with($storedHash, '$2') || str_starts_with($storedHash, '$argon2') || str_starts_with($storedHash, '$pbkdf2')) {
        return password_verify($password, $storedHash);
    }
    return hash_equals($storedHash, hash_password($password));
}
function cart_count(): int {
    $customer = current_customer_id(); if (!$customer) return 0;
    $q = db()->prepare('SELECT COALESCE(SUM(ci.Quantidade),0) FROM tbl_CarrinhoItens ci JOIN tbl_Carrinho c ON c.ID_Carrinho=ci.ID_Carrinho WHERE c.ID_Cliente=?');
    $q->execute([$customer]); return (int)$q->fetchColumn();
}
