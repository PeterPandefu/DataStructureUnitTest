using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructureUnitTest.Bu
{
    /// <summary>
    /// LeedCode题库算法题单元测试
    /// </summary>
    [TestClass]
    public class AlgorithmQuestionFromLeetCode
    {

        [ClassInitialize]
        public static void AssemblyInit(TestContext context)
        {

        }
        #region 两数之和
        // 给定一个整数数组 nums 和一个整数目标值 target，请你在该数组中找出 和为目标值 的那 两个 整数，并返回它们的数组下标。
        //你可以假设每种输入只会对应一个答案。但是，数组中同一个元素不能使用两遍。
        //你可以按任意顺序返回答案。
        //来源：力扣（LeetCode）
        //链接：https://leetcode-cn.com/problems/two-sum
        //著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处.
        //示例1
        //输入：nums = [2,7,11,15], target = 9
        //输出：[0,1]
        //解释：因为 nums[0] + nums[1] == 9 ，返回[0, 1] 。

        //示例二
        //输入：nums = [3,2,4], target = 6
        //输出：[1,2]

        //示例三
        //输入：nums = [3,3], target = 6
        //输出：[0,1]

        //时间复杂度为o(n);
        [TestMethod]
        [TestCategory("已完成测试")]
        [DataRow(1,new int[] { 2,7,11,15},new int[] { 9})]
        [DataRow(12, new int[] { 3, 2, 4 },new int[]{6})]
        [DataRow(1, new int[] { 3, 3 }, new int[] { 6 })]
        public void AddSum(int expectedValue, int[] arr1,int[] target)
        {
            int[] indexs = new int[2];
            Dictionary<int, int> dic = new Dictionary<int, int>();//key保存arr1的值，value保存下标
            for (int i = 0; i < arr1.Length; i++)
            {
                if (dic.ContainsKey(arr1[i]))
                {
                    dic.TryGetValue(arr1[i],out indexs[0]);
                    indexs[1] = i;
                    break;
                }
                else
                {
                    dic.Add(target[0]-arr1[i],i);
                }
            }
            int a = int.Parse(string.Join("", indexs));
            Assert.AreEqual(expectedValue,a);
        }
        #endregion
    }
}
