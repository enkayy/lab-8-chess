namespace Chess
{
    /// <summary>
    /// ����� ����������� ��������� �����.
    /// </summary>
    class Rook : Figure
    {
        public Rook(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// ���������� ������������ ������.
        /// </summary>
        public override string Name()
        {
            return "�����";
        }
    }
}