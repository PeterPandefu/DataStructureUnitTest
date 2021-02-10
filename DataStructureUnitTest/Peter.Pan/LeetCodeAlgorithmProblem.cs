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

            //简单实现  就是把两个链表补充成长度一样的链表 两两相加 最后一位有进位的在补一个
            LinkedListNode<int> newNode1 = LeetCode_No2_Method1(elementA, elementB);
            int res = GetResultByLinkedListNode(newNode1);

            Assert.AreEqual(expected, res);
            //根据链表特性实现
            LinkedListNode<int> newNode2 = LeetCode_No2_Method2(elementA, elementB);

            res = GetResultByLinkedListNode(newNode2);
            Assert.AreEqual(expected, res);
        }

        private int GetResultByLinkedListNode(LinkedListNode<int> newNode1)
        {
            List<int> resList = new List<int>();
            foreach (var item in newNode1.List)
            {
                resList.Add(item);
            }
            int res = 0;
            //倒叙计算
            resList.Reverse();
            resList.ForEach(t => { res = res * 10 + t; });
            return res;

        }

        private LinkedListNode<int> LeetCode_No2_Method1(LinkedListNode<int> elementA, LinkedListNode<int> elementB)
        {
            LinkedListNode<int> headA = new LinkedListNode<int>(0);
            elementA.List.AddFirst(headA);
            LinkedListNode<int> headB = new LinkedListNode<int>(0);
            elementB.List.AddFirst(headB);

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

            elementA = headA.Next;
            elementB = headB.Next;

            //填充短链表  填0
            //接着让  elementA elementB指向链表的除head外第一个元素
            if (linkedListALength > linkedListBLength)
            {
                for (int i = 0; i < linkedListALength - linkedListBLength; i++)
                {
                    elementB.List.AddLast(new LinkedListNode<int>(0));
                }
                elementA = headA.Next;
            }
            if (linkedListALength < linkedListBLength)
            {
                for (int i = 0; i < linkedListBLength - linkedListALength; i++)
                {
                    elementA.List.AddLast(new LinkedListNode<int>(0));
                }
                elementB = headA.Next;
            }

            //得到的结果
            List<int> resList = new List<int>();
            int v1 = 0;
            int v2 = 0;

            //是否需要进位
            int carry = 0;

            LinkedList<int> ls = new LinkedList<int>();
            //循环次数
            int cycles = linkedListALength > linkedListBLength ? linkedListALength : linkedListBLength;
            for (int i = 0; i < cycles; i++)
            {
                v1 = elementA.Value;
                v2 = elementB.Value;
                int num = (v1 + v2 + carry) % 10;
                carry = (v1 + v2 + carry) / 10;
                ls.AddLast(num);
                elementA = elementA.Next;
                elementB = elementB.Next;

                //若最后一位有进位 则再新增一个
                if (i == cycles - 1 && carry == 1)
                {
                    ls.AddLast(1);
                }
            }

            // 因为两个链表长度一样为n  执行次数为链表长度 所以时间复杂度为O(n)

            return ls.First;
        }

        private LinkedListNode<int> LeetCode_No2_Method2(LinkedListNode<int> elementA, LinkedListNode<int> elementB)
        {
            LinkedList<int> ls = new LinkedList<int>();
            int v1 = 0;
            int v2 = 0;

            //是否需要进位
            int carry = 0;

            //循环次数
            while (elementA != null || elementB != null)
            {
                v1 = elementA == null ? 0 : elementA.Value;
                v2 = elementB == null ? 0 : elementB.Value;

                int num = (v1 + v2 + carry) % 10;
                carry = (v1 + v2 + carry) / 10;
                ls.AddLast(num);
                if (elementA != null) elementA = elementA.Next;
                if (elementB != null) elementB = elementB.Next;


                //如果两个链表都循环完毕 并且有进位  则增加一个
                if (elementA == null && elementB == null && carry == 1)
                {
                    ls.AddLast(1);
                }
            }
            // 执行次数那个链表长度长 就执行多少次 所以时间复杂度为O(max(m,n))
            return ls.First;
        }



        #endregion

        #region 3. 无重复字符的最长子串
        //示例 1:
        //输入: s = "abcabcbb"
        //输出: 3 
        //解释: 因为无重复字符的最长子串是 "abc"，所以其长度为 3。

        //示例 2:
        //输入: s = "bbbbb"
        //输出: 1
        //解释: 因为无重复字符的最长子串是 "b"，所以其长度为 1。

        //示例 3:
        //输入: s = "pwwkew"
        //输出: 3
        //解释: 因为无重复字符的最长子串是 "wke"，所以其长度为 3。
        //     请注意，你的答案必须是 子串 的长度，"pwke" 是一个子序列，不是子串。

        //示例 4:
        //输入: s = ""
        //输出: 0

        [TestMethod]
        [TestCategory("已完成的测试")]
        [DataRow(3, "abcabcbb")]
        [DataRow(1, "bbbbb")]
        [DataRow(3, "pwwkew")]
        [DataRow(0, "")]
        [DataRow(1, " ")]
        [DataRow(2, "aua")]
        [DataRow(2, "au")]
        [DataRow(1, "aa")]
        [DataRow(2, "aab")]
        public void LeetCode_No3(int expected, string str)
        {
            //简单实现
            int res = LeetCode_No3_Method1(str);

            Assert.AreEqual(expected, res);

            //滑动窗口实现  保证每个窗口里字母都是唯一的
            //没有重复字符时 调整右边界  有重复字符时调整左边界
            res = LeetCode_No3_Method2(str);

            Assert.AreEqual(expected, res);
        }

        private int LeetCode_No3_Method1(string str)
        {
            if (str.Length <= 1) return str.Length;

            StringBuilder sb = new StringBuilder();
            //用来存放所有所有的不重复的字符串
            List<char> charList = new List<char>();
            int maxLength = 0;
            for (int i = 0; i < str.Length; i++)  //控制外循环
            {

                for (int j = i; j < str.Length; j++)  //控制内循环
                {

                    if (charList.Contains(str[j]))
                    {
                        string temp = sb.ToString();
                        maxLength = temp.Length > maxLength ? temp.Length : maxLength;
                        sb.Clear();
                        charList.Clear();
                        break;
                    }
                    charList.Add(str[j]);
                    sb.Append(str[j]);

                }
            }
            //  执行次数为 n+(n-1)+(n-2)+...+1  时间复杂度为O(n^2)
            return maxLength;
        }
        private int LeetCode_No3_Method2(string s)
        {
            // 哈希集合，记录每个字符是否出现过
            HashSet<char> occ = new HashSet<char>();
            int n = s.Length;
            // 右指针，初始值为 -1，相当于我们在字符串的左边界的左侧，还没有开始移动
            int rk = -1, ans = 0;
            for (int i = 0; i < n; ++i)
            {
                if (i != 0)
                {
                    // 左指针向右移动一格，移除一个字符
                    occ.Remove(s[i - 1]);
                }
                while (rk + 1 < n && !occ.Contains(s[rk + 1]))
                {
                    // 不断地移动右指针
                    occ.Add(s[rk + 1]);
                    ++rk;
                }
                // 第 i 到 rk 个字符是一个极长的无重复字符子串
                ans = Math.Max(ans, rk - i + 1);
            }
            return ans;

        }
        #endregion

        #region 4. 寻找两个正序数组的中位数
        //给定两个大小为 m 和 n 的正序（从小到大）数组 nums1 和 nums2。请你找出并返回这两个正序数组的中位数。
        //进阶：你能设计一个时间复杂度为 O(log (m+n)) 的算法解决此问题吗？

        //示例 1：
        //输入：nums1 = [1,3], nums2 = [2]
        //输出：2.00000
        //解释：合并数组 = [1,2,3] ，中位数 2

        //示例 2：
        //输入：nums1 = [1,2], nums2 = [3,4]
        //输出：2.50000
        //解释：合并数组 = [1,2,3,4] ，中位数(2 + 3) / 2 = 2.5

        //示例 3：
        //输入：nums1 = [0,0], nums2 = [0,0]
        //输出：0.00000

        //示例 4：
        //输入：nums1 = [], nums2 = [1]
        //输出：1.00000

        //示例 5：
        //输入：nums1 = [2], nums2 = []
        //输出：2.00000

        [TestMethod]
        [TestCategory("未开始")]
        [DataRow(2.00000f, new int[] { 1, 3 }, new int[] { 2 })]
        [DataRow(2.50000f, new int[] { 1, 2 }, new int[] { 3, 4 })]
        [DataRow(0.00000f, new int[] { 0, 0 }, new int[] { 0, 0 })]
        [DataRow(1.00000f, new int[] { }, new int[] { 1 })]
        [DataRow(2.00000f, new int[] { 2 }, new int[] { })]
        public void LeetCode_No4(int expected, string str)
        {
        }
        #endregion

        #region 5. 最长回文子串
        //给你一个字符串 s，找到 s 中最长的回文子串。

        //示例 1：
        //输入：s = "babad"
        //输出："bab"
        //解释："aba" 同样是符合题意的答案。

        //示例 2：
        //输入：s = "cbbd"
        //输出："bb"

        //示例 3：
        //输入：s = "a"
        //输出："a"

        //示例 4：
        //输入：s = "ac"
        //输出："a"
        [TestMethod]
        [TestCategory("未开始")]
        [DataRow(2, new int[] { 1, 3 }, new int[] { 2 })]
        [DataRow(1, new int[] { 1, 2 }, new int[] { 3, 4 })]
        [DataRow(3, new int[] { 0, 0 }, new int[] { 0, 0 })]
        [DataRow(0, new int[] { }, new int[] { 1 })]
        [DataRow(1, new int[] { 2 }, new int[] { })]
        public void LeetCode_No5(int expected, string str)
        {
        }
        #endregion

        #region 6. Z 字形变换
        //将一个给定字符串 s 根据给定的行数 numRows ，以从上往下、从左到右进行 Z 字形排列。
        //比如输入字符串为 "PAYPALISHIRING" 行数为 3 时，排列如下：

        //P   A   H   N
        //A P L S I I G
        //Y   I   R

        //之后，你的输出需要从左往右逐行读取，产生出一个新的字符串，比如："PAHNAPLSIIGYIR"。

        //示例 1：
        //输入：s = "PAYPALISHIRING", numRows = 3
        //输出："PAHNAPLSIIGYIR"

        //示例 2：
        //输入：s = "PAYPALISHIRING", numRows = 4
        //输出："PINALSIGYAHRPI"
        //解释：
        //P     I    N
        //A   L S  I G
        //Y A   H R
        //P     I

        //示例 3：
        //输入：s = "A", numRows = 1
        //输出："A"
        [TestMethod]
        [TestCategory("未开始")]
        [DataRow("PAHNAPLSIIGYIR", 3, "PAYPALISHIRING")]
        [DataRow("PINALSIGYAHRPI", 4, "PAYPALISHIRING")]
        [DataRow("A", 1, "A")]
        public void LeetCode_No6(int expectedStr, int numRows, string sourceStr)
        {


        }
        #endregion

        #region 7. 整数反转
        //给你一个 32 位的有符号整数 x ，返回 x 中每位上的数字反转后的结果。
        //如果反转后整数超过 32 位的有符号整数的范围[−2^31, 2^31 − 1] ，就返回 0。
        //假设环境不允许存储 64 位整数（有符号或无符号）。

        //示例 1：
        //输入：x = 123
        //输出：321

        //示例 2：
        //输入：x = -123
        //输出：-321

        //示例 3：
        //输入：x = 120
        //输出：21

        //示例 4：
        //输入：x = 0
        //输出：0

        [TestMethod]
        [TestCategory("未开始")]
        [DataRow(321, 123)]
        [DataRow(-321, -123)]
        [DataRow(21, 120)]
        [DataRow(0, 0)]
        [DataRow(0, 2147483643)]
        public void LeetCode_No7(int expected, int num)
        {
            Int32 res = LeetCode_No7_Method1(num);



            Assert.AreEqual(expected, res);

        }

        public int LeetCode_No7_Method1(int num)
        {
            string numStr = num.ToString();
            bool isegative = false;
            if (numStr.Contains("-"))
            {
                isegative = true;
                numStr = numStr.Substring(1, numStr.Length - 1);
            }
            char[] charArr = numStr.ToCharArray();
            int length = charArr.Length;
            for (int i = 0; i < (int)length / 2; i++)
            {
                (charArr[i], charArr[length - 1 - i]) = (charArr[length - 1 - i], charArr[i]);
            }

            int res = 0;

            try
            {
                res = Convert.ToInt32(string.Concat(charArr));
                if (isegative) res = -res;
            }
            catch (Exception)
            {
                res = 0;

            }
            return res;
        }

        #endregion
    }
}
