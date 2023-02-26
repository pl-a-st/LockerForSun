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
            bool isWasEqualFirstLetter = false;
            List<FileSystemInfo> temporaryListPictures = new List<FileSystemInfo>(PictureFiles);
            for (int i = 0; RandomListPictures.Count < picturesCount && temporaryListPictures.Count > 0; i++) {
                int rnd = Random.Next(0, temporaryListPictures.Count);
                RandomListPictures.Add(temporaryListPictures[rnd]);
                temporaryListPictures.RemoveAt(rnd);
                if (!isWasEqualFirstLetter) {
                    isWasEqualFirstLetter = GetEqualFirstLetter(picturesCount, temporaryListPictures);
                }
            }
        }

        private static bool GetEqualFirstLetter(int picturesCount, List<FileSystemInfo> temporaryListPictures) {
            bool isWasEqualFirstLetter;
            foreach (FileSystemInfo pictureFileInfo in temporaryListPictures) {
                if (RandomListPictures.Count == picturesCount) {
                    isWasEqualFirstLetter = true;
                    break;
                }
                if (pictureFileInfo.Name[0] == RandomListPictures[0].Name[0]) {
                    RandomListPictures.Add(pictureFileInfo);

                }
            }
            isWasEqualFirstLetter = true;
            return isWasEqualFirstLetter;
        }

        private static void GetEqualFirstLetter(int i){

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
