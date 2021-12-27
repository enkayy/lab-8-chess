namespace Chess
{
    /// <summary>
    /// ����������� ����� ��������� ������.
    /// </summary>
    abstract class Figure
    {
        /// <summary>
        /// ����������� �������� ������.
        /// </summary>
        /// <param name="vertical">����� ���������.</param>
        /// <param name="horizontal">����� �����������.</param>
        public Figure(int vertical, int horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }

        /// <summary>
        /// ����� ���������.
        /// </summary>
        public int Vertical { get; set; }

        /// <summary>
        /// ����� �����������.
        /// </summary>
        public int Horizontal { get; set; }

        /// <summary>
        /// ���������� ������������ ������.
        /// </summary>
        public virtual string Name()
        {
            return string.Empty;
        }
    }
}