using System.Text;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PracticeАndСhallenges
{
    /// <summary>
    /// Просто решаю разные задачки
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task_1();
            //Task_2();
            //Task_3();
            //Task_3_1();
            //Task_4();
            //Task_5();
            //Task_6();
            //Task_7();
            //Task_8();
            //Task_9();
            //Task_10();
            //Task_11();
            //Task_12();
            //Task_12Refactor();
            //Task_13();
            //Task_14();
            //Task_15();
            //Task_16();
            //Task_17();


        }


        /// <summary>
        /// генератор паролей 
        /// </summary>
        public static void Task_17()
        {
            Random rand = new Random();
            Console.Write("Ввод: ");
            int.TryParse(Console.ReadLine(), out int len);
            string password = "";

            for (int i = 0; i <= 12; i++)
            {
                password += (char)rand.Next(32, 128);
            }

            Console.WriteLine(password);
        }


        /// <summary>
        /// Палиндром
        /// </summary>
        public static void Task_16()
        {
            Console.Write("Ввод: ");
            string userInput = Console.ReadLine();
            string str = "";

            for (int i = userInput.Length - 1; i >= 0; i--)
            {
                str += userInput[i];
            }

            if (userInput.ToLower().Replace(" ", "") == str.ToLower().Replace(" ", ""))
            {
                Console.WriteLine($"Строка: {userInput} является палиндромом");
            }
            else
            { 
                Console.WriteLine($"Строка: \"{userInput}\" НЕ является палиндромом");
            }

        }

        /// <summary>
        /// "Шифр Цезаря" можно через %26 но сделал так
        /// </summary>
        public static void Task_15()
        {
            Console.WriteLine("Введите строку: ");
            string userInput = Console.ReadLine();
            int.TryParse(Console.ReadLine(), out int shift);

            foreach (var elem in userInput)
            {
                Console.Write((char)(elem+shift));    
            }

         //   Console.WriteLine($"{(int)'A' + 3} {(int)'D'}");
        }

        /// <summary>
        /// Пузырьковая сортировка
        /// </summary>
        public static void Task_14()
        {
            int[] numbers = { 64, 34, 25, 12, 22, 11, 90 };

            for (int i = 0; i < numbers.Length -1; i++)
            {
                for (int j = 0; j < numbers.Length - 1 - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    { 
                        var temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Средний балл
        /// </summary>
        public static void Task_13()
        {
            int bestScore = 0;
            int middleScore = 0;
            int sum = 0;
            string bestStudent = "";

            Dictionary<string, int> students = new()
            {
                ["Alex"] = 85,
                ["Eddie"] = 92,
                ["David"] = 78
            };

            foreach (var student in students)
            { 
                Console.WriteLine($"Имя: {student.Key} балл: {student.Value}");
                sum += student.Value;

                if (student.Value >= bestScore)
                { 
                    bestScore = student.Value;
                    bestStudent = student.Key;
                }
            }
            Console.WriteLine($"Средний балл: {sum/students.Count}");
            Console.WriteLine($"Лучший балл {bestScore} у {bestStudent}");
        }

        /// <summary>
        /// Рефакторинг 12 задачи. Список покупок 2.0.
        /// Просто разбиваю код на несколько небольших локальных функций.
        /// Данные в фукции не передаю, так-как область видимости позволяет 
        /// работать с shoppingList как с глобальным полем/>
        /// </summary>
        public static void Task_12Refactor()
        {
            // Список покупок
            List<string> shoppingList = new List<string>() { "Молоко", "Мясо", "Яйца" };
            // Ввод пользователя
            string userInput = "";

            do
            {
                ShowMenu();
                userInput = Console.ReadLine()?.Trim();

                switch (userInput)
                {
                    // добавить товар
                    case "1":
                        AddProduct();
                        continue;
                    // удалить товар
                    case "2":
                        DeleteProduct();
                        GetList();
                        continue;
                    // показать товары
                    case "3":
                        ShowList();
                        continue;
                    // выйти из программы
                    case "4":
                        break;
                }

            }while (userInput != "4");

            // показать меню
            void ShowMenu()
            {
                Console.WriteLine("1. Добавить товар\n2. Удалить товар\n3. Показать список\n4. Выход");
                Console.Write($"\nВведите: ");
            }

            // добавить товар
            void AddProduct()
            {
                Console.Write("Введите товар, который нужно добавить в список: ");
                foreach (var item in Regex.Replace(Console.ReadLine().ToLower(), @"[^\w\s]", "").Split(" "))
                {
                    if (item == "")
                    {
                        continue;
                    }
                    shoppingList.Add(item);
                }
            }
            
            // удалить товар
            void DeleteProduct()
            {
                while (shoppingList.Count > 0)
                {
                    Console.WriteLine("Чтобы выйти введите -1");
                    Console.Write("Введите товар, который хотите удалить: ");
                    int.TryParse(Console.ReadLine(), out int index);
                    // выход из цикла
                    if (index == -1)
                    {
                        break;
                    }
                    if (index > shoppingList.Count() - 1 || index < 0)
                    {
                        Console.WriteLine("Элемента с таким индексом нет");
                        continue;
                    }

                    shoppingList.RemoveAt(index);

                    GetList();
                }
            }

            // загрузить список
            void GetList()
            {
                Console.Clear();
                for (int i = 0; i < shoppingList.Count(); i++)
                {
                    Console.WriteLine($"{i}- {shoppingList[i]}");
                }
                Console.WriteLine();
            }

            // показать список
            void ShowList()
            {
                if (shoppingList.Count() > 0)
                {
                    GetList();
                }
                else
                {
                    Console.WriteLine("Список пуст! Добавте товар\n");
                }
            }
        }


        /// <summary>
        /// Список покупок
        /// Данная задача решена намеренно так. Главное, чтобы работала. 
        /// </summary>
        public static void Task_12()
        {
            // список покупок 
            List<string> shoppingList = new List<string>() {"Молоко", "Мясо", "Яйца" };
            string userInput = "";
            string message = "1. Добавить товар\n2. Удалить товар\n3. Показать список\n4. Выход";
            do
            {
                Console.Write($"{message}\nВведите: ");
                userInput = Console.ReadLine();
                // меню
                switch (userInput)
                {
                    // добавить элементы в масссив
                    case "1":
                        Console.Write("Введите товар, который нужно добавить в список: ");
                        // удаляем (/;*.) и т.п. и разбиваем строку на массив используя пробелы
                        ListFill();
                        continue;
                    // удалить элементы из массива
                    case "2":
                        GetList();
                        while (shoppingList.Count > 0)
                        {
                            Console.WriteLine("Чтобы выйти введите -1");
                            Console.Write("Введите товар, который хотите удалить: ");
                            int.TryParse(Console.ReadLine(), out int index);
                            // выход из цикла
                            if (index == -1)
                            {
                                break;
                            }
                            if (index > shoppingList.Count() - 1 || index < 0)
                            {
                                Console.WriteLine("Элемента с таким индексом нет");
                                continue;
                            }

                            shoppingList.RemoveAt(index);
                            
                            GetList();  
                        }
                        continue;
                    // показать список
                    case "3":
                        if (shoppingList.Count() > 0)
                        {
                            GetList();
                        }
                        else
                        { 
                            Console.WriteLine("Список пуст! Добавте товар\n");
                        }
                        continue;
                    // выход из программы
                    case "4":
                        Console.WriteLine("Прощайте!");
                        break;
                }
            } while (userInput != "4");

            void ListFill()
            {
                foreach (var item in Regex.Replace(Console.ReadLine().ToLower(), @"[^\w\s]", "").Split(" "))
                {
                    if (item == "")
                    {
                        continue;
                    }
                    shoppingList.Add(item);
                }
            }

            void GetList()
            {
                Console.Clear();
                foreach (var item in shoppingList)
                {
                    Console.WriteLine($"{shoppingList.IndexOf(item)}- {item}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Частота слов
        /// </summary>
        public static void Task_11()
        {
            Dictionary<string, int> words = new Dictionary<string, int>();
            Console.WriteLine("Ввод: ");
            string str = Console.ReadLine();
            string newWord = "";


            foreach (var word in str.Split(" ")) 
            { 
                newWord = Regex.Replace(word, @"[^\w\s]", "");

                if (newWord == "")
                {
                    continue;
                }

                if (words.ContainsKey(newWord))
                {
                    words[newWord] += 1;
                }
                else
                { 
                    words.Add(newWord, 1);
                }


            }

            foreach (var word in words) 
            { 
                Console.WriteLine($"{word.Key}: {word.Value}");
            }

        }


        /// <summary>
        /// Простые числа
        /// </summary>
        public static void Task_10()
        {
            for (int i = 2; i < 100; i++)
            {
                bool flag = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    Console.WriteLine(i);
                }
            }
        }

        /// <summary>
        /// Угадай число
        /// </summary>
        public static void Task_9()
        {
            Random rand = new Random();
            int secret = rand.Next(1, 101);
            int count = 0;
            do 
            {
                count++;
                Console.WriteLine($"Компьютер: Я загадал число 1 до 100");
                Console.Write("Ты: ");
                if (!int.TryParse(Console.ReadLine(), out int userRespons))
                {
                    Console.WriteLine("Ты ввел не число");
                    continue;
                }

                if (userRespons == secret)
                { 
                    break;
                }
                else if (userRespons > secret)
                {
                    Console.WriteLine("Меньше!");
                    continue;
                }
                else
                {
                    Console.WriteLine("Больше!");
                    continue;
                }

            } while (true);
            
            Console.WriteLine($"Тебе потребовалось {count} попыток");
        }

        /// <summary>
        /// Треугольник из звезд
        /// </summary>
        public static void Task_8()
        {
            Console.Write("Ввод: ");
            int.TryParse(Console.ReadLine(), out int height);

            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Подсчет букв
        /// </summary>
        public static void Task_7()
        {
            Console.Write("Ввод: ");
            string userInput = Console.ReadLine();
            int count = 0;

            char[] allVowels = { 
                    // English
                    'a','e','i','o','u','A','E','I','O','U',
                    // Russian  
                    'а','е','ё','и','о','у','ы','э','ю','я',
                    'А','Е','Ё','И','О','У','Ы','Э','Ю','Я'
            };



            foreach (var letter in userInput)
            {
                count += allVowels.Contains(letter) ? 1 : 0;
            }

            Console.WriteLine(count);
        }

        /// <summary>
        /// Переворот массива
        /// </summary>
        public static void Task_6()
        {
            string[] names = { "Alex", "Eddie", "David", "Michael"};

            for (int i = names.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(names[i]);
            }
        }

        /// <summary>
        /// Фильтр четных
        /// </summary>
        public static void Task_5()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            List<int> evenNumbers = new List<int>();

            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                { 
                    evenNumbers.Add(number);
                }
            }

            foreach (var eNumber in evenNumbers)
            {
                Console.Write(eNumber + " ");
            }
        }

        /// <summary>
        /// Поиск максимума
        /// </summary>
        public static void Task_4()
        {
            int[] numbers = { 3, 7, 2, 9, 1, 5 };
            int max = numbers[0];

            foreach (var num in numbers)
            {
                max = num > max ? num : max;
            }

            Console.WriteLine(max); 

        }

        /// <summary>
        /// Таблица умножения полная
        /// </summary>
        public static void Task_3_1()
        {
            for (int i = 1; i < 10; i++)
            {
                Console.Write($"{i}|");   
                for (int j = 1; j < 10; j++)
                {
                    Console.Write($" {j*i,3} ");
                }
                Console.WriteLine("|");
            }
        }

        /// <summary>
        /// Обратный отсчет
        /// </summary>
        public static void Task_3()
        {
            for (int i = 10; i > 0; i--)
            { 
                Console.WriteLine(i);
            }
            Console.WriteLine("Поехали!");
        }

        /// <summary>
        /// Таблица умножения простая
        /// </summary>
        public static void Task_2()
        {
            Console.Write("Ввод: ");
            if (!int.TryParse(Console.ReadLine(), out int userInput) || (userInput <= 0 || userInput > 10))
            {
                Console.WriteLine("Неверный ввод!");
            }

            Console.WriteLine("Вывод: ");

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{userInput} x {i} = {userInput*i}");
            }
        }

        /// <summary>
        /// Сумма чисел
        /// </summary>
        public static void Task_1()
        {
            int sum = 0;

            int.TryParse(Console.ReadLine(), out int n);

            Console.WriteLine($"Введенное число: {n}");

            for (int i = 10; n > 0;)
            {
                Console.WriteLine($"n: {n}; i: {i}; n/i: {n}/{i} = {n / i};");

                sum += n % i;
                Console.WriteLine($"- {sum} += ({n} - ({n} / {i})*{i}) = {(n - (n / i) * i)}");
                n /= i;

                Thread.Sleep(1000);
            }

            Console.WriteLine($"Sum: {sum}");
        }
    }
}
