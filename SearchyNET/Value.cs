namespace SearchyNET
{
    /// <summary>
    /// Represents a string representation of some value
    /// which may or not be an actual string.
    /// </summary>
    public class Value
    {
        /// <summary>
        /// Constructs a default Value with an empty string.
        /// </summary>
        public Value() : this(string.Empty) { }
        
        /// <summary>
        /// Constructs a Value with some given text.
        /// </summary>
        /// <param name="text"><see cref="Text"/></param>
        public Value(string text)
        {
            Text = text;
        }
        
        /// <summary>
        /// A string representation of some value of some type.
        /// </summary>
        public string Text { get; }
    }
}