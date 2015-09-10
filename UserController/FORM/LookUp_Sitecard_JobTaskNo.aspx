<%@ Page Title="" Language="VB" MasterPageFile="~/PageSetting/MasterIntern.master" AutoEventWireup="false" CodeFile="LookUp_Sitecard_JobTaskNo.aspx.vb" Inherits="UserController_FORM_LookUp_Sitecard_JobTaskNo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="~/usercontroller/ucPaging.ascx" tagname="ucPaging" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mpCONTENT" Runat="Server">
    <title>LooKUp SiteCard </title>
    <link href="~/script/NewStyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="~/script/JavaScript/Elsa.js"></script>
    <script type="text/javascript" src="~/script/JavaScript/Eloan.js"></script>
    <script type="text/javascript">
        function WinClose() {
            Window.close();
        }
    </script>
    <script language="javascript">
        function sendRequest(pSiteCardID, pSiteCardName, pJobNo, pJobTaskNo) {
            with (document.forms) {
                var lObjName = '<%= Request.QueryString("txtJobNo")%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pJobNo;
                }
                var lObjName = '<%= Request.QueryString("txtJobTaskNo")%>';
               if (eval('opener.document.forms[0].' + lObjName)) {
                   eval('opener.document.forms[0].' + lObjName).value = pJobTaskNo;
               }
               var lObjName = '<%= Request.QueryString("SitecardID")%>';
               if (eval('opener.document.forms[0].' + lObjName)) {
                   eval('opener.document.forms[0].' + lObjName).value = pSiteCardID;
               }
               var lObjName = '<%= Request.QueryString("SiteCardName")%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pSiteCardName;
                }
                var lObjName = '<%= Request.QueryString("hdnSiteCardID")%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pSiteCardID;
                }
                var lObjName = '<%= Request.QueryString("hdnSiteCardName")%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pSiteCardName;
                }
                var lObjName = '<%= Request.QueryString("hdnJobNo")%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pJobNo;
                }
                var lObjName = '<%= Request.QueryString("hdnJobTaskNo")%>';
                if (eval('opener.document.forms[0].' + lObjName)) {
                    eval('opener.document.forms[0].' + lObjName).value = pJobTaskNo;
                }
                window.close();
            }

            
        }
    </script>

    
    <form id="form2" runat="server">
    <ajaxtoolkit:toolkitscriptmanager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ToolkitScriptManager1" />


        <input id="Hidden1" type="hidden" name="hdnSiteCardID" runat="server" style="z-index: 103;
            left: 136px; position: absolute; top: 8px">
        <input id="Hidden2" type="hidden" name="hdnSiteCardName" runat="server" style="z-index: 104;
            left: 288px; position: absolute; top: 8px">
        <input id="Hidden3" type="hidden" name="hdnJobNo" runat="server" style="z-index: 103;
            left: 136px; position: absolute; top: 8px">
        <input id="Hidden4" type="hidden" name="hdnJobTaskNo" runat="server" style="z-index: 104;
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
  	   <asp:panel id="Panel1" runat="server" Width="100%" >
				<table width="100%" border="0" cellpadding="0" cellspacing="0" align ="center">
                    <tr class="trtopikuning"> 
                      <td width="10" height="20" class="tdtopikiri">&nbsp;</td>
                      <td align="center" class="tdtopi">SITECARD - JOB[TASK.NO] SEARCH</td>
                      <td width="10" class="tdtopikanan">&nbsp;</td>
                    </tr>
                  </table>
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="tablegrid" align ="center">
                    <tr> 
                      <td width="20%" class="tdganjil">Entity</td>
                      <td class="tdganjil">
      	                <asp:DropDownList runat="server" id="ddlEntity">
			                <asp:ListItem Value=" ">Select One</asp:ListItem>
			                <asp:ListItem Value="ISSN">ISS</asp:ListItem>
			                <asp:ListItem Value="IFSN" Selected="True">IFS</asp:ListItem>
			                <asp:ListItem Value="ICSN">ICS</asp:ListItem>
			                <asp:ListItem Value="IPMN">IPM</asp:ListItem>

		                </asp:DropDownList>
                      </td>
                    </tr> 
                    <tr> 
                      <td width="20%" class="tdganjil">Search By</td>
                      <td class="tdganjil">
      	                <asp:DropDownList runat="server" id="ddlSearchBy">
			                <asp:ListItem Value="">Select One</asp:ListItem>
			                <asp:ListItem Value="SiteCard">SiteCard</asp:ListItem>
			                <asp:ListItem Value="Description">Description</asp:ListItem>
			                <asp:ListItem Value="JobNo">JobNo</asp:ListItem>
			                <asp:ListItem Value="JobTaskNo">JobTaskNo</asp:ListItem>

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
		                <td align="center" class="tdtopi">SITECARD - JOB[TASK.NO] LISTING</td>
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
                                onclick="sendRequest('<%# DataBinder.Eval(Container.DataItem, "SiteCard") %>    '
                                                     ,'<%# DataBinder.Eval(Container.DataItem, "SiteCardName") %>    '
                                                     ,'<%# DataBinder.Eval(Container.DataItem, "Job_No")%>    '
                                                     ,'<%# DataBinder.Eval(Container.DataItem, "Job_TaskNo") %>    '                                                     
                                                    );"
                                type="radio" />
                                               
                            </ItemTemplate>
                        </asp:TemplateColumn>
             
                        <asp:TemplateColumn HeaderText="SITECARD" SortExpression="sitecard">
                            <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                               <asp:Label ID="lblSitecard" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Sitecard") %>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SITECARD NAME" SortExpression="SiteCardName">
                            <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>                               
                               <asp:Label ID="lblSiteCardName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SiteCardName")%>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="JOB NO" SortExpression="JobNo">
                            <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>                               
                               <asp:Label ID="lblJobNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Job_No")%>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="JOB TASK NO" SortExpression="JobTaskNo">
                            <HeaderStyle HorizontalAlign="Center" Height="30px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>                               
                               <asp:Label ID="lblJobTaskNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Job_TaskNo")%>' />
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
</asp:Content>

