using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal_Grafos2018
{
    class Tarjan
    {
        public List<Vertices> V { get; }
        public Dictionary<Vertices, List<Vertices>> Adj { get; }

        public Tarjan(List<Vertices> Vertices, Dictionary<Vertices, List<Vertices>> Adj)
        {
            this.V = Vertices;
            this.Adj = Adj;
        }

        /// <summary>
        /// Tarjan's strongly connected components algorithm
        /// </summary>
        public void TarjanAlg()
        {
            var index = 0; // number of nodes
            var S = new Stack<Vertices>();

            Action<Vertices> StrongConnect = null;
            StrongConnect = (v) =>
            {
                // Set the depth index for v to the smallest unused index
                v.Index = index;
                v.LowLink = index;

                index++;
                S.Push(v);

                // Consider successors of v
                foreach (var w in Adj[v])
                    if (w.Index < 0)
                    {
                        // Successor w has not yet been visited; recurse on it
                        StrongConnect(w);
                        v.LowLink = Math.Min(v.LowLink, w.LowLink);
                    }
                    else if (S.Contains(w))
                        // Successor w is in stack S and hence in the current SCC
                        v.LowLink = Math.Min(v.LowLink, w.Index);

                // If v is a root node, pop the stack and generate an SCC
                if (v.LowLink == v.Index)
                {
                    Console.Write("SCC: ");

                    Vertices w;
                    do
                    {
                        w = S.Pop();
                        Console.Write(w.Aeroporto + " ");
                    } while (w != v);

                    Console.WriteLine();
                }
            };

            foreach (var v in V)
                if (v.Index < 0)
                    StrongConnect(v);
        }
    }
}
