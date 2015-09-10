<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterPrint.master" AutoEventWireup="false" CodeFile="ap_doc_mr_worksheet_cr.aspx.vb" Inherits="ap_doc_mr_worksheet" title="Untitled Page"  enableEventValidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">

<form id="mpFORM" runat="server">    
<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 

</asp:TableCell>    
</asp:TableRow>

<asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell><br /><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p><br /><br /></asp:TableCell></asp:TableRow>

<asp:TableRow>   
<asp:TableCell> 
    <%--<CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" />--%>
    <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" />
</asp:TableCell></asp:TableRow></asp:Table></form><br /><br /><br /><br />
</asp:Content>

