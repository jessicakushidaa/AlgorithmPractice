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

// Applied memoization, sorting and prunning for optimization
// Best time yet for leetcode test cases: 30ms

public class Solution {

    public int FindTargetSumWays(int[] nums, int target) {
        int n = nums.Length;

        Array.Sort(nums, (a, b) => Math.Abs(b).CompareTo(Math.Abs(a))); // this method combines heap-sorting and insertion sorting

        int[] suffixSum = new int[n+1];
        suffixSum[n] = 0;
        for (int i = n -1 ; i >= 0 ; i--) {
            suffixSum[i] = suffixSum[i + 1] + Math.Abs(nums[i]);
        }

        var track = new Dictionary<(int, int), int>(); // using memoization (cache of outranged or in-range sums)
        return DFS(nums, 0, 0, target, track, suffixSum);
    }

    public int DFS(int[] nums, int idx, int currentSum, int target, Dictionary<(int, int), int> track, int[] suffixSum)
    {
        // Prunning - Validates whether SUM is out of range, early return;
        if ( target < currentSum - suffixSum[idx] || target > currentSum + suffixSum[idx])
            return 0;
        if (idx == nums.Length)
            return currentSum == target ? 1 : 0;

        var key = (idx, currentSum);
        if (track.ContainsKey(key)) 
            return track[key];

        int count = DFS(nums, idx + 1, currentSum + nums[idx], target, track, suffixSum) 
                    + DFS(nums, idx + 1, currentSum - nums[idx] , target, track, suffixSum);

        track[key] = count;

        return count;
    }
}