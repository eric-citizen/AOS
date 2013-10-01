using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZDataObjects.Interfaces
{
    public interface ISortable
    {
        int SortItemID { get; }
        int SortOrder { get; set; }
    }
}
