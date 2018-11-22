using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal_Grafos2018
{
    public class Vertices
    {
        /*Lembrar que vértice são aeroportos*/
        int id;
        string aeroporto;

        public string Aeroporto { get => aeroporto; set => aeroporto = value; }
        public int Id { get => id; set => id = value; }

        public Vertices(int id, string Aeroporto)
        {
            this.aeroporto = Aeroporto;
            this.id = id;
        }
    }
}
