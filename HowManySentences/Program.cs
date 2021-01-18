using System;
using System.Collections.Generic;
using System.Linq;

namespace HowManySentences
{
    class Program
    {
        class Anagram
        {
            public HashSet<string> Words { get; } = new HashSet<string>();
        }

        static void Main(string[] args)
        {
            var sentences = new List<string> { "cat the bats", "in the act", "act tabs in" };
            var words = new HashSet<string> { "bats", "tabs", "in", "cat", "act" };
            Console.WriteLine(string.Join(Environment.NewLine, CountSentences(sentences, words)));
        }

        static int[] CountSentences(IEnumerable<string> sentences, IEnumerable<string> words)
        {
            var result = new List<int>();
            foreach (var sentence in sentences)
            {
                var anagrams = new List<Anagram>();
                var sentenceWords = sentence.Split(" ");
                foreach (var sentenceWord in sentenceWords)
                {
                    var sentenceWordAnagrams = words.Where(x => IsAnagram(x, sentenceWord));
                    if (!sentenceWordAnagrams.Any())
                    {
                        continue;
                    }
                    var anagram = new Anagram();
                    anagram.Words.Add(sentenceWord);
                    anagram.Words.UnionWith(sentenceWordAnagrams);
                    anagrams.Add(anagram);
                }
                var sentenceCombinationsCount = 0;
                foreach (var anagram in anagrams)
                {
                    if (sentenceCombinationsCount == 0)
                    {
                        sentenceCombinationsCount = anagram.Words.Count;
                    }
                    else
                    {
                        sentenceCombinationsCount *= anagram.Words.Count;
                    }
                }
                result.Add(sentenceCombinationsCount);
            }
            return result.ToArray();
        }

        static bool IsAnagram(string a, string b)
        {
            if (a.Equals(b))
            {
                return false;
            }
            if (a.Length != b.Length)
            {
                return false;
            }
            return a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));
        }
    }
}
