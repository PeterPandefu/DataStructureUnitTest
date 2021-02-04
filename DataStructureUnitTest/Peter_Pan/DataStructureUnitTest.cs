using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace DataStructureUnitTest
{
    [TestClass]
    public partial class DataStructureUnitTest
    {
        private static int _maxValue;
        [ClassInitialize]
        public static void AssemblyInit(TestContext context)
        {
            _maxValue = default;
        }


        [TestMethod]
        [DataRow(99, new int[10] { 12, 24, 90, 73, 84, 56, 48, 27, 19, 99 })]
        public void GetMaxVal(int expected, int[] arr)
        {
            //方法1 迭代遍历
            for (int i = 0; i < arr.Length; i++)
            {
                _maxValue = ReturnMax(arr[i], _maxValue);
            }
            Assert.AreEqual(expected, _maxValue);

            //方法2 递归
            _maxValue = default;
            _maxValue = Recursion(arr);
            Assert.AreEqual(expected, _maxValue);



        }

        public static int ReturnMax(int a, int b)
        {
            return a > b ? a : b;
        }

        public static int Recursion(int[] array, int a = default, int b = default, int index = 0)
        {
            if (index <= array.Length)
            {
                if (index == array.Length)
                {
                    _maxValue = ReturnMax(a, b);
                }
                else
                {
                    _maxValue = ReturnMax(a, b);
                    _maxValue = Recursion(array, array[index], _maxValue, ++index);
                }
            }
            return _maxValue;
        }

    }
}
