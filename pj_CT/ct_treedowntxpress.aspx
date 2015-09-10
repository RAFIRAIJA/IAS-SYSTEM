<%@ Page language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ct_treedowntxpress.aspx.vb" Inherits="ct_treedowntxpress"  %>
<%@ Import Namespace = "system.data" %>
<%@ Import Namespace = "system.data.oledb" %>
<%@ Import Namespace = "system.io" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<link rel="StyleSheet" href="../script/dtree/dtree.css" type="text/css" />    
<script type="text/javascript" src="../script/dtree/dtree.js"></script>

<form id="form1" runat="server">

<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
    <td>
        <asp:Panel ID="pnTOOLBAR" runat="server">
            <table width="0" border="0" cellpadding="3" cellspacing="3">
            <tr>
                <td><asp:ImageButton ID="btSearchRecord" ToolTip="SearchRecord" ImageUrl="~/images/toolbar/find.jpg" runat="server" OnClientClick="return confirm('Search Record ?');" /></td>
            </tr>
            </table>
        </asp:Panel>
    </td>
</tr>

<tr>
    <td>
    <hr />
    <p class="header1"><b><asp:Label id="mlTITLE" runat="server"></asp:Label></b></p>
    <p><asp:Label ID="mlMESSAGE" runat="server"  Font-Italic="true" ForeColor ="red"></asp:Label> <br /> </p>
    <asp:HiddenField ID="mlSYSCODE" runat="server"/>    
    <asp:HiddenField ID="mlVALUE1" runat="server"/>
    </td>
</tr>
</table>

        
 <table border="0"> 
 <tr>
    <td valign="top">        
        <asp:Label ID="lbTYPE" runat="server" Text="Type"></asp:Label>        
    </td>
    <td valign="top">        
        <asp:DropDownList ID="ddTYPE" runat="server"></asp:DropDownList>
    </td>    
 </tr>
 
 
 <tr>
    <td valign="top">        
        <asp:Label ID="lbDOCNO" runat="server" Text="Tracking ID"></asp:Label>
        <asp:ImageButton ID="btSEARCHCONTRACT" ToolTip="Contract No" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
    </td>
    <td valign="top">
        <asp:TextBox id="txDISTID"  AutoPostBack="true" runat="server"></asp:TextBox>
        <asp:ImageButton ID="btDISTID" ToolTip="Dist ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" Visible="false" />
        <asp:Label id="txNAME" runat="server"></asp:Label>
        <br /><asp:Label id="lb1" runat="server"></asp:Label>
        <br /><asp:Label id="lb2" runat="server"></asp:Label>
    </td>    
 </tr>
 
 <tr>                    
    <td valign="top"></td>                        
    <td valign="top">
        <asp:Panel ID="pnSEARCHCONTRACT" runat="server">                            
            <asp:Label ID="lbSEARCHCONTRACT" Text="Contract No : " runat="server"></asp:Label>
            <asp:TextBox ID="mlSEARCHCONTRACT" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
            <asp:ImageButton ID="btSEARCHCONTRACTSUBMIT" ToolTip="Contract No" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
            <br />
            <asp:Label ID="lbSEARCHCONTRACT2" Text="Cust Name : " runat="server"></asp:Label>
            <asp:TextBox ID="mlSEARCHCONTRACT2" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
            <asp:ImageButton ID="btSEARCHCONTRACTSUBMIT2" ToolTip="Customer Name" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
            
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
                    <asp:ButtonColumn HeaderText = "Cust"  DataTextField = "Cust"></asp:ButtonColumn>
                </Columns>
             </asp:DataGrid> 
        </asp:Panel>            
    </td>
</tr>

 
 </table>
 
 
<table visible="true"  border="0">
<tr><td>
 
 <div class="dtree">                             	
  <% If mlSTATUS = True Then%> 
 
 <% 
     Dim mlMAXLENGTH As Integer
     Dim mlUPLINEID As String
     Dim mlDISTID As String
     Dim mlDISTNAME As String
     Dim mlCOUNTER As Integer
        
     mlDISTID = ""
     mlUPLINEID = ""
     mlDISTNAME = ""
     mlCOUNTER = 0
     
   
 %>
 <p><a href="javascript: mlNODE.openAll();">Buka Semua</a> | <a href="javascript: mlNODE.closeAll();">Tutup Semua</a></p>     
 <% While mlREADER.Read%>
 <% If mlFIRST Then %>
    <%
        mlCOUNTER = mlCOUNTER + 1
        Select Case ddTYPE.Text
            Case "By Transfer Task No"
                mlDISTID = mlREADER("Linno2")
            Case "By Contract No"
                mlDISTID = "0"
        End Select
        mlDISTNAME = mlREADER("Linno2") & "-" & mlREADER("SysID")
    %>
    
    <script type="text/javascript">
    <!--
    mlNODE = new dTree('mlNODE');       
    mlNODE.add('<%response.write(mlDISTID) %>', -1,'<%response.write(mlDISTNAME) %>');            		
    
    //-->
    </script>
    <% mlFIRST = False %>
<%  Else
        mlTREEPIC2 = "../images/tree/man.gif"
%>	 

    <%       
        mlCOUNTER = mlCOUNTER + 1
        Select Case ddTYPE.Text
            Case "By Transfer Task No"
                mlUPLINEID = mlREADER("Linno")
                mlDISTID = mlREADER("Linno2")
            Case "By Contract No"
                mlUPLINEID = "0"
                mlDISTID = Format(mlCOUNTER, "000") & "_" & mlREADER("Linno2")
        End Select
        
        mlDISTNAME = mlREADER("DocDate") & "-" & mlREADER("SysID")
        Select Case mlREADER("SysID")
            Case "Transfer"
                mlDISTNAME = mlREADER("DocDate") & "-" & mlREADER("SysID") & " # " & mlREADER("Description2")
            Case "note"
                mlDISTNAME = mlREADER("DocDate") & " - " & mlREADER("SysID") & " # " & mlREADER("Description")
            Case "delivery_info"
                mlDISTNAME = mlREADER("DocDate") & " - " & mlREADER("SysID") & " # " & mlREADER("Description") & " - " & mlREADER("Description2")
            Case "ship_info"
                mlDISTNAME = mlREADER("DocDate") & " - " & mlREADER("SysID") & " # " & mlREADER("Description") & " - " & mlREADER("Description2")
            Case Else
        End Select
        
        If Len(mlDISTNAME) > mlMAXLENGTH Then
            mlMAXLENGTH = Len(mlDISTNAME) + 5
        End If
        
    %>
    
        <script type="text/javascript">
        <!--		
        mlNODE.add('<%response.write(mlDISTID)%>','<%response.write(mlUPLINEID)%>','<%response.write(mlDISTNAME)%>','','','','<%response.write(mlTREEPIC2)%>');		
        
        //-->
    </script>

   
<%End If%>
<%End While%>

<script type="text/javascript">
    <!--	
    document.write(mlNODE);
    //-->
</script>
<%
    Dim mlA As String
    mlA = "1"
 %>
 </div> 

</td></tr>
</table>



<% End If%>

</form> 

<br /><br /><br /><br />
</asp:Content>

