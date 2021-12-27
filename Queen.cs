namespace Chess
{
    /// <summary>
    /// ����� ����������� ���������� �����.
    /// </summary>
    class Queen : Figure
    {
        public Queen(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// ���������� ������������ ������.
        /// </summary>
        public override string Name()
        {
            return "�����";
        }
    }
}