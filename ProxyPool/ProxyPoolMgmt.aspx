<%@ Page Title="代理池配置" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProxyPoolMgmt.aspx.cs" Inherits="ProxyPoolMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active" runat="server" id="nav_1">
                    <asp:LinkButton ID="lkb_Mgmt" runat="server" OnClick="lkb_Mgmt_Click">管理代理池</asp:LinkButton></li>
                <li role="presentation" runat="server" id="nav_2">
                    <asp:LinkButton ID="lkb_Add" runat="server" OnClick="lkb_Add_Click">新增代理池</asp:LinkButton></li>
            </ul>
            <br />
            <asp:FormView ID="fv_pxy_pool" runat="server" Width="100%" DefaultMode="Edit" AllowPaging="True" DataKeyNames="pool_id" DataSourceID="sds_pxy_pool" OnItemUpdated="fv_pxy_pool_ItemUpdated">
                <EditItemTemplate>
                    <div class="form-group">
                        <label for="iptId" class="col-sm-2 control-label">代理池ID</label>
                        <asp:TextBox ID="iptId" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pool_id") %>' placeholder="自增变量无需输入" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="iptName" class="col-sm-2 control-label">代理池名称</label>
                        <asp:TextBox ID="iptName" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pool_name") %>' placeholder="来源名称" />
                    </div>
                    <div class="form-group">
                        <label for="iptVldId" class="col-sm-2 control-label">验证方式ID</label>
                        <asp:TextBox ID="iptVldId" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_id") %>' placeholder="验证方式ID" />
                    </div>
                    <div class="form-group">
                        <label for="iptVldTimespan" class="col-sm-2 control-label">验证间隔时间(ms)</label>
                        <asp:TextBox ID="iptVldTimespan" runat="server" Text='<%# Bind("pool_validate_timespan") %>' placeholder="验证间隔时间（毫秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptVldThread" class="col-sm-2 control-label">验证使用的线程数</label>
                        <asp:TextBox ID="iptVldThread" runat="server" Text='<%# Bind("pool_validate_thread") %>' placeholder="验证使用的线程数" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptPoolSource" class="col-sm-2 control-label">从下述来源中采集</label>
                        <asp:TextBox ID="iptPoolSource" runat="server" Text='<%# Bind("pool_source") %>' placeholder="代理源ID清单，逗号分割" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptCatheSize" class="col-sm-2 control-label">缓存池最大容量</label>
                        <asp:TextBox ID="iptCatheSize" runat="server" Text='<%# Bind("pool_cathe_size") %>' placeholder="缓存池中保存的全部代理数量（包括有效与失效代理）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptPoolStatus" class="col-sm-2 control-label">状态</label>
                        <asp:TextBox ID="iptPoolStatus" runat="server" Text='<%# Bind("pool_status") %>' placeholder="代理池状态（0-未启用；1-已启用）" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptCTime" class="col-sm-2 control-label">创建时间</label>
                        <asp:TextBox ID="iptCTime" runat="server" Text='<%# Bind("pool_create_time") %>' placeholder="创建时间" ClientIDMode="Static" CssClass="form-control" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2">
                            <asp:Button ID="btn_save" runat="server" Text="保 存" CausesValidation="True" CssClass="btn btn-default" CommandName="Update" />
                        </div>
                    </div>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <div class="form-group">
                        <label for="iptId" class="col-sm-2 control-label">代理池ID</label>
                        <asp:TextBox ID="iptId" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pool_id") %>' placeholder="自增变量无需输入" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="iptName" class="col-sm-2 control-label">代理池名称</label>
                        <asp:TextBox ID="iptName" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pool_name") %>' placeholder="来源名称" />
                    </div>
                    <div class="form-group">
                        <label for="iptVldId" class="col-sm-2 control-label">验证方式ID</label>
                        <asp:TextBox ID="iptVldId" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_id") %>' placeholder="验证方式ID" />
                    </div>
                    <div class="form-group">
                        <label for="iptVldTimespan" class="col-sm-2 control-label">验证间隔时间(ms)</label>
                        <asp:TextBox ID="iptVldTimespan" runat="server" Text='<%# Bind("pool_validate_timespan") %>' placeholder="验证间隔时间（毫秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptVldThread" class="col-sm-2 control-label">验证使用的线程数</label>
                        <asp:TextBox ID="iptVldThread" runat="server" Text='<%# Bind("pool_validate_thread") %>' placeholder="验证使用的线程数" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptPoolSource" class="col-sm-2 control-label">从下述来源中采集</label>
                        <asp:TextBox ID="iptPoolSource" runat="server" Text='<%# Bind("pool_source") %>' placeholder="代理源ID清单，逗号分割" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptCatheSize" class="col-sm-2 control-label">启动库存代理数量</label>
                        <asp:TextBox ID="iptCatheSize" runat="server" Text='<%# Bind("pool_cathe_size") %>' placeholder="启动时从源历史库存中获取的代理数量" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptPoolStatus" class="col-sm-2 control-label">状态</label>
                        <asp:TextBox ID="iptPoolStatus" runat="server" Text='<%# Bind("pool_status") %>' placeholder="代理池状态（0-未启用；1-已启用）" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2">
                            <asp:Button ID="btn_save" runat="server" Text="确认新增" CausesValidation="True" CssClass="btn btn-default" CommandName="Insert" />
                        </div>
                    </div>
                </InsertItemTemplate>
                <ItemTemplate>
                    pool_id:
                    <asp:Label ID="pool_idLabel" runat="server" Text='<%# Eval("pool_id") %>' />
                    <br />
                    pool_name:
                    <asp:Label ID="pool_nameLabel" runat="server" Text='<%# Bind("pool_name") %>' />
                    <br />
                    vld_id:
                    <asp:Label ID="vld_idLabel" runat="server" Text='<%# Bind("vld_id") %>' />
                    <br />
                    pool_validate_timespan:
                    <asp:Label ID="pool_validate_timespanLabel" runat="server" Text='<%# Bind("pool_validate_timespan") %>' />
                    <br />
                    pool_validate_thread:
                    <asp:Label ID="pool_validate_threadLabel" runat="server" Text='<%# Bind("pool_validate_thread") %>' />
                    <br />
                    pool_source:
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("pool_source") %>' />
                    <br />
                    pool_cathe_size:
                    <asp:Label ID="pool_cathe_sizeLabel" runat="server" Text='<%# Bind("pool_cathe_size") %>' />
                    <br />
                    pool_status:
                    <asp:Label ID="pool_statusLabel" runat="server" Text='<%# Bind("pool_status") %>' />
                    <br />
                    pool_create_time:
                    <asp:Label ID="pool_create_timeLabel" runat="server" Text='<%# Bind("pool_create_time") %>' />
                    <br />
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="编辑" />
                    &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" />
                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="新建" />
                </ItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="sds_pxy_pool" runat="server" ConnectionString="<%$ ConnectionStrings:proxy_pool %>"
                DeleteCommand="DELETE FROM [tb_pool] WHERE [pool_id] = @pool_id"
                InsertCommand="INSERT INTO [tb_pool] ([pool_name], [vld_id], [pool_validate_timespan], [pool_validate_thread], [pool_source], [pool_cathe_size], [pool_status], [pool_create_time]) VALUES (@pool_name, @vld_id, @pool_validate_timespan, @pool_validate_thread, @pool_source, @pool_cathe_size, @pool_status,, GETDATE())"
                SelectCommand="SELECT * FROM [tb_pool]"
                UpdateCommand="UPDATE [tb_pool] SET [pool_name] = @pool_name, [vld_id] = @vld_id, [pool_validate_timespan] = @pool_validate_timespan, [pool_validate_thread] = @pool_validate_thread, [pool_source] = @pool_source, [pool_cathe_size] = @pool_cathe_size, [pool_status] = @pool_status WHERE [pool_id] = @pool_id">
                <DeleteParameters>
                    <asp:Parameter Name="pool_id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="pool_name" Type="String" />
                    <asp:Parameter Name="vld_id" Type="Int32" />
                    <asp:Parameter Name="pool_validate_timespan" Type="Int32" />
                    <asp:Parameter Name="pool_validate_thread" Type="Int32" />
                    <asp:Parameter Name="pool_source" Type="Int32" />
                    <asp:Parameter Name="pool_cathe_size" Type="Int32" />
                    <asp:Parameter Name="pool_status" Type="Byte" />
                    <asp:Parameter DbType="DateTime2" Name="pool_create_time" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="pool_name" Type="String" />
                    <asp:Parameter Name="vld_id" Type="Int32" />
                    <asp:Parameter Name="pool_validate_timespan" Type="Int32" />
                    <asp:Parameter Name="pool_validate_thread" Type="Int32" />
                    <asp:Parameter Name="pool_source" Type="String" />
                    <asp:Parameter Name="pool_cathe_size" Type="Int32" />
                    <asp:Parameter Name="pool_status" Type="Byte" />
                    <asp:Parameter Name="pool_id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

