using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LockerForSun {
    static class DAL {
        public static string TakeUserPathFile(string fileName) {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + Constant.ProgramName + "\\";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return path + fileName;
        }
        public static string TakeUserPathDirectory() {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + Constant.ProgramName;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
