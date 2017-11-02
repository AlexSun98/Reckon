using Reckon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reckon.DomainService
{
    public class PrintService : IPrintService
    {
        public void Print(List<int> numbers)
        {
            if (numbers.Count != 100)
                throw new Exception("Total numnber of printable items is not 100");

            foreach(var number in numbers)
            {
                var result = NumberConverter(number);
                Console.WriteLine(result);
            }
        }

        public List<int> SetupNumeric()
        {
            return Enumerable.Range(1, 100).ToList();
        }

        public string NumberConverter(int number)
        {
            var result = number.ToString();
            //For multiples of 3 print the word “Boss” instead of the number
            if (number % 3 == 0)
            {
                result = "Boss";
            }
            //For multiples of 5 print the word “Hog” instead of the number
            if (number % 5 == 0)
            {
                result = "Hog";
            }
            //Fpr numbers which are multiples of both 3 and 5 print the word “BossHog”
            if (number % 3 == 0 && number % 5 == 0)
            {
                result = "BossHog";
            }
            return result;
            
        }
    }
}
