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
                        if (Request.QueryString["StartDate"] != null && Request.QueryString["EndDate"] != null)
                        {
                            string userId = Request.QueryString["UserId"].ToString();
                            string sDate = Request.QueryString["StartDate"].ToString().Replace("-","/");
                            string eDate = Request.QueryString["EndDate"].ToString().Replace("-","/");
                            rvDataViewer.Reset();
                            StringBuilder sb = new StringBuilder("");
                            DateTime startDate = Convert.ToDateTime(sDate);
                            DateTime endDate = Convert.ToDateTime(eDate);
                            DataTable dt = ConvertListToDataTable(getRecords(userId, startDate, endDate));
                            rvDataViewer.LocalReport.ReportPath = "Reports/Users.rdlc";
                            rvDataViewer.LocalReport.DataSources.Clear();
                            rvDataViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                            rvDataViewer.DataBind();
                            rvDataViewer.LocalReport.Refresh();

                            if (!string.IsNullOrEmpty(Request.QueryString["mode"]) && Request.QueryString["mode"] == "email")
                            {
                                SavePDF(rvDataViewer, Request.QueryString["FileName"]);
                            }
                        }
                        else
                        {
                            ShowDateSelector.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        static List<vw_GetUserTransactions> getRecords(string userId, DateTime startDate, DateTime endDate)
        {
            return PayOutScheduler.GetUserTransactions().Where(g => g.userId == userId && g.TDate >= startDate && g.TDate <= endDate).ToList();
        }
        static DataTable ConvertListToDataTable(List<vw_GetUserTransactions> list)
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
            for (int i = 0; i < list.Count; i++)
                table.Rows.Add(list[i].Transaction, String.Format("{0:0.00}", list[i].Deposit), String.Format("{0:0.00}", list[i].Withdrawal),
                    list[i].TDate.HasValue ? list[i].TDate.Value.ToString("dd MMM yyyy hh:mm:ss") : "",String.Format("{0:0.00}", list[i].AccountBalance),
                    String.Format("{0:0.00}",  list[i].CurentBalance),list[i].VoucherTitle);

            return table;
        }

        protected void Filter_Click(object sender, EventArgs e)
        {
            string userId = Request.QueryString["UserId"].ToString();
            rvDataViewer.Reset();
            StringBuilder sb = new StringBuilder("");
            DateTime startDate = Convert.ToDateTime(ReleaseDate.Value);
            DateTime endDate = Convert.ToDateTime(EndDate.Value);
            DataTable dt = ConvertListToDataTable(getRecords(userId, startDate, endDate));
            rvDataViewer.LocalReport.ReportPath = "Reports/Users.rdlc";
            rvDataViewer.LocalReport.DataSources.Clear();
            rvDataViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            rvDataViewer.DataBind();
            rvDataViewer.LocalReport.Refresh();
          
        }
        public void SavePDF(ReportViewer viewer, string FileName)
        {
            byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

            using (FileStream stream = new FileStream(HttpContext.Current.Server.MapPath("~/SMD_Content/EmailAttachments/"+ FileName), FileMode.Create))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }
        }
    }
}