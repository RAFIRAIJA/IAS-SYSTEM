<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" CodeFile="hr_allowance.aspx.vb" Inherits="hr_allowance"%>
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
            <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
            <td><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>
            <td><asp:ImageButton id="btDeleteRecord" ToolTip="DeleteRecord" ImageUrl="~/images/toolbar/delete.jpg" runat="server" OnClientClick="return confirm('Delete Record ?');" /></td>
            <td><asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" /></td>
            <td><asp:ImageButton id="btEditRecord" ToolTip="EditRecord" ImageUrl="~/images/toolbar/edit.jpg" runat="server" /></td>            
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


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddENTITY" runat="server"></asp:DropDownList></td>     
        </tr>
                     
        <tr>
            <td valign="top"><asp:Label ID="lbDOCNO" Text="No Dokumen" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txDOCUMENTNO" runat="server" Width="150" Enabled="false"></asp:TextBox></td>            
        </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbDOCDATE" Text="Tanggal" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txDOCDATE" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATE" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_txDOCDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
                <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="txDOCDATE" TargetControlID="txDOCDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            </td>
        </tr>   
        
        <tr>
            <td valign="top">
                <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHSITECARD" ToolTip="Kode Site Card" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txSITECARD" runat="server" Width="100"></asp:TextBox>                                                                    
                <asp:ImageButton ID="btSITECARD" ToolTip="Cari Site Card" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                <asp:Label ID="lbSITEDESC" Text="" runat="server"></asp:Label>                                               
            </td>
        </tr>
        
        <tr>                    
            <td valign="top"></td>            
            <td valign="top"></td> 
            <td valign="top">
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
                            <asp:ButtonColumn  HeaderText = "Kode" DataTextField = "SiteCardID" ></asp:ButtonColumn>
                            <asp:ButtonColumn HeaderText = "Nama"  DataTextField = "SiteCardName"></asp:ButtonColumn>
                        </Columns>
                     </asp:DataGrid> 
                </asp:Panel>            
            </td>
         </tr>
         
         <tr>
            <td valign="top"><asp:Label ID="lbDOCDATEFRM" Text="Tanggal Penggajian Dari" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txDOCDATEFRM" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATEFRM" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_txDOCDATEFRM,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
                <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="txDOCDATEFRM" TargetControlID="txDOCDATEFRM" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            </td>
        </tr>   
        
        <tr>
            <td valign="top"><asp:Label ID="lbDOCDATETO" Text="Tanggal Penggajian Sampai" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txDOCDATETO" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATETO" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_txDOCDATETO,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
                <asp:ImageButton runat="Server" ID="ImageButton2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="txDOCDATETO" TargetControlID="txDOCDATETO" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
            </td>
        </tr>   
        
        <tr valign="top">
            <td valign="top"><asp:Label ID="lbDESCRIPTION" Text="Keterangan" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txDESC" runat="server" Width="500" Height="80" TextMode="MultiLine"></asp:TextBox></td>
        </tr> 
        
        <tr>
            <td><asp:Label ID="lbROWS" Text="Jumlah Baris" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td>
                <asp:TextBox ID="txROWSNO" width="50" runat="server" />                
                <asp:ImageButton ID="btROWSNO" ToolTip="Row No" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
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
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"    
    OnItemCommand="mlDATAGRID_ItemCommand"
    autogeneratecolumns="true">	    
    
    
    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns>  
    
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>' CommandName="BrowseRecord">
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


<asp:TableRow>
<asp:TableCell>

<asp:Panel ID="pnGRID3" runat="server">    
   <asp:DataGrid runat="server" ID="mlDATAGRID3" 
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"
    OnItemCommand="mlDATAGRID3_ItemCommand"        
    autogeneratecolumns="false">    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns> 
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Linno")%>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
                
        <asp:BoundColumn HeaderText="No" DataField="Linno"></asp:BoundColumn>
        
        
         <asp:TemplateColumn>
            <ItemTemplate>                
                <asp:TextBox ID="txUSERID" runat="server"> </asp:TextBox>
                <asp:ImageButton ID="btSEARCHUSERID" ToolTip="Item Code" ImageUrl="~/images/toolbar/zoom.jpg" runat="server"  CommandName="Showpanel_UserID"  CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Linno")%>'/>
                <asp:ImageButton ID="btUSERID" ToolTip="Item Code" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" CommandName="Complete_Name"  CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Linno")%>'/>
                <asp:Label ID="txUSERDESC" Width="250" Enabled="false" runat="server"></asp:Label>                 
             </ItemTemplate>
        </asp:TemplateColumn>
            
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:Panel ID="pnSEARCHUSERID" runat="server">
                <asp:Label ID="lbSEARCHUSERID" Text="Item Description : " runat="server"></asp:Label>
                <asp:TextBox ID="mpSEARCHUSERID"  width="300" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
                <asp:ImageButton ID="btSEARCHUSERIDSUBMIT" ToolTip="Search Item Key" ImageUrl="~/images/toolbar/zoom.jpg" runat="server"  CommandName="SEARCHUSERIDSUBMIT"  CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Linno")%>'/>
                
                <asp:DataGrid runat="server" ID="mlDATAGRIDUSERID" 
                    HeaderStyle-BackColor="orange"  
                    HeaderStyle-VerticalAlign ="top"
                    HeaderStyle-HorizontalAlign="Center"                    
                    autogeneratecolumns="false">	    
                    
                    <AlternatingItemStyle BackColor="#F9FCA8" />
                    <Columns>  
                        <asp:ButtonColumn  HeaderText = "Code" DataTextField = "No_" ></asp:ButtonColumn>
                        <asp:ButtonColumn HeaderText = "Name"  DataTextField = "Description"></asp:ButtonColumn>
                    </Columns>
                 </asp:DataGrid> 
                </asp:Panel>                       
           </ItemTemplate>
        </asp:TemplateColumn>    

        <asp:TemplateColumn>
        <HeaderTemplate><asp:Label ID="lbTYPE" Text="Tipe" runat="server"></asp:Label></HeaderTemplate>
        <ItemTemplate>
            <asp:DropDownList ID="ddTYPE" runat="server"></asp:DropDownList>
        </ItemTemplate>
        </asp:TemplateColumn>
        
        
        <asp:TemplateColumn>
        <HeaderTemplate><asp:Label ID="lbNILAIALLOWANCE" Text="Nilai Allowance" runat="server"></asp:Label></HeaderTemplate>
        <ItemTemplate>
            <asp:TextBox ID="txNILAIALLOWANCE" width="100"  MaxLength="50"  style="text-align:right" runat="server" />
        </ItemTemplate>
        </asp:TemplateColumn>
        
        
        <asp:TemplateColumn>
        <HeaderTemplate><asp:Label ID="lbDESCDET" Text="Description" runat="server"></asp:Label></HeaderTemplate>
        <ItemTemplate>                    
            <asp:TextBox ID="txDESC" width="300"  MaxLength="254" runat="server" />
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

