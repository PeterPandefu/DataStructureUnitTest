using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureUnitTest
{
    public partial class DataStructureUnitTest
    {
       
        /// <summary>
        /// 获取数组中的最大值
        /// </summary>
        /// <param name="expectedValue">期望值</param>
        /// <param name="array">数据源</param>
        [TestMethod]
        [DataRow(90,new int[10] { 3,57,90,30,45,80,70,26,56,78})]
        public void GetMaxValue(int expectedValue,int[] array)
        {
            //方法一:使用迭代的方法
            for (int i = 0; i < array.Length; i++)
            {
                _maxValue = GetMaxByIteration(array[i],_maxValue);
            }
            Assert.AreEqual(expectedValue,_maxValue);

            //方法二:使用递归的方法
            _maxValue = default;
            _maxValue = GetMaxByRecursion(array,0,array.Length-1);
            Assert.AreEqual(expectedValue,_maxValue);
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
                _maxValue = GetMaxByRecursion(arr, i + 1, j);
                if (arr[i] > _maxValue)
                {
                    return arr[i];
                }
                else
                {
                    return _maxValue;
                }
            }
        }
    }
}
