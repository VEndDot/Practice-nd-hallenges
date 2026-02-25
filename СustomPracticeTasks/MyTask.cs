using System.Security.Cryptography.X509Certificates;

namespace СustomPracticeTasks
{
    /// <summary>
    /// Выводит задачи. Моя практика, решение задач и т.п.
    /// Данные задачки получились кривыми. Хочу пока отдохнуть от задач
    /// и просто поделать рпг и посмотреть лекции.
    /// Потом вернусь к решению задач
    /// </summary>
    public static class MyTask
    {


        public static void LuckyTicket()
        {
            int count = 0;
            int sum1 = 0;
            int sum2 = 0;
            int digit1 = 0;
            int digit2 = 0;
            int n1 = 0;
            int n2 = 0;

            for (int i = 0; i <= 999999; i++)
            {
                n1 = i % 1000;
                n2 = i / 1000;
                digit1 = 0;
                digit2 = 0;
                sum1 = 0;
                sum2 = 0;

                while (n1 != 0 || n2 != 0)
                {
                    digit1 = n1 % 10;
                    n1 /= 10;
                    sum1 += digit1;

                    digit2 = n2%10;
                    n2 /= 10;
                    sum2 += digit2;
                }

                if (sum1 == sum2)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        /// <summary>
        /// Числа Армстронга
        /// Число Армстронга — это число, которое равно сумме своих цифр,
        /// возведённых в степень количества цифр
        /// </summary>
        public static void ArmstrongNumbers()
        {
            Console.Write("Введите N: ");
            // диапазон поиска чисел Армстронга
            int.TryParse(Console.ReadLine(), out int userRange);
            Console.Write("Числа Армстронга: ");
            string message = "";
            // каждая i - это отдельное число которое надо проверить
            for (int i = 0; i <= userRange; i++)
            {
                // сохраняем в новой переменной, чтобы не изменять оригинал
                int number = i;
                // сохраняет сумму цифр числа. Нужна для нахождения числа Армстронга
                int sum = 0;
                // степень числа исходя из его цифр. Увеличивается с каждой итерацией while
                int digitCount = 0;

                // считает количество цифр
                while (number != 0)
                { 
                    digitCount++;
                    number /= 10;
                }
                number = i;

                while (number != 0) 
                {
                    // Достаем цифру из числа
                    int digit = number % 10;
                    // хранит степень числа
                    int power = 1;
                    for (int j = 0; j < digitCount; j++)
                    {
                        power *= digit;
                    }

                    sum += power;
                    number /= 10;
                }

                if (i == sum && i > 0)
                {
                    message += $"{i}, ";
                }
            }
            Console.WriteLine(message.Remove(message.Length - 2));
        }

        /// <summary>
        /// Поиск простых чисел
        /// </summary>
        public static void SimpleNumber()
        {
            Console.Write("Введитe N: ");
            int.TryParse(Console.ReadLine(), out int n);

            for (int i = 2; i <= n; i++)
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
                    Console.Write($"{i,3}");
                }
            }

        }

        /// <summary>
        /// Рисование прямоугольника
        /// </summary>
        public static void DrawRectangle()
        {
            Console.Write("Введи ширину: ");
            int.TryParse(Console.ReadLine(), out int width);
            Console.Write("Введи высоту: ");
            int.TryParse(Console.ReadLine(), out int height);

            if (width > 0 && height > 0)
            {
                for(int i = 1; i <= height; i++)
                {
                    Console.Write("*");
                    for (int j = 1; j < width - 1; j++)
                    {
                        if (i == 1 || i == height)
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.Write("*");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Неправильный ввод");
            }

        }

        /// <summary>
        /// Угадай число (с подсчётом попыток)
        /// </summary>
        public static void GuessNumber()
        {
            Random rand = new Random();
            int secretNumber = rand.Next(1, 101);
            int attempts = 0;
            int number = 0;
            do
            {
                Console.Write("Введите число от 1 до 100: ");
                if (int.TryParse(Console.ReadLine(), out int userNumber) && !(userNumber < 1 || userNumber >= 101))
                {
                    attempts++;
                    number = userNumber;
                    if (number > secretNumber)
                    {
                        Console.WriteLine("Меньше");
                    }
                    else
                    {
                        Console.WriteLine("Больше");
                    }
                }
                else
                {
                    Console.WriteLine($"Неправильный ввод");
                }
            } while (secretNumber != number);

            Console.WriteLine($"Секретное число: {secretNumber}");
        }

        /// <summary>
        /// Сумма цифр числа
        /// </summary>
        public static void SumDigitsNumber()
        {
            Console.Write("Введите число: ");
            int.TryParse(Console.ReadLine(), out int number);
            int saveNumber = number;
            int sum = 0;

            while (number != 0)
            {
                sum += number % 10;
                number /= 10;
            }
            Console.WriteLine($"Сумма числа {saveNumber} = {sum}");  
        }
         
        /// <summary>
        /// Таблица умножения. Ввод числа. Вывод таблицы умножения до этого числа
        /// </summary>
        public static void TheMatrixOfMultiplication()
        {
            Console.Write("Введите число от 1 до 9: ");
            

            if (int.TryParse(Console.ReadLine(), out int number) && (number >= 1 && number <= 9))
            {
                for (int i = 1; i <= number; i++)
                {
                    Console.Write(i);
                    for (int j = 1; j <= 10; j++)
                    {
                        Console.Write($"{i * j,3}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Ввод неверный");
            }
        }

    }
}
