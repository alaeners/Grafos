using System;
using System.Collections.Generic;
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

        public static void carregaGrafo()
        {

            grafoD = new GrafoDirigido();
            grafoND = new GrafoNaoDirigido();

            grafoD.ListaVertice = new List<Vertice>();
            grafoD.ListaAresta = new List<ArestaDirigida>();

            grafoND.ListaVertice = new List<Vertice>();
            grafoND.ListaAresta = new List<ArestaNaoDirigida>();

            /* Nesse momento o programa abrirá já carregando os arquivoos para os vetores */
            string[] naoDirigido = File.ReadAllLines(@"nao-dirigido.txt");
            string[] dirigido = File.ReadAllLines(@"dirigido.txt");

            grafoND.numVertice = int.Parse(naoDirigido[0]);
            //Inserindo vertices na lista
            for (int i = 0; i < grafoND.numVertice; i++)
            {
                grafoND.ListaVertice.Add(new Vertice(i + 1, ref grafoND));

            }

            grafoD.numVertice = int.Parse(dirigido[0]);
            for (int i = 0; i < grafoD.numVertice; i++)
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
                grafoND.ListaAresta.Add(new ArestaNaoDirigida(v1, v2, int.Parse(linhaArq[2])));
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

            if (opcao == 1)
            {
                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Dirigido - Grau de Entrada");

                foreach (Vertice v in grafoD.ListaVertice)
                {
                    Console.WriteLine(" " + grafoD.getGrauEntrada(v));
                    Console.Write("");
                }
                
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Dirigido - Grau de Saída");
                foreach (Vertice v in grafoD.ListaVertice)
                {
                    Console.WriteLine(" " + grafoD.getGrauSaida(v));
                    Console.Write("");
                }
                Console.ReadKey();

            }
            else
            {
                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - isAdjacente");
                foreach (Vertice v in grafoND.ListaVertice)
                {
                    foreach (var v2 in grafoND.ListaVertice)
                    {
                        if (v != v2)
                        {
                            Console.WriteLine("isAdjacente({0}, {1}) " + grafoND.isAdjacente(v, v2), v.valor, v2.valor);
                        }
                    }
                }
                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - getGrau");
                foreach (Vertice v in grafoND.ListaVertice)
                {
                    Console.WriteLine(" " + grafoND.getGrau(v));
                }
                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - isIsolado");
                foreach (Vertice v in grafoND.ListaVertice)
                {
                    Console.WriteLine(" " + grafoND.isIsolado(v));
                }
                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - isPendente");
                foreach (Vertice v in grafoND.ListaVertice)
                {
                    Console.WriteLine(" " + grafoND.isPendente(v));
                }
                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - isRegular");

                Console.WriteLine(" " + grafoND.isRegular());

                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - isNulo");

                Console.WriteLine(" " + grafoND.isNulo());

                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - isComplete");

                Console.WriteLine(" " + grafoND.isCompleto());

                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - isConexo");

                Console.WriteLine(" " + grafoND.isConexo());

                ProximoMetodo();

                Console.Clear();
                cabecalho();
                Console.WriteLine("--- * --- * --- * --- * --- * --- * --- * --- * ");
                Console.WriteLine("Grafo Não Dirigido - GetComplementar");
                GrafoNaoDirigido complementar = grafoND.getComplementar();
                if (complementar.ListaAresta.Count > 0)
                {
                    foreach (ArestaNaoDirigida item in complementar.ListaAresta)
                    {
                        Console.WriteLine(item.v1.valor + ", " + item.v2.valor + ", " + item.peso);
                    }
                }
                else
                {
                    Console.WriteLine("O grafo é completo, portanto não existe um grafo complementar");
                }
                
                Console.WriteLine("Aperte qualquer tecla para finalizar.");
                Console.ReadKey();
            }

            switch (opcao)
            {

                default:
                    break;
            }
        }

        private static void ProximoMetodo()
        {
            Console.WriteLine("Aperte qualquer tecla para o próximo método");
            Console.ReadKey();
        }
    }
}