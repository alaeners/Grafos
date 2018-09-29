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

    class Vertice
    {
        /*Declarando as variaveis para acesso nos métodos e verificar os grafos*/
        public int grau;
        public int valor;
        public string tipo_grafo;

        /*Contrutor da classe para criar vertices com  X valores*/
        public Vertice(int valor, ref GrafoDirigido grafo)
        {
            this.valor = valor;
        }

        public Vertice(int valor, ref GrafoNaoDirigido grafo)
        {
            this.valor = valor;
            this.grau = CalculaGrau(ref grafo);
        }

        /*Verifica o grau dos vértices*/
        private int CalculaGrau(ref GrafoNaoDirigido grafo)
        {
            return grafo.getGrau(this.valor) + 1;
        }
    }
}
