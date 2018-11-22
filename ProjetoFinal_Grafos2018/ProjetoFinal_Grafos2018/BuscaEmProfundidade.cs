using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal_Grafos2018
{
    public class BuscaEmProfundidade
    {
        public const byte branco = 0;
        public const byte cinza = 1;
        public const byte preto = 2;

        private int[] d;
        private int[] t;
        private int[] antecessor;
        private GrafoNaoDirigido grafo;

        public BuscaEmProfundidade(GrafoNaoDirigido grafo)
        {
            this.grafo = grafo;
            int n = this.grafo.numVertice();

            d = new int[n];
            t = new int[n];
            antecessor = new int[n];
        }

        public int tempoDeDescoberta(int v) { return this.d[v]; }
        public int tempoDeTermino(int v) { return this.t[v]; }
        public int verticeAntecessor(int v) { return this.antecessor[v]; }

        public void imprimeCaminho(int origem, int v)
        {
            if (origem == v)
                Console.WriteLine(origem);
            else if (this.antecessor[v] == -1)
                Console.WriteLine("Nao existe caminho de " + origem + " ate " + v);
            else
            {
                imprimeCaminho(origem, this.antecessor[v]);
                Console.WriteLine(v);
            }
        }

        public void buscaEmProfundidade()
        {
            int tempo = 0; int[] cor = new int[this.grafo.numVertice()];

            for (int u = 0; u < grafo.numVertice(); u++)
            {
                cor[u] = branco; this.antecessor[u] = -1;
            }

            for (int u = 0; u < grafo.numVertice(); u++)
                if (cor[u] == branco)
                    tempo = this.visitaDfs(u, tempo, cor);
        }

        private int visitaDfs(int u, int tempo, int[] cor)
        {
            cor[u] = cinza; this.d[u] = ++tempo;

            if (!this.grafo.ListaAdjVazia(u))
            {
                Arestas a = this.grafo.primeiroListaAdj(u);

                while (a != null)
                {
                    int v = a.Aeroporto2.Id != u + 1? a.Aeroporto2.Id : a.Aeroporto1.Id;
                    v--;
                    if (cor[v] == branco)
                    {
                        this.antecessor[v] = u;
                        tempo = this.visitaDfs(v, tempo, cor);
                    }
                    else if (cor[v] == cinza && v != antecessor[u])
                    {
                        this.grafo.temCiclo = true;
                    }
                    a = this.grafo.proxAdj(u, a);
                }
            }
            cor[u] = preto; this.t[u] = ++tempo;
            return tempo;
        }


    }
}
