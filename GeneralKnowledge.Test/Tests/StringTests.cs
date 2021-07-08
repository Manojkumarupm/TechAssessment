using System;
using System.Linq;
namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Basic string manipulation exercises
    /// </summary>
    public class StringTests : ITest
    {
        public void Run()
        {
            // TODO
            // Complete the methods below

            AnagramTest();
            GetUniqueCharsAndCount();
        }

        private void AnagramTest()
        {
            var word = "stop";
            var possibleAnagrams = new string[] { "test", "tops", "spin", "post", "mist", "step" };

            foreach (var possibleAnagram in possibleAnagrams)
            {
                Console.WriteLine(string.Format("{0} > {1}: {2}", word, possibleAnagram, possibleAnagram.IsAnagram(word)));
            }
        }

        private void GetUniqueCharsAndCount()
        {
            var word = "xxzwxzyzzyxwxzyxyzyxzyxzyzyxzzz";

            // TODO
            // Write an algorithm that gets the unique characters of the word below 
            // and counts the number of occurrences for each character found
            var result = word.ToLower().GroupBy(y => y).Select(x => (x.Key, Count: x.Count())).ToList();
            Console.WriteLine(string.Format(" Word : {0} contains {1} find unique characters Listed below",word,word.Length));
            foreach (var item in result)
            {
                Console.WriteLine(string.Format("Char : {0} : identified count: {1}",item.Key,item.Count));
            }
        }
    }

    public static class StringExtensions
    {
        public static bool IsAnagram(this string a, string b)
        {
            bool Result = false;
            try
            {
                Result = String.IsNullOrWhiteSpace(a) && String.IsNullOrWhiteSpace(b) ? true : false;
                // TODO
                // Write logic to determine whether a is an anagram of b
                if (!Result)
                {
                    if (!String.IsNullOrWhiteSpace(b))
                    {
                        if (!String.Equals(a, b, StringComparison.OrdinalIgnoreCase)) // compare a & b is not equal
                        {
                            var set1= a.Replace(" ", "").ToLower().GroupBy(y => y).Select(x => (x.Key, Count: x.Count())).ToList();
                            var set2 = b.Replace(" ", "").ToLower().GroupBy(y => y).Select(x => (x.Key, Count: x.Count())).ToList();
                            Result= set1.Count == set2.Count && set1.Count == set1.Where(x => set2.Contains(x)).ToList().Count();
                        }
                        else
                        {
                            Result = true; 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Some exception occured handle the exception here and take appropriate action
                throw;
            }
            return Result;
        }
    }
}
