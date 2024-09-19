using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class NegativeNumberException : Exception
{
 public NegativeNumberException(List<int> numbers) : base("Negative numbers not allowed: " + string.Join(", ", numbers))
 {
 }
}

public class StringCalculator
{
  public int Add(string NumbersWithDelimiters)
  {
    if (string.IsNullOrEmpty(NumbersWithDelimiters))
    {
      return 0;
    }
    var NumbersAndDelimitersTuple = GetDelimiter(NumbersWithDelimiters);
    var num_list = SplitNumbers(NumbersAndDelimitersTuple.Item1, NumbersAndDelimitersTuple.Item2);
    var num_list = FilterNumbers(num_list);
    
    CheckForNegatives(num_list);
    return SumNumbers(num_list);
  }

public static Tuple<string, string> GetDelimiter(string numbers)
 {
     if (numbers.StartsWith("//"))
     {
         string[] parts = numbers.Split(new[] { '\n' }, 2);
         return new Tuple<string, string>(Regex.Escape(parts[0].Substring(2)), parts[1]);
     }
     return new Tuple<string, string>(",|\\n", numbers);
 }

 public static string[] SplitNumbers(string numbers, string delimiter)
 {
     return Regex.Split(numbers, delimiter);
 }

public static void CheckForNegatives(List<int> numbers)
 {
     List<int> negatives = numbers.FindAll(n => n < 0);
     if (negatives.Count > 0)
     {
         throw new NegativeNumberException(negatives);
     }
 }

public static int SumNumbers(int[] numbers)
 {
     return numbers.Where(n => n <= 1000).Sum();
 }

 public static List<int> FilterNumbers(List<string> numList)
 {
     return numList.Where(n => int.TryParse(n, out _)).Select(n => int.Parse(n)).ToList();
 }
}
