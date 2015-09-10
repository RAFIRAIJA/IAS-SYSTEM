#Region " Revision History "

'$Header: /Clipan/eLsaClipan/UserController/ucInputNumberWithoutDecimal.ascx.vb 4     1/11/05 8:32a Rony $

'$Workfile: ucInputNumberWithoutDecimal.ascx.vb $

'-----------------------------------------------------------

'$Log: /Clipan/eLsaClipan/UserController/ucInputNumberWithoutDecimal.ascx.vb $
'
'4     1/11/05 8:32a Rony
'
'3     1/10/05 10:05a Rony

'-----------------------------------------------------------

#End Region

Public Class ucInputNumberWithoutDecimal
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    '''Protected WithEvents txtInput As System.Web.UI.WebControls.TextBox
    '''Protected WithEvents rgvInput As System.Web.UI.WebControls.RangeValidator
    '''Protected WithEvents txtInputDummy As System.Web.UI.WebControls.TextBox
    '''Protected WithEvents rfvInput As System.Web.UI.WebControls.RequiredFieldValidator
    '''Protected WithEvents cpvInput As System.Web.UI.WebControls.CompareValidator
    '''Protected WithEvents ltlMandatory As System.Web.UI.WebControls.Literal
    '''Protected WithEvents ltlPercent As System.Web.UI.WebControls.Literal
    '''Protected WithEvents hdnInput As System.Web.UI.HtmlControls.HtmlInputHidden
    '''Protected WithEvents scriptJava As System.Web.UI.HtmlControls.HtmlGenericControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Property"
    Public Property Text() As String
        Get
            If hdnInput.Value <> "" Then
                Return hdnInput.Value
            Else
                Return txtInput.Text.Replace(",", "")
            End If

        End Get
        Set(ByVal Value As String)
            txtInput.Text = Value
            hdnInput.Value = Value
        End Set
    End Property

    Public Property TextHidden() As String
        Get
            Return hdnInput.Value
        End Get
        Set(ByVal Value As String)
            hdnInput.Value = Value
        End Set
    End Property

    Public Property TextDummy() As String
        Get
            Return txtInputDummy.Text
        End Get
        Set(ByVal Value As String)
            txtInputDummy.Text = Value
        End Set
    End Property

    Public WriteOnly Property IsNull() As Boolean
        Set(ByVal Value As Boolean)
            rfvInput.Enabled = Value
            rgvInput.Enabled = Value
            cpvInput.Enabled = Value
        End Set
    End Property

    Public WriteOnly Property IsRequiredField() As Boolean
        Set(ByVal Value As Boolean)
            rfvInput.Enabled = Value
            rfvInput.InitialValue = "0.00"
            'ltlMandatory.Visible = Value
            If (Value = True) Then
                txtInput.CssClass = "inptypemandatory"
            Else
                txtInput.CssClass = "inptype"
            End If
        End Set
    End Property

    Public WriteOnly Property IsRange() As Boolean
        Set(ByVal Value As Boolean)
            rgvInput.Enabled = Value
        End Set
    End Property

    Public WriteOnly Property IsCompare() As Boolean
        Set(ByVal Value As Boolean)
            cpvInput.Enabled = Value
        End Set
    End Property

    Public Property MaxValue() As String
        Set(ByVal Value As String)
            rgvInput.MaximumValue = Value
        End Set
        Get
            Return rgvInput.MaximumValue
        End Get
    End Property

    Public Property MinValue() As String
        Set(ByVal Value As String)
            rgvInput.MinimumValue = Value
        End Set
        Get
            Return rgvInput.MinimumValue
        End Get
    End Property

    Public WriteOnly Property MaxLength() As Integer
        Set(ByVal Value As Integer)
            txtInput.MaxLength = Value
        End Set
    End Property
    Public WriteOnly Property InisialValue() As Integer
        Set(ByVal Value As Integer)
            rfvInput.InitialValue = Value
        End Set
    End Property

    Public WriteOnly Property Colums() As Integer
        Set(ByVal Value As Integer)
            txtInput.Columns = Value
        End Set
    End Property

    Public WriteOnly Property NotEditable() As Boolean
        Set(ByVal Value As Boolean)
            txtInput.ReadOnly = Value
        End Set
    End Property

    Public WriteOnly Property IsEnabled() As Boolean
        Set(ByVal Value As Boolean)
            txtInput.Enabled = Value
        End Set
    End Property

    Public WriteOnly Property isViewPercent() As Boolean
        Set(ByVal Value As Boolean)
            ltlPercent.Visible = Value
        End Set
    End Property

    Public WriteOnly Property RequiredMessage() As String
        Set(ByVal Value As String)
            rfvInput.ErrorMessage = Value
        End Set
    End Property

    Public WriteOnly Property RangeMessage() As String
        Set(ByVal Value As String)
            rgvInput.ErrorMessage = Value
        End Set
    End Property

    Public WriteOnly Property CompareMessage() As String
        Set(ByVal Value As String)
            cpvInput.ErrorMessage = Value
        End Set
    End Property

    Public WriteOnly Property CalculateValue() As Boolean
        Set(ByVal Value As Boolean)
            If Value Then
                txtInput.Attributes.Add("onKeyUp", "calculateValue()")
            End If
        End Set
    End Property

    Public WriteOnly Property CopyValue() As Boolean
        Set(ByVal Value As Boolean)
            If Value Then
                txtInput.Attributes.Add("onKeyUp", "copyValue()")
            End If
        End Set
    End Property
    Public WriteOnly Property IsVisible() As Boolean
        Set(ByVal Value As Boolean)
            txtInput.Visible = Value
        End Set
    End Property

    Public WriteOnly Property IsVisibleHiddenInput() As Boolean
        Set(ByVal Value As Boolean)
            txtInputDummy.Visible = Value
        End Set
    End Property


    Public WriteOnly Property RangeType() As ValidationDataType
        Set(ByVal Value As ValidationDataType)
            rgvInput.Type = Value
        End Set
    End Property

    Public ReadOnly Property HiddenID() As String
        Get
            Return hdnInput.ClientID
        End Get
    End Property

    Public ReadOnly Property RangeValidatorID() As String
        Get
            Return rgvInput.ClientID
        End Get
    End Property

    Public ReadOnly Property PercentID() As String
        Get
            Return ltlPercent.ClientID
        End Get
    End Property


    Public ReadOnly Property TextID() As String
        Get
            Return txtInput.ClientID
        End Get
    End Property

    Public ReadOnly Property TextDummyID() As String
        Get
            Return txtInputDummy.ClientID
        End Get
    End Property

    Public Property txtInputObj() As TextBox
        Get
            Return txtInput
        End Get
        Set(ByVal value As TextBox)
            txtInput = value
        End Set
    End Property
    Public WriteOnly Property AutoPostBack() As Boolean
        Set(ByVal Value As Boolean)
            txtInput.AutoPostBack = Value
        End Set
    End Property

    Public Delegate Sub TextChangedDelegate()

    Dim TextChangedEvt As TextChangedDelegate
    'Public Event TextChangedEvent As TextChangedDelegate

    Public WriteOnly Property TextChangedEvent() As TextChangedDelegate
        Set(ByVal value As TextChangedDelegate)
            TextChangedEvt = value
        End Set
    End Property

    Public Event txtInputTextChanged As EventHandler
#End Region

#Region " Event Handlers "
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not txtInput.Attributes("onblur") Is Nothing Then
            txtInput.Attributes("onblur") &= "formatValueWithoutDecimal('" & txtInput.ClientID & "','" & hdnInput.ClientID & "','" & txtInputDummy.ClientID & "');"
        Else
            txtInput.Attributes.Add("onblur", "formatValueWithoutDecimal('" & txtInput.ClientID & "','" & hdnInput.ClientID & "','" & txtInputDummy.ClientID & "');")
        End If
        txtInput.Attributes.Add("onfocus", "clearFormatValue('" & txtInput.ClientID & "');")
        scriptJava.InnerHtml = "<script language=""javascript"">formatValueWithoutDecimal('" & txtInput.ClientID & "','" & hdnInput.ClientID & "','" & txtInputDummy.ClientID & "')</script>"
    End Sub

    Private Sub txtInput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInput.TextChanged
        If txtInput.AutoPostBack Then
            TextChangedEvt()
        End If
    End Sub

#End Region

    Public Sub AllowZeroValue()
        rfvInput.InitialValue = ""
    End Sub


End Class
