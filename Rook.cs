namespace Chess
{
    /// <summary>
    /// Класс реализующий шахматную ладью.
    /// </summary>
    class Rook : Figure
    {
        public Rook(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// Возвращает наименование фигуры.
        /// </summary>
        public override string Name()
        {
            return "ладья";
        }
    }
}