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

namespace SMD.MIS.test
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rvDataViewer.ProcessingMode = ProcessingMode.Local;

            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["UserId"] != null)
                    {
                        string userId = Request.QueryString["UserId"].ToString();
                        rvDataViewer.Reset();
                        StringBuilder sb = new StringBuilder("");
                        DataTable dt = ConvertListToDataTable(getRecords(userId));
                        rvDataViewer.LocalReport.ReportPath = "Reports/Users.rdlc";
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
        static List<vw_GetUserTransactions> getRecords(string userId)
        {
            return PayOutScheduler.GetUserTransactions().Where(g => g.userId == userId).ToList();
        }
        static DataTable ConvertListToDataTable(List<vw_GetUserTransactions> list)
        {
            // New table.
            DataTable table = new DataTable();

            table.Columns.Add("Transaction");
            table.Columns.Add("Deposit");
            table.Columns.Add("Withdrawal");
            table.Columns.Add("TDate");

            for (int i = 0; i < list.Count; i++)
                table.Rows.Add(list[i].Transaction, list[i].Deposit, list[i].Withdrawal, list[i].TDate);

            return table;
        }
    }
}