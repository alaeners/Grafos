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
        /*Contruindo um grafo e criando uma nova lista de vestices para ele*/ 
        public Grafo()
        {
            Program.cabecalho();

            ListaVertice = new List<Vertice>();
            this.numVertice = 0;
        }


        private bool VerificaGrauCompl()
        {
            /*Este é um metedo que auxilia a verificar se em grafo é completo, retornando true se todos os vertices
             * do grafo tiverem grau igual a quantidade total de vertices -1*/
            int aux = numVertice - 1;
            foreach (var item in ListaVertice)
            {
                if (item.grau != aux)
                {
                    return false;
                }
            }
            return true;
        }


    }
}