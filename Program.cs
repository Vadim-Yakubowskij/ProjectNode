using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace node1
{
    class Program
    {
        static void Main(string[] args)
        {
                //TEST
                // Инициализация менеджера как можно раньше
                _ = Manager.Instance;

                // Запуск цепочки тестирования в консоли
                TestRandomCard(3);
                TestCardList(5);
                TestCardListManager(3, 5);


                Console.ReadKey();
        }

            /// <summary>
            /// Запустить тест создания случайных карт
            /// </summary>
            /// <param name="cardCount">Количество карт</param>
            private static void TestRandomCard(int cardCount)
            {
                Console.WriteLine("Создание случайных карт:");

                for (int i = 0; i < cardCount; i++)
                {
                    Console.WriteLine(Randomizer.RandomTask());
                }

                Console.WriteLine();
            }

            /// <summary>
            /// Запустить тест создания случайного списка карт и сортировки
            /// </summary>
            /// <param name="cardCount">Количество карт в списке</param>
            private static void TestCardList(int cardCount)
            {
                Console.WriteLine("Создание случайного списка карт и сортировки:");

                Tasklist list = Randomizer.RandomTasklist(cardCount);

                Console.WriteLine();
            }

            /// <summary>
            /// Запустить тест создания случайных списков карт в менеджере и работы с данными
            /// </summary>
            /// <param name="cardlistCount">Количество списков</param>
            /// <param name="cardCount">Количество карт в списке</param>
            private static void TestCardListManager(int cardlistCount, int cardCount)
            {
                Console.WriteLine("Создание случайных списков карт в менеджере и работа с данными:");

                Manager manager = Manager.Instance;

                // Данные загружаются из файла
                // Так что мы их перезапишем
                // Но идентификаторы продолжат инкременироваться (это ок)
                manager.All.Clear();

                for (int i = 0; i < cardlistCount; i++)
                {
                    manager.SetList(Randomizer.RandomTasklist(cardCount));
                }

                manager.SaveData();

                foreach (KeyValuePair<int, Tasklist> item in manager.All)
                {
                    Console.WriteLine(item.Value);
                }

                Console.WriteLine();
            }
        }
    }