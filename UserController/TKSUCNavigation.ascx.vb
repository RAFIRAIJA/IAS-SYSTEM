#Region "Imports"
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports TurnKey
#End Region
Partial Class TKSUCNavigation
    Inherits System.Web.UI.UserControl
    'This is Event Declaration
    Public Event NavigationButtonClicked As NavigationButtonClick
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region "Properties"
    Public ReadOnly Property GridCurrentPage() As Integer
        Get
            Dim iCurrentPage As Integer = CType(ViewState("__vsGridCurrentPage" & Me.ClientID.Trim), Integer)
            iCurrentPage = IIf(iCurrentPage <= 0, 1, iCurrentPage)
            Return iCurrentPage
        End Get
    End Property
    Public ReadOnly Property GridTotalRecords() As Integer
        Get
            Dim iTotalRecords As Integer = CType(ViewState("__vsGridTotalRecords" & Me.ClientID.Trim), Integer)
            iTotalRecords = IIf(iTotalRecords <= 0, 0, iTotalRecords)
            Return iTotalRecords
        End Get
    End Property
    Public ReadOnly Property GridRowPerPage() As Integer
        Get
            Dim iRowPerPage As Integer = CType(ViewState("__vsGridRowPerPage" & Me.ClientID.Trim), Integer)
            iRowPerPage = IIf(iRowPerPage <= 0, 10, iRowPerPage)
            Return iRowPerPage
        End Get
    End Property
    Public ReadOnly Property GridTotalPage() As Integer
        Get
            Dim iTotalPage As Integer = CType(ViewState("__vsGridTotalPage" & Me.ClientID.Trim), Integer)
            iTotalPage = IIf(iTotalPage <= 1, 1, iTotalPage)
            Return iTotalPage
        End Get
    End Property
#End Region
#Region "Subs"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub
    Protected Overridable Sub OnNavigationButtonClicked(ByVal e As TKSUCNavigationButtonEventArgs)
        RaiseEvent NavigationButtonClicked(e)
    End Sub
    Private Sub ImgNavFirst_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNavFirst.Click
        Dim oEventArgs As TKSUCNavigationButtonEventArgs = New TKSUCNavigationButtonEventArgs
        Me.txtNavPageNoGo.Text = "1"
        oEventArgs.PageToGo = 1
        ViewState("__vsGridCurrentPage" & Me.ClientID.Trim) = 1
        Me.OnNavigationButtonClicked(oEventArgs)
    End Sub
    Private Sub ImgNavPrevious_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNavPrevious.Click
        Dim oEventArgs As TKSUCNavigationButtonEventArgs = New TKSUCNavigationButtonEventArgs
        oEventArgs.ButtonClicked = TKSUCClickedButton.PreviousRecordButton
        oEventArgs.PageToGo = IIf((Me.GridCurrentPage - 1) <= 1, 1, (Me.GridCurrentPage - 1))
        Me.txtNavPageNoGo.Text = CType(IIf((Me.GridCurrentPage - 1) <= 1, 1, (Me.GridCurrentPage - 1)), Integer).ToString.Trim
        ViewState("__vsGridCurrentPage" & Me.ClientID.Trim) = CType(IIf((Me.GridCurrentPage - 1) <= 1, 1, (Me.GridCurrentPage - 1)), Integer)
        Me.OnNavigationButtonClicked(oEventArgs)
    End Sub
    Private Sub ImgNavNext_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNavNext.Click
        Dim oEventArgs As TKSUCNavigationButtonEventArgs = New TKSUCNavigationButtonEventArgs
        oEventArgs.ButtonClicked = TKSUCClickedButton.NextRecordButton
        oEventArgs.PageToGo = CType(IIf((Me.GridCurrentPage + 1) >= Me.GridTotalPage, Me.GridTotalPage, (Me.GridCurrentPage + 1)), Integer)
        Me.txtNavPageNoGo.Text = CType(IIf((Me.GridCurrentPage + 1) >= Me.GridTotalPage, Me.GridTotalPage, (Me.GridCurrentPage + 1)), Integer).ToString.Trim
        ViewState("__vsGridCurrentPage" & Me.ClientID.Trim) = CType(IIf((Me.GridCurrentPage + 1) >= Me.GridTotalPage, Me.GridTotalPage, (Me.GridCurrentPage + 1)), Integer)
        Me.OnNavigationButtonClicked(oEventArgs)
    End Sub
    Private Sub ImgNavLast_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNavLast.Click
        Dim oEventArgs As TKSUCNavigationButtonEventArgs = New TKSUCNavigationButtonEventArgs
        oEventArgs.ButtonClicked = TKSUCClickedButton.LastRecordButton
        oEventArgs.PageToGo = Me.GridTotalPage
        Me.txtNavPageNoGo.Text = Me.GridTotalPage.ToString.Trim
        ViewState("__vsGridCurrentPage" & Me.ClientID.Trim) = Me.GridTotalPage
        Me.OnNavigationButtonClicked(oEventArgs)
    End Sub
    Private Sub ImgNavGo_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNavGo.Click
        Dim oEventArgs As TKSUCNavigationButtonEventArgs = New TKSUCNavigationButtonEventArgs
        Dim iPageNo As Integer
        oEventArgs.ButtonClicked = TKSUCClickedButton.GoToPageButton
        Try
            iPageNo = CType(Me.txtNavPageNoGo.Text.Trim, Integer)
        Catch ex As Exception
            iPageNo = Me.GridCurrentPage
        End Try
        If iPageNo > Me.GridTotalPage Then
            iPageNo = Me.GridTotalPage
        End If
        oEventArgs.PageToGo = iPageNo
        Me.txtNavPageNoGo.Text = iPageNo.ToString.Trim
        ViewState("__vsGridCurrentPage" & Me.ClientID.Trim) = iPageNo
        Me.OnNavigationButtonClicked(oEventArgs)
    End Sub
