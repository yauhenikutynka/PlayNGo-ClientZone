<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_GameSheets.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_GameSheets" %>
<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12"> 
        <!-- start: PAGE TITLE & BREADCRUMB -->
          
        <div class="page-header">
        <h1><i class="fa clip-list-4"></i> <%=ViewResourceText("Header_Title", "All Game Sheets")%>
            <asp:HyperLink ID="hlAddNewLink" runat="server" CssClass="btn btn-xs btn-bricky" Text="<i class='fa fa-plus'></i> Add New " resourcekey="hlAddNewLink"></asp:HyperLink>
        </h1>
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB --> 
    </div>
</div>
<!-- end: PAGE HEADER --> 



 <!-- start: PAGE CONTENT -->
      
      <div class="row">
        <div class="col-sm-12">
          <div class="form-group">
            <div class="row">
              <div class="col-sm-8 ">
               
                  <div class="btn-group">
                    <asp:HyperLink runat="server" ID="hlAllEvent" CssClass="btn btn-default" Text="All"></asp:HyperLink> 
                    <asp:HyperLink runat="server" ID="hlPublishedEvent" CssClass="btn btn-default" Text="Published"></asp:HyperLink>  
                    <asp:HyperLink runat="server" ID="hlPendingEvent" CssClass="btn btn-default" Text="Pending"></asp:HyperLink>   
                    <asp:HyperLink runat="server" ID="hlDraftsEvent" CssClass="btn btn-default" Text="Drafts"></asp:HyperLink> 
                    <asp:HyperLink runat="server" ID="hlRecycleBinEvent" CssClass="btn btn-default" Text="Recycle Bin"></asp:HyperLink>
                  </div>
              </div>
              <div class="col-sm-4 input-group text_right">
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
              <div class="col-sm-9">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control form_default">
                    <asp:ListItem Value="-1" Text="Bulk Actions"  resourcekey="ddlStatus_BulkActions"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Published" resourcekey="ddlStatus_Published"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Pending" resourcekey="ddlStatus_Pending"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Delete" resourcekey="ddlStatus_Delete"></asp:ListItem>
                </asp:DropDownList>
                <asp:LinkButton ID="btnApply" runat="server" CssClass="btn btn-default" Text="Apply" resourcekey="btnApply" onclick="btnApply_Click" OnClientClick="return ApplyStatus();" />
              </div>
              <div class="col-sm-3 text_right"> <br/>
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label> </div>
            </div>
          </div>
          <div class="form-group">
            <asp:GridView ID="gvEventList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvEventList_RowDataBound" OnRowCreated="gvEventList_RowCreated" OnSorting="gvEventList_Sorting" AllowSorting="true"
                        Width="100%" CellPadding="0" cellspacing="0" border="0" CssClass="table table-striped table-bordered table-hover"  GridLines="none" >
                        <Columns>
                             
                             <asp:TemplateField HeaderText="Title" SortExpression="Title" >
                                <ItemTemplate>
                                     <asp:HyperLink Target="_blank" runat="server" ID="HLEventTitle"></asp:HyperLink>                        
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Version" HeaderText="Version" SortExpression="Version" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" /> 
                            <asp:BoundField DataField="GameID" HeaderText="Game ID Desktop" SortExpression="GameID" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" /> 
                            <asp:BoundField DataField="CreateUser" HeaderText="Author" SortExpression="CreateUser" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs"/> 
                            <asp:BoundField DataField="ReleaseDate" HeaderText="ReleaseDate" SortExpression="ReleaseDate" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs"/> 
                            <asp:BoundField DataField="StartTime" HeaderText="StartTime" SortExpression="StartTime" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs"/> 
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" /> 
                             

                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="center" HeaderStyle-Width="100">
                                <ItemTemplate>
                                     <div class="visible-md visible-lg hidden-sm hidden-xs">
                                        <asp:HyperLink ID="hlCopy" runat="server" CssClass="btn btn-xs btn-orange tooltips" data-original-title="Copy" data-placement="top" Text="<i class='fa fa-copy'></i>"></asp:HyperLink>
                                        <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-xs btn-teal tooltips" data-original-title="Edit" data-placement="top" Text="<i class='fa fa-edit'></i>"></asp:HyperLink>
                                        <asp:LinkButton ID="btnRemove" runat="server" CssClass="btn btn-xs btn-bricky tooltips" data-original-title="Remove" data-placement="top" Text="<i class='fa fa-times fa fa-white'></i>" OnClick="btnRemove_Click"></asp:LinkButton>
                                         
                                     </div>
                                    <div class="visible-xs visible-sm hidden-md hidden-lg">
                                      <div class="btn-group"> <a href="#" data-toggle="dropdown" class="btn btn-primary dropdown-toggle btn-sm"> <i class="fa fa-cog"></i> <span class="caret"></span> </a>
                                        <ul class="dropdown-menu pull-right" role="menu">
                                           <li role="presentation">  <asp:HyperLink ID="hlMobileCopy" runat="server" tabindex="-1" role="menuitem" Text="<i class='fa fa-copy'></i> Copy"></asp:HyperLink></li>
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
                    <ul id="paginator-EventList" class="pagination-purple"></ul>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $('#paginator-EventList').bootstrapPaginator({
                                bootstrapMajorVersion: 3,
                                currentPage: <%=PageIndex %>,
                                totalPages: <%=RecordPages %>,
                                numberOfPages:7,
                                useBootstrapTooltip:true,
                                onPageClicked: function (e, originalEvent, type, page) {
                                    window.location.href='<%=CurrentUrl %>&PageIndex='+ page;
                                }
                            });
                        });
                    </script>
          </div>
        </div>
        
        <!-- end: PAGE CONTENT--> 
      </div>

<div id="Repeats_Modal" class="modal fade" tabindex="-1" data-width="820" data-height="450" style="display: none;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            &times;
        </button>
        <h4 class="modal-title">
            <i class='fa clip-calendar'></i> Repeats - <name id="Repeats_EventTitle"></name> </h4>
    </div>
    <div class="modal-body">
        <iframe id="Repeats_Iframe" width="100%" height="100%" style="border-width: 0px;">
        </iframe>
    </div>
 </div>

<div id="Newsletter_Modal" class="modal fade" tabindex="-1" data-width="820" data-height="450" style="display: none;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            &times;
        </button>
        <h4 class="modal-title">
            <i class='fa clip-archive'></i> Send Newsletter</h4>
    </div>
    <div class="modal-body">
        <iframe id="Newsletter_Iframe" width="100%" height="100%" style="border-width: 0px;">
        </iframe>
    </div>
 </div>


  


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


           