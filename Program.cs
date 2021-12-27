using System;

namespace Chess
{
    class Program
    {
        /// <summary>
        /// Вывод ошибки.
        /// </summary>
        /// <param name="msg">Текст ошибки.</param>
        static void PrintErr(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        /// <summary>
        /// Проверяет входные данные.
        /// </summary>
        /// <returns>Введенное пользователем число от 1 до max</returns>
        static int InputInt(int max, string msg)
        {
            int value;

            while (true)
            {
                Console.Write(msg);

                if (!Int32.TryParse(Console.ReadLine(), out value))
                {
                    PrintErr("Введите число!");
                    continue;
                }

                if (!(value >= 1 && value <= max))
                {
                    PrintErr($"Число вне диапазона! (1 <= n <= {max})");
                    continue;
                }

                return value;
            }
        }

        /// <summary>
        /// Взятие фигуры.
        /// </summary>
        /// <param name="pawn">Если значение установлено в true - создавать пешку, 
        /// иначе ввести диалог с пользователем на выбор другой фигуры.</param>
        /// <returns>Выбранная фигура.</returns>
        static Figure InputFigure(bool pawn, Figure fig = null)
        {
            int numFig, hor, ver;

            Console.Clear();
            Console.Write("ВЫБОР МЕСТА ДЛЯ ФИГУРЫ НА ШАХМАТНОЙ ДОСКЕ\n\n");
            while (true)
            {
                ver = InputInt(8, "Введите номер вертикали: ");
                hor = InputInt(8, "Введите номер горизонтали: ");

                if (fig != null && ver == fig.Vertical && hor == fig.Horizontal)
                {
                    PrintErr("Координаты точек должны отличаться!");
                    continue;
                }

                break;
            }

            if (pawn == false)
            {
                Console.Clear();
                Console.Write("ВЫБОР ФИГУРЫ\n\n");
                Console.WriteLine("Выберите фигуру:\n" +
                                  "1 - Ферзь\n" +
                                  "2 - Ладья\n" +
                                  "3 - Слон\n" +
                                  "4 - Конь");
                numFig = InputInt(4, "-> ");
            }
            else return new Pawn(ver, hor);

            switch (numFig)
            {
                case 1: return new Queen(ver, hor);
                case 2: return new Rook(ver, hor);
                case 3: return new Elephant(ver, hor);
                default: return new Horse(ver, hor);
            }
        }

        static void Main(string[] args)
        {
            Figure fig, pawn;

            while (true)
            {
                bool danger;

                // Взятие информации о двух фигурах.
                fig = InputFigure(false);
                pawn = InputFigure(true, fig);

                // Сравнение их полей.
                Console.Clear();
                Console.Write("a) ");
                Chess.FieldComparison(fig, pawn);

                Console.Write("\nб) ");
                danger = Chess.CheckDanger(fig, pawn);

                Console.Write("\nв) ");
                Chess.SearchDangerMinMov(fig, pawn, danger);

                Console.WriteLine();
                Chess.PrintChessBoardWithFigs(fig, pawn);

                Console.Write("\n\nНажмите на любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}