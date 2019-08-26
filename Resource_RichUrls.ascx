<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resource_RichUrls.ascx.cs" Inherits="Playngo.Modules.ClientZone.Resource_RichUrls" %>
			 <div class="Controls_Urls" data-name="<%=ClientName %>" data-default="<%=ShowDefault %>" data-visibility="<%=FieldItem.Direction == "1" ? 1:0 %>"  data-order="ID" data-orderby="1" data-search="">
                <asp:RadioButtonList ID="rblUrlLink" runat="server" CssClass="rblUrlLink" RepeatDirection="Vertical"></asp:RadioButtonList>
                <asp:TextBox ID="txtUrlLink" runat="server" Width="350" CssClass="txtUrlLink form-control input_text" placeholder="http://" ></asp:TextBox>
                <asp:DropDownList  ID="ddlUrlLink" runat="server" CssClass="ddlUrlLink  form-control input_text " style="width:auto !important;"></asp:DropDownList>
                <asp:Panel ID="panUrlLink" CssClass="panUrlLink" runat="server">
                    <asp:HiddenField runat="server" ID="hfUrlLink" />
                    <asp:Panel ID="div_Image" runat="server" CssClass="div_Image" style=" max-width:400px;">
            
                    </asp:Panel>    
  
                     <div class="nav nav-tabs Urls_Pictures"  id="Urls_Pictures" runat="server" style="display:none;">
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
                             <div class="dd handlelist" id="Urls_Pictures_div" >
                                <div class="listtitle" >
                                    <div class="row">
                                        <div class="col-xs-2"><a href="javascript:;" class="table-title" data-order="ID">ID <i class="fa fa-sort"></i></a></div>
                                        <div class="col-xs-3"><a href="javascript:;" class="table-title" data-order="FileName">Images <i class="fa fa-sort"></i></a></div> 
                                        <div class="col-xs-4"><a href="javascript:;" class="table-title" data-order="Name">Name <i class="fa fa-sort"></i></a></div>
                                        <div class="col-xs-2"><a href="javascript:;" class="table-title" data-order="FileExtension">Extension <i class="fa fa-sort"></i></a></div>
                                        <div class="col-xs-1 last"> </div>
                                    </div>
                                </div>
                                <ol class="dd-list listbox" id="Urls_Pictures_ol">
                                </ol>
                                <ul id="Urls_Pictures_page"></ul>
                             </div> 
		                    </div>
		                    <div class="tab-pane" id="panel_tab_add_<%=ClientName %>">
                                <div id="dropzone<%=ClientName %>" action="<%=ModulePath %>Resource_jQueryFileUpload.ashx?<%=QueryString %>" method="post"  class="dropzone" enctype="multipart/form-data"></div>
                                <script type="text/javascript">
                                    $(document).ready(function(){
                                        Dropzone.options.dropzone<%=ClientName %> = {
                                            acceptedFiles:"image/*,application/pdf,.psd,.zip,.rar,.doc,.xls,.ppt,.docx,.xlsx,.pptx,.txt,.mp3,.mp4,.flv,.mkv,<%=Playngo.Modules.ClientZone.FileSystemUtils.GetFileExtensions()%>",
                                            init: function () {
                                                this.on("queuecomplete", function (data) {
                                                      $("div.Controls_Urls").each(function (i, n) {
                                                             UrlLibrary.ReBind($(this).attr("data-name"), this);
                                                      });
                                                });
                                            }
                                        };
                                    });
                                </script>
		                    </div>
	                    </div>
	
                    </div>
                </asp:Panel>
            </div>