using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    public class ConversorXmlSisgenoParaXmlPleres
    {
        public Modelos_XML.XMLgenConectPleres ConverterXMLSisgeno_ParaXMLGenConect(Modelos_XML.XMLsisgeno xMLsisgeno, string caminhoRelatorioSisgeno)
        {
            var workbook = new XLWorkbook(caminhoRelatorioSisgeno);
            var somenteLinhasPreenchidas = workbook.Worksheet(1).RowsUsed();

            foreach (var row in somenteLinhasPreenchidas)
            {
                try
                {
                    var identificadorAmostra = row.Cell(1).Value.ToString();
                    var NomePaciente = row.Cell(1).Value.ToString();
                    var DataNacimento = row.Cell(1).Value.ToString();
                    var DataSolicitacao = row.Cell(1).Value.ToString();
                    //var identificadorAmostra = row.Cell(1).Value.ToString();
                }
                catch
                {

                }
            }

            xMLsisgeno.GetType();
            return null;
        }
    }
}
