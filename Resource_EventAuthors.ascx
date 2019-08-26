<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resource_EventAuthors.ascx.cs" Inherits="Playngo.Modules.ClientZone.Resource_EventAuthors1" %> 
<div class="container">
  <div class="row">
        <div class="col-sm-8">
         <div class="form-group">
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search Text Field" x-webkit-speech></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" onclick="btnSearch_Click"  resourcekey="btnSearch" />
            </div>
        </div>
        <div class="col-sm-3 text_right">
        	<div class="control-inline"><asp:Label ID="lblRecordCount" runat="server"></asp:Label></div>
        </div>
      </div>
     <div class="row"> 
     	<div class="col-sm-12">
		<!-- start-->
			<div class="form-group">
             <asp:GridView ID="gvEventList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvEventList_RowDataBound" OnRowCreated="gvEventList_RowCreated" OnSorting="gvEventList_Sorting" AllowSorting="true"
                        Width="98%" CellPadding="0" cellspacing="0" border="0" CssClass="table table-striped table-bordered table-hover"  GridLines="none" >
                        <Columns>
                             
                             <asp:TemplateField HeaderText="User avatar">
                                <ItemTemplate>
                                    <asp:Image  runat="server" ID="imgUserAvatar" style=" max-height:80px;max-width:80px;" />           
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DisplayName" HeaderText="Display Name"  />
                            <asp:BoundField DataField="Username" HeaderText="User Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email"  HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs"/> 
                            <asp:BoundField DataField="CreatedOnDate" HeaderText="Created Date"  HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" /> 
 
                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="center">
                                <ItemTemplate>
                                     <asp:HyperLink Target="_blank" runat="server" ID="hlAdd" Text="Change" CssClass="btn btn-xs btn-primary tooltips"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <PagerSettings Visible="False" />
                    </asp:GridView>
                    <ul id="paginator-EventList" class="pagination-purple"></ul>

      <!-- end--> 
     </div>
     </div> 
 

 
           
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

             
                function ReturnUser(UserName,UserID,UserPic) {
                    window.parent.ReturnUser(UserName, UserID,UserPic);
                    window.parent.$('#EventAuthor_Modal').modal('hide');
                }

                

 
            </script>
       