<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_Events_Edit.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_Events_Edit" %>

<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12">
        <!-- start: PAGE TITLE & BREADCRUMB -->

        <div class="page-header">
            <h1><i class="fa clip-calendar"></i> <%=ViewResourceText("Header_Title", "Event Details")%></h1>
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB -->
    </div>
</div>
<!-- end: PAGE HEADER -->




<!-- start: PAGE CONTENT -->
<div class="row">
    <div class="col-sm-8">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="form-field-1">
                        <%=ViewTitle("lblTitle", "Title", "txtTitle")%>:</label>
                    <asp:TextBox ID="txtTitle" placeholder="Title" runat="server" CssClass="form-control validate[required,maxSize[500]]"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="form-field-2">
                        <%=ViewTitle("lblAddress", "Address", "txtAddress")%>:</label>
                    <asp:TextBox ID="txtAddress" placeholder="Address" CssClass="form-control limited validate[maxSize[500]]" runat="server"></asp:TextBox>
                </div>
               <div class="form-group events_edit_map">
                    <%=ViewControlTitle("lblLocation", "GoogleMap Location", "txtLocation", ":", "col-sm-3")%>
                    <div class="col-sm-3">
                         <asp:TextBox ID="txtLocationX" placeholder="" CssClass="form-control limited validate[custom[number]]" runat="server"></asp:TextBox>
                    </div>

                  
                    <div class="col-sm-3">
                         <asp:TextBox ID="txtLocationY" placeholder="" CssClass="form-control limited validate[custom[number]]" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        
                    </div>
                </div>
                <div class="row">

                </div>

                <div class="form-group">
                    <label for="form-field-3">
                        <%=ViewTitle("lblContentText", "Description", "txtContentText")%>:
                    </label>
                    <!-- <asp:HyperLink ID="hlAddMedia" resourcekey="hlAddMedia" runat="server" CssClass="btn btn-light-grey" Text="<i class='fa fa-picture-o'></i> Add Media"  data-toggle="modal" NavigateUrl="#Picture_Modal"></asp:HyperLink>-->
                    <asp:TextBox ID="txtContentText" CssClass="ckeditor_ form-control" runat="server"
                        Height="300" Width="100%" TextMode="MultiLine"></asp:TextBox>
                    <%--  <dnn:TextEditor id="txtContentTelerik" runat="server" height="400" width="100%"/>--%>
                </div>


            </div>
        </div>



        <div runat="server" id="divOptions" visible="false">
            <asp:Repeater ID="RepeaterGroup" runat="server" OnItemDataBound="RepeaterGroup_ItemDataBound">
                <ItemTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-external-link-square"></i>
                            <%#Eval("key")%>
                            <div class="panel-tools">
                                <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <asp:Repeater ID="RepeaterOptions" runat="server" OnItemDataBound="RepeaterOptions_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="form-group">
                                            <asp:Literal ID="liTitle" runat="server"></asp:Literal>
                                            <div class="col-sm-9">
                                                <asp:PlaceHolder ID="ThemePH" runat="server"></asp:PlaceHolder>
                                                <asp:Literal ID="liHelp" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <!-- end: TEXT AREA PANEL -->
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


        
         <%--Download Files--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i> <%=ViewResourceText("Title_DownloadFiles", "Download Files")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body">

                <div class="form-group">
                    <asp:HyperLink ID="hlSelectFiles" runat="server" data-toggle="modal" CssClass="btn btn-xs btn-bricky btn-download-files" NavigateUrl="#DownloadFiles_Modal"  Text="<i class='fa clip-list'></i> Select Files" resourcekey="hlSelectFiles"></asp:HyperLink>
                </div>
                <div class="form-group">
                    <div class="dd" id="nestable-download-files">
						<ol class="dd-list" id="nestable-ol-download-files">
                            <asp:Repeater ID="RepeaterFiles" runat="server" OnItemDataBound="RepeaterFiles_ItemDataBound">
                                <ItemTemplate>
                                 	<li class="dd-item dd3-item" data-id="<%#Eval("ID")%>" data-fileid="<%#Eval("DownloadID")%>">
										<div class="dd-handle dd3-handle"></div>
										<div class="dd3-content">
											<div class="row">
                                                <div class="col-sm-10"><%#Eval("Title")%></div>
                                                <div class="col-sm-2" style="text-align:right;">
                                                    <a class="btn btn-xs btn-link  btn-detele" href="javascript:;" data-id="<%#Eval("ID")%>">
											            <i class="fa fa-times"></i>
										            </a>
                                                </div>
											</div>
										</div>          
									</li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div>
                </div>
            </div>
        </div>



        <div class="panel panel-default" id="div_group_tags" runat="server">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i><%=ViewResourceText("Title_ExportCSV", "Export CSV List")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget">
                 <div class="form-group">
                     <asp:HyperLink ID="hlExportCSV" resourcekey="hlExportCSV" runat="server"  CssClass="btn btn-primary"  Text="<i class='fa clip-download'></i> DownLoad User List"></asp:HyperLink>
                 </div>
            </div>
        </div>





    </div>
    <div class="col-sm-4">
        <!-- start: SELECT BOX PANEL -->
        <!--Start-->
        <div class="panel panel-default">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i><%=ViewResourceText("Title_Publish", "Publish")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget">
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Button CssClass="btn btn-light-grey" ID="cmdSaveDraft" resourcekey="cmdSaveDraft"
                            runat="server" Text="Save Draft" CausesValidation="False" OnClick="cmdSaveDraft_Click"
                            OnClientClick="CancelValidation();"></asp:Button>
                    </div>
                    <div class="col-sm-6 text_right">
                        <asp:HyperLink ID="hlPreview" resourcekey="hlPreview" runat="server" CssClass="btn btn-light-grey" Text="<i class='fa clip-link'></i> Preview"
                            Visible="false" Target="_blank"></asp:HyperLink>
                    </div>
                </div>
                <ul class="Edit_List" id="accordion">
                    <li>
                        <p>
                            <i class="fa clip-grid-5"></i>&nbsp;<%=ViewResourceText("Title_Status", "Status")%>: <b>
                        </p>
                        <div class="panel-collapse" id="Status">
                            <div class="row form-group">
                                <div class="col-sm-6">
                                   
                                    <asp:DropDownList ID="ddlEventStatus" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <p>
                            <i class="fa clip-calendar-3"></i>&nbsp;<%=ViewResourceText("Title_Start", "Start")%>: <b>
                        </p>
                        <div class="panel-collapse" id="Start">
                            <div class="row form-group">
                                <div class="col-md-6 input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" data-date-format="mm/dd/yyyy" data-date-viewmode="years"
                                        CssClass="form-control date-picker"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                                <div id="divStartTime" class="col-md-5 input-group input-append bootstrap-timepicker">
                                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control time-picker"></asp:TextBox>
                                    <span class="input-group-addon add-on"><i class="fa fa-clock-o"></i></span>
                                </div>
                            </div>

                        </div>
                    </li>
                    <li>
                        <p>
                            <i class="fa clip-calendar-3"></i>&nbsp;<%=ViewResourceText("Title_Release", "Release")%>: <b>
                        </p>
                        <div class="panel-collapse" id="ReleaseDateTime">
                            <div class="row form-group">
                                <div class="col-md-6 input-group">
                                    <asp:TextBox ID="txtReleaseDate" runat="server" data-date-format="mm/dd/yyyy" data-date-viewmode="years"
                                        CssClass="form-control date-picker"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                                <div id="divReleaseDate" class="col-md-5 input-group input-append bootstrap-timepicker">
                                    <asp:TextBox ID="txtReleaseTime" runat="server" CssClass="form-control time-picker"></asp:TextBox>
                                    <span class="input-group-addon add-on"><i class="fa fa-clock-o"></i></span>
                                </div>
                            </div>

                        </div>
                    </li>
                    <li style="display:none;">
                        <p>
                            <i class="clip-stopwatch"></i>&nbsp;<%=ViewResourceText("Title_Disable", "Disable")%>: <b>
                        </p>
                        <div class="panel-collapse" id="DisableDateTime">
                            <div class="row form-group">
                                <div class="col-md-6 input-group">
                                    <asp:TextBox ID="txtDisableDate" runat="server" data-date-format="mm/dd/yyyy" data-date-viewmode="years"
                                        CssClass="form-control date-picker"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                                <div id="divDisableTime" class="col-md-5 input-group input-append bootstrap-timepicker">
                                    <asp:TextBox ID="txtDisableTime" runat="server" CssClass="form-control time-picker"></asp:TextBox>
                                    <span class="input-group-addon add-on"><i class="fa fa-clock-o"></i></span>
                                </div>
                            </div>

                        </div>
                    </li>

                    <li id="divFeature" runat="server">
                        <div class="panel-collapse" id="Feature">
                            <div class="row form-group">
                                <%=ViewControlTitle("lblIncludeNotification", "Include Notification", "cbIncludeNotification", ":", "col-sm-3 control-label")%>
                                <div class="col-sm-2">
                                    <div class="checkbox-table">
                                        <asp:CheckBox ID="cbIncludeNotification" runat="server" CssClass="flat-grey auto" /></div>
                                </div>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlNotificationStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li id="divTopStatus" runat="server">
                        <div class="panel-collapse" id="TopStatus">
                            <div class="row form-group">
                                 <%=ViewControlTitle("lblNotifySubscribers", "Notify Subscribers", "cbNotifySubscribers", ":", "col-sm-3 control-label")%>
                                <div class="col-sm-2">
                                    <div class="checkbox-table">
                                        <asp:CheckBox ID="cbNotifySubscribers" runat="server" CssClass="flat-grey auto" /></div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <p>
                            <i class="clip-stopwatch"></i>&nbsp;<%=ViewResourceText("Title_LastUpdated", "Last Updated")%>: 
                            <asp:Label ID="liLastUpdated" runat="server" Text="Immediately"></asp:Label>
                        </p>
                    </li>

                </ul>
                <div class="row">
                    <br />
                    <div class="col-sm-5">
                        <asp:Button CssClass="btn btn-link" ID="cmdDelete" resourcekey="cmdMovetoTrash" runat="server"
                            Text="Move to Trash" CausesValidation="False" OnClick="cmdDelete_Click" OnClientClick="CancelValidation();"></asp:Button>
                    </div>
                    <div class="col-sm-7 text_right">
                        <asp:Button CssClass="btn btn-primary btn-sm" lang="Submit" ID="cmdUpdate" resourcekey="cmdUpdate"
                            runat="server" Text="Update" OnClick="cmdUpdate_Click"></asp:Button>&nbsp;
                        <asp:Button CssClass="btn btn-primary btn-sm" ID="cmdCancel" resourcekey="cmdCancel"
                            runat="server" Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click"
                            OnClientClick="CancelValidation();"></asp:Button>&nbsp;
                    </div>
                </div>
            </div>
        </div>



    
        
        <!--Permissions-->
        <div class="panel panel-default" id="div_group_permissions" runat="server">
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
                            <asp:CheckBox ID="cbPermissionsAllUsers" runat="server" CssClass="auto" />
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

         <!--Jurisdictions-->
        <div class="panel panel-default" id="div1" runat="server">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewResourceText("Title_Jurisdictions", "Jurisdictions")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget form-horizontal checkbox-filter">
                <div class="form-group">
                    <%=ViewControlTitle("lblAllJurisdictions", "All Jurisdictions", "cbAllJurisdictions", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-9">
                        <div class="checkbox-inline">
                            <asp:CheckBox ID="cbAllJurisdictions" runat="server" CssClass="auto" />
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <%=ViewControlTitle("lblJurisdictions", "Jurisdictions", "cblJurisdictions", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-9">
                        <div class="checkbox-inline">
                            <asp:CheckBoxList ID="cblJurisdictions" runat="server" CssClass="auto">
                                <asp:ListItem Text="UK" Value="UK" ></asp:ListItem>
                                <asp:ListItem Text="Malta" Value="Malta" ></asp:ListItem>
                                <asp:ListItem Text="Asia" Value="Asia" ></asp:ListItem>
                                <asp:ListItem Text="Denmark" Value="Denmark" ></asp:ListItem>
                                <asp:ListItem Text="Italy" Value="Italy" ></asp:ListItem>
                                <asp:ListItem Text="CW" Value="CW" ></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <asp:HyperLink runat="server" ID="hlAddJurisdiction" resourcekey="hlAddJurisdiction" Text="Add new jurisdiction"></asp:HyperLink>
                    </div>
                </div>

            </div>
        </div>

        <!--Event Author-->
        <div class="panel panel-default" id="divEventAuthor" runat="server">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i><%=ViewResourceText("Title_EventAuthor", "Author")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget">
                <asp:HiddenField runat="server" ID="hfEventAuthor" />
                <div class="col-md-12 lead">
                    <asp:Image ID="imgEventAuthor" runat="server" Width="80" />
                    <asp:Label ID="lbEventAuthor" runat="server"></asp:Label>
                </div>
                <div class="col-md-7">
                    <asp:HyperLink runat="server" ID="hlEventAuthor" resourcekey="hlEventAuthor"
                        data-toggle="modal" NavigateUrl="#EventAuthor_Modal" Text="<i class='fa clip-user-plus'></i> Set Author"
                        ToolTip="Set Author" Target="_blank" CssClass="btn btn-light-grey"></asp:HyperLink>
                </div>
                <div id="EventAuthor_Modal" class="modal fade" tabindex="-1" data-width="820"
                    data-height="400" style="display: none;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;
                        </button>
                        <h4 class="modal-title">
                            <i class='fa clip-user-plus'></i>Set Author</h4>
                    </div>
                    <div class="modal-body">
                        <iframe id="EventAuthor_Iframe" width="100%" height="100%" style="border-width: 0px;"></iframe>
                    </div>
                </div>
                <%--<a href="#">Set featured image </a> --%>
            </div>
        </div>
        <!-- end: SELECT BOX PANEL -->
    </div>
    <!-- end: PAGE CONTENT-->
</div>



<div id="DownloadFiles_Modal" class="modal fade" tabindex="-1" data-width="820"
    data-height="400" style="display: none;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            &times;
        </button>
        <h4 class="modal-title">
            <i class='fa fa-folder-open'></i>Select Download Files</h4>
    </div>
    <div class="modal-body">
        <iframe id="DownloadFiles_Iframe" width="100%" height="100%" style="border-width: 0px;"></iframe>
    </div>
</div>
<script id="script-download-files" type="text/x-jquery-tmpl">
 <li class="dd-item dd3-item" data-id="${ID}" data-fileid="${DownloadID}">
	<div class="dd-handle dd3-handle"></div>
	<div class="dd3-content">
		<div class="row">
            <div class="col-sm-10">${Title}</div>
            <div class="col-sm-2" style="text-align:right;">
                <a class="btn btn-xs btn-link  btn-detele" href="javascript:;" data-id="${ID}">
					<i class="fa fa-times"></i>
				</a>
            </div>
		</div>
	</div>          
</li>
</script>


<script type="text/javascript">


    function ReturnUser(UserName, UserID, UserPic) {
        jQuery('#<%=lbEventAuthor.ClientID %>').text(UserName);
        jQuery('#<%=hfEventAuthor.ClientID %>').val(UserID);
        jQuery('#<%=imgEventAuthor.ClientID %>').attr("src", UserPic);
    }

    function InsetDownloads(jsons) {

        $.each(jsons, function (idx, obj) {

            obj["PageType"] = 4;
            obj["ItemID"] =  "<%=EventID%>";
            obj["DownloadID"] = obj["ID"];
            var $li = $("#nestable-ol-download-files li[data-fileid='" + obj["ID"] + "']");

            if (!$li.is("li"))
            {

                $.post("<%=GoServiceUrlByUpdateRelationPages()%>", obj, function (data) {

                    $("#nestable-ol-download-files").append($("#script-download-files").tmpl(JSON.parse(data)));
                });
            }
        });
    }

    jQuery(function ($) {

        $("#<%=hlAddMedia.ClientID %>").click(function () { $("#Picture_Iframe").attr("src", $(this).attr("data-href")); });
        $("#<%=hlEventAuthor.ClientID %>").click(function () { $("#EventAuthor_Iframe").attr("src", $(this).attr("data-href")); });
        $("a.btn-download-files").click(function () { $("#DownloadFiles_Iframe").attr("src", $(this).attr("data-href")); });

        CKEDITOR.replace('<%=txtContentText.ClientID %>', { height: '400px' });

        $("#nestable-ol-download-files").on("click", 'a.btn-detele', function (event) {
            if (confirm("<%=Localization.GetString("DeleteItem")%>")) {
                var id = $(this).data("id");
                $("#nestable-ol-download-files li[data-id='" + id + "']").hide("fast", function () {
                    $.getJSON("<%=GoServiceUrlByDeleteRelationPages()%>" + id, function (json) {
                        if (json["DeleteCount"] > 0) {
                            $("#nestable-ol-download-files li[data-id='" + id + "']").remove();
                        }
                    });
                });
            }
        });

        $('#nestable-download-files').nestable({ maxDepth: 1, group: 0 }).on('change', function (e) {
            var list = e.length ? e : $(e.target), IDs = '';
            if (window.JSON) {
                var jsons = list.nestable('serialize');
                $.each(jsons, function (name, value) {
                    IDs = IDs + (value.id + ",");
                });

                $.post("<%=GoServiceUrlBySortRelationPages()%>", { IDs: IDs }, function (data) {
                    //console.log('sort-data:',data);
                });

            }
        });

    });
</script>








