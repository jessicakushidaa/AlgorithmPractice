/** This is a classic TargetSum exercise
* The goal here is given an array of numbers, count all the combinations
* in wich the sum of the combos are equal to a given target

Example 1:

Input: nums = [1,1,1,1,1], target = 3
Output: 5
Explanation: There are 5 ways to assign symbols to make the sum of nums be target 3.
-1 + 1 + 1 + 1 + 1 = 3
+1 - 1 + 1 + 1 + 1 = 3
+1 + 1 - 1 + 1 + 1 = 3
+1 + 1 + 1 - 1 + 1 = 3
+1 + 1 + 1 + 1 - 1 = 3

*/


// This is okay, but it does not pass to all test cases in leetcode
// due to recursion cost for long arrays

public class Solution {
    public int FindTargetSumWays(int[] nums, int target) {
        var btree = new List<List<int>>();
        foreach (var number in nums){
            var pair = new List<int>{number, -number};
            btree.Add(pair);
        }
        var stack = new List<int>();
        return DFS(btree, 0, stack, target);
    }
    public int DFS(List<List<int>> btree, int idx, List<int> stack, int target) {
        var targetCount = 0;
        if (idx == btree.Count)
        {
            int sum = 0;
            foreach (var itemIterated in stack) sum += itemIterated;
            if (sum == target) {
                return 1;
            }
            return 0;
        }
        foreach (var item in btree[idx]) {
            stack.Add(item);
            targetCount += DFS(btree, idx + 1, stack, target);
            stack.RemoveAt(stack.Count - 1);
        }
        return targetCount;
    }
}