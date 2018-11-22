using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal_Grafos2018
{
    public class ArestasDirigidas : Arestas
    {
        int direcao;
        TimeSpan tempoVoo;
        List<TimeSpan> horariosPartidas;

        public int Direcao { get => direcao; set => direcao = value; }
        public TimeSpan TempoVoo { get => tempoVoo; set => tempoVoo = value; }
        public List<TimeSpan> HorariosPartidas { get => horariosPartidas; set => horariosPartidas = value; }

        public ArestasDirigidas(Vertices Aeroporto1, Vertices Aeroporto2, int distancia) : base(Aeroporto1, Aeroporto2, distancia)
        {
        }

        public ArestasDirigidas(Vertices Aeroporto1, Vertices Aeroporto2, int direcao, int distancia, TimeSpan tempoVoo, List<TimeSpan> horariosPartidas)
            : base(Aeroporto1, Aeroporto2, distancia)
        {
            this.Direcao = direcao;
            this.Distancia = distancia;
            this.TempoVoo = tempoVoo;
            this.HorariosPartidas = horariosPartidas;
        }

        
    }
}
