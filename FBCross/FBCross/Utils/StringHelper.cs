using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Utils
{
    public static class StringHelper
    {
        public static string RemoveLetters(string str)
        {
            if (str == null) return null;
            string[] letters = new string[]
            {
                "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"
            };
            foreach(var letter in letters)
            {
                str = str.ToLower().Replace(letter, string.Empty);
            }
            return str.Trim();
        }
    }
}
