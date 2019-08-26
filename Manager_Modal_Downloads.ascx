<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_Modal_Downloads.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_Modal_Downloads" %>
<div class="container">
 <div class="row">
        <div class="col-sm-8">
         <div class="form-group">
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search Text Field" x-webkit-speech></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" onclick="btnSearch_Click"  resourcekey="btnSearch" />
            </div>
        </div>
        

      </div>
     <div class="row">
        <div class="col-sm-8">
        <div class="form-group">
          <button class="btn btn-primary btn-sm" type="button" onclick="return  ReturnAttachments();"> Submit </button>
          </div>
        </div>
        <div class="col-sm-3 text_right">
        	<div class="control-inline"><asp:Label ID="lblRecordCount" runat="server"></asp:Label></div>
        </div>
      </div>

      <!-- start-->
      <div class="form-group">
             <asp:GridView ID="gvEventList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvEventList_RowDataBound" OnRowCreated="gvEventList_RowCreated" OnSorting="gvEventList_Sorting" AllowSorting="true"
                        Width="98%" CellPadding="0" cellspacing="0" border="0" CssClass="table table-striped table-bordered table-hover"  GridLines="none" >
                        <Columns>
                             
                             <asp:TemplateField HeaderText="File Information" SortExpression="Title" >
                                <ItemTemplate>
                                    
                                    <table cellpadding="0" cellspacing="0" border="0">
                                       <tr>
                                        <td><asp:Image  runat="server" ID="imgFileName" style=" max-height:80px;max-width:80px;" />&nbsp;</td>
                                        <td> <asp:HyperLink Target="_blank" runat="server" ID="hlFileName" CssClass=""></asp:HyperLink><br />
                                            <asp:Label ID="lblFileExtension" runat="server"></asp:Label>      </td>
                                      </tr>
                                   </table>
                                    
                                    
                                                       
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LastUser" HeaderText="Author" SortExpression="LastUser" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" /> 
                            <asp:BoundField DataField="LastTime" HeaderText="CreateTime" SortExpression="LastTime"  HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs"/> 
                             <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"/> 
                
                            
                        </Columns>
                        <PagerSettings Visible="False" />
                    </asp:GridView>
      </div>
      <!-- end--> 
      <!-- start-->
      <div class="row">
        <div class="col-sm-8">
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

             


            function ReturnAttachments() {
                var jsons = [];
                var checkok = false;
                $("input[type='checkbox'][title]:checked").each(function (index, domEle) { 
                    checkok = true;
                    jsons.push($(domEle).data("json")); 
                });
                 if (!checkok) {
                     alert("<%=ViewResourceText("lblcheckconfirm", "Please select the records needs to be Selected!")%>");
                 }
                 else {
                    window.parent.InsetDownloads(jsons);
                    window.parent.$('#DownloadFiles_Modal').modal('hide');
                 }
                return false;
            }
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
     
 
        // -->
            </script>

          
        </div>
      </div>
      <!-- end--> 
</div>