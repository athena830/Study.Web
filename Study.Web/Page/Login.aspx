<%@ Page Title="" Language="C#" MasterPageFile="~/Page/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Page_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <asp:HiddenField ID="hid_U_ID" runat="server" />
    <%--    IP--%>
    <asp:Label ID="lb_IP" runat="server" Visible="false"></asp:Label>
    <div class="message-container">
        <div class="messages" style="height:270px;">
            <div class="message">
                <figure class="avatar">
                    <img src="http://p2.wmpic.me/article/2015/06/30/1435643953_nzVXgCtE.jpg" />
                </figure>
                您好，這裡是客服系統，請問您的大名？
            </div>
            <div class="message message-personal">
                ......
            </div>
        </div>
    </div>
    <div class="message-box">
        <asp:TextBox ID="txt_Name" runat="server" CssClass="message-input"></asp:TextBox>
        <asp:Button ID="btn_Save" runat="server" Text="Start" OnClick="btn_Save_Click" CssClass="message-submit" />
    </div>
</asp:Content>

