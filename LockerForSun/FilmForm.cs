using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using LockerForSun.Properties;

namespace LockerForSun {
    public partial class FilmForm : Form {
        public FilmForm() {
            InitializeComponent();
        }

        private void FilmForm_Load(object sender, EventArgs e) {
            this.TopMost = true;
            this.Focus();
            PlayMove();
        }
        private void PlayMove() {
            string mp4Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MyVideo.avi");
            if (!File.Exists(mp4Path)) {
                File.WriteAllBytes(mp4Path, Properties.Resources.Move);
            }
            axWindowsMediaPlayer1.Size = this.Size;
            axWindowsMediaPlayer1.URL = mp4Path;
            axWindowsMediaPlayer1.Visible = true;
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e) {
            if (e.newState == 1) {
                this.Close();
            }
        }
    }
}
