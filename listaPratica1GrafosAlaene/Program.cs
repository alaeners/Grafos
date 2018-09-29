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
        static GrafoDirigido grafoD;
        static GrafoNaoDirigido grafoND;
        public static void cabecalho()
        {
            Console.WriteLine(@"* Aluno: Alaene Rufino de Sousa
* Matrícula: 609992 
* Atividade: 1a lista prática
* Entrega via SGA em: 01/10/2018
* Professora: Eveline Alonso");
        }

        public static void carregaGrafo() {

            grafoD = new GrafoDirigido();
            grafoND = new GrafoNaoDirigido();

            grafoD.ListaVertice = new List<Vertice>();
            grafoD.ListaAresta = new List<ArestaDirigida>();

            grafoND.ListaVertice = new List<Vertice>();
            grafoND.ListaAresta = new List<ArestaNaoDirigida>();

            /* Nesse momento o programa abrirá já carregando os arquivoos para os vetores */
            string[] naoDirigido = File.ReadAllLines(@"nao-dirigido.txt");
            string[] dirigido = File.ReadAllLines(@"dirigido.txt");

            //Inserindo vertices na lista
            for (int i = 0; i < int.Parse(naoDirigido[0]); i++)
            {
                grafoND.ListaVertice.Add(new Vertice(i + 1, ref grafoND));
            
            }

            for (int i = 0; i < int.Parse(dirigido[0]); i++)
            {
                grafoD.ListaVertice.Add(new Vertice(i + 1, ref grafoD));

            }

            //inserindo as arestas 
            for (int i = 1; i < naoDirigido.Length; i++)
            {
                Vertice v1, v2;
                string[] linhaArq = naoDirigido[i].Split(';');

                v1 = grafoND.ListaVertice.Find(x => x.valor == int.Parse(linhaArq[0]));
                v2 = grafoND.ListaVertice.Find(x => x.valor == int.Parse(linhaArq[1]));
                grafoND.ListaAresta.Add(new ArestaNaoDirigida(v1,v2,int.Parse(linhaArq[2])));
            }

            for (int i = 1; i < dirigido.Length; i++)
            {
                Vertice v1, v2;
                string[] linhaArq = dirigido[i].Split(';');

                v1 = grafoD.ListaVertice.Find(x => x.valor == int.Parse(linhaArq[0]));
                v2 = grafoD.ListaVertice.Find(x => x.valor == int.Parse(linhaArq[1]));
                grafoD.ListaAresta.Add(new ArestaDirigida(v1, v2, int.Parse(linhaArq[2]), int.Parse(linhaArq[3])));
            }
        }

        static void Main(string[] args)
        {
            /* Nesse momento estou fazendo firulas de decoração
             */
            cabecalho();
            Console.WriteLine("Estamos lendo e carregando os arquivo, aguarde. . . ");
            Console.Write("");

            carregaGrafo();

            Console.WriteLine("Agora que lemos os arquivos, escolha qual ação deseja realizar");
            Console.Write("");
            Console.WriteLine("Digite:  \n\t 1 para Grafos DIRIGIDOS \n\t 2  para Grafos NÃO DIRIGIDO");
            int opcao = int.Parse(Console.ReadLine());
            Console.Write("");

            int cont = 0;
            while (opcao != 1 && opcao != 2)
            {
                Console.Write("");
                Console.Clear();
                Console.WriteLine("ESCOLHA NOVAMENTE ------ Essa é sua tentaiva " + cont++);
                Console.WriteLine("Digite:  \n\t 1 para Grafos NÃO DIRIGIDOS \n\t 2  para Grafos DIRIGIDO");
                opcao = int.Parse(Console.ReadLine());                
            }

            if (opcao == 2)
            {
                opcao = 0;
                Console.Write("");
                Console.Clear();
                Console.WriteLine("Ótimo, vamos para GRAFOS DIRIGIDOS, escolha o método:");
                Console.Write("");
                Console.Write(@"ESCOLHA 1 para : • int getGrauEntrada (Vertice v1);
ESCOLHA 2 para : • int getGrauSaida (Vertice v1);");
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        foreach (Vertice v in grafoD.ListaVertice)
                        {
                            Console.WriteLine(" "+grafoD.getGrauEntrada(v));
                            Console.Write("");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        foreach (Vertice v in grafoD.ListaVertice)
                        {
                            Console.WriteLine(" " + grafoD.getGrauSaida(v));
                            Console.Write("");
                            Console.ReadKey();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.Write("");
                Console.Clear();
                Console.WriteLine("Ótimo, vamos para GRAFOS NÃO DIRIGIDOS");
            }

            switch (opcao)
            {

                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}