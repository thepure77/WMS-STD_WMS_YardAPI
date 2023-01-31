using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class DockInteval
    {
        
        public Guid DockQoutaInterval_Index { get; set; }
        public Guid Dock_Index { get; set; }
        public string Interval_Start { get; set; }
        public string Interval_End { get; set; }
        public string Dock_Id { get; set; }
        public string Dock_Name { get; set; }
        public int seq { get; set; }
    }

}
