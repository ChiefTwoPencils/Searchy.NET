namespace SearchyNET
{
    /// <summary>
    /// Represents a page definition used for pagination.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Constructs a default Page at page 1 and
        /// taking 25.
        /// </summary>
        public Page() : this(1, 25)
        {
        }

        /// <summary>
        /// Constructs a page at the given page number
        /// and take value.
        /// </summary>
        /// <param name="number"><see cref="Number"/></param>
        /// <param name="take"><see cref="Take"/></param>
        public Page(int number, int take)
        {
            Number = number;
            Take = take;
        }
        
        /// <summary>
        /// The page number.
        /// </summary>
        public int Number { get; set; }
        
        /// <summary>
        /// Number of items to take.
        /// </summary>
        public int Take { get; set; }
        
        /// <summary>
        /// Calculated skip value based on the page
        /// number and take value.
        /// </summary>
        public int Skip => (Number - 1) * Take;
    }
}