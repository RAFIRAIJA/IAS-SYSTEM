<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ct_transfer_task.aspx.vb" Inherits="ct_transfer_task"%>
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
         <td valign="top">
                <asp:Label ID="lbDOCUMENTNO" Text="Track No" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txDOCUMENTNO" runat="server" Width="150"></asp:TextBox>
                <asp:Label ID="lbLINNO" runat="server" Visible="false"></asp:Label>                    
            </td>
        </tr>
                                  
        <tr>
            <td valign="top"><asp:Label ID="lbDOCDATE1" Text="Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                             
                <asp:TextBox ID="txDOCDATE" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATE1" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                <asp:ImageButton runat="Server" ID="btCALENDAR1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="bt_ajDOCDATE1" TargetControlID="txDOCDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                <font color="blue">dd/mm/yyyy</font>
            </td>
        </tr>
               
        <tr>
            <td valign="top">
                <asp:Label ID="lbREFFNO" runat="server" Text="Contract No"></asp:Label>
                <asp:ImageButton ID="btSEARCHCONTRACT" ToolTip="Contract No" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
             </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txREFFNO" runat="server" Width="200"></asp:TextBox>
                <asp:ImageButton ID="btREFF" ToolTip="Submit Document" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />                      
                <asp:Label ID="lbREFFDOCNO" Text="" runat="server"></asp:Label>
                <asp:HiddenField ID="hfFILEDOCNO" runat="server"/>
            </td>                            
        </tr>
        
         <tr>                    
            <td valign="top"></td>            
            <td valign="top"></td> 
            <td valign="top">
                <asp:Panel ID="pnSEARCHCONTRACT" runat="server">                            
                    <asp:Label ID="lbSEARCHCONTRACT" Text="Contract No : " runat="server"></asp:Label>
                    <asp:TextBox ID="mlSEARCHCONTRACT" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
                    <asp:ImageButton ID="btSEARCHCONTRACTSUBMIT" ToolTip="Contract No" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
                    
                    <asp:DataGrid runat="server" ID="mlDATAGRIDCONTRACT" 
                        HeaderStyle-BackColor="orange"  
                        HeaderStyle-VerticalAlign ="top"
                        HeaderStyle-HorizontalAlign="Center"
                        OnItemCommand="mlDATAGRIDCONTRACT_ItemCommand"        
                        autogeneratecolumns="false">	    
                        
                        <AlternatingItemStyle BackColor="#F9FCA8" />
                        <Columns>  
                            <asp:ButtonColumn  HeaderText = "Contract_No" DataTextField = "Field_ID" ></asp:ButtonColumn>
                            <asp:ButtonColumn HeaderText = "Doc_No"  DataTextField = "Field_Name"></asp:ButtonColumn>
                        </Columns>
                     </asp:DataGrid> 
                </asp:Panel>            
            </td>
        </tr>
    
        <tr id="tr1" runat="server">
            <td valign="top"><p><asp:Label ID="lbLCUST" Text="Customer" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbCUSTL" runat="server"></asp:Label>
                <asp:Label ID="lbSPACE1" TExt = " - " runat="server"></asp:Label>
                <asp:Label ID="lbCUSTDESC" Text="" runat="server"></asp:Label>                
            </td>
         </tr> 
            
         <tr id="tr2" runat="server">
            <td valign="top">
                <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>        
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:Label ID="lbSITECARDL" runat="server"></asp:Label>                                                                    
                <asp:Label ID="Label1" TExt = " - " runat="server"></asp:Label>
                <asp:Label ID="lbSITEDESC" Text="" runat="server"></asp:Label>
                <br /><asp:Label ID="lbPRODUCTID" Text="" runat="server"></asp:Label>
            </td>
        </tr>
           
         
         <tr><td colspan="3"><hr /><br /></td></tr>    
         
         <tr>
            <td valign="top">                        
                <asp:Label ID="lbUSERF" Text="From Employee ID" runat="server"></asp:Label>                
            </td>
            <td valign="top">:</td>
             <td valign="top">
                <asp:TextBox ID="txUSERIDF" runat="server"></asp:TextBox>                                
                <asp:ImageButton ID="btUSERIDF" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="lbUSERDESCF" Width="250" Enabled="false" runat="server"></asp:Label> 
            </td>
         </tr>
         
         
         <tr>
            <td valign="top">                        
                <asp:Label ID="lbUSER" Text="To Employee ID" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
             <td valign="top">
                <asp:TextBox ID="txUSERID" runat="server"> </asp:TextBox>                                
                <asp:ImageButton ID="btUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="txUSERDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
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
            <td valign="top"><p><asp:Label ID="lbEMAILDEST" runat="server" Text="Email"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txEMAILDEST" runat="server" Width="400"></asp:TextBox> 
                <font color="blue" size="2px"><i>Use Comma (,) to separate the email address</i></font>
            </td>
         </tr>
         
         <tr>
            <td valign="top"><p><asp:Label ID="lbGROUPTASK" runat="server" Text="Group Task"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:DropDownList ID="ddGROUPTASK" runat="server" AutoPostBack="true"></asp:DropDownList>
                <asp:Label ID="lbLONGTASK" runat="server" Visible="false"></asp:Label>
            </td>
         </tr>
         
         <tr>
            <td valign="top"><asp:Label ID="lbDOCDATE2" Text="Deadline Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                             
                <asp:TextBox ID="txDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btDOCDATE2" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                <asp:ImageButton runat="Server" ID="btDOCDATE22" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="bt_ajDOCDATE2" TargetControlID="txDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                <font color="blue">dd/mm/yyyy</font>
            </td>
        </tr>
         
         <tr>
            <td valign="top"><p><asp:Label ID="lbDESCRIPTION" runat="server" Text="Remarks"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txDESCRIPTION"  TextMode="MultiLine" width="400"  Height="80"  MaxLength="3999" runat="server" />
            </td>
         </tr>         
                
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>




