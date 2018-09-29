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
    class GrafoNaoDirigido : Grafo
    {
        List<ArestaNaoDirigida> ListaAresta;
        public GrafoNaoDirigido()
        {
            ListaAresta = new List<ArestaNaoDirigida>();
            ListaVertice = new List<Vertice>();
            this.numVertice = 0;

        }

        /* Este metodo verifica se há uma conexão entre determinados vertices, percorrendo uma lista de Arestas.*/
        public bool isAdjacente(Vertice v1, Vertice v2)
        {
            foreach (var item in ListaAresta)
            {
                if (((item.v1.valor == v1.valor)
                    || (item.v1.valor == v1.valor))
                    && ((item.v2.valor == v2.valor)
                    || (item.v2.valor == v2.valor)))
                    {
                        return true;
                    }
                    else
                        return false;
            }
            return false;
        }

        /* Este metodo retorna o grau de um determinado vertice, percorrendo uma lista de vertices.*/
        public int getGrau(Vertice v1)
        {
            int cont = 0;
            foreach (var item in ListaAresta)
            {
                if ((item.v1.valor == v1.valor)
                    && (item.v1.valor == item.v2.valor))
                // Garante que se caso haja looping, seja somado apenas +1 grau
                {
                    cont++;
                }
                else if (item.v1.valor == v1.valor)
                // soma 1 grau caso v1 corresponda ao vertice pesquisado
                {
                    cont++;
                }

                else if (item.v2.valor == v1.valor)
                // soma 1 grau caso v2 corresponda ao vertice pesquisado
                {
                    cont++;
                }
            }
            return cont;
        }

        public bool isIsolado(Vertice v1)
        {
            // Este metodo verifica se um determinado vertice tem grau ZERO (Isolado).
            if (getGrau(v1) == 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool isPendente(Vertice v1)
        {
            // Este metodo verifica se determinado vertice tem grau 1 (Pendente).
            if (getGrau(v1) == 1)
            {
                return true;
            }
            else return false;
        }
        public bool isRegular()
        {
            //Este metodo verifica se todos os vertices tem o mesmo grau (Regular)
            int aux = ListaVertice.First().grau;
            foreach (var item in ListaVertice)
            {
                if (item.grau != aux)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isNulo()
        {
            if (ListaVertice == null
                && ListaAresta == null)
            {
                return true;
            }
            else { return false;  }
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

        GrafoNaoDirigido getComplementar()
        {
            GrafoNaoDirigido Complementar = new GrafoNaoDirigido();
            Complementar.ListaVertice = new List<Vertice>();
            Complementar.ListaAresta = new List<ArestaNaoDirigida>();
            foreach (var item in ListaVertice)
            {
                //paca cada vertice eu paasso em todos 
                foreach (var item2 in ListaVertice)
                {
                    if (item != item2 && !isAdjacente(item, item2))
                    {// add vertice
                        ArestaNaoDirigida aux = new ArestaNaoDirigida(item, item2, 0);
                        Complementar.ListaAresta.Add(aux);
                    }
                }
            }
            return Complementar;
        }
        private bool IsSimple()
        {
            // Este metodo verifica se o grafo é simples
            foreach (var item in ListaAresta)
            {
                if (item.v1.valor == item.v2.valor) // Verifica se há Loops no Grafo
                {
                    return false; // se houver loops o grafo não é simples
                }
            }
            return true;
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
        private int ExisteIsolado()
        {
            // Conta Quantos Vertices Isolados há no Grafo
            int cont = 0;
            foreach (var item in ListaVertice)
            {
                if (isIsolado(item))
                {
                    cont++;
                }
            }
            return cont;
        }
    }
}
