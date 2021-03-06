﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
/*Alaene Rufino de Sousa - 609992 */
namespace ProjetoFinal_Grafos2018
{
    /*Aqui o B.O é gigantesco
     * é a mãe da gambiarra
     * go horse todinho
     */

    class Program
    {
        /*Acesso as classes de Dirigidos ou não */
        static GrafoDirigido Voos;
        static GrafoNaoDirigido Rotas;
        /*que Deus abençõe a Main */
        static void Main(string[] args)
        {
            /*instanciando voo e rotas */
            Voos = new GrafoDirigido();
            Rotas = new GrafoNaoDirigido();
            /*lendo arquivos e armazenando num vetor */
            String[] arquivos = File.ReadAllLines("arquivo.txt");
            /*variaveis para poder recuperar as informações no vetor*/
            string nome1, nome2;
            int direcao;
            int distancia;
            TimeSpan duracao;
            List<TimeSpan> horarios = new List<TimeSpan>();
            int id = 0;
            /*leitura do arquivo e a criação dos objetos */
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (i != 0)
                {
                    string[] aux = arquivos[i].Split(';');
                    nome1 = aux[0];
                    nome2 = aux[1];
                    direcao = int.Parse(aux[2]);
                    distancia = int.Parse(aux[3]);
                    duracao = new TimeSpan(int.Parse(aux[4].Split(':')[0]), int.Parse(aux[4].Split(':')[1]), 0);
                    horarios = new List<TimeSpan>();
                    for (int j = 5; j < aux.Length; j++)
                    {
                        horarios.Add(new TimeSpan(int.Parse(aux[j].Split(':')[0]), int.Parse(aux[j].Split(':')[1]), 0));
                    }

                    Vertices aeroportoA = Voos.buscaAeroporto(nome1);
                    if (aeroportoA == null)
                    {
                        aeroportoA = new Vertices(++id, nome1);
                    }
                    Rotas.inserirVertice(aeroportoA);
                    Voos.inserirVertice(aeroportoA);

                    Vertices aeroportoB = Voos.buscaAeroporto(nome2);
                    if (aeroportoB == null)
                    {
                        aeroportoB = new Vertices(++id, nome2);
                    }
                    Rotas.inserirVertice(aeroportoB);
                    Voos.inserirVertice(aeroportoB);

                    Arestas rota = new Arestas(aeroportoA, aeroportoB, distancia);
                    Rotas.inserirRota(rota);

                    ArestasDirigidas arestaDirigida = new ArestasDirigidas(aeroportoA, aeroportoB, direcao, distancia, duracao, horarios);
                    Voos.inserirAresta(arestaDirigida);
                }
            }
            /*menuzinho com frufru, porque sou dessas
             * não tem muito o que dizer... é um switch que pega o dado do user e faz o que é necessário
             * não preocupei muito com tratamento de exeções porque se nao esse negocio nao ia acabar nunca
             * mas tive o cuidado de colocar um ToUpperCase() para usar sempre maiusculo nas entradas do usuário
             */
            string acao = string.Empty;
            string origem, destino = string.Empty, horariostring = string.Empty;
            TimeSpan horario = new TimeSpan();

            bool flg = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Selecione uma ação: ");
                Console.WriteLine("1 - Calcular a menor rota em escalas");
                Console.WriteLine("2 - Calcular a menor rota em distância percorrida");
                Console.WriteLine("3 - Calcular a menor rota em tempo de voo");
                Console.WriteLine("4 - Calcular a menor rota em tempo total");
                Console.WriteLine("5 - Verificar se todos os aeroportos estão conectados (ou quais grupos estão)");
                Console.WriteLine("6 - Como não chegar atrasado na reunião");
                Console.WriteLine("7 - Quais rota serão utilizadas na minha empresa de tráfico aéreo");
                Console.WriteLine("0 - Sair");

                acao = Console.ReadLine();

