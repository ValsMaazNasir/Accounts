﻿<%@ Page Language="C#" AutoEventWireup="true" Codefile="Report.aspx.cs" Inherits="LiquadCargoManagment.rpASPX.Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
         <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" Height="900px"></rsweb:ReportViewer>
    </form>
   
</body>
</html>
