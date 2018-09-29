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
    class GrafoDirigido : Grafo
    {
        public List<ArestaDirigida> ListaAresta;
        public GrafoDirigido(): base()
        {
            this.ListaAresta = new List<ArestaDirigida>();
        }
        public int getGrauEntrada(Vertice v1)
        {
            //retorna a qtde de arestas que incidem num determindado vertice
            int cont = 0;
            foreach (var item in ListaAresta)
            {
                if ((item.v1.valor == v1.valor) && (item.direcao == -1))
                {
                    cont++;
                }
            }
            return cont;
        }
        public int getGrauSaida(Vertice v1)
        {
            //retorna a qtde de arestas que saem de um determindao vertice
            int cont = 0;
            foreach (var item in ListaAresta)
            {
                if ((item.v1.valor == v1.valor) 
                    && (item.direcao == 1))
                {
                    cont++;
                }
            }
            return cont;
        }
    }
}