<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID4" runat="server">        
    <p>Sent Task</p>
    <asp:DataGrid runat="server" ID="mlDATAGRID4" 
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"    
    OnItemCommand="mlDATAGRID4_ItemCommand"
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
              
        
        <asp:templatecolumn headertext="VW">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="mlLINKVW" navigateurl='<%# String.Format("ct_doc_ctentry.aspx?mpID={0}", Eval("ReffNo")) %>' text="VW"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>                
        
        <asp:templatecolumn headertext="No" >
        <ItemTemplate>                    
            <%#Container.ItemIndex + 1%>
        </ItemTemplate>        
    </asp:templatecolumn>            
        
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<br /><br />
<asp:Panel ID="pnGRID3" runat="server">    
   <asp:DataGrid runat="server" ID="mlDATAGRID3"     
    autogeneratecolumns="true"
    CssClass="Grid"
    >    
    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <EditItemStyle  CssClass="GridItem" />
    <PagerStyle  CssClass="GridItem" />
    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    <Columns>                
                     
    </Columns>
 </asp:DataGrid>   

</asp:Panel> 
<br /><br />
</asp:TableCell>    
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnGRID" runat="server">        
    <p>Receive Task</p>
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
        
        <asp:templatecolumn headertext="VW">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="mlLINKVW" navigateurl='<%# String.Format("ct_doc_ctentry.aspx?mpID={0}", Eval("ReffNo")) %>' text="VW"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>        
        
        <asp:TemplateColumn>
            <ItemTemplate>            
            <input type="Radio" name="rb2"  value='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>'/>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:templatecolumn headertext="No" >
        <ItemTemplate>                    
            <%#Container.ItemIndex + 1%>
        </ItemTemplate>        
    </asp:templatecolumn>            
             
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<br /><br />
<asp:Panel ID="pnGRID2" runat="server">    
   <asp:DataGrid runat="server" ID="mlDATAGRID2"     
    OnItemDataBound ="mlDATAGRID2_ItemBound"        
    autogeneratecolumns="true"
    CssClass="Grid"
    > 
       
    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <EditItemStyle  CssClass="GridItem" />
    <PagerStyle  CssClass="GridItem" />
    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    <Columns>                
        
    </Columns>
 </asp:DataGrid>   

</asp:Panel> 
<br /><br />
</asp:TableCell>    
</asp:TableRow>



<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnNOTETYPE" runat="server">    
    <br />
    <table width="30%" cellpadding="1" cellspacing="1" border="0">                 
          <tr>
            <td><asp:ImageButton ID="btNNOTE" ToolTip="Write Note" ImageUrl="~/images/toolbar/note.gif" runat="server" /></td>
            <td><asp:ImageButton ID="btNSENTMAIL" ToolTip="Mail Sent Info" ImageUrl="~/images/toolbar/shipinfo.gif" runat="server" /></td>
            <td><asp:ImageButton ID="btNSENTRECEIVE" ToolTip="Mail Receive Info" ImageUrl="~/images/toolbar/receiveinfo.gif" runat="server" /></td>
            <td><asp:ImageButton ID="btNTRANSFER" ToolTip="Transfer Task" ImageUrl="~/images/toolbar/transfertask.gif" runat="server" /></td>
            <td><asp:ImageButton ID="btNTAKE" ToolTip="Take Over Task" ImageUrl="~/images/toolbar/taketask.gif" runat="server" /></td>
            <td><asp:ImageButton ID="btNTFINISH" ToolTip="Finish Task" ImageUrl="~/images/toolbar/finishtask.gif" runat="server" OnClientClick="return confirm('Finish Task ?');" /></td>            
         </tr>                         
    </table>    
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>



