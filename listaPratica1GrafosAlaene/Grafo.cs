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
    class Grafo
    {

        public int numVertice;
        public List<Vertice> ListaVertice;

        public Grafo()
        {
            Program.cabecalho();

            ListaVertice = new List<Vertice>();
            this.numVertice = 0;
        }

        public bool isNulo()
        {
            return true;
        }
        public bool isCompleto()
        {
            return false;
        }
        public bool isConexo()
        {
            return true;
        }

    }
}
