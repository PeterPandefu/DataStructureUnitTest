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

            //两个循环  每次核心代码的执行次数为  n+(n-1)+(n-2)+.....+1=n(n-1)/2
            //所以时间复杂度为O(n^2)
        }

        public void LeetCode_No1_Method2(int expected, int[] arr, out int index1, out int index2)
        {
            index1 = default;
            index2 = default;

            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (dic.ContainsKey(expected - arr[i]))
                {
                    index1 = dic[expected - arr[i]];
                    index2 = i;
                }
                dic.Add(arr[i], i);
            }
            //一个循环  所以执行次数为n 时间复杂度则为O(n)
        }

        #endregion

        #region 2 两数相加
        //给你两个 非空 的链表，表示两个非负的整数。它们每位数字都是按照 逆序 的方式存储的，并且每个节点只能存储 一位 数字。
        //请你将两个数相加，并以相同形式返回一个表示和的链表。                                    
        //你可以假设除了数字 0 之外，这两个数都不会以 0 开头。

        //输入：l1 = [2,4,3], l2 = [5,6,4]
        //输出：[7,0,8]
        //解释：342 + 465 = 807.

        //示例 2：
        //输入：l1 = [0], l2 = [0]
        //输出：[0]

        //示例 3：
        //输入：l1 = [9,9,9,9,9,9,9], l2 = [9,9,9,9]
        //输出：[8,9,9,9,0,0,0,1]
        //解释: 9999000+9999999=10009998


        [TestMethod]
        [TestCategory("已完成的测试")]
        [DataRow(807, new int[] { 2, 4, 3 }, new int[] { 5, 6, 4 })]
        [DataRow(0, new int[] { 0 }, new int[] { 0 })]
        [DataRow(10009998, new int[] { 9, 9, 9, 9, 9, 9, 9 }, new int[] { 9, 9, 9, 9 })]
        public void LeetCode_No2(int expected, int[] arr1, int[] arr2)
        {

            LinkedList<int> linkedListA = new LinkedList<int>(arr1);
            LinkedList<int> linkedListB = new LinkedList<int>(arr2);


            LinkedListNode<int> elementA = linkedListA.First;
            LinkedListNode<int> elementB = linkedListB.First;

            //获取两个链表的长度
            int linkedListALength = 0;
            while (elementA != null)
            {
                elementA = elementA.Next;
                linkedListALength++;
            }

            int linkedListBLength = 0;
            while (elementB != null)
            {
                elementB = elementB.Next;
                linkedListBLength++;
            }
            elementA = linkedListA.First;
            elementB = linkedListB.First;

            //填充短链表  填0
            if (linkedListALength > linkedListBLength)
            {
                for (int i = 0; i < linkedListALength - linkedListBLength; i++)
                {
                    elementB.List.AddLast(new LinkedListNode<int>(0));
                }
            }
            if (linkedListALength < linkedListBLength)
            {
                for (int i = 0; i < linkedListBLength - linkedListALength; i++)
                {
                    elementA.List.AddLast(new LinkedListNode<int>(0));
                }
            }

            //接着让  elementA elementB指向链表的第一个元素
            elementA = linkedListA.First;
            elementB = linkedListB.First;

            //得到的结果
            List<int> resList = new List<int>();
            int res = 0;

            int v1 = 0;
            int v2 = 0;

            //是否需要进位
            int carry = 0;

            //循环次数
            int cycles = linkedListALength > linkedListBLength ? linkedListALength : linkedListBLength;
            for (int i = 0; i < cycles; i++)
            {
                v1 = elementA.Value;
                v2 = elementB.Value;
                int num = (v1 + v2 + carry) % 10;
                carry = (v1 + v2 + carry) / 10;
                resList.Add(num);
                elementA = elementA.Next;
                elementB = elementB.Next;

                //若最后一位有进位 则再新增一个
                if (i == cycles - 1 && carry == 1)
                {
                    resList.Add(1);
                }
            }
            //将结果倒叙算值
            resList.Reverse();
            if (resList.Count > 0) resList.ForEach(t => { res = res * 10 + t; });

            Assert.AreEqual(expected, res);

        }




        #endregion
    }
}
