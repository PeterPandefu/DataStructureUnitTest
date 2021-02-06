using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureUnitTest.Bu
{
    [TestClass]
    public class DataStructureUnitTest
    {
        private static int _maxVal;
        [ClassInitialize]
        public static void AssemblyInit(TestContext context)
        {
            _maxVal = default;

        }
        #region 1.获取数组中的最大值
        /// <summary>
        /// 获取数组中的最大值
        /// </summary>
        /// <param name="expectedValue">期望值</param>
        /// <param name="array">数据源</param>
        [TestMethod]
        [DataRow(90, new int[10] { 3, 57, 90, 30, 45, 80, 70, 26, 56, 78 })]
        public void GetMaxValue(int expectedValue, int[] array)
        {
            //方法一:使用迭代的方法
            for (int i = 0; i < array.Length; i++)
            {
                _maxVal = GetMaxByIteration(array[i], _maxVal);
            }
            Assert.AreEqual(expectedValue, _maxVal);

            //方法二:使用递归的方法
            _maxVal = default;
            _maxVal = GetMaxByRecursion(array, 0, array.Length - 1);
            Assert.AreEqual(expectedValue, _maxVal);
        }
        public static int GetMaxByIteration(int a, int b)
        {
            return a > b ? a : b;
        }

        public static int GetMaxByRecursion(int[] arr, int i, int j)
        {
            //如果数组的长度为1，直接输出arr[0]为最大值
            if (i == j)
            {
                return arr[i];
            }
            else
            {
                _maxVal = GetMaxByRecursion(arr, i + 1, j);
                if (arr[i] > _maxVal)
                {
                    return arr[i];
                }
                else
                {
                    return _maxVal;
                }
            }
        }
        #endregion
    }
}
