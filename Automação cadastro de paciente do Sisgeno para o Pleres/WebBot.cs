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
        public void BaixarXMLSisgeno_InserirXMLnoPleres(string sisgenoLink,string genConectLink, DateTime dataPacientes)
        {
            WebDriver webDriver = new OpenQA.Selenium.Edge.EdgeDriver();
            webDriver.Navigate().GoToUrl(sisgenoLink);

            IWebElement btnLaboratorio = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[1]/ul/li[2]/a"));
            btnLaboratorio.Click();
            IWebElement txtCPF =         webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[1]/input"));
            IWebElement txtSenha =       webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[2]/input"));
            Thread.Sleep(2000);
            var cpf =   ConfigurationManager.AppSettings["cpfSisgeno"];
            var senha = ConfigurationManager.AppSettings["SenhaSisgeno"];

            txtCPF.SendKeys  (cpf);
            txtSenha.SendKeys(senha);
            InserirXMLNoGenConectPleres(genConectLink, dataPacientes, webDriver);
        }

        public void InserirXMLNoGenConectPleres(string genConectLink, DateTime dataPaciente, WebDriver webDriver) 
        {
            webDriver.Navigate().GoToUrl(genConectLink);
            IWebElement txtEmail =       webDriver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div/div/div/section/div/form/div[1]/div/input"));
            IWebElement txtSenha =       webDriver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div/div/div/section/div/form/div[2]/div/input"));
            IWebElement btnAcessar =     webDriver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div/div/div/section/div/form/div[3]/div/button"));
            Thread.Sleep(2000);
            var email =   ConfigurationManager.AppSettings["EmailGenConect"];
            var senha =   ConfigurationManager.AppSettings["SenhaGenConect"];

            txtEmail.SendKeys(email);
            txtSenha.SendKeys(senha);

            btnAcessar.Click();
            webDriver.Quit();

            var conversorXmlSisgenoParaXmlPleres = new ConversorXmlSisgenoParaXmlPleres();
            conversorXmlSisgenoParaXmlPleres.ConverterXMLSisgeno_ParaXMLGenConect(new Modelos_XML.XMLsisgeno(), @"C:\Users\d9lb\OneDrive - Eurofins\Documentos\Relatorios SISGENO\exame_data_digit_solicitacao.xls");
        }
    }
}
