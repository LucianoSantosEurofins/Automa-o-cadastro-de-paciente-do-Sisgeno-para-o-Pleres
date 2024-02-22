using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataAtual = DateTime.Now;

            WebBot webBot = new WebBot();
            var linkGenConectPleres = ConfigurationManager.AppSettings["LinkGenConect"];
            var linkSisgeno =         ConfigurationManager.AppSettings["LinkSisgeno"];
            CriarPastaArquivoBaixadoDoSisgeno();
            webBot.BaixarXMLSisgeno_InserirXMLnoPleres(linkSisgeno, linkGenConectPleres, dataAtual);

            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void CriarPastaArquivoBaixadoDoSisgeno()
        {
            var gerenciador = new GerenciadorDeArquivos();
            gerenciador.CreateSisgenoFilesDir("RelatorioSisgeno");
        }
    }
}
