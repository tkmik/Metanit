using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task17_1.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState AgeSort { get; set; }
        public SortState Current { get; set; }
        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            Current = sortOrder;
        }
    }
}