<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnNOTE" runat="server"> 
    <asp:HiddenField  ID="hfTYPE" runat="server"/>    
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                 
          <tr id="trR1" runat="server">
            <td valign="top"><asp:Label ID="lbDOCDATER" Text="Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                             
                <asp:TextBox ID="txDOCDATER" runat="server" Width="100" enabled="false"></asp:TextBox>                                                                    
                <input id="btDOCDATER" runat="server" visible="false" onclick="displayCalendar(mpCONTENT_txDOCDATER,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                <asp:ImageButton runat="Server" visible="false" ID="btCALENDAR2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="bt_ajDOCDATER" TargetControlID="txDOCDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                <font color="blue">dd/mm/yyyy</font>
            </td>
         </tr>
        
         
         <tr id="trR4" runat="server">
            <td valign="top"><asp:Label ID="lbDOCTYPER" Text="Document Type" runat="server"></asp:Label></td>
            <td valign="top">:</td>            
            <td valign="top"><asp:DropDownList ID="ddDOCTYPER" runat="server" Font-Size="11px"></asp:DropDownList></td>
        </tr>
        
         <tr id="trR5" runat="server">
            <td valign="top"><asp:Label ID="lbCOURIER_TYPER" Text="Courier Type" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top"><asp:DropDownList ID="ddCOURIER_TYPE" runat="server" Font-Size="11px"></asp:DropDownList></td>
        </tr>
        
                        
        <tr id="trR6" runat="server">
            <td valign="top">
                <asp:Label ID="lb_COURIER_NAMER" Text="Courier Name" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="tx_COURIER_NAMER" runat="server" Width="150"></asp:TextBox>                
            </td>
        </tr>
        
         
         <tr id="trR2" runat="server">
            <td valign="top">
                <asp:Label ID="lb_DOCNOR" Text="Document No" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="tx_DOCNOR" runat="server" Width="150"></asp:TextBox>
            </td>
        </tr>
        
        <tr id="trR3" runat="server">
            <td valign="top"><asp:Label ID="lbSENDDATER" Text="Date" runat="server"></asp:Label></td>
            <td valign="top">:</td>
            <td valign="top">                             
                <asp:TextBox ID="txSENDDATER" runat="server" Width="100"></asp:TextBox>                                                                    
                <input id="btSENDDATER" runat="server" onclick="displayCalendar(mpCONTENT_txSENDDATER,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                <asp:ImageButton runat="Server" ID="btSENDCAL2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="bt_ajSENDDATER" TargetControlID="txDOCDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                <font color="blue">dd/mm/yyyy</font>
            </td>
         </tr>
               
        <tr id="trR7" runat="server">
            <td valign="top">
                <asp:Label ID="lb_COURIER_PIC_ID" Text="Courier PIC ID" runat="server"></asp:Label>
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="tx_COURIER_PIC_ID" runat="server" Width="150"></asp:TextBox>
            </td>
        </tr>
        
        
        <tr id="trR8" runat="server">
            <td valign="top">
                <asp:Label ID="lb_COURIER_PIC_NAME" Text="Courier PIC Name" runat="server"></asp:Label>                    
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="tx_COURIER_PIC_NAME" runat="server" Width="150"></asp:TextBox>                
            </td>
        </tr>
       
       
        <tr id="trR9" runat="server">
            <td valign="top">
                <asp:Label ID="lb_COURIER_PIC_PHONE" Text="Courier PIC Phone" runat="server"></asp:Label>
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="tx_COURIER_PIC_PHONE" runat="server" Width="150"></asp:TextBox>
            </td>
        </tr>

        <tr id="trR10" runat="server">
            <td valign="top">
                <asp:Label ID="lb_COURIER_PIC_POS" Text="Courier PIC Position" runat="server"></asp:Label>
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="tx_COURIER_PIC_POS" runat="server" Width="150"></asp:TextBox>
            </td>
        </tr>
        
                       
         <tr id="trR11" runat="server">
            <td valign="top"><p><asp:Label ID="lbDESCRIPTIONR" runat="server" Text="Additional Note"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txDESCRIPTIONR"  TextMode="MultiLine" width="600"  Height="120"  MaxLength="3999" runat="server" />
            </td>
         </tr>                
         
         <tr id="trR12" runat="server">
            <td valign="top"></td>
            <td valign="top"></td>
            <td valign="top">
                <asp:ImageButton ID="btNOTE" ToolTip="Submit" ImageUrl="~/images/toolbar/submit.gif" runat="server" />                                         
            </td>
         </tr>
         
    </table>    
    <hr /><br />                
    
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

