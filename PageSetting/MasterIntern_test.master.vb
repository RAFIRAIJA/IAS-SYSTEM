Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.io
Imports System.Web.UI.HtmlControls


Partial Class MasterIntern_00_test
    Inherits System.Web.UI.MasterPage

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Public mlREADERM As OleDb.OleDbDataReader
    Dim mlSQLM As String
    Dim mlUSERID As String
    Dim mlGROUPMENU As String

    Dim mlVALIDATE As String
    Dim mlVALIDATE2 As String
    Dim mlSPECIALPARAM As String

    Public mlMENUCODE As String
    Public mlMENUCODETEMP As String
    Public mlMENUID As String
    Public mlMENUINT As Byte
    Public mlFIRST As Boolean
    Public mlCLOSETAG As Boolean
    Public mlMENUFILL As Boolean


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mlOBJGS.Main()
            If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
            mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

            mlUSERID = Session("mgUSERID")
            mlGROUPMENU = Session("mgGROUPMENU")

            'AuthorizeMenu()
            RetrieveFieldsDetail()
            lbTEXT.Text = "<font size=1px>Login ID : " & Session("mgUSERID") & "- " & Session("mgNAME") & ", Current Time : " & Now() & " </font>"

            'If mlUSERID <> "" Then
            '    If Not Page.IsPostBack Then
            '        RetrieveFieldsDetail()
            '    End If
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Sub RetrieveFieldsDetail()
        Try
            mlSQLM = " SELECT '01' AS ColInd, 'Admin' AS Description,'' AS ExecFile,'' AS SpecialParameter UNION ALL " & _
                    " SELECT '02' AS ColInd, Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                    " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & mlGROUPMENU & "')" & _
                    " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'AD%' " & _
                    "" & _
                    " UNION ALL SELECT '03' AS ColInd,'^','','' " & _
                    " UNION ALL  SELECT '04'AS ColInd,'Master Data','','' UNION ALL " & _
                    " SELECT '05',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                    " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & mlGROUPMENU & "')" & _
                    " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'MS%' " & _
                    "" & _
                    " UNION ALL SELECT '06' AS ColInd,'^','','' " & _
                    " UNION ALL  SELECT '07' AS ColInd,'Transaction','','' UNION ALL " & _
                    " SELECT '08', Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                    " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & mlGROUPMENU & "')" & _
                    " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'TR%' " & _
                    "" & _
                    " UNION ALL SELECT '09' AS ColInd,'^','','' " & _
                    " UNION ALL  SELECT '10' AS ColInd,'Posting','','' UNION ALL " & _
                    " SELECT '11',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                    " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & mlGROUPMENU & "')" & _
                    " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'PS%' " & _
                    "" & _
                    " UNION ALL SELECT '12' AS ColInd,'^','','' " & _
                    " UNION ALL  SELECT '13' AS ColInd,'Report','','' UNION ALL " & _
                    " SELECT '14',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                    " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & mlGROUPMENU & "')" & _
                    " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'RP%' " & _
                    "" & _
                    " UNION ALL SELECT '15' AS ColInd,'^','','' " & _
                    " UNION ALL  SELECT '16' AS ColInd,'Utility','','' UNION ALL " & _
                    " SELECT '17',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                    " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & mlGROUPMENU & "')" & _
                    " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'UT%' " & _
                    "" & _
                    " UNION ALL SELECT '18' AS ColInd,'^','','' " & _
                    " UNION ALL  SELECT '19' AS ColInd,'','','' UNION ALL " & _
                    " SELECT '20',Description, ExecFile, SpecialParameter FROM AD_MENU WHERE " & _
                    " MenuID IN (SELECT MenuID FROM AD_GROUPMENU WHERE GroupMenu = '" & mlGROUPMENU & "')" & _
                    " AND ExecFile <> '' AND (SysID='IA') AND MenuTransType LIKE 'BN%' " & _
                    "" & _
                    " ORDER BY ColInd,Description" & _
                    ""
            mlREADERM = mlOBJGS.DbRecordset(mlSQLM, "AD", "AD")
        Catch ex As Exception

        End Try
    End Sub


    Sub AuthorizeMenu()
        Dim mlSQLVAL As String
        Dim mlRSVALIDATE As OleDbDataReader

        Try

            If mlUSERID = "" Then
                mlVALIDATE = "31"
                mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
                Response.Write("Empty UserID ")
                Response.Write("<br>")
                mlOBJGS.XMtoLog(1, "AD", "Empty UserID", "Empty UserID " & Request.Url.AbsoluteUri, "New", mlUSERID, mlOBJGF.FormatDate(Now))
                'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
            ElseIf mlGROUPMENU = "" Then
                mlVALIDATE = "31"
                mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
                Response.Write("Empty GroupMenu")
                Response.Write("<br>")
                mlOBJGS.XMtoLog(1, "AD", "Empty GroupMenu", "Empty GroupMenu " & Request.Url.AbsoluteUri, "New", mlUSERID, mlOBJGF.FormatDate(Now))
                'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
            End If

            mlVALIDATE = Path.GetFileName(Request.Path)
            mlVALIDATE2 = Request.Url.AbsoluteUri
            If mlOBJGF.IsNone(mlVALIDATE2) = False Then
                mlVALIDATE2 = "?" & mlOBJGF.GetStringAtPosition(mlVALIDATE2, 1, "?")
            End If

            mlSQLVAL = "SELECT GM.GroupMenu, MN.MenuID, MN.Description, MN.ExecFile, MN.SpecialParameter FROM AD_MENU MN, AD_GROUPMENU GM " & _
                        " WHERE MN.MenuID = GM.MenuID AND GM.GroupMenu ='" & mlGROUPMENU & "' AND MN.EXECFILE LIKE '%" & mlVALIDATE & "%'"

            If InStr(mlVALIDATE2, "FP") <> 0 Then
                mlSQLVAL = "SELECT GM.GroupMenu, MN.MenuID, MN.Description, MN.ExecFile, MN.SpecialParameter FROM AD_MENU MN, AD_GROUPMENU GM " & _
                        " WHERE MN.MenuID = GM.MenuID AND GM.GroupMenu ='" & mlGROUPMENU & "' AND MN.EXECFILE LIKE '%" & mlVALIDATE & "%'" & _
                        " AND SpecialParameter LIKE '%" & mlVALIDATE2 & "%'"
            End If
            mlRSVALIDATE = mlOBJGS.DbRecordset(mlSQLVAL, "AD", "AD")
            If mlRSVALIDATE.HasRows = False Then
                mlVALIDATE = "32"
                Response.Write("<br>" & mlVALIDATE & "<br>" & mlVALIDATE2)
                mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
                Response.Write("NotAuthorize1")
                Response.Write("<br>")
                mlOBJGS.XMtoLog(1, "AD", "MenuAuthorize", "Not Authorize " & Request.Url.AbsoluteUri, "New", mlUSERID, mlOBJGF.FormatDate(Now))
                'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
            Else
                If InStr(mlVALIDATE2, "FP") <> 0 Then
                    If mlRSVALIDATE.HasRows = True Then
                        mlRSVALIDATE.Read()
                        mlSPECIALPARAM = mlRSVALIDATE("SpecialParameter") & ""
                        If InStr(mlVALIDATE2, mlSPECIALPARAM) = 0 Then
                            mlVALIDATE = "32"
                            Response.Write("<br>" & mlVALIDATE & "<br>" & mlVALIDATE2)
                            mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
                            Response.Write("NotAuthorize2")
                            Response.Write("<br>")
                            mlOBJGS.XMtoLog("1", "AD", "MenuAuthorize", "Not Authorize " & Request.Url.AbsoluteUri, "New", mlUSERID, mlOBJGF.FormatDate(Now))
                            Response.Write(mlVALIDATE2 & "<br>" & mlSPECIALPARAM)
                            'Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

End Class

