<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="gl_rpt_profitandloss_12.aspx.vb" Inherits="backoffice_gl_rpt_profitandloss_12" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace = "System.Data" %>
<%@ Import Namespace = "System.Data.OleDb" %>
<%@ Import Namespace = "System.Web" %>
<%@ Import Namespace = "System.Collections.Generic" %>
<%@ Import Namespace = "System.Drawing" %>
<%@ Import Namespace = "System.IO" %>

<script RUNAT="server" language="vbscript">
   
           
       
</script>
   
<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>

<script type="text/javascript" language="Javascript">
<!-- hide script from older browsers
function openwindow(url,nama,width,height)
{
OpenWin = this.open(url, nama);
if (OpenWin != null)
{
  if (OpenWin.opener == null)
  OpenWin.opener=self;
}
OpenWin.focus();
} 
// End hiding script-->
</script>

<form id="form1" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>            
            <td><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
            <td><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
            <td><asp:ImageButton id="btExCsv" ToolTip="csv" ImageUrl="~/images/toolbar/csvfile.png" runat="server" /></td>            
        </tr>               
    </table>
    <hr />
</asp:Panel>
</asp:TableCell>    
</asp:TableRow>

<asp:TableRow><asp:TableCell><p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE" runat="server"/></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><p><asp:HyperLink ID="mlLINKDOC" runat="server"></asp:HyperLink></p></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">   

<table width="80%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td><asp:Label ID="lbDOCDATE1" Text="Posting Date (From)" runat="server"></asp:Label></td>
        <td>:</td>
        <td>
            <asp:TextBox ID="mlDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="btDOCDATE1" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mlDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="mlDOCDATE1" TargetControlID="mlDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            <font color="blue">dd/mm/yyyy</font>
        </td>
    </tr>
    
    <tr>    
        <td><asp:Label ID="lbDOCDATE2" Text="Posting Date (To)" runat="server"></asp:Label></td>
        <td>:</td>
        <td>                
            <asp:TextBox ID="mlDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
            <input id="btDOCDATE2" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mlDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
            <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="mlDOCDATE2" TargetControlID="mlDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            <font color="blue">dd/mm/yyyy</font>
        </td>
    </tr>        
    
    <tr>
        <td><p>Branch</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpBRANCH" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td><p>Site</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpSITE" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td><p>Jumlah Site Card Maksimal</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpMAXSITEQTY" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td><p>Report Type</p></td>            
        <td>:</td>
        <td><asp:DropDownList ID="mpREPORTTYPE" runat="server"></asp:DropDownList></td>
    </tr>
    
    <tr>
        <td valign="top"><p>Retrieve Data to Local</p></td>            
        <td valign="top">:</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Retrieve Data to Local Table" /><br />
            <asp:Button ID="Button2" runat="server" Text="Retrieve Data and Preview Report" /><br />
            <asp:Button ID="Button3" runat="server" Text="Load Drop Box Data" />
        </td>
    </tr>
        
</table>  

