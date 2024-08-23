/* Implement of Censoring Library */

namespace MobileShopAPI.Helpers
{
    public class CensoredWord
    {
        public static List<string> censoredWords = new List<string>
        {
            "đụ",
            "du",
            "mat day",
            "mất dạy",
            "lon",
            "lồn",
            "cặc",
            "cac",
            "đĩ",
            "Đỉ"
        };//Caution: HOT. List of Censored Words

        public static string? result = null;

        public static bool isCensoredWord(string word)
        {
            bool check = false;//Check of censored words
            var censor = new Censor(censoredWords);
            check = censor.HasCensoredWord(word);
            result = censor.CensorText(word);
            if (check)
                return true;
            else
                return false;
        }
    }
}
