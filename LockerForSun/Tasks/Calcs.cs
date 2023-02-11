using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerForSun {
   
    public static class Calcs {
        public static int FirstValue;
        public static int SecondValue;
        private static Random Random = new Random();
        public static int Result;

        public static void Sum(Numbers numbers) {
            GetValues(numbers);
            Result = FirstValue + SecondValue;
        }
        public static void Diff(Numbers numbers) {
            GetValues(numbers);
            if (SecondValue > FirstValue) {
                int firstValue = FirstValue;
                int secondValue = SecondValue;
                FirstValue = secondValue;
                SecondValue = firstValue;
            }
            Result = FirstValue - SecondValue;
        }
        private static void GetValues(Numbers numbers) {
            FirstValue = GetRandomNum(numbers);
            SecondValue = GetRandomNum(numbers);
        }
        private static int GetRandomNum(Numbers numbers) {
            int num = (int)numbers;
            return Random.Next((int)Math.Pow(10, num), (int)Math.Pow(10, num + 1));
        }

    }
}
