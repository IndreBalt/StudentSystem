using Microsoft.IdentityModel.Tokens;
using StudentSystem.Database.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentSystem.Presentation
{
    public static class InputsValidations
    {
        public static bool StringLengthNotLessThan5AndIsNotEmpty(string str) // Ne maziau 5 simboliai
        {
            bool truee = true;
            if (str.IsNullOrEmpty() || str.Length < 5)
            {
                Console.WriteLine("Netinkamas simboliu skaicius");
                truee = false;
                Console.ReadKey();
            }
            return truee;
        }
        public static bool StringLength6AndNotEmpty(string str) //6 simboliai
        {
            bool truee = true;            
            if (str.IsNullOrEmpty() || str.Length != 6)
            {
                Console.WriteLine("Netinkamas simboliu skaicius");
                truee = false;
                Console.ReadKey();
            }
            return truee;
        }
        public static bool StringLength8AndIsNotEmpty(string str) // 8 simboliai
        {
            bool truee = true;
            if (str.IsNullOrEmpty() || str.Length != 8)
            {
                Console.WriteLine("Netinkamas simboliu skaicius");
                truee = false;
                Console.ReadKey();
            }
            return truee;
        }
        public static bool StringLengthFrom2To50AndNotEmpty(string str) // 2-50 simboliai
        {
            bool truee = true;
            if (str.IsNullOrEmpty() || str.Length < 2 || str.Length > 50)
            {
                Console.WriteLine("Netinkamas simboliu skaicius");
                truee = false;
                Console.ReadKey();
            }
            return truee;
        }
        public static bool StringLengthFrom3To100AndNotEmpty(string str) // 3-100 simboliai
        {
            bool truee = true;            
            if (str.IsNullOrEmpty() || str.Length < 3 || str.Length > 100)
            {
                Console.WriteLine("Netinkamas simboliu skaicius");
                truee = false;
                Console.ReadKey();
            }
            return truee;
        }                  
        public static bool IsOnlyLetters(string str) // raides
        {
            bool truee = true;
            foreach (char c in str)
            {
                if (!Char.IsLetter(c))
                {
                    truee = false;
                }
            }
            if (truee == false)
            {
                Console.WriteLine("Netinkami simboliai");
                Console.ReadKey();
            }
            return truee;
        }
        public static bool IsOnlyNumbers(string str) // skaiciai
        {
            bool truee = true;
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                {                    
                    truee = false;
                }
            }
            if (truee == false)
            {
                Console.WriteLine("Netinkami simboliai");
                Console.ReadKey();
            }
            return truee;
        }
        public static bool IsLettersAndNumbers(string str) // raides ir skaiciai
        {
            bool truee = true;
            foreach (char c in str)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    truee = false;
                }
            }
            if (truee == false)
            {
                Console.WriteLine("Netinkami simboliai");
                Console.ReadKey();
            }
            return truee;
        }
        public static TimeOnly IsTimeOnlyValid(string time) // Ar laiko formatas tinkamas
        {
            if (!TimeOnly.TryParse(time, out TimeOnly lectureTime))
            {
                Console.WriteLine("Netinkamas formatas");
                Console.ReadKey();
            }
            return lectureTime;
        }
    }
}
