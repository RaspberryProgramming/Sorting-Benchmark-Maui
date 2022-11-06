using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Benchmark_CLI
{
    internal class Sorting
    {
        /****************************
         * Sorting Algorithms       *
         ****************************/

        #region Sorting Algorithms
        /****************************
         * Bubble Sort              *
         ****************************/

        #region Bubble Sort

        public static long[] bubbleSort(long[] arr)
        {
            //Console.WriteLine(arr);
            bool sorted = false;
            long tmp = 0;

            while (!sorted)
            {
                sorted = true;

                for (long i = 0; i < arr.Length - 1; i++)
                {
                    // If next element is less than current element
                    if (arr[i] > arr[i + 1])
                    {
                        tmp = arr[i];
                        // swap
                        arr[i] = arr[i + 1];
                        arr[i + 1] = tmp;
                        sorted = false;
                    }
                }

            }

            return arr;
        }
        #endregion
        /****************************
         * Radix Sort               *
         ****************************/

        #region Radix Sort
        private static long getMaxValue(long[] arr)
        {
            long max = arr[0];

            // Search arr for max value
            for (long i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }

            return max;

        }

        private static long[] countSort(long[] arr, long exp)
        {

            long[] output = new long[arr.Length];
            long[] count = new long[10];
            long i;

            // Initialize count array
            for (i = 0; i < 10; i++)
            {
                count[i] = 0;
            }

            // count occurences into count array
            for (i = 0; i < arr.Length; i++)
            {
                count[(arr[i] / exp) % 10]++; //Increment
            }

            // change count values to position of digit in output array
            for (i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            // Build output array
            for (i = arr.Length - 1; i >= 0; i--)
            {
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            return output;
        }

        public static long[] radixSort(long[] arr)
        {
            long max = getMaxValue(arr);

            for (long exp = 1; max / exp > 0; exp *= 10)
            {
                arr = countSort(arr, exp);
            }

            return arr;
        }

        #endregion

        /****************************
         * Bucket Sort              *
         ****************************/

        #region Bucket Sort
        public static long[] bucketSort(long[] arr)
        {
            long minValue = arr[0];
            long maxValue = arr[0];

            for (long i = 1; i < arr.Length; i++)
            {
                if (arr[i] > maxValue)
                    maxValue = arr[i];
                if (arr[i] < minValue)
                    minValue = arr[i];
            }

            // Create buckets
            List<long>[] bucket = new List<long>[maxValue - minValue + 1];

            for (long i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<long>();
            }

            // Fill each bucket based on 
            for (long i = 0; i < arr.Length; i++)
            {
                bucket[arr[i] - minValue].Add(arr[i]);
            }


            long k = 0;
            for (long i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (long j = 0; j < bucket[i].Count; j++)
                    {
                        arr[k] = bucket[i][(int)j];
                        k++;
                    }
                }

                bucket[i].Clear();
            }

            return arr;
        }

        # endregion

        /****************************
         * Tim Sort                  *
         ****************************/

        #region Tim Sort
        public static void insertionSort(long[] arr,
                                long left, long right)
        {
            for (long i = left + 1; i <= right; i++)
            {
                long temp = arr[i];
                long j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }

        // merge function merges the sorted runs
        public static void merge(long[] arr, long l,
                                       long m, long r)
        {
            // original array is broken in two parts
            // left and right array
            long len1 = m - l + 1, len2 = r - m;
            long[] left = new long[len1];
            long[] right = new long[len2];
            for (long x = 0; x < len1; x++)
                left[x] = arr[l + x];
            for (long x = 0; x < len2; x++)
                right[x] = arr[m + 1 + x];

            long i = 0;
            long j = 0;
            long k = l;

            // After comparing, we merge those two array
            // in larger sub array
            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            // of left, if any
            while (i < len1)
            {
                arr[k] = left[i];
                k++;
                i++;
            }

            // Copy remaining element
            // of right, if any
            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }
        }

        // Iterative Timsort function to sort the
        // array[0...n-1] (similar to merge sort)
        public static long[] timSort(long[] arr)
        {
            long n = arr.Length;
            int RUN = 32;

            // Sort individual subarrays of size RUN
            for (long i = 0; i < n; i += RUN)
                insertionSort(arr, i,
                             Math.Min((i + RUN - 1), (n - 1)));

            // Start merging from size RUN (or 32).
            // It will merge
            // to form size 64, then
            // 128, 256 and so on ....
            for (long size = RUN; size < n;
                                     size = 2 * size)
            {

                // Pick starting point of
                // left sub array. We
                // are going to merge
                // arr[left..left+size-1]
                // and arr[left+size, left+2*size-1]
                // After every merge, we increase
                // left by 2*size
                for (long left = 0; left < n;
                                      left += 2 * size)
                {

                    // Find ending point of left sub array
                    // mid+1 is starting point of
                    // right sub array
                    long mid = left + size - 1;
                    long right = Math.Min((left +
                                        2 * size - 1), (n - 1));

                    // Merge sub array arr[left.....mid] &
                    // arr[mid+1....right]
                    if (mid < right)
                        merge(arr, left, mid, right);
                }
            }

            return arr;
        }

        #endregion
        #endregion

        /****************************
         * Helper Function          *
         ****************************/

        #region helper Functions

        private static short getDigit(long val, long k)
        {
            long digits = countDigits(val);
            short output = (short)(val / Math.Pow(10, (digits - k)) % 10);
            return output;

        }

        private static long countDigits(long val)
        {
            long count = 0;

            while (val > 0)
            {
                val = val / 10;
                count++;
            }

            return count;
        }


        public static bool isSorted(long[] arr)
        {

            for (long i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        public static long[] generateRandom(long size)
        {
            long[] arr = new long[size];

            Random rand = new Random();

            for (long i = 0; i < size; i++)
            {
                arr[i] = rand.Next((int)size);
            }

            return arr;
        }

        public static long[] generateSorted(long size)
        {
            long[] arr = new long[size];

            for (long i = 0; i < size; i++)
            {
                arr[i] = i;
            }

            return arr;
        }

        public static void benchmark(long[] arr, string name, Func<long[], long[]> sortFunc)
        {

            Stopwatch st = new Stopwatch();

            long[] tmpArr = (long[])arr.Clone();

            // ** Radix Sort **

            st.Start();
            long[] rsArr = sortFunc(tmpArr);
            st.Stop();

            Console.WriteLine($"{name.PadRight(25)} Sorted: {isSorted(rsArr)} in {st.ElapsedMilliseconds} ms");

            GC.Collect();

        }

        #endregion

    }
}
