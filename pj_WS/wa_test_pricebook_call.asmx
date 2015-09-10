<%@ WebService Language="VB" Class="wa_test_pricebook_call" %>

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

<WebService(Namespace := "http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _  
Public Class wa_test_pricebook_call
    Inherits System.Web.Services.WebService 
    
    <WebMethod()> _
    Public Function CallSub(ByVal strbookid) As String
        
    End Function
    
    
End Class
