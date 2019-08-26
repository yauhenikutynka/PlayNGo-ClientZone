<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resource_MultiImages.ascx.cs" Inherits="Playngo.Modules.ClientZone.Resource_MultiImages" %>

<div class="controls_full_images" data-name="<%=ClientName %>" >
    <asp:HiddenField runat="server" ID="hfImages" Value="11,12,13,14" />
    <div class="gallery_widget_attached_images">
        <ul class="gallery_widget_attached_images_list ui-sortable"></ul>
    </div>
    <div class="gallery_widget_site_images"></div>
    <a class="gallery_widget_add_images" href="#multi_images<%=ClientName %>" title="Add images" data-toggle="modal"><i class="gallery-composer-icon gallery-c-icon-add"></i>Add images</a>
    <span class="gallery_description gallery_clearfix">Select images from media library.</span>
 
 

    <!-- start: BOOTSTRAP EXTENDED MODALS -->
		<div id="multi_images<%=ClientName %>" class="modal fade" tabindex="-1" data-width="760" style="display: none;">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">
					&times;
				</button>
				<h4 class="modal-title">Select Image</h4>
			</div>
			<div class="modal-body">
                <div class="control_images" data-name="<%=ClientName %>" data-order="ID" data-orderby="1" data-search="">
				 <%--模态窗口--%>
                <div class="nav nav-tabs multi_images"  id="multi_images" runat="server" >
                    <ul id="myTab2" class="nav nav-tabs tab-purple">
		                <li class="active">
			                <a href="#panel_tab_list_<%=ClientName %>" data-toggle="tab">
				                <i class="green fa clip-list"></i> List
			                </a>
		                </li>
		                <li class="">
			                <a href="#panel_tab_add_<%=ClientName %>" data-toggle="tab">
				                <i class="green fa clip-plus-circle"></i> Uploads
			                </a>
		                </li>
	                </ul>
	                <div class="tab-content">
		                <div class="tab-pane active" id="panel_tab_list_<%=ClientName %>">

                           <div class="sidebar-search list-search">
								<div class="form-group">
									<input type="text" placeholder="Search File Name ...">
									<a href="javascript:;" class="submit_but">
										<i class="clip-search-3"></i>
									</a>
								</div>
							</div>

                            <div class="dd handlelist" id="multi_images_div" >
                            <div class="listtitle" >
                                <div class="row">
                                    <div class="col-xs-2"><a href="javascript:;" class="table-title" data-order="ID">ID <i class="fa fa-sort"></i></a></div>
                                    <div class="col-xs-3"><a href="javascript:;" class="table-title" data-order="FileName">Images <i class="fa fa-sort"></i></a></div> 
                                    <div class="col-xs-4"><a href="javascript:;" class="table-title" data-order="Name">Name <i class="fa fa-sort"></i></a></div>
                                    <div class="col-xs-2"><a href="javascript:;" class="table-title" data-order="FileExtension">Extension <i class="fa fa-sort"></i></a></div>
                                    <div class="col-xs-1 last"> </div>
                                </div>
                            </div>
                            <ol class="dd-list listbox multi_images_ol" id="multi_images_ol">
                            </ol>
                            <ul id="multi_images_page" class="multi_images_page"></ul>
                            </div> 
		                </div>
		                <div class="tab-pane" id="panel_tab_add_<%=ClientName %>">
                            <div id="dropzone<%=ClientName %>" action="<%=ModulePath %>Resource_jQueryFileUpload.ashx?<%=QueryString %>" method="Event"  class="dropzone" enctype="multipart/form-data"></div>
                            <script type="text/javascript">
                                $(document).ready(function(){
                                        Dropzone.options.dropzone<%=ClientName %> = {
                                        init: function () {
                                            this.on("queuecomplete", function (data) {
                                                    $("div.control_images").each(function (i, n) {
                                                        ImagesLibrary.re_bind($(this).attr("data-name"), this);
                                                    });
                                            });
                                        }
                                    };
                                });
                            </script>
		                </div>
	                </div>
                </div> 
			</div>

		</div>
</div>



        
</div>