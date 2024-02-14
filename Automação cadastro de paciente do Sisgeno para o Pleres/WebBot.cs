using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    public class WebBot
    {
        public void BaixarXMLSisgeno(string sisgenoLink, DateTime dataPacientes)
        {
            WebDriver webDriver = new OpenQA.Selenium.Edge.EdgeDriver();
            webDriver.Navigate().GoToUrl(sisgenoLink);

            IWebElement btnLaboratorio = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[1]/ul/li[2]/a"));
            btnLaboratorio.Click();
            IWebElement txtCPF = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[1]/input"));
            IWebElement txtSenha = webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[3]/div[2]/form/div[2]/input"));
            
            txtCPF.SendKeys("73298097055");
            txtSenha.SendKeys("Senha"); 
                       
            //webDriver.Quit();
        }
    }
}
