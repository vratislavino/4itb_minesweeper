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
    public partial class Mine : UserControl {

        public event Action<Mine, MouseButtons> MineClicked;

        private static List<Color> colors = new List<Color>() {
            Color.Transparent,
            Color.Blue,
            Color.Green,
            Color.Red,
            Color.Purple,
            Color.Maroon,
            Color.Turquoise,
            Color.Black,
            Color.Gray
        };

        public int X { get; set; }
        public int Y { get; set; }

        private int value;
        private bool isRevealed;

        public bool IsMine {
            get { return value == -1; }
        }

        public bool IsRevealed {
            get { return isRevealed; }
            set {
                isRevealed = value;
                this.BackColor = isRevealed ? SystemColors.ControlDark : SystemColors.Control;
                DisplayNumber();
            }
        }

        public Mine() {
            InitializeComponent();
            IsRevealed = false;
        }

        public void AssignValue() {

        }

        private void DisplayNumber() {
            if (!IsRevealed)
                return;
            label1.Text = value.ToString();
            label1.ForeColor = colors[value];
        }

        private void label1_MouseClick(object sender, MouseEventArgs e) {
            MineClicked?.Invoke(this, e.Button);
        }
    }
}
