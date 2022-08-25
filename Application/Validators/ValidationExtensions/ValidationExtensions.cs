using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.ValidationExtensions
{
    public static class ValidationExtensions
    {
        public static bool IsValidFullname(this string fullname)
        {
            string[] stringLimiting = fullname.Split(' ');

            if (IsValidString(fullname)) return false;

            return stringLimiting.Length > 1 && stringLimiting.Length < 7;
        }

        private static bool IsValidString(this string letter)
        {
            if (ChecksIfTheFirstCharactersAreTheSame(letter)
                || letter.Trim() != letter
                || letter.Split(' ').Contains("")
                || letter.Split(' ').Any(_ => !char.IsUpper(_.First()))) return false;

            return true;
        }

        private static bool ChecksIfTheFirstCharactersAreTheSame(this string characters)
        {
            return !characters.All(c => c.Equals(characters.First()));
        }

        public static bool OnlyNumbers(this string numbers)
        {
            return numbers.All(x => x >= '0' && x <= '9');
        }
    }
}