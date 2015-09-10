<%@ Page language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="mk_file.aspx.vb" Inherits="mk_file" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>

<form id="form1" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />        
        
        
<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">

<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" /></td>
            <td><asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" /></td>
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
<asp:TableRow><asp:TableCell><asp:HiddenField ID="mlSYSCODE"  runat="server"/></asp:TableCell></asp:TableRow>
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow>


<asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
<asp:TableRow>
<asp:TableCell BorderWidth="0">
<asp:Panel ID="pnFILL" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">                
        <tr>
            <td><p>Sys ID</p></td>            
            <td><asp:DropDownList ID="mlSYSID" runat="server"></asp:DropDownList></td>
        </tr>
        
         <tr>
            <td><p>Type</p></td>            
            <td><asp:DropDownList ID="mlTYPE" runat="server"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td><p>Doc No</p></td>
            <td><asp:TextBox ID="mlDOCNO" width="150" runat="server" /></td>
        </tr>
        
        <tr>
            <td><p>Doc Date</p></td>
            <td>                
                <asp:TextBox ID="mlDOCDATE" runat="server" Width="100"></asp:TextBox>                                                                                                          
                <input id="Button1" runat="server" onclick="displayCalendar(mpCONTENT_mlDOCDATE,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" format="dd/MM/yyyy" TargetControlID="mlDOCDATE" />               
                
            </td>
        </tr>
        
        <tr>
            <td valign="top"><p>Subject</p></td>
            <td><asp:TextBox ID="mlSUBJECT" width="500"  MaxLength="1024" runat="server" /></td>
        </tr>
        
        <tr>
            <td valign="top"><p>Short Description</p></td>
            <td><asp:TextBox ID="mlHEADER" width="500"  MaxLength="1024" runat="server" /></td>
        </tr>
        
        <tr>
            <td colspan="2" align="left" valign="top"><br /><hr /><br /></td>
        </tr>
        
        <tr>
            <td><asp:Label ID="lbDOCDATE1" Text="Date (From)" runat="server"></asp:Label></td>            
            <td>
                <asp:TextBox ID="mlDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                                                          
                <input id="btJOINDATE1" runat="server" onclick="displayCalendar(mpCONTENT_mlDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                                
                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" format="dd/MM/yyyy" TargetControlID="mlDOCDATE1" />
            </td>
        </tr>
        
        <tr>
            <td><asp:Label ID="lbDOCDATE2" Text="Date (To)" runat="server"></asp:Label></td>            
            <td>                
                <asp:TextBox ID="mlDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
                <input id="btJOINDATE2" runat="server" onclick="displayCalendar(mpCONTENT_mlDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow" />                
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" format="dd/MM/yyyy" TargetControlID="mlDOCDATE2" />
            </td>
        </tr>
        
        <tr>
            <td colspan="2" align="left" valign="top"><br /><hr /><br /></td>
        </tr>        
        
        <tr>
            <td valign="top"><p>Upload File </p></td>
            <td valign="top"><p>            
            <asp:FileUpload ID="mlIMAGEP1" runat="server" />
            <asp:Label ID="mlFILENAME" runat="server"></asp:Label>
        </tr>
         
    </table>    
    <hr />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>


<asp:Panel ID="pnGRID" runat="server">    
    
    <a href="mk_file.aspx?mpSTATUS=N">New</a>
    <a href="mk_file.aspx?mpSTATUS=D">Delete</a>
    
    <asp:DataGrid runat="server" ID="mlDATAGRID" 
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
            <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.DocNo")%>' CommandName="DeleteRecord">
            </asp:imagebutton>
            </ItemTemplate>
        </asp:TemplateColumn>     
        
        <asp:templatecolumn headertext="VW">
        <ItemTemplate>        
            <asp:hyperlink  Target="_blank"  runat="server" id="mlLINKVW" navigateurl='<%# String.Format("mk_doc_file.aspx?mpID={0}", Eval("DocNo")) %>' text="VW"></asp:hyperlink>
        </ItemTemplate>
        </asp:templatecolumn>                
        

        <asp:BoundColumn Headertext="SysID" DataField="SysID"></asp:BoundColumn>        
        <asp:BoundColumn HeaderText="Type" DataField="Type"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="DocNo" DataField="DocNo"></asp:BoundColumn>
        <asp:BoundColumn HeaderText="DocDate" DataField="DocDate"></asp:BoundColumn>              
        <asp:BoundColumn HeaderText="Subject" DataField="Subject"></asp:BoundColumn>        
        <asp:BoundColumn HeaderText="Recuserid" DataField="Recuserid"></asp:BoundColumn>        
        
    </Columns>
 </asp:DataGrid>  
</asp:Panel>

</asp:TableCell>
</asp:TableRow>

</asp:Table>

</form>

<br /><br /><br /><br />
</asp:Content>