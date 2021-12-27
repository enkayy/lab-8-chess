namespace Chess
{
    /// <summary>
    /// ����� ����������� ��������� �����.
    /// </summary>
    class Pawn : Figure
    {
        public Pawn(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// ���������� ������������ ������.
        /// </summary>
        public override string Name()
        {
            return "�����";
        }
    }
}