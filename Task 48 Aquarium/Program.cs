using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task_48_Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.StartGame();
        }
    }

    class Fish
    {
        public string Color { get; private set; }
        public int Health { get; private set; }

        public Fish(Random random)
        {
            Color = AssignRandomColor(random);
            Health = 100;
        }

        public void ReduceLife()
        {
            int amountReduction = 10;
            Health -= amountReduction;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Color} рыбка. Жизнь - {Health}");
        }

        private string AssignRandomColor(Random random)
        {
            int minLimit = 0;
            List<string> colors = new List<string>();
            colors.Add("Жёлтая");
            colors.Add("Синяя");
            colors.Add("Красная");
            colors.Add("Белая");

            return colors[random.Next(minLimit, colors.Count)];
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes = new List<Fish>();
        private Random _random = new Random();

        public void StartGame()
        {
            bool isWork = true;

            while (isWork)
            {
                ShowInfo();
                ApplyOneCycleLife();

                switch (Console.ReadLine())
                {
                    case "1":
                        AddFish(_random);
                        break;

                    case "2":
                        DeleteFish();
                        break;

                    case "3":
                        isWork = false;
                        break;

                    default:
                        break;
                }
            }
        }

        private void AddFish(Random random)
        {
            _fishes.Add(new Fish(random));
        } 

        private void DeleteFish()
        {
            if (_fishes.Count > 0)
            {
                bool isWork = true;

                while (isWork)
                {
                    Console.WriteLine("Введите номер рыбки");
                    bool isNumber = int.TryParse(Console.ReadLine(), out int index);

                    if (isNumber == true && index - 1 < _fishes.Count && index > 0)
                    {
                        _fishes.RemoveAt(index - 1);
                        Console.WriteLine("Рыбка удалена!");
                        Thread.Sleep(700);
                        isWork = false;
                    }
                    else
                    {
                        Console.WriteLine("Неккоректный ввод");
                        Thread.Sleep(700);
                    }
                }
            }
            else
            {
                Console.WriteLine("В аквариуме нет рыб");
                Thread.Sleep(700);
            }
        }

        private void ShowInfo()
        {
            Console.Clear();
            Console.WriteLine("Ваш аквариум: ");

            if (_fishes.Count > 0)
            {
                for (int i = 0; i < _fishes.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    _fishes[i].ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("рыб пока нет");
            }

            Console.WriteLine("\n1. Добавить рыбу \n2. Убрать рыбу \n3. Выход");
        }

        private void ApplyOneCycleLife()
        {
            AgeAllFishes();
            DelteDeadFishes();
        }

        private void AgeAllFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].ReduceLife();
            }
        }

        private void DelteDeadFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].Health <= 0)
                {
                    _fishes.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
