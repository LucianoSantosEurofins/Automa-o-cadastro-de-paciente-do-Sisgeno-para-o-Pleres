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

            Console.WriteLine(dataAtual.ToString("dd/MM/yyyy"));
            webBot.BaixarXMLSisgeno("https://sisgeno.aids.gov.br/", dataAtual);

            webBot.InserirXMLNoGenConectPleres("genconect");
            Console.ReadKey();
        }
    }
}
