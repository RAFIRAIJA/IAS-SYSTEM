Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.io

Partial Class pj_ut_ut_sendemail_registeruser
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJFS As New IASClass.ucmFileSystem
    Dim mlOBJPJ As New FunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Sent Email to Register User V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Sent Email to Register User V2.00"

        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Trim(Request("mpID"))

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
            pnSEARCHUSERID.Visible = False

            If mlOBJGF.IsNone(mlFUNCTIONPARAMETER) = False Then
                txUSERID.Text = Trim(mlFUNCTIONPARAMETER)
                btUSERID_Click(Nothing, Nothing)
                SaveRecord()
            End If


        Else
        End If


    End Sub

    Sub SentMailforRegisterUser()
        Try


            mlSQL = "SELECT * FROM AD_USERPROFILE WHERE UserID = '" & Trim(txUSERID.Text) & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "AD", "AD")
            If mlREADER.HasRows Then
                mlREADER.Read()

                If mlOBJGF.IsNone(mlREADER("EmailAddr")) = False Then

                    Dim mlOBJPJ_UT As New IASClass_Local_ut.ucmLOCAL_ut
                    Dim mlEMAIL_STATUS As String
                    Dim mlEMAIL_TO As String
                    Dim mlEMAIL_SUBJECT As String
                    Dim mlEMAIL_BODY As String
                    Dim mlRECEIVER As String

                    mlRECEIVER = ""
                    mlEMAIL_TO = ""

                    mlRECEIVER = Trim(txUSERID.Text) & " - " & Trim(txUSERDESC.Text)
                    mlEMAIL_TO = Trim(mlREADER("EmailAddr"))
                    mlEMAIL_TO = IIf(mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = True, "", mlEMAIL_TO & ",")
                    If mlOBJGF.IsNone(Trim(mlEMAIL_TO)) = False Then
                        mlEMAIL_SUBJECT = "" & " Welcome to ISS IAS System"
                        mlEMAIL_BODY = ""
                        mlEMAIL_BODY = mlEMAIL_BODY & "Dear : " & mlRECEIVER
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Welcome to ISS IAS System <br>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "You have been register to our system"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<table border=0>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Url Address  </td><td valign=top>:</td><td valign=top>" & "http://my.iss.co.id  (outside office)"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "or alternate : http://10.62.0.54 (inside office) "
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "UserID	</td><td valign=top>:</td><td valign=top>" & txUSERID.Text
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<tr><td valign=top>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Default Password  </td><td valign=top>:</td><td valign=top>" & "your date of birth (dd/mm/yyyy) <br> Example : 30121970"
                        mlEMAIL_BODY = mlEMAIL_BODY & "</td></tr>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "</table>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "Thank You"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><br><i>Email ini dikirim Otomatis oleh Sistem Komputer, Email ini tidak perlu dibalas/</i>"
                        mlEMAIL_BODY = mlEMAIL_BODY & "<br><i>This is an automatically generated email by computer system, please do not reply </i>"
                        mlEMAIL_STATUS = mlOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, "", "", mlEMAIL_SUBJECT, mlEMAIL_BODY)

                        ClearFields()
                        mlMESSAGE.Text = "Sent email success "

                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

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


    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        If pnFILL.Visible = True Then
            SaveRecord()
        End If
    End Sub

    Sub SaveRecord()
        If txUSERDESC.Text = "" Then Exit Sub
        SentMailforRegisterUser()
    End Sub


    Sub ClearFields()
        txUSERID.Text = ""
        txUSERDESC.Text = ""
    End Sub
End Class
