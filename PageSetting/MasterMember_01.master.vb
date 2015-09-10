Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO


Partial Class MasterMember_01
    Inherits System.Web.UI.MasterPage

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Dim mlREADERM As OleDb.OleDbDataReader
    Dim mlSQLM As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mlOBJGS.Main()
            If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
            mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

            AuthorizeMenu()

            If Session("mgUSERID") <> "" Then
                If Not Page.IsPostBack Then
                    RetrieveFieldsDetail()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub RetrieveFieldsDetail()
        mlSQLM = " SELECT '01' AS ColInd, '|| ADMINISTRATION ||' AS Description,'' AS ExecFile,'' AS SpecialParameter UNION ALL " & _
                " SELECT '02' AS ColInd, Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & Session("mgGROUPMENU") & "')" & _
                " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'AD%' " & _
                "" & _
                " UNION ALL SELECT '03' AS ColInd,'^','','' " & _
                " UNION ALL  SELECT '04'AS ColInd,'|| MASTER DATA ||','','' UNION ALL " & _
                " SELECT '05',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & Session("mgGROUPMENU") & "')" & _
                " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'MS%' " & _
                "" & _
                " UNION ALL SELECT '06' AS ColInd,'^','','' " & _
                " UNION ALL  SELECT '07' AS ColInd,'|| TRANSACTION ||','','' UNION ALL " & _
                " SELECT '08', Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & Session("mgGROUPMENU") & "')" & _
                " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'TR%' " & _
                "" & _
                " UNION ALL SELECT '09' AS ColInd,'^','','' " & _
                " UNION ALL  SELECT '10' AS ColInd,'|| REPORT ||','','' UNION ALL " & _
                " SELECT '11',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & Session("mgGROUPMENU") & "')" & _
                " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'RP%' " & _
                "" & _
                " ORDER BY ColInd,Description" & _
                ""
        mlREADERM = mlOBJGS.DbRecordset(mlSQLM)
        mlMENUGRID.DataSource = mlREADERM
        mlMENUGRID.DataBind()
        mlOBJGS.CloseFile(mlREADERM)
    End Sub

    Sub AuthorizeMenu()
        Dim mlVALIDATE As String
        Dim mlVALIDATE2 As String
        Dim mlSQLVAL As String
        Dim mlRSVALIDATE As OleDbDataReader
        Dim mlSPECIALPARAM As String

        mlVALIDATE = Path.GetFileName(Request.Path)
        mlVALIDATE2 = Request.Url.AbsoluteUri
        If mlOBJGF.IsNone(mlVALIDATE2) = False Then
            mlVALIDATE2 = "?" & mlOBJGF.GetStringAtPosition(mlVALIDATE2, 1, "?")
        End If

        mlSQLVAL = "SELECT GM.GroupMenu, MN.MenuID, MN.Description, MN.ExecFile, MN.SpecialParameter FROM AD_MENU MN, AD_GROUPMENU GM " & _
                    " WHERE MN.MenuID = GM.MenuID AND GM.GroupMenu ='" & Session("mgGROUPMENU") & "' AND MN.EXECFILE LIKE '%" & mlVALIDATE & "%'"

        If InStr(mlVALIDATE2, "FP") <> 0 Then
            mlSQLVAL = "SELECT GM.GroupMenu, MN.MenuID, MN.Description, MN.ExecFile, MN.SpecialParameter FROM AD_MENU MN, AD_GROUPMENU GM " & _
                    " WHERE MN.MenuID = GM.MenuID AND GM.GroupMenu ='" & Session("mgGROUPMENU") & "' AND MN.EXECFILE LIKE '%" & mlVALIDATE & "%'" & _
                    " AND SpecialParameter LIKE '%" & mlVALIDATE2 & "%'"
        End If
        mlRSVALIDATE = mlOBJGS.DbRecordset(mlSQLVAL)
        If mlRSVALIDATE.HasRows = False Then
            mlVALIDATE = "31"
            mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
            Response.Write("NotAuthorize1")
            Response.Write("<br>")
            mlOBJGS.XMtoLog(1, "AD", "MenuAuthorize", "Not Authorize " & Request.Url.AbsoluteUri, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
        Else
            If InStr(mlVALIDATE2, "FP") <> 0 Then
                If mlRSVALIDATE.HasRows = True Then
                    mlRSVALIDATE.Read()
                    mlSPECIALPARAM = mlRSVALIDATE("SpecialParameter") & ""
                    If InStr(mlVALIDATE2, mlSPECIALPARAM) = 0 Then
                        mlVALIDATE = "31"
                        mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
                        Response.Write("NotAuthorize2")
                        Response.Write("<br>")
                        mlOBJGS.XMtoLog("1", "AD", "MenuAuthorize", "Not Authorize " & Request.Url.AbsoluteUri, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
                        Response.Write(mlVALIDATE2 & "<br>" & mlSPECIALPARAM)
                        Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
                    End If
                End If
            End If
        End If

    End Sub
End Class

