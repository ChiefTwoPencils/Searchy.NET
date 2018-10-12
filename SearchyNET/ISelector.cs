using System;

namespace SearchyNET
{
    public interface ISelector {
        IComparable Select(ISelectable selectable);
    }
}