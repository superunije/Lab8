using Lab8.Models;

namespace Lab8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var storage = new TestStorage();

            while (true)
            {
                Console.WriteLine("\nСБОРНИК ТЕСТОВ");
                Console.WriteLine("1. Просмотр БД");
                Console.WriteLine("2. Добавить тест");
                Console.WriteLine("3. Удалить тест");
                Console.WriteLine("4. Запросы");
                Console.WriteLine("0. Выход");

                var choice = ReadInt("Выбор: ", true);

                switch (choice)
                {
                    case 1:
                        storage.Tests.ForEach(Console.WriteLine);
                        break;

                    case 2:
                        var idx = ReadInt("ID: ");
                        if (storage.Tests.Any(x => x.ID == idx))
                        {
                            Console.WriteLine("Ошибка: такой идентификатор уже существует");
                            break;
                        }

                        var test = new Test(
                            idx,
                            ReadString("Название: "),
                            ReadString("Предмет: "),
                            ReadInt("Кол-во вопросов: "),
                            ReadInt("Макс. балл: "),
                            ReadBool("Есть таймер"));

                        storage.Tests.Add(test);
                        storage.Save();
                        break;

                    case 3:
                        var id = ReadInt("ID для удаления: ");
                        var count = storage.Tests.RemoveByKey(id);
                        storage.Save();

                        Console.WriteLine(count > 0 ? "Удалено" : "Не найдено");
                        break;

                    case 4:
                        RunQueries(storage.Tests);
                        break;

                    case 0:
                        return;
                }
            }
        }

        static void RunQueries(List<Test> tests)
        {
            Console.WriteLine("\nЗАПРОСЫ");
            Console.WriteLine("1. Тесты по предмету");
            Console.WriteLine("2. Тесты с таймером");
            Console.WriteLine("3. Среднее кол-во вопросов");
            Console.WriteLine("4. Максимальный балл");

            var q = ReadInt("Выбор: ");

            switch (q)
            {
                case 1:
                    var subject = ReadString("Предмет: ");
                    foreach (var t in tests.WithSubject(subject))
                    {
                        Console.WriteLine(t);
                    }

                    break;

                case 2:
                    foreach (var t in tests.WhereTimed())
                    {
                        Console.WriteLine(t);
                    }

                    break;

                case 3:
                    Console.WriteLine($"Среднее: {tests.AverageQuestions()}");
                    break;

                case 4:
                    Console.WriteLine($"Максимальный балл: {tests.MaxScore()}");
                    break;
            }
        }

        static int ReadInt(string message, bool allowZero = false)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int value) && (allowZero ? value >= 0 : value > 0))
                {
                    return value;
                }

                Console.WriteLine("Ошибка ввода. Введите положительное целое число.");
            }
        }

        static bool ReadBool(string message)
        {
            while (true)
            {
                Console.Write(message + " (y/n): ");
                var input = Console.ReadLine()?.ToLower();

                if (input == "y")
                {
                    return true;
                }

                if (input == "n")
                {
                    return false;
                }

                Console.WriteLine("Введите y или n.");
            }
        }

        static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Строка не может быть пустой.");
            }
        }
    }
}
