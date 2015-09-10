<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ap_mr_entry.aspx.vb" Inherits="backoffice_ap_mr_entry" title="Untitled Page" %>
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

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILTER" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
        <tr>
            <td valign="top"><p><asp:Label ID="Label1" Text="Template" runat="server"></asp:Label></p></td>
            <td valign="top">
                <asp:DropDownList ID="mpMR_TEMPLATE" runat="server"></asp:DropDownList>                
                <asp:ImageButton ID="btSUBMITTEMPLATE" ToolTip="Submit" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
            </td>  
        </tr>              
    </table>    
    <hr /><br />    
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnNOTEMP" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
         <tr>
            <td valign="top">                        
                <asp:Label ID="lbITEMKEY" Text="Item Code" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHITEMKEY" ToolTip="Item Code" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            
            <td valign="top">
                <asp:TextBox ID="mpITEMKEY" runat="server"> </asp:TextBox>
                <asp:ImageButton ID="btITEMKEY" ToolTip="Item Code" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="mpITEMDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
                <br />
                <asp:ImageButton ID="btITEMKEYADD" ToolTip="Item Code" ImageUrl="~/images/toolbar/shopcart2_black30.gif" runat="server" />
                <br />
            </td>
         </tr>
        
        <tr>                    
            <td valign="top"></td>            
            <td valign="top">
                <asp:Panel ID="pnSEARCHITEMKEY" runat="server">
                <asp:Label ID="lbSEARCHITEMKEY" Text="Item Description : " runat="server"></asp:Label>
                <asp:TextBox ID="mpSEARCHITEMKEY"  width="300" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
                <asp:ImageButton ID="btSEARCHITEMKEYSUBMIT" ToolTip="Search Item Key" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
                
                <asp:DataGrid runat="server" ID="mlDATAGRIDITEMKEY" 
                    HeaderStyle-BackColor="orange"  
                    HeaderStyle-VerticalAlign ="top"
                    HeaderStyle-HorizontalAlign="Center"
                    OnItemCommand="mlDATAGRIDITEMKEY_ItemCommand"        
                    autogeneratecolumns="false">	    
                    
                    <AlternatingItemStyle BackColor="#F9FCA8" />
                    <Columns>  
                        <asp:ButtonColumn  HeaderText = "Code" DataTextField = "No_" ></asp:ButtonColumn>
                        <asp:ButtonColumn HeaderText = "Name"  DataTextField = "Description"></asp:ButtonColumn>
                    </Columns>
                 </asp:DataGrid> 
                </asp:Panel>                       
            </td>
         </tr>      

 
         <tr>   
            <td valign="top"></td>
            <td valign="top">                
                <asp:Label ID="lbITEMCART" Width="500" Enabled="false" runat="server"></asp:Label>                                       
                <asp:HiddenField id="lbITEMCART2" runat="server" />
                <br /><br />                                
                <asp:ImageButton ID="btITEMCART" ToolTip="Create Form" ImageUrl="~/images/toolbar/createform.gif" runat="server" />                                         
                <asp:ImageButton ID="btCLEARCART" ToolTip="Clear" ImageUrl="~/images/toolbar/cleardata.gif" runat="server" OnClientClick="return confirm('Clear Data ?');" />                         
            </td>            
        </tr>       
        
    </table>    
    <hr /><br />    
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">
<table width="100%" cellpadding="0" cellspacing="0" border="0">                 
<tr>
<td valign="top">

    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>
            <td>:</td>
            <td><asp:DropDownList ID="ddENTITY" runat="server"></asp:DropDownList></td>     
        </tr>
                     
        <tr>
            <td valign="top"><asp:Label ID="lbDOCNO" Text="No MR" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="mpDOCUMENTNO" runat="server" Width="150" Enabled="false"></asp:TextBox></td>            
        </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbDOCDATE" Text="Tanggal" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="mpDOCDATE" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATE" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mpDOCDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
                <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" /><br />
                <%--<ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="mpDOCDATE" TargetControlID="mpDOCDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> --%>
            </td>
        </tr>   
        
        <tr>
            <td valign="top">
                <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHSITECARD" ToolTip="Kode Site Card" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="mpSITECARD" runat="server" Width="100"></asp:TextBox>                                                                    
                <asp:ImageButton ID="btSITECARD" ToolTip="Cari Site Card" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                <asp:Label ID="mpSITEDESC" Text="" runat="server"></asp:Label>                                               
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
         
        <tr id="tr1" runat="server" visible="false">
            <td valign="top"><asp:Label ID="lbLOCATION" Text="Alamat Invoice" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="mpLOCATION" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>                
            </td>
        </tr>
      
        
        <tr>
            <td valign="top"><asp:Label ID="lbDEPT" Text="Dept" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:DropDownList ID="mpDEPT" runat="server" Font-Size="11px"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td valign="top"><asp:Label ID="lbPERIOD" Text="Periode MR" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:DropDownList runat="server" ID="ddlPeriodeMR" AutoPostBack="true">
                    <asp:ListItem Value="">Select One</asp:ListItem>
                </asp:DropDownList>&nbsp;-&nbsp;               

                <asp:TextBox Visible="false" ID="mpPERIOD" runat="server" Width="100"></asp:TextBox>                                                                                                    
                <input visible="false" id="btPERIOD" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mpPERIOD,'mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                      
                <asp:ImageButton Visible="false" runat="Server" ID="btCALENDAR2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="mpPERIOD" TargetControlID="mpPERIOD" Format="MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
                <font color="blue">cth MR agst: 08/2013</font>
                
            </td>
        </tr>
        
        
        <tr id="tr2" runat="server">
            <td valign="top"><asp:Label ID="lbPERCENTAGE" Text="MR %" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txPERCENTAGE" runat="server" Width="50" ></asp:TextBox><b>%</b></td>
        </tr>
        
        <tr valign="top">
            <td valign="top"><asp:Label ID="lbDESCRIPTION" Text="Keterangan" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="mpDESC" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox></td>
        </tr> 
        
         <tr id="TR2">
            <td colspan="3"><br /><p><b><i>Diisi bila Request dilakukan oleh Internal HO</i></b></p></td>
        </tr>
        
         <tr id="TR3">
            <td valign="top"><p><asp:Label ID="lbDEPTCODE" runat="server" Text="Dept Code"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:DropDownList ID="ddDEPTCODE" runat="server" Font-Size="11px"></asp:DropDownList></td>
        </tr>
    </table>    
    
    <asp:HiddenField ID="hfPOSTINGUSERID1" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGUSERID2" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGUSERID3" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGUSERID4" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGUSERID5" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGNAME1" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGNAME2" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGNAME3" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGNAME4" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGNAME5" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGDATE1" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGDATE2" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGDATE3" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGDATE4" runat="server"/>
    <asp:HiddenField ID="hfPOSTINGDATE5" runat="server"/>
    <asp:HiddenField ID="hfRECORDSTATUS" runat="server"/>
    
