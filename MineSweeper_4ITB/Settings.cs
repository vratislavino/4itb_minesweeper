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
    public partial class Settings : Form
    {
        int height = 12;
        public new int Height => height;

        int width = 12;
        public new int Width => width;

        int mines = 5;
        public int Mines => mines;


        public Settings() {
            InitializeComponent();
            numericUpDown1.Value = width;
            numericUpDown2.Value = height;
            numericUpDown3.Value = mines;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            width = (int)numericUpDown1.Value;
            RecalculateMines();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
            height = (int)numericUpDown2.Value;
            RecalculateMines();
        }

        private void RecalculateMines() {
            numericUpDown3.Minimum = 1;
            numericUpDown3.Maximum = (int)(width * height * 0.9);
        }
    }
}
