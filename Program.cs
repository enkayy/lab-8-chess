using System;

namespace Chess
{
    class Program
    {
        /// <summary>
        /// ����� ������.
        /// </summary>
        /// <param name="msg">����� ������.</param>
        static void PrintErr(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        /// <summary>
        /// ��������� ������� ������.
        /// </summary>
        /// <returns>��������� ������������� ����� �� 1 �� max</returns>
        static int InputInt(int max, string msg)
        {
            int value;

            while (true)
            {
                Console.Write(msg);

                if (!Int32.TryParse(Console.ReadLine(), out value))
                {
                    PrintErr("������� �����!");
                    continue;
                }

                if (!(value >= 1 && value <= max))
                {
                    PrintErr($"����� ��� ���������! (1 <= n <= {max})");
                    continue;
                }

                return value;
            }
        }

        /// <summary>
        /// ������ ������.
        /// </summary>
        /// <param name="pawn">���� �������� ����������� � true - ��������� �����, 
        /// ����� ������ ������ � ������������� �� ����� ������ ������.</param>
        /// <returns>��������� ������.</returns>
        static Figure InputFigure(bool pawn, Figure fig = null)
        {
            int numFig, hor, ver;

            Console.Clear();
            Console.Write("����� ����� ��� ������ �� ��������� �����\n\n");
            while (true)
            {
                ver = InputInt(8, "������� ����� ���������: ");
                hor = InputInt(8, "������� ����� �����������: ");

                if (fig != null && ver == fig.Vertical && hor == fig.Horizontal)
                {
                    PrintErr("���������� ����� ������ ����������!");
                    continue;
                }

                break;
            }

            if (pawn == false)
            {
                Console.Clear();
                Console.Write("����� ������\n\n");
                Console.WriteLine("�������� ������:\n" +
                                  "1 - �����\n" +
                                  "2 - �����\n" +
                                  "3 - ����\n" +
                                  "4 - ����");
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

                // ������ ���������� � ���� �������.
                fig = InputFigure(false);
                pawn = InputFigure(true, fig);

                // ��������� �� �����.
                Console.Clear();
                Console.Write("a) ");
                Chess.FieldComparison(fig, pawn);

                Console.Write("\n�) ");
                danger = Chess.CheckDanger(fig, pawn);

                Console.Write("\n�) ");
                Chess.SearchDangerMinMov(fig, pawn, danger);

                Console.WriteLine();
                Chess.PrintChessBoardWithFigs(fig, pawn);

                Console.Write("\n\n������� �� ����� �������...");
                Console.ReadKey();
            }
        }
    }
}