#End Region
#Region "Functions"
    Public Function RefreshNavigationButtons(ByVal iTotalRecord As Integer, _
                                             ByVal PageSize As Integer, _
                                             ByVal PageNumber As Integer) As Boolean
        ViewState("__vsGridCurrentPage" & Me.ClientID.Trim) = PageNumber
        ViewState("__vsGridTotalRecords" & Me.ClientID.Trim) = iTotalRecord
        ViewState("__vsGridRowPerPage" & Me.ClientID.Trim) = PageSize
        If Int(iTotalRecord / PageSize) <> (iTotalRecord / PageSize) Then
            ViewState("__vsGridTotalPage" & Me.ClientID.Trim) = Int(iTotalRecord / PageSize) + 1
        Else
            ViewState("__vsGridTotalPage" & Me.ClientID.Trim) = Int(iTotalRecord / PageSize)
        End If
        Me.txtNavPageNoGo.Enabled = True
        If PageNumber < 1 Then
            PageNumber = 1
        End If
        If iTotalRecord < 1 Then
            Me.ImgNavFirst.Enabled = False
            Me.ImgNavPrevious.Enabled = False
            Me.ImgNavNext.Enabled = False
            Me.ImgNavLast.Enabled = False
            Me.txtNavPageNoGo.Text = "1"
            Me.lblNavInfo.Text = "No Record Exists"
        Else
            If PageNumber <= 1 Then
                Me.ImgNavFirst.Enabled = False
                Me.ImgNavPrevious.Enabled = False
            Else
                Me.ImgNavFirst.Enabled = True
                Me.ImgNavPrevious.Enabled = True
            End If
            If PageNumber >= Me.GridTotalPage Then
                Me.ImgNavNext.Enabled = False
                Me.ImgNavLast.Enabled = False
            Else
                Me.ImgNavNext.Enabled = True
                Me.ImgNavLast.Enabled = True
            End If
            Me.txtNavPageNoGo.Text = PageNumber.ToString
            Me.lblNavInfo.Text = "Records Count : " & iTotalRecord.ToString.Trim & " [ Page " & PageNumber.ToString.Trim & " of " & Me.GridTotalPage.ToString.Trim & " ]"
        End If
    End Function
#End Region
End Class
Public Enum TKSUCClickedButton
    FirstRecordButton
    PreviousRecordButton
    NextRecordButton
    LastRecordButton
    GoToPageButton
End Enum
Public Class TKSUCNavigationButtonEventArgs
    Inherits System.EventArgs
    Private _ClickedButton As TKSUCClickedButton = TKSUCClickedButton.FirstRecordButton
    Private _PageToGo As Integer = 1
    Public Property ButtonClicked() As TKSUCClickedButton
        Get
            Return _ClickedButton
        End Get
        Set(ByVal Value As TKSUCClickedButton)
            _ClickedButton = Value
        End Set
    End Property
    Public Property PageToGo() As Integer
        Get
            Return _PageToGo
        End Get
        Set(ByVal Value As Integer)
            _PageToGo = Value
        End Set
    End Property
End Class
Public Delegate Sub NavigationButtonClick(ByVal e As TKSUCNavigationButtonEventArgs)
