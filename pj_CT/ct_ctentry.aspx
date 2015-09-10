<%@ Page Language="VB" MasterPageFile="~/pagesetting/MasterIntern.master" AutoEventWireup="false" CodeFile="ct_ctentry.aspx.vb" Inherits="ct_ctentry"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
<link href="../script/calendar.css" rel="stylesheet" type="text/css" media="all" />
<script type="text/javascript" src="../script/calendar.js"></script>

<script type="text/javascript" language="Javascript">
<!-- hide script from older browsersf
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


function ValidateForm() {
   
    if (document.getElementById("<%=txCUST.clientid%>").value=="") 
    {
        alert ('CustID not allowed empty');
        return false;        
    }
    
    if (document.getElementById("<%=lbCUSTDESC.clientid%>").innerHTML=="") 
    {
        alert ('CustName not allowed empty');
        return false;
    }
    
    if (document.getElementById("<%=txSITECARD.clientid%>").value=="")
    {
        alert ('SiteCardID not allowed empty');
        return false;
    }
    
    if (document.getElementById("<%=lbSITEDESC.clientid%>").innerHTML=="")
    {
        alert ('SiteCard name not allowed empty');
        return false;
    }
    
    
    if (document.getElementById("<%=txCTDOCNO.clientid%>").value=="")
    {
        alert ('Contract No / Letter No not allowed empty');
        return false;
    }
    
    if (document.getElementById("<%=txCTDOCNO.clientid%>").value=="")
    {
        alert ('Contract No not allowed empty');
        return false;
    }
    
    if (document.getElementById("<%=txDOCDATE2.clientid%>").value=="")
    {
        alert ('Contract Create Date not allowed empty');
        return false;
    }
   
    
    if (document.getElementById("<%=ddPRODUCT.clientid%>").value=="Pilih")
    {
        alert('Please Choose the Product Offering');
        return false;
    }
    
                
    return confirm('Save Record ?');
}

function GetPercentage(mpTEXT)
{
        
    var field1;
    var field2;
    var field3;
    
    //Percentage Increment
    field1= document.getElementById("<%=txPRICE.clientid%>").value;
    FormatNumber(mpTEXT);   
    field2= document.getElementById("<%=txPRICE2.clientid%>").value;    
    field1 = field1.replace(",","");    
    field2 = field2.replace(",","");
    field3= (field1-field2)/field2*100;
    if (field3=='Infinity' || field3=='Nan') 
    {
        document.getElementById("<%=txPERCENTAGE.clientid%>").value  = '0';
    }
    else
    {
        document.getElementById("<%=txPERCENTAGE.clientid%>").value  = field3;
    }    
    
    //Percentage MP    
    field1= document.getElementById("<%=txPRICE.clientid%>").value;
    field2= document.getElementById("<%=txMANPOWER.clientid%>").value;
    field1 = field1.replace(",","");    
    field2 = field2.replace(",","");
    field3= field1/field2;    
    
    if (field3=='Infinity' || field3=='Nan') 
    {
        document.getElementById("<%=txPRICEMP.clientid%>").value  = '0';
    }
    else
    {
        document.getElementById("<%=txPRICEMP.clientid%>").value  = field3;
    }        
    
    
    //Percentage UMP    
    field1= document.getElementById("<%=txPRICEMP.clientid%>").value;    
    field2= document.getElementById("<%=txUMP.clientid%>").value;
    field1 = field1.replace(",","");    
    field2 = field2.replace(",","");
    field3= (field1/field2);
    
    if (field3=='Infinity' || field3=='Nan') 
    {
        document.getElementById("<%=txUMPPERCENT.clientid%>").value  = '0';
    }
    else
    {
        document.getElementById("<%=txUMPPERCENT.clientid%>").value  = field3;
    }        

}



function GetPercentageMP()
{
        
    var field1;
    var field2;
    var field3;
    
    field1= document.getElementById("<%=txPRICE.clientid%>").value;
    FormatNumber(mpTEXT);   
    field2= document.getElementById("<%=txMANPOWER.clientid%>").value;
    
    alert('dfd');
    field1 = field1.replace(",","");    
    field2 = field2.replace(",","");            
    
    field3= (field1/field2);
    if (field3=='Infinity' || field3=='Nan') 
    {
        document.getElementById("<%=txPRICEMP.clientid%>").value  = '0';
    }
    else
    {
        document.getElementById("<%=txPRICEMP.clientid%>").value  = field3;
    }        
}


