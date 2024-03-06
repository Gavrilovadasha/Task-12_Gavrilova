using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    class Program
    {
        static int[,] board = new int[9, 9];
        static bool[,] initialCells = new bool[9, 9];
        static bool checkValidity = false;
        static bool[,] rowUsed = new bool[9, 9];
        static bool[,] colUsed = new bool[9, 9];
        static bool[][,] boxUsed = new bool[3][,];

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в программу для решения судоку!");

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Ввести начальные условия");
                Console.WriteLine("2. Решить судоку");
                Console.WriteLine("3. Закончить программу");

                int choice = GetIntInputFromUser();

                switch (choice)
                {
                    case 1:
                        EnterInitialConditions();
                        break;

                    case 2:
                        SolveSudoku();
                        break;

                    case 3:
                        Console.WriteLine("Программа закрывается...");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void EnterInitialConditions()
        {
            Console.WriteLine("\nВведите начальные условия для судоку.");

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"Введите значение для ячейки ({i + 1}, {j + 1}): ");
                    int value = GetIntInputFromUser();

                    if (value >= 1 && value <= 9)
                    {
                        board[i, j] = value;
                        initialCells[i, j] = true;
                    }
                    else
                    {
                        board[i, j] = 0;
                        initialCells[i, j] = false;
                    }
                }
            }

            Console.WriteLine("Начальные условия введены.");
        }

        static void SolveSudoku()
        {
            if (IsPuzzleSolved(board))
            {
                Console.WriteLine("Судоку уже решено!");
                return;
            }

            Console.WriteLine("\nИдет решение судоку...");

            bool solved = RecursiveSolveSudoku();

            if (solved)
            {
                Console.WriteLine("Судоку решено:");
                PrintBoard(board);
            }
            else
            {
                Console.WriteLine("Не удалось решить судоку. Возможно, начальные условия некорректны.");
            }
        }

        static bool RecursiveSolveSudoku()
        {
            // Ищем следующую пустую ячейку
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        // Пробуем заполнить пустую ячейку цифрой от 1 до 9
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsNumberValid(i, j, num))
                            {
                                board[i, j] = num;

                                // Рекурсивно пробуем решить судоку
                                if (RecursiveSolveSudoku())
                                {
                                    return true;
                                }

                                board[i, j] = 0;
                            }
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        static bool IsNumberValid(int row, int col, int num)
        {
            // Проверяем, есть ли такая цифра в строке, столбце или квадрате 3x3
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i] == num || board[i, col] == num || board[(row / 3) * 3 + i / 3, (col / 3) * 3 + i % 3] == num)
                {
                    return false;
                }
            }

            return true;
        }

        static bool IsPuzzleSolved(int[,] board)
        {
            // Проверяем, что все ячейки заполнены и соответствуют правилам судоку
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0 || !IsNumberValid(i, j, board[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static int GetIntInputFromUser()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Неверный формат ввода. Попробуйте снова.");
            }

            return value;
        }

        static void PrintBoard(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}