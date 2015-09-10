#Region "Revision History"
'$Header: /Clipan/eLsaClipan/UserController/ValidDate.ascx.vb 5     5/21/05 1:45p Henry $
'$Workfile: ValidDate.ascx.vb $
'-----------------------------------------------------------
'$Log: /Clipan/eLsaClipan/UserController/ValidDate.ascx.vb $
'
'5     5/21/05 1:45p Henry
'
'4     5/21/05 1:16p Henry
'  
'  3     11/28/03 12:00p Henry
'  
'  2     9/12/03 9:27p Henry
'  
'  1     8/23/03 11:10a Henry
'  
'  7     8/14/03 4:47p Petra
'  
'  6     8/13/03 10:28a Henry
'  
'  5     8/05/03 9:02a Vera
'  
'  4     7/31/03 3:10p Kukuh
'  
'  3     7/28/03 10:51a Henry
'  
'  2     7/01/03 3:22p Henry
'-----------------------------------------------------------
#End Region
Public MustInherit Class ValidDate
    Inherits System.Web.UI.UserControl

    '''Protected WithEvents cvlDate As System.Web.UI.WebControls.CustomValidator
    '''Protected WithEvents rfvRequired As System.Web.UI.WebControls.RequiredFieldValidator
    '''Protected WithEvents txtDate As System.Web.UI.WebControls.TextBox
    '''Protected WithEvents calDate As System.Web.UI.WebControls.Calendar
    '''Protected WithEvents pnlDate As System.Web.UI.WebControls.Panel
    '''Protected WithEvents hplDate As System.Web.UI.WebControls.HyperLink
    '''Protected WithEvents divButton As System.Web.UI.HtmlControls.HtmlGenericControl


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Public Event TextChanged As EventHandler
    Public Event SelectionChanged As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            If Not CType(ViewState("ispostback"), String) <> "" Then
                'cvlDate.ErrorMessage = "Invalid Format Date" // Remark Samsudin 11 Aug 09
                cvlDate.ErrorMessage = "Please fill date with DD/MM/YYYY"
                'cvlDate.Display = ValidatorDisplay.None // Remark by Susanti 11 Aug 09
                rfvRequired.ErrorMessage = "You have to fill Date"
                'rfvRequired.Display = ValidatorDisplay.None //Remark by Erikd 02 March 09
                'txtDate.ReadOnly = True
                'rfvRequired.Enabled = False //Remark by Erikd 02 March 09
            End If
        End If
        Try
            If txtDate.Text.Trim <> "" Then cvlDate.Validate()
        Catch exp As Exception
            Response.Write("Please Fill date with dd/MM/yyyy")
        End Try
        'If Not IsPostBack Then
        '    If Not CType(ViewState("ispostback"), String) <> "" Then
        '        cvlDate.ErrorMessage = "Invalid Format Date"
        '        cvlDate.Display = ValidatorDisplay.None

        '        rfvRequired.ErrorMessage = "You have to fill Date"
        '        rfvRequired.Display = ValidatorDisplay.None
        '        'txtDate.ReadOnly = True
        '        rfvRequired.Enabled = False
        '    End If
        'End If
        'Try
        '    If txtDate.Text.Trim <> "" Then cvlDate.Validate()
        'Catch exp As Exception
        '    Response.Write("Please Fill date with dd/MM/yyyy")
        'End Try

    End Sub

#Region "Properties"
    Public Property isCalendarPostBack() As Boolean
        Get
            Return txtDate.AutoPostBack
        End Get
        Set(ByVal blnValue As Boolean)
            txtDate.AutoPostBack = blnValue
            viewstate("ispostback") = blnValue
        End Set
    End Property

    Public Property isEnabled() As Boolean
        Get
            Return txtDate.Enabled
        End Get
        Set(ByVal Value As Boolean)
            With txtDate
                '.Enabled = Value
                If Value = False Then
                    .BorderStyle = BorderStyle.None
                Else
                    .BorderStyle = BorderStyle.NotSet
                End If
                .ReadOnly = Not Value
                '.ForeColor = Black
            End With
            divButton.Visible = Value
            'PnlButton.Visible = Value
        End Set
    End Property

    Public Property ValidationErrMessage() As String
        Get
            Return cvlDate.ErrorMessage
        End Get
        Set(ByVal StrErrMsg As String)
            cvlDate.ErrorMessage = StrErrMsg
        End Set
    End Property

    Public Property FillRequired() As Boolean
        Get
            Return rfvRequired.Enabled
        End Get
        Set(ByVal Value As Boolean)
            rfvRequired.Enabled = Value
        End Set
    End Property
    Public Property CSSClass() As String
        Get
            Return txtDate.CssClass
        End Get
        Set(ByVal Value As String)
            txtDate.CssClass = Value
        End Set
    End Property

    Public Property FieldRequiredMessage() As String
        Get
            Return rfvRequired.ErrorMessage
        End Get
        Set(ByVal Value As String)
            rfvRequired.ErrorMessage = Value
        End Set
    End Property

    Public Property dateValue() As String
        Get
            Return txtDate.Text
        End Get
        Set(ByVal Value As String)
            txtDate.Text = Value
        End Set
    End Property

    Public Property Display() As String
        Get
            Return CType(rfvRequired.Display, String)
        End Get
        Set(ByVal Value As String)
            Select Case UCase(Value)
                Case "NONE" : rfvRequired.Display = ValidatorDisplay.None
                    cvlDate.Display = ValidatorDisplay.None
                Case Else
                    rfvRequired.Display = ValidatorDisplay.Dynamic
                    cvlDate.Display = ValidatorDisplay.Dynamic
            End Select
        End Set
    End Property

    Public ReadOnly Property isValidFormatDate() As Boolean
        Get
            Return Check_Date(txtDate.Text.Trim)
        End Get
    End Property

    Public Property txtDateObj() As TextBox
        Get
            Return txtDate
        End Get
        Set(ByVal value As TextBox)
            txtDate = value
        End Set
    End Property
#End Region

    Private Function Check_Date(ByVal lStrDate As String) As Boolean
        Dim lArrDate() As String
        lArrDate = Split(lStrDate, "/")
        Dim lDteDate As Date
        Dim lBlnResult As Boolean = False
        If UBound(lArrDate) <> 2 Then
            lBlnResult = False
        Else
            If lArrDate(2).Length <> 4 Then
                lBlnResult = False
            Else

                lDteDate = New Date(CType(lArrDate(2), Integer), CType(lArrDate(1), Integer), CType(lArrDate(0), Integer))
                lBlnResult = True
            End If
        End If
        Return lBlnResult
    End Function

    Public Sub isValidDate(ByVal source As Object, ByVal e As ServerValidateEventArgs)
        e.IsValid = Check_Date(e.Value)
    End Sub

    Private Sub txtDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.TextChanged
        RaiseEvent TextChanged(sender, e)
    End Sub

    Public Function getClientID() As String
        Return txtDate.ClientID.Trim
    End Function

    Public Sub Disable()
        txtDate.Enabled = False
    End Sub

    Public Sub Enable()
        txtDate.Enabled = True
    End Sub

End Class
