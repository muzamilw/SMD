<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="SMD.MIS.test.test" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-2.1.0.js"></script>
    <script src="../Scripts/jquery-ui-1.10.4.js"></script>
</head>
<body>
      <form id="form1" runat="server">
          <div runat="server" id="ShowDateSelector" visible="false">Date Range 
            <input class="datefield" data-val="true" data-val-required="Date is required" 
      id="ReleaseDate" name="StartDate" type="date" value="1/11/1989" runat="server"/>   to 
                <input class="datefield" data-val="true" data-val-required="Date is required" 
      id="EndDate" name="ReleaseDate" type="date" value="1/11/2016" runat="server" />  
              <asp:Button ID="Filter" runat="server" Text="Filter" OnClick="Filter_Click"/>
          </div>
          
    <div >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="rvDataViewer" runat="server" AsyncRendering="false" PageCountMode="Actual"
            Width="820px" Height="600px">
        </rsweb:ReportViewer>
        &nbsp;&nbsp;&nbsp;
    </div>
    </form>
</body>
</html>
