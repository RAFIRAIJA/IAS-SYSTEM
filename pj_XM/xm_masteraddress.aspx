<%@ Page language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="xm_masteraddress.aspx.vb" Inherits="xm_masteraddress"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<form id="form1" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        
<asp:Table id="mlTABLE1" BorderWidth ="0" BorderColor="White" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" />&nbsp;
                <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
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

<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">        
        <tr>
            <td><p><asp:Label ID="lbCODE" runat="server" Text="Code*"></asp:Label></p></td>
            <td><asp:TextBox ID="mlCODE" width="150" runat="server" /></td>
            <td></td> 
            <td valign="top"><asp:Label ID="lbJOINDATE" Text="Join Date" runat="server"></asp:Label></td>
            <td valign="top">
                <asp:TextBox runat="server" ID="mlJOINDATE" Width="100"></asp:TextBox>
                <input id="btJOINDATE" runat="server" onclick="displayCalendar(ctl00_mpCONTENT_mlJOINDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />
                <ajaxtoolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="mlDOCDATE1" TargetControlID="mlJOINDATE" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender> 
                <font color="blue">dd/mm/yyyy</font>
            </td>
        </tr>
         
        <tr>      
            <td><p><asp:Label ID="lbNAME" runat="server" Text="Name*"></asp:Label></p></td>
            <td><asp:TextBox ID="mlNAME" width="300" runat="server" /></td>            
            <td></td> 
            <td><p><asp:Label ID="lbCREDITLIMIT" runat="server" Text="Credit Limit"></asp:Label></p></td>
            <td><asp:TextBox ID="mlCREDITLIMIT" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><asp:Label ID="lbDISTCODE" Text="Owner ID"  runat="server"></asp:Label></td>            
            <td align="left">
                <asp:TextBox ID="mlDISTCODE" runat="server"  width="100"></asp:TextBox>
                <asp:ImageButton ID="btDISTCODE" ToolTip="Member ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="mlDISTNAME"  runat="server"></asp:Label>                                    
            </td>
            <td></td>
            <td><p><asp:Label ID="lbDEFCURR" runat="server" Text="Default Currency"></asp:Label></p></td>
            <td><asp:DropDownList ID="mlDEFCURR" runat="server"></asp:DropDownList></td>            
        </tr>
        
               
        <tr>
            <td><asp:Label ID="lbRECRUITER" Text="Introducer" runat="server"></asp:Label></td>            
            <td>
                <asp:TextBox ID="mlRECRUITERID" runat="server"  width="100" > </asp:TextBox>                                
                <asp:ImageButton ID="btRECRUITER" ToolTip="Recruiter ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="mlRECRUITERNAME" Width="250" Enabled="false" runat="server"></asp:Label>                                                                
            </td>
            <td></td>
            <td><p><asp:Label ID="lbDEFTERM" runat="server" Text="Default Term"></asp:Label></p></td>
            <td><asp:DropDownList ID="mlDEFTERM" runat="server"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td><asp:Label ID="lbUPLINE" Text="UpBranch" runat="server"></asp:Label></td>            
            <td>
                <asp:TextBox ID="mlUPLINE" runat="server"   width="100" ></asp:TextBox>                                
                <asp:ImageButton ID="btUPLINEID" ToolTip="Upline ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="mlUPLINENAME" Width="250" Enabled="false" runat="server"></asp:Label>                                                                
            </td>
            <td></td>
            
            <td><p><asp:Label ID="lbDEFSALES" runat="server" Text="Default Sales"></asp:Label></p></td>
            <td>
                 <asp:TextBox ID="mlDEFSALES" runat="server" width="100"></asp:TextBox>
                <asp:ImageButton ID="btSALES" ToolTip="Sales" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="mlDEFSALESNAME" runat="server"></asp:Label>        
            </td>            
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbIC" runat="server" Text="IC No"></asp:Label></p></td>
            <td><asp:TextBox ID="mlIC" width="200" runat="server" /></td>
            <td></td>
            <td><p><asp:Label ID="lbDEFPIC" runat="server" Text="Person to Contact"></asp:Label></p></td>            
            <td><asp:TextBox ID="mlDEFPIC" width="150" runat="server" /></td>
         </tr>
         
         <tr>
            <td><p><asp:Label ID="lbTAXID" runat="server" Text="Tax ID"></asp:Label></p></td>
            <td><asp:TextBox ID="mlTAXID" width="150" runat="server" /></td>
            <td></td>
            <td><p><asp:Label ID="lbDEFDICSHR" runat="server" Text="Default Disc HR"></asp:Label></p></td>
            <td><asp:TextBox ID="mlDEFDICSHR" width="150" runat="server" /></td>            
        </tr>
        
        <tr>
            <td rowspan="4" valign="top"><p><asp:Label ID="lblADDR" runat="server" Text="Address"></asp:Label></p></td>
            <td rowspan="4"><asp:TextBox ID="mlADDR"  TextMode="MultiLine" width="300"  Height="100"  MaxLength="3999" runat="server" /></td>
            <td></td>
            <td valign="top"><p><asp:Label ID="lbDEFDICSDT" runat="server" Text="Default Disc DT"></asp:Label></p></td>
            <td valign="top"><asp:TextBox ID="mlDEFDICSDT" width="150" runat="server" /></td>
        </tr>
             
            
        <tr>
            <td></td>
            <td><p><asp:Label ID="lbPRICECODE" runat="server" Text="Region Price Code"></asp:Label></p></td>
            <td><asp:DropDownList ID="mlPRICECODE" runat="server" Width="150px"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td></td>
            <td><p><asp:Label ID="lbCOMMISIONP" runat="server" Text="Commision (Ex: 0.05)"></asp:Label></p></td>
            <td><asp:TextBox ID="mlCOMMISIONP" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>
                       
        <tr>
            <td><p><asp:Label ID="lbCITY" runat="server" Text="City"></asp:Label></p></td>
            <td><asp:TextBox ID="mlCITY" width="200" runat="server" /></td>            
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbSTATE" runat="server" Text="State"></asp:Label></p></td>
            <td><asp:DropDownList ID="mlSTATE" runat="server"></asp:DropDownList></td>
            
        </tr> 
        
         <tr>
            <td><p><asp:Label ID="lbCOUNTRY" runat="server" Text="Country"></asp:Label></p></td>
            <td><asp:DropDownList ID="mlCOUNTRY" runat="server"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbZIP" runat="server" Text="Zip Code"></asp:Label></p></td>
            <td><asp:TextBox ID="mlZIP" width="150" runat="server" /></td>
        </tr> 
        
        <tr>
            <td><p><asp:Label ID="lbPHONE1" runat="server" Text="Phone 1"></asp:Label></p></td>
            <td><asp:TextBox ID="mlPHONE1" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbPHONE2" runat="server" Text="Phone 2"></asp:Label></p></td>
            <td><asp:TextBox ID="mlPHONE2" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbFAX" runat="server" Text="Fax"></asp:Label></p></td>
            <td><asp:TextBox ID="mlFAX" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbMOBILE1" runat="server" Text="Mobile 1"></asp:Label></p></td>
            <td><asp:TextBox ID="mlMOBILE1" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbMOBILE2" runat="server" Text="Mobile 2"></asp:Label></p></td>
            <td><asp:TextBox ID="mlMOBILE2" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbEMAIL" runat="server" Text="Email"></asp:Label></p></td>
            <td><asp:TextBox ID="mlEMAIL" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p><asp:Label ID="lbWEBSITE" runat="server" Text="Website"></asp:Label></p></td>
            <td><asp:TextBox ID="mlWEBSITE" width="150" runat="server" /></td>
        </tr>
        
        
                
    </table>    
    <hr />
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>