function GetPercentageUMP()
{        
    var field1;
    var field2;
    var field3;
    
    field1= document.getElementById("<%=txPRICEMP.clientid%>").value;
    FormatNumber(mpTEXT);   
    field2= document.getElementById("<%=txUMP.clientid%>").value;
    
    field1 = field1.replace(",","");    
    field2 = field2.replace(",","");   
         
    
    field3= (field1/field2);
    if (field3=='Infinity' || field3=='Nan') 
    {
        document.getElementById("<%=txUMPPERCENT.clientid%>").value  = '0';
    }
    else
    {
        document.getElementById("<%=txUMPPERCENT.clientid%>").value  = field3;
    }        
}

function GetPercentageUMP(mpTEXT)
{
        
    var field1;
    var field2;
    var field3;
    
    field1= document.getElementById("<%=txPRICE.clientid%>").value;
    FormatNumber(mpTEXT);   
    field2= document.getElementById("<%=txPRICE2.clientid%>").value;
             
    
    field3= (field1-field2)/field2*100;
    if (field3=='Infinity' || field3=='Nan') 
    {
        document.getElementById("<%=txPERCENTAGE.clientid%>").value  = '0';
    }
    else
    {
        document.getElementById("<%=txPERCENTAGE.clientid%>").value  = field3;
    }    
    
}



function GetPercentage_backup(mpTEXT)
{
        
    var field1;
    var field2;
    var field3;
    
    field1= document.getElementById("<%=txPRICE.clientid%>").value;
    FormatNumber(mpTEXT);   
    field2= document.getElementById("<%=txPRICE2.clientid%>").value;
    
    field1 = field1.replace(",","");    
    field2 = field2.replace(",","");   

    
    field3= (field1-field2)/field2*100;
    if (field3=='Infinity' || field3=='Nan') 
    {
        document.getElementById("<%=txPERCENTAGE.clientid%>").value  = '0';
    }
    else
    {
        document.getElementById("<%=txPERCENTAGE.clientid%>").value  = field3;
    }    
    
    GetPercentageMP;
    GetPercentageUMP;
}


function FormatNumber(mpTEXT)
{
    mpTEXT.value=numberFormat(mpTEXT.value);
}
     
function numberFormat(nr){
	//remove the existing ,
	var regex = /,/g;
	nr = nr.replace(regex,'');
	//force it to be a string
	nr += '';
	//split it into 2 parts  (for numbers with decimals, ex: 125.05125)
	var x = nr.split('.');
	var p1 = x[0];
	var p2 = x.length > 1 ? '.' + x[1] : '';
	//match groups of 3 numbers (0-9) and add , between them
	regex = /(\d+)(\d{3})/;
	while (regex.test(p1)) {
		p1 = p1.replace(regex, '$1' + ',' + '$2');
	}
	//join the 2 parts and return the formatted number
	return p1 + p2;
}


function unnumberFormat(nr)
{    
    var str;
    str = nr.replace(",","");
    str = parseFloat(str);
    alert (str);
    return str;
}

 
</script>

<form id="mpFORM" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />

