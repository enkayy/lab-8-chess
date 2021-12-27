using System;
using System.Text;

namespace Chess
{
    /// <summary>
    /// Статический класс для работы с фигурами.
    /// </summary>
    static class Chess
    {
        /// <summary>
        /// Сравнение цветов полей.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        public static void FieldComparison(Figure fig1, Figure fig2)
        {
            if ((fig1.Vertical + fig1.Horizontal) % 2 == (fig2.Vertical + fig2.Horizontal) % 2)
                Console.WriteLine("Поля имеют один и тот же цвет.");
            else
                Console.WriteLine("Поля имеют разные цвета.");
        }

        /// <summary>
        /// Проверка угрозы первой фигуры на вторую.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <returns>true - есть угроза, false - нет.</returns>
        public static bool CheckDanger(Figure fig1, Figure fig2)
        {
            bool danger;

            if (fig1.GetType() == typeof(Queen))
                danger = CheckWalkingVerAndHoriz(fig1, fig2) || ObliqueWalkingCheck(fig1, fig2);
            else if (fig1.GetType() == typeof(Rook))
                danger = CheckWalkingVerAndHoriz(fig1, fig2);
            else if (fig1.GetType() == typeof(Elephant))
                danger = ObliqueWalkingCheck(fig1, fig2);
            else
                danger = HorseWalkCheck(fig1, fig2);

            if (danger)
                PrintDanger(fig1, fig2);
            else
                PrintNoDanger(fig1, fig2);

            return danger;
        }

