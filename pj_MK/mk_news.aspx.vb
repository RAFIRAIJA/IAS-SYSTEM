Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports IAS.Core.CSCode

Partial Class mk_news
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String

    Dim mlKEY As String
    Dim mlRECORDSTATUS As String
    Dim mlPATHTHUMBS As String
    Dim mlPATHNORMAL As String
    Dim mlROWAMOUNT As Int16
    Dim mlSQLRECORDSTATUS As String = ""
    Dim mlSPTYPE As String
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlDATESEPARATOR As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlSQLPARAM As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "News Maintenance V2.03"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "News Maintenance V2.03"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlROWAMOUNT = System.Configuration.ConfigurationManager.AppSettings("mgROWDEFAULT")
        mlDATESEPARATOR = "-"

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If mlFUNCTIONPARAMETER <> "" Then
            mlSQLPARAM = " AND ParentCode = '" & mlFUNCTIONPARAMETER & "' "
        End If

        Select Case Request("mpSTATUS")
            Case "", "N"
                mlSQLRECORDSTATUS = "WHERE  RecordStatus='New' " & mlSQLPARAM
            Case "D"
                mlSQLRECORDSTATUS = "WHERE  RecordStatus='Del' " & mlSQLPARAM
        End Select

        mlPATHTHUMBS = "../images/news/thumb/"
        mlPATHNORMAL = "../images/news/normal/"
        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "mk_news", "News Maintenance", "")

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
                mlSYSCODE.value = "edit"
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
        ClearFields()
        DisableCancel()

        mlSQL = "SELECT * FROM MK_NEWS WHERE DocNo = '" & Trim(mlKEY) & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        If mlREADER.HasRows Then
            mlREADER.Read()
            mlSYSID.Items.Add(mlREADER("SysID"))
            mlTYPE.Items.Add(mlREADER("Type"))
            mlDOCNO.Text = mlREADER("DocNo") & ""
            mlDOCDATE.Text = mlOBJGF.ConvertDateIntltoIDN(Replace(mlREADER("DocDate"), "-", "/") & "", "/")
            mlDOCDATE1.Text = mlOBJGF.ConvertDateIntltoIDN(Replace(mlREADER("FromDate"), "-", "/") & "", "/")
            mlDOCDATE2.Text = mlOBJGF.ConvertDateIntltoIDN(Replace(mlREADER("TilDate"), "-", "/") & "", "/")
            mlSUBJECT.Text = mlREADER("Subject") & ""
            mlHEADER.Text = mlREADER("Header") & ""
            mlDESC.Text = mlREADER("Description") & ""
            mlDESC2.Text = mlREADER("Description2") & ""
            mlDESC3.Text = mlREADER("Description3") & ""
            mlDESC4.Text = mlREADER("Description4") & ""
            If Not mlOBJGF.IsNone(mlREADER("ImagePath1")) = False Then
                mlIMAGE1.ImageUrl = mlREADER("ImagePath1")
            End If
            If Not mlOBJGF.IsNone(mlREADER("ImagePath2")) = False Then
                mlIMAGE2.ImageUrl = mlREADER("ImagePath2")
            End If
            If Not mlOBJGF.IsNone(mlREADER("ImagePath3")) = False Then
                mlIMAGE3.ImageUrl = mlREADER("ImagePath3")
            End If
            If Not mlOBJGF.IsNone(mlREADER("ImagePath4")) = False Then
                mlIMAGE4.ImageUrl = mlREADER("ImagePath4")
            End If
        End If
    End Sub

    Sub RetrieveFieldsDetail(Optional ByVal mpPARAM As String = "")
        If mpPARAM = "" Then
            mlSQL2 = "SELECT TOP " & mlROWAMOUNT & " * FROM MK_NEWS " & mlSQLRECORDSTATUS & " ORDER BY DocNo Desc"
        Else
            mlSQL2 = Trim(mpPARAM)
        End If
        mlREADER2 = mlOBJGS.DbRecordset(mlSQL2)
        mlDATAGRID.DataSource = mlREADER2
        mlDATAGRID.DataBind()
    End Sub

    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_News(mlSPTYPE, mlKEY)
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

        mlIMAGE1.Visible = False
        mlIMAGE2.Visible = False
        mlIMAGE3.Visible = False
        mlIMAGE4.Visible = False

        mlDOCNO.Text = "--AUTONUMBER--"
        mlDOCNO.Enabled = False
        mlDOCNO.BackColor = Color.Lavender
    End Sub

    Sub CancelOperation()
        Select Case Request("mpSTATUS")
            Case "", "N"
                mlSQLRECORDSTATUS = "WHERE  RecordStatus='New' " & mlSQLPARAM
            Case "D"
                mlSQLRECORDSTATUS = "WHERE  RecordStatus='Del' " & mlSQLPARAM
        End Select

        DisableCancel()
        RetrieveFieldsDetail()
    End Sub

    Sub SearchRecord()
        Dim mlSQL As String = ""
        Dim mlSYSID2 As String
        Dim mlTYPE2 As String

        mlSYSID2 = Trim(mlOBJGF.GetStringAtPosition(mlSYSID.Text, 0, "-"))
        mlTYPE2 = Trim(mlOBJGF.GetStringAtPosition(mlTYPE.Text, 0, "-"))

        If pnFILL.Visible = True Then
            If mlSYSID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " SysID LIKE '%" & mlSYSID2 & "%'"
            End If

            If mlTYPE.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " Type LIKE '%" & mlTYPE2 & "%'"
            End If

            If mlDOCNO.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " DocNo LIKE '%" & mlDOCNO.Text & "%'"
            End If

            If mlDOCDATE.Text <> "" Then

            End If

            If mlSUBJECT.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " Subject LIKE '%" & mlSUBJECT.Text & "%'"
            End If

            If mlHEADER.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " Header LIKE '%" & mlHEADER.Text & "%'"
            End If

            If mlDESC.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " Description LIKE '%" & mlDESC.Text & "%'"
            End If

            If mlDESC2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " Description2 LIKE '%" & mlDESC2.Text & "%'"
            End If

            If mlDESC3.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " Description3 LIKE '%" & mlDESC3.Text & "%'"
            End If

            If mlDESC4.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " Description4 LIKE '%" & mlDESC4.Text & "%'"
            End If

            If Not mlOBJGF.IsNone(mlSQL) Then
                mlSQL = "SELECT * FROM MK_NEWS WHERE " & mlSQL
                RetrieveFieldsDetail(mlSQL)
                pnFILL.Visible = False
            End If

        Else
            EnableCancel()
            ClearFields()
            mlSYSID.Items.Add("")
            mlTYPE.Items.Add("")
            LoadComboData()
            pnFILL.Visible = True
        End If
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
        LoadComboData()
    End Sub

    Sub SaveRecord()

        If Trim(mlDOCDATE.Text) = "" Then
            mlMESSAGE.Text = "Doc Date not allowed empty"
            Exit Sub
        End If

        If Trim(mlDOCDATE1.Text) = "" Then
            mlMESSAGE.Text = "Date (From) not allowed empty"
            Exit Sub
        End If

        If Trim(mlDOCDATE2.Text) = "" Then
            mlMESSAGE.Text = "Date (To) not allowed empty"
            Exit Sub
        End If

        mlSPTYPE = "New"
        If mlSYSCODE.Value = "edit" Then
            mlSPTYPE = "Edit"
        End If

        Try
            mlKEY = mlDOCNO.Text
            Sql_News(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        DisableCancel()
        RetrieveFieldsDetail()
    End Sub


    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True

        mlSYSID.Enabled = True
        mlTYPE.Enabled = True
        mlDOCNO.Enabled = True
        mlDOCDATE.Enabled = True
        mlSUBJECT.Enabled = True
        mlHEADER.Enabled = True
        mlDESC.Enabled = True
        mlDESC2.Enabled = True
        mlDESC3.Enabled = True
        mlDESC4.Enabled = True
        mlIMAGEP1.Enabled = True
        mlIMAGEP2.Enabled = True
        mlIMAGEP3.Enabled = True
        mlIMAGEP4.Enabled = True
    End Sub



    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False


        mlSYSID.Enabled = False
        mlTYPE.Enabled = False
        mlDOCNO.Enabled = False
        mlDOCDATE.Enabled = False
        mlSUBJECT.Enabled = False
        mlHEADER.Enabled = False
        mlDESC.Enabled = False
        mlDESC2.Enabled = False
        mlDESC3.Enabled = False
        mlDESC4.Enabled = False

        mlIMAGEP1.Enabled = False
        mlIMAGEP2.Enabled = False
        mlIMAGEP3.Enabled = False
        mlIMAGEP4.Enabled = False
    End Sub

    Sub ClearFields()
        mlSYSID.Items.Clear()
        mlTYPE.Items.Clear()
        mlDOCNO.Text = ""
        mlDOCDATE.Text = mlCURRENTDATE
        mlSUBJECT.Text = ""
        mlHEADER.Text = ""
        mlDESC.Text = ""
        mlDESC2.Text = ""
        mlDESC3.Text = ""
        mlDESC4.Text = ""
        mlDOCNO.BackColor = Color.White
        mlDOCDATE1.Text = mlCURRENTDATE
        mlDOCDATE2.Text = mlCURRENTDATE
    End Sub

    Sub LoadComboData()
        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'NEWSSYSID'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mlSYSID.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While

        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'NEWSTYPE'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mlTYPE.Items.Add(Trim(mlREADER("LinCode")) & "-" & mlREADER("Description"))
        End While
    End Sub


    Sub Sql_News(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean

        Dim mlIMAGEPATH1 As String = ""
        Dim mlIMAGEPATH2 As String = ""
        Dim mlIMAGEPATH3 As String = ""
        Dim mlIMAGEPATH4 As String = ""
        Dim mlSYSID2 As String
        Dim mlTYPE2 As String
        Dim mlEDIT As String = ""
        Dim mlIMAGEPATH As String = ""


        mlNEWRECORD = False
        mlKEY = mpCODE
        Try
            Select Case mpSTATUS
                Case "Delete", "Edit"
                    mlSQL = "SELECT * FROM MK_NEWS WHERE DocNo = '" & Trim(mlKEY) & "'"
                    mlREADER = mlOBJGS.DbRecordset(mlSQL)
                    While mlREADER.Read

                        Try
                            'mlIMAGEPATH = mlREADER("ImagePath1") & ""
                            'If Not mlOBJGF.IsNone(mlIMAGEPATH) Then
                            '    File.Delete(Server.MapPath(mlIMAGEPATH))
                            'End If

                            'mlIMAGEPATH = mlREADER("ImagePath2") & ""
                            'If Not mlOBJGF.IsNone(mlIMAGEPATH) Then
                            '    File.Delete(Server.MapPath(mlIMAGEPATH))
                            'End If

                            'mlIMAGEPATH = mlREADER("ImagePath3") & ""
                            'If Not mlOBJGF.IsNone(mlIMAGEPATH) Then
                            '    File.Delete(Server.MapPath(mlIMAGEPATH))
                            'End If

                            'mlIMAGEPATH = mlREADER("ImagePath4") & ""
                            'If Not mlOBJGF.IsNone(mlIMAGEPATH) Then
                            '    File.Delete(Server.MapPath(mlIMAGEPATH))
                            'End If

                        Catch ex As Exception
                        End Try
                    End While
            End Select

            Select Case mpSTATUS
                Case "Edit", "Delete"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.MK_News_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select

            Select Case mpSTATUS
                Case "New"
                    mlNEWRECORD = True
                    If mlKEY = "--AUTONUMBER--" Then
                        mlKEY = mlOBJGS.GetModuleCounter("News" & IIf(mlOBJGF.IsNone(mlFUNCTIONPARAMETER) = True, "", "_" & mlFUNCTIONPARAMETER), "000000")
                        mlDOCNO.Text = mlKEY
                    Else
                        mlKEY = Trim(mlDOCNO.Text)
                    End If

                Case "Edit"
                    mlSTATUS = "Edit"
                    mlNEWRECORD = True
                    mlSQL = mlSQL & " DELETE FROM MK_NEWS WHERE DocNo = '" & mlKEY & "';"

                Case "Delete"
                    mlSTATUS = "Delete"
                    mlSQL = mlSQL & " DELETE FROM MK_NEWS WHERE DocNo = '" & mlKEY & "';"
            End Select
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL)
            mlSQL = ""



            mlSYSID2 = Trim(mlOBJGF.GetStringAtPosition(mlSYSID.Text, 0, "-"))
            mlTYPE2 = Trim(mlOBJGF.GetStringAtPosition(mlTYPE.Text, 0, "-"))
            mlSUBJECT.Text = Replace(mlSUBJECT.Text, "'", "")
            mlHEADER.Text = Replace(mlHEADER.Text, "'", "")
            mlDESC.Text = Replace(mlDESC.Text, "'", "")
            mlDESC2.Text = Replace(mlDESC2.Text, "'", "")
            mlDESC3.Text = Replace(mlDESC3.Text, "'", "")
            mlDESC4.Text = Replace(mlDESC4.Text, "'", "")


            If mlNEWRECORD = True Then
                If mlIMAGEP1.HasFile Then
                    mlIMAGEPATH1 = mlPATHTHUMBS & mlIMAGEP1.FileName
                    mlIMAGEPATH1 = mlPATHTHUMBS & mlKEY & "_1." & mlOBJGF.GetStringAtPosition(Right(mlIMAGEP1.FileName, 5), "1", ".")
                    mlIMAGEP1.SaveAs(Server.MapPath(mlIMAGEPATH1))
                Else
                    If mpSTATUS = "Edit" Then
                        mlIMAGEPATH1 = mlIMAGE1.ImageUrl
                    End If
                End If

                If mlIMAGEP2.HasFile Then
                    mlIMAGEPATH2 = mlPATHNORMAL & mlIMAGEP2.FileName
                    mlIMAGEPATH1 = mlPATHTHUMBS & mlKEY & "_2." & mlOBJGF.GetStringAtPosition(Right(mlIMAGEP1.FileName, 5), "1", ".")
                    mlIMAGEP2.SaveAs(Server.MapPath(mlIMAGEPATH2))
                Else
                    If mpSTATUS = "Edit" Then
                        mlIMAGEPATH2 = mlIMAGE2.ImageUrl
                    End If
                End If

                If mlIMAGEP3.HasFile Then
                    mlIMAGEPATH3 = mlPATHNORMAL & mlIMAGEP3.FileName
                    mlIMAGEPATH1 = mlPATHTHUMBS & mlKEY & "_3." & mlOBJGF.GetStringAtPosition(Right(mlIMAGEP1.FileName, 5), "1", ".")
                    mlIMAGEP3.SaveAs(Server.MapPath(mlIMAGEPATH3))
                Else
                    If mpSTATUS = "Edit" Then
                        mlIMAGEPATH3 = mlIMAGE3.ImageUrl
                    End If
                End If

                If mlIMAGEP4.HasFile Then
                    mlIMAGEPATH4 = mlPATHNORMAL & mlIMAGEP4.FileName
                    mlIMAGEPATH1 = mlPATHTHUMBS & mlKEY & "_4." & mlOBJGF.GetStringAtPosition(Right(mlIMAGEP1.FileName, 5), "1", ".")
                    mlIMAGEP4.SaveAs(Server.MapPath(mlIMAGEPATH4))
                Else
                    If mpSTATUS = "Edit" Then
                        mlIMAGEPATH4 = mlIMAGE3.ImageUrl
                    End If
                End If

                mlSQL = ""
                mlSQL = mlSQL & "INSERT INTO MK_NEWS (ParentCode,SysID,Type,DocNo,DocDate,FromDate,TilDate,Subject,Header,Description,Description2,Description3,Description4," & _
                       " ImagePath1,ImagePath2,ImagePath3,ImagePath4,RecordStatus,Recuserid,RecDate) " & _
                       " VALUES ('" & mlFUNCTIONPARAMETER & "', '" & Trim(mlSYSID2) & "', '" & Trim(mlTYPE2) & "', '" & Trim(mlKEY) & "'," & _
                       " '" & mlOBJGF.FormatDate(Trim(mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE.Text, "/")))) & "', " & _
                       " '" & mlOBJGF.FormatDate(Trim(mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE1.Text, "/")))) & "'," & _
                       " '" & mlOBJGF.FormatDate(Trim(mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDOCDATE2.Text, "/")))) & "'," & _
                       " '" & Trim(mlSUBJECT.Text) & "', '" & Trim(mlHEADER.Text) & "'," & _
                       " '" & Trim(mlDESC.Text) & "', '" & Trim(mlDESC2.Text) & "','" & Trim(mlDESC3.Text) & "','" & Trim(mlDESC4.Text) & "'," & _
                       " '" & Trim(mlIMAGEPATH1) & "','" & Trim(mlIMAGEPATH2) & "'," & _
                       " '" & Trim(mlIMAGEPATH3) & "','" & Trim(mlIMAGEPATH4) & "'," & _
                       " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "'" & _
                       " )"
                mlOBJGS.ExecuteQuery(mlSQL)
                mlSQL = ""
            End If

            Select Case mpSTATUS
                Case "New"
                    mlSQL = ""
                    mlSQL = mlSQL & mlOBJPJ.MK_News_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
            End Select
            mlOBJGS.ExecuteQuery(mlSQL)
            mlSQL = ""


        Catch ex As Exception
            mlOBJGS.XMtoLog("NEWS", "MK", "News" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

End Class
