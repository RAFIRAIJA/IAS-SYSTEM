Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO


Partial Class xm_masteraddress_xls_s
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New FunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlREADER2 As OleDb.OleDbDataReader
    Dim mlSQL2 As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlDATATABLE As DataTable
    Dim mlKEY As String
    Dim mlRECORDSTATUS As String
    Dim mlSPTYPE As String
    Dim mlFUNCTIONPARAMETER As String
    Dim mlI As Integer
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlDEFAULTPARAMETER As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Address Book Import Excel Data (S) V2.03"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Address Book Import Excel Data (S) V2.03"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlDEFAULTPARAMETER = "ARSHIP"
        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "", "", "")
            txSHEET.Text = "Sheet1"
            DisableCancel()
        Else
        End If
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "BrowseRecord"
                mlMESSAGE.Text = "Update Request for " & e.CommandArgument
                RetrieveFields()
                pnFILL.Visible = True
            Case "EditRecord"
                mlMESSAGE.Text = "Edit Request for  " & e.CommandArgument
                mlSYSCODE.Value = "edit"
                EditRecord()
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                mlSYSCODE.Value = "delete"
                DeleteRecord()
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub btNewRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btNewRecord.Click
        NewRecord()
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub


    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        If pnFILL.Visible = True Then
            SaveRecord()
        End If
    End Sub

    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        DisableCancel()
    End Sub

    Sub SearchRecord()


        If mlFUNCTIONPARAMETER = "" Then mlFUNCTIONPARAMETER = mlDEFAULTPARAMETER

        Try

            mlSQL = "SELECT AddressKey,Name,Address,City,State,Country,Zip,Phone1,Phone2," & _
                " Fax,Mobile1,Mobile2,Email,DefaultPIC," & _
                " Recuserid,Recdate" & _
                " FROM XM_ADDRESSBOOK WHERE AddressCode = '" & mlFUNCTIONPARAMETER & "'"

            RetrieveFieldsDetail(mlSQL)


        Catch ex As Exception


        End Try
    End Sub


    Public Sub RetrieveFields()
        DisableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        Try

        
            If mpSQL = "" Then
                mlSQL2 = "SELECT * FROM WHERE RecordStatus='New' ORDER BY DocNo"
            Else
                mlSQL2 = mpSQL
            End If
            mlREADER2 = mlOBJGS.DbRecordset(mlSQL2)
            mlDATAGRID.DataSource = mlREADER2
            mlDATAGRID.DataBind()

            mlOBJGS.CloseFile(mlREADER2)

        Catch ex As Exception
            
        End Try
    End Sub


    Sub DeleteRecord()
        mlSPTYPE = "Delete"
        Try
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        RetrieveFields()
    End Sub

    Sub NewRecord()
        mlOBJGS.mgNEW = True
        mlOBJGS.mgEDIT = False
        EnableCancel()
        ClearFields()
        LoadComboData()
    End Sub

    Sub EditRecord()
        mlOBJGS.mgNEW = False
        mlOBJGS.mgEDIT = True
        ClearFields()
        LoadComboData()
        RetrieveFields()
        EnableCancel()
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
    End Sub

    Sub LoadComboData()
    End Sub

    Sub SaveRecord()
        Dim mlSQLHR As String = ""
        Dim mlSQLDT As String = ""

        mlOBJGS.mgMESSAGE = ValidateForm()
        If mlOBJGF.IsNone(mlOBJGS.mgMESSAGE) = False Then
            mlMESSAGE.Text = mlOBJGS.mgMESSAGE
            Exit Sub
        End If

        mlSPTYPE = "New"
        If mlSYSCODE.Value = "edit" Then
            mlSPTYPE = "Edit"
        End If

        Try
            mlKEY = ""
            Sql_1(mlSPTYPE, mlKEY)
        Catch ex As Exception
        End Try

        mlSYSCODE.Value = ""
        ClearFields()
        DisableCancel()
    End Sub

    Function ValidateForm() As String
        ValidateForm = ""

        If mlOBJGS.mgNEW = True Then

        End If
    End Function


    Protected Sub btSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSUBMIT.Click
        'If fpUPLOAD1.HasFile = False Then Exit Sub
        'If txSHEET.Text = "" Then Exit Sub

        Dim mlFILEPATH2 As String
        Dim mlFILENAME2 As String
        Dim mlSHEETNAME As String

        Dim mlFILENAME As String
        Dim mlFILEPATH As String


        If fpUPLOAD1.FileName = "" Then
            mlMESSAGE.Text = "Please choose worksheet file"
            Exit Sub
        End If

        mlFILENAME2 = fpUPLOAD1.FileName
        mlSHEETNAME = Trim(txSHEET.Text)



        
        mlFILENAME = "Ms_Addrbook_s." & mlOBJGF.GetStringAtPosition(mlFILENAME2, 1, ".")
        mlFILEPATH = "~/output/" & mlFILENAME
        mlFILEPATH = Server.MapPath(mlFILEPATH)
        fpUPLOAD1.SaveAs(mlFILEPATH)

        Excel_Display(mlFILEPATH, mlFILENAME, mlSHEETNAME)
    End Sub


    Function Excel_Display(ByVal mpPATH As String, ByVal mpFILENAME As String, ByVal mpSHEETNAME As String) As Boolean

        Excel_Display = True
        Dim mlCONNECTIONSTRING As String
        Dim mlSQLXLS As String

        mlCONNECTIONSTRING = ""
        Select Case LCase(mlOBJGF.GetStringAtPosition(mpFILENAME, 1, "."))
            Case "xls"
                mlCONNECTIONSTRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & mpPATH _
                    & ";" & "Extended Properties=Excel 8.0;"
            Case "xlsx"
                mlCONNECTIONSTRING = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & mpPATH _
                    & ";" & "Extended Properties=Excel 12.0;"
        End Select

        Dim mlOBJCONN As New OleDbConnection(mlCONNECTIONSTRING)
        mlOBJCONN.Open()

        mpSHEETNAME = mpSHEETNAME & "$"
        mlSQLXLS = "SELECT * FROM [" & mpSHEETNAME & "]"
        Dim mlOBJCMD As New OleDbCommand(mlSQLXLS, mlOBJCONN)


        Dim mlDATAADAPTER_XLS As New OleDbDataAdapter()
        mlDATAADAPTER_XLS.SelectCommand = mlOBJCMD
        Dim mlDATAASET_XLS As New DataSet()
        mlDATAADAPTER_XLS.Fill(mlDATAASET_XLS, "XLData")


        mlDATAGRID.DataSource = mlDATAASET_XLS.Tables(0).DefaultView
        mlDATAGRID.DataBind()
        mlOBJCONN.Close()
    End Function


    Function XM_AddressBook_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO XM_ADDRESSBOOK_LOG (AddressCode,AddressKey,Name,CustID,CustName,UplineID,UplineName,RecruiterID," & _
                    " RecruiterName,CommPercentage,PriceCode,ICNo,TaxID,Address,City,State,Country,Zip,Phone1,Phone2,Fax,Mobile1,Mobile2," & _
                    " Email,Website,JoinDate,CreditLimit,DefaultCurrency,DefaultTerm,DefaultSales,DefaultPIC,DefaultDiscHR," & _
                    " DefaultDiscDT,RecordStatus,Recuserid,Recdate,RecUDate)" & _
                    " SELECT AddressCode,AddressKey,Name,CustID,CustName,UplineID,UplineName,RecruiterID," & _
                    " RecruiterName,CommPercentage,PriceCode,ICNo,TaxID,Address,City,State,Country,Zip,Phone1,Phone2,Fax,Mobile1,Mobile2," & _
                    " Email,Website,JoinDate,CreditLimit,DefaultCurrency,DefaultTerm,DefaultSales,DefaultPIC,DefaultDiscHR," & _
                    " DefaultDiscDT,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & mlOBJGF.FormatDate(Now) & "'," & _
                    " getdate() FROM XM_ADDRESSBOOK WHERE AddressKey = '" & mpKEY & "' ;"
        Return mlLOG
    End Function

    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpCODE As String)
        Dim mlSTATUS As String
        Dim mlNEWRECORD As Boolean
        Dim mlCOUNTER_EXECSQL As Integer
        Dim mlMAX_EXECSQL As Integer


        mlKEY = mpCODE
        mpSTATUS = "Edit"
        Try
            Dim mlDG As DataGridItem

            Dim mlFIELD0 As String
            Dim mlFIELD1 As String
            Dim mlFIELD2 As String
            Dim mlFIELD3 As String
            Dim mlFIELD4 As String
            Dim mlFIELD5 As String
            Dim mlFIELD6 As String
            Dim mlFIELD7 As String
            Dim mlFIELD8 As String
            Dim mlFIELD9 As String
            Dim mlFIELD10 As String
            Dim mlFIELD11 As String
            Dim mlFIELD12 As String
            Dim mlFIELD13 As String
            Dim mlFIELD14 As String
            Dim mlFIELD15 As String

            If mlFUNCTIONPARAMETER = "" Then mlFUNCTIONPARAMETER = mlDEFAULTPARAMETER
            mlMAX_EXECSQL = 0


            For Each mlDG In mlDATAGRID.Items

                mlFIELD0 = Trim(mlDG.Cells("0").Text)
                mlFIELD1 = Trim(mlDG.Cells("1").Text)
                mlFIELD2 = Trim(mlDG.Cells("2").Text)
                mlFIELD3 = Trim(mlDG.Cells("3").Text)
                mlFIELD4 = Trim(mlDG.Cells("4").Text)
                mlFIELD5 = Trim(mlDG.Cells("5").Text)
                mlFIELD6 = Trim(mlDG.Cells("6").Text)
                mlFIELD7 = Trim(mlDG.Cells("7").Text)
                mlFIELD8 = Trim(mlDG.Cells("8").Text)
                mlFIELD9 = Trim(mlDG.Cells("9").Text)
                mlFIELD10 = Trim(mlDG.Cells("10").Text)
                mlFIELD11 = Trim(mlDG.Cells("11").Text)
                mlFIELD12 = Trim(mlDG.Cells("12").Text)
                mlFIELD13 = Trim(mlDG.Cells("13").Text)

                mlFIELD0 = Trim(Replace(mlFIELD0, "&nbsp;", ""))
                mlFIELD1 = Trim(Replace(mlFIELD1, "&nbsp;", ""))
                mlFIELD2 = Trim(Replace(mlFIELD2, "&nbsp;", ""))
                mlFIELD3 = Trim(Replace(mlFIELD3, "&nbsp;", ""))
                mlFIELD4 = Trim(Replace(mlFIELD4, "&nbsp;", ""))
                mlFIELD5 = Trim(Replace(mlFIELD5, "&nbsp;", ""))
                mlFIELD6 = Trim(Replace(mlFIELD6, "&nbsp;", ""))
                mlFIELD7 = Trim(Replace(mlFIELD7, "&nbsp;", ""))
                mlFIELD8 = Trim(Replace(mlFIELD8, "&nbsp;", ""))
                mlFIELD9 = Trim(Replace(mlFIELD9, "&nbsp;", ""))
                mlFIELD10 = Trim(Replace(mlFIELD10, "&nbsp;", ""))
                mlFIELD11 = Trim(Replace(mlFIELD11, "&nbsp;", ""))
                mlFIELD12 = Trim(Replace(mlFIELD12, "&nbsp;", ""))
                mlFIELD13 = Trim(Replace(mlFIELD13, "&nbsp;", ""))



                If mlOBJGF.IsNone(mlFIELD0) Or Trim(mlFIELD0) <> "&nbsp;" Or Trim(mlFIELD0) <> "" Then

                    mlSQL = mlSQL & " IF EXISTS (" & _
                            " SELECT AddressCode,AddressKey,Name,Address,City,State,Country,Zip,Phone1,Phone2," & _
                            " Fax,Mobile1,Mobile2,Email,DefaultPIC" & _
                            " FROM XM_ADDRESSBOOK WHERE AddressCode='" & mlFUNCTIONPARAMETER & "' " & _
                            " AND AddressKey = '" & mlFIELD0 & "' AND Name = '" & mlFIELD1 & "' " & _
                            " AND Address = '" & mlFIELD2 & "' AND City = '" & mlFIELD3 & "' " & _
                            " AND State = '" & mlFIELD4 & "' AND Country = '" & mlFIELD5 & "' " & _
                            " AND Zip = '" & mlFIELD6 & "' AND Phone1 = '" & mlFIELD7 & "' " & _
                            " AND Phone2 = '" & mlFIELD8 & "' AND Fax = '" & mlFIELD9 & "' " & _
                            " AND Mobile1 = '" & mlFIELD10 & "' AND Mobile2 = '" & mlFIELD11 & "' " & _
                            " AND Email = '" & mlFIELD12 & "' AND DefaultPIC = '" & mlFIELD13 & "' " & _
                            " )"
                    mlSQL = mlSQL & " BEGIN "
                    mlSQL = mlSQL & " Print 'N " & mlFIELD1 & "'"
                    mlSQL = mlSQL & " END "
                    mlSQL = mlSQL & " ELSE "
                    mlSQL = mlSQL & " BEGIN "
                    mlSQL = mlSQL & XM_AddressBook_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))
                    mlSQL = mlSQL & "INSERT INTO XM_ADDRESSBOOK (AddressCode,AddressKey,Name,Address,City,State,Country,Zip,Phone1,Phone2," & _
                            " Fax,Mobile1,Mobile2,Email,DefaultPIC," & _
                            " RecordStatus,Recuserid,Recdate)" & _
                            " VALUES (" & _
                            " '" & Trim(mlFUNCTIONPARAMETER) & "','" & Trim(mlFIELD0) & "', " & _
                            " '" & Trim(mlFIELD1) & "', '" & Trim(mlFIELD2) & "'," & _
                            " '" & Trim(mlFIELD3) & "', '" & Trim(mlFIELD4) & "'," & _
                            " '" & Trim(mlFIELD5) & "', '" & Trim(mlFIELD6) & "'," & _
                            " '" & Trim(mlFIELD7) & "', '" & Trim(mlFIELD8) & "'," & _
                            " '" & Trim(mlFIELD9) & "', '" & Trim(mlFIELD10) & "'," & _
                            " '" & Trim(mlFIELD11) & "', '" & Trim(mlFIELD12) & "'," & _
                            " '" & Trim(mlFIELD13) & "', " & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "'" & _
                            " )"
                    mlSQL = mlSQL & " Print 'Y " & mlFIELD1 & "'"
                    mlSQL = mlSQL & " END "
                    mlSQL = mlSQL & ";"


                    mlCOUNTER_EXECSQL = mlCOUNTER_EXECSQL + 1
                    If mlCOUNTER_EXECSQL >= mlMAX_EXECSQL Then
                        mlOBJGS.ExecuteQuery(mlSQL)
                        mlSQL = ""
                    End If
                End If
            Next mlDG
            If mlOBJGF.IsNone(mlSQL) = False Then mlOBJGS.ExecuteQuery(mlSQL)

            mlSQL = ""

            mlMESSAGE.Text = "Successfull"
        Catch ex As Exception
            mlMESSAGE.Text = "Fail"
            mlOBJGS.XMtoLog("XM", "XM", "AddressBook" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub


End Class
