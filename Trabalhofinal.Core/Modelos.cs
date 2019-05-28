using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trabalhofinal.Core
{
    public class Vertice
    {
        #region propriedades
        public bool IsAberto { get; private set; }
        public string Nome { get; private set; }
        public List<Intercessao> Intercessoes { get; private set; }
        #endregion

        #region contrutores
        public Vertice(string nome)
        {
            IsAberto = true;
            Nome = nome;
            Intercessoes = new List<Intercessao>();
        }

        public Vertice(bool isAberto, string nome)
        {
            IsAberto = isAberto;
            Nome = nome;
            Intercessoes = new List<Intercessao>();
        }
        #endregion

        #region Funionalidades
        public void AddIntercessoes(params Intercessao[] intercessoes)
        {
            Intercessoes.AddRange(intercessoes);
        }

        public List<Intercessao> GetIntercessoesAbertas()
        {
            return Intercessoes.Where(x => x.Vertice.IsAberto).ToList();
        }

        public void Fechar()
        {
            IsAberto = false;
        }

        public override string ToString()
        {
            return Nome;
        }

        #endregion

        #region operadores
        public static bool operator ==(Vertice op1, Vertice op2)
        {
            return op1.Nome.Equals(op2.Nome);
        }

        public static bool operator !=(Vertice op1, Vertice op2)
        {
            if (op1 is null && op2 is null) return false;
            if (op1 is null && !(op2 is null)) return true;
            if (!(op1 is null) && (op2 is null)) return true;
            return !op1.Nome.Equals(op2.Nome);
        }
        #endregion
    }

    public class Intercessao
    {
        #region propriedades
        public int Distancia { get; private set; }
        public Vertice Vertice { get; private set; }
        #endregion

        #region contrutores
        public Intercessao(Vertice vertice, int distancia)
        {
            Distancia = distancia;
            Vertice = vertice;
        }
        #endregion

        #region operadores
        public static bool operator <(Intercessao op1, Intercessao op2)
        {
            return op1.Distancia < op2.Distancia;
        }
        public static bool operator >(Intercessao op1, Intercessao op2)
        {
            return op1.Distancia > op2.Distancia;
        }
        #endregion
    }

    public class Dijsktra
    {
        public Dijsktra(params Vertice[] vertices)
        {
            Vertices = new Dictionary<Vertice, int>();
            
            foreach (Vertice v in vertices)
            {
                Vertices.Add(v, int.MaxValue /2);
            }
            Solucao = new Dictionary<Vertice, int>();
            root = vertices.First();
            root.Fechar();
            Solucao.Add(root, 0);
        }

        public Dictionary<Vertice, int> Vertices { get; private set; }
        private Dictionary<Vertice, int> Solucao { get; set; }
        private Vertice root { get; set; }
        private void ProcurarProximoVertice(params Intercessao[] intercessoes)
        {
            Vertice verticeMenorDistancia = null;
            int menorDistancia = -1;
            int distanciaAtual = Solucao.Sum(x => x.Value);
            foreach (Intercessao intercessao in intercessoes.Where(x=>x.Vertice.IsAberto).ToList())
            {
                KeyValuePair<Vertice, int> op = Vertices.FirstOrDefault(x => x.Key == intercessao.Vertice && x.Key.IsAberto);
                if (op.Value + distanciaAtual > intercessao.Distancia + distanciaAtual)
                {
                    verticeMenorDistancia = intercessao.Vertice;
                    menorDistancia = intercessao.Distancia + distanciaAtual;
                }
            }
            if (verticeMenorDistancia != null)
            {
                verticeMenorDistancia.Fechar();
                root = verticeMenorDistancia;
                Solucao.Add(verticeMenorDistancia, menorDistancia);
            }
        }

        public void Processar()
        {
            while (HasVerticesAbertos())
            {
                ProcurarProximoVertice(root.Intercessoes.ToArray());
            }
        }

        private bool HasVerticesAbertos()
        {
            return Vertices.Keys.ToList().Exists(x => x.IsAberto);
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            foreach (KeyValuePair<Vertice, int> item in Vertices)
            {
                buffer.Append(item.Key);
                buffer.Append(": ");
                buffer.Append(item.Value);
                buffer.Append(" / ");

            }
            return buffer.ToString();
        }
    }
}
