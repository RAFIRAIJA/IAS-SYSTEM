Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO

Partial Class xm_masteraddress
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New FunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String

    Dim mlFUNCTIONPARAMETER As String
    Dim mlKEY As String
    Dim mlKEYAUTONUMBER As Boolean
    Dim mlSPTYPE As String
    Dim mlRECORDSTATUS As String
    Dim mlROWAMOUNT As Int16
    Dim mlSQLRECORDSTATUS As String = ""
    Dim mlSEARCHRECORD As Boolean
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Master Address Book V2.04"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Master Address Book V2.04"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        'mlROWAMOUNT = System.Configuration.ConfigurationManager.AppSettings("mgROWDEFAULT")
        mlROWAMOUNT = "100"
        mlKEYAUTONUMBER = True

        mlFUNCTIONPARAMETER = Request("mpFP")

        Select Case mlFUNCTIONPARAMETER
            Case "INLOC"
                mlTITLE.Text = "Master Contact Book (Branch) V2.00"
                mlFUNCTIONPARAMETER = "INLOC"
            Case "S1"
                mlTITLE.Text = "Master Contact Book (Premier Stockist) V2.00"
                mlFUNCTIONPARAMETER = Request("mpFP")
            Case "S2"
                mlTITLE.Text = "Master Contact Book (Service Center) V2.00"
                mlFUNCTIONPARAMETER = Request("mpFP")
            Case "S3"
                mlTITLE.Text = "Master Contact Book (Mobile Stockist) V2.00"
                mlFUNCTIONPARAMETER = Request("mpFP")
        End Select

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "xm_masteraddress", "Master Address", "")
            mlSQLRECORDSTATUS = "WHERE  RecordStatus='New' AND AddressCode='" & mlFUNCTIONPARAMETER & "'"
            DisableCancel()
            RetrieveFieldsDetail()
        Else
        End If
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub btNewRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNewRecord.Click
        NewRecord()
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        mlDISTNAME.Text = mlOBJGS.ARGeneralLostFocus("CustID", mlDISTCODE.Text, "", "")
        mlRECRUITERNAME.Text = mlOBJGS.ARGeneralLostFocus("CustID", mlRECRUITERID.Text, "", "")

        mlOBJGS.mgMESSAGE = ValidateForm()
        If mlOBJGF.IsNone(mlOBJGS.mgMESSAGE) = False Then
            mlMESSAGE.Text = mlOBJGS.mgMESSAGE
            Exit Sub
        End If

        If pnFILL.Visible = True Then
            SaveRecord()
        End If
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        CancelOperation()
    End Sub

    Public Sub RetrieveFields()
        Try

        
            ClearFields()
            DisableCancel()

            mlSQL = "SELECT * FROM XM_ADDRESSBOOK WHERE AddressCode='" & mlFUNCTIONPARAMETER & "' AND AddressKey = '" & Trim(mlKEY) & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL)
            If mlREADER.HasRows Then
                mlREADER.Read()
                mlCODE.Text = mlREADER("AddressKey") & ""
                mlNAME.Text = mlREADER("Name") & ""
                mlDISTCODE.Text = mlREADER("CustID") & ""
                mlDISTNAME.Text = mlREADER("CustName") & ""
                mlRECRUITERID.Text = mlREADER("RecruiterID") & ""
                mlRECRUITERNAME.Text = mlREADER("RecruiterName") & ""
                mlCOMMISIONP.Text = mlREADER("CommPercentage") & ""
                mlPRICECODE.Text = mlREADER("PriceCode") & ""
                mlIC.Text = mlREADER("ICNo") & ""
                mlTAXID.Text = mlREADER("TaxID") & ""
                mlADDR.Text = mlREADER("Address") & ""
                mlCITY.Text = mlREADER("City") & ""
                mlSTATE.Items.Add(mlREADER("State") & "")
                mlCOUNTRY.Items.Add(mlREADER("Country") & "")
                mlZIP.Text = mlREADER("Zip") & ""
                mlPHONE1.Text = mlREADER("Phone1") & ""
                mlPHONE2.Text = mlREADER("Phone2") & ""
                mlFAX.Text = mlREADER("Fax") & ""
                mlMOBILE1.Text = mlREADER("Mobile1") & ""
                mlMOBILE2.Text = mlREADER("Mobile2") & ""
                mlEMAIL.Text = mlREADER("Email") & ""
                mlWEBSITE.Text = mlREADER("Website") & ""
                If IsDBNull(mlREADER("JoinDate")) = False Then mlJOINDATE.Text = mlOBJGF.ConvertDateIntltoIDN(mlREADER("JoinDate"), "/")
                mlCREDITLIMIT.Text = mlREADER("CreditLimit") & ""
                mlDEFCURR.Items.Add(mlREADER("DefaultCurrency") & "")
                mlDEFTERM.Items.Add(mlREADER("DefaultTerm") & "")
                mlDEFSALES.Text = mlREADER("DefaultSales") & ""
                mlDEFPIC.Text = mlREADER("DefaultPIC") & ""
                mlDEFDICSHR.Text = mlREADER("DefaultDiscHR") & ""
                mlDEFDICSDT.Text = mlREADER("DefaultDiscDT") & ""
            End If

        Catch ex As Exception
            mlMESSAGE.Text = "An error has occured on retrieve data" & Err.Description
        End Try
    End Sub

    Sub RetrieveFieldsDetail(Optional ByVal mpPARAM As String = "")
        If mlSQLRECORDSTATUS = "" Then mlSQLRECORDSTATUS = "WHERE  RecordStatus='New' AND AddressCode='" & mlFUNCTIONPARAMETER & "'"

        If mlSEARCHRECORD = False Then
            mlSQL2 = "SELECT TOP " & mlROWAMOUNT & " * FROM XM_ADDRESSBOOK " & mlSQLRECORDSTATUS & " ORDER BY AddressKey"
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2)
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
    End Sub

    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_AddressBook(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail()


    End Sub

    Sub NewRecord()
        EnableCancel()
        ClearFields()
        LoadComboData()

        mlMESSAGE.Text = ""
        mlOBJPJ.SetTextBox(False, mlCODE)


        'If mlKEYAUTONUMBER = True Then
        '    mlCODE.Text = "--AutoNumber--"
        '    mlCODE.Enabled = False
        '    mlCODE.BackColor = Color.Lavender
        'End If
    End Sub

    Sub CancelOperation()
        mlSPTYPE = ""
        mlSEARCHRECORD = False
        mlMESSAGE.Text = ""

        DisableCancel()
        RetrieveFieldsDetail()
    End Sub

    Sub SearchRecord()
        mlSEARCHRECORD = True


        If pnFILL.Visible = True Then
            If mlCODE.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " AddressKey LIKE '%" & mlCODE.Text & "%'"
            End If

            If mlNAME.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Name LIKE '%" & mlNAME.Text & "%'"
            End If

            If mlIC.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " ICNo LIKE '%" & mlIC.Text & "%'"
            End If

            If mlTAXID.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " TaxID LIKE '%" & mlTAXID.Text & "%'"
            End If

            If mlADDR.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Address LIKE '%" & mlADDR.Text & "%'"
            End If

            If mlCITY.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " City LIKE '%" & mlCITY.Text & "%'"
            End If

            If mlSTATE.Text <> "" Then
                If mlSTATE.Text <> "[Choose]" Then
                    mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " State LIKE '%" & mlSTATE.Text & "%'"
                End If
            End If

            If mlCOUNTRY.Text <> "" Then
                If mlCOUNTRY.Text <> "[Choose]" Then
                    mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Country LIKE '%" & mlCOUNTRY.Text & "%'"
                End If
            End If

            If mlZIP.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Zip LIKE '%" & mlZIP.Text & "%'"
            End If

            If mlPHONE1.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Phone1 LIKE '%" & mlPHONE1.Text & "%'"
            End If

            If mlPHONE2.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Phone2 LIKE '%" & mlPHONE2.Text & "%'"
            End If

            If mlFAX.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Fax LIKE '%" & mlFAX.Text & "%'"
            End If

            If mlMOBILE1.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Mobile1 LIKE '%" & mlMOBILE1.Text & "%'"
            End If

            If mlMOBILE2.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Mobile2 LIKE '%" & mlMOBILE2.Text & "%'"
            End If

            If mlEMAIL.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Email LIKE '%" & mlEMAIL.Text & "%'"
            End If

            If mlWEBSITE.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " Website LIKE '%" & mlWEBSITE.Text & "%'"
            End If


            'If mlJOINDATE.Text <> "" Then
            '    mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " JoinDate LIKE '%" & mlJOINDATE.Text & "%'"
            'End If


            If mlCREDITLIMIT.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " CreditLimit LIKE '%" & mlCREDITLIMIT.Text & "%'"
            End If


            If mlDEFCURR.Text <> "" Then
                If mlDEFCURR.Text <> "[Choose]" Then
                    mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " DefaultCurrency LIKE '%" & mlDEFCURR.Text & "%'"
                End If
            End If


            If mlDEFTERM.Text <> "" Then
                If mlDEFTERM.Text <> "[Choose]" Then
                    mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " DefaultTerm LIKE '%" & mlDEFTERM.Text & "%'"
                End If
            End If

            If mlDEFSALES.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " DefaultSales LIKE '%" & mlDEFSALES.Text & "%'"
            End If

            If mlDEFPIC.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " DefaultPIC LIKE '%" & mlDEFPIC.Text & "%'"
            End If

            If mlDEFDICSHR.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " DefaultDiscHR LIKE '%" & mlDEFDICSHR.Text & "%'"
            End If

            If mlDEFDICSDT.Text <> "" Then
                mlSQL2 = mlSQL2 & IIf(mlSQL2 = "", "", "AND") & " DefaultDiscDT LIKE '%" & mlDEFDICSDT.Text & "%'"
            End If

            If Not mlOBJGF.IsNone(mlSQL2) Then
                mlSQL2 = "SELECT * FROM XM_ADDRESSBOOK WHERE " & mlSQL2
                RetrieveFieldsDetail()
                pnFILL.Visible = False
                pnGRID.Visible = True
            End If
        Else
            EnableCancel()
            mlOBJPJ.SetTextBox(False, mlCODE)
            ClearFields()
            LoadComboData()
            pnFILL.Visible = True
        End If

        btNewRecord.Visible = False
        btSaveRecord.Visible = False
        mlCODE.Enabled = True
        mlMESSAGE.Text = ""
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
        LoadComboData()
    End Sub

    Sub SaveRecord()
        Dim mlKEY As String

        mlSPTYPE = "New"
        If mlSYSCODE.Value = "edit" Then
            mlSPTYPE = "Edit"
        End If

        Try
            mlKEY = mlCODE.Text
            Sql_AddressBook(mlSPTYPE, mlKEY)
        Catch ex As Exception

        End Try


        DisableCancel()
        RetrieveFieldsDetail()
        mlMESSAGE.Text = ""
        mlSYSCODE.Value = ""
    End Sub


    Private Sub EnableCancel()
        btSaveRecord.Visible = True
        btNewRecord.Visible = False

        pnFILL.Visible = True
        pnGRID.Visible = False


        mlOBJPJ.SetTextBox(True, mlCODE)
        mlOBJPJ.SetTextBox(False, mlNAME)
        mlDISTCODE.Enabled = True
        mlOBJPJ.SetTextBox(False, mlDISTCODE)
        mlOBJPJ.SetTextBox(False, mlRECRUITERID)
        btDISTCODE.Enabled = True
        btRECRUITER.Enabled = True
        mlDISTNAME.Enabled = True
        mlRECRUITERNAME.Enabled = True
        mlOBJPJ.SetTextBox(False, mlCOMMISIONP)
        mlPRICECODE.Enabled = True
        mlOBJPJ.SetTextBox(False, mlTAXID)
        mlOBJPJ.SetTextBox(False, mlADDR)
        mlOBJPJ.SetTextBox(False, mlCITY)
        mlSTATE.Enabled = True
        mlCOUNTRY.Enabled = True
        mlOBJPJ.SetTextBox(False, mlZIP)
        mlOBJPJ.SetTextBox(False, mlPHONE1)
        mlOBJPJ.SetTextBox(False, mlPHONE2)
        mlOBJPJ.SetTextBox(False, mlMOBILE1)
        mlOBJPJ.SetTextBox(False, mlMOBILE2)
        mlOBJPJ.SetTextBox(False, mlEMAIL)
        mlOBJPJ.SetTextBox(False, mlWEBSITE)
        mlOBJPJ.SetTextBox(False, mlJOINDATE)
        mlOBJPJ.SetTextBox(False, mlCREDITLIMIT)
        mlDEFCURR.Enabled = True
        mlDEFTERM.Enabled = True
        mlOBJPJ.SetTextBox(False, mlDEFSALES)
        mlOBJPJ.SetTextBox(False, mlDEFPIC)
        mlOBJPJ.SetTextBox(False, mlDEFDICSHR)
        mlOBJPJ.SetTextBox(False, mlDEFDICSDT)


    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False

        pnFILL.Visible = False
        pnGRID.Visible = True

        mlOBJPJ.SetTextBox(True, mlCODE)
        mlOBJPJ.SetTextBox(True, mlNAME)
        mlDISTCODE.Enabled = False
        mlOBJPJ.SetTextBox(True, mlDISTCODE)
        mlOBJPJ.SetTextBox(True, mlRECRUITERID)
        btDISTCODE.Enabled = False
        btRECRUITER.Enabled = False
        mlDISTNAME.Enabled = False
        mlRECRUITERNAME.Enabled = False
        mlOBJPJ.SetTextBox(True, mlCOMMISIONP)
        mlPRICECODE.Enabled = False
        mlOBJPJ.SetTextBox(True, mlTAXID)
        mlOBJPJ.SetTextBox(True, mlADDR)
        mlOBJPJ.SetTextBox(True, mlCITY)
        mlSTATE.Enabled = False
        mlCOUNTRY.Enabled = False
        mlOBJPJ.SetTextBox(True, mlZIP)
        mlOBJPJ.SetTextBox(True, mlPHONE1)
        mlOBJPJ.SetTextBox(True, mlPHONE2)
        mlOBJPJ.SetTextBox(True, mlMOBILE1)
        mlOBJPJ.SetTextBox(True, mlMOBILE2)
        mlOBJPJ.SetTextBox(True, mlEMAIL)
        mlOBJPJ.SetTextBox(True, mlWEBSITE)
        mlOBJPJ.SetTextBox(True, mlJOINDATE)
        mlOBJPJ.SetTextBox(True, mlCREDITLIMIT)
        mlDEFCURR.Enabled = False
        mlDEFTERM.Enabled = False
        mlOBJPJ.SetTextBox(True, mlDEFSALES)
        mlOBJPJ.SetTextBox(True, mlDEFPIC)
        mlOBJPJ.SetTextBox(True, mlDEFDICSHR)
        mlOBJPJ.SetTextBox(True, mlDEFDICSDT)

    End Sub

    Sub ClearFields()
        mlCODE.BackColor = Color.White

        mlCODE.Text = ""
        mlNAME.Text = ""
        mlDISTCODE.Text = ""
        mlDISTNAME.Text = ""
        mlUPLINE.Text = ""
        mlUPLINENAME.Text = ""
        mlRECRUITERID.Text = ""
        mlRECRUITERNAME.Text = ""
        mlCOMMISIONP.Text = ""
        mlPRICECODE.Items.Clear()
        mlIC.Text = ""
        mlTAXID.Text = ""
        mlADDR.Text = ""
        mlCITY.Text = ""
        mlSTATE.Items.Clear()
        mlCOUNTRY.Items.Clear()
        mlZIP.Text = ""
        mlPHONE1.Text = ""
        mlPHONE2.Text = ""
        mlFAX.Text = ""
        mlMOBILE1.Text = ""
        mlMOBILE2.Text = ""
        mlEMAIL.Text = ""
        mlWEBSITE.Text = ""
        mlJOINDATE.Text = mlCURRENTDATE
        mlCREDITLIMIT.Text = ""
        mlDEFCURR.Items.Clear()
        mlDEFTERM.Items.Clear()
        mlDEFSALES.Text = ""
        mlDEFSALESNAME.Text = ""
        mlDEFPIC.Text = ""
        mlDEFDICSHR.Text = ""
        mlDEFDICSDT.Text = ""
        mlPRICECODE.Items.Clear()
    End Sub

    Sub LoadComboData()
        mlSTATE.Items.Add("[Choose]")
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'PROPINSI'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mlSTATE.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        mlCOUNTRY.Items.Add("[Choose]")
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'NEGARA'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mlCOUNTRY.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        mlDEFCURR.Items.Add("[Choose]")
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'MATAUANG'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mlDEFCURR.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        mlDEFTERM.Items.Add("[Choose]")
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'TERM'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mlDEFTERM.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        mlPRICECODE.Items.Add("1")
        mlPRICECODE.Items.Add("2")
        mlPRICECODE.Items.Add("3")
        mlPRICECODE.Items.Add("4")
        mlPRICECODE.Items.Add("5")

    End Sub

    Protected Sub mlLINKNEW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlLINKNEW.Click
        mlSQLRECORDSTATUS = "WHERE  RecordStatus='New'"
        DisableCancel()
        RetrieveFieldsDetail()
    End Sub

    Protected Sub mlLINKDEL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlLINKDEL.Click
        mlSQLRECORDSTATUS = "WHERE  RecordStatus='Del'"
        DisableCancel()
        RetrieveFieldsDetail()
    End Sub

    Protected Sub btDISTCODE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btDISTCODE.Click
        mlDISTNAME.Text = mlOBJGS.ARGeneralLostFocus("CustID", mlDISTCODE.Text, "", "")
    End Sub

    Protected Sub btRECRUITER_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btRECRUITER.Click
        mlRECRUITERNAME.Text = mlOBJGS.ARGeneralLostFocus("CustID", mlRECRUITERID.Text, "", "")
    End Sub

    Protected Sub btUPLINEID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUPLINEID.Click
        mlUPLINENAME.Text = mlOBJGS.XMGeneralLostFocus("LOC", mlUPLINE.Text, "", "")
    End Sub

    Protected Sub btSALES_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSALES.Click
        mlDEFSALESNAME.Text = mlOBJGS.ADGeneralLostFocus("USER", mlDEFSALES.Text)
    End Sub



    Function ValidateForm() As String
        ValidateForm = ""

        If mlCODE.Text = "" Then
            ValidateForm = ValidateForm & " <br /> ID Lokasi tidak boleh kosong"
        End If

        If mlNAME.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Nama Lokasi tidak boleh kosong"
        End If

        'If mlDISTCODE.Text = "" Then
        '    ValidateForm = ValidateForm & " <br /> Owner ID tidak boleh kosong"
        'End If

        'If Len(mlDISTCODE.Text) <> "8" Then
        '    ValidateForm = ValidateForm & " <br /> Owner ID harus 8 digit angka"
        'End If

        'If mlDISTNAME.Text = "" Then
        '    ValidateForm = ValidateForm & " <br /> Nama Owner tidak boleh kosong"
        'End If

        'If mlRECRUITERID.Text <> "" And mlRECRUITERNAME.Text = "" Then
        '    ValidateForm = ValidateForm & " <br /> Nama Sponsor tidak boleh kosong"
        'End If

        'If mlSYSCODE.Value <> "edit" Then
        '    mlSQLTEMP = "SELECT CustID FROM XM_ADDRESSBOOK WHERE CustID = '" & mlDISTCODE.Text & "'"
        '    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
        '    If mlRSTEMP.HasRows Then
        '        mlRSTEMP.Read()
        '        ValidateForm = ValidateForm & " <br /> ID Owner ditemukan didalam database (duplikat) "
        '    End If
        'End If

        'If mlDISTCODE.Text = mlRECRUITERID.Text Then
        '    ValidateForm = ValidateForm & " <br /> ID Owner dan Sponsor tidak boleh sama"
        'End If

        'If mlCOMMISIONP.Text = "" Then
        '    ValidateForm = ValidateForm & " <br /> Komisi Stockist tidak boleh kosong"
        'End If

    End Function


    Function XM_AddressBook_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO XM_ADDRESSBOOK_LOG (AddressCode,AddressKey,Name,CustID,CustName,UplineID,UplineName,RecruiterID," & _
                    " RecruiterName,CommPercentage,PriceCode,ICNo,TaxID,Address,City,State,Country,Zip,Phone1,Phone2,Fax,Mobile1,Mobile2," & _
                    " Email,Website,JoinDate,CreditLimit,DefaultCurrency,DefaultTerm,DefaultSales,DefaultPIC,DefaultDiscHR," & _
                    " DefaultDiscDT,RecordStatus,Recuserid,Recdate,RecUDate)" & _
                    " SELECT AddressCode,AddressKey,Name,CustID,CustName,UplineID,UplineName,RecruiterID," & _
                    " RecruiterName,CommPercentage,PriceCode,ICNo,TaxID,Address,City,State,Country,Zip,Phone1,Phone2,Fax,Mobile1,Mobile2," & _
                    " Email,Website,JoinDate,CreditLimit,DefaultCurrency,DefaultTerm,DefaultSales,DefaultPIC,DefaultDiscHR," & _
                    " DefaultDiscDT,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & mlOBJGF.FormatDate(Now) & "'," & _
                    " getdate() FROM XM_ADDRESSBOOK WHERE AddressKey = '" & mpKEY & "' ;"
        Return mlLOG
    End Function


    Sub Sql_AddressBook(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean

        mlKEY = mpCODE
        mlNEWRECORD = False
        Try
            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & XM_AddressBook_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If mlKEY = "--AUTONUMBER--" Then
                        mlKEY = mlOBJGS.GetModuleCounter("MSBOOK_" & mlFUNCTIONPARAMETER, "000000")
                        mlCODE.Text = mlKEY
                    Else
                        mlKEY = Trim(mlCODE.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM XM_ADDRESSBOOK WHERE AddressKey = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM XM_ADDRESSBOOK WHERE AddressKey = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL)
            mlSQL = ""




            If mlNEWRECORD = True Then
                Dim mlSTATE2 As String = ""
                Dim mlCOUNTRY2 As String = ""
                Dim mlDEFCURR2 As String = ""
                Dim mlDEFTERM2 As String = ""

                mlSTATE2 = mlOBJGF.GetStringAtPosition(mlSTATE.Text, 0, "-")
                mlCOUNTRY2 = mlOBJGF.GetStringAtPosition(mlCOUNTRY.Text, 0, "-")
                mlDEFCURR2 = mlOBJGF.GetStringAtPosition(mlDEFCURR.Text, 0, "-")
                mlDEFTERM2 = mlOBJGF.GetStringAtPosition(mlDEFTERM.Text, 0, "-")
                If mlSTATE.Text = "[Choose]" Then mlSTATE2 = ""
                If mlCOUNTRY.Text = "[Choose]" Then mlCOUNTRY2 = ""
                If mlDEFCURR.Text = "[Choose]" Then mlDEFCURR2 = ""
                If mlDEFTERM.Text = "[Choose]" Then mlDEFTERM2 = ""

                mlSQL = ""
                mlSQL = mlSQL & "INSERT INTO XM_ADDRESSBOOK (AddressCode,AddressKey,Name," & _
                        " CustID,CustName,UplineID,UplineName,RecruiterID,RecruiterName," & _
                        " CommPercentage,PriceCode,ICNo,TaxID,Address,City,State,Country,Zip,Phone1,Phone2,Fax,Mobile1,Mobile2," & _
                        " Email,Website,JoinDate,CreditLimit,DefaultCurrency,DefaultTerm,DefaultSales,DefaultPIC,DefaultDiscHR," & _
                        " DefaultDiscDT,RecordStatus,Recuserid,Recdate)" & _
                        " VALUES (" & _
                        " '" & Trim(mlFUNCTIONPARAMETER) & "','" & Trim(mlKEY) & "', '" & Trim(mlNAME.Text) & "'," & _
                        " '" & Trim(mlDISTCODE.Text) & "', '" & Trim(mlDISTNAME.Text) & "'," & _
                        " '" & Trim(mlUPLINE.Text) & "', '" & Trim(mlUPLINENAME.Text) & "'," & _
                        " '" & Trim(mlRECRUITERID.Text) & "', '" & Trim(mlRECRUITERNAME.Text) & "'," & _
                        " '" & mlOBJGF.FormatNum(Trim(mlCOMMISIONP.Text)) & "','" & Trim(mlPRICECODE.Text) & "', '" & Trim(mlIC.Text) & "', " & _
                        " '" & Trim(mlTAXID.Text) & "', '" & Trim(mlADDR.Text) & "', '" & Trim(mlCITY.Text) & "', " & _
                        " '" & Trim(mlSTATE2) & "', '" & Trim(mlCOUNTRY2) & "', '" & Trim(mlZIP.Text) & "', " & _
                        " '" & Trim(mlPHONE1.Text) & "', '" & Trim(mlPHONE2.Text) & "', '" & Trim(mlFAX.Text) & "', " & _
                        " '" & Trim(mlMOBILE1.Text) & "', '" & Trim(mlMOBILE2.Text) & "', '" & Trim(mlEMAIL.Text) & "', " & _
                        " '" & Trim(mlWEBSITE.Text) & "', " & _
                        " '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlJOINDATE.Text, "/")) & "', " & _
                        " '" & Trim(mlCREDITLIMIT.Text) & "', " & _
                        " '" & Trim(mlDEFCURR2) & "', '" & Trim(mlDEFTERM2) & "', '" & Trim(mlDEFSALES.Text) & "', " & _
                        " '" & Trim(mlDEFPIC.Text) & "', '" & Trim(mlDEFDICSHR.Text) & "', '" & Trim(mlDEFDICSDT.Text) & "', " & _
                        " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "'" & _
                        " );"
                mlOBJGS.ExecuteQuery(mlSQL)
                mlSQL = ""
            End If



            Select Case mpSTATUS
                Case "New"
                    mlSQL = ""
                    mlSQL = mlSQL & XM_AddressBook_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL)
            mlSQL = ""


        Catch ex As Exception
            mlOBJGS.XMtoLog("XM", "XM", "AddressBook" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


    
End Class
