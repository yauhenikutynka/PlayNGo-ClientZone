﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_Settings.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_Dashboard_Settings" %>
<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12">
        <!-- start: PAGE TITLE & BREADCRUMB -->
        
        <div class="page-header">
            <h1>
                <i class="fa clip-wrench"></i>
                <%=ViewResourceText("Header_Title", "General Settings")%></h1>
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB -->
    </div>
</div>
<!-- end: PAGE HEADER -->
<!-- start: PAGE CONTENT -->
<div class="row">
    <div class="col-sm-12">
 
        <div class="panel panel-default">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewTitle("lblDataSettings", "Data Settings")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <%=ViewControlTitle("lblPortals", "Portals", "ddlPortals", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline">
                                <asp:DropDownList ID="ddlPortals" runat="server" CssClass="form-control input_text validate[required]"
                                    OnSelectedIndexChanged="ddlPortals_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <%=ViewControlTitle("lblTabModule", "Tab & Module", "ddlTabModule", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline">
                                <asp:DropDownList ID="ddlTabModule" runat="server" CssClass="form-control input_text validate[required]"></asp:DropDownList>
                                
                                </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
 
    </div>
</div>
<!-- end: PAGE CONTENT-->
<div class="row">
    <div class="col-sm-2">
    </div>
    <div class="col-sm-10">
        <asp:Button CssClass="btn btn-primary" lang="Submit" ID="cmdUpdate" resourcekey="cmdUpdate"
            runat="server" Text="Update" OnClick="cmdUpdate_Click"></asp:Button>&nbsp;
        <asp:Button CssClass="btn btn-default" ID="cmdCancel" resourcekey="cmdCancel" runat="server"
            Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click" OnClientClick="CancelValidation();">
        </asp:Button>&nbsp;
    </div>
</div>

