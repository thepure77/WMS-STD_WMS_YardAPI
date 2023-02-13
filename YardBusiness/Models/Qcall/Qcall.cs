using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Business.Commons;
using static Business.Models.AppointmentSearchModel;

namespace Business.Models
{
    public class QueueModel
    {
        public string appointment_Id { get; set; }
        public string status { get; set; }
    }

    public class QcallModel
    {
        public List<InboundModel> lstInbound { get; set; } = new List<InboundModel>();
        public List<OutboundModel> lstOutbound { get; set; } = new List<OutboundModel>();
    }

    public class InboundModel
    {
        public string TransactionID { get; set; }
        public string QNo { get; set; }
        public string DockNo { get; set; }
        public string DockType { get; set; }
        public string LicenseNo { get; set; }
        public string Appointment_Id { get; set; }
        public string QueueDate { get; set; }
        public string QueueTime { get; set; }
        public string CheckInDate { get; set; }
        public string CheckInTime { get; set; }
        public string StatusText { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string UpdateDT { get; set; }
        public string UpdateBy { get; set; }
    }

    public class OutboundModel
    {
        public string TransactionID { get; set; }
        public string QNo { get; set; }
        public string DockNo { get; set; }
        public string DockType { get; set; }
        public string LicenseNo { get; set; }
        public string Appointment_Id { get; set; }
        public string QueueDate { get; set; }
        public string QueueTime { get; set; }
        public string CheckInDate { get; set; }
        public string CheckInTime { get; set; }
        public string StatusText { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string UpdateDT { get; set; }
        public string UpdateBy { get; set; }
    }

}
