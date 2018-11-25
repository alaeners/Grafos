using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Alaene Rufino de Sousa - 609992 */
namespace ProjetoFinal_Grafos2018
{
    public class Vertices
    {
        /*Lembrar que vértice são aeroportos*/
        int id;
        string aeroporto;
        int lowLink;
        int index;

        public string Aeroporto { get => aeroporto; set => aeroporto = value; }

        public int Id { get => id; set => id = value; }

        public int LowLink { get => lowLink; set => lowLink = value; }

        public int Index { get => index; set => index = value; }

        public Vertices(int id, string Aeroporto)
        {
            this.aeroporto = Aeroporto;
            this.id = id;
            this.index = -1;
            this.lowLink = 0;
        }
    }
}
