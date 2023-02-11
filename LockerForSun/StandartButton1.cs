using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace LockerForSun {
    public partial class StandartButton : Button {
        public StandartButton(string text) {
            Text = text;
            BackColor = Color.FromArgb(162, 31, 90);
            ForeColor = Color.White;
            BackgroundImageLayout = ImageLayout.Stretch;
            CausesValidation = false;
            FlatStyle = FlatStyle.Flat;
            Margin = new Padding(0);
            UseVisualStyleBackColor = false;
            FlatAppearance.BorderSize = 0;
            this.MouseDown += StandartButton_MouseDown;
            this.MouseUp += StandartButton_MouseUp;
            this.Click += StandartButton_Click;
        }

        private void StandartButton_Click(object sender, EventArgs e) {
            using (var soundPlayer = new SoundPlayer(Properties.Resources.zvuki_derevyannyih_dverey_sborka_31752)) {
                soundPlayer.Play();
            }
        }

        private void StandartButton_MouseUp(object sender, MouseEventArgs e) {
            this.Width += 4;
            this.Height += 4;
            this.Location = new Point(this.Location.X - 2, this.Location.Y - 2);
        }
        private void StandartButton_MouseDown(object sender, MouseEventArgs e) {
            this.Width -= 4;
            this.Height -= 4;
            this.Location = new Point(this.Location.X + 2, this.Location.Y + 2);
        }

    }
}
