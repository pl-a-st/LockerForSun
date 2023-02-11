using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerForSun {
    enum TypeWork {
        Math,
        Read
    }
    static public class Employer {
        static Random Random = new Random();
        static TypeWork GetRandomTypeWork() {
            var values = Enum.GetValues(typeof(TypeWork));
            return (TypeWork)values.GetValue(Random.Next(values.Length));
        }
        static void SetSizesToCalc(MainForm mainForm) {
            const int Half = 2;
            int lableLocationY = mainForm.Height / Half - mainForm.GetLabel1().Height / Half;
            int lableLocationX = mainForm.Width / Half - mainForm.GetLabel1().Width / Half - mainForm.GetTextBox1().Width / Half - mainForm.GetButton1().Width / Half - 25;
            mainForm.GetLabel1().Location = new Point(lableLocationX, lableLocationY);
            int textBoxLocation = mainForm.GetLabel1().Location.X + mainForm.GetLabel1().Width;
            mainForm.GetTextBox1().Location = new Point(textBoxLocation, lableLocationY);
            int bottonLocation = mainForm.GetTextBox1().Location.X + mainForm.GetTextBox1().Width + 50;
            mainForm.GetButton1().Location = new Point(bottonLocation, lableLocationY);
        }
    }
}
                                                                                                                                     