<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ad_changemenustyle.aspx.cs" Inherits="pj_ad_ad_changemenustyle" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>
    <script type="text/javascript">
        function WinClose() {
            Window.close();
        }
    </script>

    <form runat="server">
        <table border="0" cellpadding="2" cellspacing="1" width="100%">
            <tr>
                <td>
                    <p><asp:Label ID="mlMESSAGE" runat="server" ForeColor="Purple" Font-Italic="true"></asp:Label></p>
                </td>
            </tr>
            <tr>
                    <td>
                        <b><asp:Label id="mlTITLE" runat="server"></asp:Label></b>
                    </td>
            </tr>
        </table>
        <asp:Panel ID="pnTOOLBAR" runat="server" Width="85%">  
            <table border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td>
                        <b><asp:Label id="Label1" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="mlSYSCODE" runat="server" Visible="false"/>
                    </td>
                </tr>  
                <tr>
                    <td><asp:ImageButton id="btNewRecord" ToolTip="NewRecord" Visible="false" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return confirm('Save Record ?');" OnClick="btSaveRecord_Click" />&nbsp;
                        <asp:ImageButton id="btSearchRecord" ToolTip="SearchRecord" Visible="false" ImageUrl="~/images/toolbar/find.jpg" runat="server" />&nbsp;
                        <asp:ImageButton id="btCancelOperation" ToolTip="CancelOperation" Visible="false" ImageUrl="~/images/toolbar/cancel.jpg" runat="server" />    
                    </td>
                </tr>                      
            </table>
        <hr />
        </asp:Panel>
        <asp:Panel ID="pnlSearch" runat="server" Width="85%" >
            <table runat="server" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                    <td align="center" class="tdtopi">CHANGE MENU STYLE</td>
                    <td width="10" class="tdtopikanan">&nbsp;</td>
                </tr>
            </table>
            <table runat="server" border="0" cellpadding="2" cellspacing="1" width="95%">
                <tr>
                    <td class="tdganjil" width="17%">Menu Style</td>
                    <td class="tdganjil" width="33%">
                        <asp:DropDownList runat="server" ID="ddlSearchBy">
                            <asp:ListItem Value="TREE">TREE MENU</asp:ListItem>
                            <asp:ListItem Value="MENUSMENU">MENUS MENU</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                    </td>                    
                </tr>                
            </table>   
        </asp:Panel>
        

    </form>
</asp:Content>

