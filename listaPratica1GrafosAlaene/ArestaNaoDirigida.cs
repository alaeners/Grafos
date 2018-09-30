using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace listaPratica1GrafosAlaene
{
    /* Aluno: Alaene Rufino de Sousa
     * Matrícula: 609992
     * Atividade: 1a lista prática
     * Entrega via SGA em: 01/10/2018
     * Professora Eveline Alonso
     */
    class ArestaNaoDirigida
    {
        public Vertice v1;
        public Vertice v2;
        public int peso;
        
        /*Contruindo uma aresta*/ 
        public ArestaNaoDirigida(Vertice v1, Vertice v2, int peso)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.peso = peso;
        }
    }
}
