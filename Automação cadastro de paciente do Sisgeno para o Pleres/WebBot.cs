using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Configuration;

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

            IWebElement btnLaboratorio = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[1]/ul/li[2]/a"));
            btnLaboratorio.Click();
            IWebElement txtCPF =         webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[1]/input"));
            IWebElement txtSenha =       webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[2]/input"));
            IWebElement btnEntrar =       webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[4]/div/div/input"));
            
            Thread.Sleep(2000);
            var cpf =   ConfigurationManager.AppSettings["cpfSisgeno"];
            var senha = ConfigurationManager.AppSettings["SenhaSisgeno"];

            try
            {
                txtCPF.SendKeys(cpf);
                txtSenha.SendKeys(senha);
                btnEntrar.Click();
                Thread.Sleep(2000);
                IWebElement dropDown = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div/div/div/select/option[2]"));
                dropDown.Click();
                //Thread.Sleep(1000);
                //webDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr[2]/td[1]/div[2]/table[2]/tbody/tr/td/ul/li[3]")).Click();
                //Thread.Sleep(1000);
                //webDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr[2]/td[1]/div[2]/table[2]/tbody/tr/td/ul/li[3]/ul/li[2]/a")).Click(); 
                webDriver.Navigate().GoToUrl("https://sisgeno.aids.gov.br/appRelatorio/frm_relatorio_situacao_exame.php");
                
                var txtInicio = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/form/div[2]/div[1]/input"));
                var txtFim = webDriver.FindElement(By.Id("fim"));

                txtFim.SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));
                txtInicio.SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));

                webDriver.FindElement(By.XPath("/html/body/fieldset/legend/h4/strong")).Click();
                IWebElement btnEnviar = webDriver.FindElement(By.XPath("/html/body/fieldset/div/div/form/div[5]/div/input"));
                btnEnviar.Click();
                webDriver.SwitchTo().Alert().Accept();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Não foi possível simular ações do usuário no Sisgeno {ex}");
            }

            var conversor = new ConversorXmlSisgenoParaXmlPleres();
            var caminhoArquivo = @"C:\Users\d9lb\OneDrive - Eurofins\Documentos\Relatorios SISGENO\exame_data_digit_solicitacao (5).xls"; 
            var caminho = conversor.ConverterXMLSisgeno_ParaXMLGenConect(new Modelos_XML.XMLsisgeno(), caminhoArquivo).Item2;

            InserirXMLNoGenConectPleres(genConectLink, dataPacientes, webDriver, caminhoArquivo);

            return caminho;
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
