<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View_Display.ascx.cs" Inherits="Playngo.Modules.ClientZone.View_Display" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<asp:Panel ID="panNavigation" runat="server"  CssClass="Navigation">
    <asp:HyperLink ID="hlManager" runat="server" CssClass="hlManager" Text="<i class='icon-file'></i> Manager" resourcekey="Actions_Manager"></asp:HyperLink>
</asp:Panel>


<asp:Panel ID="plLicense" runat="server">
<asp:PlaceHolder ID="phScript" runat="server"></asp:PlaceHolder>


<div class="validationEngineContainer form_div_<%=ModuleId %>" data-Token="<%=UIToken %>">

    <div class="client-zone-group">

        <%--这是手机响应式的区域--%>
        <div class="Modal">
            <a class="ModalClose lnr lnr-cross2" href="#"></a>
        </div>
        <%--这是手机响应式的区域--%>
        <div class="ModalOpen">
            <a class="dg-btn-1 radius-3px btn-accent" href="#">Filter<span class="fa fa-angle-double-up"></span></a>
        </div>

        <%--这是打印的区域元素--%>
        <asp:Literal ID="liNavJurisdictions" runat="server"></asp:Literal>
        
        <%--这是打印顶部tabs的元素--%>
        <div class="client-zone-nav">
            <asp:Literal ID="liTopMenus" runat="server"></asp:Literal>
        </div><%--client-zone-nav End--%>

        
        <%--这里显示每个菜单下的内容区域--%>
        <div class="client-zone-main">
            <asp:PlaceHolder  ID="phContainer" runat="server"></asp:PlaceHolder>
        </div><%--client-zone-main End--%>



    </div><%--client-zone-group--%>
  
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

     $(document).ready(function () {
        Playngo.init({ moduleId: ".form_div_<%=ModuleId %>" });
     })
     
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


