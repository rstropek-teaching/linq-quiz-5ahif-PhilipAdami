using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            List<int> list = new List<int>();

            if (exclusiveUpperLimit < 1) throw new ArgumentOutOfRangeException();

            for(int i=1; i<exclusiveUpperLimit; i++)
            {
                list.Add(i);
            }
            return list.Where(s => s % 2 == 0).ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {

            List<int> list = new List<int>();

            for(int i = exclusiveUpperLimit-1; i>0; i--)
            {
                if (i * i > System.Int32.MaxValue/2)
                    throw new OverflowException();
                    list.Add(i * i);                                                       
                            
            }
            return list.OrderByDescending(p=> p).Where(s=> s%7==0).ToArray();
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            List<FamilySummary> fs = new List<FamilySummary>();
            if (families == null)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < families.Count; i++)
            {
                IFamily family = families.ElementAt(i);
                double old = 0.0;
                double count = 0.0;
                double temp = 0.0;
                if (family.Persons.Count > 0)
                {
                    for (int j = 0; j < family.Persons.Count; j++)
                    {
                        IPerson person = family.Persons.ElementAt(j);
                        old += (int)person.Age;
                        count++;
                    }
                    temp = old / count;
                }
                FamilySummary help = new FamilySummary();
                help.FamilyID = family.ID;
                help.NumberOfFamilyMembers = (int)count;
                help.AverageAge = (decimal)temp;
                fs.Add(help);

            }
            return fs.ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            text.ToUpper();
            text.ToCharArray();
            string neu = "";
            for(int i=0; i<text.Length; i++)
            {
                if (Char.IsLetter(text[i])){
                    neu += text[i];
                }
            }
            Dictionary<char, int> occs = neu.Distinct().Select(x => new KeyValuePair<char, int>(x, neu.Count(f => f == x))).ToDictionary(k => k.Key, v => v.Value);
            var expectedResult = new(char letter, int numberOfOccurrences)[occs.Count];
            for (int count = 0; count < occs.Count; count++)
            {
                var element = occs.ElementAt(count);
                expectedResult[count].letter = element.Key;
                expectedResult[count].numberOfOccurrences = element.Value;

            }
            return expectedResult;
            
        }

    }
}
