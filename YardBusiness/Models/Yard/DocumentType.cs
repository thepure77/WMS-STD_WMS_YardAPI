using System;
using System.Collections.Generic;

using Business.Commons;

namespace Business.Models
{
    public class DocumentType
    {
        public Guid? DocumentType_Index { get; set; }

        public Guid? Process_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }


        public string Format_Text { get; set; }


        public string Format_Date { get; set; }


        public string Format_Running { get; set; }

        public string Format_Document { get; set; }

        public int? IsResetByYear { get; set; }

        public int? IsResetByMonth { get; set; }

        public int? IsResetByDay { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }



        public string Create_By { get; set; }

        
        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }

     
        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }

   
        public DateTime? Cancel_Date { get; set; }

        public string DocumentRunningNo { get; set; }
    }

    public class SearchDocumentTypeInClauseViewModel : Pagination
    {
        public List<Guid> List_DocumentType_Index { get; set; }

        public List<string> List_DocumentType_Id { get; set; }

        public Guid? Process_Index { get; set; }
    }

    public class ActionResultSearchDocumentTypeModel
    {
        public Pagination Pagination { get; set; }

        public List<DocumentType> ItemsDocumentType { get; set; }
    }
}
