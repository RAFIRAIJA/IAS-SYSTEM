Imports System.Data
Imports System.Data.OleDb
Imports System.Web
Imports System.Drawing
Imports IAS.Core.CSCode
Partial Class ap_post_mrbatch_appo
    Inherits System.Web.UI.Page

    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJPJ As New ModuleFunctionLocal

    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlSQLUSER As String
    Dim mlSQLUSERSITE As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlFUNCTIONPARAMETER As String

    Public mlBATCHINV As String
    Public mlBATCHINV2 As String
    Public mlBATCHTOTALPOINT As Double
    Public mlBATCHTOTALAMOUNT As Double


    Dim mlKEY As String
    Dim mlFUNCTIONPARAMETERORI As String
    Dim mlRECORDSTATUS As String
    Dim mlCHECKID As String
    Dim mlMEMBERGROUP As String
    Dim mlSTOCKISTGROUP As String
    Dim mlCURRENTDATE As String = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()
    Dim mlUSERLEVEL As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        CekSession()
        Me.MasterPageFile = mlOBJPJ.AD_CHECKMENUSTYLE(Session("mgMENUSTYLE").ToString(), Me.MasterPageFile)
    End Sub
    Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = System.Configuration.ConfigurationManager.AppSettings("mgTITLE") & "Posting Material Requisition (MR) V2.03"
        mlTITLE.Text = "Posting Material Requisition to Worksheet Requisition V2.03"
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If Session("mgACTIVECOMPANY") = "" Then Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = Session("mgACTIVECOMPANY")

        mlMEMBERGROUP = System.Configuration.ConfigurationManager.AppSettings("mgMEMBERGROUPMENU")
        mlSTOCKISTGROUP = System.Configuration.ConfigurationManager.AppSettings("mgSTOCKISTGROUPMENU")

        mlFUNCTIONPARAMETER = Request("mpFP")
        'mlFUNCTIONPARAMETER = "1"
        If mlFUNCTIONPARAMETER <> "" Then
            If mlFUNCTIONPARAMETERORI = "" Then mlFUNCTIONPARAMETERORI = mlFUNCTIONPARAMETER
            mlSYSCODE.Value = mlFUNCTIONPARAMETER
        End If

        mlFUNCTIONPARAMETERORI = mlSYSCODE.Value
        mlFUNCTIONPARAMETER = "" & mlSYSCODE.Value

        If Page.IsPostBack = False Then
            mlOBJGS.XM_UserLog(Session("mgUSERID"), Session("mgNAME"), "ap_post_mrbatch_appo", "Post MR to PO", "")

            EnableCancel()
            LoadComboData()
            mlMESSAGE.Text = "Fill the Data then Click Search Button to Get Posting Document"
        End If
    End Sub
    Protected Sub CekSession()
        If Session("mgMENUSTYLE") = "" Then
            Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=34FC35D4")
            Return
        End If
    End Sub

    Protected Sub mlDATAGRID_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles mlDATAGRID.ItemCommand
        mlKEY = e.CommandArgument
        Select Case e.CommandName
            Case "DeleteRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
                SearchRecord()

            Case "CheckedRecord"
                mlMESSAGE.Text = "Delete Request for  " & e.CommandArgument
            Case Else
                ' Ignore Other
        End Select
    End Sub

    Protected Sub btSaveRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSaveRecord.Click
        Try
            If pnFILL.Visible = True Then
                SaveRecord()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btCancelOperation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btCancelOperation.Click
        RetrieveFields()
        LoadComboData()
    End Sub

    Public Sub RetrieveFields()
        ClearFields()
        EnableCancel()
    End Sub

    Sub RetrieveFieldsDetail(ByVal mpSQL As String)
        mlREADER = mlOBJGS.DbRecordset(mpSQL, "PB", "ISSP3")
        mlDATAGRID.DataSource = mlREADER
        mlDATAGRID.DataBind()

        If mlREADER.HasRows Then
            btSaveRecord.Visible = True
        End If
    End Sub

    Sub NewRecord()
        ClearFields()
        EnableCancel()
    End Sub

    Sub DeleteRecord()
        mlRECORDSTATUS = "Delete"
        mlOBJGS.ExecuteQuery(mlSQL)
        RetrieveFields()
    End Sub

    Sub EditRecord()
        EnableCancel()
    End Sub


    Private Sub EnableCancel()
        pnFILL.Visible = True
        pnGRID.Visible = False
        btSaveRecord.Visible = False
        btSearchRecord.Visible = True
    End Sub

    Private Sub DisableCancel()
        btSaveRecord.Visible = True
        btSearchRecord.Visible = False
        pnFILL.Visible = True
        pnGRID.Visible = True
    End Sub

    Sub ClearFields()
        mlMESSAGE.Text = ""
        mlLINKDOC.Text = ""
        mlLINKDOC3.Text = ""
    End Sub

    Sub LoadComboData()
        mlDATEFROM.Text = mlCURRENTDATE
        mlDATETO.Text = mlCURRENTDATE

        mlRECORDPERPAGE.Items.Clear()
        mlRECORDPERPAGE.Items.Add("10")
        mlRECORDPERPAGE.Items.Add("20")
        mlRECORDPERPAGE.Items.Add("50")
        mlRECORDPERPAGE.Items.Add("100")
        mlRECORDPERPAGE.Items.Add("200")

        mlSTATUS.Items.Clear()
        mlSTATUS.Items.Add("New")
        mlSTATUS.Items.Add("Post")
        mlUPDATESTATUS.Items.Clear()
        mlUPDATESTATUS.Items.Add("Post")
        mlUPDATESTATUS.Items.Add("New")

    End Sub

    Protected Sub mlCHECKALL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mlCHECKALL.CheckedChanged
        Dim mlDG As DataGridItem

        If mlCHECKALL.Checked Then
            For Each mlDG In mlDATAGRID.Items
                Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)
                mlBOX.Checked = True
            Next mlDG
        Else
            For Each mlDG In mlDATAGRID.Items
                Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)
                mlBOX.Checked = False
            Next mlDG
        End If
    End Sub

    Sub SearchRecord()
        Dim mlSQL As String
        Dim mlFUNCTIONPARAMETER2 As String

        Try
            mlSQL = ""

            If mlDATEFROM.Text <> "" And mlDATETO.Text <> "" Then
                mlSQL = mlSQL & " (HR.DocDate > = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDATEFROM.Text, "/")) & "' AND HR.DocDate < = '" & mlOBJGF.FormatDate(mlOBJGF.ConvertDateIDNtoIntl(mlDATETO.Text, "/")) & "')"
            End If

            If mlDOCUMENTNO1.Text <> "" And mlDOCUMENTNO2.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " DocNo >= '" & Trim(mlDOCUMENTNO1.Text) & "' AND DocNo <= '" & Trim(mlDOCUMENTNO2.Text) & "'"
            End If

            If mpPERIOD.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " BVMonth LIKE '" & Trim(mpPERIOD.Text) & "' "
            End If

            If mlUSERID.Text <> "" Then
                mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " PostingUserID1 LIKE '" & Trim(mlUSERID.Text) & "' "
            End If


            'If mlSTATUS.Text <> "" Then
            mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " RecordStatus LIKE '" & mlSTATUS.Text & "' "
            'End If

            mlFUNCTIONPARAMETER2 = mlFUNCTIONPARAMETER
            'mlSQL = mlSQL & IIf(mlSQL = "", "", "AND") & " ParentCode = '" & mlFUNCTIONPARAMETER2 & "'"

            
            If mlOBJGF.IsNone(mlSQL) = False Then
                Try
                    mlSQL = "SELECT DocNo,DocDate,SiteCardID,SiteCardName,Do_Address as Delivery_Address,PercentageMR,BVMonth,TotalAmount,RecordStatus, " & _
                            " PostingUserID1,PostingName1,PostingDate1,PostingUserID2,PostingName2,PostingDate2,PostingUserID3,PostingName3,PostingDate3" & _
                            " FROM AP_MR_REQUESTHR HR WHERE " & mlSQL & mlSQLUSER & " ORDER BY DocNo"
                    RetrieveFieldsDetail(mlSQL)
                Catch ex As Exception
                End Try
            End If

            DisableCancel()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btSearchRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btSearchRecord.Click
        SearchRecord()
    End Sub

    Public Function mpCrossPageField() As String
        Return mlFUNCTIONPARAMETERORI & "#" & mlBATCHINV & "#" & mlBATCHTOTALPOINT & "#" & mlBATCHTOTALAMOUNT
    End Function

    Sub SaveRecord()
        Dim mlDG As DataGridItem
        Dim mlSAVESTATUS As Boolean = False

        mlBATCHINV2 = ""
        mlBATCHINV = ""
        mlBATCHTOTALPOINT = "0"
        mlBATCHTOTALAMOUNT = "0"
        For Each mlDG In mlDATAGRID.Items
            Dim mlBOX As CheckBox = CType(mlDG.FindControl("mlCHECKBOX"), CheckBox)

            If mlBOX.Checked Then
                mlSAVESTATUS = True
                mlBATCHINV = mlBATCHINV & IIf(mlBATCHINV = "", "", ",") & Trim(mlDG.Cells("2").Text)
                mlBATCHINV2 = mlBATCHINV2 & IIf(mlBATCHINV2 = "", "'", "','") & Trim(mlDG.Cells("2").Text)
            End If
        Next mlDG
        mlBATCHINV2 = mlBATCHINV2 & IIf(mlBATCHINV2 = "", "", "'")

        If mlSAVESTATUS Then
            'mpCrossPageField()
            Sql_1(mlUPDATESTATUS.Text, mlBATCHINV)
        End If

        pnFILL.Visible = False
        pnGRID.Visible = False
        btSaveRecord.Visible = False
        btSearchRecord.Visible = False

    End Sub

    Sub Sql_1(ByVal mpSTATUS As String, ByVal mpBATCHINVNO As String)
        Dim mlSQLITEM1 As String
        Dim mlRSITEM1 As OleDbDataReader
        Dim mlSQLMR As String
        Dim mlRSMR As OleDbDataReader
        Dim mlITEMKEY As String
        Dim mlSITECARDID As String
        Dim mlVENDORID As String
        Dim mlVENDORNAME As String
        Dim mlSQLINSERTREQUISITION As String
        Dim mlSQLINSERTSCHEDULE As String
        Dim mlITEMUOM As String
        Dim mlITEMUOMQTY As Double
        Dim mlGETVENDOR As Boolean
        Dim mlITEMSPLIT As Boolean
        Dim mlITEMQTYBYUOM(3) As Double
        Dim mlITEMQTYSPLIT As Double
        Dim mlITEMUOMBYUOM(3) As String
        Dim mlJ As Byte
        Dim mlITEMSPLITTIMES As Byte

        Dim mlOUT_ITEMTYPE As String
        Dim mlOUT_DRIVER As String
        Dim mlOUT_DELIVERYDATE As String
        Dim mlDOCNO_RQ As String
        Dim mlDOCNO_STATUS As Boolean
        Dim mlDOCNO_SCH_DEL As String
        Dim mlSITECARDREFF As String


        Try
            mlMESSAGE.Text = ""
            mlDOCNO_RQ = ""
            mlDOCNO_SCH_DEL = ""
            mlSQLINSERTREQUISITION = ""
            mlSQLINSERTSCHEDULE = ""



            Select Case mpSTATUS
                Case "Post", "Void"
                    mlSQL = ""
                    'mlSQL = mlSQL & mlOBJPJ.ISS_MR_MREntry_ToLog(mlKEY, mpSTATUS, Session("mgUSERID"))

            End Select

            Select Case mpSTATUS
                Case "New", "Post"

                    mlSQLMR = "SELECT HR.*, DT.*, DT.Description as ItemDesc FROM AP_MR_REQUESTHR HR,AP_MR_REQUESTDT DT " & _
                            " WHERE HR.DocNo = DT.DocNo AND HR.DocNo in (" & mlBATCHINV2 & ")"
                    mlRSMR = mlOBJGS.DbRecordset(mlSQLMR, "PB", "ISSP3")
                    While mlRSMR.Read
                        mlITEMUOM = ""
                        mlITEMUOMQTY = "0"
                        mlVENDORID = ""
                        mlVENDORNAME = ""
                        mlGETVENDOR = False
                        mlITEMSPLIT = False

                        mlITEMKEY = mlRSMR("ItemKey")
                        mlSITECARDID = mlRSMR("SiteCardID")

                        mlITEMUOMBYUOM(1) = Trim(mlRSMR("Uom"))
                        mlITEMQTYBYUOM(1) = Trim(mlRSMR("Qty"))
                        mlITEMQTYBYUOM(2) = 1
                        mlITEMUOMBYUOM(2) = mlITEMUOMBYUOM(1)
                        mlITEMSPLITTIMES = 1

                        mlSQLITEM1 = "SELECT [Code],[Qty_ per Unit of Measure] FROM [ISS Servisystem, PT$Item Unit of Measure] WHERE [Item No_] = '" & mlITEMKEY & "'" & _
                                " AND [Code] <> '" & Trim(mlRSMR("uom")) & "'"
                        mlRSITEM1 = mlOBJGS.DbRecordset(mlSQLITEM1, "PB", "ISSN3")
                        If mlRSITEM1.HasRows Then
                            mlRSITEM1.Read()
                            mlITEMSPLIT = True
                            mlITEMUOMBYUOM(2) = mlRSITEM1("Code")
                            mlITEMQTYBYUOM(2) = mlRSITEM1("Qty_ per Unit of Measure")

                            If mlITEMQTYBYUOM(1) > mlITEMQTYBYUOM(2) Then
                                mlITEMSPLITTIMES = 2
                            End If
                        End If


                        mlSQLITEM1 = "SELECT * FROM IN_INMAST_ADDINFO_APZONE WHERE (ItemKey = '" & mlITEMKEY & "' OR  ItemKey = 'ALL')" & _
                                    " AND SiteCardID = '" & mlSITECARDID & "'"
                        mlRSITEM1 = mlOBJGS.DbRecordset(mlSQLITEM1, "PB", "ISSP3")
                        While mlRSITEM1.Read
                            If mlGETVENDOR = False Then
                                mlVENDORID = mlRSITEM1("VendID")
                                mlVENDORNAME = mlRSITEM1("VendName")
                            End If

                            If mlRSITEM1("ItemKey") <> "ALL" Then
                                mlGETVENDOR = True
                            End If
                        End While

                        If mlVENDORID = "" Then
                            mlOBJGS.CloseFile(mlRSITEM1)
                            mlSQLITEM1 = "SELECT INV.[Vendor No_], VND.[Search Name] FROM [ISS Servisystem, PT$Item] INV, " & _
                            " [dbo].[ISS Servisystem, PT$Vendor] VND WHERE INV.[Vendor No_] = VND.[No_] AND INV.[No_] = '" & mlITEMKEY & "'"
                            mlRSITEM1 = mlOBJGS.DbRecordset(mlSQLITEM1, "PB", "ISSN3")
                            If mlRSITEM1.HasRows Then
                                mlRSITEM1.Read()
                                mlVENDORID = mlRSITEM1("Vendor No_")
                                mlVENDORNAME = mlRSITEM1("Search Name")
                            End If
                        End If

                        If mlDOCNO_STATUS = False Then
                            mlDOCNO_RQ = mlOBJGS.GetModuleCounter("AP_OUT_REQUISITION", "00000000")
                            mlDOCNO_SCH_DEL = mlOBJGS.GetModuleCounter("AP_OUT_SCHEDULE", "00000000")
                            mlDOCNO_STATUS = True
                        End If


                        mlOUT_ITEMTYPE = "ITEM"
                        mlOUT_DRIVER = ""
                        mlOUT_DELIVERYDATE = "01/" & Trim(mlRSMR("BVMonth"))
                        mlITEMQTYSPLIT = 1

                        mlJ = 1
                        For mlJ = 1 To mlITEMSPLITTIMES
                            mlSITECARDREFF = ""
                            Select Case mlJ
                                Case "1"
                                    mlSITECARDREFF = "A_" & mlDOCNO_SCH_DEL & "_" & mlVENDORID & "_" & mlVENDORNAME & "_" & Trim(mlRSMR("SiteCardID"))
                                    If mlITEMQTYBYUOM(1) > mlITEMQTYBYUOM(2) Then
                                        mlITEMQTYSPLIT = Convert.ToInt32(mlITEMQTYBYUOM(1) / mlITEMQTYBYUOM(2))
                                        mlITEMQTYBYUOM(3) = mlITEMQTYSPLIT
                                        mlITEMUOMBYUOM(3) = mlITEMUOMBYUOM(2)
                                    Else
                                        mlITEMQTYBYUOM(3) = mlITEMQTYBYUOM(1)
                                        mlITEMUOMBYUOM(3) = mlITEMUOMBYUOM(1)
                                    End If
                                Case "2"
                                    mlSITECARDREFF = "B_" & mlDOCNO_SCH_DEL & "_" & mlVENDORID & "_" & mlVENDORNAME & "_" & "Warehouse"
                                    If mlITEMQTYBYUOM(1) > mlITEMQTYBYUOM(2) Then
                                        mlITEMQTYBYUOM(3) = mlITEMQTYBYUOM(1) - (mlITEMQTYBYUOM(2) * mlITEMQTYSPLIT)
                                        mlITEMUOMBYUOM(3) = mlITEMUOMBYUOM(1)
                                    Else
                                        mlITEMQTYBYUOM(3) = mlITEMQTYBYUOM(2)
                                        mlITEMUOMBYUOM(3) = mlITEMUOMBYUOM(1)
                                    End If
                            End Select

                            'If after conversion, the balance of conversion is 0
                            If mlJ = "2" And mlITEMQTYBYUOM(3) = "0" Then Exit For


                            mlSQL = mlSQL & "INSERT INTO AP_OUT_REQUISITION ([Type],[No.],[Driver],[Delivery Date]," & _
                            " [Created By],[Verified By]," & _
                            " [Agreed By]," & _
                            " [Site Card No.],[Location Code],[Material Requisition No.]," & _
                            " [Description],[Material Requisition Quantity],[Unit of Measure Code]," & _
                            " [Item Journal Batch], [Vendor No.],[Sitecard Reference]," & _
                            " [Qty PO w1],[Qty PO w2],[Qty PO w3],[Qty PO w4],[Qty PO w5]," & _
                            " DocNo,ReffNo,RecordStatus,RecUserID,RecDate)" & _
                            " VALUES (" & _
                            " '" & Trim(mlOUT_ITEMTYPE) & "','" & Trim(mlRSMR("ItemKey")) & "','" & Trim(mlOUT_DRIVER) & "','" & Trim(mlOBJGF.FormatDate(mlOUT_DELIVERYDATE)) & "'," & _
                            " '" & Trim(mlRSMR("PostingUserID1") & "-" & mlRSMR("PostingName1")) & "','" & Trim(mlRSMR("PostingUserID2") & "-" & mlRSMR("PostingName2")) & "'," & _
                            " '" & Trim(mlRSMR("PostingUserID3") & "-" & mlRSMR("PostingName3")) & "'," & _
                            " '" & Trim(mlRSMR("SiteCardID")) & "','" & Trim(mlRSMR("SC_BranchCode")) & "','" & Trim(mlRSMR("DocNo")) & "'," & _
                            " '" & Trim(mlRSMR("ItemDesc")) & "','" & Trim(mlITEMQTYBYUOM(3)) & "','" & Trim(mlITEMUOMBYUOM(3)) & "'," & _
                            " '','','" & Trim(mlSITECARDREFF) & "'," & _
                            " '0','0','0','0','0'," & _
                            " '" & Trim(mlDOCNO_RQ) & "','" & Trim(mlDOCNO_RQ) & "'," & _
                            " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "'" & _
                            " );"

                            mlSQL = mlSQL & "INSERT INTO AP_OUT_SCHEDULE ([Vendor No.],[No_MR],[Site Card No.],[Site Card Search Name],[State]," & _
                             " Unit, Qty, " & _
                             " DocNo,ReffNo,ItemKey,ItemDesc,RecordStatus,RecUserID,RecDate)" & _
                             " VALUES (" & _
                             " '" & Trim(mlVENDORID) & "','" & Trim(mlRSMR("DocNo")) & "','" & Trim(mlRSMR("SiteCardID")) & "'," & _
                             " '" & Trim(mlRSMR("SiteCardName")) & "','" & Trim(mlRSMR("SC_State")) & "'," & _
                             " '" & Trim(mlITEMUOMBYUOM(3)) & "', '" & Trim(mlITEMQTYBYUOM(3)) & "'," & _
                             " '" & mlDOCNO_SCH_DEL & "','" & mlDOCNO_SCH_DEL & "','" & Trim(mlRSMR("ItemKey")) & "','" & Trim(mlRSMR("ItemDesc")) & "'," & _
                             " 'New','" & Session("mgUSERID") & "','" & mlOBJGF.FormatDate(Now) & "'" & _
                             " );"
                        Next


                        'Update Status
                        Dim mlRECSTATUS As String
                        Dim mlI As Integer
                        Dim mlLOOP As Boolean
                        Dim mlDOCNO3 As String


                        mlRECSTATUS = mlUPDATESTATUS.Text
                        mlI = 0
                        mlLOOP = True
                        Do While mlLOOP
                            mlDOCNO3 = mlOBJGF.GetStringAtPosition(mpBATCHINVNO, mlI, ",")
                            mlI = mlI + 1
                            If mlDOCNO3 <> "" Then
                                mlSQL = mlSQL & "UPDATE AP_MR_REQUESTHR SET " & _
                                        " RecordStatus = '" & mlRECSTATUS & "'" & _
                                        " WHERE DocNo = '" & Trim(mlDOCNO3) & "';"
                            Else
                                mlLOOP = False
                            End If
                        Loop



                    End While
                Case "Void"
            End Select

            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            mlSQL = ""

            mlOBJGS.CloseFile(mlRSITEM1)
            mlOBJGS.CloseFile(mlRSMR)

            mlMESSAGE.Text = mlMESSAGE.Text & "<br>" & " No Worksheet Anda adalah  Anda Adalah " & "<a href=ap_doc_mr_worksheet.aspx?mpID=" & Trim(mlDOCNO_RQ) & "> " & mlDOCNO_RQ & " </a>" & _
            "<br> No Schedule Anda adalah  Anda Adalah " & "<a href=ap_doc_mr_schedule.aspx?mpID=" & Trim(mlDOCNO_SCH_DEL) & "> " & mlDOCNO_SCH_DEL & "  </a>" & _
            "<br><br>"

            mlKEY = Trim(mlDOCNO_RQ)
            mlLINKDOC.Text = ""
            mlLINKDOC.Text = "<font Color=blue> Click to View your Worksheet Document </font>"
            mlLINKDOC.NavigateUrl = ""
            mlLINKDOC.Attributes.Add("onClick", "window.open('ap_doc_mr_worksheet.aspx?mpID=" & mlKEY & "','','');")


            mlKEY = Trim(mlDOCNO_SCH_DEL)
            mlLINKDOC3.Text = ""
            mlLINKDOC3.Text = "<font Color=blue> Click to View your Schedule Document </font>"
            mlLINKDOC3.NavigateUrl = ""
            mlLINKDOC3.Attributes.Add("onClick", "window.open('ap_doc_mr_schedule.aspx?mpID=" & mlKEY & "','','');")


        Catch ex As Exception
            mlOBJGS.XMtoLog("MR", "MRtoPO", "MRtoPO" & mlKEY, Err.Description, "New", Session("mgUSERID"), mlOBJGF.FormatDate(Now))
        End Try
    End Sub

End Class
