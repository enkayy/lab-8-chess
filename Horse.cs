namespace Chess
{
    /// <summary>
    /// ����� ����������� ���������� ����.
    /// </summary>
    class Horse : Figure
    {
        public Horse(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// ���������� ������������ ������.
        /// </summary>
        public override string Name()
        {
            return "����";
        }
    }
}