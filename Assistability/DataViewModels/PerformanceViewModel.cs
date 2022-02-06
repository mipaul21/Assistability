/// <summary>
/// Ryan Taylor
/// Created: 2021/03/29
/// 
/// This is a view model for the Performance Objects
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModels
{
    public class PerformanceViewModel
    {
        public string PerformanceName { get; set; }
        public string PerformanceDescription { get; set; }
        public int UserID_client { get; set; }
        public int UserIDCreator { get; set; }
    }
}
