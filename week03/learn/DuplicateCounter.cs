using System.Diagnostics;

public class DuplicateCounter
{
    //Count how many duplicates are in a collection of data.

    public static void Run()
    {
        int[] data = [
            50, 9, 24, 100, 7, 75, 93, 24, 17, 16, 97, 6, 18, 81, 48, 37, 49, 33, 60, 3, 99, 32, 88, 29, 65, 20, 35, 33,
            15, 81, 31, 93, 17, 5, 5, 79, 12, 91, 18, 31, 12, 94, 39, 98, 10, 72, 20, 79, 100, 27, 46, 28, 50, 1, 7, 14,
            78, 100, 55, 26, 48, 33, 96, 77, 69, 8, 33, 36, 42, 98, 42, 32, 49, 65, 1, 82, 30, 74, 73, 89, 23, 76, 25,
            4, 76, 7, 72, 86, 71, 29, 18, 98, 84, 20, 24, 18, 11, 33, 39, 96, 1, 97, 65, 41, 62, 48, 59, 51, 17, 89, 6,
            29, 98, 49, 37, 72, 63, 49, 12, 79, 27, 23, 23, 13, 90, 47, 11, 66, 41, 97, 2, 60, 1, 21, 38, 100, 98, 2,
            18, 75, 86, 52, 63, 58, 26, 80, 62, 82, 63, 94, 33, 76, 7, 11, 49, 2, 34, 3, 10, 27, 71, 60, 4, 94, 100, 95,
            46, 15, 21, 40, 35, 98, 89, 25, 46, 54, 24, 75, 92, 69, 37, 63, 71, 70, 90, 91, 82, 81, 4, 10, 82, 1, 32, 8,
            13, 47, 8, 52, 30, 54, 4, 79, 7, 90, 81, 33, 65, 89, 84, 83, 46, 95, 82, 6, 93, 5, 22, 67, 8, 79, 3, 55, 79,
            6, 54, 10, 22, 16, 40, 67, 50, 58, 37, 35, 7, 44, 10, 31, 45, 93, 12, 55, 67, 48, 32, 43, 57, 58, 37, 76,
            85, 47, 80, 18, 32, 59, 98, 92, 53, 98, 29, 61, 82, 42, 78, 97, 23, 94, 38, 20, 73, 11, 99, 94, 92, 82, 82,
            65
        ];

        Console.WriteLine($"Number of items in the collection: {data.Length}");
        Console.WriteLine($"Number of duplicates : {CountDuplicates(data)}");
        Console.WriteLine();
        Console.WriteLine("Me just messing around:");
        MeasureExecutionTime(() => CountDuplicates(data), "CountDuplicates");
        MeasureExecutionTime(() => CountDuplicatesAlternate(data), "CountDuplicatesAlternate");
        MeasureExecutionTime(() => CountDuplicatesUsingDictionary(data), "CountDuplicatesUsingDictionary");
    }

    private static int CountDuplicates(int[] data)
    {
        //Find the largest number in the array and create hashset with that many spaces
        //Using this method I would maybe create a hard-cap or maybe a check to see if the array is too large
        int max = data.Max();
        HashSet<int> hashSet = new HashSet<int>(max);

        //Create a counter for the number of duplicates
        int duplicates = 0;

        //Iterate through the array
        foreach (int number in data)
        {
            //If the number is already in the hashset, increment the duplicates counter
            if (hashSet.Contains(number))
                duplicates++;
            //If the number is not in the hashset, add it
            else
                hashSet.Add(number);
        }


        // After the fact, this function could actually be used to reduce memory usage. It could initially count the duplicates and then subtract that from the max number in any future calls.
        // Another way to accomplish this task would be to store the uniques in a list and then return the count of the duplicates.
        // Actually... I'm going to do that and compare the complexity of the two methods.
        return duplicates;
    }
    private static int CountDuplicatesAlternate(int[] data)
    {
        // Create hashset for each unique number
        var unique = new HashSet<int>();
        // Create counter for duplicates
        var duplicates = 0;

        // Iterate through array and count every time a unique entry is encountered again. Otherwise, add to unique hashset.
        foreach (var x in data)
        {
            if (unique.Contains(x))
                duplicates++;
            else
                unique.Add(x);
        }
        return duplicates;

        // Interestingly enough, this method seems to take longer. But maybe it uses less resources compared the the larger hashset?
    }

    private static int CountDuplicatesUsingDictionary(int[] data)
    {
        // Create a dictionary to store the frequency of each number
        var frequency = new Dictionary<int, int>();
        // Create a counter for duplicates
        int duplicates = 0;

        // Iterate through the array
        foreach (int number in data)
        {
            // If the number is already in the dictionary, increment its count
            if (frequency.ContainsKey(number))
            {
                frequency[number]++;
                // If the count is more than or equal to 2, it means we have found a duplicate
                if (frequency[number] >= 2)
                {
                    duplicates++;
                }
            }
            // If the number is not in the dictionary, add it with a count of 1
            else
            {
                frequency[number] = 1;
            }
        }

        return duplicates;

        // I tried some AI after completing the assignment and it said it could make a more efficient version of my code... I'm not sure I believe it after seeing the results. It is slow, innefficient, and was actually incorrect until I fixed a bool check. I guess it avoids potential collisions at least.
    }

    private static void MeasureExecutionTime(Func<int> function, string functionName)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        int result = function();
        stopwatch.Stop();
        Console.WriteLine($"{functionName} took {stopwatch.ElapsedTicks} ticks or {stopwatch.Elapsed.TotalMilliseconds:F3} ms to find {result} duplicates.");
    }
}