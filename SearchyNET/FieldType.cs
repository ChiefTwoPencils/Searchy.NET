using System;
using System.Collections.Generic;

namespace SearchyNET
{
    /// <summary>
    /// Represents a type, a converter function and a list of
    /// operators the type supports.
    /// </summary>
    public class FieldType
    {
        /// <summary>
        /// Constructs a FieldType with a given data type descriptor,
        /// conversion function, and a list of supported operators.
        /// </summary>
        /// <param name="dataType"><see cref="DataType"/></param>
        /// <param name="converter"><see cref="Convert"/></param>
        /// <param name="ops"><see cref="Ops"/></param>
        public FieldType(string dataType, Func<string, IComparable> converter, List<Operator> ops)
        {
            DataType = dataType;
            Ops = ops;
            Convert = converter;
        }
        
        /// <summary>
        /// A name that describes the field data type.
        /// </summary>
        public string DataType { get; }
        
        /// <summary>
        /// Operators supported by the field type (=, !=, etc.).
        /// <see cref="SearchyNET.Operator"/>
        /// </summary>
        public List<Operator> Ops { get; }
        
        /// <summary>
        /// A function that should take a string representation for
        /// a value of the FieldType and return an it parsed as an
        /// IComparable. 
        /// </summary>
        public Func<string, IComparable> Convert { get; }
    }
}