</td>

<td valign="top">&nbsp;</td>
<td valign="top">

    <table width="100%" cellpadding="0" cellspacing="0" border="0">  
        
    <tr>
        <td valign="top"><p><asp:Label ID="lblADDR" runat="server" Text="Alamat Pengiriman"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top">
            <asp:TextBox ID="txADDR"  TextMode="MultiLine" width="300"  Height="100"  MaxLength="3999" runat="server" />
            <br />
            <asp:CheckBox id="mpLOCSAVE" runat="server" Text="Informasikan HO untuk Update Alamat Site Card"  Font-Italic="true"/>
        </td>
    </tr>

    <tr>
        <td valign="top"><p><asp:Label ID="lbCITY" runat="server" Text="Kota"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:TextBox ID="txCITY" width="300" runat="server" /></td>
    </tr> 

    <tr>
        <td valign="top"><p><asp:Label ID="lbSTATE" runat="server" Text="Propinsi"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:DropDownList ID="ddSTATE" runat="server"></asp:DropDownList></td>
    </tr> 

     <tr>
        <td valign="top"><p><asp:Label ID="lbCOUNTRY" runat="server" Text="Negara"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:DropDownList ID="ddCOUNTRY" runat="server"></asp:DropDownList></td>
    </tr>

    <tr>
        <td valign="top"><p><asp:Label ID="lbZIP" runat="server" Text="Kode Pos"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:TextBox ID="txZIP" width="100" runat="server" /></td>
    </tr> 

    <tr>
        <td valign="top"><p><asp:Label ID="lbPHONE1" runat="server" Text="Telp"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:TextBox ID="txPHONE1" width="300" runat="server" /></td>
    </tr>
    
    <tr>
        <td valign="top"><p><asp:Label ID="lbPHONE_PIC" runat="server" Text="PIC & Hp"></asp:Label></p></td>
        <td valign="top">:</td>
        <td valign="top"><asp:TextBox ID="txPHONE1_PIC" width="300" runat="server" /></td>
    </tr>
    
   

    </table>

