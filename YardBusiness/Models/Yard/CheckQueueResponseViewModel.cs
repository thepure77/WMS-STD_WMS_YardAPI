using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Business.Models
{
    public class CheckQueueResponseViewModel
    {

        public string Vehicle_Id { get; set; }
        public string Vehicle_LicenseNo { get; set; }
        public string Appointment_Id { get; set; }
        //date
        public string Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string Dock_ID { get; set; }
        public string Dock_Name { get; set; }
        public string VehicleType_Id { get; set; }
        public string VehicleType_Name { get; set; }
        public string Ref_Document_No { get; set; }
        public string DocumentType_Id { get; set; }
        public string DocumentType_Name { get; set; }
        public string GateCheckIn_NO { get; set; }
        public string GateCheckOut_NO { get; set; }
        public string GateCheckIn_Date { get; set; }
        public string GateCheckOut_Date { get; set; }
        public string Queue_No { get; set; }
        public string Before_Queue { get; set; }
        public string Status { get; set; }

        public decimal GateCheckIN_lat { get; set; }
        public decimal GateCheckIN_long { get; set; }
        public decimal GateCheckOUT_lat { get; set; }
        public decimal GateCheckOUT_long { get; set; }
        public int GateCheckIN_Radius { get; set; }
        public int GateCheckOUT_Radius { get; set; }

        
    }
}
