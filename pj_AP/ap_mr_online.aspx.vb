Imports System.Data
Imports System.Data.OleDb
Imports System.Web

Partial Class ap_mr_online
    Inherits System.Web.UI.Page
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New FunctionLocal

    Dim mlKEY As String
    Dim mlSQL As String
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlDATAADAPTER As OleDb.OleDbDataAdapter
    Dim mlDATASET As New DataSet
    Dim mlDATATABLE As New DataTable
    Dim mlSQL2 As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQLSOLD As String
    Dim mlREADERSOLD As OleDb.OleDbDataReader
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String

    Dim mlRECORDSTATUS As String
    Dim mlSQLRECORDSTATUS As String
    Dim mlFUNCTIONPARAMETER As String
    Protected mlSQLSTATP As String = "SELECT * FROM WB_DF_RACKDT BC"
    Dim mlI As Byte
    Dim mlTEMPDATA As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Digital File Searching V2.00"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Digital File Searching V2.00"
        mlOBJGS.Main()

        mlFUNCTIONPARAMETER = Request.QueryString("mpFP")
        If mlOBJGF.IsNone(mlFUNCTIONPARAMETER) = True Then mlFUNCTIONPARAMETER = "1"
        mlDATALIST.RepeatColumns = "3"

        Select Case mlFUNCTIONPARAMETER
            Case "1"
                mlTITLE.Text = mlTITLE.Text & " (Digital)"
            Case "2"
                mlTITLE.Text = mlTITLE.Text & " (Fisik)"
        End Select

        If Page.IsPostBack = False Then
            'pnGRID2.Visible = False
            LoadComboData()
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "df_search.aspx.", "DF Searching", "")
        End If
    End Sub



    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        CancelOperation()
    End Sub

    Public Sub RetrieveFields()
        ClearFields()
        DisableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try

            mlDATAADAPTER = mlOBJGS.DbAdapter(mpSQL)
            mlDATASET.Clear()
            mlDATAADAPTER.Fill(mlDATASET)
            mlDATATABLE.Clear()
            mlDATATABLE = mlDATASET.Tables("table")

            mlDATALIST.DataSource = Nothing
            mlDATALIST.DataSource = mlDATATABLE
            mlDATALIST.DataBind()

            mlOBJGS.CloseDataSet(mlDATASET)
            mlOBJGS.CloseDataAdapter(mlDATAADAPTER)

        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
    End Sub

    Sub NewRecord()
        EnableCancel()
    End Sub

    Sub EditRecord()
        RetrieveFields()
        EnableCancel()
    End Sub

    Sub SaveRecord()
    End Sub

    Sub ClearFields()
        mpSEARCHCRITERIA.Text = ""
    End Sub

    Private Sub EnableCancel()
        pnFILL.Visible = True
        pnGRID.Visible = False
        btSearchRecord.Visible = True
        btCancelOperation.Visible = True
    End Sub

    Private Sub DisableCancel()
        pnFILL.Visible = False
        pnGRID.Visible = True
        btSearchRecord.Visible = False
        btSearchRecord.Visible = False
    End Sub

    Sub CancelOperation()
        EnableCancel()
    End Sub

    Sub SearchRecord()
        Dim mlSQLSEARCH2 As String

        mlMESSAGE.Text = ""
        mlSQLSEARCH2 = ""
        mlSQL = ""

        Select Case mpSEARCHCRITERIA.Text
            Case "All"
                'mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " RDT.Status LIKE '" & mpSEARCHCRITERIA.Text & "'"
            Case Else
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " RDT.Type LIKE '" & mpSEARCHCRITERIA.Text & "'"
        End Select

        mlSQLSEARCH2 = "(" & _
                " no_polis LIKE '" & mpSEARCHTEXT.Text & "'" & _
                " OR no_spaj LIKE '" & mpSEARCHTEXT.Text & "' " & _
                " OR nama_tertanggung LIKE '" & mpSEARCHTEXT.Text & "' " & _
                " OR nama_pemegang LIKE '" & mpSEARCHTEXT.Text & "' " & _
                " OR Description LIKE '" & mpSEARCHTEXT.Text & "' " & _
                ")"
        mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & mlSQLSEARCH2


        If Not mlOBJGF.IsNone(mlSQL) Then
            mlSQL = "SELECT RDT.DocNo,RDT.Linno,RDT.Type,RDT.no_polis,RDT.ImagePath3 FROM WB_DF_RACKDT RDT WHERE " & mlSQL
            mlSQLSTATEMENT.Text = mlSQL
            RetrieveFieldsDetail(mlSQLSTATEMENT.Text)
            DisableCancel()
        End If


    End Sub

    Sub LoadComboData()
        mpSEARCHCRITERIA.Items.Clear()
        mpSEARCHCRITERIA.Items.Add("All")
        mlSQL = "SELECT * FROM XM_UNIVERSALLOOKUPLIN WHERE UniversalID = 'DF_FILETYPE'"
        mlREADER = mlOBJGS.DbRecordset(mlSQL)
        While mlREADER.Read
            mpSEARCHCRITERIA.Items.Add(Trim(mlREADER("LinCode")))
        End While
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Protected Sub mlDATALIST_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles mlDATALIST.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "AddToCart"
                Dim mlDL_DOCNO As Label = CType(e.Item.FindControl("obDOCNO"), Label)
                Dim mlDL_LINNO As Label = CType(e.Item.FindControl("obLINNO"), Label)
                Dim mlDL_ITEMKEY As Label = CType(e.Item.FindControl("obITEMKEY"), Label)
                Dim mlDL_TYPE As Label = CType(e.Item.FindControl("obTYPE"), Label)
                Dim mlDL_QTY As TextBox = CType(e.Item.FindControl("obQTY"), TextBox)

                If mlTEMPDATA <> mlDL_DOCNO.Text & mlDL_LINNO.Text Then
                    mlTEMPDATA = mlDL_DOCNO.Text & mlDL_LINNO.Text

                    Dim mlTEMPDATA2 As String
                    If IsNumeric(mlDL_QTY.Text) Then
                        If pnGRID2.Visible = False Then pnGRID2.Visible = True
                        mlTEMPDATA2 = Shopping_AddtoTempSessionCart("P", Session("mgSHOPPINGCART") & "", Session("mgUSERID") & "", mlDL_LINNO.Text, mlDL_DOCNO.Text, mlDL_TYPE.Text, mlDL_QTY.Text, "0", "0")
                        Session("mgSHOPPINGCART") = mlTEMPDATA2
                        mpSHOPCARTDATA.Value = mlTEMPDATA2

                    End If
                End If
        End Select

    End Sub


    Protected Sub mlDATALIST_ItemDataBound(ByVal sender As Object, ByVal e As DataListItemEventArgs) Handles mlDATALIST.ItemDataBound
        'Try
        '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
        '        Dim mlBVLABEL As Label = CType(e.Item.FindControl("obBV"), Label)
        '        If mlBVLABEL.Text <> "" Then
        '            mlBVLABEL.Text = Convert.ToDouble(mlBVLABEL.Text).ToString("n")
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub btCLEARCART_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCLEARCART.Click
        Session("mgSHOPPINGCART") = ""
        mpSHOPCARTDATA.Value = ""
        mlMESSAGE.Text = "Clear Cart"
    End Sub

    Function Shopping_AddtoTempSessionCart(ByVal mpTYPE As String, ByVal mpCURRENTDATA As String, ByVal mpDOCNO As String, ByVal mpLINNO As Integer, ByVal mpITEMKEY As String, ByVal mpITEMDESCRIPTION As String, ByVal mpQTY As String, ByVal mpUNITPOINT As Double, ByVal mpUNITPRICE As Double) As String
        Dim mlTEMPDATA As String
        Dim mlTEMPDATA2 As String
        Dim mlTEMPDATA3 As String
        Dim mlI As Integer
        Dim mlLOOPC As Boolean

        mlI = 0
        mlLOOPC = True
        Shopping_AddtoTempSessionCart = ""

        mlTEMPDATA = mpCURRENTDATA
        mlTEMPDATA2 = ""
        mlTEMPDATA3 = ""

        Do While mlLOOPC
            mlTEMPDATA2 = mlOBJGF.GetStringAtPosition(mlTEMPDATA, mlI, "#")
            mlI = mlI + 1
            If mlTEMPDATA2 <> "" Then
                'If (mlOBJGF.GetStringAtPosition(Trim(mlTEMPDATA2), 0, "|") <> Trim(mpDOCNO)) And (mlOBJGF.GetStringAtPosition(mlTEMPDATA2, 2, "|") <> mpITEMKEY) Then
                If (mlOBJGF.GetStringAtPosition(mlTEMPDATA2, 2, "|") <> mpITEMKEY) Then
                    mlTEMPDATA3 = mlTEMPDATA2
                End If
            Else
                mlLOOPC = False
            End If
        Loop


        Select Case UCase(mpTYPE)
            Case "P"
                mlTEMPDATA3 = mlTEMPDATA3 & IIf(mlOBJGF.IsNone(mlTEMPDATA3) = True, "", "#") & _
                mpDOCNO & "|" & mpLINNO & "|" & mpITEMKEY & "|" & mpITEMDESCRIPTION & "|" & CDbl(mpQTY) & "|" & "0" & "|" & _
                CDbl(mpUNITPOINT) & "|" & CDbl(mpUNITPRICE) & "|" & _
                CDbl(mpUNITPOINT) * CDbl(mpQTY) & "|" & CDbl(mpUNITPRICE) * CDbl(mpQTY)
            Case "M"

        End Select

        Shopping_AddtoTempSessionCart = mlTEMPDATA3
    End Function

    Function CreateTemporaryID(ByVal mpUSERID As String) As String
        Return "Guest" & "_" & mlOBJGF.CurrentBVMonthDate & "_" & mlOBJGF.GetRandomPasswordUsingGUID(6)
    End Function

    Function Shopping_AddtoTempCart(ByVal mpDOCNO As String, ByVal mpITEMKEY As String, ByVal mpITEMDESCRIPTION As String, ByVal mpQTY As String, ByVal mpUNITPOINT As Double, ByVal mpUNITPRICE As Double) As String
        Dim mlSQLINV As String
        Dim mlLINNO As Integer

        Shopping_AddtoTempCart = ""

        mlSQLINV = ""
        mlLINNO = "1"
        mlSQLINV = "DELETE FROM AR_INVDT WHERE Itemkey = '" & mpITEMKEY & "' WHERE DocNo = '" & mpDOCNO & "' "
        mlSQLINV = mlSQLINV & "INSERT INTO AR_INVDT (DocNo,Linno,ItemKey,Description,Qty,QtyDO,UnitPoint," & _
                    " UnitPrice,DiscType,DiscAmount,Total)" & _
                    " VALUES ( " & _
                    " '" & mpDOCNO & "','" & mlLINNO & "'," & _
                    " '" & Trim(mpITEMKEY) & "','" & Trim(mpITEMDESCRIPTION) & "'," & _
                    " '" & Trim(mlOBJGF.FormatNum(mpQTY)) & "','0','" & CDbl(mlOBJGF.FormatNum(mpUNITPOINT)) & "'," & _
                    " '" & CDbl(mlOBJGF.FormatNum(mpUNITPRICE)) & "','" & CDbl(mlOBJGF.FormatNum(mpUNITPRICE)) & "'," & _
                    " '0','" & CDbl(mlOBJGF.FormatNum(mpUNITPRICE * mpQTY)) & "'" & _
                    " )"

    End Function

    Protected Sub btCHECKOUT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCHECKOUT.Click
        Response.Redirect("df_invoice.aspx?mpFP=" & mlFUNCTIONPARAMETER)
    End Sub

    Protected Sub btVIEWCART_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btVIEWCART.Click

    End Sub
End Class
