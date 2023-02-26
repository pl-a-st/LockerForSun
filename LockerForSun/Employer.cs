using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace LockerForSun {
    enum TypeWork {
        Math,
        Read
    }
    static public class Employer {
        static int Counter;
        static int ReadCounter;
        static List<Control> ControlsToDel = new List<Control>();
        static public void GetTaskRead(MainForm form, int picturesCount = 4, int taskCount = 5) {
            const int Padding = 10;
            Read.SetNewTask(picturesCount, out bool isHasAnswer);
            if (!isHasAnswer) {
                return;
            }
            foreach (Control control in form.Controls) {
                control.Visible = false;
            }
            DesporeControls();
            Label LableForAnswer = NewLable(form);
            SetLableForReadCounter(form, taskCount);
            for (int i = 0; i < Read.RandomListPictures.Count; i++) {
                Button button = NewButton(form, picturesCount, taskCount, Padding, LableForAnswer, i);
                ControlsToDel.Add(button);
                form.Controls.Add(button);
            }

        }

        private static void SetLableForReadCounter(MainForm form, int taskCount) {
            Label lableForReadCounter = new Label();
            lableForReadCounter.Text = ReadCounter + " из " + taskCount;
            lableForReadCounter.Font = new Font(lableForReadCounter.Font.FontFamily, 25);
            lableForReadCounter.ForeColor = Color.DarkGreen;
            lableForReadCounter.AutoSize = true;
            form.Controls.Add(lableForReadCounter);
        }

        private static Button NewButton(MainForm form, int picturesCount, int taskCount, int Padding, Label LableForAnswer, int numButton) {
            Button button = new Button();
            button.Width = (form.ClientSize.Width - (picturesCount + 1) * Padding) / picturesCount;
            button.Height = button.Width;
            button.Location = new Point(Padding + numButton * (button.Width + Padding), LableForAnswer.Location.Y + LableForAnswer.Height + 3 * Padding);
            button.BackgroundImage = Image.FromFile(Read.RandomListPictures[numButton].FullName);
            button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            button.Name = Read.RandomListPictures[numButton].Name.Replace(Read.RandomListPictures[numButton].Extension, "");
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = form.BackColor;
            button.Click += Button_Click;
            void Button_Click(object sender, EventArgs e) {
                if (button.Name == LableForAnswer.Text) {
                    ReadCounter++;
                    using (var soundPlayer = new SoundPlayer(Properties.Resources.OK)) {
                        soundPlayer.Play();
                    }
                    if (ReadCounter == taskCount) {
                        ReadCounter = 0;
                        DesporeControls();
                        foreach (Control control in form.Controls) {
                            control.Visible = true;
                        }
                        form.MinimizedForm();
                        return;
                    }
                }
                else {
                    if (ReadCounter > 0) {
                        ReadCounter--;
                    }
                    using (var soundPlayer = new SoundPlayer(Properties.Resources.NO)) {
                        soundPlayer.Play();
                    }
                }
                GetTaskRead(form, picturesCount, taskCount);
            }
            return button;
        }

        private static Label NewLable(MainForm form) {
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font(label.Font.FontFamily, 160);
            label.Text = Read.Answer;
            label.AutoSize = true;
            label.Location = new Point(form.ClientSize.Width / 2 - label.PreferredWidth / 2, form.ClientSize.Height / 3 - label.PreferredHeight);
            ControlsToDel.Add(label);
            form.Controls.Add(label);
            return label;
        }

        private static void DesporeControls() {
            while (ControlsToDel.Count > 0) {
                ControlsToDel[0].Dispose();
                ControlsToDel.RemoveAt(0);
            }
        }
    }
}
