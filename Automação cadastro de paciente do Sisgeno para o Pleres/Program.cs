using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataAtual = DateTime.Now;

            WebBot webBot = new WebBot();
            //baixaxar xml sisgeno
            webBot.BaixarXMLSisgeno_InserirXMLnoPleres("https://sisgeno.aids.gov.br/", "http://pixeon02-app.pleres.net:9812/#/login", dataAtual);

            Console.ReadKey();
        }
    }
}
