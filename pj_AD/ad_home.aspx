<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ad_home.aspx.vb" Inherits="ad_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p>
    <p>Selamat Datang <% Response.Write(Session("mgNAME"))%>
    </p>
    
    <br /><br /><br /><br />
</asp:Content>

