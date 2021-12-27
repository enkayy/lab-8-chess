namespace Chess
{
    /// <summary>
    /// ����� ����������� ���������� �����.
    /// </summary>
    class Elephant : Figure
    {
        public Elephant(int vertical, int horizontal) : base(vertical, horizontal) { }

        /// <summary>
        /// ���������� ������������ ������.
        /// </summary>
        public override string Name()
        {
            return "����";
        }
    }
}
