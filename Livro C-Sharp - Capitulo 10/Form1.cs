using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Livro_C_Sharp___Capitulo_10
{
    public partial class Form1 : Form
    {
        int contador = 0;   //index de linha
        int pagina = 1;     //numero de pagina actual
        float pos = 0;      //posição vertical

        public Form1()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Imprimir";
            button2.Text = "Pré-Visualizar";

            listView1.View = View.Details;
            listView1.Columns.Add("Ângulo");
            listView1.Columns.Add("Seno");
            listView1.Columns.Add("Cosseno");
            listView1.Columns.Add("Tangente");

            for (double angulo = 0; angulo < 1.001; angulo += 0.001) {
                ListViewItem linha = new ListViewItem();
                linha.Text = angulo.ToString("0.000");
                ListViewItem.ListViewSubItem seno = new ListViewItem.ListViewSubItem();
                seno.Text = Math.Sin(angulo).ToString("0.00000");
                linha.SubItems.Add(seno);
                ListViewItem.ListViewSubItem cosseno = new ListViewItem.ListViewSubItem();
                cosseno.Text = Math.Cos(angulo).ToString("0.00000");
                linha.SubItems.Add(cosseno);
                ListViewItem.ListViewSubItem tangente = new ListViewItem.ListViewSubItem();
                tangente.Text = Math.Tan(angulo).ToString("0.00000");
                linha.SubItems.Add(tangente);
                listView1.Items.Add(linha);

            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linhas = 23;
            string linha = null;
            float margemEsquerda = 140;
            float margemSuperior = e.MarginBounds.Top;
            Font tipoLetra = new Font("Arial", 15);
            SolidBrush corTitulos = new SolidBrush(Color.White);

            //Preparação de Cabeçalhos
            Rectangle ret = new Rectangle();
            ret.Height = 140;
            ret.Width = 125;
            ret.X = (int)margemEsquerda;
            ret.Y = 60;
            e.Graphics.FillRectangle(Brushes.Gray, ret);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("ÂNGULO", tipoLetra, corTitulos, ret, stringFormat);

            Rectangle ret2 = new Rectangle();
            ret2.Height = 40;
            ret2.Width = 125;
            ret2.X = (int)margemEsquerda + 150;
            ret2.Y = 60;
            e.Graphics.FillRectangle(Brushes.Gray, ret2);
            e.Graphics.DrawString("SENO", tipoLetra, corTitulos, ret2, stringFormat);

            Rectangle ret3 = new Rectangle();
            ret3.Height = 40;
            ret3.Width = 125;
            ret3.X = (int)margemEsquerda + 300;
            ret3.Y = 60;
            e.Graphics.FillRectangle(Brushes.Gray, ret3);
            e.Graphics.DrawString("COSSENO", tipoLetra, corTitulos, ret3, stringFormat);

            Rectangle ret4 = new Rectangle();
            ret4.Height = 40;
            ret4.Width = 125;
            ret4.X = (int)margemEsquerda + 450;
            ret4.Y = 60;
            e.Graphics.FillRectangle(Brushes.Gray, ret4);
            e.Graphics.DrawString("TANGENTE", tipoLetra, corTitulos, ret4, stringFormat);

            //DADOS
            SolidBrush cor = new SolidBrush(Color.Black);
            int idx = 0;
            pos = margemSuperior;
            while (contador < linhas * pagina) {
                pos = margemSuperior + idx * 40;
                if (contador >= listView1.Items.Count) { break; }

                linha = listView1.Items[contador].Text;
                Rectangle retDado1 = new Rectangle();
                retDado1.Height = 40;
                retDado1.Width = 125;
                retDado1.X = (int)margemEsquerda;
                retDado1.Y = (int)pos;
                e.Graphics.FillRectangle(Brushes.White, retDado1);
                e.Graphics.DrawString(linha, tipoLetra, cor, retDado1, stringFormat);

                linha = listView1.Items[contador].SubItems[1].Text;
                Rectangle retDado2 = new Rectangle();
                retDado2.Height = 40;
                retDado2.Width = 125;
                retDado2.X = (int)margemEsquerda + 150;
                retDado2.Y = (int)pos;
                e.Graphics.FillRectangle(Brushes.White, retDado2);
                e.Graphics.DrawString(linha, tipoLetra, cor, retDado2, stringFormat);

                linha = listView1.Items[contador].SubItems[2].Text;
                Rectangle retDado3 = new Rectangle();
                retDado3.Height = 40;
                retDado3.Width = 125;
                retDado3.X = (int)margemEsquerda + 300;
                retDado3.Y = (int)pos;
                e.Graphics.FillRectangle(Brushes.White, retDado3);
                e.Graphics.DrawString(linha, tipoLetra, cor, retDado3, stringFormat);

                linha = listView1.Items[contador].SubItems[3].Text;
                Rectangle retDado4 = new Rectangle();
                retDado4.Height = 40;
                retDado4.Width = 125;
                retDado4.X = (int)margemEsquerda + 450;
                retDado4.Y = (int)pos;
                e.Graphics.FillRectangle(Brushes.White, retDado4);
                e.Graphics.DrawString(linha, tipoLetra, cor, retDado4, stringFormat);

                contador += 1;
                idx += 1;

            }
            if (contador >= listView1.Items.Count)   //há mais linhas para imprimir?
            {
                e.HasMorePages = false;   //Não
            }
            else {
                e.HasMorePages = true;    //Sim
                pagina += 1;
                pos = 0;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK) {
                this.printDocument1.Print();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = this.printDocument1;
            printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
            printPreviewDialog1.ShowDialog();
        }
    }
}
