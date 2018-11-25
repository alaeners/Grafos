using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Alaene Rufino de Sousa - 609992 */
namespace ProjetoFinal_Grafos2018
{
    /*Classe que representará as rotas dirigidas deste grafo */

    public class GrafoNaoDirigido
    {
        /*Foi desenvolvido uma lista de vertices e arestas a partir dos dados lidos do arquivo.
         *Uma varável booleana foi criada para validar a existencia de um ciclo atraves do busca em profundidade
         */
        List<Vertices> vertices;
        List<Arestas> arestas;
        internal bool temCiclo;
        /*Construtor da Classe */
        public GrafoNaoDirigido()
        {
            vertices = new List<Vertices>();
            arestas = new List<Arestas>();
        }
        /*FirstOrDefault é para o primeiro dado que ele encontrar seja o aeroporto e se não achar retornará null */
        public void inserirVertice(Vertices vertice)
        {
            if (vertices.FirstOrDefault(x => x.Aeroporto == vertice.Aeroporto) == null)
            {
                vertices.Add(vertice);
            }
        }
        /*FirstOrDefault é para o primeiro dado que ele encontrar seja o aeroporto e se não achar retornará null */
        internal void inserirRota(Arestas aresta)
        {
            if (arestas.FirstOrDefault(x => (x.Aeroporto1 == aresta.Aeroporto1 && x.Aeroporto2 == aresta.Aeroporto2)
            || (x.Aeroporto1 == aresta.Aeroporto2 && x.Aeroporto2 == aresta.Aeroporto1)) == null)
            {
                arestas.Add(aresta);
            }
        }
        /*De modo geral os métodos foram desenvolvidos com nomes que as suas funções fiquem claras */
        internal void menorCaminhoEmConexoes(string origem, string destino)
        {
            /*procurando o aeroporto da lista para*/
            Vertices orig = vertices.Find(x => x.Aeroporto == origem);
            Vertices dest = vertices.Find(x => x.Aeroporto == destino);

            int[] antecessores = new int[vertices.Count];

            for (int i = 0; i < antecessores.Length; i++)
            {
                antecessores[i] = -1;
            }
            /*enfileirando e desinfileirando os aeroportos de origem */
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
            /*Montar as escalas de aeroporto(string) */
            Stack<int> pilha = new Stack<int>();
            int antecessor = antecessores[dest.Id - 1];
            while (antecessor != -1)
            {
                pilha.Push(antecessor);
                antecessor = antecessores[antecessor - 1];
            }
            /*impressão com frufru*/
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
        /*pegar o próximo da lista de adjacencia que eu uso para um tanto de coisa nesse código e mais em busca em profundidade*/
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
        /*mesma coisa que a de cima só que o 1o item */
        internal Arestas primeiroListaAdj(int u)
        {
            List<Arestas> aux = arestas.FindAll(x => x.Aeroporto1.Aeroporto == vertices[u].Aeroporto || x.Aeroporto2.Aeroporto == vertices[u].Aeroporto);
            return aux.First();
        }
        /*mesma coisa só que para validar se ta vazia ou nao */
        internal bool ListaAdjVazia(int u)
        {
            List<Arestas> aux = arestas.FindAll(x => x.Aeroporto1.Aeroporto == vertices[u].Aeroporto || x.Aeroporto2.Aeroporto == vertices[u].Aeroporto);
            if (aux.Count() > 0)
            {
                return false;
            }

            return true;
        }
        /*Contando o número de vertices existentes no grafo inteiro */
        internal int numVertice()
        {
            return this.vertices.Count();
        }
        /*retorna a lista de adj do vertice */
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
        /*AGM ordenada por distancia */
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
        /*remove aresta né */
        private void retirarRota(Arestas aresta)
        {
            arestas.Remove(aresta);
        }
        /*Chama o busca em profundidade para validar a existencia de ciclos ou nao no grafo */
        private bool ExisteCiclo()
        {
            new BuscaEmProfundidade(this).buscaEmProfundidade();
            return temCiclo;
        }
        /*rotas que estao sendo utilizadas */
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
