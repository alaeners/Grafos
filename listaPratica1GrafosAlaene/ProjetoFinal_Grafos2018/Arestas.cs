using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Alaene Rufino de Sousa - 609992 */
namespace ProjetoFinal_Grafos2018
{
   /*
    * A classe arestas ficou responsável por ser o caminho de um aeroporto a outro.
    * Arestas são as rotas existentes entre dois objetos.
    */
    public class Arestas
    {
        /*
         * Declaração de variáveis, onde os vértices são representados pelos aeroportos em questão
         */
        Vertices aeroporto1, aeroporto2;
        int distancia;

        /*
         * Getters e Setter da classe
         */
        public Vertices Aeroporto1 { get => aeroporto1; set => aeroporto1 = value; }
        public Vertices Aeroporto2 { get => aeroporto2; set => aeroporto2 = value; }
        public int Distancia { get => distancia; set => distancia = value; }

        /*
         * Construtor da Classe 
         */
        public Arestas(Vertices Aeroporto1, Vertices Aeroporto2, int distancia)
        {
            this.Aeroporto1 = Aeroporto1;
            this.Aeroporto2 = Aeroporto2;
            this.distancia = distancia;
        }
    }
}
