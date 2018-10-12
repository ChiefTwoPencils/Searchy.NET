namespace SearchyNET
{
    public class Value
    {
        public Value() : this("") { }
        public Value(string text)
        {
            Text = text;
        }
        public string Text { get; set; }
    }
}