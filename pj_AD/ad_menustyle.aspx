<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MsPageBlank.master" AutoEventWireup="true" CodeFile="ad_menustyle.aspx.cs" Inherits="pj_AD_ad_menustyle" %>
<%@ Register src="../usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>
<%@ Register src="../UserController/ValidDate.ascx" tagname="ValidDate" tagprefix="uc1" %>
<%@ Register src="../UserController/ucInputNumber.ascx" tagname="ucInputNumber" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <link href="../script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script src="../script/JavaScript/Elsa.js" type="text/javascript"></script>
    <script src="../script/JavaScript/Eloan.js" type="text/javascript"></script>


    <%--<script type="text/javascript">
            function OpenWinLookUp() {
                var AppInfo = '<%= Request.ServerVariables["PATH_INFO"]%>';
            var App = AppInfo.substr(1, AppInfo.indexOf('/', 1) - 1)
            window.open('http://<%=Request.ServerVariables["SERVER_NAME"]%>:<%=Request.ServerVariables["SERVER_PORT"]%>/' + App + '/pj_ad/ad_changemenustyle.aspx?NIK=<%=Session["mgUSERID"].ToString().Trim() %>&MENUSTYLE=<%=Session["mgMENUSTYLE"].ToString().Trim() %>', 'UserLookup', 'left=150, top=150, width=900, height=200, menubar=0, scrollbars=yes');
        }

    </script>

    <body onload="javascript:OpenWinLookUp()">
    </body>--%>

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
        <asp:Panel ID="pnTOOLBAR" runat="server" Width="100%">  
            <table border="0" cellpadding="2" cellspacing="1" width="95%">                        
                <tr>
                    <td>
                        <asp:HiddenField ID="mlSYSCODE" runat="server" Visible="false"/>
                    </td>
                </tr>  
                <tr>
                    <td>
                        <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClick="btSaveRecord_Click" />&nbsp;
                        <a href="javascript:parent.frames.location.href='ad_logout.aspx'" target="_top">
                            <img id="imgLogout" runat="server" border="0" src="../Images/toolbar/cancel.jpg"  />
                        </a>
                    </td>
                </tr>                      
            </table>
        <hr />
        </asp:Panel>
        <asp:Panel ID="pnlTitle" runat="server" Width="100%" >
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
                        <asp:DropDownList runat="server" ID="ddlMenuStyle">
                            <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="TREE">TREE MENU</asp:ListItem>
                            <asp:ListItem Value="MENUSMENU">MENUS MENU</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                    </td>   
                    <td class="tdganjil" colspan="2"></td>                 
                </tr>                
            </table>   
        </asp:Panel>        
   </form>
   
</asp:Content>

