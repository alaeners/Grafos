﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Alaene Rufino de Sousa - 609992 */
namespace ProjetoFinal_Grafos2018
{
    /*Essa classe aqui é onde o filho chora e a mãe não vê */
    public class GrafoDirigido
    {
        List<Vertices> vertices;
        List<ArestasDirigidas> arestas;

        public List<Vertices> Vertices { get => vertices; set => vertices = value; }
        //contrutor 
        public GrafoDirigido()
        {
            Vertices = new List<Vertices>();
            arestas = new List<ArestasDirigidas>();
        }
        //insere vertice
        internal void inserirVertice(Vertices vertice)
        {
            if (Vertices.FirstOrDefault(x => x.Aeroporto == vertice.Aeroporto) == null)
            {
                Vertices.Add(vertice);
            }
        }
        //insere aresta
        internal void inserirAresta(ArestasDirigidas arestaDirigida)
        {
            arestas.Add(arestaDirigida);
        }
        //Usando Dijkstra repete os comentários de MenorCaminhoTempoTotal
        public void MenorCaminhoDistancia(string origem, string destino)
        {
            Vertices orig = Vertices.Find(x => x.Aeroporto == origem);
            Vertices dest = Vertices.Find(x => x.Aeroporto == destino);

            string[] caminhos = new string[Vertices.Count];
            int[] distancias = new int[Vertices.Count];
            for (int i = 0; i < distancias.Length; i++)
            {
                distancias[i] = int.MaxValue;
            }
            distancias[orig.Id - 1] = 0;

            bool[] foiVisitado = new bool[Vertices.Count];

            foiVisitado[orig.Id - 1] = true;

            Queue<Vertices> fila = new Queue<Vertices>();

            List<ArestasDirigidas> arestas = getArestasQueSaemDeUmVertice(orig);
            foreach (var aresta in arestas)
            {
                if (aresta.Aeroporto1 == orig)
                {
                    distancias[aresta.Aeroporto2.Id - 1] = aresta.Distancia;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
                else
                {
                    distancias[aresta.Aeroporto1.Id - 1] = aresta.Distancia;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
            }

            fila.Enqueue(orig);

            Vertices aux;
            while (fila.Count() > 0)
            {
                aux = fila.Dequeue();
                List<Vertices> listaAdjacencia = this.getListaAdjacencia(aux);
                foreach (var adjacente in listaAdjacencia)
                {
                    if (!foiVisitado[adjacente.Id - 1])
                    {
                        foiVisitado[adjacente.Id - 1] = true;
                        fila.Enqueue(adjacente);
                        List<ArestasDirigidas> listaAdjacenciaVzinhos = getArestasQueSaemDeUmVertice(adjacente);

                        foreach (var voo in listaAdjacenciaVzinhos)
                        {
                            if (voo.Aeroporto1 == adjacente)
                            {
                                if (!foiVisitado[voo.Aeroporto2.Id - 1])
                                {
                                    if (distancias[adjacente.Id - 1] + voo.Distancia < distancias[voo.Aeroporto2.Id - 1])
                                    {
                                        distancias[voo.Aeroporto2.Id - 1] = distancias[adjacente.Id - 1] + voo.Distancia;
                                        caminhos[voo.Aeroporto2.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto2.Aeroporto;
                                    }
                                }
                            }
                            else
                            {
                                if (!foiVisitado[voo.Aeroporto1.Id - 1])
                                {
                                    if (distancias[adjacente.Id - 1] + voo.Distancia < distancias[voo.Aeroporto1.Id - 1])
                                    {
                                        distancias[voo.Aeroporto1.Id - 1] = distancias[adjacente.Id - 1] + voo.Distancia;
                                        caminhos[voo.Aeroporto1.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto1.Aeroporto;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(caminhos[dest.Id - 1]);
        }
        //Djikstra mesma coisa do de cima 
        internal void ComoNaoChegarAtrasadoNaReuniao(string origem, string destino, TimeSpan horario)
        {
            Vertices orig = Vertices.Find(x => x.Aeroporto == origem);
            Vertices dest = Vertices.Find(x => x.Aeroporto == destino);
            
            string[] caminhos = new string[Vertices.Count];
            TimeSpan[] tempo = new TimeSpan[Vertices.Count];
            for (int i = 0; i < tempo.Length; i++)
            {
                tempo[i] = TimeSpan.MaxValue;
            }
            tempo[orig.Id - 1] = new TimeSpan(0, 0, 0);

            bool[] foiVisitado = new bool[Vertices.Count];

            foiVisitado[orig.Id - 1] = true;

            Queue<Vertices> fila = new Queue<Vertices>();

            List<ArestasDirigidas> arestas = getArestasQueSaemDeUmVertice(orig);
            foreach (var aresta in arestas)
            {
                if (aresta.Aeroporto1 == orig)
                {
                    tempo[aresta.Aeroporto2.Id - 1] = aresta.HorariosPartidas[0] + aresta.TempoVoo;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
                else
                {
                    tempo[aresta.Aeroporto1.Id - 1] = aresta.HorariosPartidas[0] + aresta.TempoVoo;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
            }

            fila.Enqueue(orig);

            Vertices aux;
            while (fila.Count() > 0)
            {
                aux = fila.Dequeue();
                List<Vertices> listaAdjacencia = this.getListaAdjacencia(aux);
                foreach (var adjacente in listaAdjacencia)
                {
                    if (!foiVisitado[adjacente.Id - 1])
                    {
                        foiVisitado[adjacente.Id - 1] = true;
                        fila.Enqueue(adjacente);
                        List<ArestasDirigidas> listaAdjacenciaVzinhos = getArestasQueSaemDeUmVertice(adjacente);

                        foreach (var voo in listaAdjacenciaVzinhos)
                        {
                            if (voo.Aeroporto1 == adjacente)
                            {
                                if (!foiVisitado[voo.Aeroporto2.Id - 1])
                                {
                                    if (tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo < tempo[voo.Aeroporto2.Id - 1])
                                    {
                                        tempo[voo.Aeroporto2.Id - 1] = tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo;
                                        caminhos[voo.Aeroporto2.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto2.Aeroporto;
                                    }
                                }
                            }
                            else
                            {
                                if (!foiVisitado[voo.Aeroporto1.Id - 1])
                                {
                                    if (tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo < tempo[voo.Aeroporto1.Id - 1])
                                    {
                                        tempo[voo.Aeroporto1.Id - 1] = tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo;
                                        caminhos[voo.Aeroporto1.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto1.Aeroporto;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string[] escalas = caminhos[dest.Id - 1].Replace(" -> ", ";").Split(';');

            ArestasDirigidas primeiraEscala = getArestasQueSaemDeUmVertice(orig).Find(x => x.Aeroporto1.Aeroporto == escalas[1] || x.Aeroporto2.Aeroporto == escalas[1]);
            TimeSpan UltimoHorarioPossivel = new TimeSpan(0, 0, 0);
            foreach (var horarios in primeiraEscala.HorariosPartidas)
            {
                if (horario >= (tempo[dest.Id - 1] - primeiraEscala.HorariosPartidas[0] + horarios))
                {
                    UltimoHorarioPossivel = horarios;
                }
            }

            Console.WriteLine("O último horário possível para pegar o voo de {0} e não se atrasar é {1}", orig.Aeroporto, UltimoHorarioPossivel.ToString());
        }
        //Djikstra mesma coisa do de cima 
        public void MenorCaminhoTempo(string origem, string destino)
        {
            Vertices orig = Vertices.Find(x => x.Aeroporto == origem);
            Vertices dest = Vertices.Find(x => x.Aeroporto == destino);

            string[] caminhos = new string[Vertices.Count];
            TimeSpan[] tempo = new TimeSpan[Vertices.Count];
            for (int i = 0; i < tempo.Length; i++)
            {
                tempo[i] = TimeSpan.MaxValue;
            }
            tempo[orig.Id - 1] = new TimeSpan(0, 0, 0);

            bool[] foiVisitado = new bool[Vertices.Count];

            foiVisitado[orig.Id - 1] = true;

            Queue<Vertices> fila = new Queue<Vertices>();

            List<ArestasDirigidas> arestas = getArestasQueSaemDeUmVertice(orig);
            foreach (var aresta in arestas)
            {
                if (aresta.Aeroporto1 == orig)
                {
                    tempo[aresta.Aeroporto2.Id - 1] = aresta.TempoVoo;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
                else
                {
                    tempo[aresta.Aeroporto1.Id - 1] = aresta.TempoVoo;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
            }

            fila.Enqueue(orig);

            Vertices aux;
            while (fila.Count() > 0)
            {
                aux = fila.Dequeue();
                List<Vertices> listaAdjacencia = this.getListaAdjacencia(aux);
                foreach (var adjacente in listaAdjacencia)
                {
                    if (!foiVisitado[adjacente.Id - 1])
                    {
                        foiVisitado[adjacente.Id - 1] = true;
                        fila.Enqueue(adjacente);
                        List<ArestasDirigidas> listaAdjacenciaVzinhos = getArestasQueSaemDeUmVertice(adjacente);

                        foreach (var voo in listaAdjacenciaVzinhos)
                        {
                            if (voo.Aeroporto1 == adjacente)
                            {
                                if (!foiVisitado[voo.Aeroporto2.Id - 1])
                                {
                                    if (tempo[adjacente.Id - 1] + voo.TempoVoo < tempo[voo.Aeroporto2.Id - 1])
                                    {
                                        tempo[voo.Aeroporto2.Id - 1] = tempo[adjacente.Id - 1] + voo.TempoVoo;
                                        caminhos[voo.Aeroporto2.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto2.Aeroporto;
                                    }
                                }
                            }
                            else
                            {
                                if (!foiVisitado[voo.Aeroporto1.Id - 1])
                                {
                                    if (tempo[adjacente.Id - 1] + voo.TempoVoo < tempo[voo.Aeroporto1.Id - 1])
                                    {
                                        tempo[voo.Aeroporto1.Id - 1] = tempo[adjacente.Id - 1] + voo.TempoVoo;
                                        caminhos[voo.Aeroporto1.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto1.Aeroporto;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(caminhos[dest.Id - 1]);
        }
        /*Realizado com o método de Djisktra*/
        public void MenorCaminhoTempoTotal(string origem, string destino)
        {
            Vertices orig = Vertices.Find(x => x.Aeroporto == origem);
            Vertices dest = Vertices.Find(x => x.Aeroporto == destino);

            string[] caminhos = new string[Vertices.Count];
            TimeSpan[] tempo = new TimeSpan[Vertices.Count];
            for (int i = 0; i < tempo.Length; i++)
            {
                tempo[i] = TimeSpan.MaxValue;
            }
            tempo[orig.Id - 1] = new TimeSpan(0, 0, 0);
            //o vetor começa false
            bool[] foiVisitado = new bool[Vertices.Count];
            //mas a origem começa com true por ser a primeira visitada
            foiVisitado[orig.Id - 1] = true;
            //cria a fila 
            Queue<Vertices> fila = new Queue<Vertices>();
            /*verifica as arestas que estao saindo de um vertice */
            List<ArestasDirigidas> arestas = getArestasQueSaemDeUmVertice(orig);
            foreach (var aresta in arestas)
            {
                //o calculo do tempo total é realizado com o primeiro horario de saida
                if (aresta.Aeroporto1 == orig)
                {
                    //tempo = primeiro horario(voo cheogu) + tempo de voo
                    tempo[aresta.Aeroporto2.Id - 1] = aresta.HorariosPartidas[0] + aresta.TempoVoo;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
                else
                {
                    tempo[aresta.Aeroporto1.Id - 1] = aresta.HorariosPartidas[0] + aresta.TempoVoo;
                    caminhos[aresta.Aeroporto2.Id - 1] = orig.Aeroporto + " -> " + aresta.Aeroporto2.Aeroporto;
                }
            }
            //enfileirar os vértices
            fila.Enqueue(orig);

            Vertices aux;
            while (fila.Count() > 0)
            {
                //desenfila os vertices 
                aux = fila.Dequeue();
                //pega a lista de adj
                List<Vertices> listaAdjacencia = this.getListaAdjacencia(aux);
                foreach (var adjacente in listaAdjacencia)
                {
                    if (!foiVisitado[adjacente.Id - 1])
                    {
                        foiVisitado[adjacente.Id - 1] = true;
                        fila.Enqueue(adjacente);
                        List<ArestasDirigidas> listaAdjacenciaVzinhos = getArestasQueSaemDeUmVertice(adjacente);

                        foreach (var voo in listaAdjacenciaVzinhos)
                        {
                            if (voo.Aeroporto1 == adjacente)
                            {
                                if (!foiVisitado[voo.Aeroporto2.Id - 1])
                                {
                                    if (tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo < tempo[voo.Aeroporto2.Id - 1])
                                    {
                                        //tempo = recebe o tempo da escala anterior + tempo que demora nessa escala
                                        tempo[voo.Aeroporto2.Id - 1] = tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x  => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo;
                                        caminhos[voo.Aeroporto2.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto2.Aeroporto;
                                    }
                                }
                            }
                            else
                            {
                                if (!foiVisitado[voo.Aeroporto1.Id - 1])
                                {
                                    if (tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo < tempo[voo.Aeroporto1.Id - 1])
                                    {
                                        //tempo = recebe o tempo da escala anterior + tempo que demora nessa escala
                                        tempo[voo.Aeroporto1.Id - 1] = tempo[adjacente.Id - 1] + (voo.HorariosPartidas.FirstOrDefault(x => x > tempo[adjacente.Id - 1]) - tempo[adjacente.Id - 1]) + voo.TempoVoo;
                                        caminhos[voo.Aeroporto1.Id - 1] = caminhos[adjacente.Id - 1] + " -> " + voo.Aeroporto1.Aeroporto;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(caminhos[dest.Id - 1]);
        }
        //getArestas faz o que um método Get faz
        private List<ArestasDirigidas> getArestasQueSaemDeUmVertice(Vertices vertice)
        {
            List<ArestasDirigidas> arestasRetorno = new List<ArestasDirigidas>();
            foreach (var item in arestas.FindAll(x => x.Aeroporto1 == vertice && x.Direcao == 1))
            {
                arestasRetorno.Add(item);
            }

            foreach (var item in arestas.FindAll(x => x.Aeroporto2 == vertice && x.Direcao == -1))
            {
                arestasRetorno.Add(item);
            }

            return arestasRetorno;
        }
        //usando busca em amplitude
        internal void menorCaminhoEmConexoes(string origem, string destino)
        {
            Vertices orig = Vertices.Find(x => x.Aeroporto == origem);
            Vertices dest = Vertices.Find(x => x.Aeroporto == destino);

            int[] antecessores = new int[Vertices.Count];

            for (int i = 0; i < antecessores.Length; i++)
            {
                antecessores[i] = -1;
            }
            bool[] foiVisitado = new bool[Vertices.Count];

            foiVisitado[orig.Id - 1] = true;
            Queue<Vertices> fila = new Queue<Vertices>();

            fila.Enqueue(orig);

            Vertices aux;
            while (fila.Count() > 0)
            {
                aux = fila.Dequeue();
                List<Vertices> listaAdjacencia = this.getListaAdjacencia(aux);
                foreach (var adjacente in listaAdjacencia)
                {
                    if (!foiVisitado[adjacente.Id - 1])
                    {
                        foiVisitado[adjacente.Id - 1] = true;
                        fila.Enqueue(adjacente);
                        antecessores[adjacente.Id - 1] = aux.Id;
                    }
                }
            }

            Stack<int> pilha = new Stack<int>();
            int antecessor = antecessores[dest.Id - 1];
            while (antecessor != -1)
            {
                pilha.Push(antecessor);
                antecessor = antecessores[antecessor - 1];
            }
            //frufru de impressao
            string caminho = string.Format("A menor rota em escalas do aeroporto {0} até o aeroporto {1} é: ", orig.Aeroporto, dest.Aeroporto);

            while (pilha.Count > 0)
            {
                caminho += " -> ";
                int auxInt = pilha.Pop();
                caminho += Vertices.Find(x => x.Id == auxInt).Aeroporto;
            }

            caminho += " -> " + dest.Aeroporto;

            Console.WriteLine(caminho);
        }
        /*Utilizando o Tarjan para validar*/
        public void verificaSeGrafoFortementeConexo()
        {
            //Usando o Tarjan para berificar a situação do grafo
            Dictionary<Vertices, List<Vertices>> Adj = new Dictionary<Vertices, List<Vertices>>();
            foreach (var vertice in Vertices)
            {
                Adj.Add(vertice, getListaAdjacencia(vertice));
            }
            new Tarjan(vertices, Adj).TarjanAlg();
        }
        //precisa mesmo comentar o que esse método faz????
        private void ImprimirGrupoDeAeroportos(List<Vertices> grupo)
        {
            foreach (var aeroporto in grupo)
            {
                Console.Write(aeroporto.Aeroporto + " - ");
            }
            Console.WriteLine();
            Console.WriteLine("----*------------------------*----");
        }
        //verifica se tem ou nao vertices visitados
        private bool ExisteNaoVisitado(bool[] foiVisitado)
        {
            for (int i = 0; i < foiVisitado.Length; i++)
            {
                if (!foiVisitado[i])
                {
                    return true;
                }
            }
            return false;
        }
        //busca aeroporto 
        internal Vertices buscaAeroporto(string nome)
        {
            return Vertices.FirstOrDefault(x => x.Aeroporto == nome);
        }
        //get arestas dirigidas
        private List<Vertices> getListaAdjacencia(Vertices vertice)
        {
            List<Vertices> adjacentes = new List<Vertices>();
            foreach (var item in arestas.FindAll(x => x.Aeroporto1 == vertice && x.Direcao == 1))
            {
                adjacentes.Add(item.Aeroporto2);
            }

            foreach (var item in arestas.FindAll(x => x.Aeroporto2 == vertice && x.Direcao == -1))
            {
                adjacentes.Add(item.Aeroporto1);
            }

            return adjacentes;
        }
    }
}
