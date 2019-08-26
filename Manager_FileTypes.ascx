<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_FileTypes.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_FileTypes" %>

<!-- start: PAGE HEADER -->
      <div class="row">
        <div class="col-sm-12"> 
          <!-- start: PAGE TITLE & BREADCRUMB -->
          
          <div class="page-header">
            <h1><i class="fa fa-files-o"></i> <%=ViewResourceText("Header_Title", "File Types")%></h1>
          </div>
          <!-- end: PAGE TITLE & BREADCRUMB --> 
        </div>
      </div>
      <!-- end: PAGE HEADER --> 
      <!-- start: PAGE CONTENT -->
      
      <div class="row">
        <div class="col-sm-12">
          <div class="row">
            <div class="col-sm-6">
              <div class="form-group">
                <div class="row">
                  <div class="col-sm-12"><i class="fa fa-plus"></i> <%=ViewResourceText("Title_AddNew", "Add New File Type")%> </div>
                </div>
              </div>
              <div class="form-group">
                <%=ViewControlTitle("lblName", "Name", "txtName", ":", "")%>
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control validate[required]" placeholder="File Type Name"></asp:TextBox>
                <p><%=ViewHelp("lblName", "Enter the file type")%></p>
              </div>
              <div class="form-group">
                <%=ViewControlTitle("lblContentText", "Description", "txtContentText", ":", "")%>
                <asp:TextBox runat="server" ID="txtContentText" TextMode="MultiLine" Rows="3"  CssClass="form-control"></asp:TextBox>
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


              <div class="form-group">
                 <asp:Button CssClass="btn btn-primary" lang="Submit" ID="cmdUpdate" resourcekey="cmdUpdate"
                runat="server" Text="Update" OnClick="cmdUpdate_Click"></asp:Button>&nbsp;
                <asp:Button CssClass="btn btn-default" ID="cmdCancel" resourcekey="cmdCancel" runat="server"
                    Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click"  OnClientClick="CancelValidation();"></asp:Button>&nbsp;
                <asp:Button CssClass="btn btn-default" ID="cmdDelete" resourcekey="cmdDelete" runat="server"
                    Text="Delete"  CausesValidation="False" OnClick="cmdDeleteCategory_Click"  OnClientClick="CancelValidation();"></asp:Button>&nbsp;
             
              </div>



            </div>
            <div class="col-sm-6">
            	<div class="form-group">
            <div class="row">
               <div class="col-sm-12 input-group">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Width="100%" placeholder="Search Text Field" x-webkit-speech></asp:TextBox>
                <div class="input-group-btn">
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary" 
                      Text="<i class='fa fa-search'></i>" onclick="btnSearch_Click" />
				</div>
                
              </div>
            </div>
          </div>
              <div class="form-group">
                <div class="row">
                  <div class="col-sm-7">
                     <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control form_default">
                        <asp:ListItem Value="-1" Text="Bulk Actions" resourcekey="ddlStatus_BulkActions"></asp:ListItem>
                        <asp:ListItem Value="0" Text="Delete" resourcekey="ddlStatus_Delete"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnApply" resourcekey="btnApply" runat="server" CssClass="btn btn-default" Text="Apply" onclick="btnApply_Click" OnClientClick="CancelValidation();return ApplyStatus();" />
                  </div>
                  <div class="col-sm-5 text_right"> <br/>
                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label> </div>
                </div>
              </div>
              


              <asp:GridView ID="gvEventList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvEventList_RowDataBound" OnRowCreated="gvEventList_RowCreated" OnSorting="gvEventList_Sorting" AllowSorting="true"
                        Width="100%" CellPadding="0" cellspacing="0" border="0" CssClass="table table-striped table-bordered table-hover"  GridLines="none" >
                        <Columns>
                             <asp:TemplateField HeaderText="File Type">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlCategorie" runat="server" CssClass="" Target="_blank"></asp:HyperLink>
                             </ItemTemplate>
                             </asp:TemplateField>
                            <asp:BoundField DataField="ContentText" HeaderText="Description" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" /> 
                           
                            <asp:TemplateField HeaderText="Sort"  HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" >
                                <ItemTemplate>
                                    <div class="visible-md visible-lg hidden-sm hidden-xs">
                                        <asp:LinkButton ID="lbSortUp" runat="server" ToolTip="up" CssClass="btn btn-xs btn-default tooltips" data-original-title="Sort up" data-placement="top" Text="<i class='fa fa-arrow-up'></i>" OnClick="lbSort_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lbSortDown" runat="server" ToolTip="down" CssClass="btn btn-xs btn-default tooltips" data-original-title="Sort down" data-placement="top" Text="<i class='fa fa-arrow-down'></i>" OnClick="lbSort_Click"></asp:LinkButton>
                                     </div>
                                    
                             </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="center">
                                <ItemTemplate>
                                     <div class="visible-md visible-lg hidden-sm hidden-xs">
                                        <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-xs btn-teal tooltips" data-original-title="Edit" data-placement="top" Text="<i class='fa fa-edit'></i>"></asp:HyperLink>
                                        <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-xs btn-bricky tooltips" data-original-title="Remove" data-placement="top" Text="<i class='fa fa-times fa fa-white'></i>" OnClick="btnRemove_Click"></asp:LinkButton>
                                     </div>
                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                      <div class="btn-group"> <a href="#" data-toggle="dropdown" class="btn btn-primary dropdown-toggle btn-sm"> <i class="fa fa-cog"></i> <span class="caret"></span> </a>
                                        <ul class="dropdown-menu pull-right" role="menu">
                                        <li role="presentation"> <asp:LinkButton ID="lbMobileSortUp" runat="server" ToolTip="up" tabindex="-1" role="menuitem" data-placement="top" Text="<i class='fa clip-arrow-up-3'></i> up"  OnClick="lbSort_Click"></asp:LinkButton></li>
                                        <li role="presentation"> <asp:LinkButton ID="lbMobileSortDown" runat="server" ToolTip="down" tabindex="-1" role="menuitem" data-placement="top" Text="<i class='fa clip-arrow-down-3'></i> down"  OnClick="lbSort_Click"></asp:LinkButton></li>
                                          <li role="presentation">  <asp:HyperLink ID="hlMobileEdit" runat="server" tabindex="-1" role="menuitem" Text="<i class='fa fa-edit'></i> Edit"></asp:HyperLink></li>
                                          <li role="presentation"> <asp:LinkButton ID="btnMobileRemove" runat="server" tabindex="-1" role="menuitem" data-placement="top" Text="<i class='fa fa-times'></i> Remove"  OnClick="btnRemove_Click"></asp:LinkButton></li>
                                        </ul>
                                      </div>
                                    </div>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <PagerSettings Visible="False" />
                    </asp:GridView>
            </div>
          </div>
        </div>
      </div>
      <!-- end: PAGE CONTENT--> 
 



       <script type="text/javascript">
