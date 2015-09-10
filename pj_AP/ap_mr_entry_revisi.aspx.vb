
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports IAS.Core.CSCode

Partial Class pj_AP_ap_mr_entry_revisi
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSql_2 As String
    Dim mlKEY As String
    Dim mlKEY2 As String
    Dim mlKEY3 As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String

    Dim mlDATATABLE As DataTable
    Dim mlDATAROW As DataRow
    Dim mlI As Integer

    Dim mlSQLTEMP As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlCURRENTBVMONTH As String = DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlSHOWTOTAL As Boolean
    Dim mlSHOWPRICE As Boolean
    Dim mlUSERLEVEL As String

    Dim mlCOMPANYTABLENAME As String
    Dim mlCOMPANYID As String
    Dim mlPARAM_COMPANY As String

    Dim _mlDATATABLE_ITEMLIST As DataTable
    Public Property mlDATATABLE_ITEMLIST() As DataTable
        Get
            Return ViewState("mlDATATABLE_ITEMLIST")
        End Get
        Set(ByVal Value As DataTable)
            ViewState("mlDATATABLE_ITEMLIST") = Value
        End Set
    End Property
   

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        mlTITLE.Text = "Material Requisition Entry V2.11"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Material Requisition Entry V2.11"

        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        'Session("mgUSERID") = "52919"
        'Session("mgNAME") = "52919"


        mlPARAM_COMPANY = Left(Trim(Request("mpFP")), 1)
        If mlPARAM_COMPANY = "" Then mlPARAM_COMPANY = "1"
        mlFUNCTIONPARAMETER = Trim(Request("mpFP"))

        Select Case mlPARAM_COMPANY
            Case "", "1"
                ddlEntity.Items.Clear()
                ddlEntity.Text = "ISS"
                ddlEntity.Items.Add("ISS")
                mlTITLE.Text = mlTITLE.Text & " (ISS)"
            Case "2"
                ddlEntity.Items.Clear()
                ddlEntity.Text = "IFS"
                ddlEntity.Items.Add("IFS")
                mlTITLE.Text = mlTITLE.Text & " (IFS - Facility Services)"
            Case "3"
                ddlEntity.Items.Clear()
                ddlEntity.Text = "IPM"
                ddlEntity.Items.Add("IPM")
                mlTITLE.Text = mlTITLE.Text & " (IPM - Parking Management)"
            Case "4"
                ddlEntity.Items.Clear()
                ddlEntity.Text = "ICS"
                ddlEntity.Items.Add("ICS")
                mlTITLE.Text = mlTITLE.Text & " (ICS - Catering Services)"
        End Select

        mlCOMPANYTABLENAME = "ISS Servisystem, PT$"
        mlCOMPANYID = mlCOMPANYID
        Select Case ddlEntity.Text
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
            LoadCombo()
            tr2.Visible = False
            ' tbTABLE1.Visible = False
            DisableCancel()
            RetrieveFieldsDetail()
            mlSHOWTOTAL = False
            mlSHOWPRICE = False
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "pc_mr_entry", "Mr. Request", "")
            GetMonth()

            mlDATATABLE_ITEMLIST = New DataTable()
        Else
        End If

        hpLookup.NavigateUrl = "javascript:OpenWinLookUpSiteCard('" + mpSITECARD.ClientID + "','" + mpSITEDESC.ClientID + "','" + hdnSiteCardID.ClientID + "','" + hdnSiteCardName.ClientID + "','" + mpJobNo.ClientID + "','" + mpJobTaskNo.ClientID + "','" + hdnJobNo.ClientID + "','" + hdnJobTaskNo.ClientID + "','" + ddlEntity.ClientID + "','AccMnt')"

    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Sub LoadCombo()
        ''fill Template DDL
        mpMR_TEMPLATE.Items.Clear()
        mpMR_TEMPLATE.Items.Add(New ListItem("Pilih", ""))
        mpMR_TEMPLATE.Items.Add(New ListItem("No Template", "-"))
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_FORM' ORDER BY LinCode"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            mpMR_TEMPLATE.Items.Add(New ListItem(Trim(mlRSTEMP("LinCode")) & "-" & mlRSTEMP("Description"), mlOBJGF.GetStringAtPosition(Trim(mlRSTEMP("LinCode")), 0, "-")))
        End While


        ''fill Dept DDL
        mpDEPT.Items.Clear()
        mpDEPT.Items.Add(New ListItem("Pilih", ""))
        mlSQLTEMP = "SELECT DISTINCT Name FROM  [" & mlCOMPANYTABLENAME & "Resource] ORDER BY Name"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
        While mlRSTEMP.Read
            mpDEPT.Items.Add(New ListItem(Trim(mlRSTEMP("Name")), ""))
        End While
        mpDEPT.Items.Add(New ListItem("Lainnya", ""))

        '' fill DeptCode DDL
        ddDEPTCODE.Items.Clear()
        ddDEPTCODE.Items.Add(New ListItem("Pilih", ""))
        mlSQLTEMP = "SELECT * FROM  [" & mlCOMPANYTABLENAME & "Dimension Value] WHERE [DIMENSION CODE]='DEPT' ORDER BY NAME"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", mlCOMPANYID)
        While mlRSTEMP.Read
            ddDEPTCODE.Items.Add(New ListItem(Trim(mlRSTEMP("Code") & "-" & mlRSTEMP("Name")), Trim(mlRSTEMP("Code"))))
        End While
        mpDEPT.Items.Add(New ListItem("Lainnya", ""))

        '' fill State DDL
        ddSTATE.Items.Clear()
        ddSTATE.Items.Add(New ListItem("Pilih", ""))
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'PROPINSI'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            ddSTATE.Items.Add(New ListItem(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"), Trim(mlREADER("LinCode"))))
        End While

        '' fill Country DDL
        ddCOUNTRY.Items.Clear()
        ddCOUNTRY.Items.Add(New ListItem("Pilih", ""))
        mlSQL = "SELECT LinCode,Description FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'NEGARA'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            ddCOUNTRY.Items.Add(New ListItem(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"), Trim(mlREADER("LinCode"))))
        End While
        ddCOUNTRY.SelectedValue = "IDN"

    End Sub
    Sub DeleteRecord()
        If CheckRecordForEditing(Session("mgUSERID"), mlKEY2, mlKEY3) = False Then
            Exit Sub
        End If

        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
        RetrieveFieldsDetail()
    End Sub
    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        '   LoadCombo()
        mpDOCUMENTNO.Text = "--AUTONUMBER--"
        mlOBJPJ.SetTextBox(True, mpDOCUMENTNO)
        pnlDATALIST.Visible = False
    End Sub
    Sub ClearFields()
        mpDOCUMENTNO.Text = ""
        mpDOCDATE.Text = mlCURRENTDATE
        mpDESC.Text = ""
        mpSITECARD.Text = ""
        mpSITEDESC.Text = ""
        mpJobNo.Text = ""
        mpJobTaskNo.Text = ""
        ''mpPERIOD.Text = mlCURRENTBVMONTH
        mpDESC.Text = ""
        mlLINKDOC.Text = ""
        mlMESSAGE.Text = ""

        txADDR.Text = ""
        txCITY.Text = ""
        txZIP.Text = ""
        txPHONE1.Text = ""
        txPHONE1_PIC.Text = ""

    End Sub
    Sub EditRecord()
        mlMESSAGE.Text = ""

        If CheckRecordForEditing(Session("mgUSERID"), mlKEY2, mlKEY3) = False Then
            Exit Sub
        End If

        mlOBJGS.mgNEW = False
        mlOBJGS.mgEDIT = True
        ClearFields()
        LoadCombo()
        EnableCancel()
        mlOBJPJ.SetTextBox(True, mpDOCUMENTNO)

        'RetrieveFields()
        'RetrieveFieldsDetail2()
        'RetrieveFieldsDetail4()
        'FillDetail(mlKEY)

        pnlFILL.Visible = True
        pnlGRID.Visible = True
        'VisiblePrice()
    End Sub
    Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnlFILL.Visible = False
        pnlGRID.Visible = False
        pnlTemplate.Visible = True

        btDOCDATE.Visible = False
        btCALENDAR1.Visible = False
        btCALENDAR2.Visible = False
        btPERIOD.Visible = False
        mlOBJPJ.SetTextBox(False, mpDOCUMENTNO)
        mlOBJPJ.SetTextBox(True, mpDOCDATE)
        mlOBJPJ.SetTextBox(False, mpSITECARD)
        mpDEPT.Enabled = True
        mlOBJPJ.SetTextBox(False, mpPERIOD)
        mlOBJPJ.SetTextBox(False, mpDESC)
        mpLOCSAVE.Enabled = True
        '        mlOBJPJ.SetTextBox(False, txPERCENTAGE)

        mlOBJPJ.SetTextBox(False, txADDR)
        mlOBJPJ.SetTextBox(False, txCITY)
        ddSTATE.Enabled = True
        ddCOUNTRY.Enabled = True
        mlOBJPJ.SetTextBox(False, txZIP)
        mlOBJPJ.SetTextBox(False, txPHONE1)
        mlOBJPJ.SetTextBox(False, txPHONE1_PIC)
        ddDEPTCODE.Enabled = True

    End Sub
    Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnlFILL.Visible = False
        pnlGRID.Visible = False
        pnlTemplate.Visible = False
        pnlDATALIST.Visible = True

        btDOCDATE.Visible = False
        btCALENDAR1.Visible = False
        btCALENDAR2.Visible = False
        btPERIOD.Visible = False
        ' btSITECARD.Visible = False
        mlOBJPJ.SetTextBox(True, mpDOCUMENTNO)
        mlOBJPJ.SetTextBox(True, mpDOCDATE)
        mlOBJPJ.SetTextBox(True, mpSITECARD)
        mpDEPT.Enabled = False
        mlOBJPJ.SetTextBox(True, mpPERIOD)
        mlOBJPJ.SetTextBox(True, mpDESC)
        mpLOCSAVE.Enabled = False
        '        mlOBJPJ.SetTextBox(True, txPERCENTAGE)

        mlOBJPJ.SetTextBox(True, txADDR)
        mlOBJPJ.SetTextBox(True, txCITY)
        ddSTATE.Enabled = False
        ddCOUNTRY.Enabled = False
        mlOBJPJ.SetTextBox(True, txZIP)
        mlOBJPJ.SetTextBox(True, txPHONE1)
        mlOBJPJ.SetTextBox(True, txPHONE1_PIC)
        ddDEPTCODE.Enabled = False

    End Sub
    Sub SiteLocation()
        Dim mlMRLEVEL As Byte
        Try
            mlMRLEVEL = Left(mlOBJPJ.ISS_MR_UserLevel(Trim(Session("mgUSERID")), mpSITECARD.Text), 1)
            If mlMRLEVEL = "0" Then
                mlMESSAGE.Text = " Site Card " & mpSITECARD.Text & " belum diberikan Akses kepada Anda, silahkan hub Head Office (HO)"
                mpSITECARD.Text = ""
                mpSITEDESC.Text = ""
                Exit Sub
            End If

            If mlMRLEVEL >= "2" Then
                tr2.Visible = True
            End If

            mpLOCATION.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_ADDR_ALL", Trim(mpSITECARD.Text), "")
            '            mpLOCATION.Text = mlOBJPJ.ISS_XMGeneralLostFocus("SITECARD_ADDR_ALL", Trim(mpSITECARD.Text))

            If txADDR.Text = "" Then
                mlSQLTEMP = "SELECT * FROM XM_ADDRESSBOOK WHERE AddressCode='ARSHIP' AND AddressKey='" & Trim(mpSITECARD.Text) & "'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP)
                If mlRSTEMP.HasRows Then
                    mlRSTEMP.Read()
                    txADDR.Text = mlRSTEMP("Address") & ""
                    txCITY.Text = mlRSTEMP("City") & ""
                    If mlRSTEMP("State") <> "" Then ddSTATE.Items.Add(mlRSTEMP("State"))
                    txPHONE1.Text = mlRSTEMP("Phone1") & " " & mlRSTEMP("Phone2") & ""
                    txPHONE1_PIC.Text = mlRSTEMP("DefaultPIC") & " " & mlRSTEMP("Mobile1") & " " & mlRSTEMP("Mobile2")

                    txADDR.Text = Replace(txADDR.Text, "&nbsp;", "")
                    txCITY.Text = Replace(txCITY.Text, "&nbsp;", "")
                    txPHONE1.Text = Replace(txPHONE1.Text, "&nbsp;", "")
                    txPHONE1_PIC.Text = Replace(txPHONE1_PIC.Text, "&nbsp;", "")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub CancelOperation()
        mlMESSAGE.Text = ""
        ClearFields()
        DisableCancel()
        'RetrieveFieldsDetail()
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
            mlKEY = mpDOCUMENTNO.Text
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
            mlMESSAGE.Text = ex.Message
            Return
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
        RetrieveFieldsDetail()

        mlMESSAGE.Text = mlMESSAGE.Text & "<br>" & " MR Anda Adalah " & "<a href=ap_doc_mr.aspx?mpID=" & mlKEY & ">" & mlKEY & " (click to view) " & "</a>"
        mlLINKDOC.Text = "<font Color=blue> Click to melihat MR Document Anda </font>"
        mlLINKDOC.NavigateUrl = ""
        mlLINKDOC.Attributes.Add("onClick", "window.open('ap_doc_mr.aspx?mpID=" & mlKEY & "','','');")
    End Sub
    Sub RetrieveFields()
        mlSQL = "SELECT * FROM AP_MR_REQUESTHR WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows Then
            mlREADER.Read()
            Try
                mpMR_TEMPLATE.SelectedValue = mlREADER("MRType")
            Catch ex As Exception
                mpMR_TEMPLATE.Items.Add(mlREADER("MRType") & "-" & mlREADER("MRDesciption") & "")
            End Try

            mpDOCUMENTNO.Text = mlREADER("DocNo")
            mpDOCDATE.Text = IIf(mlOBJGF.IsNone(mlREADER("DocDate")), "", mlOBJGF.ConvertDateIntltoIDN(mlREADER("DocDate"), "/") & "")
            mpSITECARD.Text = mlREADER("SiteCardID")
            mpSITEDESC.Text = mlREADER("SiteCardName")
            mpLOCATION.Text = mlREADER("Location")
            mpJobNo.Text = mlREADER("JobNo")
            mpJobTaskNo.Text = mlREADER("JobTaskNo")


            Try
                mpDEPT.SelectedValue = mlREADER("DeptID")
            Catch ex As Exception
                mpDEPT.Items.Add(mlREADER("DeptID"))
            End Try

            mpPERIOD.Text = mlREADER("BVMonth")
            mpDESC.Text = mlREADER("Description")

            txADDR.Text = mlREADER("Do_Address")
            txCITY.Text = mlREADER("Do_City")
            Try
                ddSTATE.SelectedValue = mlREADER("Do_State")
            Catch ex As Exception
                ddSTATE.Items.Add(mlREADER("Do_State"))
            End Try

            Try
                ddCOUNTRY.SelectedValue = mlREADER("Do_Country")
            Catch ex As Exception
                ddCOUNTRY.Items.Add(mlREADER("Do_Country"))
            End Try

            Try
                ddDEPTCODE.SelectedValue = mlREADER("DeptCode")
            Catch ex As Exception
                ddDEPTCODE.Items.Add(mlREADER("DeptCode"))
            End Try

            txZIP.Text = mlREADER("Do_Zip")
            txPHONE1.Text = mlREADER("DO_Phone1")
            txPHONE1_PIC.Text = mlREADER("PIC_ContactNo")

            'hfPOSTINGUSERID1.Value = mlREADER("PostingUserID1")
            'hfPOSTINGUSERID2.Value = mlREADER("PostingUserID2")
            'hfPOSTINGUSERID3.Value = mlREADER("PostingUserID3")
            'hfPOSTINGUSERID4.Value = mlREADER("PostingUserID4")
            'hfPOSTINGUSERID5.Value = mlREADER("PostingUserID5")
            'hfPOSTINGNAME1.Value = mlREADER("PostingName1")
            'hfPOSTINGNAME2.Value = mlREADER("PostingName2")
            'hfPOSTINGNAME3.Value = mlREADER("PostingName3")
            'hfPOSTINGNAME4.Value = mlREADER("PostingName4")
            'hfPOSTINGNAME5.Value = mlREADER("PostingName5")
            'hfPOSTINGDATE1.Value = mlREADER("PostingDate1")
            'hfPOSTINGDATE2.Value = mlREADER("PostingDate2")
            'hfPOSTINGDATE3.Value = mlREADER("PostingDate3")
            'hfPOSTINGDATE4.Value = mlREADER("PostingDate4")
            'hfPOSTINGDATE5.Value = mlREADER("PostingDate5")
            'hfRECORDSTATUS.Value = mlREADER("RecordStatus")

            ' Added by Rafi (2014-11-25)           
            'ddlPeriodeMR.SelectedItem.Text = mlREADER("BVMonth").ToString()
            mpDESC.Text = mlREADER("Keterangan").ToString()

            For n As Integer = 0 To ddlPeriodeMR.Items.Count - 1
                If ddlPeriodeMR.Items(n).Value = Left(mlREADER("BVMonth").ToString(), 2) Then
                    ddlPeriodeMR.SelectedIndex = n
                    Exit For
                End If
            Next

        End If
    End Sub
    Sub FillDetail(ByVal mpDOCNO As String)
        Dim mlCODEID As String

        mlSql_2 = " select a.DocNo,a.Description,a.ItemKey,a.Uom,a.Description3 as Keterangan,a.Qty_Bal as BalanceAmount,  b.Linno,b.fSize,b.Qty as Quantity, " & vbCrLf
        mlSql_2 += "        CASE WHEN INM.ITEMKEY IS NULL THEN 0 ELSE 1 END AS FlagView       " & vbCrLf
        mlSql_2 += " from AP_MR_REQUESTDT a " & vbCrLf
        mlSql_2 += " inner join AP_MR_REQUESTDT2 b" & vbCrLf
        mlSql_2 += " 	on a.ItemKey = b.ItemKey" & vbCrLf
        mlSql_2 += " 	and a.DocNo = b.DocNo" & vbCrLf
        mlSql_2 += " left join PROD_ISS_NAV.dbo.IN_INMAST_ADDINFO INM " & vbCrLf
        mlSql_2 += " 	on a.ItemKey = inm.ItemKey " & vbCrLf
        mlSql_2 += " where a.DocNo = '" & mpDOCNO & "'" & vbCrLf
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", "ISSP3")
        mlDATATABLE_ITEMLIST = New DataTable()
        mlDATATABLE_ITEMLIST.Load(mlREADER2)
        mlDATAGRIDITEMLIST.DataSource = mlDATATABLE_ITEMLIST
        mlDATAGRIDITEMLIST.DataBind()

    End Sub
    Sub RetrieveFieldsDetail()
        mlSql_2 = "SELECT distinct A.DocNo,convert(varchar(10),a.DocDate,103) as Date,MRType,a.SiteCardID,A.SiteCardName,BVMonth as Period, Do_Address as Delivery_Address,Do_City AS City,a.RecordStatus as MRStatus,PostingUserID1 AS CreateID,PostingName1 AS Name,a.JobNo,a.JobTaskNo " & vbCrLf
        mlSql_2 += " FROM AP_MR_REQUESTHR a " & vbCrLf
        mlSql_2 += " INNER JOIN OP_USER_SITE b " & vbCrLf
        mlSql_2 += "    on a.JobNo= b.JobNo " & vbCrLf
        mlSql_2 += "    and a.JobTaskNo = b.JobTaskNo " & vbCrLf
        '        mlSql_2 += "    and a.SiteCardID = b.SiteCardID " & vbCrLf
        mlSql_2 += " WHERE A.ParentCode = '" & mlFUNCTIONPARAMETER & "' " & vbCrLf
        mlSql_2 += " AND (PostingUserID1 = '" & Session("mgUSERID") & "' OR PostingUserID2 = '" & Session("mgUSERID") & "' OR PostingUserID3 = '" & Session("mgUSERID") & "' ) " & vbCrLf
        'mlSql_2 += " and b.userID = '" & Trim(Session("mgUSERID")) & "' " & vbCrLf
        mlSql_2 += " AND b.RecordStatus='New' " & vbCrLf
        mlSql_2 += " AND a.RecordStatus not in ('New','Post','Delete')" & vbCrLf
        mlSql_2 += " ORDER BY a.DocNo"
        mlREADER2 = mlOBJGS.DbRecordset(mlSql_2, "PB", "ISSP3")
        mlDGDATALIST.DataSource = mlREADER2
        mlDGDATALIST.DataBind()
    End Sub

    Function ValidateForm() As String

        ValidateForm = ""
        mpPERIOD.Text = ddlPeriodeMR.SelectedItem.Text

        If mpSITECARD.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Site Card tidak boleh kosong"
        End If

        If mpSITEDESC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Nama Site Card tidak boleh kosong"
        End If

        If txADDR.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Alamat untuk Pengiriman tidak boleh kosong"
        End If

        If txCITY.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Kota untuk Pengiriman tidak boleh kosong"
        End If

        If txPHONE1_PIC.Text = "" Then
            ValidateForm = ValidateForm & " <br /> PIC tidak boleh kosong"
        End If

        If ddSTATE.Text = "Pilih" Then
            ValidateForm = ValidateForm & " <br /> Propinsi untuk Pengiriman tidak boleh kosong"
        End If

        If mpPERIOD.Text = "" Then
            ValidateForm = ValidateForm & " <br /> Periode MR tidak boleh kosong"
        Else
            Dim mlSTR1 As String
            Dim mlSTR2 As String
            mlSTR1 = mlOBJGF.GetStringAtPosition(mpPERIOD.Text, 0, "/")
            mlSTR2 = mlOBJGF.GetStringAtPosition(mpPERIOD.Text, 1, "/")



            Try
                Dim mlSTR(2) As String
                mlSTR = mpPERIOD.Text.Split("/")

                If Len(mlSTR(0)) > "2" Or Len(mlSTR(1)) > "4" Then
                    ValidateForm = ValidateForm & " <br /> Format Periode MR adalah MM/YYYY = 2 digit Bulan + 4 digit Tahun tanpa spasi"
                End If
            Catch ex As Exception
            End Try

            If IsNumeric(mlSTR1) = False Or IsNumeric(mlSTR2) = False Then
                ValidateForm = ValidateForm & " <br /> Format Periode MR adalah MM/YYYY = 2 digit Bulan + 4 digit Tahun, contoh MR Agustus 2013 diisi dengan 08/2013"
            End If

            If Len(mpPERIOD.Text) > "7" Then
                ValidateForm = ValidateForm & " <br /> Format Periode MR adalah MM/YYYY = 2 digit Bulan + 4 digit Tahun (7 digit) "
            End If
        End If

        If Left(mlOBJPJ.ISS_MR_UserLevel(Trim(Session("mgUSERID")), mpSITECARD.Text), 1) = "0" Then
            ValidateForm = ValidateForm & " <br /> Site Card " & mpSITECARD.Text & " belum diberikan Akses kepada Anda, silahkan hub Head Office (HO)"
        End If

        Dim mlDG As DataGridItem
        Dim mlDG2 As DataGridItem
        Dim mlQTY2DT As Double
        Dim mlSIZEHR As String
        Dim mlHAVINGDETAILS As Boolean
        Dim mlANYDETAIL As Boolean
        Dim mlSAMEITEM As Boolean


        mlHAVINGDETAILS = True
        mlANYDETAIL = False
        mlSAMEITEM = False

        'For Each mlDG In mlDATAGRID2.Items
        '    Dim mlQTY As TextBox = CType(mlDG.FindControl("mpQTY"), TextBox)
        '    Dim mlSIZE2 As Label = CType(mlDG.FindControl("mpSIZE"), Label)

        '    If IsNumeric(mlQTY.Text) = True Then
        '        If mlHAVINGDETAILS = False Then mlHAVINGDETAILS = True

        '        mlQTY2DT = 0
        '        mlSIZEHR = ""
        '        For Each mlDG2 In mlDATAGRID3.Items
        '            If UCase(mlDG2.Cells("1").Text) = UCase(mlDG.Cells("2").Text) Then
        '                mlANYDETAIL = True

        '                Dim mlQTY2 As TextBox = CType(mlDG2.FindControl("mpQTY"), TextBox)

        '                If IsNumeric(mlQTY2.Text) = True Then
        '                    mlSIZEHR = mlSIZEHR & IIf(mlSIZEHR = "", "", ", ") & mlDG2.Cells("3").Text & "=" & mlQTY2.Text
        '                    mlQTY2DT = mlQTY2DT + mlQTY2.Text

        '                    mlSAMEITEM = True
        '                End If
        '            End If
        '        Next mlDG2


        '        If mlDATAGRID3.Items.Count <= 0 Then
        '            mlSAMEITEM = True
        '        End If

        '        If Not mlSAMEITEM Then
        '            ValidateForm = ValidateForm & " <br /> Item yang dipilih tidak sama "
        '        End If

        '        mlSIZE2.Text = mlSIZEHR

        '        If mlANYDETAIL = True Then
        '            If mlQTY2DT <> mlQTY.Text Then
        '                ValidateForm = ValidateForm & " <br /> Total Qty Ukuran untuk Kode " & mlDG.Cells("2").Text & " tidak sama = " & mlQTY.Text & " # " & mlQTY2DT
        '            End If
        '            mlANYDETAIL = False
        '        End If
        '    End If
        'Next mlDG

        If mlHAVINGDETAILS = False Then
            ValidateForm = ValidateForm & " <br /> Details tidak ditemukan"
        End If

    End Function
    Function GetUnitPrice(ByVal mpPITEMKEY As String) As Double
        Dim mlSQLUPRICE As String
        Dim mlRSUPRICE As OleDbDataReader

        ' Added by Rafi (2015-06-24) , khusus buat IFS...ambil UnitPrice nya mesti dCompare dg UnitItem yg lama dl..
        If ddlEntity.Text = "IFS" Then
            mlSQLUPRICE = "SELECT * FROM IFS$Mapping_Item WHERE [ItemNo] = '" & mpPITEMKEY & "' "
            mlRSUPRICE = mlOBJGS.DbRecordset(mlSQLUPRICE, "PB", "IFSN")
            If mlRSUPRICE.HasRows Then
                mlRSUPRICE.Read()
                mpPITEMKEY = IIf(IsDBNull(mlRSUPRICE("ItemNo_OLD")) = True, mlRSUPRICE("ItemNo"), mlRSUPRICE("ItemNo_OLD"))
            End If
        End If
        ' end of added

        GetUnitPrice = 0

        mlSQLUPRICE = "SELECT * FROM [" & mlCOMPANYTABLENAME & "Item] WHERE [No_] = '" & mpPITEMKEY & "' "
        mlRSUPRICE = mlOBJGS.DbRecordset(mlSQLUPRICE, "PB", mlCOMPANYID)
        If mlRSUPRICE.HasRows Then
            mlRSUPRICE.Read()
            GetUnitPrice = IIf(IsDBNull(mlRSUPRICE("Last Direct Cost")) = True, 0, mlRSUPRICE("Last Direct Cost"))
        End If
    End Function
    Function FindUDocNo(ByVal mpDOCNO As String) As String
        FindUDocNo = ""
        mlSQLTEMP = "SELECT RecUDocNo FROM AP_MR_REQUESTHR_Log WHERE DocNo = '" & mpDOCNO & "' ORDER BY RecUDocNo Desc"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        If mlRSTEMP.HasRows Then
            mlRSTEMP.Read()
            If mlOBJGF.IsNone(mlRSTEMP("RecUDocNo")) = True Then
                Return "1"
            Else
                Return mlRSTEMP("RecUDocNo") + 1
            End If
        Else
            Return "1"
        End If
    End Function
    Function CheckRecordForEditing(ByVal mpUSERID As String, ByVal mpSITECARDID As String, ByVal mpSTATUS As String) As Boolean
        CheckRecordForEditing = True

        Dim mlUSERLEVEL As String
        mlUSERLEVEL = Left(mlOBJPJ.ISS_MR_UserLevel(mpUSERID, mpSITECARDID), 1)
        If mlUSERLEVEL = "0" Then
            mlMESSAGE.Text = " <br /> Site Card " & mpSITECARD.Text & " belum diberikan Akses kepada Anda, silahkan hub Head Office (HO)"
            CheckRecordForEditing = False
        ElseIf Left(UCase(mpSTATUS), 4) = "WAIT" Then
            If CInt(Right(mpSTATUS, 1)) > CInt(mlUSERLEVEL) Then
                mlMESSAGE.Text = " <br /> Site Card " & mpSITECARD.Text & " telah melewati level otorisasi akses Anda, silahkan hub atasan Anda"
                CheckRecordForEditing = False
            End If
        End If

        If mpSTATUS = "New" Or mpSTATUS = "Post" Then
            mlMESSAGE.Text = "Record telah dikunci untuk di Edit"
            CheckRecordForEditing = False
        End If
    End Function


    Function Sql_2(ByVal mpSITECARDID As String, ByVal mpSITECARDDESC As String, ByVal mpSITEADDRESS As String, ByVal mpCITY As String, ByVal mpSTATE As String, ByVal mpCOUNTRY As String) As String
        mlSQLTEMP = "SELECT * FROM AR_SITECARD_ADDINFO WHERE SiteCardId = '" & mpSITECARDID & "'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        mlSQLTEMP = ""
        If mlRSTEMP.HasRows Then
            mlSQLTEMP = mlSQLTEMP & " UPDATE AR_SITECARD_ADDINFO SET Address = '" & mpSITEADDRESS & "'," & _
                    " City= '" & mpCITY & "', State= '" & mpSTATE & "',Country= '" & mpCOUNTRY & "'," & _
                    " RecUserID = '" & Session("mgUSERID") & "', RecDate = '" & mlOBJGF.FormatDate(Now) & "'" & _
                    " WHERE SiteCardID = '" & mpSITECARDID & "'"
        Else
            mlSQLTEMP = mlSQLTEMP & "INSERT INTO AR_SITECARD_ADDINFO (ParentCode,SiteCardID,Description,Address,City,State,Country," & _
                    " RecordStatus,RecUserID,RecDate) " & _
                    " VALUES ('','" & mpSITECARDID & "','" & mpSITECARDDESC & "','" & mpSITEADDRESS & "'," & _
                    " '" & mpCITY & "','" & mpSTATE & "','" & mpCOUNTRY & "'," & _
                    " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "')"
        End If
        Return mlSQLTEMP
    End Function
    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlI As Byte
        Dim mlHAVINGDETAIL As Boolean
        Dim mlMRTYPE As String
        Dim mlMRDESCRIPTION As String
        Dim mlMRLINE As Integer

        Dim mlUNITPRICE As Double
        Dim mlTOTALUNITPRICE As Double
        Dim mlTOTALPRICE As Double
        Dim mlDEPT2 As String

        Dim mlSql_2 As String
        Dim mlUDOCNO As String

        Dim mlSC_STATE As String
        Dim mlSC_BRANCH As String
        Dim mlSC_BRANCHCODE As String
        Dim mlSC_BRANCHNAME As String

        Dim mlPOSTUSERID1 As String
        Dim mlPOSTUSERID2 As String
        Dim mlPOSTUSERID3 As String
        Dim mlPOSTUSERID4 As String
        Dim mlPOSTUSERID5 As String
        Dim mlPOSTUSERNAME1 As String
        Dim mlPOSTUSERNAME2 As String
        Dim mlPOSTUSERNAME3 As String
        Dim mlPOSTUSERNAME4 As String
        Dim mlPOSTUSERNAME5 As String
        Dim mlPOSTUSERDATE1 As Date
        Dim mlPOSTUSERDATE2 As Date
        Dim mlPOSTUSERDATE3 As Date
        Dim mlPOSTUSERDATE4 As Date
        Dim mlPOSTUSERDATE5 As Date
        Dim mlRECSTATUS As String

        Try
            mlMRTYPE = mlOBJGF.GetStringAtPosition(mpMR_TEMPLATE.Text, "0", "-")
            mlMRDESCRIPTION = mlOBJGF.GetStringAtPosition(mpMR_TEMPLATE.Text, "1", "-")
            mlNEWRECORD = False
            mlMRLINE = 0
            mlTOTALPRICE = 0
            mlUDOCNO = FindUDocNo(mlKEY)

            mlSC_STATE = ""
            mlSC_BRANCH = ""
            mlSC_BRANCHCODE = ""
            mlSC_BRANCHNAME = ""

            If mlRECSTATUS = "" Then mlRECSTATUS = "Wait1"

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_MR_MREntry_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"), mlUDOCNO)
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If mpDOCUMENTNO.Text = "--AUTONUMBER--" Then
                        mlKEY = mlOBJGS.GetModuleCounter("MR_Entry_" & mlFUNCTIONPARAMETER, "00000000")
                        mpDOCUMENTNO.Text = mlKEY
                    Else
                        mlKEY = Trim(mpDOCUMENTNO.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True

                    mlSQL = mlSQL & " DELETE FROM AP_MR_REQUESTHR WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_REQUESTDT WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_REQUESTDT2 WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_REQUESTHR WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_REQUESTDT WHERE DocNo = '" & mlKEY & "';"
                    mlSQL = mlSQL & " DELETE FROM AP_MR_REQUESTDT2 WHERE DocNo = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            mlDEPT2 = IIf(mpDEPT.Text = "Pilih", "", mpDEPT.Text)

            Dim mlDG As DataGridItem
            Dim mlITEMKEY As String
            Dim mlORIITEMKEY As String
            Dim mlDESCDT3 As String
            Dim mlFIRST As Boolean
            Dim mlSQLDT3 As String
            Dim mlQtyItem As Double
            Dim mlLINNO As Int16

            If mlNEWRECORD = True Then
                mlSQL = ""

                mlHAVINGDETAIL = False
                mlI = 0

                mlFIRST = True
                mlORIITEMKEY = ""
                mlDESCDT3 = ""

                For Each mlDG In mlDATAGRIDITEMLIST.Items
                    Dim mlQTY As TextBox = CType(mlDG.FindControl("txtQty"), TextBox)
                    Dim mlBalanceAmount As ucInputNumber = CType(mlDG.FindControl("ucBalanceAmount"), ucInputNumber)
                    Dim mlSIZE As Label = CType(mlDG.FindControl("lblSize"), Label)
                    Dim mlDESCDT2 As TextBox = CType(mlDG.FindControl("txtKeterangan"), TextBox)
                    Dim mlItemKeyTxt As Label = CType(mlDG.FindControl("lblItemkey"), Label)
                    Dim mlItemDesc As Label = CType(mlDG.FindControl("lblItemName"), Label)
                    Dim mlSatuanItem As Label = CType(mlDG.FindControl("lblSatuan"), Label)


                    If IsNumeric(mlQTY.Text) = True Then
                        If CDbl(mlQTY.Text) > 0 Then

                            mlI = mlI + 1

                            mlUNITPRICE = Replace(GetUnitPrice(mlItemKeyTxt.Text), ",", ".")
                            mlITEMKEY = mlItemKeyTxt.Text
                            mlTOTALUNITPRICE = mlUNITPRICE * Trim(mlQTY.Text)
                            mlTOTALPRICE = Replace(mlTOTALPRICE + mlTOTALUNITPRICE, ",", ".")

                            mlBalanceAmount.Text = Replace(mlBalanceAmount.Text, ".00", "")
                            mlSIZE.Text = Replace(mlSIZE.Text, ",", ".")
                            mlQTY.Text = Replace(mlQTY.Text, ",", ".")

                            mlSQL += "INSERT INTO AP_MR_REQUESTDT2 (DocNo,Linno,ItemKey,Description,fSize,Qty,QtyDO)" & vbCrLf
                            mlSQL += " VALUES ('" & mlKEY & "','" & CDbl(mlI) & "','" & mlItemKeyTxt.Text & "','" & mlItemDesc.Text & "','" & mlSIZE.Text & "', '" & Trim(mlQTY.Text) & "','0');" & vbCrLf

                            If mlITEMKEY = mlORIITEMKEY Then
                                'mlDESCDT3 = mlDESCDT3 & IIf(mlOBJGF.IsNone(mlDESCDT3) = True, "", ", ") & mlSIZE.Text & "=" & mlQTY.Text

                                If FlagNamaOnItem(UCase(mlItemKeyTxt.Text)) Then    '' cek jika ada item yang harus menuliskan NAMA EMPLOYEE (ex:NameTag)
                                    mlDESCDT3 = mlDESCDT3 & IIf(mlOBJGF.IsNone(mlDESCDT3) = True, "", ", ") & mlDESCDT2.Text
                                Else
                                    mlDESCDT3 = mlDESCDT3 & IIf(mlOBJGF.IsNone(mlDESCDT3) = True, "", ", ") & mlSIZE.Text & "=" & mlQTY.Text
                                End If

                                mlQtyItem = mlQtyItem + mlQTY.Text
                                If mlSIZE.Text = "0" Then       ''jika sizenya tidak ada...ga usah isi description detailnya...
                                    mlDESCDT3 = ""
                                End If


                                mlSQL += " update AP_MR_REQUESTDT " & vbCrLf
                                mlSQL += " set Qty = '" & Trim(mlQtyItem) & "', " & vbCrLf
                                mlSQL += "     RequestDesc = '" & Trim(mlDESCDT3) & "', " & vbCrLf
                                mlSQL += "     Description2 = '" & Trim(mlDESCDT3) & "', " & vbCrLf
                                mlSQL += "     Description3 = '" & Trim(mlDESCDT3) & "' " & vbCrLf
                                mlSQL += " Where DocNo = '" & mlKEY & "' " & vbCrLf
                                mlSQL += " and ItemKey = '" & mlItemKeyTxt.Text & "' " & vbCrLf

                            Else
                                mlLINNO = mlLINNO + 1
                                'mlDESCDT3 = mlSIZE.Text & "=" & mlQTY.Text
                                If FlagNamaOnItem(UCase(mlItemKeyTxt.Text)) Then    '' cek jika ada item yang harus menuliskan NAMA EMPLOYEE (ex:NameTag)
                                    mlDESCDT3 = mlDESCDT3 & IIf(mlOBJGF.IsNone(mlDESCDT3) = True, "", ", ") & mlDESCDT2.Text
                                Else
                                    mlDESCDT3 = mlDESCDT3 & IIf(mlOBJGF.IsNone(mlDESCDT3) = True, "", ", ") & mlSIZE.Text & "=" & mlQTY.Text
                                End If

                                mlQtyItem = mlQTY.Text
                                If mlSIZE.Text = "0" Then       ''jika sizenya tidak ada...ga usah isi description detailnya...
                                    mlDESCDT3 = ""
                                End If


                                mlSQL += "INSERT INTO AP_MR_REQUESTDT (DocNo,Linno,ItemKey,Description,Uom,Qty,QtyDO," & vbCrLf
                                mlSQL += " UnitPoint,UnitPrice,TotalPoint,TotalPrice," & vbCrLf
                                mlSQL += " Qty_Bal,RequestDesc,Description2,Description3)" & vbCrLf
                                mlSQL += " VALUES ('" & mlKEY & "','" & CDbl(mlLINNO) & "','" & mlItemKeyTxt.Text & "','" & mlItemDesc.Text & "','" & mlSatuanItem.Text & "', " & vbCrLf
                                mlSQL += "'" & Trim(mlQtyItem) & "','0','0','" & CDbl(mlUNITPRICE) & "','0','" & CDbl(mlTOTALUNITPRICE) & "'," & vbCrLf
                                mlSQL += "'" & Trim(mlBalanceAmount.Text) & "','" & Trim(mlDESCDT3) & "','" & Trim(mlDESCDT3) & "','" & Trim(mlDESCDT3) & "');" & vbCrLf

                                mlORIITEMKEY = mlITEMKEY
                            End If
                        End If

                    End If
                Next

            End If

            mlSQL = mlSQL & mlSQLDT3

            'mlSQLTEMP = "SELECT SC.[CustomerNo_],SC.[SiteCode],SC.[LineNo_],SC.Branch," & _
            '    " BC.[Branch Location], BC.Code,BC.[Branch Location], BC.Name " & _
            '    " FROM [" & mlCOMPANYTABLENAME & "CustServiceSite]  SC,  [" & mlCOMPANYTABLENAME & "Location] BC" & _
            '    " WHERE SC.Branch = BC.[Branch Location]" & _
            '    " AND  SC.[LineNo_] = '" & Trim(mpSITECARD.Text) & "'" & vbCrLf
            'mlSC_BRANCH = mlRSTEMP("Branch Location") & ""
            'mlSC_BRANCHCODE = mlRSTEMP("Code") & ""
            'mlSC_BRANCHNAME = mlRSTEMP("Name") & ""

            mlSQLTEMP = "Select * from IFS$Mapping_JobTaskNo_SiteCard where Sitecard = '" & Trim(mpSITECARD.Text) & "' "

            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "IFSN")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                mlSC_STATE = ""
                mlSC_BRANCH = mlRSTEMP("BranchID")
                mlSC_BRANCHCODE = mlRSTEMP("LocationCode")
                mlSC_BRANCHNAME = mlRSTEMP("Description_LocationCode")
            End If


            Select Case mpSTATUS
                Case "New"
                    mlPOSTUSERID1 = Trim(Session("mgUSERID"))
                    mlPOSTUSERNAME1 = Trim(Session("mgNAME"))
                    mlPOSTUSERDATE1 = mlOBJGF.FormatDate(Now)
                Case Else
            End Select


            mpPERIOD.Text = ddlPeriodeMR.SelectedItem.Text

            ' Added by Rafi (25-11-2014), Field Keterangan di Table AP_MR_REQUESTHR supaya Data Keterangan bisa diSave

            mlSQL = mlSQL & " INSERT INTO AP_MR_REQUESTHR (ParentCode,MRType,MRDesciption,DocNo,DocDate,SiteCardID,SiteCardName," & _
                " Location,DeptID,BVMonth,Description,MRLine," & _
                " PercentageMR,TotalPoint,TotalAmount," & _
                " SC_State,SC_Branch,SC_BranchCode,SC_BranchName," & _
                " DeptCode,Do_Address,Do_City,Do_State,Do_Country,Do_Zip,DO_Phone1,PIC_ContactNo," & _
                " EntityID," & _
                " PostingUserID1,PostingName1,PostingDate1," & _
                " PostingUserID2,PostingName2,PostingDate2," & _
                " PostingUserID3,PostingName3,PostingDate3," & _
                " PostingUserID4,PostingName4,PostingDate4," & _
                " PostingUserID5,PostingName5,PostingDate5," & _
                " Keterangan, " & _
                " JobNo, JobTaskNo, " & _
                " RecordStatus,RecUserID,RecDate)" & _
                " VALUES ( " & _
                " '" & mlFUNCTIONPARAMETER & "','" & mlMRTYPE & "','" & mlMRDESCRIPTION & "'," & _
                " '" & mlKEY & "'," & _
                " '" & mlOBJGF.FormatDate(Now) & "'," & _
                " '" & Trim(mpSITECARD.Text) & "','" & Trim(mpSITEDESC.Text) & "'," & _
                " '" & Trim(mpLOCATION.Text) & "'," & _
                " '" & Trim(mlDEPT2) & "','" & _
                Trim(mpPERIOD.Text) & "','" & _
                Trim(mpDESC.Text) & "'," & _
                " '" & mlMRLINE & "'," & _
                " '" & Trim(txPERCENTAGE.Text) & "','0','" & CDbl(mlTOTALPRICE) & "'," & _
                " '" & Trim(mlSC_STATE) & "','" & Trim(mlSC_BRANCH) & "','" & Trim(mlSC_BRANCHCODE) & "','" & Trim(mlSC_BRANCHNAME) & "'," & _
                " '" & Trim(ddDEPTCODE.Text) & "', '" & Trim(txADDR.Text) & "','" & Trim(txCITY.Text) & "','" & Trim(ddSTATE.Text) & "'," & _
                " '" & Trim(ddCOUNTRY.Text) & "','" & Trim(txZIP.Text) & "','" & Trim(txPHONE1.Text) & "','" & Trim(txPHONE1_PIC.Text) & "'," & _
                " '" & Trim(ddlEntity.SelectedItem.Text) & "'," & _
                " '" & Trim(mlPOSTUSERID1) & "', '" & Trim(mlPOSTUSERNAME1) & "', '" & mlOBJGF.FormatDate(mlPOSTUSERDATE1) & "'," & _
                " '" & Trim(mlPOSTUSERID2) & "', '" & Trim(mlPOSTUSERNAME2) & "', '" & Trim(mlPOSTUSERDATE2) & "'," & _
                " '" & Trim(mlPOSTUSERID3) & "', '" & Trim(mlPOSTUSERNAME3) & "', '" & Trim(mlPOSTUSERDATE3) & "'," & _
                " '" & Trim(mlPOSTUSERID4) & "', '" & Trim(mlPOSTUSERNAME4) & "', '" & Trim(mlPOSTUSERDATE4) & "'," & _
                " '" & Trim(mlPOSTUSERID5) & "', '" & Trim(mlPOSTUSERNAME5) & "', '" & Trim(mlPOSTUSERDATE5) & "'," & _
                " '" & Trim(mpDESC.Text) & "', " & _
                " '" & Trim(mpJobNo.Text) & "','" & Trim(mpJobTaskNo.Text) & "', " & _
                " '" & mlRECSTATUS & "','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "');" & vbCrLf


            Select Case mpSTATUS
                Case "New"
                    mlSql_2 = ""
                    If mpLOCSAVE.Checked = True Then
                        mlSql_2 = Sql_2(Trim(mpSITECARD.Text), Trim(mpSITEDESC.Text), Trim(mpLOCATION.Text), Trim(txCITY.Text), Trim(ddSTATE.Text), Trim(ddCOUNTRY.Text))
                    End If
                    mlOBJGS.ExecuteQuery(mlSql_2, "PB", "ISSP3")
            End Select

            Select Case mpSTATUS
                Case "New", "Edit", "Delete"
                    mlSQL = mlSQL & mlOBJPJ.ISS_MR_MREntry_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"), mlUDOCNO)
            End Select

            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            Dim mlOBJPJ_UT As New IASClass_Local_ut.ucmLOCAL_ut
            mlSQL = "SELECT * FROM AP_MR_REQUESTDT WHERE DocNo = '" & mlKEY & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlRSTEMP.HasRows = False Then
                mlOBJPJ_UT.Sendmail_1("1", "sugianto@iss.co.id", "", "", "Error Save MR Online", mlKEY & " - " & Session("mgUSERID"))
                mlMESSAGE.Text = "Error ditemukan ketika MR disimpan" & "Save Fail"
                Exit Sub
            End If
            mlSQL = ""

            Select Case mpSTATUS
                Case "New"

                    Dim mlEMAIL_STATUS As String
                    Dim mlEMAIL_TO As String
                    Dim mlEMAIL_SUBJECT As String
                    Dim mlEMAIL_BODY As String
                    Dim mlLINKSERVER1 As String
                    Dim mlBCC As String

                    Dim mlRECSTATUSDESC As String
                    mlRECSTATUSDESC = ""
                    Select Case mlRECSTATUS
                        Case "Wait1"
                            mlRECSTATUSDESC = "Permintaan Baru, Ke Proses Review"
                    End Select

                    mlBCC = "iassystem_log@iss.co.id"
                    mlEMAIL_TO = ""
                    mlLINKSERVER1 = System.Configuration.ConfigurationManager.AppSettings("mgLINKEDSERVER1")

                    'Remark (2015-06-05) buat ganti SiteCard dg Job+JobTaskNo
                    'mlSQLTEMP = "SELECT EmailAddr FROM " & mlLINKSERVER1 & "AD_USERPROFILE  WHERE UserID IN " & _
                    '            " (SELECT UserID FROM OP_USER_SITE WHERE UserLevel < '3' AND SiteCardID='" & Trim(mpSITECARD.Text) & "') " & vbCrLf
                    mlSQLTEMP = "SELECT EmailAddr FROM " & mlLINKSERVER1 & "AD_USERPROFILE  WHERE UserID IN " & _
                                " (SELECT UserID FROM OP_USER_SITE WHERE UserLevel < '3' AND JobNo = '" & Trim(mpJobNo.Text) & " and JobTaskNo'='" & Trim(mpJobTaskNo.Text) & "') " & vbCrLf


                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                    While mlRSTEMP.Read
                        mlEMAIL_TO = mlEMAIL_TO & IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", ",") & mlRSTEMP("EmailAddr") & ""
                    End While

                    mlEMAIL_TO = IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", mlEMAIL_TO & ",") & ""
                    If mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = False Then
                        mlEMAIL_SUBJECT = "" & " Permintaan MR untuk " & Trim(mpSITECARD.Text) & "-" & mpSITEDESC.Text
                        mlEMAIL_BODY = ""
                        mlEMAIL_BODY = mlEMAIL_BODY & "Dear : " & Session("mgUSERID") & "-" & Session("mgNAME")
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Terima kasih telah melakukan transaksi permintaan MR "
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br> Berikut ini adalah informasi transaksi yang telah Anda lakukan :"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<table border=0>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Tanggal	</td><td valign=top>:</td><td valign=top>" & Now
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Periode MR	</td><td valign=top>:</td><td valign=top>" & ddlPeriodeMR.SelectedItem.Text
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Jenis(Transaksi) </td><td valign=top>:</td><td valign=top>" & mlMRTYPE & "-" & mlMRDESCRIPTION
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "No Dokumen  </td><td valign=top>:</td><td valign=top>" & mlKEY
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Lokasi  </td><td valign=top>:</td><td valign=top>" & Trim(mpSITECARD.Text) & "-" & Trim(mpSITEDESC.Text)
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Alamat  </td><td valign=top>:</td><td valign=top>" & Trim(txADDR.Text)
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Kota  </td><td valign=top>:</td><td valign=top>" & Trim(txCITY.Text)
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Propinsi  </td><td valign=top>:</td><td valign=top>" & Trim(ddSTATE.Text)
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Status Transaksi  </td><td valign=top>:</td><td valign=top>" & mlRECSTATUS & " -> " & mlRECSTATUSDESC
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "</table>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br>Terima Kasih"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br><i>Email ini dikirim Otomatis oleh Sistem Komputer, Email ini tidak perlu dibalas/</i>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><i>This is an automatically generated email by computer system, please do not reply </i>"
                        mlEMAIL_STATUS = mlOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, "", mlBCC, mlEMAIL_SUBJECT, mlEMAIL_BODY)
                    End If


                    'Email Update Delivery Address
                    If mpLOCSAVE.Checked = True Then
                        mlEMAIL_TO = ""
                        mlEMAIL_TO = IIf(mlOBJGF.IsNone(Session("mgUSERMAIL")), "", Session("mgUSERMAIL") & ",")
                        Select Case ddlEntity.Text
                            Case "ISS"
                                mlEMAIL_TO = mlEMAIL_TO & "emilia@iss.co.id"
                            Case "IPM"
                                mlEMAIL_TO = mlEMAIL_TO & "winarsih@iss.co.id"
                            Case "ICS"
                                'mlEMAIL_TO = mlEMAIL_TO & "emilia@iss.co.id"
                            Case "IFS"
                                mlEMAIL_TO = mlEMAIL_TO & "henyo@iss.co.id"
                            Case Else
                                mlEMAIL_TO = mlEMAIL_TO & "ops.regional@iss.co.id"
                        End Select


                        If mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = False Then
                            mlEMAIL_SUBJECT = "" & " Permintaan Pergantian Alamat Pengiriman untuk " & Trim(mpSITECARD.Text) & "-" & mpSITEDESC.Text
                            mlEMAIL_BODY = ""
                            mlEMAIL_BODY = mlEMAIL_BODY & "Dear Operation"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br> Berikut ini adalah informasi permintaan perubahan alamat pengiriman oleh " & Session("mgUSERID") & "-" & Session("mgNAME")
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<table border=0>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Tanggal	</td><td valign=top>:</td><td valign=top>" & Now
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "SiteCard </td><td valign=top>:</td><td valign=top>" & Trim(mpSITECARD.Text) & " - " & Trim(mpSITEDESC.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Alamat  </td><td valign=top>:</td><td valign=top>" & Trim(txADDR.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Kota  </td><td valign=top>:</td><td valign=top>" & Trim(txCITY.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Propinsi  </td><td valign=top>:</td><td valign=top>" & Trim(ddSTATE.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "Pic Contact </td><td valign=top>:</td><td valign=top>" & Trim(txPHONE1_PIC.Text)
                            mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "</table>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br>Terima Kasih"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><br><i>Email ini dikirim Otomatis oleh Sistem Komputer, Email ini tidak perlu dibalas/</i>"
                            mlEMAIL_BODY = mlEMAIL_BODY & "<br><i>This is an automatically generated email by computer system, please do not reply </i>"
                            mlEMAIL_STATUS = mlOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, "", mlBCC, mlEMAIL_SUBJECT, mlEMAIL_BODY)
                        End If

                    End If
            End Select

        Catch ex As Exception
            mlMESSAGE.Text = "Error ditemukan ketika MR disimpan" & "Save Fail"
            mlOBJGS.XMtoLog("MR", "MRRequest", "MRRequest" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub
    Protected Function FlagNamaOnItem(ByRef mlITEMNO As String) As Boolean
        Dim mlSQLCHECk As String
        FlagNamaOnItem = False

        mlSQLCHECk = " SELECT UniversalID, LinCode, Description, AdditionalDescription1, AdditionalDescription2, AdditionalDescription3" & vbCrLf
        mlSQLCHECk += " FROM XM_UNIVERSALLOOKUPLIN A" & vbCrLf
        mlSQLCHECk += " WHERE UNIVERSALID = 'FLAGNAMA_ON_ITEM'" & vbCrLf
        mlSQLCHECk += " AND a.LinCode = '" & mlITEMNO & "' " & vbCrLf
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLCHECk, "PB", "ISS")
        If mlRSTEMP.HasRows Then
            FlagNamaOnItem = True
        End If
        Return FlagNamaOnItem
    End Function

    Protected Sub btNewRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNewRecord.Click
        NewRecord()
    End Sub
    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        If pnlFILL.Visible = True And mlDATAGRIDITEMLIST.Items.Count > 0 Then
            SaveRecord()
        End If
    End Sub
    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        DisableCancel()
    End Sub
    Protected Sub btSUBMITTEMPLATE_Click(sender As Object, e As ImageClickEventArgs) Handles btSUBMITTEMPLATE.Click
        Dim mlTEMPLATEID As String
        mlTEMPLATEID = Trim(mlOBJGF.GetStringAtPosition(mpMR_TEMPLATE.Text, 0, "-"))
        If mlTEMPLATEID = "No Template" Then
            mlMESSAGE.Text = "Fasilitas No Template saat ini sedang dinon aktifkan"
            Exit Sub
        End If

        Try
            hlLookUp.NavigateUrl = "javascript:OpenWinLookUpItem('" + txtItemNo.ClientID + "','" + txtItemName.ClientID + "','" + hdnItemNo.ClientID + "','" + hdnItemName.ClientID + "','" + mlTEMPLATEID + "','AccMnt')"

            btSaveRecord.Visible = True
            NewRecord()
            pnlFILL.Visible = True
            pnlGRID.Visible = True

            CreateDatagriItem()

        Catch ex As Exception
        End Try

    End Sub

    Sub CreateDatagriItem()
        mlDATATABLE_ITEMLIST = New DataTable()
        mlDATATABLE_ITEMLIST.Columns.Add("Linno")
        mlDATATABLE_ITEMLIST.Columns.Add("FlagView")
        mlDATATABLE_ITEMLIST.Columns.Add("ItemKey")
        mlDATATABLE_ITEMLIST.Columns.Add("Description")
        mlDATATABLE_ITEMLIST.Columns.Add("uom")
        mlDATATABLE_ITEMLIST.Columns.Add("fsize")
        mlDATATABLE_ITEMLIST.Columns.Add("Quantity")
        mlDATATABLE_ITEMLIST.Columns.Add("BalanceAmount")
        mlDATATABLE_ITEMLIST.Columns.Add("Keterangan")

    End Sub
    Sub RetrieveItems()
        Dim mlSQL As String
        Dim mlDATALIST As New DataTable()
        Dim mlI As Integer


        mlSQL = " SELECT distinct a.ItemNo,a.ItemDesc,b.AdditionalDescription1 as fSize,c.uom,   " & vbCrLf
        mlSQL += "       CASE WHEN INM.ITEMKEY IS NULL THEN 0 ELSE 1 END AS FlagView " & vbCrLf
        mlSQL += " FROM [PROD_IFS_NAV2013].[dbo].[IFS$Mapping_Item] a " & vbCrLf
        mlSQL += " left join PROD_ISS_NAV.dbo.XM_UNIVERSALLOOKUPLIN  b " & vbCrLf
        mlSQL += "      on a.ItemNo_OLD = b.LinCode " & vbCrLf
        mlSQL += "      and B.UniversalID='Item_Size' " & vbCrLf
        mlSQL += "      AND b.AdditionalDescription2='" & ddlEntity.SelectedItem.Text & "' " & vbCrLf
        mlSQL += " left join PROD_ISS_NAV.dbo.IN_INMAST_ADDINFO INM " & vbCrLf
        mlSQL += "      on a.ItemNo_OLD = iNM.ItemKey " & vbCrLf
        mlSQL += " inner join PROD_ISS_NAV.dbo.AP_MR_TEMPLATEDT c " & vbCrLf
        mlSQL += "      on a.ItemNo_OLD = c.ItemKey " & vbCrLf
        mlSQL += "      and c.DocNo = '" & mpMR_TEMPLATE.SelectedValue & "' " & vbCrLf
        mlSQL += " WHERE 1=1 " & vbCrLf
        mlSQL += " and a.itemNo = '" & hdnItemNo.Value & "' " & vbCrLf
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQL, "PB", "IFSN")
        If Not mlRSTEMP.HasRows Then
            mlMESSAGE.Text = "Detail Item " & hdnItemName.Value & " Not Found"
            Return
        End If

        While mlRSTEMP.Read
            mlI = mlI + 1
            Dim drItem As DataRow
            drItem = mlDATATABLE_ITEMLIST.NewRow()
            drItem("FlagView") = mlRSTEMP.Item("FlagView").ToString()
            drItem("ItemKey") = mlRSTEMP.Item("ItemNo").ToString()
            drItem("Description") = mlRSTEMP.Item("ItemDesc").ToString()
            drItem("uom") = mlRSTEMP.Item("uom").ToString()
            drItem("fsize") = IIf(mlRSTEMP.Item("fSize").ToString() = "", "0", mlRSTEMP.Item("fSize").ToString())
            drItem("Quantity") = "0"
            drItem("BalanceAmount") = "0"
            drItem("Keterangan") = ""

            mlDATATABLE_ITEMLIST.Rows.Add(drItem)
        End While

        mlDATAGRIDITEMLIST.DataSource = mlDATATABLE_ITEMLIST
        mlDATAGRIDITEMLIST.DataBind()

    End Sub
    Protected Sub SaveitemTemporary()
        mlDATATABLE_ITEMLIST.Rows.Clear()
        Dim i As Int16
        i = 0

        For Each mlDG In mlDATAGRIDITEMLIST.Items
            Dim mlFlagView As Label = CType(mlDG.FindControl("lblFlagView"), Label)
            Dim mlItemKeyTxt As Label = CType(mlDG.FindControl("lblItemkey"), Label)
            Dim mlItemDesc As Label = CType(mlDG.FindControl("lblItemName"), Label)
            Dim mlSatuanItem As Label = CType(mlDG.FindControl("lblSatuan"), Label)
            Dim mlSIZE As Label = CType(mlDG.FindControl("lblSize"), Label)
            Dim mlQTY As TextBox = CType(mlDG.FindControl("txtQty"), TextBox)
            Dim mlBalanceAmount As ucInputNumber = CType(mlDG.FindControl("ucBalanceAmount"), ucInputNumber)
            Dim mlDESCDT2 As TextBox = CType(mlDG.FindControl("txtKeterangan"), TextBox)


            Dim drItem As DataRow
            drItem = mlDATATABLE_ITEMLIST.NewRow()
            drItem("FlagView") = mlFlagView.Text
            drItem("ItemKey") = mlItemKeyTxt.Text
            drItem("Description") = mlItemDesc.Text
            drItem("uom") = mlSatuanItem.Text
            drItem("fsize") = mlSIZE.Text
            drItem("Quantity") = mlQTY.Text
            drItem("BalanceAmount") = mlBalanceAmount.Text
            drItem("Keterangan") = mlDESCDT2.Text

            mlDATATABLE_ITEMLIST.Rows.Add(drItem)

        Next


    End Sub

    Protected Sub mlDATAGRIDITEMLIST_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles mlDATAGRIDITEMLIST.ItemCommand
        On Error Resume Next

        If mlI >= 1 Then
            Return
        End If

        Select Case e.CommandName
            Case "Delete"
                SaveitemTemporary()
                mlDATATABLE_ITEMLIST.Rows.RemoveAt(e.Item.ItemIndex)
                mlDATAGRIDITEMLIST.DataSource = mlDATATABLE_ITEMLIST
                mlDATAGRIDITEMLIST.DataBind()

                mlI = mlI + 1
        End Select

    End Sub
    Protected Sub mlDATAGRIDITEMLIST_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles mlDATAGRIDITEMLIST.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim HL As HyperLink = DirectCast(e.Item.FindControl("Hyperlink2"), HyperLink)
            Dim FlagView As Label = DirectCast(e.Item.FindControl("lblFlagView"), Label)

            HL.Visible = IIf(FlagView.Text = "1", True, False)
        End If
    End Sub
    Protected Sub GetMonth()
        Dim mlSQLPeriode As String, mlDt_Periode As New DataTable
        Dim mlPeriodeCount As Integer

        mlSQLPeriode = " Select * from dbo.fnPeriodeMR()"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLPeriode, "AD", "AD")
        mlDt_Periode.Load(mlRSTEMP)

        ddlPeriodeMR.Items.Clear()

        mlPeriodeCount = mlDt_Periode.Rows.Count - 1

        For i As Integer = 0 To mlPeriodeCount
            ddlPeriodeMR.Items.Add(New ListItem(mlDt_Periode.Rows(i)("PeriodeMR").ToString(), mlDt_Periode.Rows(i)("IDPeriode").ToString()))

            If ddlEntity.Text = "IFS" And mlDt_Periode.Rows(i)("IDPeriode").ToString() = "09" Then
                Exit For
            End If
        Next

        For n As Integer = 0 To ddlPeriodeMR.Items.Count - 1
            If ddlPeriodeMR.Items(n).Value = DateTime.Now.Month.ToString("00") Then
                ddlPeriodeMR.SelectedIndex = n
                Exit For
            End If
        Next

    End Sub

    Protected Sub imbSubMit_Click(sender As Object, e As ImageClickEventArgs) Handles imbSubMit.Click
        mlMESSAGE.Text = ""
        SaveitemTemporary()
        RetrieveItems()
    End Sub

    Protected Sub imbRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imbRefresh.Click
        mlSQLTEMP = "SELECT SiteCardID,SiteCardName FROM OP_USER_SITE WHERE SiteCardID LIKE  '" & Trim(mpSITECARD.Text) & "' AND EntityID = '" & Trim(ddlEntity.Text) & "' "
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
        If mlRSTEMP.HasRows Then
            mlRSTEMP.Read()
            mpSITEDESC.Text = mlRSTEMP("SiteCardName") & ""
            SiteLocation()
        End If
        mlOBJGS.CloseFile(mlRSTEMP)
    End Sub

    Protected Sub mlDGDATALIST_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles mlDGDATALIST.ItemCommand
        mlKEY = e.CommandArgument
        mlKEY2 = mlOBJGF.GetStringAtPosition(mlKEY, 1, "#")
        mlKEY3 = mlOBJGF.GetStringAtPosition(mlKEY, 2, "#")
        mlKEY = mlOBJGF.GetStringAtPosition(mlKEY, 0, "#")

        Dim mlTEMPLATEID As String
        mlTEMPLATEID = Trim(mlOBJGF.GetStringAtPosition(mpMR_TEMPLATE.Text, 0, "-"))
        hlLookUp.NavigateUrl = "javascript:OpenWinLookUpItem('" + txtItemNo.ClientID + "','" + txtItemName.ClientID + "','" + hdnItemNo.ClientID + "','" + hdnItemName.ClientID + "','" + mlTEMPLATEID + "','AccMnt')"

        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
                ClearFields()
                LoadCombo()
                RetrieveFields()
                FillDetail(mlKEY)
                pnTOOLBAR.Visible = False
                pnlDATALIST.Visible = False
                pnlTemplate.Visible = True
                pnlFILL.Visible = True
                pnlGRID.Visible = True
                'VisiblePrice()

            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                EditRecord()
                RetrieveFields()
                FillDetail(mlKEY)
                pnlDATALIST.Visible = False
                pnlTemplate.Visible = True
                pnlFILL.Visible = True
                pnlGRID.Visible = True
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub
    Protected Sub mlDGDATALIST_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles mlDGDATALIST.ItemDataBound

    End Sub
End Class
