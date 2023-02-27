using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sliding_Puzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int size = 4;
        Label[,] tiles = new Label[size, size];
        bool[] options = new bool[16];

        private void Form1_Load(object sender, EventArgs e)
        {
            tiles[0, 0] = label1;
            tiles[0, 1] = label2;
            tiles[0, 2] = label3;
            tiles[0, 3] = label4;

            tiles[1, 0] = label5;
            tiles[1, 1] = label6;
            tiles[1, 2] = label7;
            tiles[1, 3] = label8;

            tiles[2, 0] = label9;
            tiles[2, 1] = label10;
            tiles[2, 2] = label11;
            tiles[2, 3] = label12;

            tiles[3, 0] = label13;
            tiles[3, 1] = label14;
            tiles[3, 2] = label15;
            tiles[3, 3] = label16;

            for (int i = 0; i < options.Length; i++)
                options[i] = true;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j && i == size - 1)
                        break;
                    Random rand = new Random();
                    int nr = rand.Next(1, 16);
                    while (options[nr] == false)
                        nr = rand.Next(1, 16);
                    tiles[i, j].Text = Convert.ToString(nr);
                    options[nr] = false;
                    if (nr == 1 || nr == 3 || nr == 6 || nr == 8 || nr == 9 || nr == 11 || nr == 14)
                        tiles[i, j].BackColor = Color.Tomato;
                    else
                        tiles[i, j].BackColor = Color.White;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            int ii = 0, jj = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (label == tiles[i, j])
                    {
                        ii = i; 
                        jj = j;
                    }
                }
            }
            if (ii - 1 >= 0 && tiles[ii - 1, jj].BackColor == Color.Black)
            {
                tiles[ii - 1, jj].BackColor = tiles[ii, jj].BackColor;
                tiles[ii - 1, jj].Text = tiles[ii, jj].Text;
                tiles[ii, jj].BackColor = Color.Black;
                tiles[ii, jj].Text = "";
            }
            else if (ii + 1 < size && tiles[ii + 1, jj].BackColor == Color.Black)
            {
                tiles[ii + 1, jj].BackColor = tiles[ii, jj].BackColor;
                tiles[ii + 1, jj].Text = tiles[ii, jj].Text;
                tiles[ii, jj].BackColor = Color.Black;
                tiles[ii, jj].Text = "";
            }
            else if (jj - 1 >= 0 && tiles[ii, jj - 1].BackColor == Color.Black)
            {
                tiles[ii, jj - 1].BackColor = tiles[ii, jj].BackColor;
                tiles[ii, jj - 1].Text = tiles[ii, jj].Text;
                tiles[ii, jj].BackColor = Color.Black;
                tiles[ii, jj].Text = "";
            }
            else if (jj + 1 < size && tiles[ii, jj + 1].BackColor == Color.Black)
            {
                tiles[ii, jj + 1].BackColor = tiles[ii, jj].BackColor;
                tiles[ii, jj + 1].Text = tiles[ii, jj].Text;
                tiles[ii, jj].BackColor = Color.Black;
                tiles[ii, jj].Text = "";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool end = true;
            for (int i = 0; i < size && end; i++)
            {
                for (int j = 0; j < size && end; j++)
                {
                    if (tiles[i, j].Text != "" && Convert.ToInt32(tiles[i, j].Text) != i * size + j + 1)
                        end = false;
                }
            }
            if (end)
            {
                timer1.Enabled = false;
                MessageBox.Show("You Win!");
                this.Close();
            }
        }
    }
}
