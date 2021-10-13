using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper_4ITB
{
    public partial class Form1 : Form
    {
        Mine[,] mines;
        bool isGenerated = false;

        public Form1() {
            InitializeComponent();
            InitFields(12, 12);
        }

        public void InitFields(int width, int height) {
            mines = new Mine[width, height];
            Mine m = null;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    m = new Mine();
                    panel1.Controls.Add(m);
                    m.Location = new Point(i * m.Width, j * m.Height);
                    m.MineClicked += OnMineClicked;
                    m.X = i;
                    m.Y = j;
                    mines[i, j] = m;
                }
            }
            panel1.Width = m.Width * width;
            panel1.Height = m.Height * height;
            this.Width = panel1.Width + 40;
            this.Height = panel1.Height + 86;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Minesweeper";
        }

        private void OnMineClicked(Mine mine, MouseButtons btn) {
            if (!isGenerated)
                Generate(mine);

            mine.IsRevealed = true;
        }

        private void Generate(Mine mine) {
            int count = 20;
            Random r = new Random();
            List<Tuple<int, int>> generatedMines = new List<Tuple<int, int>>();
            int x, y;
            for(int i = 0; i < count; i++) {
                x = r.Next(mines.GetLength(0));
                y = r.Next(mines.GetLength(1));
                if(x == mine.X && y == mine.Y) {
                    i--;
                    continue;
                }
                if (generatedMines.Exists(tup => tup.Item1 == x && tup.Item2 == y)) {
                    i--;
                    continue;
                }
                generatedMines.Add(Tuple.Create(x, y));

            }
        }
    }
}
