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

        public event Action<Mine> OffenbarenAngefordert;

        public event Action MineExploded;

        private static List<Color> colors = new List<Color>() {
            SystemColors.ControlDark,
            //Color.Black,
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

        private bool hasFlag = false;
        public bool HasFlag {
            get { return hasFlag; }
            set {
                if (hasFlag && value == !hasFlag)
                    label1.Text = "";

                hasFlag = value;
                DisplayNumber();
            }
        }

        public bool IsMine {
            get { return value == -1; }
        }

        public bool IsRevealed {
            get { return isRevealed; }
            set {
                isRevealed = value;
                if (hasFlag)
                    hasFlag = false;
                this.BackColor = isRevealed ? SystemColors.ControlDark : SystemColors.Control;
                DisplayNumber();
            }
        }

        public Mine() {
            InitializeComponent();
            IsRevealed = false;
        }

        public void AssignValue(int value) {
            this.value = value;
        }

        public void IncreaseValue() {
            this.value++;
        }

        private void DisplayNumber() {
            if (hasFlag) {
                label1.Text = "🚩";
                return;
            }

            if (!IsRevealed)
                return;
            
            if(IsMine) {
                label1.BackColor = Color.Red;
                label1.Text = "☻";
                MineExploded?.Invoke();
                return;
            }

            label1.Text = value.ToString();
            label1.ForeColor = colors[value];

            if(value == 0) {
                OffenbarenAngefordert?.Invoke(this);
            }
        }

        public void PeekMine() {
            this.BackColor = SystemColors.ControlDark;
            label1.Text = "☻";
            isRevealed = true;
        }

        private void label1_MouseClick(object sender, MouseEventArgs e) {
            MineClicked?.Invoke(this, e.Button);
        }
    }
}
