<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ap_mr_online.aspx.vb" Inherits="ap_mr_online" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
<ajax:ScriptManager ID="ScriptManager1" runat="server">
</ajax:ScriptManager>


<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>            
            <td><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
            <td><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>
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

<table width="100%" cellpadding="0" cellspacing="0" border="0">              
<tr>
    <td  valign="middle"><asp:Label ID="lbSEARCHTEXT" Text="SEARCH" runat="server" Font-Bold="true" Font-Size="14" ForeColor="darkgreen"   ></asp:Label></td>    
    <td  valign="top">
        <asp:TextBox ID="mpSEARCHTEXT"  Height="30px" Width="400px" runat="server" Font-Size="14"></asp:TextBox>
        <asp:DropDownList ID="mpSEARCHCRITERIA" runat="server"  Width="200px" Height="30"></asp:DropDownList>
    </td>
</tr>           
</table>

</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td width="80%" valign="top">       
        
        <asp:Panel ID="pnGRID" runat="server">
            <p><asp:Label ID="Label1" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>

            <asp:Datalist runat="server" ID="mlDATALIST" 
            RepeatDirection="Horizontal"
            Width="100%"
            OnItemCommand="mlDATALIST_ItemCommand"
            OnItemDataBound ="mlDATALIST_ItemDataBound" 
            >	    
            
            <AlternatingItemStyle BackColor="#EFEEEE" />
            
            <ItemTemplate>
              <table border="0" cellpadding="0" cellspacing="0" width="100%">
              <tr>
                <td colspan="2" align="center">
                  <br />
                  <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImagePath3") %>' Width="100" Height="100" />
                </td>
              </tr>
              
              <tr>
                <td align="left"><p>No Dok</p></td>        
                <td align="left">
                    <asp:Label ID="obDOCNO" runat="server" Text='<%# Eval("DocNo") %>'/>
                    -
                    <asp:Label ID="obLINNO" runat="server" Text='<%# Eval("Linno") %>'/>
                </td>
              </tr>
              
              <tr>
                <td align="left"><p>No Polis</p></td>        
                <td align="left"><asp:Label ID="obITEMKEY" runat="server" Text='<%# Eval("no_polis") %>'/></td>
              </tr>
              
              <tr>
                <td align="left"><p>Type</p></td>
                <td align="left"><asp:Label ID="obTYPE" runat="server" Text='<%# Eval("Type") %>'/></td>
              </tr>
              
              <tr>
                <td align="left"><p>Qty  </p></td>
                <td align="left">
                    <ajax:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>           
                        <img alt="progress" src="../images/progress/loading.gif"/>       
                        </ProgressTemplate>
                    </ajax:UpdateProgress>

                    
                    <ajax:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>                         
                        
                    </ContentTemplate>
                    </ajax:UpdatePanel>
                    
                    <asp:TextBox ID="obQTY" runat="server" Width="40"></asp:TextBox>
                    <asp:ImageButton id="btADDTOCART" ToolTip="Add to Cart" CommandName="AddToCart" ImageUrl="~/images/toolbar/shopcart2_black30.gif" runat="server" />                    
                </td>
              </tr>      
                
              </table>      
                    
           </ItemTemplate>
           <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  />   
         </asp:Datalist>  
        </asp:Panel>
        </td>
        
        <td width="20%" valign="top">
            <asp:Panel ID="pnGRID2" runat="server">            
            <asp:HiddenField ID="mpSHOPCARTDATA" runat="server"/>
            <asp:HiddenField ID="mpITEMKEYCART" runat="server"/>
            
            <table cellpadding="0" cellspacing="0" border="1" width="100%" bordercolor="gray">
                <tr>
                    <td colspan="3" valign="top" align="center"><p><b>Your Shopping List</b></p></td>
                </tr>
                <tr>
                    <td>Code</td>
                    <td>Type</td>
                    <td align="right">Qty</td>
                </tr>
                
                <%
                    Dim mlTX1 As String
                    Dim mlTX2 As String
                    Dim mlLOOPTX1 As Boolean
                    Dim mlI As Integer
                    Dim mlOBJGF2 As New IASClass.ucmGeneralFunction
                    
                    mlI = 0
                    mlLOOPTX1 = True
                    mlTX1 = mpSHOPCARTDATA.Value
                    Do While mlLOOPTX1 = True
                        mlTX2 = mlOBJGF2.GetStringAtPosition(mlTX1, mlI, "#")
                        mlI = mlI + 1
                        If mlTX2 <> "" Then
                            Response.Write("<tr>")
                            Response.Write("<td>")
                            Response.Write(mlOBJGF2.GetStringAtPosition(mlTX2, 2, "|"))
                            Response.Write("</td>")
                            Response.Write("<td>")
                            Response.Write(mlOBJGF2.GetStringAtPosition(mlTX2, 3, "|"))
                            Response.Write("</td>")
                            Response.Write("<td align=right>")
                            Response.Write(mlOBJGF2.GetStringAtPosition(mlTX2, 4, "|"))
                            Response.Write("</td>")
                            Response.Write("</tr>")
                        Else
                            mlLOOPTX1 = False
                        End If
                    Loop
                 %> 
                 
                 <tr>
                    <td colspan="3" valign="top" align="center">
                        <asp:ImageButton ID="btVIEWCART" ImageUrl="../images/toolbar/shop_bag1_v2_60.gif" ToolTip="View Cart"  OnClientClick="window.open('df_doc_shopbag1.aspx')" runat="server" />            
                        <asp:ImageButton ID="btCLEARCART" ImageUrl="../images/toolbar/shopcart_empty2_80.gif" ToolTip="Clear Cart" runat="server" />
                    </td>
                 </tr>
                 
                 <tr>
                    <td colspan="3" valign="top" align="center">
                        <asp:ImageButton ID="btCHECKOUT" ImageUrl="~/images/toolbar/checkout2_150.gif" ToolTip="Check Out"  runat="server" />                                  
                    </td>
                 </tr>
            </table>
            
            </asp:Panel> 
            
        </td>
    </tr>
</table>

</asp:TableCell>
</asp:TableRow>






</asp:Table>


</form>

<br /><br /><br /><br />

</asp:Content>

