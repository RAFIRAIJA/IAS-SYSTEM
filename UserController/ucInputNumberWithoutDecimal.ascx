<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucInputNumberWithoutDecimal.ascx.vb" Inherits="ucInputNumberWithoutDecimal" %>

    
<input id="hdnInput" type="hidden" name="hdnInput" runat="server">
<asp:TextBox ID="txtInput" runat="server" Text="0" CssClass="inptype" style="text-align:right;"></asp:TextBox>
<asp:TextBox ID="txtInputDummy" runat="server" BorderColor="#ffffff" ForeColor="#ffffff"
    Width="0px" Visible="true" Style="visibility: hidden"></asp:TextBox><asp:Label ID="ltlPercent"
        Visible="False" runat="server" Text="%"></asp:Label><font color="#ff0000"><asp:Literal
            ID="ltlMandatory" runat="server" Visible="False" Text="*)"></asp:Literal></font>
            <asp:RequiredFieldValidator
                ID="rfvInput" runat="server" Display="Dynamic" ControlToValidate="txtInputDummy"
                ErrorMessage="*" Enabled="False" InitialValue="0"></asp:RequiredFieldValidator>
                <asp:RangeValidator
                    ID="rgvInput" runat="server" Type="Double" Display="Dynamic" MaximumValue="999999999999999"
                    MinimumValue="-999999999999999" ControlToValidate="txtInputDummy" ErrorMessage="*"></asp:RangeValidator><asp:CompareValidator
                        ID="cpvInput" runat="server" Display="Dynamic" ControlToValidate="txtInputDummy"
                        ErrorMessage="*" Enabled="False"></asp:CompareValidator>
<div id="scriptJava" runat="server">
</div>