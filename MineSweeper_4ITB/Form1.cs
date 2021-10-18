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
    //https://github.com/vratislavino/4itb_minesweeper.git

    public partial class Form1 : Form
    {
        Mine[,] mines;
        bool isGenerated = false;
        int count = 5;
        bool lost = false;

        int cachedWidth=12;
        int cachedHeight=12;
        int cachedMines=5;

        public Form1() {
            InitializeComponent();
            StartNewGame(cachedWidth, cachedHeight, cachedMines);
        }

        private void StartNewGame(int width, int height, int mines) {
            panel1.Controls.Clear();
            
            count = mines;
            InitFields(width, height);
            lost = false;
            isGenerated = false;

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
                    m.OffenbarenAngefordert += AnOffenbarenAngefordert;
                    m.MineExploded += OnMineExploded;
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

        private void OnMineExploded() {
            foreach (var m in mines) {
                if (m.IsMine) {
                    m.PeekMine();
                }
            }
            lost = true;
            Lost();
        }

        private void AnOffenbarenAngefordert(Mine mine) {
            for (int j = mine.X - 1; j <= mine.X + 1; j++) {
                for (int k = mine.Y - 1; k <= mine.Y + 1; k++) {
                    if (j >= 0 && j < mines.GetLength(0) && k >= 0 && k < mines.GetLength(1))
                        if (!mines[j, k].IsRevealed) {
                            mines[j, k].IsRevealed = true;
                        }
                }
            }
        }

        private void CheckForWin() {
            if (lost)
                return;
            int notRevealed = 0;
            foreach (var mine in mines) {
                if (!mine.IsRevealed) {
                    notRevealed++;
                }
            }
            if (notRevealed == count) {
                Win();
            }
        }

        private void Win() {
            foreach (var mine in mines) {
                if (!mine.IsRevealed) {
                    mine.HasFlag = true;
                }
            }
            var dr = MessageBox.Show("Gratz, vyhrál jsi! Chceš spustit novou hru?", "Konec hry!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes) {
                StartNewGame(cachedWidth, cachedHeight, cachedMines);
            } else {
                Application.Exit();
            }
        }

        private void Lost() {
            var dr = MessageBox.Show("Prohrál jsi! Chceš spustit novou hru?", "Konec hry!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes) {
                StartNewGame(cachedWidth, cachedHeight, cachedMines);
            } else {
                Application.Exit();
            }
        }

        private void OnMineClicked(Mine mine, MouseButtons btn) {

            if (btn == MouseButtons.Right) {
                if (mine.IsRevealed)
                    return;
                mine.HasFlag = !mine.HasFlag;
                return;
            }

            if (btn == MouseButtons.Left) {
                if (!mine.HasFlag) {
                    if (!isGenerated) {
                        Generate(mine);
                        isGenerated = true;
                    }

                    mine.IsRevealed = true;
                }
            }

            CheckForWin();
        }

        private void Generate(Mine mine) {
            Random r = new Random();
            List<Tuple<int, int>> generatedMines = new List<Tuple<int, int>>();
            int x, y;
            for (int i = 0; i < count; i++) {
                x = r.Next(mines.GetLength(0));
                y = r.Next(mines.GetLength(1));
                if (x == mine.X && y == mine.Y) {
                    i--;
                    continue;
                }
                if (generatedMines.Exists(tup => tup.Item1 == x && tup.Item2 == y)) {
                    i--;
                    continue;
                }
                generatedMines.Add(Tuple.Create(x, y));
            }

            // generated!
            for (int i = 0; i < count; i++) {
                x = generatedMines[i].Item1;
                y = generatedMines[i].Item2;
                mines[x, y].AssignValue(-1);
                for (int j = x - 1; j <= x + 1; j++) {
                    for (int k = y - 1; k <= y + 1; k++) {
                        if (j >= 0 && j < mines.GetLength(0) && k >= 0 && k < mines.GetLength(1))
                            if (!mines[j, k].IsMine) {
                                mines[j, k].IncreaseValue();
                            }
                    }
                }
            }
        }

        private void konecHryToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void nastaveníToolStripMenuItem_Click(object sender, EventArgs e) {
            Settings s = new Settings();
            
            var res = s.ShowDialog();
            if(res == DialogResult.OK) {
                cachedWidth = s.Width;
                cachedHeight = s.Height;
                cachedMines = s.Mines;
                StartNewGame(s.Width, s.Height, s.Mines);
            }
        }
    }
}
