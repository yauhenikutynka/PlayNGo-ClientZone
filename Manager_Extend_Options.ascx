<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_Extend_Options.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_Extend_Options" %>


<!-- start: PAGE HEADER -->
      <div class="row">
        <div class="col-sm-12"> 
          <!-- start: PAGE TITLE & BREADCRUMB -->
          
          <div class="page-header">
            <h1>
                <i class="<%=ExtendConfig.Icon %>"></i> 
                <%--<%=ExtendConfig.Alias%>--%>
                <asp:Literal runat="server" ID="liExtendConfigAlias"></asp:Literal>
                <small><%--<%=ExtendConfig.Description%>--%><asp:Literal runat="server" ID="liExtendConfigDescription"></asp:Literal></small>
             </h1>
          </div>
          <!-- end: PAGE TITLE & BREADCRUMB --> 
        </div>
      </div>
      <!-- end: PAGE HEADER -->
            <!-- start: PAGE CONTENT -->
      
      <div class="row">
        <div class="col-sm-12">
 
           



          
            <div runat="server" id="divOptions" visible="false">
                <asp:Repeater ID="RepeaterGroup" runat="server" OnItemDataBound="RepeaterGroup_ItemDataBound">
                    <ItemTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <i class="fa fa-external-link-square"></i>
                                <%--<%#Eval("key")%>--%><asp:Literal runat="server" ID="liGroupName"></asp:Literal>
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
                                                <div class="col-sm-8">
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




        </div>
      </div>
      <!-- end: PAGE CONTENT-->
      
      <div class="row">
        <div class="col-sm-2"> </div>
        <div class="col-sm-10">
          <asp:Button CssClass="btn btn-primary" lang="Submit" ID="cmdUpdate" resourcekey="cmdUpdate"
        runat="server" Text="Update" OnClick="cmdUpdate_Click"></asp:Button>&nbsp;
        <asp:Button CssClass="btn btn-default" ID="cmdCancel" resourcekey="cmdCancel" runat="server"
            Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click"  OnClientClick="CancelValidation();"></asp:Button>&nbsp;
        
         </div>
      </div>


 