namespace listaPratica1GrafosAlaene
{
    /* Aluno: Alaene Rufino de Sousa
     * Matrícula: 609992
     * Atividade: 1a lista prática
     * Entrega via SGA em: 01/10/2018
     * Professora Eveline Alonso
     */
    class GrafoNaoDirigido
    {

        public bool isAdjacente(Vertice v1, Vertice v2)
        {
            foreach (var item in ListaAresta)
            {
                if (((item.v1.valor == v1) || (item.v1.valor == v1)) && ((item.v2.valor == v2) || (item.v2.valor == v2)))
                {
                    return true;
                }

                else
                    return false;
            }

            return false;
            return true;
        }
        public int getGrau(Vertice v1)
        {
            return 0;
        }
        public bool isIsolado(Vertice v1)
        {
            return false;
        }
        public bool isPendente(Vertice v1)
        {
            return true;
        }
        public bool isRegular()
        {
            return false;
        }

        Grafo getComplementar()
        {
            return null;
        }
    }
}
