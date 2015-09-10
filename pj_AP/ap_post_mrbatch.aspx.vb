Imports System.Data
Imports System.Data.OleDb
Imports System.Web
Imports System.Drawing
Imports IAS.Core.CSCode
Partial Class ap_post_mrbatch
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal
    Dim mlOBJPJ_UT As New IASClass_Local_ut.ucmLOCAL_ut

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlSQLUSER As String
    Dim mlSQLUSERSITE As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlFUNCTIONPARAMETER As String

    Public mlBATCHINV As String
    Public mlBATCHTOTALPOINT As Double
    Public mlBATCHTOTALAMOUNT As Double


    Dim mlKEY As String
    Dim mlFUNCTIONPARAMETERORI As String
    Dim mlRECORDSTATUS As String
    Dim mlCHECKID As String
    Dim mlMEMBERGROUP As String
    Dim mlSTOCKISTGROUP As String
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlUSERLEVEL As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Posting Material Requisition (MR) V2.05"
        mlTITLE.Text = "Posting Material Requisition V2.05"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlSTOCKISTGROUP = System.Configuration.ConfigurationManager.AppSettings("mgSTOCKISTGROUPMENU")


        mlFUNCTIONPARAMETER = Request("mpFP")

        If mlFUNCTIONPARAMETER <> "" Then
            If mlFUNCTIONPARAMETERORI = "" Then mlFUNCTIONPARAMETERORI = mlFUNCTIONPARAMETER
            mlSYSCODE.Value = mlFUNCTIONPARAMETER
        End If

        mlFUNCTIONPARAMETERORI = mlSYSCODE.Value
        mlFUNCTIONPARAMETER = "" & mlSYSCODE.Value


        mlSQLUSER = ""
        Select Case mlFUNCTIONPARAMETERORI
            Case "2", "3"
                mlTITLE.Text = mlTITLE.Text & " ( Level " & mlFUNCTIONPARAMETER & ")"
                mlSTATUS.Items.Clear()
                mlSTATUS.Items.Add("Wait" & CInt(mlFUNCTIONPARAMETER) - 1)
                mlUPDATESTATUS.Items.Clear()
                mlUPDATESTATUS.Items.Add("Wait" & CInt(mlFUNCTIONPARAMETER))
                mlUPDATESTATUS.Items.Add("Void")

        End Select

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_post_mrbatch", "Post MR", "")

            DisableCancel()
            LoadComboData()
            mlMESSAGE.Text = "Fill the Data then Click Search Button to Get Posting Document"
        End If
    End Sub

    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                SearchRecord()

            Case "CheckedRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        Try
            If pnFILL.Visible = True Then
                SaveRecord()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        RetrieveFields()
        LoadComboData()
    End Sub

    Public Sub RetrieveFields()
        ClearFields()
        DisableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)

        Try

        
            mlREADER = mlOBJGS.DbRecordset(mpSQL, "PB", "ISSP3")
            mlDATAGRID.DataSource = mlREADER
            mlDATAGRID.DataBind()


            If mlREADER.HasRows Then
                btSaveRecord.Visible = True
            End If


        Catch ex As Exception


        End Try
    End Sub

    Sub NewRecord()
        ClearFields()
        EnableCancel()
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
        'mlOBJGS.ExecuteQuery(mlSQL)
        RetrieveFields()
    End Sub

    Sub EditRecord()
        EnableCancel()
    End Sub


    Private Sub EnableCancel()
        'pnFILL.Visible = True
    End Sub

    Private Sub DisableCancel()
        btSaveRecord.Visible = False
        'pnFILL.Visible = False
    End Sub

    Sub ClearFields()
        mlMESSAGE.Text = ""
    End Sub

    Sub LoadComboData()
        mlDATEFROM.Text = mlCURRENTDATE
        mlDATETO.Text = mlCURRENTDATE

        mlRECORDPERPAGE.Items.Clear()
        mlRECORDPERPAGE.Items.Add("10")
        mlRECORDPERPAGE.Items.Add("20")
        mlRECORDPERPAGE.Items.Add("50")
        mlRECORDPERPAGE.Items.Add("100")
        mlRECORDPERPAGE.Items.Add("200")

        ddCHECK.Items.Clear()
        ddCHECK.Items.Add("Pilih")
        ddCHECK.Items.Add("5")
        ddCHECK.Items.Add("10")
        ddCHECK.Items.Add("25")
        ddCHECK.Items.Add("50")
        ddCHECK.Items.Add("100")
        ddCHECK.Items.Add("250")
        ddCHECK.Items.Add("500")
        ddCHECK.Items.Add("1000")
        ddCHECK.Items.Add("1500")
        ddCHECK.Items.Add("2000")
        ddCHECK.Items.Add("UnCheck")

        ddBATCH.Items.Add("1 Transaksi per Batch")
        ddBATCH.Items.Add("5 Transaksi per Batch")
        ddBATCH.Items.Add("10 Transaksi per Batch")
        ddBATCH.Items.Add("25 Transaksi per Batch")
        ddBATCH.Items.Add("50 Transaksi per Batch")
        ddBATCH.Items.Add("100 Transaksi per Batch")

        ddEntity.Items.Clear()
        ddEntity.Items.Add("Choose")
        mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='ISS_Entity'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            ddEntity.Items.Add(Trim(mlRSTEMP("LinCode")))
        End While

        ddType.Items.Clear()
        ddType.Items.Add("No Type")
      
    End Sub

    Protected Sub mlCHECKALL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlCHECKALL.CheckedChanged
        Dim mlDG As DataGridItem

        If mlCHECKALL.Checked Then
            For Each mlDG In mlDATAGRID.Items
                Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)
                mlBOX.Checked = True
            Next mlDG
        Else
            For Each mlDG In mlDATAGRID.Items
                Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)
                mlBOX.Checked = False
            Next mlDG
        End If
    End Sub

    Protected Sub ddCHECK_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddCHECK.TextChanged
        Dim mlROWAMOUNT As Integer
        Dim mlCOUNTER As Integer
        Dim mlDG As DataGridItem

        If ddCHECK.Text = "Pilih" Then
            Exit Sub
        End If

        'UnCheckbox
        For Each mlDG In mlDATAGRID.Items
            Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)

            If mlBOX.Checked = False Then
                Exit For
            Else
                mlBOX.Checked = False
            End If
        Next mlDG

        'Start Checkbox
        mlROWAMOUNT = ddCHECK.Text
        mlCOUNTER = 0

        For Each mlDG In mlDATAGRID.Items
            Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)
            mlBOX.Checked = True

            mlCOUNTER = mlCOUNTER + 1
            If mlCOUNTER >= mlROWAMOUNT Then
                Exit For
            End If
        Next mlDG
    End Sub

    Sub SearchRecord()
        Dim mlSQL As String
        Dim mlFUNCTIONPARAMETER2 As String

        Try
            mlSQL = ""

            If mlDATEFROM.Text <> "" And mlDATETO.Text <> "" Then
                'mlSQL = mlSQL & " (HR.DocDate > = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDATEFROM.Text, "/")) & "' AND HR.DocDate < = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDATETO.Text, "/")) & "')"
                mlSQL = mlSQL & " (HR.DocDate > = '" & mlOBJGF.FormatDate(mlDATEFROM.Text) & "' AND HR.DocDate < = '" & mlOBJGF.FormatDate(mlDATETO.Text) & "')"
            End If

            If mlDOCUMENTNO1.Text <> "" And mlDOCUMENTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " DocNo >= '" & Trim(mlDOCUMENTNO1.Text) & "' AND DocNo <= '" & Trim(mlDOCUMENTNO2.Text) & "'"
            End If

            If mpPERIOD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " BVMonth LIKE '" & Trim(mpPERIOD.Text) & "' "
            End If

            If mlUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " PostingUserID1 LIKE '" & Trim(mlUSERID.Text) & "' "
            End If

            If ddEntity.Text <> "Choose" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " EntityID = '" & Trim(ddEntity.Text) & "' "
            End If

            If ddType.Text <> "" Then
                'mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & Trim(ddTYPE.Text) & "' "
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & Trim(ddType.SelectedValue) & "' "

            End If

            'If mlSTATUS.Text <> "" Then
            mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " RecordStatus LIKE '" & mlSTATUS.Text & "' "
            'End If


            mlFUNCTIONPARAMETER2 = mlFUNCTIONPARAMETER
            'mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & mlFUNCTIONPARAMETER2 & "'"

            'Remarked (2015-06-05) buat ganti Sitecard dengan JobNo n JobTaskNo
            mlSQLTEMP = "SELECT SiteCardID FROM OP_USER_SITE WHERE SiteCardID='ALL' AND UserID = '" & Session("mgUSERID") & "' AND UserLevel > = '" & mlFUNCTIONPARAMETERORI & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows = False Then
                ' Diremark (2015-06-04) dl...krn Sitecard udah diganti dengan Job n JobTaskNo 
                'mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " SiteCardID IN (SELECT SiteCardID FROM OP_USER_SITE WHERE UserID = '" & Session("mgUSERID") & "' AND UserLevel > = '" & mlFUNCTIONPARAMETERORI & "')"

                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " JobNo+JobTaskNo IN (SELECT JobNo+JobTaskNo FROM OP_USER_SITE WHERE UserID = '" & Session("mgUSERID") & "' AND UserLevel > = '" & mlFUNCTIONPARAMETERORI & "')"

            End If


            If mlOBJGF.IsNone(mlSQL) = False Then
                Try
                    Dim mlQUERY As String

                    mlQUERY = "SELECT DocNo,DocDate,SiteCardID,SiteCardName,Do_Address as Delivery_Address,PercentageMR,BVMonth,TotalAmount,RecordStatus, " & vbCrLf
                    mlQUERY += " PostingUserID1,PostingName1,PostingDate1,PostingUserID2,PostingName2,PostingDate2,PostingUserID3,PostingName3,PostingDate3, JobNo,JobTaskNo" & vbCrLf
                    mlQUERY += " FROM AP_MR_REQUESTHR HR WHERE " & mlSQL & mlSQLUSER & " ORDER BY DocNo"
                    RetrieveFieldsDetail(mlQUERY)

                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

    Public Function mpCrossPageField() As String
        Return mlFUNCTIONPARAMETERORI & "#" & mlBATCHINV & "#" & mlBATCHTOTALPOINT & "#" & mlBATCHTOTALAMOUNT
    End Function

    Sub SaveRecord()
        Dim mlDG As DataGridItem
        Dim mlSAVESTATUS As Boolean = False

        mlBATCHINV = ""
        mlBATCHTOTALPOINT = "0"
        mlBATCHTOTALAMOUNT = "0"
        For Each mlDG In mlDATAGRID.Items
            Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)

            If mlBOX.Checked Then
                mlSAVESTATUS = True
                mlBATCHINV = mlBATCHINV & IIf(mlBATCHINV = "", "", ",") & Trim(mlDG.Cells("3").Text)

            End If
        Next mlDG

        If mlSAVESTATUS Then
            'mpCrossPageField()
            Sql_1(mlUPDATESTATUS.Text, mlBATCHINV)
        End If
        SearchRecord()
        DisableCancel()
    End Sub

    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpBATCHINVNO As String)
        Dim mlRECSTATUS As String
        Dim mlI As Integer
        Dim mlLOOP As Boolean
        Dim mlDOCNO3 As String
        Dim mlLINEPERBATCH As Integer

        CekSession()

        mlLINEPERBATCH = mlOBJGF.GetStringAtPosition(ddBATCH.Text, "0", "T")
        Try
            Select Case mpSTATUS
                Case "Post", "Void"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.ISS_MR_MREntry_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"), "")

            End Select

            Select Case mpSTATUS
                Case "Wait1", "Wait2", "Wait3"
                    mlSQL = mlSQL & mlOBJPJ.ISS_MR_MREntry_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"), "")

                    mlRECSTATUS = "Wait" & mlFUNCTIONPARAMETER
                    If mlRECSTATUS = "Wait3" Then
                        mlRECSTATUS = "New"
                    End If

                    mlI = 0
                    mlLOOP = True
                    Do While mlLOOP
                        mlDOCNO3 = mlOBJGF.GetStringAtPosition(mpBATCHINVNO, mlI, ",")
                        mlI = mlI + 1
                        If mlDOCNO3 <> "" Then
                            mlSQL = mlSQL & "UPDATE AP_MR_REQUESTHR SET PostingUserID" & mlFUNCTIONPARAMETER & "= '" & Session("mgUSERID") & "'," & _
                                    " PostingName" & mlFUNCTIONPARAMETER & "= '" & Session("mgNAME") & "'," & _
                                    " PostingDate" & mlFUNCTIONPARAMETER & "= '" & mlOBJGF.FormatDate(Now) & "'," & _
                                    " RecordStatus = '" & mlRECSTATUS & "'" & _
                                    " WHERE DocNo = '" & Trim(mlDOCNO3) & "';"
                        Else
                            mlLOOP = False
                        End If

                        If mlI >= mlLINEPERBATCH Then
                            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
                            mlSQL = ""
                        End If



                        If mlDOCNO3 <> "" Then
                            Dim mlSQLTEMP2 As String
                            Dim mlRSTEMP2 As OleDbDataReader
                            Dim mlEMAIL_STATUS As String
                            Dim mlEMAIL_TO As String
                            Dim mlEMAIL_SUBJECT As String
                            Dim mlEMAIL_BODY As String
                            Dim mlLINKSERVER1 As String
                            Dim mlBCC As String


                            Try
                                mlBCC = "iassystem_log@iss.co.id"

                                mlSQLTEMP2 = "SELECT * FROM AP_MR_REQUESTHR WHERE DocNo = '" & Trim(mlDOCNO3) & "' AND RecordStatus = '" & mlRECSTATUS & "'"
                                mlRSTEMP2 = mlOBJGS.DbRecordset(mlSQLTEMP2, "PB", "ISSP3")
                                If mlRSTEMP2.HasRows Then
                                    mlRSTEMP2.Read()

                                    mlEMAIL_TO = ""
                                    mlLINKSERVER1 = System.Configuration.ConfigurationManager.AppSettings("mgLINKEDSERVER1")


                                    mlSQLTEMP = "SELECT EmailAddr FROM " & mlLINKSERVER1 & "AD_USERPROFILE  WHERE UserID IN " & _
                                                " (SELECT UserID FROM OP_USER_SITE WHERE SiteCardID='" & Trim(mlRSTEMP2("SiteCardID")) & "') "
                                    mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                                    While mlRSTEMP.Read
                                        mlEMAIL_TO = mlEMAIL_TO & IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", ",") & mlRSTEMP("EmailAddr") & ""
                                    End While


                                    Dim mlRECSTATUSDESC As String
                                    mlRECSTATUSDESC = ""
                                    Select Case mlRECSTATUS
                                        Case "Wait1"
                                            mlRECSTATUSDESC = "Permintaan Baru, Menunggu Proses Review"
                                        Case "Wait2"
                                            mlRECSTATUSDESC = "Selesai Review, Menunggu Proses Authorize"
                                        Case "New"
                                            mlRECSTATUSDESC = "Selesai Authorize, Menunggu Proses Procurement"

                                            'Remarked by Rafi (2014-11-27) , supaya bisa di groupkan email Proc ke masing2 PIC Entity yg ada di login user
                                            'mlSQLTEMP = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID='MR_PROC_EMAIL'"
                                            mlSQLTEMP = " SELECT b.UserID,b.EmailAddr as Description " & _
                                                        " FROM XM_UNIVERSALLOOKUPLIN a " & _
                                                        " left join ADM_ISS.dbo.AD_USERPROFILE b " & _
                                                        "	on a.Description = b.UserID  " & _
                                                        " WHERE UniversalID='MR_PROC_EMAIL_PURCHASE' and b.UserID is not null and isnull(b.EmailAddr,'') <> '' " & _
                                                        " AND A.AdditionalDescription1 = '" + ddEntity.SelectedItem.Text + "' "

                                            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
                                            While mlRSTEMP.Read
                                                mlEMAIL_TO = mlEMAIL_TO & IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", ",") & mlRSTEMP("Description") & ""
                                            End While
                                            mlSQLTEMP = ""
                                            mlEMAIL_TO = mlEMAIL_TO
                                    End Select


                                    If mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = False Then
                                        mlEMAIL_SUBJECT = "Posting Permintaan MR untuk " & Trim(mlRSTEMP2("SiteCardID")) & "-" & Trim(mlRSTEMP2("SiteCardName"))
                                        mlEMAIL_BODY = ""
                                        mlEMAIL_BODY = mlEMAIL_BODY & "Dear : " & Session("mgUSERID") & "-" & Session("mgNAME")
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "Terima kasih telah melakukan Posting untuk transaksi permintaan MR "
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br> Berikut ini adalah informasi transaksi yang telah Anda lakukan :"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<table border=0>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "Tanggal	</td><td>:</td><td>" & Now
                                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "Periode MR	</td><td valign=top>:</td><td valign=top>" & mpPERIOD.Text
                                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "Jenis(Transaksi) </td><td>:</td><td>" & Trim(mlRSTEMP2("MRType")) & "-" & Trim(mlRSTEMP2("MRDesciption"))
                                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "No Dokumen  </td><td>:</td><td>" & Trim(mlDOCNO3)
                                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "Lokasi  </td><td>:</td><td>" & Trim(mlRSTEMP2("SiteCardID")) & "-" & Trim(mlRSTEMP2("SiteCardName"))
                                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "Status Transaksi  </td><td>:</td><td>" & mlRECSTATUS & " -> " & mlRECSTATUSDESC
                                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "</table>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br> Terima kasih"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>Hormat Kami,"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br> "
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br><i>Email ini dikirim Otomatis oleh Sistem Komputer, Email ini tidak perlu dibalas/</i>"
                                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><i>This is an automatically generated email by computer system, please do not reply </i>"
                                        mlEMAIL_STATUS = mlOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, "", mlBCC, mlEMAIL_SUBJECT, mlEMAIL_BODY)
                                    End If
                                End If

                            Catch ex As Exception
                                mlSQLTEMP = Err.Description
                            End Try
                        End If
                    Loop

                Case "Void"
            End Select

            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            Response.Write(mlSQL)
            mlSQL = ""

        Catch ex As Exception
            Response.Write(Err.Description)
            mlOBJGS.XMtoLog("MR", "MRRequest", "MRRequest" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

    Protected Sub ddEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddEntity.SelectedIndexChanged
        ddType.Items.Clear()
        ddType.Items.Add(New ListItem("Choose One", ""))
        mlSQLTEMP = "select Description as ID ,AdditionalDescription2 as DATA" & vbCrLf
        mlSQLTEMP += "from XM_UNIVERSALLOOKUPLIN " & vbCrLf
        mlSQLTEMP += "Where UniversalID = 'ISS_Mapping_BranchCode' and AdditionalDescription3 = '" + ddEntity.SelectedItem.Text + "'"
        mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISS")
        While mlRSTEMP.Read
            'ddTYPE.Items.Add(Trim(mlRSTEMP("ParentCode")))
            ddType.Items.Add(New ListItem(mlRSTEMP("DATA"), mlRSTEMP("ID")))
        End While
    End Sub
End Class
