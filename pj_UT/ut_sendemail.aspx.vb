Imports system.data
Imports system.data.DataView
Imports system.data.DataTable
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.Net.Mail

Partial Class utility_ut_sendemail
    Inherits System.Web.UI.Page
    Dim mLOBJPJ_UT As New IASClass_Local_ut.ucmLOCAL_ut


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Email V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Email V2.00"
        txEMAILF.Text = "iassystem@iss.co.id"
        txEMAILF.Enabled = False

    End Sub


    Function Sendmail_2(ByVal mpPARAM As String, ByVal mpTOEMAIL As String, ByVal mpCCEMAIL As String, ByVal mpBCCEMAIL As String, ByVal mpDATE As Date, ByVal mpTITLE As String, ByVal mpBODY As String, ByVal mpDOCNO As String, ByVal mpLOCID As String, ByVal mpMEMBERID As String, ByVal mpBVMONTH As String, ByVal mpTOTALPOINT As Double, ByVal mpTOTALPRICE As Double) As Boolean
        Dim mlTO As String
        Dim mlFROM As String
        Dim mlSUBJECT As String
        Dim mlBODY As String
        Dim mlMAIL As New MailMessage()
        Dim mlSMTP As New SmtpClient()
        Dim mlSMTPID As String
        Dim mlMAILUSERID As String
        Dim mlMAILPASSWORD As String
        Dim mlSMTPPORT As Integer

        mlTO = mpTOEMAIL
        mlSUBJECT = mpTITLE
        mlBODY = ""
        mlFROM = "iassystem@iss.co.id"

        mlSMTPID = "mail.iss.co.id"
        mlSMTPPORT = "25"
        mlMAILUSERID = "iassystem@iss.co.id"
        mlMAILPASSWORD = "pass7526"



        Select Case mpPARAM
            Case "1"
                mlSUBJECT = mlSUBJECT & " my.iss.co.id - Jurnal Transaksi"
                mlBODY = ""
                mlBODY = mlBODY & mpBODY
                mlBODY = mlBODY & "<br><br>"
                mlBODY = mlBODY & "Terima kasih telah melakukan transaksi di my.iss.co.id "
                mlBODY = mlBODY & "<br> Berikut ini adalah informasi transaksi yang telah Anda lakukan :"
                mlBODY = mlBODY & "<br><br>"
                mlBODY = mlBODY & "<table border=0>"
                mlBODY = mlBODY & "<tr><td>"
                mlBODY = mlBODY & "Tanggal	</td><td>:</td><td>" & mpDATE
                mlBODY = mlBODY & "</td></tr>"
                mlBODY = mlBODY & "<tr><td>"
                mlBODY = mlBODY & "Jenis(Transaksi) </td><td>:</td><td>" & mpTITLE
                mlBODY = mlBODY & "</td></tr>"
                mlBODY = mlBODY & "<tr><td>"
                mlBODY = mlBODY & "No Dokumen  </td><td>:</td><td>" & mpDOCNO
                mlBODY = mlBODY & "</td></tr>"
                mlBODY = mlBODY & "<tr><td>"
                mlBODY = mlBODY & "Lokasi  </td><td>:</td><td>" & mpLOCID
                mlBODY = mlBODY & "</td></tr>"
                mlBODY = mlBODY & "<tr><td>"
                mlBODY = mlBODY & "Member ID  </td><td>:</td><td>" & mpMEMBERID
                mlBODY = mlBODY & "</td></tr>"
                mlBODY = mlBODY & "<tr><td>"
                mlBODY = mlBODY & "</table>"
                mlBODY = mlBODY & "<br><br> Terima kasih"
                mlBODY = mlBODY & "<br><br>Hormat Kami,"
                mlBODY = mlBODY & "<br><br>PT. Testing"
                mlBODY = mlBODY & "<br><br><i>This is Computer Generated, No Need to Reply</i>"
        End Select


        Try
            mlMAIL = New MailMessage()
            mlMAIL.From = New MailAddress(mlFROM)
            mlMAIL.To.Add(mlTO)
            mlMAIL.Subject = mlSUBJECT
            mlMAIL.Body = mlBODY
            mlMAIL.IsBodyHtml = True


            'mlSMTP.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
            mlSMTP.Credentials = New Net.NetworkCredential(mlMAILUSERID, mlMAILPASSWORD)

            mlSMTP.Port = mlSMTPPORT
            mlSMTP.Host = mlSMTPID
            'mlSMTP.EnableSsl = True
            mlSMTP.Send(mlMAIL)

        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.Write("<br>")
            Response.Write(Err.Description)
        End Try
    End Function


    Protected Sub btEmail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btEmail.Click
        Sendmail_2("1", txEMAIL.Text, "", "", Now, txSUBJECT.Text, txBODY.Text, "d001", "i01-lokasi", "00000002-mr.t", "201010", "100.2", "190.000")
        mlMESSAGE.Text = "Finish"
    End Sub

    Protected Sub btEmail2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btEmail2.Click
        Dim mlEMAIL_STATUS As String
        Dim mlEMAIL_TO As String
        Dim mlEMAIL_CC As String
        Dim mlEMAIL_BCC As String
        Dim mlEMAIL_SUBJECT As String
        Dim mlEMAIL_BODY As String

        mlEMAIL_TO = txEMAIL.Text
        mlEMAIL_CC = ""
        mlEMAIL_BCC = ""
        mlEMAIL_SUBJECT = txSUBJECT.Text
        mlEMAIL_BODY = txBODY.Text

        mlEMAIL_STATUS = mLOBJPJ_UT.Sendmail_1("1", mlEMAIL_TO, mlEMAIL_CC, mlEMAIL_BCC, mlEMAIL_SUBJECT, mlEMAIL_BODY)
        mlMESSAGE.Text = mlEMAIL_STATUS & " <br> Finish"
    End Sub


End Class
