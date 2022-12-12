using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Intrinsics.X86;

namespace открыватель
{
    internal class l
    {
        public ConsoleKey key;
        public string sslka;
    }

    internal class ch : de_ser
    {
        protected static List<l> vse = new List<l>();
        protected static List<string> sslki = new List<string>();
        protected static List<ConsoleKey> keys = new List<ConsoleKey>();
        public static void nach()
        {
            int pos = 3;
            Console.Clear();
            Console.WriteLine($"Список того, что можно открыть\n-------------------------------\nКлавиша\t\tПриложение");
            foreach (l a in vse)
            {
                Console.SetCursorPosition(3, pos);
                Console.WriteLine(a.key);
                Console.SetCursorPosition(16, pos);
                Console.WriteLine(a.sslka);
                pos++;
            }
            Console.WriteLine();
            Console.WriteLine($"Добавить открывашку - '+'");
            Console.WriteLine($"Удалить открывашку - '-'");
            Console.WriteLine($"Перейти в режим открытия - '*'");
            ser(vse);
        }
        public static void New()
        {
            l hg = new l();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите клавишу");
                hg.key = Console.ReadKey().Key;
                if (keys.Contains(hg.key))
                {
                    Console.Clear();
                    Console.WriteLine("Вы ввели клавишу, которая уже существует, попробуйте еще раз!");
                    Thread.Sleep(700);
                }
                else
                {
                    keys.Add(hg.key);
                    break;
                }
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите ссылку, для этого выберите приложение, нажмите свойства и скопируйте из вкладки общее то, что там написано");
                hg.sslka = Console.ReadLine();
                if (sslki.Contains(hg.sslka))
                {
                    Console.Clear();
                    Console.WriteLine("Вы ввели ссылку, которая уже существует, попробуйте еще раз!");
                    Thread.Sleep(700);
                }
                else
                {
                    sslki.Add(hg.sslka);

                    break;
                }
            }
            vse.Add(hg);
        }
        public static void Open()
        {
            Console.SetCursorPosition(0, 9 + vse.Count);
            Console.WriteLine("Вводите клавиши, которые доступны. \nЧтобы выйти нажмите '/' ");
            ConsoleKeyInfo n = Console.ReadKey(true);
            int u = 0;
            foreach (ConsoleKey g in keys)
            {
                if (g == n.Key)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo { FileName = sslki[u], UseShellExecute = true });
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Отказано в доступе");
                    }
                    break;
                }
                else
                {
                    u++;
                }
            }
        }
    }
    class strelka : ch
    {
        public static int Del()
        {
            nach();
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("-->");
            ConsoleKeyInfo k;
            int pos = 3;
            do
            {
                Console.SetCursorPosition(0, pos);
                k = Console.ReadKey();
                if (k.Key == ConsoleKey.DownArrow)
                {
                    pos++;
                    if (pos > 2 + vse.Count)
                    {
                        pos--;
                    }
                }
                if (k.Key == ConsoleKey.UpArrow)
                {
                    pos--;
                    if (pos < 3)
                    {
                        pos = 3;
                    }
                }
                Console.Clear();
                nach();
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("-->");
            } while (k.Key != ConsoleKey.Enter);
            return pos - 3;
        }
    }
    class de_ser
    {
        public static void deser(List<l> vse)
        {
            vse = JsonConvert.DeserializeObject<List<l>>(File.ReadAllText("C:\\Users\\user\\Desktop\\Result.json")) ?? new List<l>();
        }
        public static void ser(List<l> vse)
        {
            File.WriteAllText("C:\\Users\\user\\Desktop\\Result.json", JsonConvert.SerializeObject(vse));
        }

    }
    class Program : ch 
    {
        static void Main()
        {
            deser(vse);
            while (true)
            {
                ConsoleKeyInfo key;
                do
                {
                    Console.Clear();
                    nach();
                    key = Console.ReadKey();
                } while (key.Key != ConsoleKey.Add && key.Key != ConsoleKey.Subtract && key.Key != ConsoleKey.Multiply);
                if (key.Key == ConsoleKey.Add)
                {
                    New();
                }
                if (key.Key == ConsoleKey.Subtract)
                {
                    int pos = strelka.Del();
                    vse.RemoveAt(pos);
                    sslki.RemoveAt(pos);
                    keys.RemoveAt(pos);
                }
                if (key.Key == ConsoleKey.Multiply)
                {
                    Open();
                }
            }
        }
    }
}