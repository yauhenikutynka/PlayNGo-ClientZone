<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_Modal_DynamicModule.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_Modal_DynamicModule" %>
<div class="panel panel-default"  id="div1" runat="server">
    <div class="panel-heading">
        <i class="fa fa-external-link-square"></i>
        <%=ViewResourceText("Title_Permissions", "Basic")%>
        <div class="panel-tools">
            <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
        </div>
    </div>
    <div class="panel-body buttons-widget form-horizontal">
        <div class="form-group">
            
                <%=ViewControlTitle("lblTitle", "Title", "txtTitle", ":", "col-xs-3 control-label")%>
                <div class="col-xs-9"> <asp:TextBox ID="txtTitle" placeholder="Title" runat="server" CssClass="form-control validate[required,maxSize[500]]"></asp:TextBox></div>
        </div>
        <div class="form-group">
            <%=ViewControlTitle("lblIncludeTabLink", "Include TabLink", "cbIncludeTabLink", ":", "col-xs-3 control-label")%>
            <div class="col-xs-9">
                    <div class="checkbox-inline">
                    <asp:CheckBox ID="cbIncludeTabLink" runat="server" CssClass="auto"/>
                </div>
            </div>
        </div>
        <div class="form-group">
            <%=ViewControlTitle("lblPDFGenerator", "PDF Generator", "cbPDFGenerator", ":", "col-xs-3 control-label")%>
            <div class="col-xs-9">
                    <div class="checkbox-inline">
                    <asp:CheckBox ID="cbPDFGenerator" runat="server" CssClass="auto"/>
                </div>
            </div>
        </div>
    </div>
</div>


  <!--Permissions-->
<div class="panel panel-default"  id="div_group_permissions" runat="server">
    <div class="panel-heading">
        <i class="fa fa-external-link-square"></i>
        <%=ViewResourceText("Title_Permissions", "Permissions")%>
        <div class="panel-tools">
            <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
        </div>
    </div>
    <div class="panel-body buttons-widget form-horizontal checkbox-filter">
            <div class="form-group">
                <%=ViewControlTitle("lblPermissionsAllUsers", "All Users", "cbPermissionsAllUsers", ":", "col-sm-3 control-label")%>
                <div class="col-sm-9">
                        <div class="checkbox-inline">
                        <asp:CheckBox ID="cbPermissionsAllUsers" runat="server" CssClass="auto"/>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <%=ViewControlTitle("lblPermissionsRoles", "Permission Roles", "cblPermissionsRoles", ":", "col-sm-3 control-label")%>
                <div class="col-sm-9">
                    <div class="checkbox-inline">
                        <asp:CheckBoxList ID="cblPermissionsRoles" runat="server" CssClass="auto"></asp:CheckBoxList>
                    </div>
                </div>
            </div>
    </div>
</div>

<div class="row">

        <div class="col-sm-12">
        <asp:Button CssClass="btn btn-primary" lang="Submit" ID="cmdUpdate" resourcekey="cmdUpdate"
    runat="server" Text="Update" OnClick="cmdUpdate_Click"></asp:Button>&nbsp;
    <asp:Button CssClass="btn btn-default" ID="cmdCancel" resourcekey="cmdCancel" runat="server" Visible="false"
        Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click"  OnClientClick="CancelValidation();"></asp:Button>&nbsp;
        
        </div>
</div>