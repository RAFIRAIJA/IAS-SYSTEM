<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="fs_file_upload.aspx.vb" Inherits="fs_file_upload"%>
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

<form id="mpFORM" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
                <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
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


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td><p><asp:Label ID="Label1" Text="DocID" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txDOCUMENTNO" runat="server" Width="150" Enabled="false"></asp:TextBox></td>            
        </tr>
        
        <tr>
            <td><asp:Label ID="lbDOCDATE1" Text="Doc Date" runat="server"></asp:Label></td>
            <td>:</td>
            <td>                             
                <asp:TextBox ID="txDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATE1" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="bt_ajDOCDATE1" TargetControlID="txDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                <font color="blue">dd/mm/yyyy</font>
            </td>
        </tr>
             
        <tr>
            <td valign="top"><p><asp:Label ID="lbTYPE" Text="Group File" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:DropDownList ID="ddTYPE" runat="server"></asp:DropDownList></td>  
        </tr>
        
        <tr id="TR1" runat="server">
            <td valign="top"><p><asp:Label ID="lbOPENPWD" Text="Password to Open" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txOPENPWD" runat="server" Width="150"></asp:TextBox></td>            
        </tr>
        
                
        <tr>
            <td valign="top"><p><asp:Label ID="lbDESCRIPTION" Text="Description" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txDESCRIPTION"  TextMode="MultiLine" width="350"  Height="80"  MaxLength="3999" runat="server" /></td>  
        </tr>
        
        <tr><td colspan="3"><br /><hr /><br /></td></tr>
        
        <tr><td colspan="3"><asp:Label ID="lbAUTH" Text="Authorize Employee"  Font-Bold="true"  runat="server"></asp:Label></td></tr>
        
        <tr id="trU0" runat="server">
            <td colspan="3">
                <asp:DataGrid runat="server" ID="mlDATAGRID3" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"    
                OnItemCommand="mlDATAGRID3_ItemCommand"
                autogeneratecolumns="true">	    
                
                               
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>                  
                    
                </Columns>
             </asp:DataGrid>  
            </td>
        </tr>
        
        
        <tr id="trU1" runat="server">
            <td valign="top">                        
                <asp:Label ID="lbUSER" Text="Employee ID" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
             <td valign="top">
                <asp:TextBox ID="txUSERID" runat="server"> </asp:TextBox>                                
                <asp:ImageButton ID="btUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="txUSERDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
                <asp:Label ID="txUSEREMAIL" Width="250" Enabled="false" runat="server"></asp:Label> 
            </td>
         </tr>
         
         <tr id="trU2" runat="server">
            <td></td>            
            <td></td>         
            <td>
                <asp:Panel ID="pnSEARCHUSERID" runat="server">
                <asp:Label ID="lbSEARCHUSERID" Text="Employee Name : " runat="server"></asp:Label>
                <asp:TextBox ID="mpSEARCHUSERID"  width="300" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
                <asp:ImageButton ID="btSEARCHUSERIDSUBMIT" ToolTip="Search Staff ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
                
                <asp:DataGrid runat="server" ID="mlDATAGRIDUSERID" 
                    HeaderStyle-BackColor="orange"  
                    HeaderStyle-VerticalAlign ="top"
                    HeaderStyle-HorizontalAlign="Center"
                    OnItemCommand="mlDATAGRIDUSERID_ItemCommand"        
                    autogeneratecolumns="false">	    
                    
                    <AlternatingItemStyle BackColor="#F9FCA8" />
                    <Columns>  
                        <asp:ButtonColumn  HeaderText = "EmployeeID" DataTextField = "UserID" ></asp:ButtonColumn>
                        <asp:ButtonColumn HeaderText = "Name"  DataTextField = "Name"></asp:ButtonColumn>                        
                    </Columns>
                 </asp:DataGrid> 
                </asp:Panel>                       
            </td>         
         </tr>
         
         
         <tr id="trU3" runat="server">   
            <td valign="top"></td>
            <td></td>
            <td valign="top">                
                <asp:Label ID="lbITEMCART" Width="500" Enabled="false" runat="server"></asp:Label>                                       
                <asp:HiddenField id="lbITEMCART2" runat="server" />
                <br /><br />      
                <asp:ImageButton ID="btITEMKEYADD" ToolTip="Item Code" ImageUrl="~/images/toolbar/Add.gif" runat="server" />                                                          
                <asp:ImageButton ID="btCLEARCART" ToolTip="Clear" ImageUrl="~/images/toolbar/cleardata.gif" runat="server" OnClientClick="return confirm('Clear Data ?');" />                         
            </td>            
        </tr>       
        
        
        <tr> <td colspan="3"><br /><hr /><br /></td></tr>
        
        <tr id="trUP0" runat="server">
            <td colspan="3">
                <asp:DataGrid runat="server" ID="mlDATAGRID2" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"    
                OnItemCommand="mlDATAGRID2_ItemCommand"
                autogeneratecolumns="true">	                   
                               
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>                    
                </Columns>
             </asp:DataGrid>  
            </td>
        </tr>
        
                
        <tr id="trUP1" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD1_N" Text="File 1 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD1_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload1"  runat="server" />         
                <br /><asp:HyperLink ID="lnLINK1" runat="server">ssss</asp:HyperLink>                
            </td>  
        </tr>
        
               
        <tr id="trUP2" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD2_N" Text="File 2 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD2_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload2" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK2" runat="server"></asp:HyperLink>
            </td>  
        </tr>
                
        <tr id="trUP3" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD3_N" Text="File 3 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD3_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload3" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK3" runat="server"></asp:HyperLink>
            </td>  
        </tr>
        
        <tr id="trUP4" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD4_N" Text="File 4 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD4_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload4" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK4" runat="server"></asp:HyperLink>
            </td>  
        </tr>
        
               
        <tr id="trUP5" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD5_N" Text="File 5 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD5_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload5" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK5" runat="server"></asp:HyperLink>
            </td>  
        </tr>                     
               
        
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">    
    
    <asp:DataGrid runat="server" ID="mlDATAGRID" 
    OnItemCommand="mlDATAGRID_ItemCommand"
    autogeneratecolumns="true"
    CssClass="Grid"
    >	    
    
    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <EditItemStyle  CssClass="GridItem" />
    <PagerStyle  CssClass="GridItem" />
    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    <Columns>  
    
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
                        
         <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>' CommandName="DeleteRecord" OnClientClick="return confirm('Delete Record ?');">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>       
        
        <asp:templatecolumn headertext="LG">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="lnLINK1" navigateurl='<%# String.Format("fs_doc_file_downloadlog.aspx?mpID={0}", Eval("DocNo")) %>' text="LG"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>
        
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

