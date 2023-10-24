using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    class Program
    {
        static void Main(string[] args)
        {
            Prison prison = new Prison();
            prison.ShowInfoCryminals();
        }
    }

    class Prison
    {
        private static Random _random = new Random();
        List<Criminal> _criminalsBeforeAmnesty = new List<Criminal>();
        private List<string> _crime = new List<string>();

        public Prison()
        {
            GenerateCrimes();
            GenerateCriminals();
        }

        public void ShowInfoCryminals()
        {
            Console.WriteLine("Список заключенных до амнистии");
            ShowArrestedCryminals();

            Console.WriteLine("Список заключенных после амнистии");

            _criminalsBeforeAmnesty.Where(criminal => criminal.Crime == TypeCrime.AntiGovernment).ToList().ForEach(criminal => criminal.IsArrested = false);

            ShowArrestedCryminals();
        }

        private void ShowArrestedCryminals()
        {
            foreach (Criminal criminal in _criminalsBeforeAmnesty.Where(x => x.IsArrested == true))
            {
                Console.WriteLine($"Name - {criminal.Name}, crime - {criminal.Crime}");
            }
        }

        private void GenerateCriminals()
        {
            int countCriminals = 10;

            {
                for (int i = 0; i < countCriminals; i++)
                {
                    string crime = GetRandomCrime();
                    _criminalsBeforeAmnesty.Add(new Criminal($"{i + 1}", crime, GenerateArrestedValue(crime, i)));
                }
            }
        }

        private bool GenerateArrestedValue(string crime, int index)
        {
            if (crime == TypeCrime.AntiGovernment)
            {
                return true;
            }

            return index % 2 == 0 ? true : false;
        }

        private void GenerateCrimes()
        {
            _crime.AddRange(new List<string>() { TypeCrime.AntiGovernment, TypeCrime.Killer });
        }

        private string GetRandomCrime()
        {
            return _crime[_random.Next(0, _crime.Count)];
        }
    }

    class Criminal
    {
        public Criminal(string name, string crime, bool isArrested)
        {
            Name = name;
            Crime = crime;
            IsArrested = isArrested;
        }

        public string Crime { get; private set; }
        public string Name { get; private set; }
        public bool IsArrested { get; set; }
    }

    class TypeCrime
    {
        public static string AntiGovernment => "Антиправительственное";
        public static string Killer => "Заказное убийство";
    }
}
