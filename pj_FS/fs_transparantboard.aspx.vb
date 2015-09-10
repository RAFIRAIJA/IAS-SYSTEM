Imports System.IO
Imports IAS.Core.CSCode
Partial Class fs_transparantboard
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlFILEPATH As String
    Dim mlFILEPATH2 As String


    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Transparant Board V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Transparant Board V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlFILEPATH = "../App_Data/fs_file/board/TransparantBoard.txt"
        If Page.IsPostBack = False Then
            DisableCancel()
            RetrieveFieldsDetail()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
        Else
        End If

    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
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
        DisableCancel()
    End Sub

    Sub DeleteRecord()
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        LoadComboData()

    End Sub

    Sub EditRecord()
    End Sub

    Private Sub EnableCancel()
        btNewRecord.Visible = False
        btSaveRecord.Visible = True
        pnFILL.Visible = True
    End Sub

    Private Sub DisableCancel()
        btNewRecord.Visible = True
        btSaveRecord.Visible = False
        pnFILL.Visible = False
    End Sub

    Sub ClearFields()
        txDESCRIPTION.Text = ""
    End Sub

    Sub LoadComboData()
    End Sub


    Function ValidateForm() As String
        ValidateForm = ""
    End Function


    Sub RetrieveFieldsDetail()
        Dim mlRS As System.IO.FileStream
        Dim mlSR As System.IO.StreamReader

        Try
            lbTEXT.Text = ""
            mlFILEPATH2 = Server.MapPath(mlFILEPATH)
            mlRS = File.Open(mlFILEPATH2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            mlSR = New StreamReader(mlRS)

            Dim mlLINE As String
            Dim mlALLLINE As String

            mlALLLINE = ""
            mlLINE = mlSR.ReadLine
            Do While (Not mlLINE Is Nothing)
                mlALLLINE = mlALLLINE & IIf(mlLINE = "", "<br>", "<br>" & mlLINE)
                mlLINE = mlSR.ReadLine
            Loop
            mlSR.Close()

            lbTEXT.Text = mlALLLINE

        Catch ex As Exception
            Response.Write(Err.Description)

        End Try
    End Sub

    Sub SaveRecord()
        Try
            mlFILEPATH2 = Server.MapPath(mlFILEPATH)
            Dim mlSW As System.IO.StreamWriter

            mlSW = File.AppendText(mlFILEPATH2)
            mlSW.WriteLine(vbNewLine & "<br><hr><br>" & txDESCRIPTION.Text & "<br> By:" & Session("mgUSERID") & " - " & Now())
            'mlSW.WriteLine(lbTEXT.Text & vbNewLine & "<br><hr><br>" & txDESCRIPTION.Text & "<br> By:" & Session("mgUSERID") & " - " & Now())
            mlSW.Flush()
            mlSW.Close()
            mlSW.Dispose()

            ClearFields()
            DisableCancel()
            RetrieveFieldsDetail()


        Catch ex As Exception

        End Try

    End Sub


    'Sub SaveRecord()
    '    Try
    '        mlFILEPATH2 = Server.MapPath(mlFILEPATH)
    '        Dim mlSW As New System.IO.StreamWriter(mlFILEPATH2)

    '        mlSW = File.AppendText("dfd")
    '        'mlSW.WriteLine(lbTEXT.Text & vbNewLine & "<br><hr><br>" & txDESCRIPTION.Text & "<br> By:" & Session("mgUSERID") & " - " & Now())
    '        mlSW.Close()
    '        mlSW.Dispose()

    '        ClearFields()
    '        DisableCancel()
    '        RetrieveFieldsDetail()


    '    Catch ex As Exception

    '    End Try

    'End Sub


End Class
