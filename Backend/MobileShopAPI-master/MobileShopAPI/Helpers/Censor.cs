/* ----> Censored - A.NET CORE Profanity Censoring Library <---- */

using System.Text.RegularExpressions;
//using System;
//using System.Collections.Generic;

namespace MobileShopAPI.Helpers
{
    public class Censor
    {
        //
        // Summary:
        //     Gets the censored words list.
        //
        // Value:
        //     The censored words list.
        public IList<string> CensoredWords { get; }

        //
        // Summary:
        //     Initializes a new instance of the Censored.Censor class.
        //
        // Parameters:
        //   censoredWords:
        //     Censored words, if null uses default list
        public Censor(IEnumerable<string>? censoredWords = null)
        {
            CensoredWords = ((censoredWords == null) ? new List<string>() : new List<string>(censoredWords));
        }

        //
        // Summary:
        //     Censors the text and replaces dirty words with ****
        //
        // Parameters:
        //   text:
        //     Text to censor
        //
        // Returns:
        //     The text that is now censored
        public string CensorText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string text2 = text;
            foreach (string censoredWord in CensoredWords)
            {
                string pattern = ToRegexPattern(censoredWord);
                text2 = Regex.Replace(text2, pattern, "\'" + censoredWord.ToUpper() + "\'", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }

            return text2;
        }

        //
        // Summary:
        //     Determines whether the text is dirty (has a bad word in it).
        //
        // Parameters:
        //   text:
        //     Text to check for dirty words.
        //
        // Returns:
        //     true if text is dirty; otherwise, false.
        public bool HasCensoredWord(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            string text2 = text; //Converts every characters of Description to uppercase
            foreach (string censoredWord in CensoredWords)
            {
                string pattern = ToRegexPattern(censoredWord);
                text2 = Regex.Replace(text2, pattern, new MatchEvaluator(StarCensoredMatch), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                if (text2 != text)
                {
                    return true;
                }
            }

            return false;
        }

        private static string StarCensoredMatch(Group m)
        {
            return new string('*', m.Captures[0].Value.Length);
        }

        private static string ToRegexPattern(string wildcardSearch)
        {
            string text = Regex.Escape(wildcardSearch);
            text = text.Replace("\\*", ".*?");
            text = text.Replace("\\?", ".");
            if (text.StartsWith(".*?", StringComparison.Ordinal))
            {
                text = text.Substring(3);
                text = "(^\\b)*?" + text;
            }

            return "\\b" + text + "\\b";
        }
    }
}
