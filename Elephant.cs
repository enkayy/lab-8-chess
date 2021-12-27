namespace Chess
{
    /// <summary>
    /// Класс реализующий шахматного слона.
    /// </summary>
    class Elephant : Figure
    {
        public Elephant(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// Возвращает наименование фигуры.
        /// </summary>
        public override string Name()
        {
            return "слон";
        }
    }
}
