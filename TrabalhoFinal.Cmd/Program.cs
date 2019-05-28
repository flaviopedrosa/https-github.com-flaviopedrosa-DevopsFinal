using System;
using Trabalhofinal.Core;

namespace TrabalhoFinal.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Vertice v0 = new Vertice("V0");
            Vertice v1 = new Vertice("V1");
            Vertice v2 = new Vertice("V2");
            Vertice v3 = new Vertice("V3");
            Vertice v4 = new Vertice("V4");
            Vertice v5 = new Vertice("V5");

            v0.AddIntercessoes(new Intercessao(v0, 0), new Intercessao(v1, 10), new Intercessao(v2, 5));
            v1.AddIntercessoes(new Intercessao(v0, 10), new Intercessao(v2, 3), new Intercessao(v3, 1));
            v2.AddIntercessoes(new Intercessao(v0, 5), new Intercessao(v1, 3), new Intercessao(v3, 8), new Intercessao(v4,2));
            v3.AddIntercessoes(new Intercessao(v1, 1), new Intercessao(v2, 8), new Intercessao(v4, 4), new Intercessao(v5, 4));
            v4.AddIntercessoes(new Intercessao(v2, 2), new Intercessao(v3, 4), new Intercessao(v5, 6));
            v5.AddIntercessoes(new Intercessao(v3, 4), new Intercessao(v4, 6));

            Dijsktra dijsktra = new Dijsktra(v0, v1, v2, v3, v4, v5);
            dijsktra.Processar();



        }
    }
}
