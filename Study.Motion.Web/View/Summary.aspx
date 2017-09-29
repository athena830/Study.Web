<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="Study.Motion.Web.View.Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script src="../js/jquery.scrollTo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <%--    <asp:HiddenField ID="hid_DM_ID" runat="server" />
    <asp:HiddenField ID="hid_Score" runat="server" />--%>
    <div class="message-container">
        <div class="messages">
            <asp:ListView ID="lv_Dialog" runat="server" DataSourceID="objds_Get_DIALOG_DETAIL">
                <EmptyDataTemplate></EmptyDataTemplate>
                <LayoutTemplate>
                    <div id="itemplaceholder" runat="server"></div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="message message-personal">
                        <%# Eval("DD_Sentence") %>
                        <div class="timestamp"><%# DateTime.Parse(Eval("DD_CreateTime").ToString()).ToString("hh:mm") %></div>
                    </div>
                    <div class="message" <%# string.IsNullOrEmpty(Eval("DD_Reply").ToString()) ? "style='display:none;'" : "" %>>
                        <figure class="avatar">
                            <img src="http://p2.wmpic.me/article/2015/06/30/1435643953_nzVXgCtE.jpg" />
                        </figure>
                        "<font color="red"><%# Eval("DD_Sentence") %></font>"
                        <br/>
                        這句話是"<%# Eval("DD_Judgment").ToString() %>"，你同意嗎?
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="message-submit"
                                        OnCommand="On_Command" CommandName='<%# Eval("DD_ID") %>' CommandArgument="1"
                                        Enabled='<%# string.IsNullOrEmpty(Eval("DF_ID").ToString()) ? true : false %>'
                        >同意</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="message-submit"
                                        OnCommand="On_Command" CommandName='<%# Eval("DD_ID") %>' CommandArgument="-1"
                                        Enabled='<%# string.IsNullOrEmpty(Eval("DF_ID").ToString()) ? true : false %>'
                        >不同意</asp:LinkButton>
                        <ul class="message-emotion">
                            <li runat="server" style="display: none;"></li>
                            <li runat="server" style="display: none;"></li>
                            <li runat="server" class='<%# Convert.ToInt32(Eval("DD_Bad")) > Convert.ToInt32(Eval("DD_Good")) ? "active" : "" %>'></li>
                            <li runat="server" class='<%# Convert.ToInt32(Eval("DD_Bad")) == Convert.ToInt32(Eval("DD_Good")) ? "active" : "" %>'></li>
                            <li runat="server" class='<%# Convert.ToInt32(Eval("DD_Bad")) < Convert.ToInt32(Eval("DD_Good")) ? "active" : "" %>'></li>
                            <li runat="server" style="display: none;"></li>
                            <li runat="server" style="display: none;"></li>
                        </ul>
                        <%--<%# Eval("DD_Reply").ToString().Replace("\n", "<br />") %>--%>
                        <div class="timestamp"><%# DateTime.Parse(Eval("DD_UpdateTime").ToString()).ToString("hh:mm") %></div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <%--            <div class="message">
                <figure class="avatar">
                    <img src="http://p2.wmpic.me/article/2015/06/30/1435643953_nzVXgCtE.jpg" />
                </figure>
                您好，請問<asp:Literal ID="ll_Name" runat="server"></asp:Literal>有什麼需要服務的地方?
            </div>--%>
        </div>
        <%--        <ul class="message-emotion">
            <li runat="server" id="negativeFifteen"><span>-15</span></li>
            <li runat="server" id="negativeTen"><span>-10</span></li>
            <li runat="server" id="negativeFive"><span>-5</span></li>
            <li runat="server" id="zero"><span>0</span></li>
            <li runat="server" id="five"><span>+5</span></li>
            <li runat="server" id="ten"><span>+10</span></li>
            <li runat="server" id="fifteen"><span>+15</span></li>
        </ul>--%>
    </div>
    <div class="message-box">
        <%--        <asp:TextBox ID="txt_Sentence" runat="server" CssClass="message-input"></asp:TextBox>
        <asp:Button ID="btn_Submit" runat="server" Text="Send" OnClick="btn_Submit_Click" CssClass="message-submit" />--%>
    </div>

    <asp:ObjectDataSource ID="objds_Get_DIALOG_DETAIL" runat="server" SelectMethod="Get_DIALOG_DETAIL_List" TypeName="Study.Motion.Web.SQLFunc">
        <SelectParameters>
            <asp:Parameter Name="DD_DMID" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
