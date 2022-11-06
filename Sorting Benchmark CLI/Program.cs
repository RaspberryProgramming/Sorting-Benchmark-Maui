using Sorting_Benchmark_CLI;

Console.Write("Please type the amount of elements you'd like to sort and press enter: ");

long n;

try
{
    n = long.Parse(Console.ReadLine());
} catch
{
    throw new Exception("Please type a valid number...");
}

long[] randArr = Sorting.generateRandom(n);

long[] sortedArr = Sorting.generateSorted(n);

Console.WriteLine("\nPre-Sorted Array\n");

// PreSorted Array

Sorting.benchmark(sortedArr, "Bubble Sort - Sorted", Sorting.bubbleSort);

Sorting.benchmark(sortedArr, "Bucket Sort - Sorted", Sorting.bucketSort);

Sorting.benchmark(sortedArr, "Radix Sort  - Sorted ", Sorting.radixSort);

Sorting.benchmark(sortedArr, "Tim Sort    - Sorted   ", Sorting.timSort);

Console.WriteLine("\nRandom Array:\n");

// Random Array

Sorting.benchmark(randArr, "Bubble Sort - Random", Sorting.bubbleSort);

Sorting.benchmark(randArr, "Bucket Sort - Random", Sorting.bucketSort);

Sorting.benchmark(randArr, "Radix Sort  - Random ", Sorting.radixSort);

Sorting.benchmark(randArr, "Tim Sort    - Random", Sorting.timSort);