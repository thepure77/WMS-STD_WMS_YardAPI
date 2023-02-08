using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.GI.Table
{
    public class log_api_reponse
    {
        [Key]
        public Guid log_id { get; set; }
        public DateTime log_date { get; set; }
        public string log_reponsebody { get; set; }
        public string log_absoluteuri { get; set; }
        public int status { get; set; }
        public string Interface_Name { get; set; }
        public string Status_Text { get; set; }
        public string File_Name { get; set; }
    }
}
