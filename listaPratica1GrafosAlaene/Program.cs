using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace listaPratica1GrafosAlaene
{
    public class Program
    {
        public static void cabecalho() {
            Console.WriteLine(@"* Aluno: Alaene Rufino de Sousa
* Matrícula: 609992 
* Atividade: 1a lista prática
* Entrega via SGA em: 01/10/2018
* Professora: Eveline Alonso");
        }

        static void Main(string[] args)
        {
            /* Nesse momento estou fazendo firulas de decoração
             */
            cabecalho();

            /* Nesse momento o programa abrirá já carregando os arquivoos para os vetores
             */
            string [] naoDirigido = File.ReadAllLines(@"nao-dirigido.txt");
            string[] dirigido = File.ReadAllLines(@"dirigido.txt");

            Console.ReadKey();
        }
    }
}
