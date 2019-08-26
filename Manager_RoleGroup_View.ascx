<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_RoleGroup_View.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_RoleGroup_View" %>


<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12"> 
        <!-- start: PAGE TITLE & BREADCRUMB -->
          
        <div class="page-header">
        <h1>
            <i class="fa fa-group"></i> <%=ViewResourceText("Header_Title", "View Role Groups")%>
        </h1>
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB --> 
    </div>
</div>
<!-- end: PAGE HEADER --> 





 <!-- start: PAGE CONTENT -->
      
      <div class="row">
        <div class="col-sm-12">
          <div class="form-group role-groups">
            <div class="clearfix">
              <div class="pull-left">
                   <asp:LinkButton ID="btnExportExcel" runat="server" CssClass="btn btn-default" Text="Export Excel" resourcekey="btnExportExcel" onclick="btnExportExcel_Click" OnClientClick="return ApplyStatus();" />
              </div>
              <div class="pull-left">
                   <div class="form-group space0">
                <asp:DropDownList ID="ddlRoleGroups" runat="server" CssClass="form-control  search-select"></asp:DropDownList>

                  
              </div>
                  </div>
                 <div class="pull-left">
                       <div class="form-group space0">
                     <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-default" Text="<i class='fa fa-search'></i> Search" resourcekey="btnSearch" OnClick="btnSearch_Click"  /></div>
            </div> </div>
          </div>
          <div class="form-group">
            <asp:GridView ID="gvEventList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvEventList_RowDataBound" OnRowCreated="gvEventList_RowCreated" OnSorting="gvEventList_Sorting" AllowSorting="true"
                        Width="100%" CellPadding="0" cellspacing="0" border="0" CssClass="table table-striped table-bordered table-hover"  GridLines="none" >
                        <Columns>
                             <asp:BoundField DataField="UserID" HeaderText="ID" />
                            <asp:BoundField DataField="Username" HeaderText="User Name" />
                            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
                            <asp:BoundField DataField="DisplayName" HeaderText="Roles" />
                        </Columns>
                        <PagerSettings Visible="False" />
                    </asp:GridView>
                     
          </div>
        </div>
        
        <!-- end: PAGE CONTENT--> 
      </div>
 