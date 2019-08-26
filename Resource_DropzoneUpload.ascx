<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resource_DropzoneUpload.ascx.cs" Inherits="Playngo.Modules.ClientZone.Resource_DropzoneUpload" %>
<script type="text/javascript" src="<%=ModulePath %>Resource/js/dropzone.js"></script>
<link rel="stylesheet" href="<%=ModulePath %>Resource/css/dropzone.css" />
<form action="<%=ModulePath %>Resource_jQueryFileUpload.ashx?<%=QueryString %>" method="post"  id="dropzone" class="dropzone" enctype="multipart/form-data"></form>


<script type="text/javascript">
    $(document).ready(function () {
        $("#dropzone").dropzone({acceptedFiles:"image/*,application/pdf,.psd,.zip,.rar,.doc,.xls,.ppt,.docx,.xlsx,.pptx,.txt,.mp3,.mp4,.flv,.mkv,<%=Playngo.Modules.ClientZone.FileSystemUtils.GetFileExtensions()%>"});
    });
</script>