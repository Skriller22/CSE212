using System.Xml;

public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10};
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1};
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        var output = new List<int>();
        var list1Index = 0;
        var list2Index = 0;
        for (int i = 0; i < select.Length; i++)
        {
            if (select[i] == 1 )
            {
                list1Index++;
                output.Add(list1[list1Index - 1]);
            }
            else if (select[i] == 2)
            {
                list2Index++;
                output.Add(list2[list2Index - 1]);
            }
        }
        return output.ToArray();
    }
}