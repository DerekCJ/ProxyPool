<%@ Page Title="验证方式配置" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProxyValidationMgmt.aspx.cs" Inherits="ProxyValidationMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active" runat="server" id="nav_1">
                    <asp:LinkButton ID="lkb_Mgmt" runat="server" OnClick="lkb_Mgmt_Click">管理验证方式</asp:LinkButton></li>
                <li role="presentation" runat="server" id="nav_2">
                    <asp:LinkButton ID="lkb_Add" runat="server" OnClick="lkb_Add_Click">新增验证方式</asp:LinkButton></li>
            </ul>
            <br />
            <asp:FormView ID="fv_pxy_vld" runat="server" Width="100%" DefaultMode="Edit" AllowPaging="True" DataKeyNames="vld_id" DataSourceID="sds_pxy_vld" OnItemInserting="fv_pxy_vld_ItemInserting" OnItemInserted="fv_pxy_vld_ItemInserted" OnItemUpdated="fv_pxy_vld_ItemUpdated">
                <EditItemTemplate>
                    <div class="form-group">
                        <label for="vld_id" class="col-sm-2 control-label">验证方式ID</label>
                        <asp:TextBox ID="vld_id" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_id") %>' placeholder="自增变量无需输入" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="vld_name" class="col-sm-2 control-label">验证方式名称</label>
                        <asp:TextBox ID="vld_name" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_name") %>' placeholder="验证方式名称" />
                    </div>
                    <div class="form-group">
                        <label for="vld_url" class="col-sm-2 control-label">验证使用的URL</label>
                        <asp:TextBox ID="vld_url" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_url") %>' placeholder="验证使用的URL" />
                    </div>
                    <div class="form-group">
                        <label for="vld_request_method" class="col-sm-2 control-label">请求模式</label>
                        <asp:TextBox ID="vld_request_method" runat="server" Text='<%# Bind("vld_request_method") %>' placeholder="Get/Post" ClientIDMode="Static" CssClass="form-control" ReadOnly="true" ToolTip="当前仅实现Get模式" />
                    </div>
                    <div class="form-group">
                        <label for="vld_pass_regex" class="col-sm-2 control-label">验证Regex</label>
                        <asp:TextBox ID="vld_pass_regex" runat="server" Text='<%# Bind("vld_pass_regex") %>' placeholder="用于验证是否获取目标内容的Regex" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="vld_timeout" class="col-sm-2 control-label">超时时间(ms)</label>
                        <asp:TextBox ID="vld_timeout" runat="server" Text='<%# Bind("vld_timeout") %>' placeholder="超时时间（毫秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="vld_attemps" class="col-sm-2 control-label">尝试次数</label>
                        <asp:TextBox ID="vld_attemps" runat="server" Text='<%# Bind("vld_attemps") %>' placeholder="尝试次数" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="vld_status" class="col-sm-2 control-label">状态</label>
                        <asp:TextBox ID="vld_status" runat="server" Text='<%# Bind("vld_status") %>' placeholder="该验证方式当前状态（0-未启用；1-已启用）" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="vld_create_time" class="col-sm-2 control-label">创建时间</label>
                        <asp:TextBox ID="vld_create_time" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_create_time") %>' placeholder="创建时间" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2">
                            <asp:Button ID="btn_test" runat="server" Text="测试一下" CausesValidation="True" CssClass="btn btn-default" OnClick="btn_test_Click" />
                            <asp:Button ID="btn_save" runat="server" Text="保存" CausesValidation="True" CssClass="btn btn-default" Enabled="false" ToolTip="测试成功后可保存" CommandName="Update" />
                        </div>
                    </div>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <div class="form-group">
                        <label for="vld_id" class="col-sm-2 control-label">验证方式ID</label>
                        <asp:TextBox ID="vld_id" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_id") %>' placeholder="自增变量无需输入" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="vld_name" class="col-sm-2 control-label">验证方式名称</label>
                        <asp:TextBox ID="vld_name" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_name") %>' placeholder="验证方式名称" />
                    </div>
                    <div class="form-group">
                        <label for="vld_url" class="col-sm-2 control-label">验证使用的URL</label>
                        <asp:TextBox ID="vld_url" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("vld_url") %>' placeholder="验证使用的URL" />
                    </div>
                    <div class="form-group">
                        <label for="vld_request_method" class="col-sm-2 control-label">请求模式</label>
                        <asp:TextBox ID="vld_request_method" runat="server" Text="Get" placeholder="Get/Post" ClientIDMode="Static" CssClass="form-control" ReadOnly="true" ToolTip="当前仅实现Get模式" />
                    </div>
                    <div class="form-group">
                        <label for="vld_pass_regex" class="col-sm-2 control-label">验证Regex</label>
                        <asp:TextBox ID="vld_pass_regex" runat="server" Text='<%# Bind("vld_pass_regex") %>' placeholder="用于验证是否获取目标内容的Regex" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="vld_timeout" class="col-sm-2 control-label">超时时间(ms)</label>
                        <asp:TextBox ID="vld_timeout" runat="server" Text='<%# Bind("vld_timeout") %>' placeholder="超时时间（毫秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="vld_attemps" class="col-sm-2 control-label">尝试次数</label>
                        <asp:TextBox ID="vld_attemps" runat="server" Text='<%# Bind("vld_attemps") %>' placeholder="尝试次数" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="vld_status" class="col-sm-2 control-label">状态</label>
                        <asp:TextBox ID="vld_status" runat="server" Text='<%# Bind("vld_status") %>' placeholder="该验证方式当前状态（0-未启用；1-已启用）" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2">
                            <asp:Button ID="btn_test" runat="server" Text="测试一下" CausesValidation="True" CssClass="btn btn-default" OnClick="btn_test_Click" />
                            <asp:Button ID="btn_save" runat="server" Text="保存" CausesValidation="True" CssClass="btn btn-default" Enabled="false" ToolTip="测试成功后可保存" CommandName="Insert" />
                            <asp:Button ID="btn_cancel" runat="server" Text="取消" CausesValidation="False" CssClass="btn btn-default" OnClick="btn_cancel_Click" />
                        </div>
                    </div>
                </InsertItemTemplate>
                <ItemTemplate>
                    vld_id:
                    <asp:Label ID="vld_idLabel" runat="server" Text='<%# Eval("vld_id") %>' />
                    <br />
                    vld_name:
                    <asp:Label ID="vld_nameLabel" runat="server" Text='<%# Bind("vld_name") %>' />
                    <br />
                    vld_url:
                    <asp:Label ID="vld_urlLabel" runat="server" Text='<%# Bind("vld_url") %>' />
                    <br />
                    vld_request_method:
                    <asp:Label ID="vld_request_methodLabel" runat="server" Text='<%# Bind("vld_request_method") %>' />
                    <br />
                    vld_pass_regex:
                    <asp:Label ID="vld_pass_regexLabel" runat="server" Text='<%# Bind("vld_pass_regex") %>' />
                    <br />
                    vld_timeout:
                    <asp:Label ID="vld_timeoutLabel" runat="server" Text='<%# Bind("vld_timeout") %>' />
                    <br />
                    vld_attemps:
                    <asp:Label ID="vld_attempsLabel" runat="server" Text='<%# Bind("vld_attemps") %>' />
                    <br />
                    vld_status:
                    <asp:Label ID="vld_statusLabel" runat="server" Text='<%# Bind("vld_status") %>' />
                    <br />
                    vld_create_time:
                    <asp:Label ID="vld_create_timeLabel" runat="server" Text='<%# Bind("vld_create_time") %>' />
                    <br />
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="编辑" />
                    &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" />
                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="新建" />
                </ItemTemplate>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:FormView>
            <asp:SqlDataSource ID="sds_pxy_vld" runat="server" ConnectionString="<%$ ConnectionStrings:proxy_pool %>"
                DeleteCommand="DELETE FROM [tb_validation] WHERE [vld_id] = @vld_id"
                InsertCommand="INSERT INTO [tb_validation] ([vld_name], [vld_url], [vld_request_method], [vld_pass_regex], [vld_timeout], [vld_attemps], [vld_status], [vld_create_time]) VALUES (@vld_name, @vld_url, @vld_request_method, @vld_pass_regex, @vld_timeout, @vld_attemps, @vld_status, GETDATE())"
                SelectCommand="SELECT * FROM [tb_validation]"
                UpdateCommand="UPDATE [tb_validation] SET [vld_name] = @vld_name, [vld_url] = @vld_url, [vld_request_method] = @vld_request_method, [vld_pass_regex] = @vld_pass_regex, [vld_timeout] = @vld_timeout, [vld_attemps] = @vld_attemps, [vld_status] = @vld_status, [vld_create_time] = @vld_create_time WHERE [vld_id] = @vld_id">
                <DeleteParameters>
                    <asp:Parameter Name="vld_id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="vld_name" Type="String" />
                    <asp:Parameter Name="vld_url" Type="String" />
                    <asp:Parameter Name="vld_request_method" Type="String" DefaultValue="Get" />
                    <asp:Parameter Name="vld_pass_regex" Type="String" />
                    <asp:Parameter Name="vld_timeout" Type="Int32" />
                    <asp:Parameter Name="vld_attemps" Type="Int32" />
                    <asp:Parameter Name="vld_status" Type="Int32" />
                    <asp:Parameter Name="vld_create_time" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="vld_name" Type="String" />
                    <asp:Parameter Name="vld_url" Type="String" />
                    <asp:Parameter Name="vld_request_method" Type="String" />
                    <asp:Parameter Name="vld_pass_regex" Type="String" />
                    <asp:Parameter Name="vld_timeout" Type="Int32" />
                    <asp:Parameter Name="vld_attemps" Type="Int32" />
                    <asp:Parameter Name="vld_status" Type="Int32" />
                    <asp:Parameter Name="vld_create_time" Type="DateTime" />
                    <asp:Parameter Name="vld_id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