</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="Panel1" runat="server">
    <br /><hr /><br />
    <% If mlHAVINGDETAIL = True Then%>
    <table cellspacing="5" cellpadding="5" border="1" bordercolor="#B0AEAE">
    <%
        Response.Write("<tr bgcolor=#C4C4C4>")
        Response.Write("<td><p><b>Account</b></p></td>")
        Response.Write("<td><p><b>Description</b></p></td>")
        Response.Write("<td><p><b>Total</b></p></td>")
        Response.Write("<td><p><b>%</b></p></td>")
        mlI = 0
        For mlI = 0 To mlJUMLAHSITE - 1
            mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
            Response.Write("<td><p><b>" & mlCURRENTSITE & "</b></p></td>")
            Response.Write("<td><p><b>%</b></p>")
        Next
        Response.Write("</tr>")
    %>                    



    <%
        Dim mlCOATYPE3 As String
        Dim mlLINENUMBER As Integer
        Dim mlTOTAL3_ As Double
        
        mlCOATYPE3 = ""
        mlLINENUMBER = 0
        
        
        If mlRS_SITE.IsClosed = False Then
        
            Do While mlSTRING_SITE2_ARR_COUNTER < mlSTRING_SITE2_ARR
                While mlRS_SITE.Read
                    Dim mlCOATYPE As String
                    Dim mlTOTAL2_ As Double
                    mlCOATYPE = ReportFieldGet("fieldtype", "ID-B-10PB", mlRS_SITE("G_L Account No_"))
                    Select Case mlCOATYPE
                        Case "R"
                            mlTOTAL_ = mlTOTAL_REVENUE
                        Case "W"
                            mlTOTAL_ = mlTOTAL_WAGES
                        Case "C"
                            mlTOTAL_ = mlTOTAL_COST
                    End Select
                
                    If (mlCOATYPE3 <> mlCOATYPE) Then
                        If mlCOATYPE3 = "" Then
                            mlCOATYPE3 = mlCOATYPE
                            mlRSSITETOTAL.Read()
                        Else
                        
                            mlLINENUMBER = mlLINENUMBER + 1
                            If mlLINENUMBER Mod 2 <> 0 Then
                                Response.Write("<tr bgcolor=" & mlGRIDCOLOR1 & ">")
                            Else
                                Response.Write("<tr bgcolor=" & mlGRIDCOLOR2 & ">")
                            End If
                            Response.Write("<td><p><b>" & mlRSSITETOTAL("G_L Account No_") & "</b></p></td>")
                            Response.Write("<td><p><b>" & mlRSSITETOTAL("Name_") & "</b></p></td>")
                            Response.Write("<td><p><b>" & mlTOTAL_ & "</b></p></td>")
                            Response.Write("<td align=right><p><b>100</b></p></td>")
                            mlI = 0
                            For mlI = 0 To mlJUMLAHSITE - 1
                                mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                                mlTOTAL3_ = IIf(IsDBNull(mlRSSITETOTAL(mlCURRENTSITE)) = True, 0, mlRSSITETOTAL(mlCURRENTSITE))
                                Response.Write("<td align=right><p><b>" & Math.Round(CDbl(mlTOTAL3_)).ToString("n") & "</b></p></td>")
                                Response.Write("<td align=right><p><b>100</b></p></td>")
                            Next
                            Response.Write("</tr>")
                        
                            Response.Write("<tr>")
                            Response.Write("<td colspan= 4 + mlJUMLAHSITE><br></td>")
                            Response.Write("</tr>")
                        
                            mlCOATYPE3 = mlCOATYPE
                            mlRSSITETOTAL.Read()
                        End If
                    End If
                
                
                    mlLINENUMBER = mlLINENUMBER + 1
                    If mlLINENUMBER Mod 2 <> 0 Then
                        Response.Write("<tr bgcolor=" & mlGRIDCOLOR1 & ">")
                    Else
                        Response.Write("<tr bgcolor=" & mlGRIDCOLOR2 & ">")
                    End If
                    Response.Write("<td><p>" & mlRS_SITE("G_L Account No_") & "</p></td>")
                    Response.Write("<td><p>" & mlRS_SITE("Name_") & "</p></td>")
                    Response.Write("<td><p>" & mlRS_SITE("Total") & "</p></td>")
                    Response.Write("<td align=right><p>" & Math.Round(CDbl((mlRS_SITE("Total") / mlTOTAL_) * 100), 2).ToString("n") & "</p></td>")
    
                    mlI = 0
                    For mlI = 0 To mlJUMLAHSITE - 1
                        mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                        mlTOTAL_ = IIf(IsDBNull(mlRSSITETOTAL(mlCURRENTSITE)) = True, 0, mlRSSITETOTAL(mlCURRENTSITE))
                        mlTOTAL2_ = IIf(IsDBNull(mlRS_SITE(mlCURRENTSITE)) = True, 0, mlRS_SITE(mlCURRENTSITE))
                        Response.Write("<td align=right><p>" & Math.Round(CDbl(mlTOTAL2_), 2).ToString("n") & "</p></td>")
                        Response.Write("<td align=right><p>" & Math.Round(CDbl((mlTOTAL2_ / mlTOTAL_) * 100), 2).ToString("n") & "</p></td>")
                    Next
                    Response.Write("</tr>")
                End While
                
                mlSTRING_SITE2_ARR_COUNTER = mlSTRING_SITE2_ARR_COUNTER + 1
                If mlSTRING_SITE2_ARR_COUNTER < mlSTRING_SITE2_ARR Then
                    mlOBJGS.CloseFile(mlRS_SITE)
                    mlRS_SITE = mlOBJGS.DbRecordset(mlSQL_SITE21(mlSTRING_SITE2_ARR_COUNTER))
                End If
            Loop
            
            'Add
            mlLINENUMBER = mlLINENUMBER + 1
            If mlLINENUMBER Mod 2 <> 0 Then
                Response.Write("<tr bgcolor=" & mlGRIDCOLOR1 & ">")
            Else
                Response.Write("<tr bgcolor=" & mlGRIDCOLOR2 & ">")
            End If
            Response.Write("<td><p><b>" & mlRSSITETOTAL("G_L Account No_") & "</b></p></td>")
            Response.Write("<td><p><b>" & mlRSSITETOTAL("Name_") & "</b></p></td>")
            Response.Write("<td><p><b>" & mlTOTAL_ & "</b></p></td>")
            Response.Write("<td align=right><p><b>100</b></p></td>")
            mlI = 0
            For mlI = 0 To mlJUMLAHSITE - 1
                mlCURRENTSITE = Trim(mlOBJGF.GetStringAtPosition(mlSITE, mlI, ","))
                mlTOTAL3_ = IIf(IsDBNull(mlRSSITETOTAL(mlCURRENTSITE)) = True, 0, mlRSSITETOTAL(mlCURRENTSITE))
                Response.Write("<td align=right><p><b>" & Math.Round(CDbl(mlTOTAL3_)).ToString("n") & "</b></p></td>")
                Response.Write("<td align=right><p><b>100</b></p></td>")
            Next
            Response.Write("</tr>")
                        
            Response.Write("<tr>")
            Response.Write("<td colspan= 4 + mlJUMLAHSITE><br></td>")
            Response.Write("</tr>")
            'End Add
            
            mlOBJGS.CloseFile(mlRSSITE1)
            mlOBJGS.CloseFile(mlRSCOA)
            mlOBJGS.CloseFile(mlRS_SITE)
            mlOBJGS.CloseFile(mlRSSITETOTAL)
            mlOBJGS.CloseFile(mlRS_TOTAL)
        End If
        
    %>       

    </table>
    <%End If %>
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">
    <asp:DataGrid runat="server" ID="mlDATAGRID"
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"    
    HeaderStyle-ForeColor="White"
    HeaderStyle-Font-Bold="True"
    AlternatingItemStyle-BackColor="#EFEFEF"
    OnItemCommand="mlDATAGRID_ItemCommand"
    
    AutoGenerateColumns = "true"
    ShowHeader="True"    
    AllowSorting="True"
    OnSortCommand="mlDATAGRID_SortCommand"    
    OnItemDataBound ="mlDATAGRID_ItemBound"
    
    AllowPaging="True"    
    PagerStyle-Mode="NumericPages"
    PagerStyle-HorizontalAlign="center"
    OnPageIndexChanged="mlDATAGRID_PageIndex"    
    >	    
    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns>  
         
    </Columns>
 </asp:DataGrid>  
 
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

</asp:Table>

<br /><br /><br /><br />
</form>

</asp:Content>

