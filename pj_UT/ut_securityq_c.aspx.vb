Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO



Partial Class ad_resetpwd
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlTABLE As String = "XM_USERQ"
    Dim mlKEY As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlFUNCTIONID As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Security Question V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Security Question V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFUNCTIONPARAMETER = Request("mpFP")
        Select Case mlFUNCTIONPARAMETER
            Case "A"
                mlTITLE.Text = mlTITLE.Text & " - for Intern"
            Case "M"
                mlTITLE.Text = mlTITLE.Text & " - for Distributor"
        End Select

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ut_securityq_r", "Reset Pwd with Security Q", "")
        End If
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        SaveRecord()
    End Sub

    Public Sub RetrieveFields()
        DisableCancel()

        mlSQL = "SELECT * FROM XM_USERQ WHERE UserID = '" & Trim(mlKEY) & "' AND GroupID = '" & mlFUNCTIONPARAMETER & "'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        If mlREADER.HasRows Then
            mlREADER.Read()

            mpQ1.Text = mlREADER("Q1") & ""
            mpQ2.Text = mlREADER("Q2") & ""
            mpQ3.Text = mlREADER("Q3") & ""
            mpQ4.Text = mlREADER("Q4") & ""
            mpQ5.Text = mlREADER("Q5") & ""

            mpA1.Text = mlREADER("A1") & ""
            mpA2.Text = mlREADER("A2") & ""
            mpA3.Text = mlREADER("A3") & ""
            mpA4.Text = mlREADER("A4") & ""
            mpA5.Text = mlREADER("A5") & ""
        End If
    End Sub

    Private Sub EnableCancel()
        btSaveRecord.Visible = True
        pnFILL.Visible = True

        mpQ1.Enabled = True
        mpQ2.Enabled = True
        mpQ3.Enabled = True
        mpQ4.Enabled = True
        mpQ5.Enabled = True
        mpA1.Enabled = True
        mpA2.Enabled = True
        mpA3.Enabled = True
        mpA4.Enabled = True
        mpA5.Enabled = True
    End Sub

    Private Sub DisableCancel()
        btSaveRecord.Visible = False
        pnFILL.Visible = False

        mpQ1.Enabled = False
        mpQ2.Enabled = False
        mpQ3.Enabled = False
        mpQ4.Enabled = False
        mpQ5.Enabled = False

        mpA1.Enabled = False
        mpA2.Enabled = False
        mpA3.Enabled = False
        mpA4.Enabled = False
        mpA5.Enabled = False
    End Sub

    Sub ClearFields()
        mpQ1.Text = ""
        mpQ2.Text = ""
        mpQ3.Text = ""
        mpQ4.Text = ""
        mpQ5.Text = ""

        mpA1.Text = ""
        mpA2.Text = ""
        mpA3.Text = ""
        mpA4.Text = ""
        mpA5.Text = ""
    End Sub

    Sub SaveRecord()

        Try
            mlSQL = "SELECT * FROM " & mlTABLE & " WHERE UserID='" & Session("mgUSERID") & "' AND GroupID = '" & mlFUNCTIONPARAMETER & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL)
            If mlREADER.HasRows Then

                mlSQL = "UPDATE " & mlTABLE & " SET UserID = '" & Session("mgUSERID") & "', " & _
                        " Q1 = '" & Trim(mpQ1.Text) & "', A1 = '" & Trim(mpA1.Text) & "', " & _
                        " Q2 = '" & Trim(mpQ2.Text) & "', A2 = '" & Trim(mpA2.Text) & "', " & _
                        " Q3 = '" & Trim(mpQ3.Text) & "', A3 = '" & Trim(mpA3.Text) & "', " & _
                        " Q4 = '" & Trim(mpQ4.Text) & "', A4 = '" & Trim(mpA4.Text) & "', " & _
                        " Q5 = '" & Trim(mpQ5.Text) & "', A5 = '" & Trim(mpA5.Text) & "', " & _
                        " RecUserID = '" & Session("mgUSERID") & "', RecDate = '" & mlOBJGF.FormatDate(Now) & "'" & _
                        " WHERE UserID='" & Session("mgUSERID") & "' AND GroupID='" & mlFUNCTIONPARAMETER & "'"
                mlOBJGS.ExecuteQuery(mlSQL)
                mlMESSAGE.Text = "Security Question Berhasil di Update "
            Else
                mlSQL = "INSERT INTO " & mlTABLE & " (ParentCode,UserID,GroupID,Q1,A1,Q2,A2,Q3,A3,Q4,A4,Q5,A5,RecordStatus,RecUserID,RecDate)" & _
                        " VALUES ('','" & Session("mgUSERID") & "','" & mlFUNCTIONPARAMETER & "'," & _
                        "'" & Trim(mpQ1.Text) & "','" & Trim(mpA1.Text) & "','" & Trim(mpQ2.Text) & "','" & Trim(mpA2.Text) & "'," & _
                        "'" & Trim(mpQ3.Text) & "','" & Trim(mpA3.Text) & "','" & Trim(mpQ4.Text) & "','" & Trim(mpA4.Text) & "'," & _
                        "'" & Trim(mpQ5.Text) & "','" & Trim(mpA5.Text) & "'," & _
                        "'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "'" & _
                        " )"
                mlOBJGS.ExecuteQuery(mlSQL)

                mlMESSAGE.Text = "Security Question sudah di Tambahkan"
                pnFILL.Visible = False
            End If

        Catch ex As Exception
            mlMESSAGE.Text = "Security Question Failed "
        End Try
    End Sub
End Class


