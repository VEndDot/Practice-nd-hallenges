using System;
using System.Text;

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
    /// Также, хочу создать отдельный репозиторий для нее.
    /// Но пока разрабатываю тут.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Активности игры
        /// </summary>
        public static bool FlagGameActive = true;

        /// <summary>
        /// Генерирует псевдо-случайные числа
        /// </summary>
        public static Random rand = new Random();

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
        
        /// <summary>
        /// Инвентарь персонажа
        /// </summary>
        public static Dictionary<string, int> CharacterInventory = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            // Создаем персонажа
            CreateCharacter();

            // Главыный цикл игры
            while (FlagGameActive)
            {
                Console.Clear();
                MainMenu();
                Console.CursorVisible = false;

                switch (Console.ReadKey(true).Key)
                {
                    // Если нажата 1 - игрок идет в лес
                    case ConsoleKey.D1:
                        GoToForest();
                        break;
                    // Если нажата 2 - игрок идет в город
                    case ConsoleKey.D2:
                        GoToTown();
                        break;
                    // Если нажата 3 - показывает информацию о персонаже
                    case ConsoleKey.D3:
                        ShowCharacter();
                        break;
                    // Если нажата 4 - показывает инвентарь персонажа
                    case ConsoleKey.D4:
                        ShowInventory();
                        break;
                    // Если нажата 5 - 
                    case ConsoleKey.D5:
                        GoToRest();
                        break;
                    case ConsoleKey.D6:
                        FlagGameActive = false;
                        Console.Clear();
                        MessageWithGoodBye();
                        break;

                }

                //ShowCharacter();
                //ShowInventory();
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

                // Регистрирует нажатую пользователем клавишу
                var key = Console.ReadKey(true);

                // Проверяет нажатие ESC для выхода из игры
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
                    //LoadWorld(61);
                    return;
                }

                ColorMessage("Неверный выбор! Введи число 1-3 или ESC.", ConsoleColor.Red, textTransfer:true);

            }
        }

        /// <summary>
        /// Имитирует загрузку мира, локации
        /// Можно удалить. Просто для красоты
        /// </summary>
        /// <param name="loadTime">Данный параметр просто отвечает за длительность загрузки. Чем меньше значение, тем меньше загрузка</param>
        public static void LoadWorld(int loadTime)
        {
            Console.CursorVisible = false;   
            for (int i = 0; i < loadTime; i++)
            {
                Console.Write("#");
                Thread.Sleep(rand.Next(1, 80));

                // игнорирует все нажатия во время имитации загрузки
                while (Console.KeyAvailable)
                    Console.ReadKey(true);

            }
            Console.WriteLine();
            Console.CursorVisible = true;
        }

        /// <summary>
        ///  Показывает характеристики классов (воин, маг, лучник)
        /// </summary>
        public static void CharacteristicsOfClasses()
        {
            ColorMessage("Характеристики классов:", ConsoleColor.DarkYellow);
            ColorMessage("Класс  |  HP |  MP |  Сила |  Ловкость   |  Интеллект | Урон", ConsoleColor.Cyan);
            ColorMessage("Воин   | 120 |   0 |   10  |      5      |      3     | 8-12", ConsoleColor.Red);
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
                "5. Отдохнуть (восстановить здоровье)\n" +
                "6. Выход");
        }

        /// <summary>
        /// Показывает информацию о персонаже
        /// </summary>
        public static void ShowCharacter()
        {
            Console.Clear();
            Console.Write($"Имя: {CharacterName}\n");
            Console.Write($"Класс: {GetClassNameRU(CharacterClass)}\n");
            ColorMessage("Здоровье: ", ConsoleColor.Green, textTransfer: true);
            Console.Write($"{CharacterHP}\n");
            ColorMessage("Мана: ", ConsoleColor.Blue, textTransfer: true);
            Console.Write($"{CharacterMP}\n");
            ColorMessage("Урон: ", ConsoleColor.Red, textTransfer: true);
            Console.Write($"{CharacterDamage}\n");
            ColorMessage("Статы героя: ", ConsoleColor.Gray, textTransfer: true);
            Console.Write($"Сила: {CharacterPower} | Ловкость: {CharacterAgility} | Интеллект: {CharacterIntelligence}\n");
        }

        /// <summary>
        /// Показывает инвентарь персонажа
        /// </summary>
        public static void ShowInventory()
        {
            Console.Clear();
            ColorMessage("Инвентарь героя", ConsoleColor.Cyan);
            foreach (var item in CharacterInventory)
            { 
                Console.WriteLine($"- {item.Key}: {item.Value} шт");
            }
        }

        /// <summary>
        /// Начальный инвентарь воина
        /// </summary>
        public static void CreateWarriorInventory()
        {
            CharacterInventory = new() {
                ["Лечебное зелье"] = 3,
            };
        }

        /// <summary>
        /// Начальный инвентарь мага
        /// </summary>
        public static void CreateMageInventory()
        {
            CharacterInventory = new()
            {
                ["Лечебное зелье"] = 1,
                ["Зелье маны"] = 2,
            };
        }

        /// <summary>
        /// Начальный инвентарь лучника
        /// </summary>
        public static void CreateArcherInventory()
        {
            CharacterInventory = new()
            {
                ["Лечебное зелье"] = 2,
                ["Отравленная стрела"] = 1,
            };
        }

        /// <summary>
        /// Инициализирует характеристики для выбранного класса
        /// Создает инвентарь классу
        /// </summary>
        public static void InitializeClassCharacteristics(int playerClass)
        {
            // Инициализируем класс персонажа
            CharacterClass = (CharacterClass)playerClass;

            switch (CharacterClass)
            {
                case CharacterClass.Warrior: 
                    InitializingWarriorStats();
                    CreateWarriorInventory();
                    break;
                case CharacterClass.Mage: 
                    InitializingMageStats();
                    CreateMageInventory();
                    break;
                case CharacterClass.Archer: 
                    InitializingArcherStats();
                    CreateArcherInventory();
                    break;
            }
        }


        /// <summary>
        /// Инициализация стат класса воин
        /// </summary>
        public static void InitializingWarriorStats()
        {
            /*
                Класс  |  HP |  MP |  Сила |  Ловкость   |  Интеллект | Урон
                Воин   | 120 |   0 |   10  |      5      |      3     | 8-12
            */
            int damage = rand.Next(8, 13);
            Stats(120, 0, 10, 5, 3, damage);
        }

        /// <summary>
        /// Инициализация стат класса маг
        /// </summary>
        public static void InitializingMageStats()
        {
            /*
                Класс  |  HP |  MP |  Сила |  Ловкость   |  Интеллект | Урон
                Маг    |  80 | 100 |    3  |      5      |     10     | 4-6
            */

            int damage = rand.Next(4, 7);
            Stats(80, 100, 3, 5, 10, damage);
        }

        /// <summary>
        /// Инициализация стат класса лучник
        /// </summary>
        public static void InitializingArcherStats()
        {
            /*
                Класс  |  HP |  MP |  Сила |  Ловкость   |  Интеллект | Урон
                Лучник |  90 |   0 |    5  |     10      |      4     | 6-10
            */

            int damage = rand.Next(6, 10);
            Stats(90, 0, 5, 10, 4, damage);
        }

        /// <summary>
        /// Статы классов
        /// </summary>
        public static void Stats(int health, int mana, int power, int agility, int intelligence, int damage)
        {
            SetNameForCharacter();
            CharacterHP = health;
            CharacterMP = mana;
            CharacterPower = power;
            CharacterAgility = agility;
            CharacterIntelligence = intelligence;
            CharacterDamage = damage;
        }

        /// <summary>
        /// Задает имя персонажу
        /// </summary>
        /// <returns>Возвращает строку с именем</returns>
        public static void SetNameForCharacter()
        {
            // В данный момент, имя может содержать цифры. Пока решил оставить так
            string heroName = "Неизвестно";
            Console.Write("Введите имя героя: ");
            while (true)
            {
                heroName = Console.ReadLine().Trim();
 
                if (string.IsNullOrWhiteSpace(heroName))
                {
                    ColorMessage($"Имя не может быть пустым, повторите ввод: ", ConsoleColor.Red, textTransfer: true);
                    continue;
                }
                break;
            }
            CharacterName = heroName;
        }

        /// <summary>
        /// Поход в лес
        /// </summary>
        public static void GoToForest()
        {
            Console.Clear();    
            Console.Write("Ты идешь в лес... (тут будет бой)");
        }

        /// <summary>
        /// Вход в город
        /// </summary>
        public static void GoToTown()
        {
            Console.Clear();
            Console.Write("Ты в городе... (тут будет магазин)");
        }

        /// <summary>
        /// Тут герой отдыхает
        /// </summary>
        public static void GoToRest()
        {
            Console.Clear();
            Console.Write("Ты решил отдохнуть... (тут востановится здоровье)");
        }
    }
}
