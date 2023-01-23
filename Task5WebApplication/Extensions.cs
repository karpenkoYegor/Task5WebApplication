using System.Text;
using Task5WebApplication.Models;

namespace Task5WebApplication
{
    public static class Extensions
    {
        public static Random r = new Random(PersonSeed + CurrentPage);
        public static Random randomForUsers = new Random(PersonSeed + CurrentPage);
        public static int PersonSeed = 1;
        public static int CurrentPage = 1;
        public static int CountryId = 1;
        public static double Errors = 0;

        public static List<PersonInformationModel> Persons = new List<PersonInformationModel>();

        public static T PickRandom<T>(this IList<T> source)
        {
            int randIndex = randomForUsers.Next(source.Count);
            return source[randIndex];
        }

        public static StringBuilder DeleteSymbol(this StringBuilder source)
        {
            int randIndex = r.Next(source.Length);
            return source.Remove(randIndex, 1);
        }

        public static StringBuilder SwapSymbol(this StringBuilder source)
        {
            int randIndex;
            int minSymbols = 2;
            if (source.Length - 2 > minSymbols)
                randIndex = r.Next(2, source.Length - 2);
            else
                randIndex = r.Next(2, source.Length - 1);
            var temp = source[randIndex];
            source[randIndex] = source[randIndex + 1];
            source[randIndex + 1] = temp;
            return source;
        }

        public static StringBuilder InsertSymbol(this StringBuilder source, int countryId)
        {
            var engAlphabet = "qwertyuiopasdfghjklzxcvbnm";
            var rusAlphabet = "йцукенгшщзхъфывапролджэячсмитьбю";
            var belAlphabet = "бвгджзйклмнпрстўфхцчшьаоуіэыяеёю";

            int randIndex = r.Next(source.Length);
            
            if (countryId == 1)
            {
                int randChangedIndex = r.Next(rusAlphabet.Length);
                source.Insert(randIndex, rusAlphabet[randChangedIndex]);
            }
            if (countryId == 2)
            {
                int randChangedIndex = r.Next(engAlphabet.Length);
                source.Insert(randIndex, engAlphabet[randChangedIndex]);
            }
            if (countryId == 3)
            {
                int randChangedIndex = r.Next(belAlphabet.Length);
                source.Insert(randIndex, belAlphabet[randChangedIndex]);
            }

            return source;
        }
    }
}
