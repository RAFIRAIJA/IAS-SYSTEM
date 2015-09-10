<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="op_user_sitecard.aspx.vb" Inherits="op_user_sitecard"%>
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

<script type="text/javascript">
    function OpenWinLookUpSiteCard(pSiteCardID, pSiteCardName, pSiteCardIDHdn, pSiteCardNameHdn, pJobNo, pJobTaskNo, pJobNoHdn, pJobTaskNoHdn,pEntity, pStyle) {
         var AppInfo = '<%= Request.ServerVariables("PATH_INFO")%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables("SERVER_NAME")%>:<%=Request.ServerVariables("SERVER_PORT")%>/' + App + '/UserController/form/LookUpSiteCard.aspx?hdnSiteCardID=' + pSiteCardIDHdn + '&SitecardID=' + pSiteCardID + '&hdnSiteCardName=' + pSiteCardNameHdn + '&SiteCardName=' + pSiteCardName + '&txtJobNo=' + pJobNo + '&hdnJobNo=' + pJobNoHdn + '&txtJobTaskNo=' + pJobTaskNo + '&hdnJobTaskNo=' + pJobTaskNoHdn + '&Entity=' + pEntity, 'UserLookup', 'left=100, top=10, width=900, height=600, menubar=0, scrollbars=yes');
     }
</script>

<form id="mpFORM" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />
<input type="hidden" id="hdnSiteCardID" runat="server" name="hdnSiteCardID" class="inptype"/>
<input type="hidden" id="hdnSiteCardName" runat="server" name="hdnSiteCardName" class="inptype"/>
<input type="hidden" id="hdnJobNo" runat="server" name="hdnJobNo" class="inptype"/>
<input type="hidden" id="hdnJobTaskNo" runat="server" name="hdnJobTaskNo" class="inptype"/>


<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td>
                <asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
                <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />
            </td>
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
            <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddENTITY" runat="server"></asp:DropDownList>
            </td>
        </tr>
            
        <tr>
            <td valign="top"><asp:Label ID="lbDOCNO" Text="Doc No" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="mpDOCUMENTNO" runat="server" Width="150" Enabled="false"></asp:TextBox></td>            
        </tr>
        
         <tr>
            <td valign="top">                        
                <asp:Label ID="lbUSER" Text="Employee ID" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
             <td valign="top">
                <asp:TextBox ID="mpUSERID" runat="server"> </asp:TextBox>                                
                <asp:ImageButton ID="btUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="mpUSERDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
            </td>
         </tr>
         
         <tr>
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
         
        <tr>
            <td valign="top">
                Job [Task] No
            </td>               
            <td valign="top">:</td>     
            <td valign="top">
                <%--<uc1:ucLUSiteCard runat="server" id="LUSiteCard"></uc1:ucLUSiteCard>--%>
                <asp:TextBox ID="mpJobNo" runat="server" Width="80" ></asp:TextBox>&nbsp;-&nbsp;
                <asp:TextBox ID="mpJobTaskNo" runat="server" Width="60" ></asp:TextBox>
                <asp:hyperlink id="hpLookup" runat="server" imageurl="~/images/toolbar/find.jpg"></asp:hyperlink>
            </td>                    
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHSITECARD" ToolTip="Kode Site Card" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" Visible ="false"  />                                
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="mpSITECARD" runat="server" Width="100"></asp:TextBox>                                                                    
                <asp:ImageButton ID="btSITECARD" ToolTip="Cari Site Card" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                <asp:Label ID="mpSITEDESC" Text="" runat="server"></asp:Label>
                <br /><i>ALL for All Site Card</i>
            </td>
        </tr>   
           
        <tr>                    
            <td></td>            
            <td></td> 
            <td>
                <asp:Panel ID="pnSEARCHSITECARD" runat="server">                            
                    <asp:Label ID="lbSEARCHSITECARD" Text="Nama Site : " runat="server"></asp:Label>
                    <asp:TextBox ID="mlSEARCHSITECARD" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
                    <asp:ImageButton ID="btSEARCHSITECARDSUBMIT" ToolTip="Search Agent ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
                    
                    <asp:DataGrid runat="server" ID="mlDATAGRIDSITECARD" 
                        HeaderStyle-BackColor="orange"  
                        HeaderStyle-VerticalAlign ="top"
                        HeaderStyle-HorizontalAlign="Center"
                        OnItemCommand="mlDATAGRIDSITECARD_ItemCommand"        
                        autogeneratecolumns="false">	    
                        
                        <AlternatingItemStyle BackColor="#F9FCA8" />
                        <Columns>  
                            <asp:ButtonColumn  HeaderText = "Kode" DataTextField = "LineNo_" ></asp:ButtonColumn>
                            <asp:ButtonColumn HeaderText = "Nama"  DataTextField = "SearchName"></asp:ButtonColumn>
                        </Columns>
                     </asp:DataGrid> 
                </asp:Panel>            
            </td>
         </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbMRLEVEL" Text="MR Level" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:DropDownList ID="mpMRLEVEL" runat="server"></asp:DropDownList></td>            
        </tr>       
        
        
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>




<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">    
   <asp:LinkButton  ID="lnVIEWNORMAL" Text="Normal View |" runat="server"></asp:LinkButton> 
   <asp:LinkButton  ID="lnVIEWUSER" Text="View User Assignment |" runat="server" Visible="false"></asp:LinkButton> 
   <asp:LinkButton  ID="lnVIEWSITE" Text="View Site Card Assignment |" runat="server" Visible="false"></asp:LinkButton> 
   
    
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
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo") %>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
        
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>' CommandName="EditRecord">
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

