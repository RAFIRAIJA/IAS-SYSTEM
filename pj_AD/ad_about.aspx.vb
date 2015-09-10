Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.OleDb

Imports IAS.Core.CSCode
Imports IAS.APP.DataAccess.AD


Partial Class ad_about
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New ModuleGeneralSystem
    Dim mlOBJGF As New ModuleGeneralFunction
    Dim mlOBJDBF As New ModuleDBFunction
    Dim mlOBJMI As New ModuleInitialization


    'Dim mlOBJGS As New IASClass.ucmGeneralSystem
    'Dim mlOBJGF As New IASClass.ucmGeneralFunction

    Dim oFnAD As New FunctionAD
    Dim oVarAD As New VariableAD


    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "About System V2.01"
        mlOBJMI.Main()

        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJDBF.mgCOMPANYDEFAULT
        mlOBJDBF.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        If Page.IsPostBack = False Then
            oFnAD.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_about", "about_us", "")
        End If

        oVarAD.mpModule = "AD"
        oVarAD.mpCompanyID = "AD"
        oFnAD.GetRegisterdInfo(oVarAD)
        mlREADER = oVarAD.mlReader
        If mlREADER.HasRows Then
            mlREADER.Read()

            lblINFO1.Text = mlREADER("CompanyID")
            lblINFO2.Text = mlREADER("Description")
            lblINFO3.Text = mlREADER("Address")
            lblINFO4.Text = mlREADER("City") & " - " & mlREADER("Zip")
            lblINFO5.Text = mlREADER("State")
            lblINFO6.Text = mlREADER("Country")
            lblINFO7.Text = "Tel. " & mlREADER("Phone")
            lblINFO8.Text = "Fax. " & mlREADER("Fax")

            lblINFO9.Text = "Current Client License : " & mlREADER("CurClientLic")
            lblINFO10.Text = "Maximum Client License : " & mlREADER("MaxClientLic")

        End If

        mlOBJDBF.CloseFile(mlREADER)



    End Sub
End Class
