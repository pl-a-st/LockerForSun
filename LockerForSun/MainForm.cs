using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Windows;
using System.Media;
using System.Threading;
using System.IO;
using System.Reflection;
using LockerForSun.Properties;



namespace LockerForSun {
    public partial class MainForm : Form {
        private int CountOfAnswers = 0;
        private decimal CountSet = 0;
        private CurrentTask CurrentTask = CurrentTask.Read;
        public MainForm() {
            InitializeComponent();
            this.Activated += MainForm_Activated;
        }

        private void MainForm_Activated(object sender, EventArgs e) {
            textBox1.Focus();
        }
        public TextBox GetTextBox1() {
            return textBox1;
        }
        public Button GetButton1() {
            return button1;
        }
        public Label GetLabel1() {
            return label1;
        }
        private void Form1_Load(object sender, EventArgs e) {


            LoadAsync();
            async Task LoadAsync() {
                await NewImage();
                await PlayStart();
                await MakeTaskAsync();

            }
            async Task PlayStart() {
                using (var soundPlayer = new SoundPlayer(Properties.Resources.Тихо_все)) {
                    soundPlayer.Play();
                }
            }
            async Task NewImage() {
                this.BackgroundImage = Properties.Resources.MainScreen1;
            }
            async Task MakeTaskAsync() {
                await Task.Delay(3500);
                MakeTask();
            }

        }
        private void MakeTask() {
            this.BackgroundImage = null;
            if (CurrentTask == CurrentTask.Read) {
                Employer.GetTaskRead(this, picturesCount: 6, taskCount: 5);
                return;
            }
            foreach (Control control in this.Controls) {
                control.Visible = true;
            }
            if (CountOfAnswers % 2 == 0) {
                Calcs.Sum((Numbers)CountSet);
                label1.Text = "" + Calcs.FirstValue + " + " + Calcs.SecondValue + " =";
            }
            if (CountOfAnswers % 2 > 0) {
                Calcs.Diff((Numbers)CountSet);
                label1.Text = "" + Calcs.FirstValue + " - " + Calcs.SecondValue + " =";
            }
            SetSizesToElements();
            textBox1.Focus();
        }

        public void MinimizedForm() {
            this.WindowState = FormWindowState.Minimized;
            timer1.Interval = Constant.Interval;
            timer1.Start();
            if (CurrentTask == CurrentTask.Read) {
                CurrentTask = CurrentTask.Math;
                return;
            }
            if (CurrentTask == CurrentTask.Math) {
                CurrentTask = CurrentTask.Read;
                return;
            }
        }

        private void SetSizesToElements() {
            const int Half = 2;
            int lableLocationY = this.Height / Half - label1.Height / Half;
            int lableLocationX = this.Width / Half - label1.Width / Half - textBox1.Width / Half - button1.Width / Half - 25;
            label1.Location = new Point(lableLocationX, lableLocationY);
            int textBoxLocation = label1.Location.X + label1.Width;
            textBox1.Location = new Point(textBoxLocation, lableLocationY);
            int bottonLocation = textBox1.Location.X + textBox1.Width + 50;
            button1.Location = new Point(bottonLocation, lableLocationY);
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (!int.TryParse(e.KeyChar.ToString(), out int num) && e.KeyChar != '\b') {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            CheckAnswerAndMakeTask();
        }

        private void CheckAnswerAndMakeTask() {
            textBox1.Focus();

            if (!int.TryParse(textBox1.Text, out int textBox1Num)) {
                using (var soundPlayer = new SoundPlayer(Properties.Resources.zvuki_derevyannyih_dverey_sborka_31752)) {
                    soundPlayer.Play();
                }
                return;
            }
            if (textBox1Num == Calcs.Result) {
                using (var soundPlayer = new SoundPlayer(Properties.Resources.OK)) {
                    soundPlayer.Play();
                }
                CountOfAnswers++;
                CountSet = Math.Truncate(Convert.ToDecimal(CountOfAnswers / 2));

            }
            if (textBox1Num != Calcs.Result) {
                using (var soundPlayer = new SoundPlayer(Properties.Resources.NO)) {
                    soundPlayer.Play();
                }
                textBox1.Clear();
                return;
            }
            if (CountOfAnswers >= Enum.GetNames(typeof(Numbers)).Length * 2) {
                using (var soundPlayer = new SoundPlayer(Properties.Resources.Finish)) {
                    soundPlayer.Play();
                }

                this.WindowState = FormWindowState.Minimized;
                FilmForm filmForm = new FilmForm();
                filmForm.Show();
                CountOfAnswers = 0;
                CountSet = 0;
                CurrentTask = CurrentTask.Read;
                timer1.Interval = Constant.Interval;
                timer1.Start();
            }
            textBox1.Clear();
            MakeTask();
        }



        private void Form1_Deactivate(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            ShowForm1();
        }

        private void ShowForm1() {
            if (this.WindowState == FormWindowState.Minimized) {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
                this.WindowState = FormWindowState.Maximized;
                MakeTask();
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Stop();
            ShowForm1();
        }

        private void button1_MouseEnter(object sender, EventArgs e) {
            button1.BackgroundImage = Properties.Resources.BottHow2;
        }

        private void button1_MouseHover(object sender, EventArgs e) {

        }

        private void button1_MouseLeave(object sender, EventArgs e) {
            button1.BackgroundImage = Properties.Resources.Кнопка1;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e) {

            button1.Width -= 3;
            button1.Height -= 3;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e) {
            button1.Width += 3;
            button1.Height += 3;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                CheckAnswerAndMakeTask();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            ////PlayMove();
        }

        private void btnFirstPicture_Click(object sender, EventArgs e) {

        }
    }
}
