using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CriterioDeSturges
{
    public partial class Form1 : Form
    {
        List<double> ListaNumeros = new List<double>();
        public Form1()
        {
            InitializeComponent();
            //double[] range = new double[20] { 75.0, 60.1, 74.6, 68.1, 64.3, 67.2, 75.3, 79.3, 66.4, 86.6, 80.0, 85.0, 72.5, 73.2, 68.9, 71.0, 81.3, 64.2, 73.0, 81.2 };
            //ListaNumeros.AddRange(range);
            //double[] range = new double[30] { 52, 27, 46, 15, 22, 20, 68, 73, 19, 30, 33, 58, 24, 35, 32, 27, 42, 30, 45, 40, 70, 21, 27, 50, 51, 31, 17, 20, 60, 63 };
            //ListaNumeros.AddRange(range);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adicionar();
        }
        private void Adicionar()
        {
            ListaNumeros.Add((double)numericUpDown1.Value);
            numericUpDown1.Value = 0;
            numericUpDown1.Focus();

            label1.Text = $"Numeros adicionados: {ListaNumeros.Count}";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int quantidadeNumeros = ListaNumeros.Count;
            double maiorNumero = ListaNumeros.Max();
            double menorNumero = ListaNumeros.Min();
            double k = 1 + 3.3 * Math.Log10(quantidadeNumeros);

            int h = Convert.ToInt32(Math.Round((maiorNumero - menorNumero) / k));

            //MessageBox.Show(h.ToString());

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

                string[] linha = new string[5] { primeiraCelula, segundaCelula, terceiraCelula, $"{quartaCelula}%", $"{quintaCelula}%"};

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

        private void button3_Click(object sender, EventArgs e)
        {
            ListaNumeros.Clear();
            listView1.Items.Clear();
            label1.Text = "Numeros adicionados: 0";
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                Adicionar();
            }
        }
    }
}
