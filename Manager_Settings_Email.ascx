<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Manager_Settings_Email.ascx.cs" Inherits="Playngo.Modules.ClientZone.Manager_Settings_Email" %>

 
      <!-- start: PAGE HEADER -->
      <div class="row">
        <div class="col-sm-12"> 
          <!-- start: PAGE TITLE & BREADCRUMB -->
          
          <div class="page-header">
            <h1><i class="fa fa-envelope"></i> <%=ViewResourceText("Header_Title", "Email Settings")%></h1>
              <%--language switch: <asp:Literal ID="liLanguageLinks" runat="server"></asp:Literal>--%>
          </div>
          <!-- end: PAGE TITLE & BREADCRUMB --> 
        </div>
      </div>
      <!-- end: PAGE HEADER -->
            <!-- start: PAGE CONTENT -->
      
      <div class="row">
        <div class="col-sm-12">
    
            
          <div class="panel panel-default">
            <div class="panel-heading"> <i class="fa fa-external-link-square"></i> <%=ViewResourceText("Title_BaseSettings", "Basic Settings")%>
              <div class="panel-tools"> <a href="#" class="btn btn-xs btn-link panel-collapse collapses"> </a> </div>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">

                  <div class="form-group">
                    <%=ViewControlTitle("lblTemplateName", "Template Name", "lbShowTemplateName", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-5">
                       <label class="control-label"><asp:Literal ID="lbShowTemplateName" runat="server"></asp:Literal></label>
                    </div>
                  </div>
                      <div class="form-group">
                    <%=ViewControlTitle("lblLanguage", "Language", "labShowLanguage", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-5">
                       
                        <label class="control-label"><asp:Literal ID="lbShowLanguage" runat="server"></asp:Literal></label>
                    </div>
                  </div>

                  <div class="form-group">
                    <%=ViewControlTitle("lblEnable", "Enable", "cbEnable", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-5">
                        <div class="checkbox-inline"><asp:CheckBox ID="cbEnable" runat="server"  CssClass="auto"/></div>
                    </div>
                  </div>

                  <div class="form-group" id="divMailTime" runat="server" visible="false">
                    <%=ViewControlTitle("lblMailTime", "Time Before Starting", "txtMailTime", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server" ID="txtMailTime" Width="300px" CssClass="form-control  validate[required,maxSize[200]]"></asp:TextBox>
                         <%=ViewHelp("lblMailTime", "Hours")%>
                         <%=ViewHelp("lblMailTime2", "Users will receive the reminder email at a set time.")%>
                    </div>
                  </div>
                  
                  <div class="form-group">
                    <%=ViewControlTitle("lblMailTo", "Mail To", "txtMailTo", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server" ID="txtMailTo" Width="300px" CssClass="form-control  validate[required,maxSize[200]]"></asp:TextBox>
                    </div>
                  </div>
                   <div class="form-group">
                    <%=ViewControlTitle("lblMailCC", "Mail CC", "txtMailCC", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server" ID="txtMailCC" Width="300px" CssClass="form-control  validate[maxSize[200]]"></asp:TextBox>
                    </div>
                  </div>


                </div>
            </div>
          </div>


           <div class="panel panel-default">
            <div class="panel-heading"> <i class="fa fa-external-link-square"></i> <%=ViewResourceText("Title_TemplateSettings", "Email Content Template")%>
              <div class="panel-tools"> <a href="#" class="btn btn-xs btn-link panel-collapse collapses"> </a> </div>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
      

                  <div class="form-group">
                    <%=ViewControlTitle("lblMailSubject", "Mail Subject", "txtMailSubject", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server" ID="txtMailSubject" Width="500px" CssClass="form-control  validate[required,maxSize[200]]"></asp:TextBox>
                    </div>
                  </div>

                  <div class="form-group">
                    <%=ViewControlTitle("lblMailBody", "Mail Body", "txtMailBody", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-8">
                      <asp:TextBox  id="txtMailBody" CssClass="form-control" runat="server" height="300" width="100%" TextMode="MultiLine"></asp:TextBox>
                    </div>
                  </div>
  

                  <div class="form-group">
                    <%=ViewControlTitle("lblTemplateTitle", "Template Token","", ":", "col-sm-3 control-label")%>
                    <div class="col-sm-8">
                        <%=ViewResourceText("Template_USER_NAME", "[USER_NAME]: User name of the one who registered the activity, can be only obtained after logging in.")%><br/>
                        <%=ViewResourceText("Template_USER_DISPLAYNAME", "[USER_DISPLAYNAME]: Display name of the one who registered the activity, can be only obtained after logging in.")%><br/>

                        <%=ViewResourceText("Template_AUTHOR_MAIL", "[AUTHOR_MAIL]: Email address of the author created in item.")%><br/>
                        <%=ViewResourceText("Template_AUTHOR_USERNAME", "[AUTHOR_USERNAME]: Username of the author created in item.")%><br/>
                        <%=ViewResourceText("Template_AUTHOR_DISPLAYNAME", "[AUTHOR_DISPLAYNAME]: Display name of the author created in the item.")%><br/>

                        <%=ViewResourceText("Template_TITLE", "[TITLE]: Title of the item.")%><br/>
                        <%=ViewResourceText("Template_CREATETIME", "[CREATETIME]: Created time of the item.")%><br/>
                        <%=ViewResourceText("Template_CREATEDATE", "[CREATEDATE]: Created date of the item.")%><br/>
                        <%=ViewResourceText("Template_LINKURL", "[LINK]: Page URL to view the item details.")%><br/>


                        <%=ViewResourceText("Template_STARTTIME", "[STARTTIME]: Start time of the item.")%><br/>
                        <%=ViewResourceText("Template_STARTDATE", "[STARTDATE]: Start date of the item.")%><br/>
                        <%=ViewResourceText("Template_MAILSETTING_TIME", "[MAILSETTING_TIME]: Set how many hours ahead of the starting item time to send mails.")%><br/>
                        <%=ViewResourceText("Template_DATETIME_NOW", "[DATETIME_NOW]: Template will obtain the current system time.")%><br/>



                        
                     <%--   <%=ViewResourceText("Template_", "[]: ")%><br/>--%>
               
                    </div>
                  </div>
                </div>
            </div>
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


<script type="text/javascript">

    jQuery(function ($) {
        CKEDITOR.replace('<%=txtMailBody.ClientID %>', { height: '400px' });

   
    });
</script>