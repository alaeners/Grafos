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
        public int grau;
        public int valor;
        public string tipo_grafo;

        public Vertice(int valor, ref ArestaDirecionada grafo)
        {
            this.valor = valor;
        }

        public Vertice(int valor, ref ArestaNaoDirecionada grafo)
        {
            this.valor = valor;
            this.grau = CalculaGrau(ref gr);
        }

        private int CalculaGrau(ref ArestaNaoDirecionada grafo)
        {
            return grafo.getGrau(this.valor) + 1;
        }
    }
}
