Imports System
Imports System.Data
Imports System.Data.OleDb


Partial Class ct_treedowntxpress
    Inherits System.Web.UI.Page

    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJGS As New IASClass.ucmGeneralSystem

    Dim mlSQL As String
    Public mlREADER As OleDbDataReader
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String

    Public mlSTATUS As Boolean = False
    Public mlFIRST As Boolean = True
    Public mlTREEPIC2 As String
    Dim mlTREETABLE As String
    Public mlDOCNO As String
    Public mlRUN As Boolean



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mlTITLE.Text = "Contract Tracking (Tree Model) V2.01"
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Contract Tracking (Tree Model) V2.01"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")


        If Page.IsPostBack = False Then
            LoadComboData()
            pnSEARCHCONTRACT.Visible = False
            mlMESSAGE.Text = ""
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ct_treedowntxpress", "TreeDown TxPress", "")
        End If
    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub
    Sub btDISTID_click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btDISTID.Click
        'SearchDistName()
    End Sub

    Sub SearchDistName()
        'txNAME.Text = mlOBJGS.ARGeneralLostFocus("DISTID", txDISTID.Text, "", "")
    End Sub


    ''
    Protected Sub btSearchContract_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchContract.Click
        If pnSearchContract.Visible = False Then
            pnSearchContract.Visible = True
        Else
            pnSearchContract.Visible = False
        End If
    End Sub


    Protected Sub btSearchContractSUBMIT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchContractSUBMIT.Click
        If mlOBJGF.IsNone(mlSearchContract.Text) = False Then SearchContract(1, mlSearchContract.Text)
    End Sub

    Protected Sub btSearchContractSUBMIT2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchContractSUBMIT2.Click
        If mlOBJGF.IsNone(mlSearchContract2.Text) = False Then SearchContract(3, mlSearchContract2.Text)
    End Sub

    Protected Sub mlDATAGRIDCONTRACT_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRIDCONTRACT.ItemCommand
        Try
            txDISTID.Text = CType(e.Item.Cells(1).Controls(0), LinkButton).Text
            lb1.Text = "Cust : " & CType(e.Item.Cells(2).Controls(0), LinkButton).Text
            lb2.Text = "Contract No : " & CType(e.Item.Cells(0).Controls(0), LinkButton).Text
            mlVALUE1.Value = Trim(CType(e.Item.Cells(0).Controls(0), LinkButton).Text)
            mlDATAGRIDCONTRACT.DataSource = Nothing
            mlDATAGRIDCONTRACT.DataBind()
            pnSearchContract.Visible = False

        Catch ex As Exception
        End Try
    End Sub

    Sub SearchContract(ByVal mpTYPE As Byte, ByVal mpNAME As String)
        Select Case mpTYPE
            Case "1"
                mlSQLTEMP = "SELECT  ReffNo as field_ID,DocNo as Field_Name,CustName as Cust FROM UT_TRANSFERTASK WHERE [ReffNo] LIKE  '%" & mlSEARCHCONTRACT.Text & "%' AND RecordStatus='New'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                mlDATAGRIDCONTRACT.DataSource = mlRSTEMP
                mlDATAGRIDCONTRACT.DataBind()

            Case "3"
                mlSQLTEMP = "SELECT  ReffNo as field_ID,DocNo as Field_Name,CustName as Cust FROM UT_TRANSFERTASK WHERE [CustName] LIKE  '%" & mlSEARCHCONTRACT2.Text & "%' AND RecordStatus='New'"
                mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
                mlDATAGRIDCONTRACT.DataSource = mlRSTEMP
                mlDATAGRIDCONTRACT.DataBind()
        End Select
    End Sub


    Function ValidateTreeDownline(ByVal mpTREETABLE As String, ByVal mpSESSIONID As String, ByVal txDISTID As String, ByVal mpDISTNAME As String) As Boolean
        ValidateTreeDownline = False

        ValidateTreeDownline = True
    End Function


    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

    Sub LoadComboData()
        ddTYPE.Items.Clear()
        ddTYPE.Items.Add("By Transfer Task No")
        ddTYPE.Items.Add("By Contract No")
    End Sub


    Sub SearchRecord()
        If ValidateTreeDownline(mlTREETABLE, Session("mgUSERID"), txDISTID.Text, txNAME.Text) = False Then
            Exit Sub
        End If

        Select Case ddTYPE.Text
            Case "By Transfer Task No"
                SearchRecord2()
            Case "By Contract No"
                SearchRecord3()
        End Select
    End Sub

    Sub SearchRecord2()
    
        Try
            mlDOCNO = Trim(txDISTID.Text)

            mlSQL = "SELECT DOCNO,SysID,Linno, Linno2,UserID1, UserName1, UserID2,UserName2, DocDate, Description, Description2 FROM " & _
                    "( " & _
                    " SELECT '" & mlDOCNO & "' AS DOCNO, 'Start' AS SysID, '0' as Linno, '0' as Linno2,'' as UserID1, '' as UserName1, '' as UserID2,'' as UserName2, '' as DocDate, '' as Description, '' as Description2 " & _
                    " UNION ALL " & _
                    " SELECT DOCNO,SysID,'0' as Linno,  Linno as Linno2,UserID1, UserName1, UserID2,UserName2, DocDate, Description," & _
                    " UserName1 + ' to ' + UserName2 + ', Deadline= ' + CONVERT(VARCHAR(10),[DeadlineDate],101) as Description2 FROM UT_TRANSFERTASK" & _
                    " WHERE DOCNO='" & mlDOCNO & "'" & _
                    " UNION ALL" & _
                    " SELECT DOCNO,SysID,Linno,  Linno2,RecUserID as UserID1, RecName as UserName2,Courier_Type as UserID2,Courier_Name as UserName2,DocDate, Description," & _
                    " 'Type=' +  Courier_DocType + ', C_No=' +  Courier_DocNo + " & _
                    " ', C_Date=' +   CONVERT(VARCHAR(10),[Courier_Date],101) + " & _
                    " ', C_Type=' +  Courier_DocType + ', C_Type=' +  Courier_Type +  " & _
                    "', C_Name=' +  Courier_Name + ', C_PIC=' +  Courier_PIC_ID + ', C_PICName=' +  Courier_PIC_Name + " & _
                    "', C_PICNo=' +  Courier_PIC_Phone +  ', C_PIC_Pos=' +  Courier_PIC_Pos " & _
                    " as Description2 FROM CT_CONTRACT_TASKDESC" & _
                    " WHERE DOCNO='" & mlDOCNO & "'" & _
                    " ) TB1" & _
                    " ORDER BY Linno,Linno2"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlSTATUS = True
            End If


        Catch ex As Exception
        End Try
    End Sub


    Sub SearchRecord3()
        Dim mlDOCNO2 As String

        Try
            mlDOCNO2 = Trim(txDISTID.Text) & "x"
            mlDOCNO = ""
            mlSQL = "SELECT DISTINCT DOCNO FROM UT_TRANSFERTASK WHERE ReffNo ='" & mlVALUE1.Value & "'"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            While mlREADER.Read
                mlDOCNO = mlDOCNO & IIf(mlDOCNO <> "", ",", "") & "'" & mlREADER("DocNo") & "'"
            End While


            mlSQL = "SELECT DOCNO,SysID,Linno, Linno2,UserID1, UserName1, UserID2,UserName2, DocDate, Description, Description2 FROM " & _
                    "( " & _
                    " SELECT '" & mlDOCNO2 & "' AS DOCNO, 'Start' AS SysID, '0' as Linno, '0' as Linno2,'' as UserID1, '' as UserName1, '' as UserID2,'' as UserName2, '' as DocDate, '' as Description, '' as Description2 " & _
                    " UNION ALL " & _
                    " SELECT DOCNO,SysID,'0' as Linno,  Linno as Linno2,UserID1, UserName1, UserID2,UserName2, DocDate, Description," & _
                    " UserName1 + ' to ' + UserName2 + ', Deadline= ' + CONVERT(VARCHAR(10),[DeadlineDate],101) as Description2 FROM UT_TRANSFERTASK" & _
                    " WHERE DOCNO IN (" & mlDOCNO & ")" & _
                    " UNION ALL" & _
                    " SELECT DOCNO,SysID,Linno,  Linno2,RecUserID as UserID1, RecName as UserName2,Courier_Type as UserID2,Courier_Name as UserName2,DocDate, Description," & _
                    " 'Type=' +  Courier_DocType + ', C_No=' +  Courier_DocNo + " & _
                    " ', C_Date=' +   CONVERT(VARCHAR(10),[Courier_Date],101) + " & _
                    " ', C_Type=' +  Courier_DocType + ', C_Type=' +  Courier_Type +  " & _
                    "', C_Name=' +  Courier_Name + ', C_PIC=' +  Courier_PIC_ID + ', C_PICName=' +  Courier_PIC_Name + " & _
                    "', C_PICNo=' +  Courier_PIC_Phone +  ', C_PIC_Pos=' +  Courier_PIC_Pos " & _
                    " as Description2 FROM CT_CONTRACT_TASKDESC" & _
                    " WHERE DOCNO IN (" & mlDOCNO & ")" & _
                    " ) TB1" & _
                    " ORDER BY DOCNO,Linno,Linno2"
            mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
            If mlREADER.HasRows Then
                mlSTATUS = True
            End If

        Catch ex As Exception
        End Try
    End Sub


End Class
