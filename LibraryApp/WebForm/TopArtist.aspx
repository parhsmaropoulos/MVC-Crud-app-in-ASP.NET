<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopArtist.aspx.cs" Inherits="LibraryApp.WebForm.TopArtist" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title> Top Selling Categories Report</title>
</head>
<body>
    <div class="row">
        <p align="center" size="30px">
          Give a time period to retrieve the top selling categories.  
          <asp:HyperLink ID="Home" runat="server" navigateUrl="~/Home/Index">Back</asp:HyperLink>
        </p>
        <br />
        <p align="center">
            Date format must be like this  <br />
            2009-01-01 00:00:00.000 <br />
            Time stamp is not necessary
        </p>
    </div>
        <form id="form1" runat="server" align="center">
    <div class="row">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
        <asp:TextBox ID="FromDate" runat="server"></asp:TextBox>
        <asp:Label ID="FromError" runat="server" Text="From Date must not be blank" Visible="False"></asp:Label>
    </div>
            <div style = "overflow: visible;" >
                <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                <asp:TextBox ID="ToDate" runat="server"></asp:TextBox>
                <asp:Label ID="ToError" runat="server" Text="To Date must not be blank" Visible="False"></asp:Label>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" width="90%" height="90%" align="center">

            </rsweb:ReportViewer>
            </div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
            <p>
                <asp:Label ID="DateValid" runat="server" Text="Dates are not valid.Try again." Visible="False"></asp:Label>
            </p>
        </form>
    </body>
</html>
