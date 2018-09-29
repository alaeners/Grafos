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

        public bool isNulo()
        {
            return true;
        }
        public bool isCompleto()
        {
         //retorna true se o grafo for simples e se o grau de todos os vertices forem iguais a qtde total de vertices -1 */
            if (IsSimple() && VerificaGrauCompl())
            {
                return true;
            }
            else
                return false;
        }
        public bool isConexo()
        {
            // verifica se é possível traçar um caminho entre um qualquer vertice inicial,e um vertice final
            int v_aux = ListaAresta.First().v1.valor;
            int v_aux2 = ListaAresta.First().v2.valor;

            if (ExisteIsolado() > 0) // verifica se a  qtde de vertices isolados é maior que ZERO.
            {
                return false;
            }
            return true;
        }
    }
}