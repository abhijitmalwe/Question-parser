using System.Linq;

namespace QParser.Admin.Core
{
    public static class Extensions
    {
        public static string[] Trim(this string[] text)
        {
            return text.Select(t => t.Trim()).ToArray();
        }


        public static string RemoveQuestionLabel(this string phrase)
        {
            return phrase.ToLower().Replace("question:","").Trim();
        }


        public static bool CharsInOrder(this string[] values)
        {
            var number2 = 0;
            var number = 0;
            char ch;
            for (int i = 0; i < values.Length; i++)
            {
                ch = values[i][0];
                number = (int)ch;
                if (number < 65 || number > 90)
                {
                    return false;
                }

                if (i <= 0 &&  number!=65)
                {
                    return false;
                }
                if (i <= 0 ) continue;
                
                    number2 = (int)values[i - 1][0];
                if (number2 != (number - 1))
                {
                    return false;
                }
            }
            return true;
        }
	}
}