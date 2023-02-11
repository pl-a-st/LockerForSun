using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LockerForSun {
    static class Read {
        public static string Result {
            get;
            private set;
        }
        public static List<string> ListPictureNames {
            get;
            private set;
        } = new List<string>();
        public static FileSystemInfo[] PictureFiles {
            get;
            private set;
        } = new DirectoryInfo(DAL.TakeUserPathDirectory()).GetFileSystemInfos();

        static Random Random = new Random();
        public static List<FileSystemInfo> RandomListPictures {
            get;
            private set;
        } = new List<FileSystemInfo>();
        public static string Answer {
            get;
            private set;
        }
        private static void SetNewListRandomPictureFiles(int picturesCount) {
            RandomListPictures.Clear();
            List<FileSystemInfo> temporaryListPictures = new List<FileSystemInfo>(PictureFiles);
            for (int i = 0; i < picturesCount && temporaryListPictures.Count > 0; i++) {
                int rnd = Random.Next(0, temporaryListPictures.Count);
                RandomListPictures.Add(temporaryListPictures[rnd]);
                temporaryListPictures.RemoveAt(rnd);
            }
        }
        private static void SetAnswer(out bool isHasAnswer) {
            if (RandomListPictures.Count < 0) {
                isHasAnswer = false;
                return;
            }
            int rnd = Random.Next(RandomListPictures.Count);
            Answer = RandomListPictures[rnd].Name.Replace(RandomListPictures[rnd].Extension, "");
            isHasAnswer = true;
        }
        public static void SetNewTask(int picturesCount, out bool isHasAnswer) {
            SetNewListRandomPictureFiles(picturesCount);
            SetAnswer(out isHasAnswer);
        }
    }
}
