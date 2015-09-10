Imports System
Imports System.Data
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.OleDb
Imports System.IO
Imports System.Xml


Partial Class nv_script_mr_template
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Script Sunfish MR Frontliner List V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Script Sunfish MR Frontliner List V2.00"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")


        If Page.IsPostBack = False Then
            LoadComboData()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ad_menu", "Menu", "")
        Else
        End If
    End Sub

    Sub LoadComboData()
        mpMENU.Items.Clear()
        mpMENU.Items.Add("MR Frontliner")


    End Sub

    Protected Sub btSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSUBMIT.Click
        Dim mlSTATUS As Boolean
        Select Case mpMENU.Text
            Case "MR Frontliner"
                mlSTATUS = MR_Frontliner()
        End Select

        If mlSTATUS = True Then
            mlMESSAGE.Text = "Success"
        Else
            mlMESSAGE.Text = "Fail"
        End If
    End Sub

    Function MR_Frontliner() As Boolean
        Dim mlPATH As String
        Dim mlXMLTEXTREADER As XmlTextReader

        'mlPATH = "http://10.62.0.43/iss/a2a/?key=1e59b_c4f5798d_845cc9"
        'mlPATH = "C:\Users\sugianto.liau\Downloads\dat6038_65127399.xml"
        mlPATH = "C:\Users\sugianto.liau\Downloads\a.xml"

        mlXMLTEXTREADER = New XmlTextReader(mlPATH)
        Do While (mlXMLTEXTREADER.Read())
            ' Do some work here on the data.
            Select Case mlXMLTEXTREADER.NodeType
                Case XmlNodeType.Element 'Display beginning of element.
                    Console.Write("<" + mlXMLTEXTREADER.Name)
                    Console.WriteLine(">")
                Case XmlNodeType.Text 'Display the text in each element.
                    Console.WriteLine(mlXMLTEXTREADER.Value)
                Case XmlNodeType.EndElement 'Display end of element.
                    Console.Write("</" + mlXMLTEXTREADER.Name)
                    Console.WriteLine(">")
            End Select

            Console.WriteLine(mlXMLTEXTREADER.Name)
        Loop



    End Function
End Class
