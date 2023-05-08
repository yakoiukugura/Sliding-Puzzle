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

        // Dimensiunea puzzle-ului (4x4)
        const int SIZE = 4;

        // Matricea de etichete care reprezinta patratele puzzle-ului
        Label[,] tiles;

        // Vector care tine evidenta patratelor care pot fi mutate
        bool[] options = new bool[SIZE * SIZE];

        // Evenimentul pentru butonul de start
        private void start_button_Click(object sender, EventArgs e)
        {
            // Dezactiveaza si ascunde elementele din meniu si afiseaza puzzle-ul
            menu.Enabled = false;
            menu.Visible = false;

            title.Enabled = false;
            title.Visible = false;

            start_button.Enabled = false;
            start_button.Visible = false;

            quit_button.Enabled = false;
            quit_button.Visible = false;

            // Creeaza matricea de etichete reprezentand patratele puzzle-ului
            tiles = new Label[,]{
                { label1, label2, label3, label4},
                { label5, label6, label7, label8},
                { label9, label10, label11, label12},
                { label13, label14, label15, label16}
            };

            // Amesteca puzzle-ul
            shuffle();

            // Porneste cronometrul
            timer1.Enabled = true;
        }

        // Evenimentul pentru butonul de iesire
        private void quit_button_Click(object sender, EventArgs e)
        {
            // Afiseaza o fereastra de dialog pentru a confirma iesirea din aplicatie
            if (MessageBox.Show("Are you sure you want to quit?", "Quit Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        // Evenimentul pentru butonul de anulare
        private void cancel_button_Click(object sender, EventArgs e)
        {
            // Reactiveaza si afiseaza elementele din meniu si ascunde puzzle-ul
            menu.Enabled = true;
            menu.Visible = true;

            title.Text = "Sliding Puzzle";
            title.Enabled = true;
            title.Visible = true;

            start_button.Text = "Start";
            start_button.Enabled = true;
            start_button.Visible = true;

            quit_button.Enabled = true;
            quit_button.Visible = true;
        }

        // Evenimentul pentru butonul de amestecare
        private void shuffle_button_Click(object sender, EventArgs e)
        {
            // Amesteca puzzle-ul
            shuffle();
        }

        // Functia pentru amestecarea puzzle-ului
        private void shuffle()
        {
            // Resetarea vectorului de optiuni
            for (int i = 0; i < options.Length; i++)
                options[i] = true;

            // Parcurgerea fiecarui patratel
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    // Pe ultimul patratel il facem negru
                    if (i == j && i == SIZE - 1)
                    {
                        tiles[i, j].Text = "";
                        tiles[i, j].BackColor = Color.Black;
                        break;
                    }

                    // Alegem un numar aleatoriu pentru fiecare patratel
                    Random rand = new Random();
                    int nr = rand.Next(1, 16);
                    while (options[nr] == false)
                        nr = rand.Next(1, 16);
                    tiles[i, j].Text = Convert.ToString(nr);
                    options[nr] = false;

                    // Coloram patratelul in functie de numar
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

            //Cautam labelul selectat in matricea tiles
            int i = 0, j = 0;
            for (i = 0; i < SIZE; i++)
            {
                for (j = 0; j < SIZE; j++)
                    if (label == tiles[i, j])
                        break;
                if (j < SIZE && label == tiles[i, j])
                    break;
            }

            /*  După ce se găsește poziția etichetei selectate, verificăm dacă eticheta de deasupra, dedesubtul, stânga sau dreapta 
            etichetei selectate are culoarea neagră (adică este gol). Dacă găsim o astfel de etichetă, schimbăm culoarea și textul 
            acesteia cu cel al etichetei selectate și apoi setăm culoarea și textul etichetei selectate la negru și, respectiv, la zero. 
            Acest lucru este echivalent cu mutarea unei piese în jocul puzzle. */

            if (i - 1 >= 0 && tiles[i - 1, j].BackColor == Color.Black)
            {
                tiles[i - 1, j].BackColor = tiles[i, j].BackColor;
                tiles[i - 1, j].Text = tiles[i, j].Text;
                tiles[i, j].BackColor = Color.Black;
                tiles[i, j].Text = "";
            }
            else if (i + 1 < SIZE && tiles[i + 1, j].BackColor == Color.Black)
            {
                tiles[i + 1, j].BackColor = tiles[i, j].BackColor;
                tiles[i + 1, j].Text = tiles[i, j].Text;
                tiles[i, j].BackColor = Color.Black;
                tiles[i, j].Text = "";
            }
            else if (j - 1 >= 0 && tiles[i, j - 1].BackColor == Color.Black)
            {
                tiles[i, j - 1].BackColor = tiles[i, j].BackColor;
                tiles[i, j - 1].Text = tiles[i, j].Text;
                tiles[i, j].BackColor = Color.Black;
                tiles[i, j].Text = "";
            }
            else if (j + 1 < SIZE && tiles[i, j + 1].BackColor == Color.Black)
            {
                tiles[i, j + 1].BackColor = tiles[i, j].BackColor;
                tiles[i, j + 1].Text = tiles[i, j].Text;
                tiles[i, j].BackColor = Color.Black;
                tiles[i, j].Text = "";
            }
        }

        /*  Această funcție este apelată în fiecare secundă și verifică dacă jocul a ajuns la sfârșitul său. Verificarea se face prin 
        parcurgerea matricei de căsuțe a jocului și compararea numărului din fiecare căsuță cu numărul așteptat pentru acea poziție. 
        Dacă toate numerele sunt în ordinea corectă, atunci jocul se încheie prin apelarea funcției gameOver(). */
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool end = true;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (tiles[i, j].Text != "" && Convert.ToInt32(tiles[i, j].Text) != i * SIZE + j + 1)
                        end = false;
                }
            }
            if (end == true)
            {
                gameOver();
            }
        }

        void gameOver()
        {
            // Reactiveaza si afiseaza elementele din meniu si ascunde puzzle-ul
            menu.Enabled = true;
            menu.Visible = true;

            title.Text = "You Win!";
            title.Enabled = true;
            title.Visible = true;

            start_button.Text = "Play Again";
            start_button.Enabled = true;
            start_button.Visible = true;

            quit_button.Enabled = true;
            quit_button.Visible = true;
        }
    }
}
