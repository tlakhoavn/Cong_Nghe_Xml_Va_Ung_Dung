<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MH_TrangChu.aspx.cs" Inherits="UngdungWeb_KhachHang.MH_TrangChu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 768px; width: 1300px">
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Button ID="ID_BTN_TRANGCHU" runat="server" BackColor="#00CC99" BorderColor="#FF33CC" BorderStyle="None" BorderWidth="5px" ForeColor="Black" Height="40px" OnClick="ID_BTN_TRANGCHU_Click" Text="Trang chủ" Width="140px" />
        <p>
            &nbsp;</p>
        <asp:Panel ID="PanelBangPhongCacKhuVuc" runat="server" Height="374px">
        </asp:Panel>
        </form>
</body>
</html>
