using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automação_cadastro_de_paciente_do_Sisgeno_para_o_Pleres
{
    public class GerenciadorDeArquivos
    {
        public string CreateSisgenoFilesDir(string pastaRaiz)
        {
            try
            {
                var path = pastaRaiz;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Falha ao criar pasta {ex}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                return null;
            }
            
        }

        private string ChangeFileName(string originalFileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            var renamedFile = $"{fileNameWithoutExtension}{DateTime.Now.ToString("dd-MM-yyyy_hh-mm")}.txt";
            return renamedFile;
        }

        public void MoverArquivoUsadoDoSisgeno(string pastaRaiz, string fileName)
        {
            string caminhoOrigem = System.IO.Path.Combine(pastaRaiz, fileName);
            string caminhoDestino = System.IO.Path.Combine(pastaRaiz, System.IO.Path.GetFileName(ChangeFileName(fileName)));

            try
            {
                File.Move(caminhoOrigem, caminhoDestino);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro ao mover arquivo: {ex}");
            }
        }
    }
}