<!--

           function SelectAll() {
               var e = document.getElementsByTagName("input");
               var IsTrue;
               if (document.getElementById("CheckboxAll").value == "0") {
                   IsTrue = true;
                   document.getElementById("CheckboxAll").value = "1"
               }
               else {
                   IsTrue = false;
                   document.getElementById("CheckboxAll").value = "0"
               }
               for (var i = 0; i < e.length; i++) {
                   if (e[i].type == "checkbox") {
                       e[i].checked = IsTrue;
                   }
               }
           }
           function ApplyStatus() {
               var StatusSelected = $("#<%=ddlStatus.ClientID %>").find("option:selected").val();
               if (StatusSelected >= 0) {
                   var checkok = false;
                   $("#<%=gvEventList.ClientID %> input[type='checkbox'][type-item='true']").each(function (i, n) {
                       if ($(this).prop('checked')) {
                           checkok = true;
                       }
                   });

                   if (checkok) {
                       return confirm('<%=ViewResourceText("Confirm_ApplyStatus", "Are you sure to operate the records you choose?") %>');
                   }
                   alert('<%=ViewResourceText("Alert_NoItems", "Please operate with one line of record selected at least.") %>');

               } else {
                   alert('<%=ViewResourceText("Alert_NoActions", "Please choose the operation you need.") %>');
               }
               return false;
           }
   
 
// -->
    </script>