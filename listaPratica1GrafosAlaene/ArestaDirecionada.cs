using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace listaPratica1GrafosAlaene
{
    class ArestaDirecionada : Aresta
    {
        public int direcao;

        public ArestaDirecionada(Vertice v1, Vertice v2, int peso, int direcao) : base(v1, v2, peso)
        {
            this.direcao = direcao;
        }

    }
}
