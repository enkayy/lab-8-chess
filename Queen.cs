namespace Chess
{
    /// <summary>
    /// Класс реализующий шахматного ферзя.
    /// </summary>
    class Queen : Figure
    {
        public Queen(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// Возвращает наименование фигуры.
        /// </summary>
        public override string Name()
        {
            return "ферзь";
        }
    }
}