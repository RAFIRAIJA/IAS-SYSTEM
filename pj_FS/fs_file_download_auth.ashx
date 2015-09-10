<%@ WebHandler Language="VB" Class="fs_file_download_auth" %>
Imports System
Imports System.Data
Imports System.Data.OleDb

Public Class fs_file_download_auth : Implements IHttpHandler, IReadOnlySessionState
    Dim mlOBJGF As New IASClass.ucmGeneralFunction
    Dim mlOBJGS As New IASClass.ucmGeneralSystem
    Dim mlREADER As OleDb.OleDbDataReader
    Dim mlSQL As String
    Dim mlRSTEMP As OleDb.OleDbDataReader
    Dim mlSQLTEMP As String
    Dim mlPATHNORMAL As String
            
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        'If context.Session("mgUSERID") = "" Then
        '    context.Response.Write("Your Login was Expired")
        '    'Exit Sub            
        '    Dim mlVALIDATE As String
        '    mlVALIDATE = "31"
        '    mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
        '    context.Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
        'End If
        
                      
        mlOBJGS.Main()
        If mlOBJGS.ValidateExpiredDate() = True Then
            Exit Sub
        End If
        If context.Session("mgACTIVECOMPANY") = "" Then context.Session("mgACTIVECOMPANY") = mlOBJGS.mgCOMPANYDEFAULT
        mlOBJGS.mgACTIVECOMPANY = context.Session("mgACTIVECOMPANY")
        
        mlPATHNORMAL = "../"
        
        Dim mlDOCNO As String
        Dim mlLINNO As String
        dim mlTYPE AS String 
                
        Dim mlFILEPATH As String
        Dim mlFILENAME As String
        Dim mlFILEDESC As String
        
        mlDOCNO = context.Request("mpID")
        mlLINNO = context.Request("mpNO")
        mlTYPE = context.Request("mpTY")
        
        
        'mlDOCNO = "CU00000012"
        'mlTYPE = "GP"
        'mlLINNO = "1"
        'SESSION("mgUSERID") = "60467"
        'Session("mgGROUPMENU")
        
        SELECT CASE mlTYPE
            CASE "GP"
                mlSQL = "SELECT FilePath,FileName,FileDesc FROM XM_FILEDT WHERE DocNo = '" & mlDOCNO & "' AND Linno='" & mlLINNO & "'" & _
                    " AND DocNo in (SELECT DocNo FROM XM_FILEDTU WHERE GroupID = '" & context.Session("mgGROUPMENU") & "' AND RecordStatus='New' )"
                        
            CASE ""
                mlSQL = "SELECT FilePath,FileName,FileDesc FROM XM_FILEDT WHERE DocNo = '" & mlDOCNO & "' AND Linno='" & mlLINNO & "'" & _
                " AND DocNo in (SELECT DocNo FROM XM_FILEDTU WHERE UserID = '" & context.Session("mgUSERID") & "' AND RecordStatus='New' )"        
        END SELECT 
        'CONTEXT.Response.Write(mlSQL)
        
        
        
        mlREADER = mlOBJGS.DbRecordset(mlSQL, "PB", "ISSP3")
        If mlREADER.HasRows = True Then
            mlREADER.Read()
            mlFILEPATH = Trim(mlREADER("FilePath"))
            mlFILEPATH = mlPATHNORMAL & mlFILEPATH
            mlFILENAME = Trim(mlREADER("FileName"))
            mlFILEDESC = Trim(mlREADER("FileDesc"))
              
            
            Dim mlPROCESSID As String
            Dim mlPROCESS_SUBJECT As String
            Dim mlPROCESS_DESC As String
            Dim mlI As Integer
            
            mlI = 1
            mlSQLTEMP = "SELECT Max(Linno) as Linno FROM XM_FILEDTU WHERE DocNo = '" & mlDOCNO & "'"
            mlRSTEMP = mlOBJGS.DbRecordset(mlSQLTEMP, "PB", "ISSP3")
            If mlRSTEMP.HasRows Then
                mlRSTEMP.Read()
                If IsDBNull(mlRSTEMP("Linno")) = False Then
                    mlI = mlRSTEMP("Linno") + 1
                End If
            End If
            
            mlPROCESSID = "Download"
            mlPROCESS_SUBJECT = "Download Document No:" & mlDOCNO & "/" & mlLINNO & " : " & mlFILEDESC
            mlPROCESS_DESC = ""
            mlSQL = ""

            mlSQL = mlSQL & " INSERT INTO XM_FILEDTU " & _
                        " (DocNo,Linno,Type,UserID,Name," & _
                        " TaskID,Description," & _
                        " RecUserID,RecDate) VALUES ( " & _
                        " '" & mlDOCNO & "','" & mlI & "','1'," & _
                        " '" & context.Session("mgUSERID") & "','" & context.Session("mgNAME") & "', '" & mlPROCESSID & " ','" & mlPROCESS_SUBJECT & "'," & _
                        " '" & context.Session("mgUSERID") & "','" & Now & "');"
            mlOBJGS.ExecuteQuery(mlSQL, "PB", "ISSP3")
            
            
            context.Response.Buffer = True
            context.Response.Clear()
            context.Response.AddHeader("content-disposition", "attachment; filename=" + mlFILENAME)
            context.Response.ContentType = "application/octet-stream"
            context.Response.WriteFile(mlFILEPATH)
         
        Else
            'context.Response.Write("Authentication Fail")
            'Dim mlVALIDATE As String
            'mlVALIDATE = "31"
            'mlVALIDATE = mlOBJGF.Encrypt(mlVALIDATE)
            'context.Response.Redirect("../pageconfirmation.aspx?mpMESSAGE=" & mlVALIDATE)
        End If
        
        'context.Response.Write(mlSQL)
        context.Response.Write("Download Starting")
    End Sub
    
    
    Sub RedirectToErrorPage()
        
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class