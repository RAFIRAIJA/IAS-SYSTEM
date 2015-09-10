<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TKSUCNavigation.ascx.vb" Inherits="usercontroller_TKSUCNavigation"%>
<link href="../tks.css" rel="stylesheet" type="text/css" />
<script src="../tks.js" type="text/javascript"></script>
<asp:panel id="PnlNavigation" runat="server" Width="100%" Height="25px">
    <table class="TableEntry" style="height:25px; width:95%;" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td>
                <asp:ImageButton ID="ImgNavFirst" Width="25px" CausesValidation="false" ImageUrl="../Images/first.gif" ImageAlign="Middle" runat="server" />
                <asp:ImageButton ID="ImgNavPrevious" Width="25px" CausesValidation="false" ImageUrl="../Images/Previous.gif" ImageAlign="Middle" runat="server" />
                <asp:ImageButton ID="ImgNavNext" Width="25px" CausesValidation="false" ImageUrl="../Images/Next.gif" ImageAlign="Middle" runat="server" />
                <asp:ImageButton ID="ImgNavLast" Width="25px" CausesValidation="false" ImageUrl="../Images/Last.gif" ImageAlign="Middle" runat="server" />
                &nbsp;Page :
				<asp:TextBox id="txtNavPageNoGo" Width="40px" Runat="server" CssClass="InputBox"></asp:TextBox>&nbsp;
                <asp:ImageButton ID="ImgNavGo" CausesValidation="false" ImageUrl="../Images/Go.gif" ImageAlign="Middle" runat="server" />
                <asp:Label id="lblNavInfo" Runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:panel>
