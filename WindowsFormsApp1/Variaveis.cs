using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public static class Variaveis
    {
        public static string CaixaTxtNomeProd { get; set; }
        public static string CaixaTxtDescricaoProd { get; set; }
        public static string CaixaTxtEspecificacoesProd { get; set; }
        public static string CaixaTxtMarcaProd { get; set; }
        public static string CaixaCmbCategoriaProd { get; set; }
        public static string CaixaTxtValorProd { get; set; }
        public static string CaixaTxtAlturaProd { get; set; }
        public static string CCaixaTxtLarguraProd { get; set; }
        public static string CaixaTxtPesoProd { get; set; }
        public static DateTime DataCompra { get; set; }
        public static string DadoIdCliente { get; set; }
        public static string NomeCliente { get; set; }
        public static string SobrenomeCliente { get; set; }
        public static string IdEditora { get; set; }
        public static string IdMarca { get; set; }
        public static string UsuarioLogado { get; set; }
        public static string TipoUsuario { get; set; }

        //String conexão sql
        public static string strConn = "server=localhost; database=db_Tec; uid=admin67; pwd=six@seven;";
    }
}
