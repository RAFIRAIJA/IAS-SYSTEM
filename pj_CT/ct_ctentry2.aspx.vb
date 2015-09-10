Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.io

Partial Class ct_ctentry2
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New FunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlKEY As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Integer
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlPATHNORMAL As String
    Dim mlPATHNORMAL2 As String
    Dim mlPRTCODE As String
    Dim mlCOMPANYTABLENAME As String
    Dim mlCOMPANYID As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Contract Maintenance V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Contract Maintenance V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlPATHNORMAL = "../App_Data/ct_contractdoc/"
        mlPATHNORMAL2 = "App_Data/ct_contractdoc/"
        mlFUNCTIONPARAMETER = "contract"

        mlSEARCHCUST.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btSEARCHCUSTSUBMIT.UniqueID + "').click();return false;}} else {return true}; ")
        mlSEARCHSITECARD.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btSEARCHSITECARDSUBMIT.UniqueID + "').click();return false;}} else {return true}; ")
        mpSEARCHJOBNO.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btSEARCHJOBNOSUBMIT.UniqueID + "').click();return false;}} else {return true}; ")

        mlFUNCTIONPARAMETER = Trim(Request("mpFP"))
        If mlFUNCTIONPARAMETER = "" Then mlFUNCTIONPARAMETER = "1"

        Select Case mlFUNCTIONPARAMETER
            Case "1" 'Normal
                tr1.Visible = False
                tr2.Visible = False
                tr3.Visible = False
                tr4.Visible = False
                tr5.Visible = False
                lbUSER.Text = "Sales"
                lbPRICE.Text = "Price"

            Case "2" 'Existing Metro
                tr1.Visible = True
                tr2.Visible = True
                tr3.Visible = True
                tr4.Visible = True
                tr5.Visible = True
                lbUSER.Text = "Negotiator"
                lbPRICE.Text = "Approved Price"

            Case "3" 'Existing Branch
                tr1.Visible = True
                tr2.Visible = True
                tr3.Visible = True
                tr4.Visible = True
                tr5.Visible = True
                lbUSER.Text = "Negotiator"
                lbPRICE.Text = "Approved Price"

            Case "4" 'Letter
                lbCTDOCNO.Text = "Letter No"
                tr1.Visible = True
                tr2.Visible = True
                tr3.Visible = True
                tr4.Visible = True
                trSTARTDATE.Visible = False
                trENDDATE.Visible = False
                lbUSER.Text = "Negotiator"

            Case "5" 'No Continue Contract
                tr10.Visible = False
                tr9.Visible = False
                trSTARTDATE.Visible = False
                trENDDATE.Visible = False
                trENDDATE.Visible = False
                tr11.Visible = False
                tr12.Visible = False
                tr5.Visible = False
                tr13.Visible = False
                trU1.Visible = False
                trU2.Visible = False
                tr1.Visible = False
                tr2.Visible = False
                tr3.Visible = False
                tr4.Visible = False
                lbDESCRIPTION.Text = "Terminated Reason"
        End Select

        mlCOMPANYTABLENAME = "ISS Servisystem, PT$"
        mlCOMPANYID = "ISSN3"
        Select Case ddENTITY.Text
            Case "ISS"
                mlCOMPANYTABLENAME = "ISS Servisystem, PT$"
                mlCOMPANYID = "ISSN3"
            Case "IPM"
                mlCOMPANYTABLENAME = "ISS Parking Management$"
                mlCOMPANYID = "IPM3"
            Case "ICS"
                mlCOMPANYTABLENAME = "ISS CATERING SERVICES$"
                mlCOMPANYTABLENAME = "ISS Catering Service 5SP1$"
                mlCOMPANYID = "ICS5"
            Case "IFS"
                mlCOMPANYTABLENAME = "INTEGRATED FACILITY SERVICES$"
                mlCOMPANYID = "IFS3"
        End Select



        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
            pnSEARCHSITECARD.Visible = False
            pnSEARCHCUST.Visible = False
            pnSEARCHUSERID.Visible = False
            pnSEARCHBRANCH.Visible = False
            pnSEARCHCONTRACT.Visible = False
            pnSEARCHJOBNO.Visible = False
            DisableCancel()
            RetrieveFieldsDetail("")
        Else
        End If
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "View Request for " & e.CommandArgument
                mlSYSCODE.Value = "new"
                LoadComboData()
                RetrieveFields()
                pnFILL.Visible = True
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                LoadComboData()
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub mlDATAGRID_ItemBound(ByVal Source As Object, ByVal E As DataGridItemEventArgs) Handles mlDATAGRID.ItemDataBound
        If E.Item.ItemType = ListItemType.Item Or E.Item.ItemType = ListItemType.AlternatingItem Then
            mlI = 4
            Dim mlDOCDATE4 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            E.Item.Cells(mlI).Text = mlDOCDATE4.ToString("d")
            E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

            mlI = 6
            Dim mlDOCDATE6 As Date = Convert.ToDateTime(E.Item.Cells(mlI).Text)
            E.Item.Cells(mlI).Text = mlDOCDATE6.ToString("d")
            E.Item.Cells(mlI).HorizontalAlign = HorizontalAlign.Right

        End If
    End Sub

    Protected Sub mlDATAGRID2_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID2.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete File for  " & e.CommandArgument
                mlSYSCODE.Value = "deletefile"
                DeleteFile(lbFILEDOCNO.Value, mlKEY)
        End Select
    End Sub

    Protected Sub mlDATAGRID3_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID3.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete File for  " & e.CommandArgument
                mlSYSCODE.Value = "deletefile"

        End Select
    End Sub

    Protected Sub btNewRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNewRecord.Click
        NewRecord()
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        If pnFILL.Visible = True Then
            SaveRecord()
        End If
    End Sub

    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        CancelOperation()
    End Sub

    Sub CancelOperation()
        pnFILL.Visible = False
        DisableCancel()
        btSearchRecord.Visible = True
        btCancelOperation.Visible = True
    End Sub

    Sub Fill_CustomerInfo()
        Dim mlOLDCUST As String

        mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name, Branch,CustomerNo_ as CustID FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE LineNo_ LIKE  '%" & txSITECARD.Text & "%'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
        If mlRSTEMP.HasRows Then
            mlRSTEMP.Read()
            mlOLDCUST = lbCUSTDESC.Text

            txBRANCH.Text = mlRSTEMP("Branch")
            lbBRANCH.Text = mlOBJPJ.ISS_XMGeneralLostFocus("BRANCH_DESC2", Trim(txBRANCH.Text))
            txCUST.Text = mlRSTEMP("CustID")
            lbCUSTDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("CUST_DESC", Trim(txCUST.Text))
            If (mlOLDCUST <> "" And mlOLDCUST <> txCUST.Text) Then
                mlMESSAGE.Text = "Customer has been change from " & mlOLDCUST & " to" & lbCUSTDESC.Text
            End If
        End If

    End Sub
    ''

    Protected Sub btUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btUSERID.Click
        If mlOBJGF.IsNone(Trim(txUSERID.Text)) = False Then
            txUSERDESC.Text = mlOBJGS.ADGeneralLostFocus("USER", Trim(txUSERID.Text))
        End If
    End Sub

    Protected Sub btSEARCHUSERID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHUSERID.Click
        If pnSEARCHUSERID.Visible = False Then
            pnSEARCHUSERID.Visible = True
        Else
            pnSEARCHUSERID.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHUSERIDSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHUSERIDSUBMIT.Click
        If mlOBJGF.IsNone(mpSEARCHUSERID.Text) = False Then SearchUser(1, mpSEARCHUSERID.Text)
    End Sub

    Protected Sub mlDATAGRIDUSERID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDUSERID.ItemCommand
        Try
            txUSERID.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            txUSERDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDUSERID.DataSource = Nothing
            mlDATAGRIDUSERID.DataBind()
            pnSEARCHUSERID.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchUser(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Try
            Select Case mpTYPE
                Case "1"
                    mlSQLTEMP = "SELECT UserID, Name FROM AD_USERPROFILE WHERE Name LIKE  '%" & mpNAME & "%'"
                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "AD", "AD")
                    mlDATAGRIDUSERID.DataSource = mlRSTEMP
                    mlDATAGRIDUSERID.DataBind()
            End Select
        Catch ex As Exception
        End Try
    End Sub


    ''
    Protected Sub btSEARCHSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARD.Click
        If pnSEARCHSITECARD.Visible = False Then
            pnSEARCHSITECARD.Visible = True
        Else
            pnSEARCHSITECARD.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHSITECARDSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHSITECARDSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHSITECARD.Text) = False Then SearchSiteCard(1, mlSEARCHSITECARD.Text)
    End Sub

    Protected Sub mlDATAGRIDSITECARD_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDSITECARD.ItemCommand
        Try
            txSITECARD.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbSITEDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDSITECARD.DataSource = Nothing
            mlDATAGRIDSITECARD.DataBind()
            pnSEARCHSITECARD.Visible = False

            Fill_CustomerInfo()
            txADDR.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_ADDR_ALL", Trim(txSITECARD.Text))
            txADDR.Text = Replace(txADDR.Text, vbNewLine, "")
            txADDR.Text = Replace(txADDR.Text, " ", "")
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchSiteCard(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                'mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%' AND CustomerNo_= '" & txCUST.Text & "'"
                'mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%'"
                mlSQLTEMP = "SELECT LineNo_ as Field_ID,SearchName as Field_Name, Branch,CustomerNo_ as CustID FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlSEARCHSITECARD.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                mlDATAGRIDSITECARD.DataSource = mlRSTEMP
                mlDATAGRIDSITECARD.DataBind()
        End Select
    End Sub

    Protected Sub btSITECARD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSITECARD.Click
        lbSITEDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_DESC", Trim(txSITECARD.Text))
        txADDR.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_ADDR_ALL", Trim(txSITECARD.Text))
        Fill_CustomerInfo()
    End Sub

    ''
    Protected Sub btSEARCHCUST_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCUST.Click
        If pnSEARCHCUST.Visible = False Then
            pnSEARCHCUST.Visible = True
        Else
            pnSEARCHCUST.Visible = False
        End If
    End Sub


    Protected Sub btSEARCHCUSTSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCUSTSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHCUST.Text) = False Then SearchCUST(1, mlSEARCHCUST.Text)
    End Sub

    Protected Sub mlDATAGRIDCUST_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDCUST.ItemCommand
        Try
            txCUST.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbCUSTDESC.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDCUST.DataSource = Nothing
            mlDATAGRIDCUST.DataBind()
            pnSEARCHCUST.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchCUST(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT No_ as Field_ID,[Search Name] as Field_Name FROM [" & mlCOMPANYTABLENAME & "Customer] WHERE [Search Name] LIKE  '%" & mlSEARCHCUST.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                mlDATAGRIDCUST.DataSource = mlRSTEMP
                mlDATAGRIDCUST.DataBind()
        End Select
    End Sub

    Protected Sub btCUST_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCUST.Click
        lbCUSTDESC.Text = mlOBJPJ.ISS_XMGeneralLostFocus("CUST_DESC", Trim(txCUST.Text))
    End Sub

    ''
    Protected Sub btSEARCHBRANCH_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHBRANCH.Click
        If pnSEARCHBRANCH.Visible = False Then
            pnSEARCHBRANCH.Visible = True
        Else
            pnSEARCHBRANCH.Visible = False
        End If
    End Sub


    Protected Sub btSEARCHBRANCHSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHBRANCHSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHBRANCH.Text) = False Then SearchBRANCH(1, mlSEARCHBRANCH.Text)
    End Sub

    Protected Sub mlDATAGRIDBRANCH_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDBRANCH.ItemCommand
        Try
            txBRANCH.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbBRANCH.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDBRANCH.DataSource = Nothing
            mlDATAGRIDBRANCH.DataBind()
            pnSEARCHBRANCH.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchBRANCH(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT [Branch Location] as field_ID,[Name] as Field_Name FROM [" & mlCOMPANYTABLENAME & "Location] WHERE [Name] LIKE  '%" & mlSEARCHBRANCH.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                mlDATAGRIDBRANCH.DataSource = mlRSTEMP
                mlDATAGRIDBRANCH.DataBind()
        End Select
    End Sub


    Protected Sub btBRANCH_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btBRANCH.Click
        lbBRANCH.Text = mlOBJPJ.ISS_XMGeneralLostFocus("BRANCH_DESC2", Trim(txBRANCH.Text))
    End Sub


    ''
    Protected Sub btSearchContract_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCONTRACT.Click
        If pnSEARCHCONTRACT.Visible = False Then
            pnSEARCHCONTRACT.Visible = True
        Else
            pnSEARCHCONTRACT.Visible = False
        End If
    End Sub


    Protected Sub btSearchContractSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCONTRACTSUBMIT.Click
        If mlOBJGF.IsNone(mlSEARCHCONTRACT.Text) = False Then SearchContract(1, mlSEARCHCONTRACT.Text)
    End Sub

    Protected Sub btSearchContractSUBMIT2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHCONTRACTSUBMIT2.Click
        If mlOBJGF.IsNone(mlSEARCHCONTRACT2.Text) = False Then SearchContract(3, mlSEARCHCONTRACT2.Text)
    End Sub

    Protected Sub mlDATAGRIDCONTRACT_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDCONTRACT.ItemCommand
        Try
            txREFFNO.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            lbREFFDOCNO.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            mlDATAGRIDCONTRACT.DataSource = Nothing
            mlDATAGRIDCONTRACT.DataBind()
            pnSEARCHCONTRACT.Visible = False

            Fill_ReffValue(lbREFFDOCNO.Text)
        Catch ex As Exception
        End Try
    End Sub

    Sub SearchContract(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT  ContractNo as field_ID,DocNo as Field_Name FROM CT_CONTRACTHR WHERE [ContractNo] LIKE  '%" & mlSEARCHCONTRACT.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                mlDATAGRIDCONTRACT.DataSource = mlRSTEMP
                mlDATAGRIDCONTRACT.DataBind()
            Case "2"
                mlSQLTEMP = "SELECT  DocNo as Field_Name FROM CT_CONTRACTHR WHERE [ContractNo] =  '" & mpNAME & "' ORDER BY DocDate Desc"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                If mlRSTEMP.HasRows Then
                    mlRSTEMP.Read()
                    lbREFFDOCNO.Text = mlRSTEMP("Field_Name") & ""
                End If
                mlOBJGS.CloseFile(mlRSTEMP)

            Case "3"
                mlSQLTEMP = "SELECT  ContractNo as field_ID,DocNo as Field_Name,CustName FROM CT_CONTRACTHR WHERE [CustName] LIKE  '%" & mlSEARCHCONTRACT.Text & "%'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                mlDATAGRIDCONTRACT.DataSource = mlRSTEMP
                mlDATAGRIDCONTRACT.DataBind()

        End Select
    End Sub


    ''
    Protected Sub btJOBNO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btJOBNO.Click
        If mlOBJGF.IsNone(Trim(txJOBNO.Text)) = False Then
            SearchJobProductID("1", Trim(txJOBNO.Text))
        End If
    End Sub

    Protected Sub btSEARCHJOBNO_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHJOBNO.Click
        If pnSEARCHJOBNO.Visible = False Then
            pnSEARCHJOBNO.Visible = True
            SearchJobNo(1, Trim(txCUST.Text))
        Else
            pnSEARCHJOBNO.Visible = False
        End If
    End Sub

    Protected Sub btSEARCHJOBNOSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSEARCHJOBNOSUBMIT.Click
        'If mlOBJGF.IsNone(mpSEARCHJOBNO.Text) = False Then SearchJobNo(1, Trim(txCUST.Text))
    End Sub

    Protected Sub mlDATAGRIDJOBNO_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDJOBNO.ItemCommand
        Try
            txJOBNO.Text = CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mlDATAGRIDJOBNO.DataSource = Nothing
            mlDATAGRIDJOBNO.DataBind()
            pnSEARCHJOBNO.Visible = False
            SearchJobProductID("1", Trim(txJOBNO.Text))
        Catch ex As Exception
        End Try
    End Sub


    Sub SearchJobNo(ByVal mpTYPE As Byte, ByVal mpCUSTID As String)
        Try
            Select Case mpTYPE
                Case "1"
                    mlSQLTEMP = "SELECT DISTINCT " & _
                            " JB.[Job No_] as JobNo, " & _
                            " JB.[CustServiceSiteLineNo] as SiteCard_ID," & _
                            " JB.[Global Dimension 1 Code] as Prd_Offer_ID," & _
                            " JB.[Starting Date] as Start_Date,JB.[EndDate] as End_Date" & _
                            " FROM  [" & mlCOMPANYTABLENAME & "Job Budget Line] JB,  [" & mlCOMPANYTABLENAME & "Job] JO " & _
                            " WHERE " & _
                            " JB.[Job No_] = JO.[No_] " & _
                            " AND JO.[Bill-to Customer No_] = '" & mpCUSTID & "'" & _
                            " ORDER BY JobNo "

                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                    mlDATAGRIDJOBNO.DataSource = mlRSTEMP
                    mlDATAGRIDJOBNO.DataBind()
            End Select
        Catch ex As Exception
        End Try
    End Sub


    Sub SearchJobProductID(ByVal mpTYPE As Byte, ByVal mpID As String)
        Try
            ddPRODUCT2.Items.Clear()
            ddPRODUCT2.Items.Add("Pilih")
            Select Case mpTYPE
                Case "1"
                    mlSQLTEMP = "SELECT DISTINCT " & _
                            " JB.[Global Dimension 1 Code] as Prd_Offer_ID" & _
                            " FROM  [" & mlCOMPANYTABLENAME & "Job Budget Line] JB " & _
                            " WHERE " & _
                            " JB.[Job No_] = '" & mpID & "'"
                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
                    While mlRSTEMP.Read
                        ddPRODUCT2.Items.Add(Trim(mlRSTEMP("Prd_Offer_ID")))
                    End While
            End Select

        Catch ex As Exception

        End Try
    End Sub


    ''

    Protected Sub btPRODUCT2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btPRODUCT2.Click
        If ddPRODUCT2.Text <> "" Then
            lbITEMCART.Text = lbITEMCART.Text & IIf(mlOBJGF.IsNone(lbITEMCART.Text) = False, ",<br>", lbITEMCART.Text) & Trim(ddPRODUCT2.Text)
            lbITEMCART2.Value = lbITEMCART2.Value & IIf(mlOBJGF.IsNone(lbITEMCART2.Value) = False, "#", lbITEMCART2.Value) & Trim(ddPRODUCT2.Text)
        End If
    End Sub

    Protected Sub btCLEARCART_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCLEARCART.Click
        lbITEMCART.Text = ""
    End Sub

    Protected Sub btREFF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btREFF.Click
        SearchContract("2", Trim(txREFFNO.Text))
        Fill_ReffValue(Trim(lbREFFDOCNO.Text))
    End Sub

    Sub Fill_ReffValue(ByVal mpDOCNO As String)
        mlSQLTEMP = "SELECT  * FROM CT_CONTRACTHR WHERE DocNo =  '" & mpDOCNO & "'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        If mlRSTEMP.HasRows Then
            mlRSTEMP.Read()

            txDOCUMENTNO.Text = ""
            txDOCDATE.Text = mlCURRENTDATE

            txCUST.Text = mlRSTEMP("CustID") & ""
            lbCUSTDESC.Text = mlRSTEMP("CustName") & ""
            txSITECARD.Text = mlRSTEMP("SiteCardID") & ""
            lbSITEDESC.Text = mlRSTEMP("SiteCardName") & ""
            txADDR.Text = mlRSTEMP("Address") & ""
            txCITY.Text = mlRSTEMP("City") & ""

            Try
                ddSTATE.SelectedValue = mlRSTEMP("State") & ""
            Catch ex As Exception
                ddSTATE.Items.Add(mlRSTEMP("State") & "")
                ddSTATE.SelectedValue = mlRSTEMP("State")
            End Try

            Try
                ddCOUNTRY.SelectedValue = mlRSTEMP("Country") & ""
            Catch ex As Exception
                ddCOUNTRY.Items.Add(mlRSTEMP("Country") & "")
                ddCOUNTRY.SelectedValue = mlRSTEMP("Country")
            End Try


            txZIP.Text = mlRSTEMP("Zip") & ""
            txPHONE1.Text = mlRSTEMP("Phone1") & ""
            txPHONE1_PIC.Text = mlRSTEMP("PIC_ContactNo") & ""


            Try
                ddPRODUCT.SelectedValue = mlRSTEMP("ServiceType") & ""
            Catch ex As Exception
                ddPRODUCT.Items.Add(mlRSTEMP("ServiceType") & "")
                ddPRODUCT.SelectedValue = mlRSTEMP("ServiceType")
            End Try

            txPRICE2.Text = mlRSTEMP("ExistingPrice") & ""
            txMANPOWER2.Text = mlRSTEMP("ExistingEmployeeQty") & ""

        End If
        mlOBJGS.CloseFile(mlRSTEMP)
    End Sub

    Sub SearchRecord()
        Dim mlSQL As String

        If pnFILL.Visible = False Then
            ClearFields()
            EnableCancel()
            pnFILL.Visible = True
            mlOBJPJ.SetTextBox(False, txDOCUMENTNO)
            txDOCDATE.Text = ""
            txDOCDATE2.Text = ""
            btSaveRecord.Visible = False
            Exit Sub
        End If

        Try
            mlSQL = ""

            If txDOCUMENTNO.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.DocNo LIKE '%" & txDOCUMENTNO.Text & "%'"
            End If

            If txDOCDATE.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.DocDate = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE.Text, "/")) & "'  "
            End If

            If txCUST.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.CustID LIKE '%" & txCUST.Text & "%'"
            End If

            If txSITECARD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.SiteCardID LIKE '%" & txSITECARD.Text & "%'"
            End If

            If txADDR.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.Address LIKE '%" & txADDR.Text & "%'"
            End If

            If txCITY.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.City LIKE '%" & txCITY.Text & "%'"
            End If

            If txPHONE1.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.Phone1 LIKE '%" & txPHONE1.Text & "%'"
            End If

            If txPHONE1_PIC.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.PIC_ContactNo LIKE '%" & txPHONE1_PIC.Text & "%'"
            End If

            If txJOBNO.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.NavJobNo LIKE '%" & txJOBNO.Text & "%'"
            End If

            If ddPRODUCT2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.NavService LIKE '" & ddPRODUCT2.Text & "'"
            End If

            If txCTDOCNO.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.ContractNo LIKE '%" & txCTDOCNO.Text & "%'"
            End If

            If txDOCDATE.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.DocDate = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE.Text, "/")) & "' "
            End If

            If txDOCDATE2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.ContractDate = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'  "
            End If

            If txCRDOCDATE1.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.StartDate = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txCRDOCDATE1.Text, "/")) & "'  "
            End If

            If txCRDOCDATE2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " HR.EndDate = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txCRDOCDATE2.Text, "/")) & "'  "
            End If

            If mlOBJGF.IsNone(mlSQL) = False Then
                Try
                    mlSQL = "SELECT DocNo, DocDate as Create_Date,ContractNo as Contract_No,ContractDate as Contract_Date," & _
                    " CustID as Customer, CustName as Cust_Name, SiteCardID as SiteCard, SiteCardName,ServiceType, RecUserID as UserID" & _
                    " FROM CT_CONTRACTHR HR " & _
                    " WHERE RecordStatus='New' AND ParentCode='" & mlFUNCTIONPARAMETER & "' " & IIf(mlSQL = "", "", "AND") & mlSQL

                    RetrieveFieldsDetail(mlSQL)
                    pnFILL.Visible = False
                    pnFILL2.Visible = False
                    pnFILL3.Visible = False
                    btSearchRecord.Visible = False

                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Public Sub RetrieveFields()
        DisableCancel()

        Try
            mlSQL = "SELECT * FROM CT_CONTRACTHR WHERE DocNo = '" & Trim(mlKEY) & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlREADER.Read()

                Try
                    ddENTITY.SelectedValue = mlREADER("CompanyID") & ""
                Catch ex As Exception
                    ddENTITY.Items.Add(mlREADER("CompanyID") & "")
                    ddENTITY.SelectedValue = mlREADER("CompanyID")
                End Try

                txDOCUMENTNO.Text = mlREADER("DocNo") & ""
                txDOCDATE.Text = IIf(mlOBJGF.IsNone(mlREADER("DocDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DocDate"), "/") & "")
                txCUST.Text = mlREADER("CustID") & ""
                lbCUSTDESC.Text = mlREADER("CustName") & ""
                txSITECARD.Text = mlREADER("SiteCardID") & ""
                lbSITEDESC.Text = mlREADER("SiteCardName") & ""
                txADDR.Text = mlREADER("Address") & ""
                txCITY.Text = mlREADER("City") & ""

                Try
                    ddSTATE.SelectedValue = mlREADER("State") & ""
                Catch ex As Exception
                    ddSTATE.Items.Add(mlREADER("State") & "")
                    ddSTATE.SelectedValue = mlREADER("State")
                End Try

                Try
                    ddCOUNTRY.SelectedValue = mlREADER("Country") & ""
                Catch ex As Exception
                    ddCOUNTRY.Items.Add(mlREADER("Country") & "")
                    ddCOUNTRY.SelectedValue = mlREADER("Country")
                End Try


                txZIP.Text = mlREADER("Zip") & ""
                txPHONE1.Text = mlREADER("Phone1") & ""
                txPHONE1_PIC.Text = mlREADER("PIC_ContactNo") & ""

                txJOBNO.Text = mlREADER("NavJobNo") & ""
                lbITEMCART.Text = mlREADER("NavService") & ""
                lbITEMCART2.Value = mlREADER("NavService") & ""


                txCTDOCNO.Text = mlREADER("ContractNo") & ""
                txDOCDATE2.Text = IIf(mlOBJGF.IsNone(mlREADER("ContractDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("ContractDate"), "/") & "")
                txREFFNO.Text = mlREADER("ReffNo") & ""
                lbREFFDOCNO.Text = mlREADER("ReffDocNo") & ""
                txCRDOCDATE1.Text = IIf(mlOBJGF.IsNone(mlREADER("StartDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("StartDate"), "/") & "")
                txCRDOCDATE2.Text = IIf(mlOBJGF.IsNone(mlREADER("EndDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("EndDate"), "/") & "")

                Try
                    ddPRODUCT.SelectedValue = mlREADER("ServiceType") & ""
                Catch ex As Exception
                    ddPRODUCT.Items.Add(mlREADER("ServiceType") & "")
                    ddPRODUCT.SelectedValue = mlREADER("ServiceType")
                End Try

                txMANPOWER.Text = mlREADER("EmployeeQty") & ""
                txMANPOWER2.Text = mlREADER("ExistingEmployeeQty") & ""
                txPERCENTAGE.Text = mlREADER("IncrementPercent") & ""
                txPRICE2.Text = mlREADER("ExistingPrice") & ""
                txPRICE3.Text = mlREADER("ProposePrice") & ""
                txPRICE.Text = mlREADER("Price") & ""
                txUSERID.Text = mlREADER("Negotiator") & ""
                txBRANCH.Text = mlREADER("SC_Branch") & ""
                lbBRANCH.Text = mlREADER("SC_BranchName") & ""
                txDESCRIPTION.Text = mlREADER("Description") & ""
                lbFILEDOCNO.Value = mlREADER("FileDocNo") & ""


                txMANPOWER.Text = IIf(txMANPOWER.Text = "", 0, txMANPOWER.Text)
                txMANPOWER2.Text = IIf(txMANPOWER2.Text = "", 0, txMANPOWER2.Text)
                txPERCENTAGE.Text = IIf(txPERCENTAGE.Text = "", 0, txPERCENTAGE.Text)
                txPRICE2.Text = IIf(txPRICE2.Text = "", 0, txPRICE2.Text)
                txPRICE3.Text = IIf(txPRICE3.Text = "", 0, txPRICE3.Text)
                txPRICE.Text = IIf(txPRICE.Text = "", 0, txPRICE.Text)

                txMANPOWER.Text = Convert.ToDouble(txMANPOWER.Text).ToString("n")
                txMANPOWER2.Text = Convert.ToDouble(txMANPOWER2.Text).ToString("n")
                txPERCENTAGE.Text = Convert.ToDouble(txPERCENTAGE.Text).ToString("n")
                txPRICE2.Text = Convert.ToDouble(txPRICE2.Text).ToString("n")
                txPRICE3.Text = Convert.ToDouble(txPRICE3.Text).ToString("n")
                txPRICE.Text = Convert.ToDouble(txPRICE.Text).ToString("n")


                RetrieveFieldsDetail2("")
                RetrieveFieldsDetail3("")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try
            If mpSQL = "" Then
                mlSQL2 = "SELECT DocNo, DocDate as Create_Date,ContractNo as Contract_No,ContractDate as Contract_Date," & _
                    " CustID as Customer, CustName as Cust_Name, SiteCardID as SiteCard, SiteCardName,ServiceType, RecUserID as UserID" & _
                    " FROM CT_CONTRACTHR " & _
                    " WHERE RecordStatus='New' AND ParentCode='" & mlFUNCTIONPARAMETER & "' ORDER BY DocNo"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()

            mlOBJGS.CloseFile(mlREADER2)

        Catch ex As Exception
        End Try
    End Sub

    Sub RetrieveFieldsDetail2(ByVal mpSQL As String)
        Try

            If mpSQL = "" Then
                mlSQL2 = "SELECT DocNo,Linno as No,FileDesc as File_Name FROM XM_FILEDT" & _
                    " WHERE DocNo='" & lbFILEDOCNO.Value & "'"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID2.DataSource = mlREADER2
            mlDATAGRID2.DataBind()

            mlOBJGS.CloseFile(mlREADER2)
        Catch ex As Exception

        End Try

    End Sub

    Sub FillDetail(ByVal mpDOCNO As String)
        Dim mlSYSID As String
        Dim mlCODEID As String

        mlSYSID = "T1"
        mlSQL2 = "SELECT Linno as No_,ItemKey as Item FROM CT_CONTRACT_DT WHERE SysID='" & mlSYSID & "'" & _
                " AND ParentCode='" & mlFUNCTIONPARAMETER & "' AND DocNo='" & txDOCUMENTNO.Text & "'"
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        While mlREADER2.Read
            Dim mlDG As DataGridItem
            For Each mlDG In mlDATAGRID_TOOLS1.Items
                mlCODEID = mlDG.Cells("1").Text
                If mlREADER2("Item") = mlCODEID Then
                    Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)
                    mlBOX.Checked = True
                End If
            Next mlDG
        End While


        mlSYSID = "T2"
        mlSQL2 = "SELECT Linno as No_,ItemKey as Item FROM CT_CONTRACT_DT WHERE SysID='" & mlSYSID & "'" & _
                " AND ParentCode='" & mlFUNCTIONPARAMETER & "' AND DocNo='" & txDOCUMENTNO.Text & "'"
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
        While mlREADER2.Read
            Dim mlDG As DataGridItem
            For Each mlDG In mlDATAGRID_TOOLS2.Items
                mlCODEID = mlDG.Cells("1").Text
                If mlREADER2("Item") = mlCODEID Then
                    Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)
                    mlBOX.Checked = True
                End If
            Next mlDG
        End While
    End Sub

    Sub RetrieveFieldsDetail3(ByVal mpSQL As String)
        Dim mlSYSID As String
        Try

            mlSYSID = "T1"
            If mpSQL = "" Then
                mlSQL2 = "SELECT Linno as No_,ItemKey as Item FROM CT_CONTRACT_DT WHERE SysID='" & mlSYSID & "'" & _
                    " AND ParentCode='" & mlFUNCTIONPARAMETER & "' AND DocNo='" & txDOCUMENTNO.Text & "'"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID_TOOLS11.DataSource = mlREADER2
            mlDATAGRID_TOOLS11.DataBind()

            mlSYSID = "T2"
            If mpSQL = "" Then
                mlSQL2 = "SELECT Linno as No_,ItemKey as Item FROM CT_CONTRACT_DT WHERE SysID='" & mlSYSID & "'" & _
                    " AND ParentCode='" & mlFUNCTIONPARAMETER & "' AND DocNo='" & txDOCUMENTNO.Text & "'"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2, "PB", "ISSP3")
            mlDATAGRID_TOOLS21.DataSource = mlREADER2
            mlDATAGRID_TOOLS21.DataBind()


            mlOBJGS.CloseFile(mlREADER2)
        Catch ex As Exception

        End Try

    End Sub


    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFieldsDetail("")
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        LoadComboData()
        txDOCUMENTNO.Text = "--AUTONUMBER--"
        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        trUP0.Visible = False
    End Sub

    Sub EditRecord()
        mlOBJGS.mgNEW = False
        mlOBJGS.mgEDIT = True
        ClearFields()
        LoadComboData()
        RetrieveFields()
        EnableCancel()
        FillDetail(mlKEY)

    End Sub


    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True
        pnFILL2.Visible = True
        pnFILL3.Visible = True
        pnFILL4.Visible = True

        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        mlOBJPJ.SetTextBox(False, txDOCDATE)
        mlOBJPJ.SetTextBox(False, txCUST)
        mlOBJPJ.SetTextBox(False, txSITECARD)
        mlOBJPJ.SetTextBox(False, txADDR)
        mlOBJPJ.SetTextBox(False, txCITY)
        mlOBJPJ.SetTextBox(False, txZIP)
        mlOBJPJ.SetTextBox(False, txPHONE1)
        mlOBJPJ.SetTextBox(False, txPHONE1_PIC)
        mlOBJPJ.SetTextBox(False, txJOBNO)

        mlOBJPJ.SetTextBox(False, txCTDOCNO)
        mlOBJPJ.SetTextBox(False, txDOCDATE2)
        mlOBJPJ.SetTextBox(False, txREFFNO)
        mlOBJPJ.SetTextBox(False, txCRDOCDATE1)
        mlOBJPJ.SetTextBox(False, txCRDOCDATE2)
        mlOBJPJ.SetTextBox(False, txMANPOWER)
        mlOBJPJ.SetTextBox(False, txMANPOWER2)
        mlOBJPJ.SetTextBox(False, txPERCENTAGE)
        mlOBJPJ.SetTextBox(False, txPRICE3)
        mlOBJPJ.SetTextBox(False, txPRICE2)
        mlOBJPJ.SetTextBox(False, txPRICE)
        mlOBJPJ.SetTextBox(False, txUSERID)
        mlOBJPJ.SetTextBox(False, txBRANCH)
        mlOBJPJ.SetTextBox(False, txDESCRIPTION)

        btDOCDATE1.Visible = True
        btSEARCHCUST.Visible = True
        btCUST.Visible = True
        btSEARCHSITECARD.Visible = True
        btSITECARD.Visible = True
        btCRDOCDATE1.Visible = True
        btCRDOCDATE2.Visible = True
        btPRODUCT.Visible = True
        btSEARCHUSERID.Visible = True
        btUSERID.Visible = True
        btSEARCHBRANCH.Visible = True
        btBRANCH.Visible = True

        ddPRODUCT.Enabled = True
        ddSTATE.Enabled = True
        ddCOUNTRY.Enabled = True


        trTOOLS1.Visible = False
        trTOOLS2.Visible = False

    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False
        pnFILL2.Visible = False
        pnFILL3.Visible = False
        pnFILL4.Visible = False

        mlOBJPJ.SetTextBox(True, txDOCUMENTNO)
        mlOBJPJ.SetTextBox(True, txDOCDATE)
        mlOBJPJ.SetTextBox(True, txCUST)
        mlOBJPJ.SetTextBox(True, txSITECARD)
        mlOBJPJ.SetTextBox(True, txADDR)
        mlOBJPJ.SetTextBox(True, txCITY)
        mlOBJPJ.SetTextBox(True, txZIP)
        mlOBJPJ.SetTextBox(True, txPHONE1)
        mlOBJPJ.SetTextBox(True, txPHONE1_PIC)
        mlOBJPJ.SetTextBox(True, txJOBNO)

        mlOBJPJ.SetTextBox(True, txCTDOCNO)
        mlOBJPJ.SetTextBox(True, txDOCDATE2)
        mlOBJPJ.SetTextBox(True, txREFFNO)
        mlOBJPJ.SetTextBox(True, txCRDOCDATE1)
        mlOBJPJ.SetTextBox(True, txCRDOCDATE2)
        mlOBJPJ.SetTextBox(True, txMANPOWER)
        mlOBJPJ.SetTextBox(True, txMANPOWER2)
        mlOBJPJ.SetTextBox(True, txPERCENTAGE)
        mlOBJPJ.SetTextBox(True, txPRICE3)
        mlOBJPJ.SetTextBox(True, txPRICE2)
        mlOBJPJ.SetTextBox(True, txPRICE)
        mlOBJPJ.SetTextBox(True, txUSERID)
        mlOBJPJ.SetTextBox(True, txBRANCH)
        mlOBJPJ.SetTextBox(True, txDESCRIPTION)

        btDOCDATE1.Visible = False
        btSEARCHCUST.Visible = False
        btCUST.Visible = False
        btSEARCHSITECARD.Visible = False
        btSITECARD.Visible = False
        btCRDOCDATE1.Visible = False
        btCRDOCDATE2.Visible = False
        btPRODUCT.Visible = False
        btSEARCHUSERID.Visible = False
        btUSERID.Visible = False
        btSEARCHBRANCH.Visible = False
        btBRANCH.Visible = False

        ddPRODUCT.Enabled = False
        ddSTATE.Enabled = False
        ddCOUNTRY.Enabled = False

        trUP0.Visible = True
        trTOOLS1.Visible = True
        trTOOLS2.Visible = True
    End Sub

    Sub ClearFields()
        txDOCUMENTNO.Text = ""
        txDOCDATE.Text = mlCURRENTDATE
        txCUST.Text = ""
        lbCUSTDESC.Text = ""
        txSITECARD.Text = ""
        lbSITEDESC.Text = ""
        txADDR.Text = ""
        txCITY.Text = ""
        txZIP.Text = ""
        txPHONE1.Text = ""
        txPHONE1_PIC.Text = ""
        txJOBNO.Text = ""
        lbITEMCART.Text = ""
        lbITEMCART2.Value = ""

        txCTDOCNO.Text = ""
        txDOCDATE2.Text = mlCURRENTDATE
        txREFFNO.Text = ""
        lbREFFDOCNO.Text = ""
        txCRDOCDATE1.Text = ""
        txCRDOCDATE2.Text = ""
        lbPRODUCT.Text = ""
        txMANPOWER.Text = "0"
        txMANPOWER2.Text = "0"
        txPERCENTAGE.Text = "0"
        txPRICE2.Text = "0"
        txPRICE3.Text = "0"
        txPRICE.Text = "0"
        txUSERID.Text = ""
        txBRANCH.Text = ""
        lbBRANCH.Text = ""
        txDESCRIPTION.Text = ""
        txUSERDESC.Text = ""

        lbFILEDOCNO.Value = ""
        txFILEUPLOAD1_N.Text = ""
        lnLINK1.Text = ""
        txFILEUPLOAD2_N.Text = ""
        lnLINK2.Text = ""
        txFILEUPLOAD3_N.Text = ""
        lnLINK3.Text = ""
        txFILEUPLOAD4_N.Text = ""
        lnLINK4.Text = ""
        txFILEUPLOAD5_N.Text = ""
        lnLINK5.Text = ""

        trUP0.Visible = True
        mlDATAGRID2.DataSource = Nothing
        mlDATAGRID2.DataBind()

    End Sub


    Sub AddRow(ByVal mpROWSNO As Integer)
        Dim mlDATATABLE As DataTable
        Dim mlDATAROW As DataRow


        mlDATATABLE = New DataTable
        mlDATATABLE.Columns.Add("Linno", GetType(Integer))

        mlI = 1
        For mlI = 1 To mpROWSNO
            mlDATAROW = mlDATATABLE.NewRow
            mlDATAROW("Linno") = mlI
            mlDATATABLE.Rows.Add(mlDATAROW)
        Next
        mlDATAGRID3.DataSource = mlDATATABLE
        mlDATAGRID3.DataBind()
    End Sub


    Sub LoadComboData()
        ddPRODUCT.Items.Clear()
        ddPRODUCT.Items.Add("Pilih")
        mlSQLTEMP = "SELECT * FROM  [dbo].[" & mlCOMPANYTABLENAME & "Dimension Value] WHERE [DIMENSION CODE]='PRD-OFF' ORDER BY Code"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
        While mlRSTEMP.Read
            ddPRODUCT.Items.Add(Trim(mlRSTEMP("Code") & "@" & mlRSTEMP("Name")))
        End While


        ddSTATE.Items.Add("Pilih")
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'PROPINSI'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            ddSTATE.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        ddCOUNTRY.Items.Add("IDN-Indonesia")
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'NEGARA'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            ddCOUNTRY.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        ddENTITY.Items.Clear()
        ddENTITY.Items.Add("Pilih")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddENTITY.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While


        mlSQLTEMP = "SELECT LinCode as field_ID,Description as Field_Name FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'Contract_Tools1'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
        mlDATAGRID_TOOLS1.DataSource = mlRSTEMP
        mlDATAGRID_TOOLS1.DataBind()

        mlSQLTEMP = "SELECT LinCode as field_ID,Description as Field_Name FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'Contract_Tools2'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
        mlDATAGRID_TOOLS2.DataSource = mlRSTEMP
        mlDATAGRID_TOOLS2.DataBind()

    End Sub



    Sub SaveRecord()
        Dim mlSQLHR As String = ""
        Dim mlSQLDT As String = ""

        mlOBJGS.mgMESSAGE = ValidateForm()
        If mlOBJGF.IsNone(mlOBJGS.mgMESSAGE) = False Then
            mlMESSAGE.Text = mlOBJGS.mgMESSAGE
            Exit Sub
        End If

        mlSPTYPE = "New"
        If mlSYSCODE.Value = "edit" Then
            mlSPTYPE = "Edit"
        End If

        Try
            mlKEY = Trim(txDOCUMENTNO.Text)
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
        RetrieveFieldsDetail("")

        mlMESSAGE.Text = mlMESSAGE.Text & "<br>" & " Data Save Successfull with Document No " & mlKEY
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        btCUST_Click(Nothing, Nothing)
        btSITECARD_Click(Nothing, Nothing)
        btBRANCH_Click(Nothing, Nothing)
        btUSERID_Click(Nothing, Nothing)


        If txCUST.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Cust ID not allowed empty"
        End If

        If lbCUSTDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Cust Name not allowed empty"
        End If

        If txSITECARD.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Site Card ID not allowed empty"
        End If

        If lbSITEDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Site Card Desc not allowed empty"
        End If

        If txCTDOCNO.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Contract No not allowed empty"
        End If

        If mlOBJGS.mgNEW = True Then
            mlSQLTEMP = "SELECT DocNo, DocDate, ContractNo FROM CT_CONTRACTHR WHERE ContractNo = '" & Trim(txCTDOCNO.Text) & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                If IsDBNull(mlRSTEMP("ContractNo")) = False Then
                    ValidateForm = ValidateForm & " <br /> Contract No are found on database, created on " & mlRSTEMP("DocDate") & " (not allow duplicate)"
                End If
            End If
        End If


        If ddPRODUCT.Text = "Pilih" Then
            ValidateForm = ValidateForm & " <br /> Product Offering not allowed empty"
        End If

        If txDOCDATE2.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Contract Date not allowed empty"
        End If


        If mlFUNCTIONPARAMETER <> "4" Then
            If txCRDOCDATE1.Text = "" Then
                ValidateForm = ValidateForm & " <br /> Start Date not allowed empty"
            End If

            If txCRDOCDATE2.Text = "" Then
                ValidateForm = ValidateForm & " <br /> End Date not allowed empty"
            End If
        End If

        'If IsDate(Trim(txDOCDATE2.Text)) = False Then
        '    ValidateForm = ValidateForm & " <br /> Contract Date has wrong Date Format"
        'End If

        'If IsDate(Trim(txCRDOCDATE1.Text)) = False Then
        '    ValidateForm = ValidateForm & " <br /> Start Date has wrong Date Format"
        'End If

        'If IsDate(Trim(txCRDOCDATE2.Text)) = False Then
        '    ValidateForm = ValidateForm & " <br /> End Date has wrong Date Format"
        'End If


        If mlSYSCODE.Value = "new" Then
            If (FileUpload1.HasFile = False And FileUpload2.HasFile = False And FileUpload3.HasFile = False And FileUpload4.HasFile = False And FileUpload5.HasFile = False) Then
                ValidateForm = ValidateForm & " <br /> Files are not found"
            End If


            If FileUpload1.HasFile = True Then Session("fileupload1") = FileUpload1
            If Session("fileupload1") IsNot Nothing And FileUpload1.HasFile = False Then FileUpload1 = Session("fileupload1")
            If FileUpload2.HasFile = True Then Session("fileupload2") = FileUpload2
            If Session("fileupload2") IsNot Nothing And FileUpload2.HasFile = False Then FileUpload1 = Session("fileupload2")
            If FileUpload3.HasFile = True Then Session("fileupload3") = FileUpload3
            If Session("fileupload3") IsNot Nothing And FileUpload3.HasFile = False Then FileUpload3 = Session("fileupload3")
            If FileUpload4.HasFile = True Then Session("fileupload4") = FileUpload4
            If Session("fileupload4") IsNot Nothing And FileUpload4.HasFile = False Then FileUpload4 = Session("fileupload4")
            If FileUpload5.HasFile = True Then Session("fileupload5") = FileUpload5
            If Session("fileupload5") IsNot Nothing And FileUpload5.HasFile = False Then FileUpload5 = Session("fileupload5")
        End If

        If FileUpload1.HasFile = True Then
            If txFILEUPLOAD1_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 1"
            End If
        End If

        If FileUpload2.HasFile = True Then
            If txFILEUPLOAD2_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 2"
            End If
        End If

        If FileUpload3.HasFile = True Then
            If txFILEUPLOAD3_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 3"
            End If
        End If

        If FileUpload4.HasFile = True Then
            If txFILEUPLOAD4_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 4"
            End If
        End If

        If FileUpload5.HasFile = True Then
            If txFILEUPLOAD5_N.Text = "" Then
                ValidateForm = ValidateForm & " <br /> File Description empty on File 5"
            End If
        End If
    End Function



    Function Sql_File(ByVal mpSTATUS As String, ByVal mpNEWRECORD As Boolean, ByVal mpFS_FUNCTIONPARAMETER As String, ByVal mpFS_DOCNO As String, ByVal mpFS_GROUPID As String, ByVal mpFS_DESCRIPTION As String)
        Dim mlOBJFS As New IASClass.ucmFileSystem

        Dim mlFOLDERPATH As String
        Dim mlFOLDERPATH2 As String
        Dim mlPATHDESTDEFAULT As String
        Dim mlFILENAME As String
        Dim mlFILEPATH As String
        Dim mlFOLDERNAMERND As String
        Dim mlLOOP As Boolean

        Dim mlFILENAME1 As String
        Dim mlFILEPATH1 As String
        Dim mlFILEDESC1 As String
        Dim mlFILEUSERID1 As String
        Dim mlFILEPASSWORD1 As String

        Dim mlPROCESSID As String
        Dim mlPROCESS_SUBJECT As String
        Dim mlPROCESS_DESC As String
        Dim mlLINE As String
        Dim mlITEMKEY2 As String
        Dim mlITEMDESC2 As String
        Dim mlFIRST As Boolean
        Dim mlUSERID2 As String
        Dim mlFS_DOCNO As String
        Dim mlFS_STATUS As String
        Dim mlRSFILE As OleDb.OleDbDataReader
        Dim mlSQLFILE As String
        Dim mlSQLFILEFILE2 As String

        mlFS_DOCNO = mpFS_DOCNO
        mlMESSAGE.Text = ""

        Select Case mpSTATUS
            Case "Edit", "Delete"
                mlSQLFILE = ""
                mlSQLFILE = mlSQLFILE & mlOBJPJ.XM_FILEHR_ToLog(mlFS_DOCNO, mpSTATUS, Session("mgUSERID"))
        End Select

        Select Case mpSTATUS
            Case "New"
                mpNEWRECORD = True

            Case "Edit"
                mlFS_STATUS = "Edit"
                mpNEWRECORD = True
                'mlSQLFILE = mlSQLFILE & " DELETE FROM XM_FILEHR WHERE DocNo = '" & mlFS_DOCNO & "';"

            Case "Delete"
                mlFS_STATUS = "Delete"
                mlSQLFILE = mlSQLFILE & " UPDATE XM_FILEHR SET RecordStatus='Delete' WHERE DocNo = '" & mlFS_DOCNO & "';"
        End Select
        If mlOBJGF.IsNone(mlSQLFILE) = False Then mlOBJGS.ExecuteQuery(mlSQLFILE, "PB", "ISSP3")
        mlSQLFILE = ""

        mlUSERID2 = ""
        mlFILENAME1 = ""
        mlFILEPATH1 = ""
        mlFILEDESC1 = ""
        mlFILEUSERID1 = ""
        mlFILEPASSWORD1 = ""

        mlLOOP = True
        mlFOLDERPATH = ""
        mlPATHDESTDEFAULT = ""
        mlFOLDERNAMERND = ""
        mlI = 0

        mlSQLFILEFILE2 = "SELECT Linno FROM XM_FILEDT WHERE DocNo = '" & mlFS_DOCNO & "' ORDER BY Linno Desc"
        mlRSFILE = mlOBJGS.DbRecordset(mlSQLFILEFILE2, "PB", "ISSP3")
        If mlRSFILE.HasRows Then
            mlRSFILE.Read()
            If IsDBNull(mlRSFILE("Linno")) = False Then
                mlI = CInt(mlRSFILE("Linno"))
            End If
        End If
        mlOBJGS.CloseFile(mlRSFILE)


        Do While mlLOOP = True
            mlFOLDERNAMERND = mlOBJGF.GetRandomPasswordUsingGUID(8)
            mlFOLDERNAMERND = "ct_" & mlFOLDERNAMERND
            mlFOLDERPATH = mlPATHNORMAL & mlFOLDERNAMERND
            mlFOLDERPATH = Server.MapPath(mlFOLDERPATH)
            If mlOBJFS.Folder_Exists(mlFOLDERPATH) = True Then
                mlLOOP = True
            Else
                mlOBJFS.Folder_New(mlFOLDERPATH)
                mlLOOP = False
            End If
        Loop


        Try
            If FileUpload1.HasFile Then
                mlFILENAME = FileUpload1.FileName
                mlFILENAME = mlFS_DOCNO & "_" & mlFILENAME
                mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                FileUpload1.SaveAs(mlFILEPATH)

                mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                mlFILENAME1 = mlFILENAME
                mlFILEDESC1 = Trim(txFILEUPLOAD1_N.Text)
                mlFILEUSERID1 = ""

                mlI = mlI + 1
                mlSQLFILE = mlSQLFILE & " INSERT INTO XM_FILEDT " & _
                            " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                            " '" & mlFS_DOCNO & "','" & mlI & "'," & _
                            " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                            " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"

            End If
        Catch ex As Exception
        End Try

        Try
            If FileUpload2.HasFile Then
                mlFILENAME = FileUpload2.FileName
                mlFILENAME = mlFS_DOCNO & "_" & mlFILENAME
                mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                FileUpload2.SaveAs(mlFILEPATH)

                mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                mlFILENAME1 = mlFILENAME
                mlFILEDESC1 = Trim(txFILEUPLOAD2_N.Text)
                mlFILEUSERID1 = ""

                mlI = mlI + 1
                mlSQLFILE = mlSQLFILE & " INSERT INTO XM_FILEDT " & _
                            " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                            " '" & mlFS_DOCNO & "','" & mlI & "'," & _
                            " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                            " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"
            End If
        Catch ex As Exception
        End Try

        Try
            If FileUpload3.HasFile Then
                mlFILENAME = FileUpload3.FileName
                mlFILENAME = mlFS_DOCNO & "_" & mlFILENAME
                mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                FileUpload3.SaveAs(mlFILEPATH)


                mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                mlFILENAME1 = mlFILENAME
                mlFILEDESC1 = Trim(txFILEUPLOAD3_N.Text)
                mlFILEUSERID1 = ""

                mlI = mlI + 1
                mlSQLFILE = mlSQLFILE & " INSERT INTO XM_FILEDT " & _
                            " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                            " '" & mlFS_DOCNO & "','" & mlI & "'," & _
                            " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                            " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"

            End If
        Catch ex As Exception
        End Try

        Try
            If FileUpload4.HasFile Then
                mlFILENAME = FileUpload4.FileName
                mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                FileUpload4.SaveAs(mlFILEPATH)


                mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                mlFILENAME1 = mlFILENAME
                mlFILEDESC1 = Trim(txFILEUPLOAD4_N.Text)
                mlFILEUSERID1 = ""

                mlI = mlI + 1
                mlSQLFILE = mlSQLFILE & " INSERT INTO XM_FILEDT " & _
                            " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                            " '" & mlFS_DOCNO & "','" & mlI & "'," & _
                            " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                            " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"
            End If
        Catch ex As Exception
        End Try

        Try
            If FileUpload5.HasFile Then
                mlFILENAME = FileUpload5.FileName
                mlFILENAME = mlFS_DOCNO & "_" & mlFILENAME
                mlFILEPATH = mlFOLDERPATH & "/" & mlFILENAME
                FileUpload5.SaveAs(mlFILEPATH)


                mlFILEPATH1 = mlPATHNORMAL2 & mlFOLDERNAMERND & "/" & mlFILENAME
                mlFILENAME1 = mlFILENAME
                mlFILEDESC1 = Trim(txFILEUPLOAD5_N.Text)
                mlFILEUSERID1 = ""

                mlI = mlI + 1
                mlSQLFILE = mlSQLFILE & " INSERT INTO XM_FILEDT " & _
                            " (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword) VALUES ( " & _
                            " '" & mlFS_DOCNO & "','" & mlI & "'," & _
                            " '" & mlFILEPATH1 & "','" & mlFILENAME1 & "','" & mlFILEDESC1 & "'," & _
                            " '" & mlFILEUSERID1 & "','" & mlFILEPASSWORD1 & "');"
            End If
        Catch ex As Exception
        End Try


        'If mpNEWRECORD = True Then
        'End If

        Select Case mpSTATUS
            Case "New"
                mlSQLFILE = mlSQLFILE & " INSERT INTO XM_FILEHR " & _
                       " (ParentCode,SysID,DocNo,DocDate,GroupID,Description,RandomStr," & _
                       " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                       " '" & mpFS_FUNCTIONPARAMETER & "','','" & mlFS_DOCNO & "','" & mlOBJGF.FormatDate(Now) & "'," & _
                       " '" & Trim(mpFS_GROUPID) & "', '" & Trim(mpFS_DESCRIPTION) & "','" & mlFOLDERNAMERND & "'," & _
                       " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"


                mlPROCESSID = "Contract"
                mlPROCESS_SUBJECT = "Create Contract No: " & Trim(txREFFNO.Text)
                mlPROCESS_DESC = ""
                mlI = 1

                mlSQLFILE = mlSQLFILE & " INSERT INTO XM_FILEDTU " & _
                    " (DocNo,Linno,Type,UserID,Name,TaskID,Description," & _
                    " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                    " '" & mlFS_DOCNO & "','" & mlI & "','1'," & _
                    " '" & Session("mgUSERID") & "','" & Session("mgNAME") & "', '" & mlPROCESSID & " ','" & mlPROCESS_SUBJECT & "'," & _
                    " 'New','" & Session("mgUSERID") & "','" & Now & "');"


                mlSQLFILE = mlSQLFILE & mlOBJPJ.XM_FILEHR_ToLog(mlFS_DOCNO, mpSTATUS, Session("mgUSERID"))
        End Select
        mlOBJGS.ExecuteQuery(mlSQLFILE, "PB", "ISSP3")
        mlSQLFILE = ""

        ClearSession()
    End Function


    Sub ClearSession()
        Session.Remove("fileupload1")
        Session.Remove("fileupload2")
        Session.Remove("fileupload3")
        Session.Remove("fileupload4")
        Session.Remove("fileupload5")
    End Sub

    Sub DeleteFile(ByVal mpDOCNO As String, ByVal mpLINNO As Integer)
        Try
            Dim mlPATH As String

            mlPATH = ""
            mlSQLTEMP = "SELECT * FROM XM_FILEDT WHERE DocNo = '" & mpDOCNO & "' AND Linno = '" & mpLINNO & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                mlPATH = "../" & mlRSTEMP("FilePath") & ""
            End If

            If mlOBJGF.IsNone(mlPATH) = False Then
                mlPATH = Server.MapPath(mlPATH)
                mlOBJFS.File_Delete(mlPATH)

                mlSQLTEMP = "DELETE FROM XM_FILEDT WHERE DocNo = '" & mpDOCNO & "' AND Linno = '" & mpLINNO & "'"
                mlOBJGS.ExecuteQuery(mlSQLTEMP, "PB", "ISSP3")
            End If

            RetrieveFieldsDetail2("")

        Catch ex As Exception

        End Try
    End Sub

    Function Sql_2(ByVal mpFUNCTIONPARAM As String, ByVal mpSYSID As String, ByVal mpDOCNO As String) As String
        Dim mlLOOP As Boolean
        Dim mlLINE As String
        Dim mlITEMKEY2 As String
        Dim mlITEMDESC2 As String
        Dim mlFIRST As Boolean
        Dim mlSQL2 As String

        Sql_2 = ""

        Try


            mlSQL2 = ""
            If lbITEMCART2.Value = "" Then
                Return ""
            End If

            mlI = 0

            mlFIRST = True
            mlLOOP = True
            Do While mlLOOP
                mlLINE = mlOBJGF.GetStringAtPosition(lbITEMCART2.Value, mlI, "#")
                If mlLINE = "" Then
                    mlLOOP = False
                Else
                    mlITEMKEY2 = mlLINE
                    mlITEMDESC2 = ""

                    mlSQL2 = mlSQL2 & "INSERT INTO [CT_CONTRACT_JOB] (ParentCode,SysID,DocNo,DocDate,Linno,ItemKey,Description," & _
                            " RecName,RecordStatus,RecUserID,RecDate)" & _
                            " VALUES ('" & Trim(mpFUNCTIONPARAM) & "', '" & Trim(mpSYSID) & " ', '" & Trim(mpDOCNO) & "','" & mlOBJGF.FormatDate(Now) & "'," & _
                            " '" & mlI & "','" & mlITEMKEY2 & "','" & mlITEMDESC2 & "', '" & Session("mgNAME") & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
                    mlI = mlI + 1

                End If
            Loop

            Return mlSQL2

        Catch ex As Exception
        End Try

    End Function


    Function Sql_3(ByVal mpFUNCTIONPARAM As String, ByVal mpSYSID As String, ByVal mpDOCNO As String) As String
        Dim mlITEMKEY2 As String
        Dim mlITEMDESC2 As String
        Dim mlSQL2 As String
        Dim mlPART1 As String
        Dim mlPART2 As String
        Dim mlSYSID2 As String

        Try
            mlSQL2 = ""
            mlPART1 = ""
            mlPART2 = ""
            mlITEMKEY2 = ""
            mlITEMDESC2 = ""

            Dim mlDG As DataGridItem

            mlI = 0
            For Each mlDG In mlDATAGRID_TOOLS1.Items
                Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)

                If mlBOX.Checked Then
                    mlSYSID2 = "T1"
                    mlITEMKEY2 = Trim(mlDG.Cells("1").Text)
                    mlPART1 = mlPART1 & IIf(mlPART1 = "", "", ",") & mlITEMKEY2
                    mlI = mlI + 1
                    mlSQL2 = mlSQL2 & "INSERT INTO [CT_CONTRACT_DT] (ParentCode,SysID,DocNo,Linno,ItemKey,Description," & _
                            " RecName,RecordStatus,RecUserID,RecDate)" & _
                            " VALUES ('" & Trim(mpFUNCTIONPARAM) & "', '" & Trim(mlSYSID2) & " ', '" & Trim(mpDOCNO) & "'," & _
                            " '" & mlI & "','" & mlITEMKEY2 & "','" & mlITEMKEY2 & "', '" & Session("mgNAME") & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
                End If
            Next mlDG

            mlI = 0
            mlPART1 = mlPART1 & "#"
            For Each mlDG In mlDATAGRID_TOOLS2.Items
                Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)

                If mlBOX.Checked Then
                    mlSYSID2 = "T2"
                    mlITEMKEY2 = Trim(mlDG.Cells("1").Text)
                    mlPART2 = mlPART2 & IIf(mlPART2 = "", "", ",") & mlITEMKEY2
                    mlI = mlI + 1

                    mlSQL2 = mlSQL2 & "INSERT INTO [CT_CONTRACT_DT] (ParentCode,SysID,DocNo,Linno,ItemKey,Description," & _
                            " RecName,RecordStatus,RecUserID,RecDate)" & _
                            " VALUES ('" & Trim(mpFUNCTIONPARAM) & "', '" & Trim(mlSYSID2) & " ', '" & Trim(mpDOCNO) & "'," & _
                            " '" & mlI & "','" & mlITEMKEY2 & "','" & mlITEMKEY2 & "', '" & Session("mgNAME") & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"
                End If
            Next mlDG
            mlPART2 = mlPART2 & "#"


            Return mlPART1 & mlPART2 & mlSQL2

        Catch ex As Exception
        End Try

    End Function


    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean

        Try
            mlNEWRECORD = False



            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_CT_ContractHR_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"), Session("mgNAME"))
            End Select


            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If txDOCUMENTNO.Text = "--AUTONUMBER--" Then
                        mlKEY = mlOBJGS.GetModuleCounter("CT_CTEntry_" & mlFUNCTIONPARAMETER, "00000000")
                        txDOCUMENTNO.Text = mlKEY
                    Else
                        mlKEY = Trim(txDOCUMENTNO.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM CT_CONTRACTHR WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM CT_CONTRACT_JOB WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM CT_CONTRACT_DT WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    'mlSQL = mlSQL & " DELETE FROM CT_CONTRACTHR WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " UPDATE CT_CONTRACTHR SET RecordStatus='Delete' WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " UPDATE CT_CONTRACT_JOB SET RecordStatus='Delete' WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " UPDATE CT_CONTRACT_DT SET RecordStatus='Delete' WHERE DocNo = '" & mlKEY & "';"

            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""



            'Update Contract File
            Dim mlFS_DOCNO As String
            Dim mlFS_FUNCTIONPARAMETER As String
            mlFS_FUNCTIONPARAMETER = "CT"
            mlFS_DOCNO = lbFILEDOCNO.Value & ""
            Select Case mpSTATUS
                Case "New"
                    mlFS_DOCNO = mlOBJGS.GetModuleCounter("FS_UPLOAD_" & mlFS_FUNCTIONPARAMETER, "00000000")
            End Select
            Sql_File(mpSTATUS, mlNEWRECORD, mlFS_FUNCTIONPARAMETER, mlFS_DOCNO, "Contract", Trim(txDESCRIPTION.Text))
            'End Update Contract File

            Dim mlCRDATE1 As String
            Dim mlCRDATE2 As String
            If txCRDOCDATE1.Text = "" Then
                mlCRDATE1 = ""
            Else
                mlCRDATE1 = mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txCRDOCDATE1.Text, "/"))
            End If

            If txCRDOCDATE1.Text = "" Then
                mlCRDATE2 = ""
            Else
                mlCRDATE2 = mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txCRDOCDATE2.Text, "/"))
            End If



            'Contract Detail
            Dim mlTOOLS1 As String
            Dim mlTOOLS2 As String
            Dim mlSQL_DT As String
            mlSQL_DT = Sql_3(mlFUNCTIONPARAMETER, "", mlKEY)
            mlTOOLS1 = mlOBJGF.GetStringAtPosition(mlSQL_DT, "0", "#")
            mlTOOLS2 = mlOBJGF.GetStringAtPosition(mlSQL_DT, "1", "#")
            mlSQL_DT = mlOBJGF.GetStringAtPosition(mlSQL_DT, "2", "#")
            lbKOMP1.Text = Trim(mlTOOLS1)
            lbKOMP2.Text = Trim(mlTOOLS2)


            If mlNEWRECORD = True Then
                mlSQL = ""

                txPRICE.Text = mlOBJGF.UnFormatMoney(txPRICE.Text)
                txPRICE2.Text = mlOBJGF.UnFormatMoney(txPRICE2.Text)
                txPRICE3.Text = mlOBJGF.UnFormatMoney(txPRICE3.Text)
                txPERCENTAGE.Text = mlOBJGF.UnFormatMoney(txPERCENTAGE.Text)
                txMANPOWER.Text = mlOBJGF.UnFormatMoney(txMANPOWER.Text)
                txMANPOWER2.Text = mlOBJGF.UnFormatMoney(txMANPOWER2.Text)


                mlSQL = mlSQL & " INSERT INTO CT_CONTRACTHR (" & _
                            " ParentCode,SysID,DocNo,DocDate,CustID,CustName,SiteCardID,SiteCardName,Address,City,State,Country,Zip," & _
                            " Phone1,PIC_ContactNo," & _
                            " ContractNo,ContractDate,ReffNo,ReffDocNo,StartDate,EndDate,ServiceType,EmployeeQty,ExistingEmployeeQty,IncrementPercent," & _
                            " ExistingPrice,ProposePrice,Price,Negotiator,SC_Branch,SC_BranchName," & _
                            " Description,FileDocNo,CompanyID," & _
                            " NavJobNo,NavService," & _
                            " Tools1,Tools2," & _
                            " RecordStatus,RecUserID,RecDate" & _
                            " ) VALUES ( " & _
                            " '" & mlFUNCTIONPARAMETER & "', '', '" & mlKEY & "','" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE.Text, "/")) & "'," & _
                            " '" & Trim(txCUST.Text) & "', '" & Trim(lbCUSTDESC.Text) & "','" & Trim(txSITECARD.Text) & "'," & _
                            " '" & Trim(lbSITEDESC.Text) & "', '" & Trim(txADDR.Text) & "','" & Trim(txCITY.Text) & "'," & _
                            " '" & Trim(ddSTATE.Text) & "', '" & Trim(ddCOUNTRY.Text) & "'," & _
                            " '" & Trim(txZIP.Text) & "', '" & Trim(txPHONE1.Text) & "','" & Trim(txPHONE1_PIC.Text) & "'," & _
                            " '" & Trim(txCTDOCNO.Text) & "', '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(txDOCDATE2.Text, "/")) & "'," & _
                            " '" & Trim(txREFFNO.Text) & "', '" & Trim(lbREFFDOCNO.Text) & "', " & _
                            " '" & mlCRDATE1 & "', '" & mlCRDATE2 & "'," & _
                            " '" & Trim(ddPRODUCT.Text) & "'," & _
                            " '" & Trim(txMANPOWER.Text) & "','" & Trim(txMANPOWER2.Text) & "','" & Trim(txPERCENTAGE.Text) & "'," & _
                            " '" & Trim(txPRICE2.Text) & "', '" & Trim(txPRICE3.Text) & "','" & Trim(txPRICE.Text) & "'," & _
                            " '" & Trim(txUSERID.Text) & "', " & _
                            " '" & Trim(txBRANCH.Text) & "','" & Trim(lbBRANCH.Text) & "'," & _
                            " '" & Trim(txDESCRIPTION.Text) & "','" & mlFS_DOCNO & "','" & ddENTITY.Text & "', " & _
                            " '" & Trim(txJOBNO.Text) & "','" & Trim(lbITEMCART2.Value) & "'," & _
                            " '" & Trim(lbKOMP1.Text) & "','" & Trim(lbKOMP2.Text) & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');"

                mlSQL = mlSQL & Sql_2(mlFUNCTIONPARAMETER, "", mlKEY)
                mlSQL = mlSQL & mlSQL_DT
            End If


            Select Case mpSTATUS
                Case "New"
                    mlSQL = mlSQL & mlOBJPJ.ISS_CT_ContractHR_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"), Session("mgNAME"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            'mlSQL = ""

        Catch ex As Exception
            mlSQL = Err.Description
            mlOBJGS.XMtoLog("", "", "" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub



End Class

