using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DataStructureUnitTest.Peter.Pan
{
    /// <summary>
    /// LeetCode的算法题单元测试
    /// </summary>
    [TestClass]
    public class LeetCodeAlgorithmProblem
    {
        [ClassInitialize]
        public static void AssemblyInit(TestContext context)
        {

        }
        #region 1 两数之和
        //给定一个整数数组 nums 和一个整数目标值 target，请你在该数组中找出 和为目标值 的那 两个 整数，并返回它们的数组下标。
        //你可以假设每种输入只会对应一个答案。但是，数组中同一个元素不能使用两遍。
        //你可以按任意顺序返回答案。
        [TestMethod]
        [TestCategory("已完成的测试")]
        [DataRow(31, new int[10] { 12, 24, 90, 73, 84, 56, 48, 27, 19, 99 })]
        public void LeetCode_No1(int expected, int[] arr)
        {
            //思路1 循环比较 
            LeetCode_No1_Method1(expected, arr, out int index1, out int index2);
            Assert.AreEqual(expected, arr[index1] + arr[index2]);
            //思路2 哈希
            LeetCode_No1_Method2(expected, arr, out index1, out index2);
            Assert.AreEqual(expected, arr[index1] + arr[index2]);

        }

        public void LeetCode_No1_Method1(int expected, int[] arr, out int index1, out int index2)
        {
            index1 = default;
            index2 = default;
            for (int i = 0; i < arr.Length; i++)
            {
                //因为数组中的元素不能用两遍 所以j只能从i之后开始循环 
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] + arr[j] == expected)
                    {
                        index1 = i;
                        index2 = j;
                    }
                }
            }

            //两个循环 
        }

        public void LeetCode_No1_Method2(int expected, int[] arr, out int index1, out int index2)
        {
            index1 = default;
            index2 = default;

            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (dic.ContainsKey(expected- arr[i]))
                {
                    index1 = dic[expected - arr[i]];
                    index2 = i;
                }
                dic.Add(arr[i], i);
            }
        }

        #endregion

    }
}
