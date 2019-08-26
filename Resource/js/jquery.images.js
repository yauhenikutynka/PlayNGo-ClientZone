var ImagesLibrary = function () {
    var bind_images = function (urls_name, urls_div, pageindex, pagesize) {
        var div_full =  $("div.controls_full_images[data-name='" + urls_name + "']");
        var ImageIds = $(div_full).find("input[type='hidden']").val();

        $.getJSON(Module.ModulePath + "Resource_Service.aspx?Token=SelectImages", {  ModuleId: Module.ModuleId, TabId: Module.TabId, PortalId: Module.PortalId, Images: ImageIds }, function (data) {
            //console.log("data:",data);
            var Pages = 0;
            $.each(data, function (i, item) {
                if (item.ID > 0) {
                    $("#scriptLibraryImages").tmpl(item).appendTo($(div_full).find(".ui-sortable"));
                    Pages = item.Pages;
                }
            });
        });

        $('#multi_images' + urls_name).on('show.bs.modal', function (e) {

            full_images(urls_name, urls_div, pageindex, pagesize);

            $(urls_div).on("click", 'a.Select_Thumbnail', function (event) {
                var id = $(this).data("id");
                var ImageIds = $(div_full).find("input[type='hidden']").val();
                if ( !( ImageIds !== '' && ImageIds.indexOf(id + ",") >= 0)) {
                   
                    var ThumbnailUrl = $(this).data("src");
                    ImageIds = ImageIds + (id + ",");
                    $(div_full).find("input[type='hidden']").val(ImageIds);


                    $("#scriptLibraryImages").tmpl({ ID: id, ThumbnailUrl: ThumbnailUrl }).appendTo($(div_full).find(".ui-sortable"));
                   


                }
                $('#multi_images' + urls_name).modal('hide');
               
            });
 
        })

       


    };
    var bindPaginator = function (urls_name, urls_div, pagesize, Pages) {
        $(urls_div).find("#multi_images_page").bootstrapPaginator({
            bootstrapMajorVersion: 3, currentPage: 1, totalPages: Pages,
            onPageChanged: function (e, oldPage, newPage) {
                $.getJSON(Module.ModulePath + "Resource_Service.aspx?Token=PictureList", { Search: urls_div.data("search"), Order: urls_div.data("order"), OrderBy: urls_div.data("orderby"), ModuleId: Module.ModuleId, TabId: Module.TabId, PortalId: Module.PortalId, PageIndex: newPage, PageSize: pagesize }, function (data) {
                    $(urls_div).find(".multi_images_ol li").detach();
                    $.each(data, function (i, item) {
                        bind_image(urls_name, urls_div, i, item);
                    });
                });
            }
        });
    };


    var BindListSearch = function (urls_name, urls_div) {
        urls_div.on("click", "a.submit_but", function (event) {
      
            event.preventDefault();

            urls_div.data("search", urls_div.find(".list-search input").val());
            //$(urls_div).find("#multi_images_ol li").detach();
            full_images(urls_name, urls_div, 1, 10);
        });
    };


    var BindListSort = function (urls_name, urls_div) {

        SortFieldFind(urls_name, urls_div);

        urls_div.on("click", "a.table-title", function () {
            urls_div.data("orderby", urls_div.data("orderby") == 1 ? 0 : 1);
            urls_div.data("order", $(this).data("order"));
            SortFieldFind(urls_name, urls_div);
            //$(urls_div).find("#multi_images_ol li").detach();
            full_images(urls_name, urls_div, 1, 10);
        });
    };

    var SortFieldFind = function (urls_name,urls_div) {
        var order = urls_div.data("order");
        var orderby = urls_div.data("orderby");

        urls_div.find("a.table-title").each(function (i) {
            $(this).find("i").removeClass();
            if ($(this).data("order") == order) {
                $(this).find("i").addClass(function () {
                    return orderby == 1 ? "fa fa-sort-amount-desc" : "fa fa-sort-amount-asc";
                });
            }
        });
    };

    var bind_image = function (urls_name, urls_div, i, item) {
        $("#scriptLibraryUrls").tmpl(item).appendTo($(urls_div).find(".multi_images_ol"));
    };


    var full_images = function (urls_name, urls_div, pageindex, pagesize) {
        //if ($(urls_div).find("#multi_images_ol li").length == 0) {
            $(urls_div).find(".multi_images").fadeIn("slow");
         
            $.getJSON(Module.ModulePath + "Resource_Service.aspx?Token=PictureList", { Search: $(urls_div).data("search"), Order: $(urls_div).data("order"), OrderBy: $(urls_div).data("orderby"), ModuleId: Module.ModuleId, TabId: Module.TabId, PortalId: Module.PortalId, PageIndex: pageindex, PageSize: pagesize }, function (data) {
                var Pages = 0;
                $(urls_div).find("#multi_images_ol li").detach();
                $.each(data, function (i, item) {
                    bind_image(urls_name, $(urls_div), i, item);
                    Pages = item.Pages;
                });
                bindPaginator(urls_name, urls_div, pagesize, Pages > 0 ? Pages : 1);
            });

 
       // }
    };


    var re_bind_images = function (urls_name, urls_div, pageindex, pagesize) {

        var Li_list = $("div.control_images[data-name='" + urls_name + "']").find(".multi_images_ol li");
        if (Li_list.length > 0) {
            $(Li_list).detach();
        }

        full_images(urls_name, urls_div, pageindex, pagesize);

    };


    return {
        re_bind: function (urls_name, urls_div) {
            re_bind_images(urls_name, urls_div, 1, 10);
        },

        init: function (urls_name, urls_div) {
            bind_images(urls_name, urls_div, 1, 10);
            BindListSort(urls_name, urls_div);
            BindListSearch(urls_name, urls_div);
        }

    };
} ();

jQuery(function ($) {
    $("div.control_images").each(function (i, n) {
        var urls_name = $(this).attr("data-name");
        var urls_div = $("div.control_images[data-name='" + urls_name + "']");
        ImagesLibrary.init(urls_name, urls_div);
       
        var div_full = $("div.controls_full_images[data-name='" + urls_name + "']");



        $(div_full).on("click", 'a.gallery-icon-remove', function (event) {
            var id = $(this).data("id");
           
            if (id > 0)
            {
                var ImageIds = $(div_full).find("input[type='hidden']");
      
                $(div_full).find(".ui-sortable li[data-id='" + id + "']").hide("fast", function () {
                    $(this).remove();
                });

                ImageIds.val(ImageIds.val().replace(id + ",", ""));
                //console.log(ImageIds.val());
            }
        });

    });
});