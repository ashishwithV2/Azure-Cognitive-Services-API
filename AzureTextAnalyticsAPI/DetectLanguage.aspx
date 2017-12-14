<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetectLanguage.aspx.cs" Inherits="AzureTextAnalyticsAPI.DetectLanguage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="BtnDetectLanuage" runat="server" Text="Detect language" OnClick="BtnDetectLanuage_Click" />
        <br />
        <asp:Label ID="lbltext" runat="server"  Text=""></asp:Label>

    </div>
    </form>
</body>
</html>