                switch (acao)
                {
                    case "1":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de origem?");
                            origem = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == origem) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        flg = false;
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de destino?");
                            destino = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == destino) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        Console.Clear();
                        Voos.menorCaminhoEmConexoes(origem, destino);
                        Console.WriteLine("Aperte qualquer botão para continuar");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de origem?");
                            origem = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == origem) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        flg = false;
                        Console.Clear();

                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de destino?");
                            destino = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == destino) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        Console.Clear();
                        Voos.MenorCaminhoDistancia(origem, destino);
                        Console.WriteLine("Aperte qualquer botão para continuar");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de origem?");
                            origem = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == origem) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        flg = false;
                        Console.Clear();

                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de destino?");
                            destino = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == destino) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        Console.Clear();
                        Voos.MenorCaminhoTempo(origem, destino);
                        Console.WriteLine("Aperte qualquer botão para continuar");
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de origem?");
                            origem = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == origem) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        flg = false;
                        Console.Clear();

                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de destino?");
                            destino = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == destino) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        Console.Clear();
                        Voos.MenorCaminhoTempoTotal(origem, destino);
                        Console.WriteLine("Aperte qualquer botão para continuar");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Clear();
                        Voos.verificaSeGrafoFortementeConexo();
                        Console.WriteLine("Aperte qualquer botão para continuar");
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de origem?");
                            origem = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == origem) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        flg = false;
                        Console.Clear();

                        do
                        {
                            Console.WriteLine("Qual é o aeroporto de destino?");
                            destino = Console.ReadLine().ToUpper();
                            if (Voos.Vertices.FirstOrDefault(x => x.Aeroporto == destino) == null)
                            {
                                Console.WriteLine("Aeroporto não encontrado.");
                                continue;
                            }
                            flg = true;
                        } while (!flg);

                        Console.Clear();
                        flg = false;
                        
                        do
                        {
                            Console.WriteLine("Digite o horário que da reunião no formato hh:mm:");
                            horariostring = Console.ReadLine();

                            if (int.Parse(horariostring.Split(':')[0]) > 23 || int.Parse(horariostring.Split(':')[1]) > 59)
                            {
                                Console.WriteLine("Horario inválido");
                                continue;
                            }
                            horario = new TimeSpan(int.Parse(horariostring.Split(':')[0]), int.Parse(horariostring.Split(':')[1]), 0);
                            flg = true;
                        } while (!flg);

                        Console.Clear();
                        Voos.ComoNaoChegarAtrasadoNaReuniao(origem, destino, horario);
                        Console.WriteLine("Aperte qualquer botão para continuar");
                        Console.ReadKey();
                        break;

                    case "7":
                        Console.Clear();
                        Rotas.RotasEfetivasUsadas();
                        Console.WriteLine("Aperte qualquer botão para continuar");
                        Console.ReadKey();
                        break;
                    case "0":
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ação inválida. Favor repetir.");
                        Thread.Sleep(1500);
                        break;
                }

            } while (acao != "0");
        }
        /*Chamada (métodos com nomes claros das suas funções, eu amo <3)  */
        private static void MenorCaminhoTempoTotal(string origem, string destino)
        {
            Voos.MenorCaminhoTempoTotal(origem, destino);
        }
        /*Chamada (métodos com nomes claros das suas funções, eu amo <3)  */
        private static void ComoNaoChegarAtrasadoNaReuniao(string origem, string destino, TimeSpan horario)
        {
           Voos.ComoNaoChegarAtrasadoNaReuniao(origem, destino, horario);
        }
        /*Chamada (métodos com nomes claros das suas funções, eu amo <3)  */
        private static void RotasEfetivamenteUtilizadas()
        {
            Rotas.RotasEfetivasUsadas();
        }
        /*Chamada (métodos com nomes claros das suas funções, eu amo <3)  */
        private static void verificaSeGrafoFortementeConexo()
        {
            Voos.verificaSeGrafoFortementeConexo();
        }
        /*Chamada (métodos com nomes claros das suas funções, eu amo <3)  */
        private static void menorCaminhoEmDisntancia(string origem, string destino)
        {
            Voos.MenorCaminhoDistancia(origem, destino);
        }
        /*Chamada (métodos com nomes claros das suas funções, eu amo <3)  */
        static void menorCaminhoEmConexoes(string origem, string destino)
        {
            Voos.menorCaminhoEmConexoes(origem, destino);
        }
    }
}
