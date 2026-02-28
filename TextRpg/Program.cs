using System;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace TextRpg
{
    /// <summary>
    /// Три типа героя.
    /// 1. Воин 
    /// 2. Маг
    /// 3. Лучник
    /// </summary>
    enum CharacterClass
    {
        Warrior = 1,
        Mage,
        Archer
    }

    /// <summary>
    /// Данная программа представляет текстовую RPG игру. Пока план не использовать ООП.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Активности игры
        /// </summary>
        public static bool FlagGameActive = true;

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public static string? CharacterName { get; set; }
        /// <summary>
        /// Здоровье персонажа
        /// </summary>
        public static int CharacterHP { get; set; }
        /// <summary>
        /// Мана персонажа
        /// </summary>
        public static int CharacterMP { get; set; }
        /// <summary>
        /// Сила персонажа
        /// </summary>
        public static int CharacterPower { get; set; }
        /// <summary>
        /// Ловкость персонажа
        /// </summary>
        public static int CharacterAgility { get; set; }
        /// <summary>
        /// Интеллект персонажа
        /// </summary>
        public static int CharacterIntelligence { get; set; }
        /// <summary>
        /// Урон персонажа
        /// </summary>
        public static int CharacterDamage { get; set; }
        /// <summary>
        /// Класс персонажа
        /// </summary>
        public static CharacterClass CharacterClass { get; set; }


        static void Main(string[] args)
        {
            // Создаем персонажа
            CreateCharacter();

            // Главыный цикл игры
            while (FlagGameActive)
            {
                ShowCharacter();
                Console.ReadKey(true);
            }
        }


        /// <summary>
        /// Приветствует игрока и предлагает выбрать класс
        /// </summary>
        public static void CreateCharacter()
        {
            MessageWelcome();
            CharacteristicsOfClasses();
            ShowAllClasses();
            ChooseCharacterClass();
        }

        /// <summary>
        /// Показывает все классы
        /// </summary>
        public static void ShowAllClasses()
        {
            ColorMessage("Доступные классы на данный момент:", ConsoleColor.Blue);
            foreach (var characterClass in Enum.GetValues(typeof(CharacterClass)))
            {
                Console.WriteLine($"№{(int)characterClass} - {GetClassNameRU((CharacterClass)characterClass)}");
            }
        }

        /// <summary>
        /// Отправляет названия классов на русском
        /// </summary>
        /// <param name="characterClass">Получает элемент перечисления</param>
        /// <returns>Возвращает стоку класса на русском</returns>
        public static string GetClassNameRU(CharacterClass characterClass)
        {
            switch (characterClass) 
            { 
                case CharacterClass.Warrior:
                    return "Воин";
                case CharacterClass.Archer:
                    return "Лучник";
                case CharacterClass.Mage:
                    return "Маг";
                default:
                    return "Неизвестно";
            }
        }

        /// <summary>
        /// Сообщение приветствия
        /// </summary>
        public static void MessageWelcome()
        { 
            ColorMessage("Добро пожаловать в Мир Тенебрис!\n", ConsoleColor.DarkMagenta);
        }

        /// <summary>
        /// Сообщает о том, как выбрать класс или выйти из игры
        /// </summary>
        public static void MessageForChoose()
        {
            Console.Write("\nДля продолжения, введи номер класса");
            ColorMessage("(или ESC для выхода): ", ConsoleColor.Yellow, textTransfer: true);
        }

        /// <summary>
        /// Сообщение с прощанием призентуемое игроку
        /// </summary>
        public static void MessageWithGoodBye()
        {
            Random rand = new Random();
            ColorMessage("Good Luck My Friend)", (ConsoleColor)rand.Next(1, 15));
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Выводит выбранный пользователем класс персонажа
        /// </summary>
        /// <param name="classNumber">Целое число, соответствует номеру перечисления <see cref="CharacterClass"/></param>
        public static void ClassSelectionMessage(int classNumber)
        {
            ColorMessage($"{GetClassNameRU((CharacterClass)classNumber)}", ConsoleColor.Green);
        }

        /// <summary>
        /// Выбор пользователем класса персонажа
        /// </summary>
        public static void ChooseCharacterClass()
        {
            // Выполнять, пока пользователь не введет корректные данные
            // Или не решит закончить игру
            while (true)
            {
                MessageForChoose();

                // Проверяет нажатие ESC для выхода из игры
                var key = Console.ReadKey(true);

                // Выход из программы
                if (key.Key == ConsoleKey.Escape)
                {
                    FlagGameActive = false;
                    MessageWithGoodBye();
                    return;
                }

                // Инициализируем персонажа и выходим из создания персонажа
                // Если введенный символ преобразуется в int и если он соответствует значению из перечисления
                // то инициализируем и выходим
                if (int.TryParse(key.KeyChar.ToString(), out int classNumber) && Enum.IsDefined(typeof(CharacterClass), classNumber))
                {
                    ClassSelectionMessage(classNumber);
                    InitializeClassCharacteristics(classNumber);
                    return;
                }

                ColorMessage("Неверный выбор! Введи число 1-3 или ESC.", ConsoleColor.Red, textTransfer:true);

            }
        }

        /// <summary>
        /// Проверка введенных данных пользователем
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static bool IsValidUserInput(int userInput)
        {
            // TODO
            return true;
        }

        /// <summary>
        ///  Показывает характеристики классов (воин, маг, лучник)
        /// </summary>
        public static void CharacteristicsOfClasses()
        {
            ColorMessage("Характеристики классов:", ConsoleColor.DarkYellow);
            ColorMessage("Класс  |  HP |  MP |  Сила |  Ловкость   |  Интеллект | Урон", ConsoleColor.Cyan);
            ColorMessage("Воин   | 120 |  0  |   10  |      5      |      3     | 8-12", ConsoleColor.Red);
            ColorMessage("Маг    |  80 | 100 |    3  |      5      |     10     | 4-6", ConsoleColor.Blue);
            ColorMessage("Лучник |  90 |   0 |    5  |     10      |      4     | 6-10", ConsoleColor.Green);

            Console.WriteLine();
        }

        /// <summary>
        /// Задает цвет сообщению
        /// </summary>
        /// <param name="message">Текстовое сообщение</param>
        /// <param name="textTransfer">Определяет, будет ли переход на новую строку. Дефолт <see cref="false"/></param>
        /// <param name="first">Цвет на который изменить сообщение</param>
        /// <param name="last">Цвет текста консоли после изменения. Желательно оставить <see cref="ConsoleColor.White"/></param>
        public static void ColorMessage(string message,  ConsoleColor first,  ConsoleColor last = ConsoleColor.White, bool textTransfer = false)
        {
            Console.ForegroundColor = first;
            if (textTransfer)
                Console.Write(message);
            else
                Console.WriteLine(message);
            Console.ForegroundColor = last;
        }

        /// <summary>
        /// Выводит главное меню. (дествия игрока в игре)
        /// </summary>
        public static void MainMenu()
        {
            Console.Write(
                "1. Идти в лес (случайная встреча)\n" +
                "2. Зайти в город\n" +
                "3. Посмотреть персонажа\n" +
                "4. Посмотреть инвентарь\n" +
                "5. Отдохнуть\n" +
                "6. Выход");
        }

        /// <summary>
        /// Показывает информацию о персонаже
        /// </summary>
        public static void ShowCharacter()
        {
            //Console.WriteLine(
            //    $"Имя: {CharacterName}\n" +
            //    $"Класс: {GetClassNameRU(CharacterClass)}\n" +
            //    $"Здоровье: {CharacterHP}\n" +
            //    $"Мана: {CharacterMP}\n" +
            //    $"Урон: {CharacterDamage}\n" +
            //    $"Сила: {CharacterPower} | Ловкость: {CharacterAgility} | Интеллект: {CharacterIntelligence}"
            //    );

            Console.Write($"Имя: {CharacterName}\n");
            Console.Write($"Класс: {GetClassNameRU(CharacterClass)}\n");
            ColorMessage("Здоровье: ", ConsoleColor.Green, textTransfer: true);
            Console.Write($"{CharacterHP}\n");
            ColorMessage("Мана: ", ConsoleColor.Blue, textTransfer: true);
            Console.Write($"{CharacterMP}\n");
            ColorMessage("Урон: ", ConsoleColor.Red, textTransfer: true);
            Console.Write($"{CharacterDamage}\n");
        }

        /// <summary>
        /// Показывает инвентарь персонажа
        /// </summary>
        public static void ShowInventory()
        {
            // TODO
        }

        /// <summary>
        /// Инициализирует характеристики для выбранного класса
        /// </summary>
        public static void InitializeClassCharacteristics(int playerClass)
        {
            // Инициализируем класс персонажа
            CharacterClass = (CharacterClass)playerClass;
        }


    }
}
