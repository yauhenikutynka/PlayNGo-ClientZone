<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_Help.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_Help" %>
<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12"> 
        <!-- start: PAGE TITLE & BREADCRUMB -->
          
        <div class="page-header">
            <h1>
                <i class="fa fa-exclamation-circle"></i> <%=ViewResourceText("Header_Title", "Help Settings")%>
                 
            </h1>
            
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB --> 
    </div>
</div>
<!-- end: PAGE HEADER --> 




<!-- start: PAGE CONTENT -->
<div class="row">
    <div class="col-sm-12">

        <!--
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="form-field-1">
                        <%=ViewTitle("lblTitle", "Title", "txtTitle")%>:</label>
                        <asp:TextBox ID="txtTitle" placeholder="Title" runat="server" CssClass="form-control validate[required,maxSize[500]]"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="form-field-2">
                        <%=ViewTitle("lblSummary", "Summary", "txtSummary")%>:</label>
                    <asp:TextBox ID="txtSummary" placeholder="Summary" CssClass="form-control limited validate[maxSize[2000]]"
                        runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="form-field-3">
                        <%=ViewTitle("lblContentText", "ContentText", "txtContentText")%>:
                    </label>
                    <asp:TextBox ID="txtContentText" CssClass="ckeditor_ form-control" runat="server"
                        Height="300" Width="100%" TextMode="MultiLine"></asp:TextBox>
                 
                </div>
            </div>
        </div>
        -->

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
                
        
        <%--Dynamic Modules Start--%>
        <div class="panel panel-default div-full-dynamic-modules">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i> <%=ViewResourceText("Title_DynamicModules", "Dynamic Modules")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body">

                <div class="form-group">
                   <asp:HyperLink ID="hlAddNewLink" runat="server" data-toggle="modal" CssClass="btn btn-xs btn-bricky btn-dynamic-module" NavigateUrl="#DynamicModule_Modal"  Text="<i class='fa fa-plus'></i> Add New Module" resourcekey="hlAddNewLink"></asp:HyperLink>
                    
                </div>

                <div class="form-group">

                    <div runat="server" id="divDynamicModules" class="div-dynamic-modules">
                        <asp:Repeater ID="RepeaterModules" runat="server" OnItemDataBound="RepeaterModules_ItemDataBound">
                            <ItemTemplate>
                                <div class="panel panel-default div-dynamic-module" data-id="<%#Eval("ID")%>">
                                    <div class="panel-heading">
                                        <i class="fa fa-reorder"></i>
                                        <div class="title"><%#Eval("Title")%></div> 
                                        <div class="panel-tools">

                                            <div class="btn-group">
										        <button data-toggle="dropdown" class="btn btn-xs btn-bricky dropdown-toggle">
											        <i class="fa fa-plus"></i>
											        Add Component <span class="caret"></span>
										        </button>
										        <ul class="dropdown-menu" role="menu">
											        <li>
                                                        <asp:HyperLink runat="server" ID="hlDynamicItemAddText" CssClass="btn-dynamic-item" data-toggle="modal" NavigateUrl="#DynamicItem_Modal" Text="<i class='fa fa-file-text-o'></i> Add New Text"></asp:HyperLink>
											        </li>
											        <li>
                                                        <asp:HyperLink runat="server" ID="hlDynamicItemAddImage" CssClass="btn-dynamic-item" data-toggle="modal" NavigateUrl="#DynamicItem_Modal" Text="<i class='fa clip-image'></i> Add New Image"></asp:HyperLink>
											        </li>
                                                    <li>
                                                        <asp:HyperLink runat="server" ID="hlDynamicItemAddImageText" CssClass="btn-dynamic-item" data-toggle="modal" NavigateUrl="#DynamicItem_Modal" Text="<i class='clip-images-3'></i> Add New Image / Text"></asp:HyperLink>
											        </li>
                                                    <li>
                                                        <asp:HyperLink runat="server" ID="hlDynamicItemAddVideo" CssClass="btn-dynamic-item" data-toggle="modal" NavigateUrl="#DynamicItem_Modal" Text="<i class='fa fa-film'></i> Add New Video"></asp:HyperLink>
											        </li>
											        <li>
                                                        <asp:HyperLink runat="server" ID="hlDynamicItemAddiFrame" CssClass="btn-dynamic-item" data-toggle="modal" NavigateUrl="#DynamicItem_Modal" Text="<i class='clip-link'></i> Add New iFrame"></asp:HyperLink>
											        </li>
										        </ul>
									        </div>

                                            <asp:HyperLink runat="server" ID="hlDynamicModuleEdit" CssClass="btn btn-xs btn-link btn-dynamic-module" data-toggle="modal" NavigateUrl="#DynamicModule_Modal" Text="<i class='fa fa-wrench'></i>"></asp:HyperLink>
                                     

                                            <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>

                                            <a class="btn btn-xs btn-link btn-module-detele" href="javascript:;" data-id="<%#Eval("ID")%>">
											    <i class="fa fa-times"></i>
										    </a>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-horizontal">
                                              <div class="dd nestable-dynamic-items" id="nestable-dynamic-items" data-id="<%#Eval("ID")%>">
										        <ol class="dd-list nestable-ol-dynamic-items" data-id="<%#Eval("ID")%>">
                                                    <asp:Repeater ID="RepeaterItems" runat="server" OnItemDataBound="RepeaterItems_ItemDataBound">
                                                        <ItemTemplate>
											                <li class="dd-item dd3-item" data-id="<%#Eval("ID")%>">
												                <div class="dd-handle dd3-handle"></div>
												                <div class="dd3-content">
													                <div class="row">
                                                                        <div class="col-sm-10"><%#Eval("Title")%>  ( <asp:Literal ID="liItemType" runat="server"></asp:Literal> )</div>
                                                                        
                                                                        <div class="col-sm-2" style="text-align:right;">

                                                                            <asp:HyperLink runat="server" ID="hlDynamicItemEdit" CssClass="btn btn-xs btn-link btn-dynamic-item" data-toggle="modal" NavigateUrl="#DynamicItem_Modal" Text="<i class='fa fa-wrench'></i>"></asp:HyperLink>
                                             
                                                                            <a class="btn btn-xs btn-link  btn-item-detele" href="javascript:;"  data-id="<%#Eval("ID")%>">
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
                                    <!-- end: TEXT AREA PANEL -->
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <%--Dynamic Modules End--%>


         

       

    </div>




     <!-- right div start-->
     <!-- right div end-->
    <!-- end: PAGE CONTENT-->
