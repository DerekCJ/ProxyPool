<%@ Page Title="代理源配置" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProxySourceMgmt.aspx.cs" Inherits="ProxySourceMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active" runat="server" id="nav_1">
                    <asp:LinkButton ID="lkb_Mgmt" runat="server" OnClick="lkb_Mgmt_Click">管理代理来源</asp:LinkButton></li>
                <li role="presentation" runat="server" id="nav_2">
                    <asp:LinkButton ID="lkb_Add" runat="server" OnClick="lkb_Add_Click">新增代理来源</asp:LinkButton></li>
            </ul>
            <br />

            <asp:FormView ID="fv_pxy_src" runat="server" DataKeyNames="pxy_src_id" DataSourceID="sds_proxy_src" DefaultMode="Edit" Width="100%" AllowPaging="True" OnItemUpdated="fv_pxy_src_ItemUpdated" OnItemInserted="fv_pxy_src_ItemInserted" OnPageIndexChanged="fv_pxy_src_PageIndexChanged">
                <EditItemTemplate>
                    <div class="form-group">
                        <label for="iptId" class="col-sm-2 control-label">来源ID</label>
                        <asp:TextBox ID="iptId" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_id") %>' placeholder="自增变量无需输入" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="iptName" class="col-sm-2 control-label">来源名称</label>
                        <asp:TextBox ID="iptName" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_name") %>' placeholder="来源名称" />
                    </div>
                    <div class="form-group">
                        <label for="iptCatheSize" class="col-sm-2 control-label">缓存容量</label>
                        <asp:TextBox ID="iptCatheSize" runat="server" Text='<%# Bind("pxy_src_cathe_size") %>' placeholder="缓存容量" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptUrl" class="col-sm-2 control-label">URL</label>
                        <asp:TextBox ID="iptUrl" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_url") %>' placeholder="代理获取来源地址" />
                    </div>
                    <div class="form-group">
                        <label for="iptUrlPara" class="col-sm-2 control-label">URL参数</label>
                        <asp:TextBox ID="iptUrlPara" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_url_para") %>' placeholder="起始值,项数,步长|......" />
                    </div>
                    <div class="form-group">
                        <label for="iptRequestTimespan" class="col-sm-2 control-label">请求间隔时间(ms)</label>
                        <asp:TextBox ID="iptRequestTimespan" runat="server" Text='<%# Bind("pxy_src_request_timespan") %>' placeholder="请求间隔时间（秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptRefreshTimespan" class="col-sm-2 control-label">刷新间隔时间(ms)</label>
                        <asp:TextBox ID="iptRefreshTimespan" runat="server" Text='<%# Bind("pxy_src_refresh_timespan") %>' placeholder="请求间隔时间（秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptCharset" class="col-sm-2 control-label">字符集</label>
                        <asp:TextBox ID="iptCharset" runat="server" Text='<%# Bind("pxy_src_charset") %>' placeholder="字符集" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptRequestMethod" class="col-sm-2 control-label">请求模式</label>
                        <asp:TextBox ID="iptRequestMethod" runat="server" Text='<%# Bind("pxy_src_request_method") %>' placeholder="Get/Post" ClientIDMode="Static" CssClass="form-control" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="iptDocType" class="col-sm-2 control-label">返回类型</label>
                        <asp:TextBox ID="iptDocType" runat="server" Text='<%# Bind("pxy_src_doc_type") %>' placeholder="HTML/XML/JSON/TEXT" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptSrchType" class="col-sm-2 control-label">检索类型</label>
                        <asp:TextBox ID="iptSrchType" runat="server" Text='<%# Bind("pxy_src_srch_type") %>' placeholder="REGEX/XPATH" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptUrlSrch" class="col-sm-2 control-label">IP地址匹配规则</label>
                        <asp:TextBox ID="iptUrlSrch" runat="server" Text='<%# Bind("pxy_src_ip_add_srch") %>' placeholder="IP地址匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptPortSrch" class="col-sm-2 control-label">端口匹配规则</label>
                        <asp:TextBox ID="iptPortSrch" runat="server" Text='<%# Bind("pxy_src_port_srch") %>' placeholder="端口匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptProtocalSrch" class="col-sm-2 control-label">协议匹配规则</label>
                        <asp:TextBox ID="iptProtocalSrch" runat="server" Text='<%# Bind("pxy_src_protocal_srch") %>' placeholder="协议匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptRequesrMethodSrch" class="col-sm-2 control-label">请求模式匹配规则</label>
                        <asp:TextBox ID="iptRequesrMethodSrch" runat="server" Text='<%# Bind("pxy_src_request_method_srch") %>' placeholder="请求模式匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptLocationSrch" class="col-sm-2 control-label">服务器地点匹配规则</label>
                        <asp:TextBox ID="iptLocationSrch" runat="server" Text='<%# Bind("pxy_src_location_srch") %>' placeholder="服务器地点匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptTypeSrch" class="col-sm-2 control-label">匿名类型匹配规则</label>
                        <asp:TextBox ID="iptTypeSrch" runat="server" Text='<%# Bind("pxy_src_type_srch") %>' placeholder="匿名类型匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptNameSrch" class="col-sm-2 control-label">用户名匹配规则</label>
                        <asp:TextBox ID="iptNameSrch" runat="server" Text='<%# Bind("pxy_src_user_srch") %>' placeholder="用户名匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptPassSrch" class="col-sm-2 control-label">密码匹配规则</label>
                        <asp:TextBox ID="iptPassSrch" runat="server" Text='<%# Bind("pxy_src_pass_srch") %>' placeholder="密码匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptDomainSrch" class="col-sm-2 control-label">域匹配规则</label>
                        <asp:TextBox ID="iptDomainSrch" runat="server" Text='<%# Bind("pxy_src_domain_srch") %>' placeholder="域匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptCTime" class="col-sm-2 control-label">创建时间</label>
                        <asp:TextBox ID="iptCTime" runat="server" Text='<%# Bind("pxy_src_create_time") %>' placeholder="创建时间" ClientIDMode="Static" CssClass="form-control" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2">
                            <asp:Button ID="btn_test" runat="server" Text="测试一下" CausesValidation="True" CssClass="btn btn-default" OnClick="btn_test_Click" />
                            <asp:Button ID="btn_save" runat="server" Text="保存" CausesValidation="True" CssClass="btn btn-default" CommandName="Update" Enabled="false" />
                        </div>
                    </div>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <div class="form-group">
                        <label for="iptId" class="col-sm-2 control-label">来源ID</label>
                        <asp:TextBox ID="iptId" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_id") %>' placeholder="自增变量无需输入" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="iptName" class="col-sm-2 control-label">来源名称</label>
                        <asp:TextBox ID="iptName" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_name") %>' placeholder="来源名称" />
                    </div>
                    <div class="form-group">
                        <label for="iptCatheSize" class="col-sm-2 control-label">缓存容量</label>
                        <asp:TextBox ID="iptCatheSize" runat="server" Text='<%# Bind("pxy_src_cathe_size") %>' placeholder="缓存容量" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptUrl" class="col-sm-2 control-label">URL</label>
                        <asp:TextBox ID="iptUrl" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_url") %>' placeholder="代理获取来源地址" />
                    </div>
                    <div class="form-group">
                        <label for="iptUrlPara" class="col-sm-2 control-label">URL参数</label>
                        <asp:TextBox ID="iptUrlPara" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Bind("pxy_src_url_para") %>' placeholder="起始值,项数,步长|......" />
                    </div>
                    <div class="form-group">
                        <label for="iptRequestTimespan" class="col-sm-2 control-label">请求间隔时间(ms)</label>
                        <asp:TextBox ID="iptRequestTimespan" runat="server" Text='<%# Bind("pxy_src_request_timespan") %>' placeholder="请求间隔时间（秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptRefreshTimespan" class="col-sm-2 control-label">刷新间隔时间(ms)</label>
                        <asp:TextBox ID="iptRefreshTimespan" runat="server" Text='<%# Bind("pxy_src_refresh_timespan") %>' placeholder="请求间隔时间（秒）" ClientIDMode="Static" CssClass="form-control" type="number" />
                    </div>
                    <div class="form-group">
                        <label for="iptCharset" class="col-sm-2 control-label">字符集</label>
                        <asp:TextBox ID="iptCharset" runat="server" Text='<%# Bind("pxy_src_charset") %>' placeholder="字符集" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptRequestMethod" class="col-sm-2 control-label">请求模式</label>
                        <asp:TextBox ID="iptRequestMethod" runat="server" Text="Get" placeholder="Get/Post" ClientIDMode="Static" CssClass="form-control" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="iptDocType" class="col-sm-2 control-label">返回类型</label>
                        <asp:TextBox ID="iptDocType" runat="server" Text='<%# Bind("pxy_src_doc_type") %>' placeholder="HTML/XML/JSON/TEXT" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptSrchType" class="col-sm-2 control-label">检索类型</label>
                        <asp:TextBox ID="iptSrchType" runat="server" Text='<%# Bind("pxy_src_srch_type") %>' placeholder="REGEX/XPATH" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptUrlSrch" class="col-sm-2 control-label">IP地址匹配规则</label>
                        <asp:TextBox ID="iptUrlSrch" runat="server" Text='<%# Bind("pxy_src_ip_add_srch") %>' placeholder="IP地址匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptPortSrch" class="col-sm-2 control-label">端口匹配规则</label>
                        <asp:TextBox ID="iptPortSrch" runat="server" Text='<%# Bind("pxy_src_port_srch") %>' placeholder="端口匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptProtocalSrch" class="col-sm-2 control-label">协议匹配规则</label>
                        <asp:TextBox ID="iptProtocalSrch" runat="server" Text='<%# Bind("pxy_src_protocal_srch") %>' placeholder="协议匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptRequesrMethodSrch" class="col-sm-2 control-label">请求模式匹配规则</label>
                        <asp:TextBox ID="iptRequesrMethodSrch" runat="server" Text='<%# Bind("pxy_src_request_method_srch") %>' placeholder="请求模式匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptLocationSrch" class="col-sm-2 control-label">服务器地点匹配规则</label>
                        <asp:TextBox ID="iptLocationSrch" runat="server" Text='<%# Bind("pxy_src_location_srch") %>' placeholder="服务器地点匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptTypeSrch" class="col-sm-2 control-label">匿名类型匹配规则</label>
                        <asp:TextBox ID="iptTypeSrch" runat="server" Text='<%# Bind("pxy_src_type_srch") %>' placeholder="匿名类型匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptNameSrch" class="col-sm-2 control-label">用户名匹配规则</label>
                        <asp:TextBox ID="iptNameSrch" runat="server" Text='<%# Bind("pxy_src_user_srch") %>' placeholder="用户名匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptPassSrch" class="col-sm-2 control-label">密码匹配规则</label>
                        <asp:TextBox ID="iptPassSrch" runat="server" Text='<%# Bind("pxy_src_pass_srch") %>' placeholder="密码匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="iptDomainSrch" class="col-sm-2 control-label">域匹配规则</label>
                        <asp:TextBox ID="iptDomainSrch" runat="server" Text='<%# Bind("pxy_src_domain_srch") %>' placeholder="域匹配规则" ClientIDMode="Static" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2">
                            <asp:Button ID="btn_test" runat="server" Text="测试一下" CausesValidation="True" CssClass="btn btn-default" OnClick="btn_test_Click" />
                            <asp:Button ID="btn_save" runat="server" Text="确认新增" CausesValidation="True" CssClass="btn btn-default" CommandName="Insert" Enabled="false" />
                        </div>
                    </div>
                </InsertItemTemplate>
                <ItemTemplate>
                    pxy_src_id:
                    <asp:Label ID="pxy_src_idLabel" runat="server" Text='<%# Eval("pxy_src_id") %>' />
                    <br />
                    pxy_src_name:
                    <asp:Label ID="pxy_src_nameLabel" runat="server" Text='<%# Bind("pxy_src_name") %>' />
                    <br />
                    pxy_src_cathe_size:
                    <asp:Label ID="pxy_src_cathe_sizeLabel" runat="server" Text='<%# Bind("pxy_src_cathe_size") %>' />
                    <br />
                    pxy_src_url:
                    <asp:Label ID="pxy_src_urlLabel" runat="server" Text='<%# Bind("pxy_src_url") %>' />
                    <br />
                    pxy_src_url_para:
                    <asp:Label ID="pxy_src_url_paraLabel" runat="server" Text='<%# Bind("pxy_src_url_para") %>' />
                    <br />
                    pxy_src_request_timespan:
                    <asp:Label ID="pxy_src_request_timespanLabel" runat="server" Text='<%# Bind("pxy_src_request_timespan") %>' />
                    <br />
                    pxy_src_request_timespan:
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("pxy_src_request_timespan") %>' />
                    <br />
                    pxy_src_charset:
                    <asp:Label ID="pxy_src_charsetLabel" runat="server" Text='<%# Bind("pxy_src_charset") %>' />
                    <br />
                    pxy_src_request_method:
                    <asp:Label ID="pxy_src_request_methodLabel" runat="server" Text='<%# Bind("pxy_src_request_method") %>' />
                    <br />
                    pxy_src_doc_type:
                    <asp:Label ID="pxy_src_doc_typeLabel" runat="server" Text='<%# Bind("pxy_src_doc_type") %>' />
                    <br />
                    pxy_src_srch_type:
                    <asp:Label ID="pxy_src_srch_typeLabel" runat="server" Text='<%# Bind("pxy_src_srch_type") %>' />
                    <br />
                    pxy_src_ip_add_srch:
                    <asp:Label ID="pxy_src_ip_add_srchLabel" runat="server" Text='<%# Bind("pxy_src_ip_add_srch") %>' />
                    <br />
                    pxy_src_port_srch:
                    <asp:Label ID="pxy_src_port_srchLabel" runat="server" Text='<%# Bind("pxy_src_port_srch") %>' />
                    <br />
                    pxy_src_protocal_srch:
                    <asp:Label ID="pxy_src_protocal_srchLabel" runat="server" Text='<%# Bind("pxy_src_protocal_srch") %>' />
                    <br />
                    pxy_src_request_method_srch:
                    <asp:Label ID="pxy_src_request_method_srchLabel" runat="server" Text='<%# Bind("pxy_src_request_method_srch") %>' />
                    <br />
                    pxy_src_location_srch:
                    <asp:Label ID="pxy_src_location_srchLabel" runat="server" Text='<%# Bind("pxy_src_location_srch") %>' />
                    <br />
                    pxy_src_type_srch:
                    <asp:Label ID="pxy_src_type_srchLabel" runat="server" Text='<%# Bind("pxy_src_type_srch") %>' />
                    <br />
                    pxy_src_user_srch:
                    <asp:Label ID="pxy_src_user_srchLabel" runat="server" Text='<%# Bind("pxy_src_user_srch") %>' />
                    <br />
                    pxy_src_pass_srch:
                    <asp:Label ID="pxy_src_pass_srchLabel" runat="server" Text='<%# Bind("pxy_src_pass_srch") %>' />
                    <br />
                    pxy_src_domain_srch:
                    <asp:Label ID="pxy_src_domain_srchLabel" runat="server" Text='<%# Bind("pxy_src_domain_srch") %>' />
                    <br />
                    pxy_src_create_time:
                    <asp:Label ID="pxy_src_create_timeLabel" runat="server" Text='<%# Bind("pxy_src_create_time") %>' />
                    <br />
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="编辑" />
                    &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" />
                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="新建" />
                </ItemTemplate>
            </asp:FormView>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    Retrieving...
                </ProgressTemplate>
            </asp:UpdateProgress>

            <div runat="server" id="dv_testResult">
            </div>

            <asp:SqlDataSource ID="sds_proxy_src" runat="server" ConnectionString="<%$ ConnectionStrings:proxy_pool %>" 
                DeleteCommand="DELETE FROM [tb_proxy_source] WHERE [pxy_src_id] = @pxy_src_id" 
                InsertCommand="INSERT INTO [tb_proxy_source] ([pxy_src_name], [pxy_src_cathe_size], [pxy_src_url], [pxy_src_url_para], [pxy_src_request_timespan], [pxy_src_refresh_timespan], [pxy_src_charset], [pxy_src_request_method], [pxy_src_doc_type], [pxy_src_srch_type], [pxy_src_ip_add_srch], [pxy_src_port_srch], [pxy_src_protocal_srch], [pxy_src_request_method_srch], [pxy_src_location_srch], [pxy_src_type_srch], [pxy_src_user_srch], [pxy_src_pass_srch], [pxy_src_domain_srch], [pxy_src_create_time]) VALUES (@pxy_src_name, @pxy_src_cathe_size, @pxy_src_url, @pxy_src_url_para, @pxy_src_request_timespan, @pxy_src_refresh_timespan, @pxy_src_charset, @pxy_src_request_method, @pxy_src_doc_type, @pxy_src_srch_type, @pxy_src_ip_add_srch, @pxy_src_port_srch, @pxy_src_protocal_srch, @pxy_src_request_method_srch, @pxy_src_location_srch, @pxy_src_type_srch, @pxy_src_user_srch, @pxy_src_pass_srch, @pxy_src_domain_srch, GETDATE())" 
                SelectCommand="SELECT * FROM [tb_proxy_source]" 
                UpdateCommand="UPDATE [tb_proxy_source] SET [pxy_src_name] = @pxy_src_name, [pxy_src_cathe_size] = @pxy_src_cathe_size, [pxy_src_url] = @pxy_src_url, [pxy_src_url_para] = @pxy_src_url_para, [pxy_src_request_timespan] = @pxy_src_request_timespan, [pxy_src_refresh_timespan] = @pxy_src_refresh_timespan, [pxy_src_charset] = @pxy_src_charset, [pxy_src_request_method] = @pxy_src_request_method, [pxy_src_doc_type] = @pxy_src_doc_type, [pxy_src_srch_type] = @pxy_src_srch_type, [pxy_src_ip_add_srch] = @pxy_src_ip_add_srch, [pxy_src_port_srch] = @pxy_src_port_srch, [pxy_src_protocal_srch] = @pxy_src_protocal_srch, [pxy_src_request_method_srch] = @pxy_src_request_method_srch, [pxy_src_location_srch] = @pxy_src_location_srch, [pxy_src_type_srch] = @pxy_src_type_srch, [pxy_src_user_srch] = @pxy_src_user_srch, [pxy_src_pass_srch] = @pxy_src_pass_srch, [pxy_src_domain_srch] = @pxy_src_domain_srch WHERE [pxy_src_id] = @pxy_src_id">
                <DeleteParameters>
                    <asp:Parameter Name="pxy_src_id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="pxy_src_name" Type="String" />
                    <asp:Parameter Name="pxy_src_url" Type="String" />
                    <asp:Parameter Name="pxy_src_cathe_size" Type="Int32" />
                    <asp:Parameter Name="pxy_src_url_para" Type="String" />
                    <asp:Parameter Name="pxy_src_request_timespan" Type="Int32" />
                    <asp:Parameter Name="pxy_src_refresh_timespan" Type="Int32" />
                    <asp:Parameter Name="pxy_src_charset" Type="String" />
                    <asp:Parameter Name="pxy_src_request_method" Type="String" DefaultValue="Get" />
                    <asp:Parameter Name="pxy_src_doc_type" Type="String" />
                    <asp:Parameter Name="pxy_src_srch_type" Type="String" />
                    <asp:Parameter Name="pxy_src_ip_add_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_port_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_protocal_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_request_method_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_location_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_type_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_user_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_pass_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_domain_srch" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="pxy_src_name" Type="String" />
                    <asp:Parameter Name="pxy_src_cathe_size" Type="Int32" />
                    <asp:Parameter Name="pxy_src_url" Type="String" />
                    <asp:Parameter Name="pxy_src_url_para" Type="String" />
                    <asp:Parameter Name="pxy_src_request_timespan" Type="Int32" />
                    <asp:Parameter Name="pxy_src_refresh_timespan" Type="Int32" />
                    <asp:Parameter Name="pxy_src_charset" Type="String" />
                    <asp:Parameter Name="pxy_src_request_method" Type="String" />
                    <asp:Parameter Name="pxy_src_doc_type" Type="String" />
                    <asp:Parameter Name="pxy_src_srch_type" Type="String" />
                    <asp:Parameter Name="pxy_src_ip_add_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_port_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_protocal_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_request_method_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_location_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_type_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_user_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_pass_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_domain_srch" Type="String" />
                    <asp:Parameter Name="pxy_src_id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

