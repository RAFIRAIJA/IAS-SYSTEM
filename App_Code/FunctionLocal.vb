'*****************************************************************************************************
'Program Name     : General for Fast Table.bas
'Program Function : General Data Access and System Administration
'Created by       : Sugianto
'Last Modificaion : 24 Desember 2011
'*****************************************************************************************************

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.ODBC
Imports Microsoft.VisualBasic

Public Class FunctionLocal
    Dim objMGF As New IASClass.ucmGeneralFunction
    Dim objMGS As New IASClass.ucmGeneralSystem


    Function AD_UserProfile_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO AD_USERPROFILE_LOG (Linno,UserID,Name,Password,DateJoin,CompanyID,BranchID,GroupID,DeptID,UserStatus," & _
                " UserLevel,IsLogin,NumberLogin,MenuStyle,LastSystem,LastCompany,TelHP,EmailAddr,EmployeeID,FingerPrintID," & _
                " ApplicationID,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                " SELECT Linno,UserID,Name,Password,DateJoin,CompanyID,BranchID,GroupID,DeptID,UserStatus,UserLevel,IsLogin,NumberLogin," & _
                " MenuStyle,LastSystem,LastCompany,TelHP,EmailAddr,EmployeeID,FingerPrintID,ApplicationID," & _
                " '" & Trim(mpSTATUS) & "','" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                " GETDATE() FROM AD_USERPROFILE WHERE UserID = '" & mpKEY & "';"
        Return mlLOG
    End Function

    Public Function ARGeneralLostFocus(ByVal mpPARAM As String, ByVal mpLOSTTEXT As String, Optional ByVal mpPARENTADDRESSCODE As String = "", Optional ByVal mpFUNCTIONPARAMETER As String = "") As String
        Dim mlLOSTTEXT As String
        Dim rsTEMPORARY As OleDb.OleDbDataReader
        Dim sqlTEMPORARY As String

        ARGeneralLostFocus = ""
        mlLOSTTEXT = Trim(mpLOSTTEXT)
        Select Case UCase(mpPARAM)
            Case "DISTID"
                sqlTEMPORARY = "SELECT * FROM AR_DIST WHERE DistID = '" & mpLOSTTEXT & "' AND SpecialMember=0"
                rsTEMPORARY = objMGS.DbRecordset(sqlTEMPORARY)
                rsTEMPORARY.Read()
                If rsTEMPORARY.HasRows Then ARGeneralLostFocus = Trim(rsTEMPORARY("Name"))
        End Select
    End Function

    Function DistInfoOnDistTree(ByVal mpDISTID As String) As String
        Dim mlSQLTREE As String
        Dim mlRSDISTTREE As OleDbDataReader
        Dim mlTEXT As String

        mlTEXT = ""
        mlSQLTREE = "SELECT * FROM AR_DISTTREE WHERE DistID = '" & mpDISTID & "'"
        mlRSDISTTREE = objMGS.DbRecordset(mlSQLTREE)
        If mlRSDISTTREE.HasRows Then
            mlRSDISTTREE.Read()
            mlTEXT = mlRSDISTTREE("DistID") & "#" & mlRSDISTTREE("forder") & "#" & mlRSDISTTREE("flevel") & "#" & _
            mlRSDISTTREE("fsponsor") & "#" & mlRSDISTTREE("frecruiter") & "#" & mlRSDISTTREE("fsporder") & "#" & _
            mlRSDISTTREE("fdxorder") & "#" & mlRSDISTTREE("fuporder") & "#" & mlRSDISTTREE("frcorder") & "#"
        End If

        Return mlTEXT
    End Function

    Function PutDistonDistTree(ByVal mpDISTID As String, ByVal mpRECRUITERID As String, ByVal mpUPLINEID As String, ByVal mpORDER As Integer, ByVal mpDXORDER As Integer, ByVal mpLEVEL As Integer, ByVal mpUPORDER As String) As String
        Dim mlSQLDISTTREE2 As String
        Dim mlORDER2 As Integer
        Dim mlDXORDER2 As Integer
        Dim mlLEVEL2 As Integer
        Dim mlUPORDER2 As String

        mlORDER2 = mpDXORDER + 1
        mlDXORDER2 = mpDXORDER + 1
        mlLEVEL2 = mpLEVEL + 1
        mlUPORDER2 = mpUPLINEID & "," & mpUPORDER

        mlSQLDISTTREE2 = ""
        mlSQLDISTTREE2 = mlSQLDISTTREE2 & " UPDATE AR_DISTTREE SET fdxorder = fdxorder + 1 WHERE forder >= '" & mlORDER2 & "';"
        mlSQLDISTTREE2 = mlSQLDISTTREE2 & " UPDATE AR_DISTTREE SET forder = forder + 1 WHERE forder >= '" & mlORDER2 & "';"
        mlSQLDISTTREE2 = mlSQLDISTTREE2 & " UPDATE AR_DISTTREE SET fdxorder = fdxorder + 1 WHERE DistID in (" & mlUPORDER2 & ");"
        objMGS.ExecuteQuery(mlSQLDISTTREE2)

        mlSQLDISTTREE2 = ""
        mlSQLDISTTREE2 = mlSQLDISTTREE2 & " INSERT INTO AR_DISTTREE (DistID,forder,flevel,fsponsor,frecruiter,fsporder,fdxorder," & _
                        " fuporder,frcorder,RecordStatus,RecUserID,RecDate) " & _
                        " VALUES ('" & mpDISTID & "', '" & mlORDER2 & "','" & mlLEVEL2 & "', '" & mpUPLINEID & "'," & _
                        " '" & mpRECRUITERID & "', '" & mpORDER & "', '" & mlDXORDER2 & "','" & mlUPORDER2 & "',''," & _
                        "'Post','', '" & objMGF.FormatDate(Now) & "'" & _
                        " )"
        objMGS.ExecuteQuery(mlSQLDISTTREE2)

        Return ""
    End Function

    Function PutDistonAddInfoTree(ByVal mpPARENTCODE As String, ByVal mpDISTID As String, ByVal mpNAME As String, ByVal mpUPLINEID As String, ByVal mpUPLINENAME As String, ByVal mpRECRUITERID As String, ByVal mpRECRUITERNAME As String, ByVal mpJOINDATE As Date, ByVal mpBVMONTH As String, ByVal mpRECORDSTATUS As String, ByVal mpUSERID As String)
        Dim mlLOG As String

        mlLOG = ""
        mlLOG = "INSERT INTO AR_DIST_ADDINFO (ParentCode,DistID,Name,UplineID,UplineName,RecruiterID,RecruiterName,UplineCount," & _
                " UplineSide,RecruiterCount,JoinDate,BVMonth,Description,Status,RecordStatus,RecUserID,RecDate) " & _
                " SELECT '" & mpPARENTCODE & "', '" & mpDISTID & "', '" & mpNAME & "', '" & mpUPLINEID & "', '" & mpUPLINENAME & "', " & _
                " '" & mpRECRUITERID & "', '" & mpRECRUITERNAME & "', (SELECT COUNT(DistID)  FROM AR_DIST WHERE UplineID = '" & mpUPLINEID & "')," & _
                " '',(SELECT COUNT(DistID) FROM AR_DIST WHERE UplineID = '" & mpRECRUITERID & "'), '" & objMGF.FormatDate(mpJOINDATE) & "', " & _
                " '" & mpBVMONTH & "', '', '','" & mpRECORDSTATUS & "', " & _
                " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "';"
        Return mlLOG

    End Function

    Public Sub SetTextBox(ByVal mpReadOnly As Boolean, ByVal mlTEXT As TextBox)
        If mpReadOnly Then
            mlTEXT.ReadOnly = True
            mlTEXT.BackColor = System.Drawing.Color.LemonChiffon
            mlTEXT.ForeColor = System.Drawing.Color.Black
        Else
            mlTEXT.ReadOnly = False
            mlTEXT.BackColor = System.Drawing.Color.White
            mlTEXT.ForeColor = System.Drawing.Color.Black
        End If
    End Sub

    Function AR_InvoiceToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO AR_INVHRLOG (ParentCode,SysID,DocNo,DocDate,LocationID,LocationName,MobileID,ReffNo," & _
                        " DistID,DistName,BVMonth,Description,TotalBruto,TotalPoint,TotalAmount,TotalShip,TotalPointR,TotalAmountR,PickingNo,PrintCounter,RecordStatus," & _
                        " RecUserID,RecDate,PayNo,BVMONTH2,DistID2,DistName2,RecUDate)" & _
                        " SELECT ParentCode,SysID,DocNo,DocDate,LocationID,LocationName,MobileID,ReffNo," & _
                        " DistID,DistName,BVMonth,Description,TotalBruto,TotalPoint,TotalAmount,TotalShip,TotalPointR,TotalAmountR,PickingNo,PrintCounter,'" & Trim(mpSTATUS) & "'," & _
                        " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " PayNo,BVMONTH2,DistID2,DistName2,GETDATE() FROM AR_INVHR WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & "INSERT INTO AR_INVDTLOG (DocNo,Linno,ItemKey,Description,Qty,QtyDO,UnitPoint,UnitPrice," & _
                " TotalPoint,TotalPrice,RecUDate)" & _
                " SELECT DocNo,Linno,ItemKey,Description,Qty,QtyDO,UnitPoint,UnitPrice, TotalPoint,TotalPrice,GETDATE()" & _
                " FROM AR_INVDT WHERE DocNo = '" & mpKEY & "';"

        Return mlLOG
    End Function

    Function AR_Receipt_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & "INSERT INTO AR_PAYHRLOG (ParentCode,DocNo,DocDate,ReffNo,BankID,Sales,CurrencyKey,CurrencyRate,Description," & _
                " TotalReceipt,TotalPoint,Discount,AREventID,Cash,CashDesc,TTOnline,OnlineDesc,Cheque,ChequeNo,ChequeDueDate," & _
                " CreditCard,CreditCardDesc,Voucher,VoucherDesc,Deposit,DepositDesc,Other,OtherDesc," & _
                " RecordStatus,RecuserID,RecDate)" & _
                " SELECT ParentCode,DocNo,DocDate,ReffNo,BankID,Sales,CurrencyKey,CurrencyRate,Description,TotalReceipt,TotalPoint," & _
                " Discount,AREventID,Cash,CashDesc,TTOnline,OnlineDesc,Cheque,ChequeNo,ChequeDueDate,CreditCard,CreditCardDesc," & _
                " Voucher,VoucherDesc,Deposit,DepositDesc,Other,OtherDesc," & _
                " '" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "', getdate()" & _
                " FROM AR_PAYHR WHERE DocNo = '" & mpKEY & "';"


        mlLOG = mlLOG & " INSERT INTO AR_PAYDTLOG (DocNo,InvNo,ReffNo,RecUDate)" & _
                        " SELECT DocNo,InvNo,ReffNo,,GETDATE() FROM AR_PAYDT WHERE DocNo = '" & mpKEY & "';"
        Return mlLOG
    End Function


    Function AR_DistToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO AR_DISTLOG (ParentCode,DistID,Name,UplineID,UplineName,RecruiterID,RecruiterName,UserID,Password," & _
                    " ReplikaID,JoinDate,ICNo,Reffno,Birth,Dob,Sex,Nation,Religion,NPWPNo,Mariage,Child,Address1,Address2,Address3," & _
                    " Address4,Zip,State,Country,Phone1,Phone2,Fax,Mobile1,Mobile2,Mobile3,Email,Website," & _
                    " MarriedStatus,SpouseID,SpouseName,SpouseICNo,SpouseBOD," & _
                    " Banker,AccNo,AccName,AccBranch,AccUnit," & _
                    " WarisID,WarisName,WarisStatus,Checked,GroupMenu,SpecialMember,MemberStatus,MemberStatus2," & _
                    " PostingUserID,PostingDate,PrintCounter,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                    " SELECT ParentCode,DistID,Name,UplineID,UplineName,RecruiterID,RecruiterName,UserID,Password," & _
                    " ReplikaID,JoinDate,ICNo,Reffno,Birth,Dob,Sex,Nation,Religion,NPWPNo,Mariage,Child,Address1,Address2,Address3," & _
                    " Address4,Zip,State,Country,Phone1,Phone2,Fax,Mobile1,Mobile2,Mobile3,Email,Website," & _
                    " MarriedStatus,SpouseID,SpouseName,SpouseICNo,SpouseBOD," & _
                    " Banker,AccNo,AccName,AccBranch,AccUnit," & _
                    " WarisID,WarisName,WarisStatus,Checked,GroupMenu,SpecialMember,MemberStatus,MemberStatus2," & _
                    " PostingUserID,PostingDate,PrintCounter," & _
                    " '" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "', getdate()" & _
                    " FROM AR_DIST WHERE DistID = '" & mpKEY & "' ;"
        Return mlLOG
    End Function


    Function IN_Inventory_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO IN_INMASTLOG (ParentCode,Itemkey,Description,Price1,Price2,Price3,FromDate,TilDate,PointValue," & _
                    " PacketStatus,Uom,Measurement,Weight,Uow,ItemGroup,Description2,RecordStatus,RecuserID,RecDate,RecUDate)" & _
                    " SELECT ParentCode,Itemkey,Description,Price1,Price2,Price3,FromDate,TilDate,PointValue,PacketStatus,Uom,Measurement,Weight,Uow," & _
                    " ItemGroup,Description2,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " getdate() FROM IN_INMAST WHERE Itemkey = '" & mpKEY & "' ;"
        Return mlLOG
    End Function


    Function IN_InventoryDT_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO IN_INGROUPLOG (Linno,Itemkey,ItemGroup,Description,Qty,RecordStatus,RecuserID,RecDate,RecUDate)" & _
                    " SELECT Linno,Itemkey,ItemGroup,Description,Qty,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " getdate() FROM IN_INGROUP WHERE Itemkey = '" & mpKEY & "' ;"
        Return mlLOG
    End Function

    Function XM_AddressBook_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO XM_ADDRESSBOOKLOG (AddressCode,AddressKey,Name,DistID,OwnerName,UplineID,UplineName,RecruiterID," & _
                    " RecruiterName,CommPercentage,PriceCode,ICNo,TaxID,Address,City,State,Country,Zip,Phone1,Phone2,Fax,Mobile1,Mobile2," & _
                    " Email,Website,JoinDate,CreditLimit,DefaultCurrency,DefaultTerm,DefaultSales,DefaultPIC,DefaultDiscHR," & _
                    " DefaultDiscDT,RecordStatus,Recuserid,Recdate,RecUDate)" & _
                    " SELECT AddressCode,AddressKey,Name,DistID,OwnerName,UplineID,UplineName,RecruiterID," & _
                    " RecruiterName,CommPercentage,PriceCode,ICNo,TaxID,Address,City,State,Country,Zip,Phone1,Phone2,Fax,Mobile1,Mobile2," & _
                    " Email,Website,JoinDate,CreditLimit,DefaultCurrency,DefaultTerm,DefaultSales,DefaultPIC,DefaultDiscHR," & _
                    " DefaultDiscDT,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " getdate() FROM XM_ADDRESSBOOK WHERE AddressKey = '" & mpKEY & "' ;"
        Return mlLOG
    End Function


    Function IN_InvBonus_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO IN_INBONUSLOG (ParentCode,Itemkey,ItemGroup,ItemFlag,FlagAmount,Bonus_1,Bonus_2,Bonus_3,Bonus_4,Bonus_5," & _
                    " FromDate,TilDate,PrintCounter,RecordStatus,RecuserID,RecDate,RecUDate)" & _
                    " SELECT ParentCode,Itemkey,ItemGroup,ItemFlag,FlagAmount,Bonus_1,Bonus_2,Bonus_3,Bonus_4,Bonus_5,FromDate,TilDate," & _
                    " PrintCounter,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " getdate() FROM IN_INBONUS WHERE Itemkey = '" & mpKEY & "' ;"
        Return mlLOG
    End Function


    Function MK_News_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO MK_NEWSLOG (SysID,Type,DocNo,DocDate,FromDate,TilDate,Subject,Header,Description,Description2,Description3,Description4," & _
                       " ImagePath1,ImagePath2,ImagePath3,ImagePath4,RecordStatus,Recuserid,RecDate,RecUDate)" & _
                    " SELECT SysID,Type,DocNo,DocDate,FromDate,TilDate,Subject,Header,Description,Description2,Description3,Description4," & _
                       " ImagePath1,ImagePath2,ImagePath3,ImagePath4,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " getdate() FROM MK_NEWS WHERE DocNo = '" & mpKEY & "' ;"
        Return mlLOG
    End Function

    Function MK_File_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO MK_FILELOG (SysID,Type,DocNo,DocDate,FromDate,TilDate,Subject,Header," & _
                   " FilePath1,FilePath2,RecordStatus,Recuserid,RecDate,RecUDate)" & _
                    " SELECT SysID,Type,DocNo,DocDate,FromDate,TilDate,Subject,Header," & _
                   " FilePath1,FilePath2,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " getdate() FROM MK_FILE WHERE DocNo = '" & mpKEY & "' ;"
        Return mlLOG
    End Function

    Function BB_BVMonthLock_ToLog(ByVal mpSYSID As String, ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO BB_BVMONTHCLOSELOG (SysID,SysIDDesc,BVMonth,Description,RecordStatus,Recuserid," & _
                    " RecDate,RecUDate)" & _
                    " SELECT SysID,SysIDDesc,BVMonth,Description,'" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " getdate() FROM BB_BVMONTHCLOSE WHERE SysID = '" & mpSYSID & "' AND BVMonth ='" & mpKEY & "';"
        Return mlLOG
    End Function

    Function IN_InventoryTransToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        'mlLOG = mlLOG & " INSERT INTO AR_INVHRLOG (ParentCode,SysID,DocNo,DocDate,LocationID,LocationName,MobileID,ReffNo," & _
        '                " DistID,DistName,BVMonth,Description,TotalBruto,TotalPoint,TotalAmount,PrintCounter,RecordStatus," & _
        '                " RecUserID,RecDate,PayNo,BVMONTH2,DistID2,DistName2,RecUDate)" & _
        '                " SELECT ParentCode,SysID,DocNo,DocDate,LocationID,LocationName,MobileID,ReffNo," & _
        '                " DistID,DistName,BVMonth,Description,TotalBruto,TotalPoint,TotalAmount,PrintCounter,'" & Trim(mpSTATUS) & "'," & _
        '                " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
        '                " PayNo,BVMONTH2,DistID2,DistName2,GETDATE() FROM AR_INVHR WHERE DocNo = '" & mpKEY & "';"

        'mlLOG = mlLOG & "INSERT INTO AR_INVDTLOG (DocNo,Linno,ItemKey,Description,Qty,QtyDO,UnitPoint,UnitPrice," & _
        '        " TotalPoint,TotalPrice,RecUDate)" & _
        '        " SELECT DocNo,Linno,ItemKey,Description,Qty,QtyDO,UnitPoint,UnitPrice, TotalPoint,TotalPrice,GETDATE()" & _
        '        " FROM AR_INVDT WHERE DocNo = '" & mpKEY & "';"

        Return mlLOG
    End Function



    Function BB_eWalletToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO BB_EWALLETHRLOG (ParentCode,Type,DocNo,DocDate,BVMonth,ReffNo,DistID,DistName,DistID2,DistName2,TotalPoint,TotalAmount," & _
                        " Description,ExpDate,PostingReffNo,PostingDesc,PostingUserID,PostingDate,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                        " SELECT ParentCode,Type,DocNo,DocDate,BVMonth,ReffNo,DistID,DistName,DistID2,DistName2,TotalPoint,TotalAmount," & _
                        " Description,ExpDate,PostingReffNo,PostingDesc,PostingUserID,PostingDate,'" & Trim(mpSTATUS) & "'," & _
                        " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM BB_EWALLETHR WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & "INSERT INTO BB_EWALLETDTLOG (DocNo,Linno,ItemKey,Description,Qty,UnitPoint,UnitPrice,TotalPoint,TotalPrice,RecUDate)" & _
                " SELECT DocNo,Linno,ItemKey,Description,Qty,UnitPoint,UnitPrice,TotalPoint,TotalPrice,GETDATE()" & _
                " FROM BB_EWALLETDT WHERE DocNo = '" & mpKEY & "';"

        Return mlLOG
    End Function

    Function BB_eWallet_Update(ByVal mpTASKTYPE As String, ByVal mpPARENTCODE2 As String, ByVal mpTYPE2 As String, ByVal mpDOCNO2 As String, ByVal mpDOCDATE2 As String, ByVal mpBVMONTH2 As String, ByVal mpREFFNO2 As String, ByVal mpDISTID2 As String, ByVal mpDISTNAME2 As String, ByVal mpDISTID22 As String, ByVal mpDISTNAME22 As String, ByVal mpTOTALPOINT2 As Double, ByVal mpTOTALAMOUNT2 As Double, ByVal mpDESCRIPTION2 As String, ByVal mpEXPDATE2 As String, ByVal mpRATE As Double, ByVal mpTOTALPOINT3 As Double, ByVal mpTOTALAMOUNT3 As Double, ByVal mpPOSTINGREFFNO2 As String, ByVal mpPOSTINGDESC2 As String, ByVal mpPOSTINGUSERID2 As String, ByVal mpPOSTINGDATE2 As String, ByVal mpRECORDSTATUS2 As String, ByVal mpUSERID2 As String, ByVal mpUSERDATE2 As String) As String
        Dim mlX As Integer
        Dim mlSQLE As String

        Dim mlEDOCDATE As String
        Dim mlEPOSTINGDATE As String
        Dim mlEEXPDATE As String


        mlEDOCDATE = mpDOCDATE2
        mlEPOSTINGDATE = mpPOSTINGDATE2
        mlEEXPDATE = mpEXPDATE2

        mlSQLE = ""
        mlSQLE = mlSQLE & " INSERT INTO BB_EWALLETHR (ParentCode,Type,DocNo,DocDate,BVMonth,ReffNo,DistID,DistName,DistID2,DistName2,TotalPoint,TotalAmount," & _
                     " Description,ExpDate,Rate,TotalPointR,TotalAmountR,PostingReffNo,PostingDesc,PostingUserID,PostingDate,RecordStatus,RecUserID,RecDate) VALUES ( " & _
                     " '" & mpPARENTCODE2 & "','" & mpTYPE2 & "','" & mpDOCNO2 & "','" & objMGF.FormatDate(mlEDOCDATE) & "'," & _
                     " '" & Trim(mpBVMONTH2) & "','" & Trim(mpREFFNO2) & "'," & _
                     " '" & Trim(mpDISTID2) & "','" & Trim(mpDISTNAME2) & "'," & _
                     " '" & Trim(mpDISTID22) & "','" & Trim(mpDISTNAME22) & "'," & _
                     " '" & mpTOTALPOINT2 & "','" & mpTOTALAMOUNT2 & "'," & _
                     " '" & Trim(mpDESCRIPTION2) & "','" & Trim(mpEXPDATE2) & "'," & _
                     " '" & Trim(mpRATE) & "','" & Trim(mpTOTALPOINT3) & "','" & Trim(mpTOTALAMOUNT3) & "'," & _
                     " '" & Trim(mpPOSTINGREFFNO2) & "','" & Trim(mpPOSTINGDESC2) & "','" & Trim(mpPOSTINGUSERID2) & "','" & mlEPOSTINGDATE & "'," & _
                     " '" & Trim(mpRECORDSTATUS2) & "','" & Trim(mpUSERID2) & "','" & objMGF.FormatDate(Now) & "'" & _
                     " );"

        mlX = 1
        mlSQLE = mlSQLE & " INSERT INTO BB_EWALLETDT (DocNo,Linno,ItemKey,Description,Qty,UnitPoint,UnitPrice,TotalPoint,TotalPrice," & _
                " Rate,TotalPointR,TotalPriceR)" & _
        " VALUES ( " & _
        " '" & mpDOCNO2 & "','" & mlX & "', '" & mpPARENTCODE2 & "','" & mpPARENTCODE2 & "'," & _
        " '" & mlX & "'," & _
        " '" & mpTOTALPOINT2 & "','" & mpTOTALPOINT2 & "'," & _
        " '" & mpTOTALAMOUNT2 & "','" & mpTOTALAMOUNT2 & "'," & _
        " '" & Trim(mpRATE) & "','" & Trim(mpTOTALPOINT3) & "','" & Trim(mpTOTALAMOUNT3) & "'" & _
        " );"

        Return mlSQLE
    End Function


    Function BB_FerateToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO BB_Feratelog (DocNo,DocDate,Type,RateMethod,BVMonthFrom,BVMonthTo,DateFrom,DateTo," & _
                        " CurrencyFrom,CurrencyTo,Rate,Description,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                        " SELECT DocNo,DocDate,Type,RateMethod,BVMonthFrom,BVMonthTo,DateFrom,DateTo," & _
                        " CurrencyFrom,CurrencyTo,Rate,Description,'" & Trim(mpSTATUS) & "'," & _
                        " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM BB_Ferate WHERE DocNo = '" & mpKEY & "';"
        Return mlLOG
    End Function

    Function AR_Shipping_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO AR_SHIPPINGCOSTLOG (ParentCode,DocNo,CargoID,Country,State,City,UOW,Price1,Price2,Price3," & _
                        " FromDate,TilDate,Description,Status,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                        " SELECT ParentCode,DocNo,CargoID,Country,State,City,UOW,Price1,Price2,Price3," & _
                        " FromDate,TilDate,Description,Status,'" & Trim(mpSTATUS) & "'," & _
                        " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM AR_SHIPPINGCOST WHERE DocNo = '" & mpKEY & "';"
        Return mlLOG
    End Function

    Function AR_DOShip_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO AR_DOSHIPLOG (ParentCode,DocNo,CargoID,Country,State,City,Address,UnitShip,Description,Status,RecordStatus," & _
                        " RecUserID,RecDate,RecUDate)" & _
                        " SELECT ParentCode,DocNo,CargoID,Country,State,City,Address,UnitShip,Description,Status,'" & Trim(mpSTATUS) & "'," & _
                        " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM AR_DOSHIP WHERE DocNo = '" & mpKEY & "';"
        Return mlLOG
    End Function

    Function XM_FILEHR_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO XM_FILEHR_LOG (ParentCode,SysID,DocNo,DocDate,GroupID,Description,RandomStr," & _
                        " RecordStatus,Recuserid,RecDate,RecUDate)" & _
                        " SELECT ParentCode,SysID,DocNo,DocDate,GroupID,Description,RandomStr," & _
                        " '" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " getdate() FROM XM_FILEHR WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & " INSERT INTO XM_FILEDT_LOG (DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword,RecUDate)" & _
                       " SELECT DocNo,Linno,FilePath,FileName,FileDesc,FileUserID,FilePassword," & _
                       " getdate() FROM XM_FILEDT WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & " INSERT INTO XM_FILEDTU_LOG (DocNo,Linno,Type,UserID,Name,TaskID,Description," & _
                       " Recuserid,RecDate,RecUDate)" & _
                       " SELECT DocNo,Linno,Type,UserID,Name,TaskID,Description," & _
                       " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                       " getdate() FROM XM_FILEDTU WHERE DocNo = '" & mpKEY & "';"

        Return mlLOG
    End Function

    Function XM_INBOX(ByVal mpPARAMDOC As String, ByVal mpSYSID As String, ByVal mpNEWDOCNO As String, ByVal mpREFFFUNCTIONPARAMETER As String, ByVal mpREFFDOCNO As String, ByVal mpREFFDOCDATE As Date, ByVal mpFROMID As String, ByVal mpFROMNAME As String, ByVal mpTOID As String, ByVal mpTONAME As String, ByVal mpPROCESSID As String, ByVal mpSUBJECT As String, ByVal mpDESCRIPTION As String) As String
        Dim mlLOG As String

        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO XM_INBOX (ParentCode,SysID,DocNo,DocDate,ReffParentCode,ReffDocNo,ReffDocDate,FromID,FromName," & _
                        " ToID,ToName,ProcessID,Subject,Description," & _
                        " RecordStatus,RecUserID,RecDate) VALUES ( " & _
                        " '" & mpPARAMDOC & "','" & mpSYSID & "','" & mpNEWDOCNO & "','" & objMGF.FormatDate(Now) & "'," & _
                        " '" & mpREFFFUNCTIONPARAMETER & "','" & mpREFFDOCNO & "','" & objMGF.FormatDate(mpREFFDOCDATE) & "'," & _
                        " '" & mpFROMID & "','" & mpFROMNAME & "'," & _
                        " '" & mpTOID & "','" & mpTONAME & "'," & _
                        " '" & mpPROCESSID & "','" & mpSUBJECT & "','" & mpDESCRIPTION & "'," & _
                        " 'New','" & mpFROMID & "','" & objMGF.FormatDate(Now) & "');"
        Return mlLOG
    End Function

    Function XM_Inbox_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO XM_INBOX_LOG (ParentCode,SysID,DocNo,DocDate,ReffParentCode,ReffDocNo,ReffDocDate,FromID,FromName," & _
                        " ToID,ToName,ProcessID,Subject,Description," & _
                        " RecordStatus,Recuserid,RecDate,RecUDate)" & _
                        " SELECT ParentCode,SysID,DocNo,DocDate,ReffParentCode,ReffDocNo,ReffDocDate,FromID,FromName," & _
                        " ToID,ToName,ProcessID,Subject,Description," & _
                        " '" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " getdate() FROM XM_INBOX WHERE DocNo = '" & mpKEY & "' ;"
        Return mlLOG
    End Function

    Function ISS_GetCompanyID(ByVal mpPARAM As String, ByVal mpCOMPANYID As String) As String
        Dim mlCOMPANYTABLENAME As String
        Dim mlCOMPANYID As String

        mlCOMPANYTABLENAME = "ISS Servisystem, PT$"
        mlCOMPANYID = "ISSN3"

        Select Case mpCOMPANYID
            Case "ISS"
                mlCOMPANYTABLENAME = "ISS Servisystem, PT$"
                mlCOMPANYID = "ISSN3"
            Case "IPM"
                mlCOMPANYTABLENAME = "ISS Parking Management$"
                mlCOMPANYID = "IPM3"
            Case "ICS"
                mlCOMPANYTABLENAME = "ISS CATERING SERVICES$"
                mlCOMPANYTABLENAME = "ISS Catering Service 5SP1$"
                mlCOMPANYID = "ICS5"
            Case "IFS"
                mlCOMPANYTABLENAME = "INTEGRATED FACILITY SERVICES$"
                mlCOMPANYID = "IFS3"
        End Select

        Select Case mpPARAM
            Case "1"
                Return mlCOMPANYID
            Case "2"
                Return mlCOMPANYTABLENAME
        End Select

    End Function

    Public Function ISS_INGeneralLostFocus(ByVal mpPARAM As String, ByVal mpLOSTTEXT As String, Optional ByVal mpCOMPANYID As String = "") As String
        Dim mlLOSTTEXT As String
        Dim rsINLOSTFOCUS As OleDb.OleDbDataReader
        Dim sqlINLOSTFOCUS As String
        Dim mlCOMPANYID As String
        Dim mlCOMPANYTABLENAME As String

        mlCOMPANYID = ISS_GetCompanyID("1", mpCOMPANYID)
        mlCOMPANYTABLENAME = ISS_GetCompanyID("2", mpCOMPANYID)

        ISS_INGeneralLostFocus = ""
        mlLOSTTEXT = Trim(mpLOSTTEXT)
        Select Case UCase(mpPARAM)
            Case "ITEMKEY"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "Item] WHERE No_ = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_INGeneralLostFocus = rsINLOSTFOCUS("Description")
            Case "UOM"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "Item] WHERE No_ = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_INGeneralLostFocus = rsINLOSTFOCUS("Base Unit of Measure")
        End Select
    End Function

    Public Function ISS_INGeneralLookup(ByVal mpPARAM As String, ByVal mpLOSTTEXT As String, Optional ByVal mpCOMPANYID As String = "") As OleDbDataReader
        Dim mlSQLIN As String
        Dim mlRSIN As OleDbDataReader
        Dim mlCOMPANYID As String
        Dim mlCOMPANYTABLENAME As String
        Dim mlLOSTTEXT As String

        mlCOMPANYID = ISS_GetCompanyID("1", mpCOMPANYID)
        mlCOMPANYTABLENAME = ISS_GetCompanyID("2", mpCOMPANYID)
        mlLOSTTEXT = Trim(mpLOSTTEXT)

        Try
            Select Case UCase(mpPARAM)
                Case "ITEMKEY"
                    mlSQLIN = "SELECT No_, Description FROM [" & mlCOMPANYTABLENAME & "Item] INV WHERE Description LIKE  '%" & mlLOSTTEXT & "%'"
                    mlRSIN = objMGS.DbRecordset(mlSQLIN, "PB", mlCOMPANYID)
            End Select

            Return mlRSIN
        Catch ex As Exception

        End Try
    End Function

    Public Function ISS_XMGeneralLostFocus(ByVal mpPARAM As String, ByVal mpLOSTTEXT As String, Optional ByVal mpCOMPANYID As String = "") As String
        Dim mlLOSTTEXT As String
        Dim rsINLOSTFOCUS As OleDb.OleDbDataReader
        Dim sqlINLOSTFOCUS As String
        Dim mlCOMPANYID As String
        Dim mlCOMPANYTABLENAME As String

        mlCOMPANYID = ISS_GetCompanyID("1", mpCOMPANYID)
        mlCOMPANYTABLENAME = ISS_GetCompanyID("2", mpCOMPANYID)
        
        ISS_XMGeneralLostFocus = ""
        mlLOSTTEXT = Trim(mpLOSTTEXT)
        Select Case UCase(mpPARAM)
            Case "SITECARD_DESC"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE LineNo_ = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_XMGeneralLostFocus = rsINLOSTFOCUS("SearchName")
            Case "SITECARD_LOC"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE LineNo_ = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_XMGeneralLostFocus = rsINLOSTFOCUS("SearchName")
            Case "SITECARD_ADDR_ALL"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE LineNo_ = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_XMGeneralLostFocus = rsINLOSTFOCUS("LAddress1") & vbNewLine & rsINLOSTFOCUS("LAddress2") & vbNewLine & rsINLOSTFOCUS("LAddress3") & vbNewLine & rsINLOSTFOCUS("LAddress4") & vbNewLine & rsINLOSTFOCUS("LCity") & rsINLOSTFOCUS("LPostCode")
            Case "CUST_DESC"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "Customer] WHERE No_ = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_XMGeneralLostFocus = rsINLOSTFOCUS("Search Name")
            Case "BRANCH_DESC"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "Location] WHERE [Code] = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_XMGeneralLostFocus = rsINLOSTFOCUS("Name")
            Case "BRANCH_DESC2"
                sqlINLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "Location] WHERE [Branch Location] = '" & mlLOSTTEXT & "'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
                rsINLOSTFOCUS.Read()
                If rsINLOSTFOCUS.HasRows Then ISS_XMGeneralLostFocus = rsINLOSTFOCUS("Name")

        End Select
    End Function

    Public Function ISS_XMGeneralLookup(ByVal mpPARAM As String, ByVal mpLOSTTEXT As String, Optional ByVal mpCOMPANYID As String = "") As OleDb.OleDbDataReader
        Dim mlLOSTTEXT As String
        Dim rsINLOSTFOCUS As OleDb.OleDbDataReader
        Dim sqlINLOSTFOCUS As String
        Dim mlCOMPANYTABLENAME As String
        Dim mlCOMPANYID As String

        mlCOMPANYID = ISS_GetCompanyID("1", mpCOMPANYID)
        mlCOMPANYTABLENAME = ISS_GetCompanyID("2", mpCOMPANYID)

        mlLOSTTEXT = Trim(mpLOSTTEXT)
        Select Case UCase(mpPARAM)
            Case "SITECARD_SEARCH"
                sqlINLOSTFOCUS = "SELECT LineNo_,SearchName FROM  [" & mlCOMPANYTABLENAME & "CustServiceSite] WHERE SearchName LIKE  '%" & mlLOSTTEXT & "%'"
                rsINLOSTFOCUS = objMGS.DbRecordset(sqlINLOSTFOCUS, "PB", mlCOMPANYID)
        End Select

        Return rsINLOSTFOCUS
    End Function


    Public Function ISS_APGeneralLostFocus(ByVal mpPARAM As String, ByVal mpLOSTTEXT As String, Optional ByVal mpCOMPANYID As String = "") As String
        Dim mlLOSTTEXT As String
        Dim rsAPLOSTFOCUS As OleDb.OleDbDataReader
        Dim sqlAPLOSTFOCUS As String
        Dim mlCOMPANYTABLENAME As String
        Dim mlCOMPANYID As String

        mlCOMPANYID = ISS_GetCompanyID("1", mpCOMPANYID)
        mlCOMPANYTABLENAME = ISS_GetCompanyID("2", mpCOMPANYID)
        
        ISS_APGeneralLostFocus = ""
        mlLOSTTEXT = Trim(mpLOSTTEXT)
        Select Case UCase(mpPARAM)
            Case "VENDOR"
                sqlAPLOSTFOCUS = "SELECT * FROM [" & mlCOMPANYTABLENAME & "Vendor] WHERE No_ = '" & mlLOSTTEXT & "'"
                rsAPLOSTFOCUS = objMGS.DbRecordset(sqlAPLOSTFOCUS, "PB", mlCOMPANYID)
                rsAPLOSTFOCUS.Read()
                If rsAPLOSTFOCUS.HasRows Then ISS_APGeneralLostFocus = rsAPLOSTFOCUS("Search Name")
        End Select
    End Function


    Function ISS_AP_MRTemplate_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO AP_MR_TEMPLATEHR_LOG (ParentCode,DocNo,DocDate,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                        " SELECT ParentCode,DocNo,DocDate,'" & Trim(mpSTATUS) & "'," & _
                        " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM AP_MR_TEMPLATEHR WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & "INSERT INTO AP_MR_TEMPLATEDT_LOG (ParentCode,DocNo,Linno,ItemKey,Description,Uom,Qty,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                " SELECT ParentCode,DocNo,Linno,ItemKey,Description,Uom,Qty,RecordStatus,RecUserID,RecDate,GETDATE()" & _
                " FROM AP_MR_TEMPLATEDT WHERE DocNo = '" & mpKEY & "';"

        Return mlLOG
    End Function

    Public Function ISS_ARGeneralLookup(ByVal mpPARAM As String, ByVal mpLOSTTEXT As String, Optional ByVal mpCOMPANYID As String = "") As OleDbDataReader
        Dim mlSQLIN As String
        Dim mlRSIN As OleDbDataReader
        Dim mlCOMPANYID As String
        Dim mlCOMPANYTABLENAME As String
        Dim mlLOSTTEXT As String

        mlCOMPANYID = ISS_GetCompanyID("1", mpCOMPANYID)
        mlCOMPANYTABLENAME = ISS_GetCompanyID("2", mpCOMPANYID)
        mlLOSTTEXT = Trim(mpLOSTTEXT)

        Try
            Select Case UCase(mpPARAM)
                Case "ITEMKEY"
                    mlSQLIN = "SELECT No_, Description FROM [" & mlCOMPANYTABLENAME & "Item] INV WHERE Description LIKE  '%" & mlLOSTTEXT & "%'"
                    mlRSIN = objMGS.DbRecordset(mlSQLIN, "PB", mlCOMPANYID)
            End Select

            Return mlRSIN
        Catch ex As Exception

        End Try
    End Function


    Function ISS_MR_MREntry_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String, ByVal mpUDOCNO As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO AP_MR_REQUESTHR_LOG (ParentCode,MRType,MRDesciption,DocNo,DocDate,SiteCardID,SiteCardName,Location,DeptID,BVMonth,Description,MRLine," & _
                        " PercentageMR,TotalPoint,TotalAmount," & _
                        " SC_State,SC_Branch,SC_BranchCode,SC_BranchName," & _
                        " DeptCode,Do_Address,Do_City,Do_State,Do_Country,Do_Zip,DO_Phone1,PIC_ContactNo," & _
                        " EntityID," & _
                        " PostingUserID1,PostingName1,PostingDate1," & _
                        " PostingUserID2,PostingName2,PostingDate2," & _
                        " PostingUserID3,PostingName3,PostingDate3," & _
                        " PostingUserID4,PostingName4,PostingDate4," & _
                        " PostingUserID5,PostingName5,PostingDate5," & _
                        " RecordStatus,RecUserID,RecDate,RecUDocNo,RecUUserID,RecUDate)" & _
                        " SELECT ParentCode,MRType,MRDesciption,DocNo,DocDate,SiteCardID,SiteCardName,Location,DeptID,BVMonth,Description,MRLine," & _
                        " PercentageMR,TotalPoint,TotalAmount," & _
                        " SC_State,SC_Branch,SC_BranchCode,SC_BranchName," & _
                        " DeptCode,Do_Address,Do_City,Do_State,Do_Country,Do_Zip,DO_Phone1,PIC_ContactNo," & _
                        " EntityID," & _
                        " PostingUserID1,PostingName1,PostingDate1," & _
                        " PostingUserID2,PostingName2,PostingDate2," & _
                        " PostingUserID3,PostingName3,PostingDate3," & _
                        " PostingUserID4,PostingName4,PostingDate4," & _
                        " PostingUserID5,PostingName5,PostingDate5," & _
                        " '" & Trim(mpSTATUS) & "'," & _
                        " RecUserID,RecDate," & _
                        " '" & Trim(mpUDOCNO) & "', '" & Trim(mpUSERID) & "'," & _
                        " GETDATE() FROM AP_MR_REQUESTHR WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & " INSERT INTO AP_MR_REQUESTDT_LOG (DocNo,Linno,ItemKey,Description,Uom,Qty,QtyDO," & _
                        " UnitPoint,UnitPrice,TotalPoint,TotalPrice," & _
                        " Qty_Bal,RequestDesc,Description2,Description3," & _
                        " RecUDocNo,RecUUserID,RecUDate)" & _
                        " SELECT DocNo,Linno,ItemKey,Description,Uom,Qty,QtyDO," & _
                        " UnitPoint,UnitPrice,TotalPoint,TotalPrice," & _
                        " Qty_Bal,RequestDesc,Description2,Description3," & _
                        " '" & Trim(mpUDOCNO) & "', '" & Trim(mpUSERID) & "'," & _
                        " GETDATE() FROM AP_MR_REQUESTDT WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & " INSERT INTO AP_MR_REQUESTDT2_LOG (DocNo,Linno,ItemKey,Description,fSize,Qty,QtyDO," & _
                        " RecUDocNo,RecUUserID,RecUDate)" & _
                        " SELECT DocNo,Linno,ItemKey,Description,fSize,Qty,QtyDO," & _
                        " '" & Trim(mpUDOCNO) & "', '" & Trim(mpUSERID) & "'," & _
                        " GETDATE() FROM AP_MR_REQUESTDT2 WHERE DocNo = '" & mpKEY & "';"
        Return mlLOG
    End Function

    Function ISS_IN_INADDINFO_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String

        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO IN_INMAST_ADDINFO_LOG (" & _
                    " ParentCode,ItemKey,Description,RandomStr," & _
                    " ImagePath1N,ImageName1N,ImagePath1W,ImageName1W,ImagePath1T,ImageName1T," & _
                    " RecordStatus,RecUserID,RecDate,RecUDate)" & _
                    " SELECT " & _
                    " ParentCode,ItemKey,Description,RandomStr," & _
                    " ImagePath1N,ImageName1N,ImagePath1W,ImageName1W,ImagePath1T,ImageName1T," & _
                    " '" & Trim(mpSTATUS) & "', '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "',GETDATE()" & _
                    " FROM IN_INMAST_ADDINFO WHERE ItemKey = '" & mpKEY & "';"


        Return mlLOG
    End Function

    Function ISS_IN_INADDINFO_APDOZONE_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO IN_INMAST_ADDINFO_APZONE_LOG (ParentCode,SiteCardID,SiteCardDesc,ItemKey,Description,VendID,VendName," & _
                    " RecordStatus,RecUserID,RecDate,RecUDate)" & _
                    " SELECT ParentCode,SiteCardID,SiteCardDesc,ItemKey,Description,VendID,VendName,'" & Trim(mpSTATUS) & "'," & _
                    " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " GETDATE() FROM IN_INMAST_ADDINFO_APZONE WHERE SiteCardID = '" & mpKEY & "';"
        Return mlLOG
    End Function

    Function ISS_OP_UserSiteCard_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO OP_USER_SITE_LOG (ParentCode,SysID,DocNo,DocDate,UserID,UserName,Linno,SiteCardID,SiteCardName,UserLevel,EntityID,RecordStatus,RecUserID,RecDate,RecUDate)" & _
                        " SELECT ParentCode,SysID,DocNo,DocDate,UserID,UserName,Linno,SiteCardID,SiteCardName,UserLevel,EntityID,'" & Trim(mpSTATUS) & "'," & _
                        " '" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM OP_USER_SITE WHERE DocNo = '" & mpKEY & "';"
        Return mlLOG
    End Function


    Function ISS_MR_UserLevel(ByVal mpUSERID As String, ByVal mpSITECARDPARAM As String) As String
        Dim mlSQLMR As String
        Dim mlRSMR As OleDbDataReader

        ISS_MR_UserLevel = "0"
        mlSQLMR = "SELECT Max(UserLevel) as UserLevel FROM OP_USER_SITE WHERE UserID = '" & Trim(mpUSERID) & "' AND (SiteCardID = '" & mpSITECARDPARAM & "' OR SiteCardID = 'ALL') AND RecordStatus='New'"
        mlRSMR = objMGS.DbRecordset(mlSQLMR, "PB", "ISSP3")
        If mlRSMR.HasRows = True Then
            mlRSMR.Read()
            If IsDBNull(mlRSMR("UserLevel")) = True Then
                Return "0"
            Else
                Return mlRSMR("UserLevel").ToString
            End If
        Else
            Return "0"
        End If
    End Function


    Function ISS_CT_ContractHR_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String, ByVal mpUSERNAME As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO CT_CONTRACTHR_LOG (" & _
                    " ParentCode,SysID,DocNo,DocDate,CustID,CustName,SiteCardID,SiteCardName,Address,City,State,Country,Zip," & _
                    " Phone1,PIC_ContactNo," & _
                    " ContractNo,ContractDate,ReffNo,ReffDocNo,StartDate,EndDate,ServiceType,EmployeeQty,ExistingEmployeeQty,IncrementPercent," & _
                    " ExistingPrice,ProposePrice,Price,Negotiator,SC_Branch,SC_BranchName," & _
                    " Description,FileDocNo,CompanyID," & _
                    " UMP,PricePerMP,UMPPercentage," & _
                    " NavJobNo,NavService," & _
                    " Tools1,Tools2," & _
                    " RecordStatus,RecUserID,RecName,RecDate" & _
                    " ,RecUDate)" & _
                    " SELECT " & _
                    " ParentCode,SysID,DocNo,DocDate,CustID,CustName,SiteCardID,SiteCardName,Address,City,State,Country,Zip," & _
                    " Phone1,PIC_ContactNo," & _
                    " ContractNo,ContractDate,ReffNo,ReffDocNo,StartDate,EndDate,ServiceType,EmployeeQty,ExistingEmployeeQty,IncrementPercent," & _
                    " ExistingPrice,ProposePrice,Price,Negotiator,SC_Branch,SC_BranchName," & _
                    " Description,FileDocNo,CompanyID," & _
                    " UMP,PricePerMP,UMPPercentage," & _
                    " NavJobNo,NavService," & _
                    " Tools1,Tools2," & _
                    " '" & Trim(mpSTATUS) & "','" & Trim(mpUSERID) & "','" & Trim(mpUSERNAME) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " GETDATE() FROM CT_CONTRACTHR WHERE DocNo = '" & mpKEY & "';"


        mlLOG = mlLOG & "INSERT INTO [CT_CONTRACT_JOB_LOG] (" & _
                        " ParentCode, SysID, DocNo, DocDate, Linno, ItemKey, Description, " & _
                        " RecordStatus,RecUserID,RecName,RecDate" & _
                        " ,RecUDate)" & _
                        " SELECT " & _
                        " ParentCode,SysID,DocNo,DocDate,Linno,ItemKey,Description," & _
                        " '" & Trim(mpSTATUS) & "','" & Trim(mpUSERID) & "','" & Trim(mpUSERNAME) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM CT_CONTRACT_JOB WHERE DocNo = '" & mpKEY & "';"

        mlLOG = mlLOG & "INSERT INTO [CT_CONTRACT_DT_LOG] (" & _
                        " ParentCode,SysID,DocNo,Linno,ItemKey,Description," & _
                        " RecordStatus,RecUserID,RecName,RecDate" & _
                        " ,RecUDate)" & _
                        " SELECT " & _
                        " ParentCode,SysID,DocNo,Linno,ItemKey,Description," & _
                        " '" & Trim(mpSTATUS) & "','" & Trim(mpUSERID) & "','" & Trim(mpUSERNAME) & "','" & objMGF.FormatDate(Now) & "'," & _
                        " GETDATE() FROM CT_CONTRACT_DT WHERE DocNo = '" & mpKEY & "';"

        Return mlLOG
    End Function

    Function ISS_UT_TRANSFERTASK_ToLog(ByVal mpKEY As String, ByVal mpSTATUS As String, ByVal mpUSERID As String) As String
        Dim mlLOG As String
        mlLOG = ""
        mlLOG = mlLOG & " INSERT INTO UT_TRANSFERTASK_LOG (" & _
                    " ParentCode,SysID,DocNo,DocDate,Linno,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName," & _
                    " UserID1,UserName1,Email1,UserID2,UserName2,Email2,DeadlineDate,Description," & _
                    " TaskStartDate,TaskEndDate,FileDocNo," & _
                    " GroupTask,LongTask, " & _
                    " RecordStatus,RecUserID,RecDate" & _
                    " ,RecUDate)" & _
                    " SELECT " & _
                    " ParentCode,SysID,DocNo,DocDate,Linno,ReffNo,ReffDocNo,CustID,CustName,SiteCardID,SiteCardName," & _
                    " UserID1,UserName1,Email1,UserID2,UserName2,Email2,DeadlineDate,Description," & _
                    " TaskStartDate,TaskEndDate,FileDocNo," & _
                    " GroupTask,LongTask, " & _
                    " '" & Trim(mpSTATUS) & "','" & Trim(mpUSERID) & "','" & objMGF.FormatDate(Now) & "'," & _
                    " GETDATE() FROM UT_TRANSFERTASK WHERE DocNo = '" & mpKEY & "';"
        Return mlLOG
    End Function

    Function AD_CHECKMENUSTYLE(mpMENUSTYLE As String, mpMASTERPAGEFORM As String) As String
        Dim mpMASTERPAGE As String = ""

        If (mpMENUSTYLE = "TREE") Then

            mpMASTERPAGE = "~/PageSetting/MsPageBlank.master"

        Else

            If (mpMASTERPAGEFORM = "~/PageSetting/MasterIntern.master") Then

                mpMASTERPAGE = mpMASTERPAGEFORM

            Else

                mpMASTERPAGE = "~/PageSetting/MasterIntern.master"

            End If
        End If

        Return mpMASTERPAGE
    End Function
       



End Class


