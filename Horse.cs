namespace Chess
{
    /// <summary>
    /// Класс реализующий шахматного коня.
    /// </summary>
    class Horse : Figure
    {
        public Horse(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// Возвращает наименование фигуры.
        /// </summary>
        public override string Name()
        {
            return "конь";
        }
    }
}