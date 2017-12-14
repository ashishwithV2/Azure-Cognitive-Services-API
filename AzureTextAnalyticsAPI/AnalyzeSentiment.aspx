<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyzeSentiment.aspx.cs" Inherits="AzureTextAnalyticsAPI.KeyPhrasesmethod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1></h1>
        </div>
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="10" Columns="100"></asp:TextBox>


    </div>
        <br /><br />
        <table>
       <tr>
           <td>
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
           </td>
           <td>
               <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
           </td>
       </tr>
            
       </table>
    </form>
</body>
</html>
