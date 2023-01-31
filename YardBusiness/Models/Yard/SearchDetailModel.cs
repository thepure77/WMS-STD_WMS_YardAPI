using Business.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public partial class SearchDetailModel : Pagination
    {

        public SearchDetailModel()
        {
            status = new List<statusViewModel>();
        }

        public Guid Appointment_Index { get; set; }
        public string Dock_Index { get; set; }
        public string Appointment_Id { get; set; }
        public string DocumentType_Name { get; set; }
        public Guid DocumentType_Index { get; set; }
        public string User_carInspection { get; set; }
        public DateTime? date_booking { get; set; }
        public string Appointment_Time { get; set; }
        public string Dock_Name { get; set; }
        public string Vehicle_No { get; set; }
        public string Driver_Name { get; set; }
        public string VehicleType_Name { get; set; }
        public string Ref_Document_No { get; set; }
        public string appointment_Date { get; set; }
        public string appointment_Date_To { get; set; }
        public string Car_Inspection { get; set; }
        public int? Is_Inspection { get; set; }
        public string update_by { get; set; }

        public List<statusViewModel> status { get; set; }


        public class actionResultViewModel
        {
            public IList<SearchDetailModel> itemsCarInspection { get; set; }
            public Pagination pagination { get; set; }
        }

        public class statusViewModel
        {
            public string value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }
    }
}
