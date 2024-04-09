using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres.Objetos
{
    public class Paciente
    {
        public string Linha { get; set; }
        public string NomeDoPaciente { get; set; }
        public string DataDeNascimento { get; set; }
        public string SexoAtribuidoAoNascimento { get; set; }
        public string UltimaCargaViralRealizada { get; set; }
        public string DataDaUltimaCargaViral { get; set; }
        public string PacienteEmTratamento { get; set; }
        public string Gestante { get; set; }
        public string UFInstituicaoSolicitante { get; set; }
        public string InstituicaoSolicitante { get; set; }
        public string UFInstituicaoColetora { get; set; }
        public string NomeDoProfissionalSolicitante { get; set; }
        public string UFConselhoDoProfissionalSolicitante { get; set; }
        public string InstituicaoColetora { get; set; }
        public string IdentificadorDaAmostra { get; set; }
        public string DataDaDigitacaoSolicitacao { get; set; }
        public string DataDaSolicitacao { get; set; }
        public string DataDaColeta { get; set; }
        public string DataDoRecebimentoConvencional { get; set; }
        public string DataDoRecebimentoNovosAlvos { get; set; }
        public string DataDaExecucaoConvencional { get; set; }
        public string DataDaExecucaoNovosAlvos { get; set; }
        public string DataDaLiberacaoConvencional { get; set; }
        public string DataDaLiberacaoNovosAlvos { get; set; }
        public string MaterialBiologicoConvencional { get; set; }
        public string MaterialBiologicoNovosAlvos { get; set; }
        public string StatusConvencional { get; set; }
        public string StatusIntegrase { get; set; }
        public string StatusGP41 { get; set; }
        public string StatusMaraviroque { get; set; }
        public string ForaDeCriterioAutorizadoDIAHV { get; set; }
        public string MensagemDeInconsistencia { get; set; }
        public string MotivoAmostraRejeitadaConvencional { get; set; }
        public string MotivoAmostraRejeitadaIntegrase { get; set; }
        public string MotivoAmostraRejeitadaGP41 { get; set; }
        public string MotivoAmostraRejeitadaMaraviroque { get; set; }
        public string SolicitacaoSimultaneaDosExamesDeCargaViralGenotipagem { get; set; }
        public string cpfPaciente { get; set; }
        public string crmMedico { get; set; }
    }

}