<asp:Table id="mlTABLE1" BorderWidth ="0" CellPadding ="0" CellSpacing="0" Width="100%" runat="server">
<asp:TableRow>   
<asp:TableCell> 
<asp:Panel ID="pnTOOLBAR" runat="server">  
    <table border="0" cellpadding="3" cellspacing="3">
        <tr>
            <td valign="top">
                <asp:ImageButton id="btNewRecord" ToolTip="NewRecord" ImageUrl="~/images/toolbar/new.jpg" runat="server" />&nbsp;
                <asp:ImageButton id="btSaveRecord" ToolTip="SaveRecord" ImageUrl="~/images/toolbar/save.jpg" runat="server" OnClientClick="return ValidateForm()" />&nbsp;
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
<asp:TableRow><asp:TableCell><asp:Label id="mlSQLSTATEMENT" runat="server" Visible="False" /></asp:TableCell></asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL" runat="server">

    <table width="80%" cellpadding="0" cellspacing="0" border="0">        
    <tr>
        <td><asp:Label ID="lbENTITY" runat="server" Text="Entity"></asp:Label></td>
        <td>:</td>
        <td><asp:DropDownList ID="ddENTITY" runat="server"></asp:DropDownList></td>     
    </tr>
    
    <tr>
        <td valign="top">
            <asp:Label ID="lbREFFNO" runat="server" Text="Reff Contract / Letter Number"></asp:Label>
            <asp:ImageButton ID="btSEARCHCONTRACT" ToolTip="Contract No" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
         </td>
        <td valign="top">:</td>
        <td valign="top">
            <asp:TextBox ID="txREFFNO" runat="server" Width="200"></asp:TextBox>
            <asp:ImageButton ID="btREFF" ToolTip="Submit Document" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />                      
            <asp:Label ID="lbREFFDOCNO" Text="" runat="server"></asp:Label>
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
                    </Columns>
                 </asp:DataGrid> 
            </asp:Panel>            
        </td>
        </tr>            
    </table>
    <br /><hr /><br />
            
            
    <table width="100%" cellpadding="0" cellspacing="2" border="0">        
                    
        <tr>
        <td valign="top">
            <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td valign="top"><p><asp:Label ID="lbDOCUMENTNO" runat="server" Text="Document No"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txDOCUMENTNO" runat="server" Width="150" Enabled="false"></asp:TextBox></td>            
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
                    <asp:Label ID="lbSITECARD" Text="Site Card" runat="server"></asp:Label>
                    <asp:ImageButton ID="btSEARCHSITECARD" ToolTip="Kode Site Card" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
                </td>
                <td valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txSITECARD" runat="server" Width="100"></asp:TextBox>                                                                    
                    <asp:ImageButton ID="btSITECARD" ToolTip="Cari Site Card" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                    <asp:Label ID="lbSITEDESC" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr>                    
                <td valign="top"></td>            
                <td valign="top"></td> 
                <td valign="top">
                    <asp:Panel ID="pnSEARCHSITECARD" runat="server">                            
                        <asp:Label ID="lbSEARCHSITECARD" Text="Nama Site : " runat="server"></asp:Label>
                        <asp:TextBox ID="mlSEARCHSITECARD" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
                        <asp:ImageButton ID="btSEARCHSITECARDSUBMIT" ToolTip="Search Site" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
                        
                        <asp:DataGrid runat="server" ID="mlDATAGRIDSITECARD" 
                            HeaderStyle-BackColor="orange"  
                            HeaderStyle-VerticalAlign ="top"
                            HeaderStyle-HorizontalAlign="Center"
                            OnItemCommand="mlDATAGRIDSITECARD_ItemCommand"        
                            autogeneratecolumns="false">	    
                            
                            <AlternatingItemStyle BackColor="#F9FCA8" />
                            <Columns>  
                                <asp:ButtonColumn  HeaderText = "Kode" DataTextField = "Field_ID" ></asp:ButtonColumn>
                                <asp:ButtonColumn HeaderText = "Nama"  DataTextField = "Field_Name"></asp:ButtonColumn>
                                <asp:ButtonColumn  HeaderText = "Branch" DataTextField = "Branch" ></asp:ButtonColumn>
                                <asp:ButtonColumn HeaderText = "Cust"  DataTextField = "CustID"></asp:ButtonColumn>
                            </Columns>
                         </asp:DataGrid> 
                    </asp:Panel>            
                </td>
        </tr>
            
        <tr>
            <td valign="top"><p><asp:Label ID="lblADDR" runat="server" Text="Alamat Site"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txADDR"  TextMode="MultiLine" width="250"  Height="100"  MaxLength="3999" runat="server" />                
            </td>
        </tr>

        <tr>
            <td valign="top"><p><asp:Label ID="lbCITY" runat="server" Text="Kota"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txCITY" width="200" runat="server" /></td>
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
            <td valign="top"><asp:TextBox ID="txPHONE1" width="200" runat="server" /></td>
        </tr>
        
        <tr>
            <td valign="top"><p><asp:Label ID="lbPHONE_PIC" runat="server" Text="PIC & Hp"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top"><asp:TextBox ID="txPHONE1_PIC" width="200" runat="server" /></td>
        </tr>            
        
        <tr>
        </tr>
        
        
        <tr>
            <td valign="top">
                <asp:Label ID="lbLCUST" Text="Customer" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHCUST" ToolTip="Kode Customer" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txCUST" runat="server" Width="100"></asp:TextBox>                                                                    
                <asp:ImageButton ID="btCUST" ToolTip="Cari Customer" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                <asp:Label ID="lbCUSTDESC" Text="" runat="server"></asp:Label>                                               
            </td>
        </tr>
        
        <tr>                    
            <td valign="top"></td>            
            <td valign="top"></td> 
            <td valign="top">
                <asp:Panel ID="pnSEARCHCUST" runat="server">                            
                    <asp:Label ID="lbSEARCHCUST" Text="Nama Cust : " runat="server"></asp:Label>
                    <asp:TextBox ID="mlSEARCHCUST" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
                    <asp:ImageButton ID="btSEARCHCUSTSUBMIT" ToolTip="Search Customer" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
                    
                    <asp:DataGrid runat="server" ID="mlDATAGRIDCUST" 
                        HeaderStyle-BackColor="orange"  
                        HeaderStyle-VerticalAlign ="top"
                        HeaderStyle-HorizontalAlign="Center"
                        OnItemCommand="mlDATAGRIDCUST_ItemCommand"        
                        autogeneratecolumns="false">	    
                        
                        <AlternatingItemStyle BackColor="#F9FCA8" />
                        <Columns>  
                            <asp:ButtonColumn  HeaderText = "Kode" DataTextField = "Field_ID" ></asp:ButtonColumn>
                            <asp:ButtonColumn HeaderText = "Nama"  DataTextField = "Field_Name"></asp:ButtonColumn>
                        </Columns>
                     </asp:DataGrid> 
                </asp:Panel>            
            </td>
         </tr>
         
         
        <tr id="tr6" runat="server">
            <td valign="top">                        
                <asp:Label ID="Label1" Text="Nav Job No" runat="server"></asp:Label>
                <asp:ImageButton ID="btSEARCHJOBNO" ToolTip="Job No" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
            </td>
            <td valign="top">:</td>
            <td valign="top">
                <asp:TextBox ID="txJOBNO"  width="100" runat="server"></asp:TextBox>                                
                <asp:ImageButton ID="btJOBNO" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                <asp:Label ID="txJOBDESC"  Enabled="false" runat="server"></asp:Label>                 
            </td>
         </tr>
         
         <tr id="tr7" runat="server">
            <td></td>            
            <td></td>         
            <td>
                <asp:Panel ID="pnSEARCHJOBNO" runat="server">
                <asp:Label ID="lbSEARCHJOBNO" Text="Date Expired : " runat="server"></asp:Label>
                <asp:TextBox ID="mpSEARCHJOBNO"  width="200" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
                <asp:ImageButton ID="btSEARCHJOBNOSUBMIT" ToolTip="Search Staff ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />
                
                <asp:DataGrid runat="server" ID="mlDATAGRIDJOBNO" 
                    HeaderStyle-BackColor="orange"  
                    HeaderStyle-VerticalAlign ="top"
                    HeaderStyle-HorizontalAlign="Center"
                    OnItemCommand="mlDATAGRIDJOBNO_ItemCommand"
                    autogeneratecolumns="false">	    
                    
                    <AlternatingItemStyle BackColor="#F9FCA8" />
                    <Columns>  
                        <asp:ButtonColumn  HeaderText = "JobNo" DataTextField = "JobNo" ></asp:ButtonColumn>                        
                        <asp:BoundColumn HeaderText="SiteCard_ID" DataField="SiteCard_ID" dataformatstring="{0:MMMM d, yyyy}"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Prd_Offer_ID" DataField="Prd_Offer_ID" dataformatstring="{0:MMMM d, yyyy}"></asp:BoundColumn>                        
                        <asp:BoundColumn HeaderText="Start_Date" DataField="Start_Date" dataformatstring="{0:MMMM d, yyyy}"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="End_Date" DataField="End_Date" dataformatstring="{0:MMMM d, yyyy}"></asp:BoundColumn>
                    </Columns>
                 </asp:DataGrid> 
                </asp:Panel>                       
            </td>         
         </tr>
         
         <tr>
                <td valign="top"><p><asp:Label ID="lbLPRODUCT2" runat="server" Text="Service"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddPRODUCT2" runat="server"></asp:DropDownList>                    
                    <br />
                    <asp:ImageButton ID="btPRODUCT2" ToolTip="Add" ImageUrl="~/images/toolbar/add.gif" runat="server" />                      
                    <asp:ImageButton ID="btCLEARCART" ToolTip="Clear" ImageUrl="~/images/toolbar/cleardata.gif" runat="server" OnClientClick="return confirm('Clear Data ?');" />
                </td>
        </tr> 
        
        <tr>   
            <td valign="top"></td>
            <td valign="top"></td>
            <td valign="top">                
                <asp:Label ID="lbITEMCART"  Enabled="false" runat="server"></asp:Label>                                       
                <asp:HiddenField id="lbITEMCART2" runat="server" />
            </td>            
        </tr> 
             
             
        </table>
        </td>
                
        <td valign="top">
            <table cellpadding="0" cellspacing="0" border="0">
             <tr id="tr10" runat="server">
                <td valign="top"><p><asp:Label ID="lbCTDOCNO" runat="server" Text="Contract No"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txCTDOCNO" runat="server" Width="200"></asp:TextBox>
                    <asp:Label ID="lbREVISENO" runat="server"></asp:Label>
                </td>            
             </tr>
            
            <tr id="tr9" runat="server">
                <td valign="top"><asp:Label ID="lbDOCDATE2" Text="Create Date" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top">                
                    <asp:TextBox ID="txDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
                    <input id="btJOINDATE2" runat="server" onclick="displayCalendar(mpCONTENT_txDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                
                    <asp:ImageButton runat="Server" ID="btCALENDAR2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                    <ajaxtoolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="bt_ajDOCDATE2" TargetControlID="txDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>                 
                    <font color="blue">dd/mm/yyyy</font>
                </td>
            </tr>
            
            
                        
             
            <tr id="trSTARTDATE" runat="server">
                <td valign="top"><asp:Label ID="lbCRDOCDATE1" Text="Start Date" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top">                             
                    <asp:TextBox ID="txCRDOCDATE1" runat="server" Width="100"></asp:TextBox>                                                                    
                    <input id="btCRDOCDATE1" runat="server" onclick="displayCalendar(mpCONTENT_txCRDOCDATE1,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                                                      
                    <asp:ImageButton runat="Server" ID="ImageButton2" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                    <ajaxtoolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="bt_ajCRDOCDATE1" TargetControlID="txCRDOCDATE1" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>
                    <font color="blue">dd/mm/yyyy</font>
                </td>
            </tr>
             
             
            <tr id="trENDDATE" runat="server">
                <td valign="top"><asp:Label ID="lbCRDOCDATE2" Text="End Date" runat="server"></asp:Label></td>
                <td valign="top">:</td>
                <td valign="top">                
                    <asp:TextBox ID="txCRDOCDATE2" runat="server" Width="100"></asp:TextBox>                                                                                                          
                    <input id="btCRDOCDATE2" runat="server" onclick="displayCalendar(mpCONTENT_txCRDOCDATE2,'dd/mm/yyyy',this)" type="button" value="D" style="background-color:Yellow " />                                
                    <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/images/toolbar/calendar.png" AlternateText="Click to show calendar" />
                    <ajaxtoolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="bt_ajCRDOCDATE2" TargetControlID="txCRDOCDATE2" Format="dd/MM/yyyy" runat="server" PopupPosition="Right"></ajaxtoolkit:CalendarExtender>                 
                    <font color="blue">dd/mm/yyyy</font>
                </td>
            </tr>
             
            <tr id="tr11" runat="server">
                <td valign="top"><p><asp:Label ID="lbLPRODUCT" runat="server" Text="Service"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top">
                    <asp:DropDownList ID="ddPRODUCT" runat="server"></asp:DropDownList>
                    <asp:ImageButton ID="btPRODUCT" ToolTip="Cari Service" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                    <asp:Label ID="lbPRODUCT" Text="" runat="server"></asp:Label>                                                                   
                </td>
            </tr> 
            
            <tr id="tr3" runat="server">
                <td valign="top"><p><asp:Label ID="lbPRICE2" runat="server" Text="Existing Price"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txPRICE2" runat="server" style="text-align:right"  Width="150" onchange="javascript:FormatNumber(this);"></asp:TextBox></td>            
             </tr>
             
             <tr id="tr5" runat="server">
                <td valign="top"><p><asp:Label ID="lbMANPOWER2" runat="server" Text="Existing Man Power"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txMANPOWER2" runat="server" style="text-align:right" Width="150" onchange="javascript:FormatNumber(this);"></asp:TextBox></td>            
             </tr>
        
            <tr id="tr15" runat="server">
                <td valign="top"><p><asp:Label ID="lbUMP" runat="server" Text="UMP"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txUMP" runat="server" style="text-align:right" Width="150" onchange="javascript:FormatNumber(this);"></asp:TextBox></td>            
             </tr>
             
             <tr id="tr12" runat="server">
                <td valign="top"><p><asp:Label ID="lbMANPOWER" runat="server" Text="Man Power Qty"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txMANPOWER" runat="server" style="text-align:right" Width="150" onchange="javascript:FormatNumber(this);"></asp:TextBox></td>            
             </tr>
             
                         
             <tr id="tr4" runat="server">
                <td valign="top"><p><asp:Label ID="lbPRICE3" runat="server" Text="Propose Price"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txPRICE3" runat="server"  style="text-align:right" Width="150" onchange="javascript:FormatNumber(this);"></asp:TextBox></td>            
             </tr>
             
             <tr id="tr13" runat="server">
                <td valign="top"><p><asp:Label ID="lbPRICE" runat="server" Text="Price"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txPRICE" runat="server" style="text-align:right" Width="150" onchange="javascript:GetPercentage(this);"></asp:TextBox></td>
             </tr>
             
             <tr id="tr16" runat="server">
                <td valign="top"><p><asp:Label ID="lbPRICEMP" runat="server" Text="Price Per Man Power"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txPRICEMP" runat="server" style="text-align:right" Width="150"    onchange="javascript:FormatNumber(this);"></asp:TextBox></td>            
             </tr>
             
             <tr id="tr17" runat="server">
                <td valign="top"><p><asp:Label ID="lbUMPPERCENT" runat="server" Text="UMP(%)"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txUMPPERCENT" runat="server" style="text-align:right" Width="50"   onchange="javascript:FormatNumber(this);"></asp:TextBox>&nbsp;%</td>
             </tr>
             
             
             <tr id="tr1" runat="server">
                <td valign="top"><p><asp:Label ID="lbPERCENTAGE" runat="server" Text="Increment %"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top"><asp:TextBox ID="txPERCENTAGE" runat="server" style="text-align:right" Width="50" onchange="javascript:FormatNumber(this);"></asp:TextBox>&nbsp;%</td>
             </tr>
             
             
             <tr id="trU1" runat="server">
                <td valign="top">                        
                    <asp:Label ID="lbUSER" Text="Negotiator" runat="server"></asp:Label>
                    <asp:ImageButton ID="btSEARCHUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
                </td>
                <td valign="top">:</td>
                 <td valign="top">
                    <asp:TextBox ID="txUSERID" runat="server"> </asp:TextBox>                                
                    <asp:ImageButton ID="btUSERID" ToolTip="User ID" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />
                    <asp:Label ID="txUSERDESC" Width="250" Enabled="false" runat="server"></asp:Label> 
                    
                </td>
             </tr>
             
             <tr id="trU2" runat="server">
                <td></td>            
                <td></td>         
                <td>
                    <asp:Panel ID="pnSEARCHUSERID" runat="server">
                    <asp:Label ID="lbSEARCHUSERID" Text="Employee Name : " runat="server"></asp:Label>
                    <asp:TextBox ID="mpSEARCHUSERID"  width="200" runat="server" BackColor="AntiqueWhite" ></asp:TextBox>
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
             
                                
            <tr id="tr2" runat="server">
                <td valign="top">
                    <asp:Label ID="lbLBRANCH" Text="Branch" runat="server"></asp:Label>
                    <asp:ImageButton ID="btSEARCHBRANCH" ToolTip="Kode BRANCHomer" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                                
                </td>
                <td valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txBRANCH" runat="server" Width="100"></asp:TextBox>
                    <asp:ImageButton ID="btBRANCH" ToolTip="Cari BRANCH" ImageUrl="~/images/toolbar/autocomplete.jpg" runat="server" />  
                    <asp:Label ID="lbBRANCH" Text="" runat="server"></asp:Label>                                               
                </td>
            </tr>
            
            <tr>                    
                <td valign="top"></td>            
                <td valign="top"></td> 
                <td valign="top">
                    <asp:Panel ID="pnSEARCHBRANCH" runat="server">                            
                        <asp:Label ID="lbSEARCHBRANCH" Text="Nama Branch : " runat="server"></asp:Label>
                        <asp:TextBox ID="mlSEARCHBRANCH" runat="server" BackColor="AntiqueWhite" Width="300"></asp:TextBox>
                        <asp:ImageButton ID="btSEARCHBRANCHSUBMIT" ToolTip="Search Branch" ImageUrl="~/images/toolbar/zoom.jpg" runat="server" />                            
                        <%--OnItemCommand="mlDATAGRIDBRANCH_ItemCommand"        --%>
                        <asp:DataGrid runat="server" ID="mlDATAGRIDBRANCH" 
                            HeaderStyle-BackColor="orange"  
                            HeaderStyle-VerticalAlign ="top"
                            HeaderStyle-HorizontalAlign="Center"
                            
                            autogeneratecolumns="false">	    
                            
                            <AlternatingItemStyle BackColor="#F9FCA8" />
                            <Columns>  
                                <asp:ButtonColumn  HeaderText = "Kode" DataTextField = "field_ID" ></asp:ButtonColumn>
                                <asp:ButtonColumn HeaderText = "Nama"  DataTextField = "Field_Name"></asp:ButtonColumn>
                            </Columns>
                         </asp:DataGrid> 
                    </asp:Panel>            
                </td>
             </tr>
             
             <tr>
                <td valign="top"><p><asp:Label ID="lbDESCRIPTION" runat="server" Text="Remarks"></asp:Label></p></td>
                <td valign="top">:</td>
                <td valign="top">
                    <asp:TextBox ID="txDESCRIPTION"  TextMode="MultiLine" width="250"  Height="80"  MaxLength="3999" runat="server" />
                </td>
             </tr>
             
            </table>
        </td>
        </tr>
        
        
        <tr id="tr8" runat="server">
            <td colspan="2">
                <br />                
                <p><asp:Label ID="Label2" Text="Main Component"  Font-Bold="true" runat="server"></asp:Label></p>            
                <asp:DataGrid runat="server" ID="mlDATAGRID_TOOLS11" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"                
                autogeneratecolumns="true">	                   
                               
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>
                     
                </Columns>
             </asp:DataGrid>  
            </td>
        </tr>
        
        <tr id="tr14" runat="server">
            <td colspan="2">
                <br />
                <p><asp:Label ID="Label3" Text="Additional Component"  Font-Bold="true" runat="server"></asp:Label></p>
                <asp:DataGrid runat="server" ID="mlDATAGRID_TOOLS21" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"                                
                autogeneratecolumns="true">	                   
                               
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>
                     
                </Columns>
             </asp:DataGrid>  
            </td>
        </tr>
        
        <tr id="trUP0" runat="server">
            <td colspan="2">
                <br />
                <p><asp:Label ID="Label4" Text="File"  Font-Bold="true" runat="server"></asp:Label></p>
                <asp:DataGrid runat="server" ID="mlDATAGRID2" 
                HeaderStyle-BackColor="orange"  
                HeaderStyle-VerticalAlign ="top"
                HeaderStyle-HorizontalAlign="Center"    
                OnItemCommand="mlDATAGRID2_ItemCommand"
                autogeneratecolumns="true">	                   
                               
                <AlternatingItemStyle BackColor="#F9FCA8" />
                <Columns>
                
                    <asp:TemplateColumn>
                        <ItemTemplate>
                        <asp:imagebutton id="btDeleteRecord" Runat="server" AlternateText="Delete" ImageUrl="~/images/toolbar/delete.jpg" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.No")%>' CommandName="DeleteRecord" OnClientClick="return confirm('Delete Record ?');">
                        </asp:imagebutton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:templatecolumn headertext="VW">
                    <ItemTemplate>        
                        <asp:hyperlink  Target="_blank"  runat="server" id="lnLINK1" navigateurl='<%# String.Format("../pj_fs/fs_file_download_auth.ashx?mpID={0}&mpNO={1}&mpTY=GP", Eval("DocNo"),Eval("No")) %>' text="DW"></asp:hyperlink>
                    </ItemTemplate>
                    </asp:templatecolumn>
                     
                </Columns>
             </asp:DataGrid>  
            </td>
        </tr>
               
        </table>
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL3" runat="server">    
    <table border="0" cellpadding="0" cellspacing="0" width="60%">
       <tr>
            <td valign="top"><p><asp:Label ID="lblKOMP1" Text="Main Component2"  Font-Bold="true" runat="server"></asp:Label></p></td>
            <td></td>
            <td valign="top"><p><asp:Label ID="lblKOMP2" Text="Additional Component3" Font-Bold="true" runat="server"></asp:Label></p></td>
       </tr>
       
       <tr>
       <td>
            <asp:Label ID="lbKOMP1" runat="server" Visible="false"></asp:Label>
            <asp:DataGrid runat="server" ID="mlDATAGRID_TOOLS1" 
            HeaderStyle-BackColor="orange"  
            HeaderStyle-VerticalAlign ="top"
            HeaderStyle-HorizontalAlign="Center"            
            autogeneratecolumns="false">	    

            <AlternatingItemStyle BackColor="#F9FCA8" />
            <Columns>
                
                <asp:TemplateColumn>
                    <ItemTemplate>
                    <asp:CheckBox id="mlCHECKBOX" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateColumn>                              
                                               
                <asp:BoundColumn HeaderText="Kode" DataField="field_ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Nama" DataField="Field_Name" DataFormatString ="{0:d}"></asp:BoundColumn>
                
            </Columns>
            </asp:DataGrid>  
             
        </td>
        <td></td>
        <td>
            <asp:Label ID="lbKOMP2" runat="server" Visible="false"></asp:Label>                                               
            <asp:DataGrid runat="server" ID="mlDATAGRID_TOOLS2" 
            HeaderStyle-BackColor="orange"  
            HeaderStyle-VerticalAlign ="top"
            HeaderStyle-HorizontalAlign="Center"            
            autogeneratecolumns="false">	    

            <AlternatingItemStyle BackColor="#F9FCA8" />
            <Columns>
                
                <asp:TemplateColumn>
                    <ItemTemplate>
                    <asp:CheckBox id="mlCHECKBOX" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateColumn>                              
                                               
                <asp:BoundColumn HeaderText="Kode" DataField="field_ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Nama" DataField="Field_Name" DataFormatString ="{0:d}"></asp:BoundColumn>
                
            </Columns>
            </asp:DataGrid>              
        </td> 
        </tr>
        
       
    </table>
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:Panel ID="pnFILL2" runat="server">
    <asp:HiddenField ID="lbFILEDOCNO" runat="server"/>    
    <table border="0" cellpadding="3" cellspacing="3" width="100%">
       <tr id="trUP1" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD1_N" Text="File 1 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD1_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload1"  runat="server" />         
                <br /><asp:HyperLink ID="lnLINK1" runat="server">ssss</asp:HyperLink>                
            </td>  
        </tr>
        
               
        <tr id="trUP2" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD2_N" Text="File 2 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD2_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload2" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK2" runat="server"></asp:HyperLink>
            </td>  
        </tr>
                
        <tr id="trUP3" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD3_N" Text="File 3 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD3_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload3" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK3" runat="server"></asp:HyperLink>
            </td>  
        </tr>
        
        <tr id="trUP4" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD4_N" Text="File 4 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD4_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload4" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK4" runat="server"></asp:HyperLink>
            </td>  
        </tr>
        
               
        <tr id="trUP5" runat="server">
            <td valign="top"><p><asp:Label ID="lbFILEUPLOAD5_N" Text="File 5 Desc" runat="server"></asp:Label></p></td>
            <td valign="top">:</td>
            <td valign="top">  
                <asp:TextBox ID="txFILEUPLOAD5_N" runat="server" Width="300"></asp:TextBox>     
                <asp:FileUpload ID="FileUpload5" runat="server" />         
                <br /><asp:HyperLink ID="lnLINK5" runat="server"></asp:HyperLink>
            </td>  
        </tr>       
         
    </table>
    <hr /><br />
</asp:Panel>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
<asp:CheckBox id="chkSORT" Text="Sort Descending" runat="server" />
<asp:Panel ID="pnGRID" runat="server">    
    
    <asp:DataGrid runat="server" ID="mlDATAGRID"
    OnItemCommand="mlDATAGRID_ItemCommand"
    
    AutoGenerateColumns = "true"
    ShowHeader="True"    
    OnItemDataBound ="mlDATAGRID_ItemBound"    
    
    AllowSorting="True"
    OnSortCommand="mlDATAGRID_SortCommand"        
    AllowPaging="True"    
    PagerStyle-Mode="NumericPages"
    PagerStyle-HorizontalAlign="center"
    OnPageIndexChanged="mlDATAGRID_PageIndex"  
    PageSize="50"  
    
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

</asp:Table>
</form>
<br /><br /><br /><br />

</asp:Content>

