<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resource_EventAuthors.aspx.cs" Inherits="Playngo.Modules.ClientZone.Resource_EventAuthors" %>
<%@ Register src="Resource_EventAuthors.ascx" tagname="Resource_EventAuthors" tagprefix="uc1" %>
<!DOCTYPE HTML>
<!--
/*
 * jQuery File Upload Plugin HTML Example 5.0.5
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://creativecommons.org/licenses/MIT/
 */
-->
<html lang="en" class="no-js">
<head>
<meta charset="utf-8">
<title>jQuery File Upload Example</title>
<!--[if lt IE 9]>
<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
     <!-- start: MAIN CSS -->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap/css/bootstrap.min.css"  media="screen" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/fonts/style.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/main.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/main-responsive.css" />
  
    <!--<link rel="stylesheet/less" type="text/css" href="<%=ModulePath %>Resource/css/styles.less" />-->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/theme_light.css" type="text/css" id="skin_color" />
    <!--[if IE 7]>
		    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/font-awesome/css/font-awesome-ie7.min.css" />
		    <![endif]-->
    <!-- end: MAIN CSS -->
    <!-- start: CSS REQUIRED FOR THIS PAGE ONLY -->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-social-buttons/social-buttons-3.css" />

       <!-- end: CSS REQUIRED FOR THIS PAGE ONLY -->
    <script src="<%=ModulePath %>Resource/js/jquery.min.js"></script>
    <script src="<%=ModulePath %>Resource/js/jquery-ui.min.js"></script>
    

    <!-- start: MAIN JAVASCRIPTS -->
    <!--[if lt IE 9]>
		    <script src="<%=ModulePath %>Resource/plugins/respond.min.js"></script>
		    <script src="<%=ModulePath %>Resource/plugins/excanvas.min.js"></script>
    <![endif]-->

    <script src="<%=ModulePath %>Resource/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="<%=ModulePath %>Resource/plugins/blockUI/jquery.blockUI.js"></script>
 


    <script src="<%=ModulePath %>Resource/plugins/bootstrap-paginator/src/bootstrap-paginator.js"></script>

 

</head>
<body>
<form id="form1" runat="server">
<%--<uc1:Resource_EventAuthors ID="Resource_EventAuthors1" runat="server" />--%>
<asp:PlaceHolder ID="phEventAuthors" runat="server"></asp:PlaceHolder>
 </form>
</body> 
</html>