</div>
 



<script id="script-Dynamic-Items" type="text/x-jquery-tmpl">
<li class="dd-item dd3-item" data-id="${ID}">
	<div class="dd-handle dd3-handle"></div>
	<div class="dd3-content">
		<div class="row">
            <div class="col-sm-10">${Title}  ( ${TypeText} )</div>                                                    
            <div class="col-sm-2" style="text-align:right;">
                <a class="btn btn-xs btn-link btn-dynamic-item" href="#DynamicItem_Modal" data-toggle="modal" data-href="${EditUrl}"  data-id="${ID}">
					<i class='fa fa-wrench'></i>
				</a>         
                <a class="btn btn-xs btn-link  btn-item-detele" href="javascript:;"  data-id="${ID}">
					<i class="fa fa-times"></i>
				</a>
            </div>
		</div>
	</div>
</li>
</script>
<script id="script-Dynamic-Modules" type="text/x-jquery-tmpl">
<div class="panel panel-default div-dynamic-module" data-id="${ID}">
    <div class="panel-heading">
        <i class="fa fa-reorder"></i> <div class="title">${Title}</div> 
        <div class="panel-tools">
            <div class="btn-group">
				<button data-toggle="dropdown" class="btn btn-xs btn-bricky dropdown-toggle">
					<i class="fa fa-plus"></i>
					Add Component <span class="caret"></span>
				</button>
				<ul class="dropdown-menu" role="menu">
				    <li><a class="btn-dynamic-item" data-toggle="modal" data-href="${AddUrlText}" href="#DynamicItem_Modal"><i class='fa fa-file-text-o'></i> Add New Text</a></li>
					<li><a class="btn-dynamic-item" data-toggle="modal" data-href="${AddUrlImage}" href="#DynamicItem_Modal"><i class='fa clip-image'></i> Add New Image</a></li>
                    <li><a class="btn-dynamic-item" data-toggle="modal" data-href="${AddUrlImageText}" href="#DynamicItem_Modal"><i class='clip-images-3'></i> Add New Image / Text</a></li>
                    <li><a class="btn-dynamic-item" data-toggle="modal" data-href="${AddUrlVideo}" href="#DynamicItem_Modal"><i class='fa fa-film'></i> Add New Video</a></li>
					<li><a class="btn-dynamic-item" data-toggle="modal" data-href="${AddUrliFrame}" href="#DynamicItem_Modal"><i class='clip-link'></i> Add New iFrame</a></li>
				</ul>
			</div>
                      
            <a class="btn btn-xs btn-link btn-dynamic-module" href="#DynamicModule_Modal" data-toggle="modal" data-href="${EditUrl}"  data-id="${ID}">
				<i class='fa fa-wrench'></i>
			</a>  
            <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>

            <a class="btn btn-xs btn-link btn-module-detele" href="javascript:;" data-id="${ID}">
				<i class="fa fa-times"></i>
			</a>
        </div>
    </div>
    <div class="panel-body">
        <div class="form-horizontal">
            <div class="dd nestable-dynamic-items" id="nestable-dynamic-items" data-id="${ID}">
				<ol class="dd-list nestable-ol-dynamic-items" data-id="${ID}"></ol>
            </div>
        </div>
    </div>
