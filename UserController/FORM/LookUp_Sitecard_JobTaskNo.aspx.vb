Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Imports IAS.Core.CSCode
Imports IAS.Initialize
Imports IAS.APP.DataAccess.UT


Partial Class UserController_FORM_LookUp_Sitecard_JobTaskNo
    Inherits System.Web.UI.Page

    Dim mlREADER As OleDbDataReader
    Dim mlREADER2 As OleDbDataReader

    Dim mlVarCore As New VariableCore()
    Dim oEnt As New VariableUT_Mapping()
    Dim oDA As New FunctionUT_Mapping()

    Dim oMGF As New ModuleGeneralFunction()
    Dim mlOBJPJ As New ModuleFunctionLocal()

    'region !--PagingVariable--!
    'Public Property PBiaya_Transport() As String
    '    Get
    '        Return FBiaya_Transport
    '    End Get
    '    Set(ByVal value As String)
    '        FBiaya_Transport = value
    '    End Set
    'End Property

    Public Property PageSize As Integer
    Public Property currentPage As Integer
    Public Property totalPages As Integer
    Public Property totalRecords As Integer
    Public Property cmdWhere As String
    Public Property sortBy As String
    Public Property mlMENUSTYLE As String
    Public Property mlCompanyID As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        mlMENUSTYLE = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
        Me.MasterPageFile = mlMENUSTYLE
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE").ToString() + " LOOKUP MAPPING SITECARD - JOB[TASKNO] V2.02"
        mlTITLE.Text = "LOOKUP MAPPING SITECARD - JOB[TASKNO] V2.02"
        Session("mgDateTime") = System.DateTime.Now

        mlCompanyID = ddlEntity.SelectedValue.ToString()
        If Not IsPostBack Then


            PageSize = CType(pagingLookUp.PageSize.ToString(), Integer)
            currentPage = 1
            cmdWhere = ""
            sortBy = ""

            SearchRecord()
        End If

    End Sub
    Protected Sub CekSession()

        If IsNothing(Session("mgUSERID")) Then
            Response.Redirect("~/pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return

        End If
    End Sub
    Protected Sub NavigationButtonClicked(e As usercontroller_ucPaging.NavigationButtonEventArgs)
        retrievepaging()
    End Sub
    Protected Sub retrievepaging()

        currentPage = pagingLookUp.currentPage
        PageSize = CType(pagingLookUp.PageSize.ToString(), Integer)
        RetrieveData()
    End Sub
    Protected Sub RetrieveData()

        oEnt.MappingType = "SiteCard"
        oEnt.CompanyID = mlCompanyID
        oEnt.ModuleID = "PB"
        oEnt.ErrorMesssage = ""
        oEnt.PageSize = PageSize
        oEnt.CurrentPage = currentPage
        oEnt.WhereCond = cmdWhere
        oEnt.SortBy = sortBy
        oDA.GetMappingPaging(oEnt)
        If oEnt.ErrorMesssage <> "" Then

            mlMESSAGE.Text = oEnt.ErrorMesssage
        End If

        dgListData.Visible = True
        dgListData.DataSource = oEnt.dtListData
        dgListData.DataBind()

        totalRecords = oEnt.TotalRecord
        pagingLookUp.PageSize = Long.Parse(PageSize.ToString())
        pagingLookUp.currentPage = currentPage
        pagingLookUp.PagingFooter(totalRecords, PageSize)
    End Sub
    Protected Sub SearchRecord()

        cmdWhere = ""
        If ddlSearchBy.SelectedValue <> "" Then

            If (txtSearchBy.Text.Contains("%")) Then

                cmdWhere += " and " + ddlSearchBy.SelectedValue + " like '%" + txtSearchBy.Text + "%'"

            Else

                cmdWhere += " and " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text + "'"

            End If
        End If

        RetrieveData()
    End Sub
    Protected Sub ResetRecord()

        Response.Redirect("LookUpSiteCard.aspx")
    End Sub    
    Protected Sub dgListData_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListData.ItemDataBound

    End Sub

    Protected Sub imgsearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgsearch.Click
        SearchRecord()
    End Sub

    Protected Sub imbReset_Click(sender As Object, e As ImageClickEventArgs) Handles imbReset.Click
        ResetRecord()
    End Sub
End Class
