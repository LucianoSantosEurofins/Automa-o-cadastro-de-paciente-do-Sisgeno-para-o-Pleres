﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres.Objetos;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    public class ConversorXmlSisgenoParaXmlPleres
    {
        public (Modelos_XML.XMLgenConectPleres, string) ConverterXMLSisgeno_ParaXMLGenConect(string caminhoRelatorioSisgeno, WebDriver webDriver)
        {
            var pacientes = getPacientesFromHTMLFile(caminhoRelatorioSisgeno);
            var caminho = pacientes.Item2;
            var webBot = new WebBot();
            var pacientesComCpf = webBot.getPacientesCpf(pacientes.Item1, webDriver);
            webBot.getDadosDaSolicitacao(webDriver, pacientesComCpf);
            var xmlGenConect = new Modelos_XML.XMLgenConectPleres();
            
            return (xmlGenConect, caminho);
        }

        private List<string> changeFileExtensionAndGetFileContend(string filePath)
        {
            try
            {
                var newFileExtensionPath = Path.ChangeExtension(filePath, ".txt");
                File.Move(filePath, newFileExtensionPath);
                using (StreamReader stream = new StreamReader(newFileExtensionPath, Encoding.UTF8))
                {
                    var htmlContend = stream.ReadToEnd();
                    return new List<string>() { htmlContend, newFileExtensionPath};
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"{ex} {filePath}");
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex} {filePath}");
                return null;
            }
        }
        
        private (List<Paciente>, string) getPacientesFromHTMLFile(string filePath)
        {
            var dadosDoArquivo = changeFileExtensionAndGetFileContend(filePath);

            if (dadosDoArquivo == null)
                return (null, null);

            var newFileExtensionPath = dadosDoArquivo[1];
            var htmlContend = dadosDoArquivo[0];
            var pacientes = new List<Paciente>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContend);

            var table = htmlDocument.DocumentNode.SelectNodes("//table").FirstOrDefault(t => t.InnerText.Contains("Nome do Paciente"));
            var rows = table.SelectNodes("tr");

            foreach (var row in rows)
            {
                var dados = row.SelectNodes("th|td");
                if (dados != null)
                {   
                    if (!dados[0].InnerText.Contains("#"))
                    {
                        var paciente = new Paciente()
                        {
                            Linha =                                                 dados[0].InnerText,
                            NomeDoPaciente =                                        dados[1].InnerText,
                            DataDeNascimento =                                      dados[2].InnerText,
                            SexoAtribuidoAoNascimento =                             dados[3].InnerText,
                            UltimaCargaViralRealizada =                             dados[4].InnerText,
                            DataDaUltimaCargaViral =                                dados[5].InnerText,
                            PacienteEmTratamento =                                  dados[6].InnerText,
                            Gestante =                                              dados[7].InnerText,
                            UFInstituicaoSolicitante =                              dados[8].InnerText,
                            InstituicaoSolicitante =                                dados[9].InnerText,
                            UFInstituicaoColetora =                                 dados[10].InnerText,
                            NomeDoProfissionalSolicitante =                         dados[11].InnerText,
                            UFConselhoDoProfissionalSolicitante =                   dados[12].InnerText,
                            InstituicaoColetora =                                   dados[13].InnerText,
                            IdentificadorDaAmostra =                                dados[14].InnerText,
                            DataDaDigitacaoSolicitacao =                            dados[15].InnerText,
                            DataDaSolicitacao =                                     dados[16].InnerText,
                            DataDaColeta =                                          dados[17].InnerText,
                            DataDoRecebimentoConvencional =                         dados[18].InnerText,
                            DataDoRecebimentoNovosAlvos =                           dados[19].InnerText,
                            DataDaExecucaoConvencional =                            dados[20].InnerText,
                            DataDaExecucaoNovosAlvos =                              dados[21].InnerText,
                            DataDaLiberacaoConvencional =                           dados[22].InnerText,
                            DataDaLiberacaoNovosAlvos =                             dados[23].InnerText,
                            MaterialBiologicoConvencional =                         dados[24].InnerText,
                            MaterialBiologicoNovosAlvos =                           dados[25].InnerText,
                            StatusConvencional =                                    dados[26].InnerText,
                            StatusIntegrase =                                       dados[27].InnerText,
                            StatusGP41 =                                            dados[28].InnerText,
                            StatusMaraviroque =                                     dados[29].InnerText,
                            ForaDeCriterioAutorizadoDIAHV =                         dados[30].InnerText,
                            MensagemDeInconsistencia =                              dados[31].InnerText,
                            MotivoAmostraRejeitadaConvencional =                    dados[32].InnerText,
                            MotivoAmostraRejeitadaIntegrase =                       dados[33].InnerText,
                            MotivoAmostraRejeitadaGP41 =                            dados[34].InnerText,
                            MotivoAmostraRejeitadaMaraviroque =                     dados[35].InnerText,
                            SolicitacaoSimultaneaDosExamesDeCargaViralGenotipagem = dados[36].InnerText
                        };
                        pacientes.Add(paciente);
                    }
                }                   
            }
            return (pacientes, newFileExtensionPath);
        }
    }
}