</div>
</script>



<script type="text/javascript">


 

    function EditDynamicItems(json) {
        if (json["Action"] == "Insert") {
            $(".nestable-ol-dynamic-items[data-id='" + json["DynamicID"] + "']").append($("#script-Dynamic-Items").tmpl(json));
        } else if (json["Action"] == "Update") {
            $(".nestable-ol-dynamic-items li[data-id='" + json["ID"] + "']").html($("#script-Dynamic-Items").tmpl(json));
        }
        $('#DynamicItem_Modal').modal('hide');
    }


    function EditDynamicModules(json) {
        if (json["Action"] == "Insert") {
            $(".div-dynamic-modules").append($("#script-Dynamic-Modules").tmpl(json));
        } else if (json["Action"] == "Update") {
            $(".div-dynamic-modules div.div-dynamic-module[data-id='" + json["ID"] + "']").find("div.title").html(json["Title"]);
        }
        $('#DynamicModule_Modal').modal('hide');
    }





    jQuery(function ($) {
      
      

        $(".div-full-dynamic-modules").on("click", 'a.btn-dynamic-module', function (event) {
            $("#DynamicModule_Iframe").attr("src", $(this).attr("data-href"));
        });

        $(".div-full-dynamic-modules").on("click", 'a.btn-dynamic-item', function (event) {
            $("#DynamicItem_Iframe").attr("src", $(this).attr("data-href"));
        });
 



        $(".div-dynamic-modules").on("click", 'a.btn-item-detele', function (event) {
            if (confirm("<%=Localization.GetString("DeleteItem")%>"))
            {
                var id = $(this).data("id");
                $(".nestable-ol-dynamic-items li[data-id='" + id + "']").hide("fast", function () {
                    $.getJSON("<%=GoDynamicItemDelete()%>" + id, function (json) {
                        if (json["DeleteCount"] > 0)
                        {
                            $(this).remove();
                        }
                    });
                });
            }
        });


        $(".div-dynamic-modules").on("click", 'a.btn-module-detele', function (event) {
            if (confirm("<%=Localization.GetString("DeleteItem")%>"))
            {
                var id = $(this).data("id");
                $("div.div-dynamic-module[data-id='" + id + "']").hide("fast", function () {
                  $.getJSON("<%=GoDynamicModuleDelete()%>" + id, function (json) {
                        if (json["DeleteCount"] > 0)
                        {
                            $(this).remove();
                        }
                    });
                });
            }
        });
        

      

        $('.nestable-dynamic-items').each(function (index, domEle) {
            var dynamic_id =  $(this).data("id");
            $(domEle).nestable({ maxDepth: 1, group: dynamic_id }).on('change', function (e) {
                var list = e.length ? e : $(e.target);
               if (window.JSON) {
                    $.post("<%=GoDynamicItemsSort()%>" + dynamic_id,{"json":window.JSON.stringify( list.nestable('serialize'))}, function (data) {
                        //console.log("Data Loaded: " , data);
                    });
                }
            });
        });

 
        $('.div-dynamic-modules').DDSort({
            target: '.div-dynamic-module',	
            delay: 100,  
            floatStyle: {'border': '1px solid #ccc','background-color': '#fff'},
            up: function () {
                var json = [];
                $('.div-dynamic-modules > div.div-dynamic-module').each(function (index, domEle) {
                    json.push({'id':$(domEle).data('id')});
                    console.log($(domEle).data('id'));
                });
                $.post("<%=GoDynamicModulesSort()%>", { "json": window.JSON.stringify(json) }, function (data) {
                    console.log("Data Loaded: " , data);
                });
            }
        });







    });
</script>




