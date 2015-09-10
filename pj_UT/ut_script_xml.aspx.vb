Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports System.Xml
Imports system.data.DataView
Imports system.data.DataTable
Imports System.Data.Common

Partial Class pj_hr_hr_script_mr_frontliner
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Public mlOBJGF As New IASClass.ucmGeneralFunction

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlPATH As String
    Dim mlXMLTEXTREADER As XmlTextReader
    Public mlVALUE As String

    Public mlATTRIBUTESCOUNT As Integer
    Dim mlI As Integer
    Dim mlHEADER As Boolean
    Dim mlLOOP As Boolean


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Script XML V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Script XML V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        LoadURL()
        If Page.IsPostBack = False Then
            LoadComboData()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_menu", "Menu", "")
        Else
        End If
    End Sub

    Sub LoadComboData()
        ddMENU.Items.Clear()
        ddMENU.Items.Add("Pilih")
    End Sub

    Sub LoadURL()
        Select Case DDMENU.TEXT
            Case ""
                txURL.Text = ""
        End Select
    End Sub

    Protected Sub btSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSUBMIT.Click
        RetrieveFieldsDetail()
    End Sub
    
    Sub RetrieveFieldsDetail()
        Try
            Dim mlDATASET As New DataSet

            mlPATH = txURL.Text
            If Trim(txURL.Text) = "" Then Exit Sub

            mlDATASET.ReadXml(mlPATH, XmlReadMode.Auto)
            mlDATAGRID.DataSource = Nothing
            mlDATAGRID.DataSource = mlDATASET
            mlDATAGRID.DataBind()

        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub


End Class
