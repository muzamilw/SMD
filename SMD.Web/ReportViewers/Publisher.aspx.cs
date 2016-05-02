using Microsoft.Reporting.WebForms;
using SMD.Implementation.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMD.MIS.ReportViewers
{
    public partial class Publisher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rvDataViewer.ProcessingMode = ProcessingMode.Local;

            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["CompanyId"] != null)
                    {
                        rvDataViewer.Reset();
                        long companyId = Convert.ToInt64(Request.QueryString["CompanyId"].ToString());
                        DataTable dt = ConvertListToDataTable(getRecords(companyId));
                        rvDataViewer.LocalReport.ReportPath = "Reports/Publisher.rdlc";
                        rvDataViewer.LocalReport.DataSources.Clear();
                        rvDataViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        rvDataViewer.DataBind();
                        rvDataViewer.LocalReport.Refresh();
                       
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        static List<vw_PublisherTransaction> getRecords(long companyId)
        {
            return PayOutScheduler.GetPublisherTransactions().Where(g => g.ownerCompanyId == companyId ).ToList();
        }
        static DataTable ConvertListToDataTable(List<vw_PublisherTransaction> list)
        {
            // New table.
            DataTable table = new DataTable();

            table.Columns.Add("Transaction");
            table.Columns.Add("Deposit");
            table.Columns.Add("Withdrawal");
            table.Columns.Add("TDate");
            table.Columns.Add("CurentBalance");
            for (int i = 0; i < list.Count; i++)
                table.Rows.Add(list[i].Transaction, String.Format("{0:0.00}", list[i].Deposit), String.Format("{0:0.00}", list[i].Withdrawal), list[i].TDate.HasValue ? list[i].TDate.Value.ToString("dd MMM yyyy hh:mm:ss") : "",  String.Format("{0:0.00}", list[i].CurentBalance));

            return table;
        }

        protected void Filter_Click(object sender, EventArgs e)
        {

        }
    }
}