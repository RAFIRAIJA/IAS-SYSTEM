<%@ Page Title="" Language="C#" MasterPageFile="~/PageSetting/MasterIntern.master" AutoEventWireup="true" CodeFile="LookUp_Asset.aspx.cs" Inherits="UserController_FORM_LookUp_Asset" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <title>LooKUp Asset </title>
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="~/script/JavaScript/Elsa.js"></script>
    <script type="text/javascript" src="~/script/JavaScript/Eloan.js"></script>
    <script type="text/javascript">
        function WinClose() {
            Window.close();
        }
    </script>
    <script language="javascript">
        function sendRequest(pAssetID, pAssetDesc) {
            window.alert(pAssetID);
            
            with (document.forms) {

                var lObjName = '<%= Request.QueryString["txtAssetID"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pAssetID;
                }
                var lObjName = '<%= Request.QueryString["txtAssetDesc"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pAssetDesc;
                }
                var lObjName = '<%= Request.QueryString["hdnAssetID"]%>';
               if (eval('opener.document.forms[0].' + lObjName)) {
                   eval('opener.document.forms[0].' + lObjName).value = pAssetID;
               }
               var lObjName = '<%= Request.QueryString["hdnAssetDesc"]%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pAssetDesc;
                }
                window.close();
            }

        }
    </script>
    </head>
    <body>
        <form id="form2" runat="server">
        <ajaxtoolkit:toolkitscriptmanager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />
        <input id="hdnAssetID" type="hidden" name="hdnAssetID" runat="server" style="z-index: 103;
            left: 136px; position: absolute; top: 8px">
        <input id="hdnAssetDesc" type="hidden" name="hdnAssetDesc" runat="server" style="z-index: 104;
            left: 288px; position: absolute; top: 8px">
        
        <div>
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
            <br />
  	       <asp:panel id="PnlItem" runat="server" Width="100%" >
				    <table width="100%" border="0" cellpadding="0" cellspacing="0" align ="center">
                        <tr class="trtopikuning"> 
                          <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                          <td align="center" class="tdtopi">ASSET SEARCH</td>
                          <td width="10" class="tdtopikanan">&nbsp;</td>
                        </tr>
                      </table>
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="tablegrid" align ="center">
                        <tr> 
                          <td width="20%" class="tdganjil">Entity</td>
                          <td class="tdganjil">
      	                    <asp:DropDownList runat="server" id="ddlEntity">
			                    <asp:ListItem Value=" ">Select One</asp:ListItem>
			                    <asp:ListItem Value="ISSN3" Selected="True">ISS</asp:ListItem>
			                    <asp:ListItem Value="IFS3">IFS</asp:ListItem>
			                    <asp:ListItem Value="ICS3">ICS</asp:ListItem>
		                    </asp:DropDownList>
                          </td>
                        </tr> 
                        <tr> 
                          <td width="20%" class="tdganjil">Search By</td>
                          <td class="tdganjil">
      	                    <asp:DropDownList runat="server" id="ddlSearchBy">
			                    <asp:ListItem Value="">Select One</asp:ListItem>
			                    <asp:ListItem Value="ItemNo">ItemNo</asp:ListItem>
			                    <asp:ListItem Value="ItemDesc">Item Name</asp:ListItem>
		                    </asp:DropDownList>
		                    <asp:TextBox runat="server"  CssClass="inptype"  id="txtSearchBy" Width="153px"></asp:TextBox>
                          </td>
                        </tr>       
                    </table>
				    <table cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
					    <tr>
						    <td>
							    <div align="right">
								    <asp:imagebutton id="imgsearch" runat="server" width="100" imageurl="~/Images/button/ButtonSearch.gif"
									    height="20" onclick="imgsearch_Click"></asp:imagebutton>
								    <asp:imagebutton id="imbReset" runat="server" imageurl="~/Images/button/ButtonReset.gif" causesvalidation="False" OnClick="imbReset_Click"></asp:imagebutton>
							    </div>
						    </td>
					    </tr>
				    </table>
	       </asp:panel>
	       <br>
	       <asp:Panel ID="Panel2" Runat="server" Width="100%" >
				    <table width="100%" border="0" cellspacing="0" cellpadding="0"  align ="center">
	                    <tr class="trtopikuning">
		                    <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
		                    <td align="center" class="tdtopi">ASSET LISTING</td>
		                    <td width="10" class="tdtopikanan">&nbsp;</td>
	                    </tr>
	                </table>
                    <asp:DataGrid ID="dgListData" runat="server" Width="100%" AutoGenerateColumns="false"
                        AllowSorting="True" BorderColor="Black" 
                        borderwidth="0px" cellpadding="3" cellspacing="1" ForeColor="#333333" CssClass="tablegrid" 
                          onitemdatabound="dgListData_ItemDataBound" HorizontalAlign="Center">
                        <SelectedItemStyle CssClass="tdgenap"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="tdgenap"></AlternatingItemStyle>
                        <ItemStyle CssClass="tdganjil"></ItemStyle>
                        <HeaderStyle CssClass="tdjudul"></HeaderStyle>
                        <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                        <Columns>                        
                            <asp:TemplateColumn HeaderText="SELECT">
                                <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                <ItemTemplate>
                                <input id="rdPilih" name="rdPilih" 
                                    onclick="sendRequest('<%# DataBinder.Eval(Container.DataItem, "AssetID") %>    '
                                                         ,'<%# DataBinder.Eval(Container.DataItem, "AssetDesc") %>    '
                                                                                                              
                                                        );"
                                    type="radio" />
                                               
                                </ItemTemplate>
                            </asp:TemplateColumn>
             
                            <asp:TemplateColumn HeaderText="ASSET NO" SortExpression="AssetID">
                                <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                   <asp:Label ID="lblAssetID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetID") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ASSET NAME" SortExpression="AssetDesc">
                                <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>                               
                                   <asp:Label ID="lblAssetDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetDesc")%>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderText="ASSET STATUS" SortExpression="AssetStatus">
                                <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>                               
                                   <asp:Label ID="lblAssetStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetStatus")%>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderText="LOCATION" SortExpression="AssetLocate">
                                <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>                               
                                   <asp:Label ID="lblAssetLocate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetLocate")%>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
				
				    <table cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
					    <tr>
                        <td>
                            <uc2:ucPaging runat="server" id="pagingLookUp" 
                                OnNavigationButtonClicked="NavigationButtonClicked" PageSize="20"  ></uc2:ucPaging>        
                        </td>
                    </tr>
				    </table>
		    </asp:Panel>
  	       
        </div>

        </form>
    </body>

    </html>
</asp:Content>

