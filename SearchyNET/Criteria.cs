using System;
using System.Collections.Generic;

namespace SearchyNET
{
    public class Criteria
    {
        public Criteria(List<Criterion> criteria)
        {
            All = criteria;
        }
        public List<Criterion> All { get; set; }
        public bool SatisfiesAll(ISelectable source)
        {
            var func = SatisfiesAll(source, All, 0);
            return func();
        }

        public Func<bool> SatisfiesAll(ISelectable source, List<Criterion> criteria, int index)
        {
            var current = criteria[index];
            var field = current.Field;
            var value = field.Selector(source);
            bool func() => current.Satisfies(value);
            if (index == criteria.Count - 1)
            {
                return func;
            }
            var next = criteria[index + 1];
            return () => next.Chain.Doop(func, SatisfiesAll(source, criteria, index + 1));
        }
    }
}