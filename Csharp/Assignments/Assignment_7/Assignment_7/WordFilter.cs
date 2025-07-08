using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7
{
    public class WordFilter
    {
        public static void DisplayWordsStartingAEndingM(List<string> words)
        {
            var filteredWords =
                from word in words
                where !string.IsNullOrEmpty(word)
                   && word.Length >= 2
                   && char.ToLower(word[0]) == 'a'
                   && char.ToLower(word[word.Length - 1]) == 'm'
                select word;

            foreach (var word in filteredWords)
            {
                Console.WriteLine(word);
            }
        }
    }

}