<asp:Panel ID="pnGRID" runat="server">    

    <p> Status :        
    <asp:LinkButton ID="mlLINKNEW" Text="New" runat="server"></asp:LinkButton> 
    <asp:LinkButton  ID="mlLINKDEL" Text="Delete" runat="server"></asp:LinkButton> 
    </p>
    
    <asp:DataGrid runat="server" ID="mlDATAGRID" 
    HeaderStyle-BackColor="orange"  
    HeaderStyle-VerticalAlign ="top"
    HeaderStyle-HorizontalAlign="Center"
    OnItemCommand="mlDATAGRID_ItemCommand"    
    autogeneratecolumns="false"
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
            <asp:imagebutton id="btBrowseRecord" Runat="server" AlternateText="BrowseRecord" ImageUrl="~/images/toolbar/browse.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.AddressKey")%>' CommandName="BrowseRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
        
        <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btEditRecord" Runat="server" AlternateText="Edit" ImageUrl="~/images/toolbar/edit.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.AddressKey")%>' CommandName="EditRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>   
        
         <asp:TemplateColumn>
            <ItemTemplate>
            <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.AddressKey")%>' CommandName="DeleteRecord" OnClientClick="return confirm('Save Record ?');" >
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>     
        

        <asp:BoundColumn Headertext="Code" DataField="AddressKey"></asp:BoundColumn>        
        <asp:BoundColumn HeaderText="Name" DataField="Name"></asp:BoundColumn>        
        <asp:BoundColumn HeaderText="ID Owner" DataField="CustID"></asp:BoundColumn>        
        <asp:BoundColumn HeaderText="Owner Name" DataField="CustName"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="%" DataField="CommPercentage"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Price Code" DataField="PriceCode"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Phone1" DataField="Phone1"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="Phone2" DataField="Phone2"></asp:BoundColumn>       
        <asp:BoundColumn HeaderText="City" DataField="City"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="User" DataField="Recuserid"></asp:BoundColumn>
        
    </Columns>
 </asp:DataGrid>  
 </asp:Panel>
 
</asp:TableCell>
</asp:TableRow>

</asp:Table>
</form>

<br /><br /><br /><br />
</asp:Content>