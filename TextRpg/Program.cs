using System;
using System.Drawing;
using System.Text;
/*
 Не забыть по ShowAnimation 
 Можно использовать:
 - Для отдыха
 - Для похода
 - Для загрузки мира
 - Для вражеской атаки
!Добавить возможность использовать разные символы
...
 */

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
    /// Предметы в игре
    /// </summary>
    enum Item
    { 
        /// <summary>
        /// Зелье лечения
        /// </summary>
        HealPotion,
        /// <summary>
        /// Зелье маны
        /// </summary>
        ManaPotion,
        /// <summary>
        /// Ядовитая стрела
        /// </summary>
        VenomArrow
    }

    /// <summary>
    /// Данная программа представляет текстовую RPG игру. Пока план не использовать ООП.
    /// Также, хочу создать отдельный репозиторий для нее.
    /// Но пока разрабатываю тут.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Лечит персонажа во время отдыха
        /// </summary>
        public const int REST_HEAL_AMOUNT = 20;

        /// <summary>
        /// Хранит статы классов. Участвует в инициалзизации 
        /// </summary>
        public static Dictionary<CharacterClass,
            (int hp, int mp, int power, int agility, int intelligence, (int min, int max) damage)>
            classStats = new()
            {
                [CharacterClass.Warrior] = (120, 0, 10, 5, 3, (8,12)),
                [CharacterClass.Mage] = (80, 100, 3, 5, 10, (4,6)),
                [CharacterClass.Archer] = (90, 0, 5, 10, 4, (6, 10))
            };

        /// <summary>
        /// Активности игры
        /// </summary>
        public static bool FlagGameActive = true;

        /// <summary>
        /// Генерирует псевдо-случайные числа
        /// </summary>
        public static readonly Random _rand = new Random();

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
        public static Dictionary<Item, int> CharacterInventory = new Dictionary<Item, int>();

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
                    // Если нажата 5 - герой идет отдыхать
                    case ConsoleKey.D5:
                        GoToRest();
                        break;
                    // Если нажата 6 - выходим из игры
                    case ConsoleKey.D6:
                        FlagGameActive = false;
                        Console.Clear();
                        MessageWithGoodBye();
                        break;
                }
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
        /// TODO: DEMO
        /// </summary>
        public static void MessageWelcome()
        {
            string message = @" \|/ Странник...";
            //ColorMessage("Добро пожаловать в Мир Тенебрис!\n", ConsoleColor.DarkMagenta);
            ShowAnimationMessage(message.Length, message, 100, 500);
            Thread.Sleep(1000);
            message = "В тени Тенебрис поет волк серинаду ночьную...";
            ShowAnimationMessage(message.Length, message);
            Thread.Sleep(1000);
            message = "\nдля мертвого странника, безвестного трупа...";
            ShowAnimationMessage(message.Length, message);
            Thread.Sleep(800);
            message = "\nи где-то в туманных лесах...";
            ShowAnimationMessage(message.Length, message);
            message = "\nШепотом молвит судьба...";
            ShowAnimationMessage(message.Length, message);
            message = "\nПроснись!";
            ShowAnimationMessage(message.Length, message, 100, 500);

            Thread.Sleep(1500);
            Console.Clear();
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
            ColorMessage("Good Luck My Friend)", (ConsoleColor)_rand.Next(1, 15));
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
                    //ShowAnimation(61, '#');
                    return;
                }

                ColorMessage("Неверный выбор! Введи число 1-3 или ESC.", ConsoleColor.Red, textTransfer:true);

            }
        }

        /// <summary>
        /// Имитирует загрузку мира, локации, отдыха, атаки врага
        /// Можно удалить. Просто для красоты
        /// </summary>
        /// <param name="loadTime">Данный параметр просто отвечает за длительность загрузки. Чем меньше значение, тем меньше загрузка</param>
        public static void ShowAnimation(int loadTime, char symbol)
        {
            Console.CursorVisible = false;
            for (int i = 0; i < loadTime; i++)
            {
                Console.Write(symbol);
                Thread.Sleep(_rand.Next(1, 80));

                // игнорирует все нажатия во время имитации загрузки
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }
            Console.WriteLine();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Анимирует выводящие сообщения для пользователя
        /// TODO: DEMO
        /// </summary>
        /// <param name="loadTime"></param>
        /// <param name="str"></param>
        /// <param name="minDelay"></param>
        /// <param name="maxDelay"></param>
        public static void ShowAnimationMessage(int loadTime, string str, int minDelay = 1, int maxDelay = 90)
        {

            Console.CursorVisible = false;
            for (int i = 0; i < loadTime; i++)
            {
                Console.Write(str[i]);
                Thread.Sleep(_rand.Next(minDelay, maxDelay));
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

            // Если инвентарь пуст
            if (!CharacterInventory.Any())
            {
                ColorMessage("Инвентарь пуст", ConsoleColor.Green);
            }
            // Если не пуст, то показывает его
            else
            {
                foreach (var item in CharacterInventory)
                {
                    Console.WriteLine($"- {GetItemRuName(item.Key)}: {item.Value} шт");
                }
            }
        }

        /// <summary>
        /// Изменяет имя перечисления <see cref="Item"/> на более подходящее русское для пользователя
        /// </summary>
        /// <param name="item">Получает перечисление</param>
        /// <returns>Возвращает название предмета на русском</returns>
        public static string GetItemRuName(Item item)
        {
            switch (item) 
            {
                case Item.HealPotion:
                    return "Лечебное зелье";
                case Item.ManaPotion:
                    return "Зелье маны";
                case Item.VenomArrow:
                    return "Ядовитая стрела";
                default:
                    return "Неизвестный предмент/ Обратитесь в GetItemRuName";
            }
        }

        /// <summary>
        /// Инициализирует характеристики для выбранного класса
        /// Создает инвентарь классу
        /// </summary>
        public static void InitializeClassCharacteristics(int playerClass)
        {
            // Инициализируем класс персонажа
            CharacterClass = (CharacterClass)playerClass;
            // Просим добавить имя персонажу
            SetNameForCharacter();
            // Инициализируем статы персонажа
            InitializingStats(CharacterClass);
            // инициализируем начальный инвентарь персонажа
            InitializingInventory(CharacterClass);
        }

        /// <summary>
        /// Инициализирует начальный инвентарь для класса
        /// </summary>
        /// <param name="characterClass">Класс создаваемого персонажа</param>
        public static void InitializingInventory(CharacterClass characterClass) 
        {
            // Это дефолтный кейс.
            switch (characterClass)
            {
                case CharacterClass.Warrior:
                    CharacterInventory[Item.HealPotion] = 3;
                    break;
                case CharacterClass.Mage:
                    CharacterInventory[Item.HealPotion] = 1;
                    CharacterInventory[Item.ManaPotion] = 2;
                    break;
                case CharacterClass.Archer:
                    CharacterInventory[Item.HealPotion] = 2;
                    CharacterInventory[Item.VenomArrow] = 1;
                    break;
            }
        }

        /// <summary>
        /// Устанавливает статы классу
        /// </summary>
        public static void InitializingStats(CharacterClass characterClass)
        {
            var stats = classStats[characterClass];

            CharacterHP = stats.hp;
            CharacterMP = stats.mp;
            CharacterPower = stats.power;
            CharacterAgility = stats.agility;
            CharacterIntelligence = stats.intelligence;
            CharacterDamage = _rand.Next(stats.damage.min, stats.damage.max + 1);
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
            // TODO
        }

        /// <summary>
        /// Вход в город
        /// </summary>
        public static void GoToTown()
        {
            Console.Clear();
            Console.Write("Ты в городе... (тут будет магазин)");
            // TODO
        }

        /// <summary>
        /// Тут герой отдыхает
        /// </summary>
        public static void GoToRest()
        {
            Console.Clear();
            ColorMessage("Ты решил отдохнуть... (тут востановится здоровье)", ConsoleColor.Gray);
            int maxHP = GetMaxHPForClass(CharacterClass);

            // Если у игрока здоровья достаточно, то сообщаем об этом и ничего не делаем
            if (CharacterHP >= maxHP)
            {
                ColorMessage("Ты уже здоров...", ConsoleColor.Green);
            }
            // иниче, увеличиваем здоровье игрока на константу
            else
            {
                // не даст выйти за предел максимального здоровья
                CharacterHP = Math.Min(CharacterHP + REST_HEAL_AMOUNT, maxHP);
                MessageForRest();
            }
        }
       
        /// <summary>
        /// Позволяет получить значение максимального здоровья для каждого класса
        /// </summary>
        /// <param name="characterClass">Принимает класс персонажа</param>
        /// <returns>Взвращает максимальное здоровье класса</returns>
        public static int GetMaxHPForClass(CharacterClass characterClass)
        {
            return classStats[characterClass].hp;
        }

        /// <summary>
        /// Выводит информации об отдыхе
        /// </summary>
        public static void MessageForRest()
        {
            Console.Write("Твое здоровье увеличино на: ");
            ColorMessage($"{REST_HEAL_AMOUNT}", ConsoleColor.Green);
            ColorMessage("Набирайся сил для битвы!", ConsoleColor.Yellow);
            ShowAnimation(61, 'Z');
        }
    }
}
