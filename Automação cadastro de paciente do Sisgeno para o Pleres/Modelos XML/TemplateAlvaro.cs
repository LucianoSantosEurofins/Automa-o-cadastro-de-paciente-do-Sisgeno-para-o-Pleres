using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres.Modelos_XML
{
    public  class TemplateAlvaro
    {
        [XmlRoot("solicitacoes")]

        public class solicitacoes
        {
            [XmlAttribute]
            public DateTime datehora { get; set; }
            [XmlAttribute]
            public string idAgente { get; set; }
            [XmlAttribute]
            public string lis { get; set; }
            [XmlAttribute]
            public string operador { get; set; }
            [XmlAttribute]
            public string senha { get; set; }
            [XmlAttribute]
            public string versao { get; set; }
            [XmlElement]
            public entidade entidade { get; set;
            }
        }

        public class entidade
        {
            [XmlAttribute]
            public string chave { get; set; }
            [XmlAttribute]
            public string codigo { get; set; }
        }

        public class paciente
        {
            [XmlAttribute]
            public string codigolis { get; set; }


            [XmlAttribute]
            public string datanasc { get; set; }


            [XmlAttribute]
            public string nome { get; set; }


            [XmlAttribute]
            public string nomeSocial { get; set; }


            [XmlAttribute]
            public string sexo { get; set; }


            [XmlAttribute]
            public string endereco { get; set; }


            [XmlAttribute]
            public string cpf { get; set; }
        }

        public class medico
        {
            [XmlAttribute]
            public string crm { get; set; }
            [XmlAttribute]
            public string nome { get; set; }
        }

        public class solicitacao
        {
            [XmlAttribute]
            public string nome { get; set; }

            [XmlAttribute]
            public string codigopaciente { get; set; }
            [XmlAttribute]
            public string crm { get; set; }
            [XmlAttribute]
            public DateTime data { get; set; }
            [XmlAttribute]
            public DateTime datacoleta { get; set; }
            [XmlAttribute]
            public string observacao { get; set; }
        }

    }
}
