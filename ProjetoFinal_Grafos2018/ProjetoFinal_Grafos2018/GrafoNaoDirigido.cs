using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal_Grafos2018
{
    public class GrafoNaoDirigido
    {
        List<Vertices> vertices;
        List<Arestas> arestas;
        internal bool temCiclo;

        public GrafoNaoDirigido()
        {
            vertices = new List<Vertices>();
            arestas = new List<Arestas>();
        }

        public void inserirVertice(Vertices vertice)
        {
            if (vertices.FirstOrDefault(x => x.Aeroporto == vertice.Aeroporto) == null)
            {
                vertices.Add(vertice);
            }
        }

        internal void inserirRota(Arestas aresta)
        {
            if (arestas.FirstOrDefault(x => (x.Aeroporto1 == aresta.Aeroporto1 && x.Aeroporto2 == aresta.Aeroporto2)
            || (x.Aeroporto1 == aresta.Aeroporto2 && x.Aeroporto2 == aresta.Aeroporto1)) == null)
            {
                arestas.Add(aresta);
            }
        }

        internal void menorCaminhoEmConexoes(string origem, string destino)
        {
            Vertices orig = vertices.Find(x => x.Aeroporto == origem);
            Vertices dest = vertices.Find(x => x.Aeroporto == destino);

            int[] antecessores = new int[vertices.Count];

            for (int i = 0; i < antecessores.Length; i++)
            {
                antecessores[i] = -1;
            }

            Queue<Vertices> fila = new Queue<Vertices>();

            fila.Enqueue(orig);

            Vertices aux;
            while (fila.Count() > 0)
            {
                aux = fila.Dequeue();
                List<Vertices> listaAdjacencia = this.getListaAdjacencia(aux);
                foreach (var adjacente in listaAdjacencia)
                {
                    if (antecessores[adjacente.Id - 1] == -1 && adjacente.Id != orig.Id)
                    {
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

            string caminho = string.Format("A menor rota em escalas do aeroporto {0} até o aeroporto {1} é: ", orig.Aeroporto, dest.Aeroporto);

            while (pilha.Count > 0)
            {
                caminho += " -> ";
                int auxInt = pilha.Pop();
                caminho += vertices.Find(x => x.Id == auxInt).Aeroporto;
            }

            caminho += " -> " + dest.Aeroporto;

            Console.WriteLine(caminho);
        }

        internal Arestas proxAdj(int u, Arestas a)
        {
            try
            {
                List<Arestas> aux = arestas.FindAll(x => x.Aeroporto1.Aeroporto == vertices[u].Aeroporto || x.Aeroporto2.Aeroporto == vertices[u].Aeroporto);
                return aux[aux.IndexOf(a) + 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }

        }

        internal Arestas primeiroListaAdj(int u)
        {
            List<Arestas> aux = arestas.FindAll(x => x.Aeroporto1.Aeroporto == vertices[u].Aeroporto || x.Aeroporto2.Aeroporto == vertices[u].Aeroporto);
            return aux.First();
        }

        internal bool ListaAdjVazia(int u)
        {
            List<Arestas> aux = arestas.FindAll(x => x.Aeroporto1.Aeroporto == vertices[u].Aeroporto || x.Aeroporto2.Aeroporto == vertices[u].Aeroporto);
            if (aux.Count() > 0)
            {
                return false;
            }

            return true;
        }

        internal int numVertice()
        {
            return this.vertices.Count();
        }

        private List<Vertices> getListaAdjacencia(Vertices vertice)
        {
            List<Vertices> adjacentes = new List<Vertices>();
            foreach (var item in arestas.FindAll(x => x.Aeroporto1 == vertice))
            {
                adjacentes.Add(item.Aeroporto2);
            }

            foreach (var item in arestas.FindAll(x => x.Aeroporto2 == vertice))
            {
                adjacentes.Add(item.Aeroporto1);
            }

            return adjacentes;
        }

        private GrafoNaoDirigido ArvoreGeradoraMinima()
        {
            List<Arestas> aux = arestas.OrderBy(x => x.Distancia).ToList();
            GrafoNaoDirigido AGM = new GrafoNaoDirigido();

            foreach (var vertice in vertices)
            {
                AGM.inserirVertice(vertice);
            }

            int incluidas = 0;
            int cont = 0;

            foreach (var aresta in arestas)
            {
                AGM.inserirRota(aresta);

                if (AGM.ExisteCiclo())
                {
                    AGM.retirarRota(aresta);
                    AGM.temCiclo = false;
                }
                else
                {
                    incluidas++;

                    if (incluidas >= vertices.Count() - 1)
                    {
                        break;
                    }
                }
            }

            return AGM;
        }

        private void retirarRota(Arestas aresta)
        {
            arestas.Remove(aresta);
        }

        private bool ExisteCiclo()
        {
            new BuscaEmProfundidade(this).buscaEmProfundidade();
            return temCiclo;
        }

        public void RotasEfetivasUsadas()
        {
            GrafoNaoDirigido AGM = this.ArvoreGeradoraMinima();

            Console.WriteLine("Rotas Efetivamente Usadas");
            Console.WriteLine();
            foreach (var rotas in AGM.arestas)
            {
                Console.WriteLine("{0} -> {1}", rotas.Aeroporto1.Aeroporto, rotas.Aeroporto2.Aeroporto);
            }
        }
    }
}
