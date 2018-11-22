using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal_Grafos2018
{
    public class Arestas
    {
        Vertices aeroporto1, aeroporto2;
        int distancia;

        public Vertices Aeroporto1 { get => aeroporto1; set => aeroporto1 = value; }
        public Vertices Aeroporto2 { get => aeroporto2; set => aeroporto2 = value; }
        public int Distancia { get => distancia; set => distancia = value; }

        public Arestas(Vertices Aeroporto1, Vertices Aeroporto2, int distancia)
        {
            this.Aeroporto1 = Aeroporto1;
            this.Aeroporto2 = Aeroporto2;
            this.distancia = distancia;
        }
    }
}
