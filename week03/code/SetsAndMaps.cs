using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // create a set to store the words
        var set = new HashSet<string>(words);

        // create a list to store the pairs
        var pairs = new List<string>();

        // loop through each word in the set
        foreach (var word in set)
        {
            // create a string to store the reverse of the word
            var reverse = new string(word.Reverse().ToArray());

            // if the reverse of the word is in the set and the word is not the same as the reverse
            if (set.Contains(reverse) && word != reverse)
            {
                // add the pair to the list
                pairs.Add($"{word} & {reverse}");
                // and remove the reverse from the set
                set.Remove(reverse);
            }

        }
        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            // Get the degree from the 4th column
            // If the degree is in the dictionary, increment the count
            // If the degree is not in the dictionary, add it with a count of 1
            degrees[fields[3]] = degrees.ContainsKey(fields[3]) ? degrees[fields[3]] + 1 : 1;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        // Convert text to lower case
        // Determine if the two words are the same length
        // If they are, continue
        // If the count for the key in word1 is not the same as the count for the key in word2, return false
            // Create a dictionary to store the count of each letter in word1
                // Loop through each letter in word1
                // If the letter is not a space, increment the count in the dictionary
            // Create a dictionary to store the count of each letter in word2
                // Loop through each letter in word2
                // If the letter is not a space, increment the count in the dictionary
            // Loop through each key in the dictionary for word1
            // If the key is not in the dictionary for word2, return false

        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length)
        {
            return false;
        }

        var dict1 = new Dictionary<char, int>();
        foreach (char letter in word1)
        {
            if (dict1.TryGetValue(letter, out int count))
            {
                dict1[letter] = count + 1;
            }
            else
            {
                dict1[letter] = 1;
            }
        }

        var dict2 = new Dictionary<char, int>();
        foreach (char letter in word2)
        {
            if (dict2.TryGetValue(letter, out int count))
            {
                dict2[letter] = count + 1;
            }
            else
            {
                dict2[letter] = 1;
            }
        }

        foreach (var key in dict1.Keys)
        {
            if (!dict2.TryGetValue(key, out int count) || dict1[key] != count)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        // 4. If there are no earthquakes, return an empty array.

        if (featureCollection?.Features == null || featureCollection.Features.Count == 0)
        {
            return Array.Empty<string>();
        }

        var summaries = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            var place = feature.Properties.Place;
            var magnitude = feature.Properties.Magnitude;
            summaries.Add($"{place} - Mag {magnitude}");
        }
        return summaries.ToArray();
    }
}