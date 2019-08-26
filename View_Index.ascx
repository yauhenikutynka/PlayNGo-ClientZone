<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View_Index.ascx.cs" Inherits="Playngo.Modules.ClientZone.View_Index" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<asp:Panel ID="panNavigation" runat="server"  CssClass="Navigation">
    <asp:HyperLink ID="hlManager" runat="server" CssClass="hlManager" Text="<i class='icon-file'></i> Manager" resourcekey="Actions_Manager"></asp:HyperLink>
</asp:Panel>


<asp:Panel ID="plLicense" runat="server">
<asp:PlaceHolder ID="phScript" runat="server"></asp:PlaceHolder>


<div class="validationEngineContainer form_div_<%=ModuleId %>" >

    <%--这是打印顶部tabs的元素--%>
    <asp:Literal ID="liTopMenus" runat="server"></asp:Literal>




    <%--这里显示每个菜单下的内容区域--%>
    <asp:PlaceHolder  ID="phContainer" runat="server"></asp:PlaceHolder>
  
 </div>

 <script type="text/javascript">
     jQuery(function (q) {
         q(".form_div_<%=ModuleId %>").validationEngine({
             promptPosition: "topRight"
         });

         q(".form_div_<%=ModuleId %> input[lang='Submit']").click(function () {
             if (!$('.form_div_<%=ModuleId %>').validationEngine('validate')) {
                 return false;
             }
         });

     });

     function CancelValidation() {
         jQuery('#Form').validationEngine('detach');
     }
</script>

</asp:Panel>



<asp:Panel ID="pnlTrial" runat="server">
    <center>
        <asp:Literal ID="litTrial" runat="server"></asp:Literal>
        <br />
        <asp:Label ID="lblMessages" runat="server" CssClass="SubHead" resourcekey="lblMessages"
            Visible="false" ForeColor="Red"></asp:Label>
    </center>
</asp:Panel>


