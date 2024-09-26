using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StringCalculator
{
  public int Add(string NumbersWithDelimiters)
  {
    if (string.IsNullOrEmpty(NumbersWithDelimiters))
    {
      return 0;
    }
    var NumbersAndDelimitersTuple = GetDelimiter(NumbersWithDelimiters);
    var numbersList = FilterOutNumbers(NumbersAndDelimitersTuple.Item2, NumbersAndDelimitersTuple.Item1);
    
    VerifyForNegativeNumber(numbersList);
    return SumOfNumbers(numbersList);
  }

 private static Tuple<string, string> GetDelimiter(string numbers)
 {
     if (numbers.StartsWith("//"))
     {
         string[] parts = numbers.Split(new[] { '\n' }, 2);
         return new Tuple<string, string>(Regex.Escape(parts[0].Substring(2)), parts[1]);
     }
     return new Tuple<string, string>(",|\\n", numbers);
 }

 private static List<int> FilterOutNumbers(string numbers, string delimiter)
 {
     var numbersList = (Regex.Split(numbers, delimiter)).ToList();
     return numbersList.Where(n => int.TryParse(n, out _)).Select(n => int.Parse(n)).ToList();
 }

private static void VerifyForNegativeNumber(List<int> numbers)
 {
     List<int> negatives = numbers.FindAll(n => n < 0);
     if (negatives.Count > 0)
     {
         throw new Exception();
     }
 }

private static int SumOfNumbers(List<int> numbers)
 {
     return numbers.Where(n => n <= 1000).Sum();
 }
}
