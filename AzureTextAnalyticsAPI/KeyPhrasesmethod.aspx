<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeyPhrasesmethod.aspx.cs" Inherits="AzureTextAnalyticsAPI.KeyPhrasesmethod1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:Button ID="BtnextractKey" runat="server" Text="Extract Key Phrases" OnClick="BtnextractKey_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
