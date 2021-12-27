namespace Chess
{
    /// <summary>
    /// Абстрактный класс шахматной фигуры.
    /// </summary>
    abstract class Figure
    {
        /// <summary>
        /// Конструктор создания фигуры.
        /// </summary>
        /// <param name="vertical">Номер вертикали.</param>
        /// <param name="horizontal">Номер горизонтали.</param>
        public Figure(int vertical, int horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }

        /// <summary>
        /// Номер вертикали.
        /// </summary>
        public int Vertical { get; set; }

        /// <summary>
        /// Номер горизонтали.
        /// </summary>
        public int Horizontal { get; set; }

        /// <summary>
        /// Возвращает наименование фигуры.
        /// </summary>
        public virtual string Name()
        {
            return string.Empty;
        }
    }
}