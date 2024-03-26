using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Configuration;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Interactions;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    public class WebBot
    {
        public string BaixarXMLSisgeno_InserirXMLnoPleres(string sisgenoLink,string genConectLink, DateTime dataPacientes)
        {
            var options = new OpenQA.Selenium.Edge.EdgeOptions();
            var downLoadPath = @"C:\Users\d9lb\OneDrive - Eurofins\Área de Trabalho\TesteArquivosSisgeno";
            options.AddUserProfilePreference("download.default_directory", downLoadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);           
            WebDriver webDriver = new OpenQA.Selenium.Edge.EdgeDriver(options);
            webDriver.Navigate().GoToUrl(sisgenoLink);
            Actions action = new Actions(webDriver);
            action.SendKeys(OpenQA.Selenium.Keys.Escape).Perform();
            IWebElement btnLaboratorio = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[1]/ul/li[2]/a"));
            btnLaboratorio.Click();
            IWebElement txtCPF =         webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[1]/input"));
            IWebElement txtSenha =       webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[2]/input"));
            IWebElement btnEntrar =      webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[4]/div/div/input"));          
            Thread.Sleep(2000);
            var cpf =   ConfigurationManager.AppSettings["cpfSisgeno"];
            var senha = ConfigurationManager.AppSettings["SenhaSisgeno"];

            try
            {
                txtCPF.SendKeys(cpf);
                txtSenha.SendKeys(senha);
                btnEntrar.Click();
                Thread.Sleep(1000);
                IWebElement dropDown = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div/div/div/select/option[2]"));
                dropDown.Click();
                Thread.Sleep(1000);
                webDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["linkRelatorioSisgeno"]);
                
                var txtInicio = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/form/div[2]/div[1]/input"));
                var txtFim = webDriver.FindElement(By.Id("fim"));

                txtFim.SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));
                txtInicio.SendKeys(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy"));

                webDriver.FindElement(By.XPath("/html/body/fieldset/legend/h4/strong")).Click();
                IWebElement btnEnviar = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/form/div[5]/div/input"));
                btnEnviar.Click();
                Thread.Sleep(6000);
                var conversor = new ConversorXmlSisgenoParaXmlPleres();
                var caminhoArquivo = getTempFileAndChangeExtension(downLoadPath);
                var caminho = conversor.ConverterXMLSisgeno_ParaXMLGenConect(caminhoArquivo, webDriver).Item2;
                InserirXMLNoGenConectPleres(genConectLink, dataPacientes, webDriver, caminhoArquivo);
                return caminho;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Não foi possível simular ações do usuário no Sisgeno {ex}");
                return null;
            }            
        }
        public void getDadosDaSolicitacao(WebDriver webDriver, List<Objetos.Paciente> pacientes)
        {
            webDriver.Navigate().GoToUrl("https://sisgeno.aids.gov.br/appSolicitacao/frmNewConsultaPaciente.php");
            var txtPaciente = webDriver.FindElement(By.Id("nome_pac"));

            foreach (var paciente in pacientes)
            {
                txtPaciente.SendKeys(paciente.NomeDoPaciente);
                try
                {
                    var btnPesquisar = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/form/div[4]/button"));
                    btnPesquisar.Click();
                    var divTabelaResultado = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/div[2]/div[2]"));
                }
                catch
                {

                }
                txtPaciente.Clear();
            }

        }
        public List<Objetos.Paciente> getPacientesCpf(List<Objetos.Paciente> pacientes, WebDriver webDriver)
        {
            try
            {
                webDriver.Navigate().GoToUrl(ConfigurationManager.AppSettings["linkHistoricoSisgeno"]);

                foreach(var paciente in pacientes)
                {
                    try
                    {
                        IWebElement txtNome = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/form/div[1]/input"));
                        IWebElement btnBuscar = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/form/div[3]/input"));

                        txtNome.Clear();
                        txtNome.SendKeys(paciente.NomeDoPaciente.Trim());
                        btnBuscar.Click();

                        Thread.Sleep(35000);
                        var htmlContend = webDriver.PageSource;
                        var result = FiltrarPacienteNaPagina(paciente, htmlContend);

                        //((IJavaScriptExecutor)webDriver).ExecuteScript("window.open();");
                        webDriver.SwitchTo().NewWindow(WindowType.Tab);
                        webDriver.Navigate().GoToUrl(result[1]);
                        Thread.Sleep(500);
                        var cpf = filtrarCpfDoTexto(webDriver.PageSource);
                        webDriver.Close();
                        webDriver.SwitchTo().Window(webDriver.WindowHandles.First());
                        paciente.cpfPaciente = cpf;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"erro ao capturar cpf do paciente {paciente.NomeDoPaciente} {ex}");
                    }
                    
                }           
            }
            catch (Exception ex)
            {
                Console.WriteLine($"erro {ex}");
            }

            return pacientes;
        }
        private string filtrarCpfDoTexto(string texto)
        {
            Regex regex = new Regex(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})");

            // Encontrar correspondências
            MatchCollection matches = regex.Matches(texto);

            // Verificar se há correspondências
            if (matches.Count > 0)
            {              
                foreach (Match match in matches)
                {
                    return match.Value;
                }
            }
            else
            {
                return "";
            }

            return "";
        }
        private List<string> FiltrarPacienteNaPagina(Objetos.Paciente Paciente, string htmlContend)
        {
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContend);
                var table = htmlDocument.DocumentNode.SelectNodes("//table");
                var rows = table.FirstOrDefault().SelectNodes("tbody").FirstOrDefault().SelectNodes("tr").FirstOrDefault(tr => tr.InnerText.Contains(Paciente.DataDeNascimento.Trim()));
                var informacoeLinkSemParametro = "https://sisgeno.aids.gov.br/appConsultas/historicoPaciente.php?cd_pac=@PACIENTEID%20&login=";

                var columns = rows.SelectNodes("td");
                var nome =                     columns[0].InnerText;
                var dataNascimento =           columns[1].InnerText;
                var btnPesquisar =             columns[10].InnerHtml;
                HtmlDocument htmlDocument2 = new HtmlDocument();
                htmlDocument2.LoadHtml(btnPesquisar);
                var idPaciente = htmlDocument2.DocumentNode.SelectNodes("a").FirstOrDefault().Attributes["data-cd-pac"].Value;
                var linkComParametro = informacoeLinkSemParametro.Replace("@PACIENTEID", idPaciente);
                return new List<string> { Paciente.NomeDoPaciente, linkComParametro };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Não foi possivel obter dados do paciente: {Paciente.NomeDoPaciente},  {ex}");
                return null;
            } 
        }
        public string getTempFileAndChangeExtension(string pastaRaiz)
        {           
            try
            {
                DirectoryInfo d = new DirectoryInfo(pastaRaiz);
                FileInfo[] Files = d.GetFiles("*.*");
                var result = Files[0].FullName;
                var newFile = Path.ChangeExtension(result, ".xls");
                File.Move(result, newFile);
                return newFile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public void InserirXMLNoGenConectPleres(string genConectLink, DateTime dataPaciente, WebDriver webDriver, string caminhoArquivo) 
        {
            webDriver.Navigate().GoToUrl(genConectLink);
            IWebElement txtEmail =       webDriver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div/div/div/section/div/form/div[1]/div/input"));
            IWebElement txtSenha =       webDriver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div/div/div/section/div/form/div[2]/div/input"));
            IWebElement btnAcessar =     webDriver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div/div/div/section/div/form/div[3]/div/button"));
            Thread.Sleep(2000);
            var email =   ConfigurationManager.AppSettings["EmailGenConect"];
            var senha =   ConfigurationManager.AppSettings["SenhaGenConect"];

            try
            {
                txtEmail.SendKeys(email);
                txtSenha.SendKeys(senha);
                //outras ações devem ser inseridas aqui
                btnAcessar.Click();
                webDriver.Quit();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Não foi possível simular ações do usuário no genConnect {ex}");
            }          
        }
    }
}