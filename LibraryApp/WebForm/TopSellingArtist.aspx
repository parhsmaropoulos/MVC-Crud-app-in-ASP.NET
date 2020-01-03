<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopSellingArtist.aspx.cs" Inherits="LibraryApp.WebForm.TopSellingArtist" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <div class="row">
        <p align="center" size="30px">
          Give a time period to retrieve the top selling artists.
            <asp:HyperLink ID="Home" runat="server" navigateUrl="~/Home/Index">Back</asp:HyperLink>
        </p>
        <br />
        <p align="center">
            Date format must be like this  <br />
            2009-01-01 00:00:00.000 <br />
            Time stamp is not necessary
        </p>
    </div>
    <div class="row" align="center">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
        <asp:TextBox ID="FromDate2" runat="server"></asp:TextBox>
        <asp:Label ID="FromError2" runat="server" Text="From Date must not be blank" Visible="False"></asp:Label>
    </div>
            <div style = "overflow: visible;" align="center">
                <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                <asp:TextBox ID="ToDate2" runat="server"></asp:TextBox>
                <asp:Label ID="ToError2" runat="server" Text="To Date must not be blank" Visible="False"></asp:Label>
                <rsweb:ReportViewer ID="ReportViewer2" runat="server" SizeToReportContent="true" width="90%" height="90%" align="center">

            </rsweb:ReportViewer>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
            <p>
                <asp:Label ID="DateValid" runat="server" Text="Dates are not valid.Try again." Visible="False"></asp:Label>
            </p>
            </div>
        </div>
    </form>
</body>
</html>
