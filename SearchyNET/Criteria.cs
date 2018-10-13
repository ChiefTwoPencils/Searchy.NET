using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchyNET
{
    public class Criteria
    {
        public Criteria(List<Criterion> criteria)
        {
            All = criteria;
        }
        public List<Criterion> All { get; set; }
        public Sort Sort { get; set; }
        public Page Page { get; set; }
        
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
            bool Func() => current.Satisfies(value);
            if (index == criteria.Count - 1)
            {
                return Func;
            }
            var next = criteria[index + 1];
            return () => next.Chain.Doop(Func, SatisfiesAll(source, criteria, index + 1));
        }

        public IEnumerable<ISelectable> SortAll(IEnumerable<ISelectable> data)
        {
            return Sort.Apply(data);
        }

        public IEnumerable<ISelectable> Paginate(IEnumerable<ISelectable> data)
        {
            return data.Skip(Page.Skip).Take(Page.Take);
        }
    }
}