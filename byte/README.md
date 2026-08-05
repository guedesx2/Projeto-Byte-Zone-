# Byte Zone — loja PHP

Loja em PHP e MySQL para **PC Gamer, desktops e notebooks**, com visual roxo original, área do cliente e painel administrativo.

## Instalação no phpMyAdmin

1. Abra o phpMyAdmin e use a aba **Importar**.
2. Importe `database/db_Tec.sql`. Ele cria exatamente as tabelas usadas pela loja.
3. Opcionalmente, importe `database/demo_produtos.sql` para já visualizar quatro produtos na vitrine.
4. Em `config/database.php`, confira usuário, senha, porta e nome do banco. Os valores iniciais estão configurados para `admin67`, `six@seven` e `db_Tec` conforme o banco informado.
5. Copie todos os arquivos para a pasta do seu servidor local (por exemplo, `htdocs/byte-zone` no XAMPP) e abra `http://localhost/byte-zone/`.
6. Acesse `http://localhost/byte-zone/criar_admin.php`, crie o primeiro administrador e **apague esse arquivo depois**.

## O que cada área faz

- `index.php`: vitrine inicial e destaques.
- `catalogo.php` e `produto.php`: busca, filtro e detalhes dos produtos.
- `login.php`, `cadastro.php`, `minha_conta.php`: conta e pedidos do cliente.
- `carrinho.php` e `checkout.php`: carrinho e criação de venda.
- `admin/`: painel de administrador com produtos, imagem, preços, estoque, pedidos e usuários.
- `config/database.php`: conexão com o MySQL.

## Observação importante

O login do cliente usa o e-mail como `Login`, o que permite relacionar a conta a `tbl_Clientes` sem alterar o banco que você enviou. Senhas são armazenadas com hash seguro do PHP.
