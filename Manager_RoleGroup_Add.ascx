<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_RoleGroup_Add.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_RoleGroup_Add" %>

<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12"> 
        <!-- start: PAGE TITLE & BREADCRUMB -->
          
        <div class="page-header">
        <h1>
            <i class="clip-user-plus"></i> <%=ViewResourceText("Header_Title", "Add Role To Groups")%>
        </h1>
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
                        <%=ViewControlTitle("lblSelectGroup", "Select Group", "ddlSelectGroup", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <select id="ddlSelectGroup" name="ddlSelectGroup" class="form-control search-select-group validate[required]"></select>
                        </div>
                    </div>
                    <div class="form-group">
                         <div class="col-sm-3"></div>
                         <div class="col-sm-5">
                                <ul class="ul-group-roles"></ul>
                            </div>
                    </div>
                    <div class="form-group role-groups">
                        <%=ViewControlTitle("lblRoles", "Roles", "cblRoles", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="checkbox-inline">
                                <asp:CheckBoxList runat="server" ID="cblRoles" CssClass="grey auto"></asp:CheckBoxList>
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
      
    </div>
</div>

 

<script type="text/javascript">
    jQuery(function ($) {
        $(".search-select-group").select2({
            placeholder: 'Select Group',
            allowClear: true,
            ajax: {
                url: "<%=GoAjaxSearchToGroups()%>",
                dataType: 'json',
                data: function (params) { return { search: params.term }; },
                delay: 250,
                processResults: function (data, params) {
                    var results = data.Items;
                    return {
                        results: results  //必须赋值给results并且必须返回一个obj
                    };
                },

            }
        }).on("select2:select", function (e) {

            // e 的话就是一个对象 然后需要什么就 “e.参数” 形式 进行获取 

            if (e.params != null) {

                $("ul.ul-group-roles li").detach();
                $("#<%=cblRoles.ClientID%> input").iCheck('uncheck');

                $.each(e.params.data.roles, function (index, item) {
                    console.log(item);
                     $("ul.ul-group-roles").append("<li><span class=\"label label-info\"> " + item.RoleName + " </span></li>");
                    $("#<%=cblRoles.ClientID%> input[value='"+item.RoleID+"']").iCheck('check');
                });
            }
       


        });
    });
</script>