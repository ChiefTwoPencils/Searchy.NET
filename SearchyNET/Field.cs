using System;

namespace SearchyNET
{
    /// <summary>
    /// Represents a field to query data on.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Constructs a default Field with a string type with
        /// an empty name and selector returning an empty string.
        /// </summary>
        public Field() : this(string.Empty, Searchy.DataType.String, a => string.Empty) { }
        
        /// <summary>
        /// Constructs a Field with a given name, field type and
        /// selector.
        /// </summary>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="fieldType"><see cref="FieldType"/></param>
        /// <param name="selector"><see cref="Selector"/></param>
        public Field(string name, FieldType fieldType, Func<ISelectable, IComparable> selector)
        {
            Name = name;
            FieldType = fieldType;
            Selector = selector;
        }
        
        /// <summary>
        /// Name that describes the field.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// <see cref="SearchyNET.FieldType"/>
        /// </summary>
        public FieldType FieldType { get; }
        
        /// <summary>
        /// A function to extract a comparable from a selectable.
        /// Should allow values from attributes of queryable types.
        /// </summary>
        public Func<ISelectable, IComparable> Selector { get; }
    }
}