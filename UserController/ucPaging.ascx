<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPaging.ascx.cs" Inherits="usercontroller_ucPaging" %>
    <table id="ucPaging" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="3%" align="center"><asp:ImageButton runat="server" ID="btnFirst" 
                ImageUrl="../images/button/butkiri1.gif" onclick="btnFirst_Click"  /></td>
            <td width="3%" align="center"><asp:ImageButton runat="server" ID="btnPrev" 
                    ImageUrl="../images/button/butkiri.gif" onclick="btnPrev_Click"  /></td>
            <td width="3%" align="center"><asp:ImageButton runat="server" ID="btnNext" 
                    ImageUrl="../images/button/butkanan.gif" onclick="btnNext_Click"   /></td>
            <td width="3%" align="center"><asp:ImageButton runat="server" ID="btnLast" 
                    ImageUrl="../images/button/butkanan1.gif" onclick="btnLast_Click"  /></td>
            <td width="10%" align="left">
                <asp:TextBox runat="server" ID="txtGO" Width="50px"></asp:TextBox>&nbsp;
                <asp:ImageButton runat="server" ID="btnGO" 
                    ImageUrl="../images/button/butGO.gif" onclick="btnGO_Click"/>
               <%-- <asp:RangeValidator ID="rgvGo" runat="server" ControlToValidate="txtGO" 
                                Display="Dynamic" ErrorMessage="Page is not valid" Font-Names="Verdana" 
                                Font-Size="Smaller" MinimumValue="0"></asp:RangeValidator>--%>
            </td>
            <td width="5%" align="left">
                <font color="#999999" face="Verdana" size="2">PageSize&nbsp;</font>                
            </td>
            <td width="25%" align="left">
                <asp:TextBox runat="server" ID="txtPagesize" Width="25px" Font-Names="verdana"  Text="20"></asp:TextBox>

            </td>
            <td align="right">
                <font color="#999999">                    
                    <font face="Verdana" size="2">Page&nbsp; </font>
                    <asp:Label ID="lblCurrentPage" runat="server" Font-Names="Verdana" Font-Size="Smaller"></asp:Label>
                    <font face="Verdana" size="2">&nbsp;of</font>
                    <asp:Label ID="lblTotalPage" runat="server" Font-Names="Verdana" Font-Size="Smaller"></asp:Label>
                    <font face="Verdana" size="2">, Total&nbsp; </font>
                    <asp:Label ID="lblTotalRec" runat="server" Font-Size="Smaller"></asp:Label>
                    &nbsp;
                    <font face="Verdana" size="2">record(s)</font>
                 </font>
            </td>
        </tr>
    </table>
