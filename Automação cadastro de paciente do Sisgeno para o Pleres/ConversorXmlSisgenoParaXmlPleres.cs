using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;


namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    public class ConversorXmlSisgenoParaXmlPleres
    {
        public Modelos_XML.XMLgenConectPleres ConverterXMLSisgeno_ParaXMLGenConect(Modelos_XML.XMLsisgeno xMLsisgeno, string caminhoRelatorioSisgeno)
        {
            ConvertXLStoXLSX(caminhoRelatorioSisgeno);
            var workbook = new XLWorkbook(caminhoRelatorioSisgeno);
            var somenteLinhasPreenchidas = workbook.Worksheet(1).RowsUsed();
            foreach (var row in somenteLinhasPreenchidas)
            {
                try
                {
                    var numLinha = row.Cell(1).Value.ToString();
                    var NomePaciente = row.Cell(2).Value.ToString();
                    var DataNacimento = row.Cell(3).Value.ToString();
                    var sexo = row.Cell(4).Value.ToString();
                    var identificadorAmostra = row.Cell(5).Value.ToString();
                }
                catch
                {

                }
            }

            xMLsisgeno.GetType();
            return null;
        }

        private void ConvertXLStoXLSX(string originalFilePath)
        {
            // Replace "input.xls" with the path to your XLS file


            // Replace "output.xlsx" with the desired path for the XLSX output file
            string inputFilePath = originalFilePath;

            try
            {
                // Open the XLS file
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(inputFilePath, false))
                {
                    // Get the workbook part
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

                    // Get the first worksheet
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                    // Iterate through each row
                    foreach (var row in sheetData.Elements<Row>())
                    {
                        // Iterate through each cell in the row
                        foreach (var cell in row.Elements<Cell>())
                        {
                            var value = cell.CellValue.Text;
                            Console.Write(value + "\t");
                        }
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("Reading complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
