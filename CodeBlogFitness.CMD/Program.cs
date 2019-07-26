using CodeBlogFitness.BL.Controller;
using System.Collections.Generic;
using System.Linq;
using System;
using CodeBlogFitness.BL.Model;
using System.Globalization;
using System.Resources;

namespace CodeBlogFitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Changing colors
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            #endregion

            var culture = CultureInfo.CreateSpecificCulture("ru-ru");
            var resourceManager = new ResourceManager("CodeBlogFitness.CMD.Languages.Messages", typeof(Program).Assembly);
            Console.WriteLine();
            Console.WriteLine(resourceManager.GetString("Hello", culture));
            Console.Write(resourceManager.GetString("EnterName", culture));
            Console.Write(" ");
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);
            var exerciseController = new ExerciseController(userController.CurrentUser);

            if (userController.IsNewUser)
            {
                Console.Write("Введите пол: ");
                var gender = Console.ReadLine();
                var birthDay = ParseToDateTime("дату рождения");
                var weight = ParseToDouble("Вес");
                var growth = ParseToDouble("Рост");
                userController.SetNewUserData(gender, birthDay, weight, growth);
            }
            Console.WriteLine(userController.CurrentUser);
            while (true)
            {
                Console.WriteLine("Введите что вы собираетесь сделать");
                Console.WriteLine("E - добавить прием пищи");
                Console.WriteLine("А - добавить выполненное упражнение");
                Console.WriteLine("Q - выход");
                Console.ForegroundColor = ConsoleColor.Black;
                var key = Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.CursorLeft -= 1;
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.E:
                        var product = EnterEating();
                        eatingController.Add(product.Food, product.Weight);

                        foreach (var item in eatingController.Eating.Foods)
                        {
                            Console.WriteLine($"\t{item.Key} - {item.Value}");
                        }
                        break;
                    case ConsoleKey.A:
                        var exercise = EnterExercise();
                        exerciseController.Add(exercise.Activity, exercise.Begin, exercise.Finish);
                        foreach (var item in exerciseController.Exercises)
                        {
                            Console.WriteLine($"\t{item.Activity} с {item.Start.ToShortTimeString()} до {item.Finish.ToShortTimeString()} ");
                        }
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }

                Console.ReadLine();
            }
        }

        private static (DateTime Begin, DateTime Finish, Activity Activity) EnterExercise()
        {
            Console.WriteLine("Введите название упражнения: ");
            var name = Console.ReadLine();
            var calorisePerMinute = ParseToDouble("Введите расход энергии в минуту: ");
            var begin = ParseToDateTime("время начала упражнения");
            var finish = ParseToDateTime("время конца упражнения");
            var activity = new Activity(name, calorisePerMinute);
            return (begin, finish, activity);

        }

        private static (Food Food, double Weight) EnterEating()
        {
            Console.Write("Введите имя продукта: ");
            var foodName = Console.ReadLine();
            //MakePosition(Console.CursorLeft,Console.CursorTop);
            var calories = ParseToDouble("калорийность");
            var proteins = ParseToDouble("белки");
            var fats = ParseToDouble("жиры");
            var carbohydrates = ParseToDouble("углеводы");
            var weight = ParseToDouble("вес порции");

            var food = new Food(foodName,calories, proteins, fats, carbohydrates);

            return (Food: food, Weight: weight);
            
        }

        private static DateTime ParseToDateTime(string value)
        {
            DateTime birthDay;
            while (true)
            {
                Console.Write($"Введите {value} (dd.mm.yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDay))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($" Неверный формат даты ");
                }
            }

            return birthDay;
        }

        private static double ParseToDouble(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($" Неверный формат поля {name} ");
                }
            }
        }
    }

}
