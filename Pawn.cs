namespace Chess
{
    /// <summary>
    /// Класс реализующий шахматной пешки.
    /// </summary>
    class Pawn : Figure
    {
        public Pawn(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// Возвращает наименование фигуры.
        /// </summary>
        public override string Name()
        {
            return "пешка";
        }
    }
}