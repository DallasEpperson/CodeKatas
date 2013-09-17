using System;
using System.Collections.Generic;
using System.Linq;

namespace Roman_Numerals
{
    public static class RomanNumerals
    {
        private static readonly Dictionary<string, int> RomanNumeralValues = new Dictionary<string, int>()
                                                                {
                                                                    {"M",1000},
                                                                    {"D",500},
                                                                    {"C",100},
                                                                    {"L",50},
                                                                    {"X",10},
                                                                    {"V",5},
                                                                    {"I",1},
                                                                    {"N",0}
                                                                };
        

        public static int Convert(string inputString)
        {
            int returnInt = 0;
            int stringCursor = 0;

            while (stringCursor < inputString.Length)
            {
                if (stringCursor == inputString.Length - 1)
                {
                    returnInt += RomanNumeralValues[inputString.Substring(stringCursor, 1)];
                }
                else if (inputString.Substring(stringCursor, 1).IsSmallerThan(inputString.Substring(stringCursor + 1, 1)))
                {
                    returnInt += (RomanNumeralValues[inputString.Substring(stringCursor + 1, 1)] - RomanNumeralValues[inputString.Substring(stringCursor, 1)]);
                    stringCursor++; //move an extra digit because we took care of a pair of digits.
                }
                else
                {
                    returnInt += RomanNumeralValues[inputString.Substring(stringCursor, 1)];
                }
                stringCursor++;
            }
            return returnInt;
        }

        public static string Convert(int inputInt)
        {
            var retString = "";
            
            var ValidCombinations = new Dictionary<string, int>();
            foreach (var SingleCharacterRomanNumeral in RomanNumeralValues)
            {
                //Add each single 'digit' entry
                ValidCombinations.Add(SingleCharacterRomanNumeral.Key, SingleCharacterRomanNumeral.Value);

                //Add multi 'digit' entries that go with this digit. Rules:
                //  Combinations where a lesser digit comes before a greater digit may exist.
                //  Lesser digit cannot be 0, because that's just retarded.
                //  Lesser digit must be a power of ten (so C, X, and I)
                //  Lesser digit must be at least one tenth the value of the greater digit (so no IC = 99)
                foreach (var PrefixChar in RomanNumeralValues)
                {
                    if ((PrefixChar.Key != "N") &&
                        (PrefixChar.Value.IsPowerOfTen()) &&
                        (PrefixChar.Value < SingleCharacterRomanNumeral.Value) &&
                        (PrefixChar.Value >= SingleCharacterRomanNumeral.Value/10))
                    {
                        ValidCombinations.Add(PrefixChar.Key + SingleCharacterRomanNumeral.Key,
                                                SingleCharacterRomanNumeral.Value - PrefixChar.Value);
                    }
                }
            }

            var valueRemaining = inputInt;
            while (valueRemaining > 0)
            {
                var thing =
                    ValidCombinations.OrderByDescending(kp => kp.Value).First(kp => kp.Value <= valueRemaining);
                retString = retString + thing.Key;
                valueRemaining -= thing.Value;
            }

            return retString == ""? "N": retString;
        }

        private static Boolean IsSmallerThan(this string first, string second)
        {
            int firstInt = RomanNumeralValues[first];
            int secondInt = RomanNumeralValues[second];

            return firstInt < secondInt;
        }

        private static Boolean IsPowerOfTen(this int integer)
        {
            //silly way to do this
            //powers of ten start with one one and have only zeros behind.
            var intString = integer.ToString();
            if (!(intString.StartsWith("1")))
                return false;
            return (intString.Replace("0", "").Length == 1);
        }
    }
}
