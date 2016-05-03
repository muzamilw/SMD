<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Publisher.aspx.cs" Inherits="SMD.MIS.ReportViewers.Publisher" %>



<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-2.1.0.js"></script>
    <script src="../Scripts/jquery-ui-1.10.4.js"></script>
    <style>
        #ShowDateSelector {
         background: linear-gradient(#E6E3D4, #DAD7C8, #E4E1D2);
             padding: 10px;
        }
        #rvDataViewer {
        width:100% !important;
        }
        .btn_main {
            border: none;
    padding: 6px 12px;
    border-bottom: 4px solid;
    -webkit-transition: border-color 0.1s ease-in-out 0s,background-color 0.1s ease-in-out 0s;
    transition: border-color 0.1s ease-in-out 0s,background-color 0.1s ease-in-out 0s;
    outline: none;  -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border-radius: 5px;
        }
        .btn_outline {
                color: #fff;
       background-color: #BDBAA8;
    border-color: #92918B;
        }
        .form-control {
   
    height: 18px;
    padding: 6px 12px;
    font-size: 14px;
    line-height: 1.42857143;
    color: #555;
    background-color: #fff;
    background-image: none;
    border: 1px solid #ccc;
    border-radius: 4px;
    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
    -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s
}
    </style>
</head>
<body>
      <form id="form1" runat="server">
          <div runat="server" id="ShowDateSelector" visible="false">Date Range 
            <input class="datefield form-control" data-val="true"  data-val-required="Date is required" 
      id="ReleaseDate" name="StartDate" type="date" value="1/11/1989" runat="server"/>   to 
                <input class="datefield form-control" data-val="true" data-val-required="Date is required" 
      id="EndDate" name="ReleaseDate" type="date" value="1/11/2016" runat="server" />  
              <asp:Button ID="Filter" runat="server" Text="Filter" class="btn_main btn_outline" OnClick="Filter_Click"/>
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
