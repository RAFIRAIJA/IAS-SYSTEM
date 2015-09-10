<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="hc_media_entry.aspx.vb" Inherits="cm_bo_template1"%>
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
            <td valign="top"><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
            <td valign="top"><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>            
            <td valign="top"><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>            
            <td valign="top"><asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" /></td>            
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
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlTYPE" runat="server"/></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnMENU" runat="server">
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td valign="top"><asp:ImageButton id="btSchedule" ToolTip="Schedule" ImageUrl="~/images/toolbar/Schedule.gif" runat="server" /></td>            
            <td valign="top"><asp:ImageButton id="btMarquee" ToolTip="Marquee" ImageUrl="~/images/toolbar/marquee.gif" runat="server" /></td>
            <td valign="top"><asp:ImageButton id="btVideo" ToolTip="Video" ImageUrl="~/images/toolbar/video.gif" runat="server" /></td> 
        </tr>               
    </table>
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr id="tr1" runat="server">
            <td valign="top"><asp:Label ID="lbDOCDATE1" Text="From Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                
                <asp:TextBox ID="txDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                                                          
                <input id="btDOCDATE1" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                
                <asp:ImageButton runat="Server" ID="btCALDOCDATE1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="bt_ajDOCDATE1" TargetControlID="txDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>                 
                <font color="blue">dd/mm/yyyy</font>
            </td>
        </tr>
        
        <tr id="tr9" runat="server">
            <td valign="top"><asp:Label ID="lbDOCDATE2" Text="To Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                
                <asp:TextBox ID="txDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
                <input id="btDOCDATE2" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                
                <asp:ImageButton runat="Server" ID="btCALDOCDATE2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="bt_ajDOCDATE2" TargetControlID="txDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>                 
                <font color="blue">dd/mm/yyyy</font>
            </td>
        </tr>
        
        <tr id="trDOCNO" runat="server">
            <td valign="top"><asp:Label ID="lbDOCUMENTNO" Text="Doc No" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txDOCUMENTNO" runat="server" Width="150" Enabled="false"></asp:TextBox></td>            
        </tr>
        
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>




<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">        
        <tr>
            <td valign="top"><asp:Label ID="lbTIME1" Text="Time From" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txTIME1" runat="server"> </asp:TextBox></td>  
        </tr>
        
         <tr>
            <td valign="top"><asp:Label ID="lbTIME2" Text="Time To" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txTIME2" runat="server"> </asp:TextBox></td>  
        </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbDESC" Text="Description" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txDESC"  TextMode="MultiLine" width="500"  Height="80"  MaxLength="3999" runat="server" /></td>  
        </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbLOC" Text="Location" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txLOC" runat="server"> </asp:TextBox></td>  
        </tr>
        
        <tr id="trU1" runat="server">
            <td valign="top">                        
                <asp:Label ID="lbUSER" Text="User" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
             <td valign="top">
                <asp:TextBox ID="txUSERID" runat="server"> </asp:TextBox>                                
                <asp:ImageButton ID="btUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="txUSERDESC" Width="250" Enabled="false" runat="server"></asp:Label>                
            </td>
         </tr>
         
         <tr id="trU2" runat="server">
                <td valign="top"></td>            
                <td valign="top"></td>         
                <td valign="top">
                    <asp:Panel ID="pnSEARCHUSERID" runat="server">
                    <asp:Label ID="lbSEARCHUSERID" Text="Employee Name : " runat="server"></asp:Label>
                    <asp:TextBox ID="mpSEARCHUSERID"  width="200" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
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
        
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>



<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL2" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td valign="top"><p><asp:Label ID="lbMARQUEE" runat="server" Text="Marquee Text"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txMARQUEE"  TextMode="MultiLine" width="500"  Height="80"  MaxLength="3999" runat="server" />
            </td>
         </tr>
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL3" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr id="trUP1" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD1_N" Text="File 1 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEPATH1_N" runat="server" Width="300"></asp:TextBox>     
                <asp:TextBox ID="txFILEUPLOAD1_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload1"  runat="server" />         
                <br /><asp:HyperLink ID="lnLINK1" runat="server"></asp:HyperLink>                
            </td>  
        </tr>
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>





<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID1" runat="server">    
    
    <asp:DataGrid runat="server" ID="mlDATAGRID1" 
    CssClass="Grid"
    OnItemCommand="mlDATAGRID1_ItemCommand"
    autogeneratecolumns="true">	    
       
    
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
    </Columns>
 </asp:DataGrid>  
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID2" runat="server">    
    
    <asp:DataGrid runat="server" ID="mlDATAGRID2" 
    CssClass="Grid"
    OnItemCommand="mlDATAGRID2_ItemCommand"
    autogeneratecolumns="true">	    
       
    
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
    </Columns>
 </asp:DataGrid>  
</asp:Panel>
</asp:TableCell>
</asp:TableRow>



<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID3" runat="server">    
    
    <asp:DataGrid runat="server" ID="mlDATAGRID3" 
    CssClass="Grid"
    OnItemCommand="mlDATAGRID3_ItemCommand"
    autogeneratecolumns="true">	    
       
    
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
    </Columns>
 </asp:DataGrid>  
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

