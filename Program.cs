using System;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine(
                    "=======================================");
                Console.WriteLine(
                    "      ЛАБОРАТОРНАЯ РАБОТА №3 " +
                    "(Вариант 4)");
                Console.WriteLine(
                    "=======================================");
                Console.WriteLine(
                    "1. Задание 1.4 — Работа с " +
                    "квадратными матрицами");
                Console.WriteLine(
                    "2. Задание 2.4 — Расчет " +
                    "максимального долга банков");
                Console.WriteLine(
                    "3. Задание 3.4 — Выражение");
                Console.WriteLine(
                    "4. Задание 4.4 — Бинарные файлы");
                Console.WriteLine(
                    "5. Задание 5.4 — Бинарные файлы и структуры (Багаж)");
                Console.WriteLine(
                    "0. Выход");
                Console.WriteLine(
                    "=======================================");

                int choice = ReadInt(
                    "Выберите пункт меню: ", 0, 5);

                switch (choice)
                {
                    case 1:
                        RunTask1();
                        break;
                    case 2:
                        RunTask2();
                        break;
                    case 3:
                        RunTask3();
                        break;
                    case 4:
                        RunTask4();
                        break;
                    case 5:
                        RunTask5();
                        break;
                    case 0:
                        exit = true;
                        Console.WriteLine(
                            "Программа завершена.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine(
                        "\nНажмите любую клавишу " +
                        "для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        private static void RunTask1()
        {
            Console.Clear();
            Console.WriteLine(
                "=== ЗАДАНИЕ 1.4 ===");

            Console.WriteLine(
                "\n--- 1.4.1 Массив 1 " +
                "(Ввод с клавиатуры) ---");
            int n1 = ReadInt(
                "Введите количество строк (n > 0): ", 
                1, 100);
            int m1 = ReadInt(
                "Введите количество столбцов (m > 0): ", 
                1, 100);

            int[,] keyboardData = new int[n1, m1];
            Console.WriteLine(
                $"\nВведите элементы матрицы " +
                $"{n1}x{m1} по строкам:");
            for (int i = 0; i < n1; i++)
            {
                Console.WriteLine(
                    $"--- Строка {i + 1} ---");
                for (int j = 0; j < m1; j++)
                {
                    keyboardData[i, j] = ReadInt(
                        $"Элемент [{i},{j}]: ");
                }
            }

            Array array1 = new Array(n1, m1);
            array1.FillFromKeyboard(keyboardData);
            Console.WriteLine(
                "\nЗаполненный Массив 1:");
            Console.WriteLine(array1);

            Console.WriteLine(
                "\n--- Задайте размерность " +
                "для квадратных массивов 2 и 3 ---");
            int nSquare = ReadInt(
                "Введите размер n для " +
                "квадратных матриц: ", 
                1, 100);

            Console.WriteLine(
                "\n--- 1.4.2 Массив 2 " +
                "(Случайные числа по " +
                "побочной диагонали) ---");
            Array array2 = new Array(nSquare);
            array2.FillBySideDiagonalCondition();
            Console.WriteLine(array2);

            Console.WriteLine(
                "\n--- 1.4.3 Массив 3 " +
                "(Заполнение змейкой " +
                "по диагоналям) ---");
            Array array3 = new Array(nSquare);
            array3.FillThirdArrayVariant4();
            Console.WriteLine(array3);
        }

        private static void RunTask2()
        {
            Console.Clear();
            Console.WriteLine(
                "=== ЗАДАНИЕ 2.4 (Банки) ===");
            int m = ReadInt(
                "Введите количество банков " +
                "(m > 1): ", 
                2, 100);

            int[,] debts = new int[m, m];

            Console.WriteLine(
                "\n--- Ввод задолженностей " +
                "банков друг другу ---");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == j)
                    {
                        debts[i, j] = 0; 
                    }
                    else
                    {
                        debts[i, j] = ReadInt(
                            $"Долог Банка {i + 1} " +
                            $"перед Банком {j + 1}: ", 
                            0, int.MaxValue);
                    }
                }
            }

            Array bankMatrix = new Array(m);
            bankMatrix.FillFromKeyboard(debts);

            Console.WriteLine(
                "\nМатрица задолженностей " +
                "(строка — должник, " +
                "столбец — кредитор):");
            Console.WriteLine(bankMatrix);

            int maxDebtBankIndex = 
                bankMatrix.GetBankWithMaxDebt();

            Console.WriteLine(
                "\n=======================================");
            if (maxDebtBankIndex == 0)
            {
                Console.WriteLine(
                    "ОТВЕТ: Задолженностей нет " +
                    "(у всех банков долги равны 0).");
            }
            else
            {
                Console.WriteLine(
                    "ОТВЕТ: Банк с максимальным " +
                    $"суммарным долгом — " +
                    $"Банк №{maxDebtBankIndex + 1}");
            }
            Console.WriteLine(
                "=======================================");
        }

        private static void RunTask3()
        {
            Console.Clear();
            Console.WriteLine(
                "=== ЗАДАНИЕ 3 (Вычисление " +
                "выражения: 2*A - B^T * C) ===");

            Console.WriteLine(
                "Введите размерности для матриц:");
            int n = ReadInt(
                "Введите количество строк " +
                "матриц A и B (n): ", 
                1, 10);
            int m = ReadInt(
                "Введите количество столбцов " +
                "матриц A и B (m): ", 
                1, 10);
            int k = ReadInt(
                "Введите количество столбцов " +
                "матрицы C (k): ", 
                1, 10);

            Array matrixA = new Array(n, m);
            Array matrixB = new Array(n, m);
            Array matrixC = new Array(m, k);

            Console.WriteLine(
                "\n--- Ввод матрицы A ---");
            matrixA.FillFromKeyboard(
                InputMatrixData(n, m));

            Console.WriteLine(
                "\n--- Ввод матрицы B ---");
            matrixB.FillFromKeyboard(
                InputMatrixData(n, m));

            Console.WriteLine(
                "\n--- Ввод матрицы C ---");
            matrixC.FillFromKeyboard(
                InputMatrixData(m, k));

            Console.WriteLine(
                "\nМатрица A:");
            Console.WriteLine(matrixA);
            Console.WriteLine(
                "Матрица B:");
            Console.WriteLine(matrixB);
            Console.WriteLine(
                "Матрица C:");
            Console.WriteLine(matrixC);

            try
            {
                Array bTransposed = 
                    matrixB.Transpose();
                Array term1 = 2 * matrixA;
                Array term2 = 
                    bTransposed * matrixC;
                Array result = term1 - term2;

                Console.WriteLine(
                    "\n=======================================");
                Console.WriteLine(
                    "ОТВЕТ (Результат выражения " +
                    "2*A - B^T * C):");
                Console.WriteLine(
                    "=======================================");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"\nОшибка при вычислении: " +
                    $"{ex.Message}");
            }
        }

        private static void RunTask4()
        {
            Console.Clear();
            Console.WriteLine(
                "=== ЗАДАНИЕ 4. БИНАРНЫЕ ФАЙЛЫ " +
                "(Вариант 4) ===");
            
            string path = "numbers.bin";
            int count = ReadInt(
                "Введите количество чисел " +
                "в файле (n > 0): ", 
                1, 100);

            int[] userNumbers = new int[count];
            Console.WriteLine(
                "\n--- Ввод элементов файла ---");
            for (int i = 0; i < count; i++)
            {
                userNumbers[i] = ReadInt(
                    $"Введите элемент {i + 1}: ");
            }

            BinaryTask.WriteUserBinaryFile(
                path, userNumbers);

            Console.WriteLine(
                "\nСодержимое созданного " +
                "бинарного файла:");
            BinaryTask.PrintBinaryFile(path);

            int pairs = 
                BinaryTask.CountOppositePairs(path);
            
            Console.WriteLine(
                "\n=======================================");
            Console.WriteLine(
                "ОТВЕТ: Количество пар " +
                $"противоположных чисел = {pairs}");
            Console.WriteLine(
                "=======================================");
        }

        private static void RunTask5()
        {
            Console.Clear();
            Console.WriteLine(
                "=== ЗАДАНИЕ 5.4 (Багаж и бинарный файл) ===");

            string path = "baggage.bin";
            int passengerCount = ReadInt(
                "Введите количество пассажиров: ", 
                1, 10);

            PassengerBaggage[] baggageData = 
                new PassengerBaggage[passengerCount];

            for (int i = 0; 
                 i < passengerCount; 
                 i++)
            {
                Console.WriteLine(
                    $"\n--- Пассажир {i + 1} ---");
                Console.Write("Введите ФИО пассажира: ");
                baggageData[i].PassengerName = 
                    Console.ReadLine();

                int itemCount = ReadInt(
                    "Количество вещей в багаже: ", 
                    1, 5);

                baggageData[i].Items = 
                    new BaggageItem[itemCount];

                for (int j = 0; 
                     j < itemCount; 
                     j++)
                {
                    Console.WriteLine(
                        $" Вещь #{j + 1}:");
                    Console.Write(
                        "   Название (чемодан/сумка): ");
                    baggageData[i].Items[j].Name = 
                        Console.ReadLine();

                    baggageData[i].Items[j].Weight = 
                        (double)ReadInt(
                            "   Масса (кг): ", 
                            1, 100);
                }
            }

            BinaryTask.WriteBaggageBinaryFile(
                path, baggageData);
            Console.WriteLine(
                "\nДанные записаны в бинарный файл!");

            double maxW, minW;
            BinaryTask.CalculateBaggageDifference(
                path, out maxW, out minW);

            double difference = maxW - minW;

            Console.WriteLine(
                "\n=======================================");
            Console.WriteLine(
                $"Максимальная масса багажа: {maxW} кг");
            Console.WriteLine(
                $"Минимальная масса багажа: {minW} кг");
            Console.WriteLine(
                "ОТВЕТ: Разница составляет " +
                $"{difference} кг");
            Console.WriteLine(
                "=======================================");
        }

        private static int[,] InputMatrixData(
            int rows, int cols)
        {
            int[,] data = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine(
                    $"Введите элементы " +
                    $"{i + 1}-й строки:");
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] = ReadInt(
                        $"Элемент [{i},{j}]: ");
                }
            }
            return data;
        }

        private static int ReadInt(
            string prompt, 
            int min = int.MinValue, 
            int max = int.MaxValue)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value) && 
                    value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine(
                    "Ошибка ввода! Введите целое " +
                    $"число от {min} до {max}.");
            }
        }
    }
}