using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriterioDeSturges
{
    public partial class FormTabela : Form
    {
        List<double> ListaNumeros;
        public FormTabela(List<double> ListaNumeros)
        {
            InitializeComponent();
            this.ListaNumeros = ListaNumeros;
            CalculareGerarTabela();
        }

        private void CalculareGerarTabela()
        {
            int quantidadeNumeros = ListaNumeros.Count;
            double maiorNumero = ListaNumeros.Max();
            double menorNumero = ListaNumeros.Min();
            double k = 1 + 3.3 * Math.Log10(quantidadeNumeros);
            int h = Convert.ToInt32(Math.Round((maiorNumero - menorNumero) / k));
            int novoMenornumero = Convert.ToInt32(menorNumero);
            int somaFs = 0;
            double somaPorcentagens = 0;

            for (; novoMenornumero <= maiorNumero; novoMenornumero += h)
            {
                string primeiraCelula = $"{novoMenornumero} a {novoMenornumero + h}";
                int f = 0;
                foreach (double numero in ListaNumeros)
                {
                    if (numero >= novoMenornumero && numero < novoMenornumero + h)
                    {
                        f++;
                    }
                }
                somaFs += f;
                somaPorcentagens += CalcularPorcentagem(f);
                string segundaCelula = f.ToString();
                string terceiraCelula = somaFs.ToString();
                string quartaCelula = CalcularPorcentagem(f).ToString();
                string quintaCelula = somaPorcentagens.ToString();

                string[] linha = new string[5] { primeiraCelula, segundaCelula, terceiraCelula, $"{quartaCelula}%", $"{quintaCelula}%" };

                listView1.Items.Add(new ListViewItem(linha));
            }
            string[] total = new string[5] { "Total", somaFs.ToString(), "", $"{somaPorcentagens}%", "" };
            listView1.Items.Add(new ListViewItem(total));
        }

        private double CalcularPorcentagem(int f)
        {
            double x = (100.0 * f) / ListaNumeros.Count;
            x = Math.Round(x);
            return x;
        }
    }
}
