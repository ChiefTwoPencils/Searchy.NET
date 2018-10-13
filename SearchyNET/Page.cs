namespace SearchyNET
{
    public class Page
    {
        public Page() : this(1, 25)
        {
        }

        public Page(int number, int take)
        {
            Number = number;
            Take = take;
        }
        public int Number { get; set; }
        public int Take { get; set; }
        public int Skip => (Number - 1) * Take;
    }
}