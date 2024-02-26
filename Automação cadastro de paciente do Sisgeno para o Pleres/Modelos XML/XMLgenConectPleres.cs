using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres.Modelos_XML
{
    public class XMLgenConectPleres
    {
        [XmlIgnore]
        public string caminhoArquivoOrigem { get; set; }
    }
}
