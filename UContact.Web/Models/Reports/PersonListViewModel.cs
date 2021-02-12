using System.Collections.Generic;
using UContact.Web.Models.Reports;

namespace UContact.Web.Models.Reports
{
    public class ReportListViewModel
    {
        public List<ReportViewModel> Persons { get; set; } = new List<ReportViewModel>();
    }
}