        /// <summary>
        /// Вывод шахматной доски с двумя фигурами.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        public static void PrintChessBoardWithFigs(Figure fig1, Figure fig2)
        {
            for (int i = 8; i > 0; i--)
            {
                Console.Write(i);
                for (int j = 0; j < 8; j++)
                {
                    if (fig1.Vertical == i && fig1.Horizontal == j + 1)
                        Console.Write("[" + fig1.Name()[0] + "]");
                    else if (fig2.Vertical == i && fig2.Horizontal == j + 1)
                        Console.Write("[" + fig2.Name()[0] + "]");
                    else if (i % 2 == 0 && j % 2 == 0 || i % 2 != 0 && j % 2 != 0)
                        Console.Write("[ ]");
                    else
                        Console.Write("[■]");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < 8; i++)
                Console.Write("  " + (char)(i + 'A'));
        }

        /// <summary>
        /// Поиск угрозы за минимальное количество ходов.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <param name="dangerInOneMov">Была ли угроза в один ход.</param>
        public static void SearchDangerMinMov(Figure fig1, Figure fig2, bool dangerInOneMov)
        {
            if (dangerInOneMov)
                PrintCountMovBeforeDanger(fig1, 1);
            else
            {
                if (fig1.GetType() == typeof(Queen))
                {
                    PrintCountMovBeforeDanger(fig1, 2);
                    Console.WriteLine("Точки: ");
                    Console.WriteLine(SearchDangerSecMovHorizAndVer(fig1, fig2));
                    Console.WriteLine(SearchDangerSecMovOblique(fig1, fig2));
                }
                else if (fig1.GetType() == typeof(Rook))
                {
                    PrintCountMovBeforeDanger(fig1, 2);
                    Console.WriteLine("Точки: ");
                    Console.WriteLine(SearchDangerSecMovHorizAndVer(fig1, fig2));
                }
                else if (fig1.GetType() == typeof(Elephant))
                {
                    string points;

                    if (!String.IsNullOrWhiteSpace(points = SearchDangerSecMovOblique(fig1, fig2)))
                    {
                        PrintCountMovBeforeDanger(fig1, 2);
                        Console.WriteLine("Точки: ");
                        Console.WriteLine(points);
                    }
                    else
                    {
                        PrintCountMovBeforeDanger(fig1, 0);
                    }
                }
                else
                {
                    string points;

                    if (!String.IsNullOrWhiteSpace(points = SearchDangerSecMovHorseWalk(fig1, fig2)))
                    {
                        PrintCountMovBeforeDanger(fig1, 2);
                        Console.WriteLine("Точки: ");
                        Console.WriteLine(points);
                    }
                    else
                    {
                        PrintCountMovBeforeDanger(fig1, 0);
                    }
                }
            }
        }

        #region private methods

        /// <summary>
        /// Проверка хождения вертикально и горизонтально.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <returns>Если true - первая фигура угрожает второй, иначе если false - нет.</returns>
        private static bool CheckWalkingVerAndHoriz(Figure fig1, Figure fig2)
        {
            // Если у фигур не совпадают ни горизонтали, ни вертикали, значит одна другой не угрожает.
            if (fig1.Horizontal != fig2.Horizontal && fig1.Vertical != fig2.Vertical)
                return false;

            return true;
        }

        /// <summary>
        /// Поиск опасности со второго хода по горизонтали и вертикали.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <returns>
        /// Точки, через которые первая фигура может срубить вторую со второго хода.
        /// Если пустая строка, то точек не существует.
        /// </returns>
        private static string SearchDangerSecMovHorizAndVer(Figure fig1, Figure fig2)
        {
            return String.Format($"({fig1.Vertical}, {fig2.Horizontal})\n" +
                                 $"({fig2.Vertical}, {fig1.Horizontal})");
        }

        /// <summary>
        /// Проверка хождения наискосок.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <returns>Если true - первая фигура угрожает второй, иначе если false - нет.</returns>
        private static bool ObliqueWalkingCheck(Figure fig1, Figure fig2)
        {
            // Идем наискосок направо вверх.
            for (int i = fig1.Horizontal, j = fig1.Vertical; i <= 8 && j <= 8; i++, j++)
                if (i == fig2.Horizontal && j == fig2.Vertical)
                    return true;
            // Идем наискосок влево вниз.
            for (int i = fig1.Horizontal, j = fig1.Vertical; i >= 1 && j >= 1; i--, j--)
                if (i == fig2.Horizontal && j == fig2.Vertical)
                    return true;
            // Идем наискосок направо вниз.
            for (int i = fig1.Horizontal, j = fig1.Vertical; i <= 8 && j >= 1; i++, j--)
                if (i == fig2.Horizontal && j == fig2.Vertical)
                    return true;
            // Идем наискосок влево вверх.
            for (int i = fig1.Horizontal, j = fig1.Vertical; i >= 1 && j <= 8; i--, j++)
                if (i == fig2.Horizontal && j == fig2.Vertical)
                    return true;

            return false;
        }

        /// <summary>
        /// Поиск опасности со второго хода по хождению наискосок (вывод первых точек).
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <returns>
        /// Точки, через которые первая фигура может срубить вторую со второго хода.
        /// Если пустая строка, то точек не существует.
        /// </returns>
        private static string SearchDangerSecMovOblique(Figure fig1, Figure fig2)
        {
            StringBuilder points = new StringBuilder();

            // Идем наискосок направо вверх.
            Figure copy = new Queen(fig1.Vertical, fig1.Horizontal);
            for (; copy.Vertical <= 8 && copy.Horizontal <= 8; copy.Vertical++, copy.Horizontal++)
                if (ObliqueWalkingCheck(copy, fig2))
                    points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            // Идем наискосок влево вниз.
            copy = new Queen(fig1.Vertical, fig1.Horizontal);
            for (; copy.Vertical >= 1 && copy.Horizontal >= 1; copy.Vertical--, copy.Horizontal--)
                if (ObliqueWalkingCheck(copy, fig2))
                    points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            // Идем наискосок влево вверх.
            copy = new Queen(fig1.Vertical, fig1.Horizontal);
            for (; copy.Vertical <= 8 && copy.Horizontal >= 1; copy.Vertical++, copy.Horizontal--)
                if (ObliqueWalkingCheck(copy, fig2))
                    points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            // Идем наискосок направо вниз.
            copy = new Queen(fig1.Vertical, fig1.Horizontal);
            for (; copy.Vertical >= 1 && copy.Horizontal <= 8; copy.Vertical--, copy.Horizontal++)
                if (ObliqueWalkingCheck(copy, fig2))
                    points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            return points.ToString();
        }

        /// <summary>
        /// Проверка хождения конем.
        /// </summary>        
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <returns>Если true - первая фигура угрожает второй, иначе если false - нет.</returns>
        private static bool HorseWalkCheck(Figure fig1, Figure fig2)
        {
            if (fig1.Vertical + 2 == fig2.Vertical && fig1.Horizontal + 1 == fig2.Horizontal ||
                fig1.Vertical + 2 == fig2.Vertical && fig1.Horizontal - 1 == fig2.Horizontal ||
                fig1.Vertical + 1 == fig2.Vertical && fig1.Horizontal + 2 == fig2.Horizontal ||
                fig1.Vertical - 1 == fig2.Vertical && fig1.Horizontal + 2 == fig2.Horizontal ||
                fig1.Vertical - 2 == fig2.Vertical && fig1.Horizontal + 1 == fig2.Horizontal ||
                fig1.Vertical - 2 == fig2.Vertical && fig1.Horizontal - 1 == fig2.Horizontal ||
                fig1.Vertical + 1 == fig2.Vertical && fig1.Horizontal - 2 == fig2.Horizontal ||
                fig1.Vertical - 1 == fig2.Vertical && fig1.Horizontal - 2 == fig2.Horizontal)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Поиск опасности со второго хода по хождению конем (вывод первых точек).
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        /// <returns>
        /// Точки, через которые первая фигура может срубить вторую со второго хода.
        /// Если пустая строка, то точек не существует.
        /// </returns>
        private static string SearchDangerSecMovHorseWalk(Figure fig1, Figure fig2)
        {
            StringBuilder points = new StringBuilder();

            Figure copy = new Horse(fig1.Vertical, fig1.Horizontal);
            copy.Vertical += 2;
            copy.Horizontal += 1;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            copy.Vertical = fig1.Vertical;
            copy.Horizontal = fig1.Horizontal;
            copy.Vertical += 2;
            copy.Horizontal -= 1;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            copy.Vertical = fig1.Vertical;
            copy.Horizontal = fig1.Horizontal;
            copy.Vertical += 1;
            copy.Horizontal += 2;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            copy.Vertical = fig1.Vertical;
            copy.Horizontal = fig1.Horizontal;
            copy.Vertical -= 1;
            copy.Horizontal += 2;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            copy.Vertical = fig1.Vertical;
            copy.Horizontal = fig1.Horizontal;
            copy.Vertical -= 2;
            copy.Horizontal += 1;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            copy.Vertical = fig1.Vertical;
            copy.Horizontal = fig1.Horizontal;
            copy.Vertical -= 2;
            copy.Horizontal -= 1;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            copy.Vertical = fig1.Vertical;
            copy.Horizontal = fig1.Horizontal;
            copy.Vertical += 1;
            copy.Horizontal -= 2;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            copy.Vertical = fig1.Vertical;
            copy.Horizontal = fig1.Horizontal;
            copy.Vertical -= 1;
            copy.Horizontal -= 2;
            if (HorseWalkCheck(copy, fig2))
                points.Append($"({copy.Vertical}, {copy.Horizontal})\n");

            return points.ToString();
        }

        /// <summary>
        /// Вывод угрозы.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        private static void PrintDanger(Figure fig1, Figure fig2)
        {
            Console.WriteLine(fig1.Name().Substring(0, 1).ToUpper()
                            + fig1.Name().Substring(1)
                            + " угрожает "
                            + fig2.Name().Substring(0, fig2.Name().Length - 1)
                            + "е.");
        }

        /// <summary>
        /// Вывод отсутствия угрозы.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="fig2">Вторая фигура.</param>
        private static void PrintNoDanger(Figure fig1, Figure fig2)
        {
            Console.WriteLine(fig1.Name().Substring(0, 1).ToUpper()
                            + fig1.Name().Substring(1)
                            + " не угрожает "
                            + fig2.Name().Substring(0, fig2.Name().Length - 1)
                            + "е.");
        }

        /// <summary>
        /// Вывод количество ходов до угрозы одной фигуры к другой.
        /// </summary>
        /// <param name="fig1">Первая фигура.</param>
        /// <param name="countMov">Количество ходов до угрозы.</param>
        private static void PrintCountMovBeforeDanger(Figure fig1, int countMov)
        {
            if (countMov == 1)
                Console.WriteLine(fig1.Name().Substring(0, 1).ToUpper() + fig1.Name().Substring(1)
                    + " может срубить пешку за один ход.");
            else if (countMov == 2)
                Console.WriteLine(fig1.Name().Substring(0, 1).ToUpper() + fig1.Name().Substring(1)
                    + " может срубить пешку за два хода.");
            else
                Console.WriteLine(fig1.Name().Substring(0, 1).ToUpper() + fig1.Name().Substring(1)
                    + " не представляет угрозу пешке.");
        }

        #endregion
    }
}