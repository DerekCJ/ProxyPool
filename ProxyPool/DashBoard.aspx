<%@ Page Title="工作台" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="t_refresh" runat="server" Interval="60000" OnTick="t_refresh_Tick"></asp:Timer>
            <div class="jumbotron">
                <h1>Proxy Pool</h1>
                <p class="lead">
                    代理池：<asp:Label ID="lb_pool_count" runat="server" Text="TBD" />；
                    代理源：<asp:Label ID="lb_source_count" runat="server" Text="TBD" />；
                    代理总数：<asp:Label ID="lb_all_proxy_count" runat="server" Text="TBD" />
                    有效代理数：<asp:Label ID="lb_active_proxy_count" runat="server" Text="TBD" />
<%--                    待验证代理数：<asp:Label ID="lb_toBeValid_count" runat="server" Text="TBD" />--%>
                    <br />
                    刷新时间：<asp:Label ID="lb_refresh_time" runat="server" Text="TBD" />
                </p>
                <p>
                    <asp:LinkButton ID="lkb_start_source" runat="server" CssClass="btn btn-primary btn-lg" OnClick="lkb_start_source_Click">启动代理源扫描</asp:LinkButton>
                    <asp:LinkButton ID="lkb_stop_source" runat="server" CssClass="btn btn-default btn-lg" OnClick="lkb_stop_source_Click">停止代理源扫描</asp:LinkButton>
                </p>
                <p>
                    <asp:LinkButton ID="lkb_start_pool" runat="server" CssClass="btn btn-primary btn-lg" OnClick="lkb_start_pool_Click" >启动代理池</asp:LinkButton>
                    <asp:LinkButton ID="lkb_stop_pool" runat="server" CssClass="btn btn-default btn-lg" OnClick="lkb_stop_pool_Click" >停止代理池</asp:LinkButton>
                    <asp:LinkButton ID="lkb_refresh" runat="server" CssClass="btn btn-default btn-lg" OnClick="lkb_refresh_Click">刷 新 数 据</asp:LinkButton>
                </p>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server"><ProgressTemplate>Processing...</ProgressTemplate></asp:UpdateProgress>
            </div>

            <div class="row">
                <asp:DataList ID="dl_Pool" runat="server" Width="100%">

                    <ItemTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <h3><%# Eval("pool_name") %>（状态：<%# Eval("status") %>）：缓存容量<%# Eval("cathe_count") %>,有效代理<%# Eval("active_count") %>,待验证代理<%# Eval("tbd_count") %></h3>
                                <div class="table-responsive">
                                    <%# Eval("pool_ip_table") %>
                                </div>
                                <p>
                                    <asp:LinkButton ID="lkb_more" runat="server" CssClass="btn btn-default" OnClick="lkb_more_Click" ToolTip='<%# Eval("pool_id") %>' CausesValidation="false">查看更多 &raquo;</asp:LinkButton>
                                    <asp:LinkButton ID="lkb_defult" runat="server" CssClass="btn btn-default" OnClick="lkb_defult_Click" ToolTip='<%# Eval("pool_id") %>' CausesValidation="false">恢复默认显示 &raquo;</asp:LinkButton>
                                </p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