</td>
</tr>    
</table>
            
    
<hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<br /><br />
<asp:Panel ID="pnGRID2" runat="server">    
   <asp:DataGrid runat="server" ID="mlDATAGRID2" 
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"
    OnItemCommand="mlDATAGRID2_ItemCommand"
    autogeneratecolumns="false">    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns>                
        <asp:BoundColumn HeaderText="No" DataField="Linno"></asp:BoundColumn>        
        
        <asp:templatecolumn headertext="VW">
        <ItemTemplate>        
            <asp:HyperLink Target="_blank"  runat="server" id="Hyperlink2" navigateurl='<%# String.Format("../pj_in/in_imagedoc.aspx?mpID={0}", Eval("ItemKey")) %>' text='Img'></asp:HyperLink>                      
            <asp:Label ID="lblFlagView" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlagView")%>'></asp:Label>
        </ItemTemplate>        
        </asp:templatecolumn>
    
        <asp:BoundColumn HeaderText="Kode" DataField="ItemKey"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Nama_Item" DataField="Description"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Satuan" DataField="Uom"></asp:BoundColumn>            
        
        <asp:TemplateColumn>
        <HeaderTemplate><p>Qty</p></HeaderTemplate>
        <ItemTemplate>
            <asp:TextBox id="mpQTY" runat="server"  Width="50" style="text-align:right" />             
        </ItemTemplate>
        </asp:TemplateColumn>       
        
        <asp:BoundColumn HeaderText="Unit Price" DataField="UnitPrice"   Visible="false"></asp:BoundColumn>            
        <asp:BoundColumn HeaderText="Total Price" DataField="TotalPrice" Visible="false"></asp:BoundColumn>        
                               
        <asp:TemplateColumn>
        <HeaderTemplate><asp:Label ID="lbBAL" Text="Saldo diArea" runat="server" ></asp:Label></HeaderTemplate>
        <ItemTemplate>
            <asp:TextBox ID="mpBAL" width="80" runat="server" style="text-align:right"/>
        </ItemTemplate>
        </asp:TemplateColumn>
                
        <asp:TemplateColumn Visible="false">
        <HeaderTemplate><asp:Label ID="lbSIZE" Text="Ukuran" runat="server"></asp:Label></HeaderTemplate>
        <ItemTemplate>
            <asp:Label ID="mpSIZE" runat="server" ></asp:Label>
        </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn>
        <HeaderTemplate><asp:Label ID="lbDESCDT" Text="Keterangan" runat="server"></asp:Label></HeaderTemplate>
        <ItemTemplate>
            <asp:TextBox ID="mpDESCDT" width="200"   runat="server" />
        </ItemTemplate>
        </asp:TemplateColumn>       
        
    </Columns>
 </asp:DataGrid>   

<table width="20%" id="tbTABLE1" runat="server" visible="false" >
<tr visible="false">
    <td valign="top"><p><b>Total Point :</b></p></td>
    <td align="right"><p><b><asp:Label ID="lbTOTALBV" runat="server" style="text-align:right"></asp:Label></b></p></td>
</tr>

<tr>
    <td valign="top"><p><b>Total Amount :</b></p></td>
    <td align="right"><p><b><asp:Label ID="lbTOTALAMOUNT" runat="server" style="text-align:right"></asp:Label></b></p></td>                                    
</tr>
</table>    
</asp:Panel> 
</asp:TableCell>    
</asp:TableRow>



<asp:TableRow>
<asp:TableCell>
<br /><br />
<asp:Panel ID="pnGRID3" runat="server">    
   <asp:DataGrid runat="server" ID="mlDATAGRID3" 
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"
    autogeneratecolumns="false">    
    <AlternatingItemStyle BackColor="#F9FCA8" />
    <Columns>                
        <asp:BoundColumn HeaderText="No" DataField="Linno"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Kode" DataField="ItemKey"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Nama_Item" DataField="Description"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Ukuran" DataField="fsize"></asp:BoundColumn>            
        
        <asp:TemplateColumn>
        <HeaderTemplate><p>Qty</p></HeaderTemplate>
        <ItemTemplate>
            <asp:TextBox id="mpQTY" runat="server"  Width="50" style="text-align:right" />            
        </ItemTemplate>
        </asp:TemplateColumn>               
        
    </Columns>
 </asp:DataGrid>   
 
</asp:Panel> 
</asp:TableCell>    
</asp:TableRow>




<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">    
<br /><br />    
    <asp:DataGrid runat="server" ID="mlDATAGRID" 
    CssClass="Grid"
    OnItemCommand="mlDATAGRID_ItemCommand"   
    OnItemDataBound ="mlDATAGRID_ItemBound"  
    autogeneratecolumns="true">	    
    
    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <EditItemStyle  CssClass="GridItem" />
    <PagerStyle  CssClass="GridItem" />
    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    <Columns>  
    
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo") & "#" &  DataBinder.Eval(Container,"DataItem.SiteCardID") & "#" &  DataBinder.Eval(Container,"DataItem.MRStatus")  %>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
        
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo") + "#" +  DataBinder.Eval(Container,"DataItem.SiteCardID") & "#" &  DataBinder.Eval(Container,"DataItem.MRStatus")%>' CommandName="EditRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
         <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo") & "#" &  DataBinder.Eval(Container,"DataItem.SiteCardID") & "#" &  DataBinder.Eval(Container,"DataItem.MRStatus")%>' CommandName="DeleteRecord" OnClientClick="return confirm('Delete Record ?');">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>    
        
        <asp:templatecolumn headertext="VW">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="mlLINKVW" navigateurl='<%# String.Format("ap_doc_mr.aspx?mpID={0}", Eval("DocNo")) %>' text="VW"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>
         
        </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell ID="TC1" RUNAT="server">
    <br /><br />
    <p><i>
    Keterangan MR Status :  <br />
    Wait1 = Permintaan Baru, Menunggu Proses Review <br />
    Wait2 = Selesai Review, Menunggu Proses Authorize <br />
    New = Selesai Authorize, Menunggu Proses Procurement <br />
    </i></p>

</asp:TableCell>
</asp:TableRow>
    
</asp:Table>
</form>

<br /><br /><br /><br />

</asp:Content>

