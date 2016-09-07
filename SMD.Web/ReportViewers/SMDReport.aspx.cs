using Microsoft.Reporting.WebForms;
using SMD.Implementation.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMD.MIS.ReportViewers
{
    public partial class SmdReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rvDataViewer.ProcessingMode = ProcessingMode.Local;

            try
            {
                if (!IsPostBack)
                {
                        rvDataViewer.Reset();
                        DataTable dt = ConvertListToDataTable(getRecords());
                        rvDataViewer.LocalReport.ReportPath = "Reports/SMDReport.rdlc";
                        rvDataViewer.LocalReport.DataSources.Clear();
                        rvDataViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        rvDataViewer.DataBind();
                        rvDataViewer.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {

            }
        }
        static List<vw_Cash4AdsReport> getRecords()
        {
            return TransactionManager.GetSmdReport();
        }
        static DataTable ConvertListToDataTable(List<vw_Cash4AdsReport> list)
        {
            // New table.
            DataTable table = new DataTable();

            table.Columns.Add("Transaction");
            table.Columns.Add("Deposit");
            table.Columns.Add("Withdrawal");
            table.Columns.Add("TDate");
            table.Columns.Add("AccountBalance");
            table.Columns.Add("CurentBalance");
            table.Columns.Add("VoucherTitle");
            table.Columns.Add("Email");
            for (int i = 0; i < list.Count; i++)
                table.Rows.Add(list[i].Transaction, String.Format("{0:0.00}", list[i].Deposit), String.Format("{0:0.00}", list[i].Withdrawal),
                    list[i].TDate.HasValue ? list[i].TDate.Value.ToString("dd MMM yyyy hh:mm:ss") : "",String.Format("{0:0.00}", list[i].AccountBalance),
                    String.Format("{0:0.00}",  list[i].CurentBalance),list[i].VoucherTitle,list[i].Email);

            return table;
        }

      
    }
}