public static class UniqueLetters {
    public static void Run() {
        var test1 = "abcdefghjiklmnopqrstuvwxyz"; // Expect True because all letters unique
        Console.WriteLine(AreUniqueLetters(test1));

        var test2 = "abcdefghjiklanopqrstuvwxyz"; // Expect False because 'a' is repeated
        Console.WriteLine(AreUniqueLetters(test2));

        var test3 = "";
        Console.WriteLine(AreUniqueLetters(test3)); // Expect True because its an empty string
    }

    /// <summary>Determine if there are any duplicate letters in the text provided</summary>
    /// <param name="text">Text to check for duplicate letters</param>
    /// <returns>true if all letters are unique, otherwise false</returns>
    private static bool AreUniqueLetters(string text) {
        // TODO Problem 1 - Replace the O(n^2) algorithm to use sets and O(n) efficiency

        // Create a unique hashset for each letter in the english alphabet.
        var unique = new HashSet<char>();
        // If the text is empty, return true.
        if (text.Length == 0) {
            return true;
        }
        // Add each letter to the hashset. If the letter is already in the hashset, return false.
        foreach (var letter in text) {
            if (!unique.Add(letter)) {
                return false;
            }
        }
        return true;
    }
}