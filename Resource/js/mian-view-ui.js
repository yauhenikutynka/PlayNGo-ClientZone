(function ($) {
    // USE STRICT
    "use strict";

    var Playngo = {};

    //----------------------------------------------------/
    // Client Zone Iframe
    //----------------------------------------------------/
    Playngo.clientZoneIframe = function () {
        $(".client-zone-iframe>a").each(function () {
            var $this = $(this);
            var thisiframe = $this.next(".responsive-video");

            $this.click(function () {
                if ($(window)[0].innerWidth >= "767") {
                    $this.fadeOut("normal");
                    var url = $this.data("url");
                    var iframe = "<iframe src=" + url + "></iframe>";
                    thisiframe.html(iframe);
                    return false;
                }
            });

            $(window).resize(function () {
                if ($(window)[0].innerWidth < "767") {
                    $this.fadeIn("normal", function () {
                        thisiframe.html("");
                    });
                }
            });

        });

    };

    //----------------------------------------------------/
    // GameList
    //----------------------------------------------------/
    Playngo.gameList = function () {
        var gameListStyle = $(".gameList-style");
        if (gameListStyle.length == 0) return false;
        var grid = gameListStyle.children().eq(0);
        var list = gameListStyle.children().eq(1);

        var gameList = $(".client-zone-gameList");

        grid.click(function () {
            if (!grid.hasClass("active")) {
                list.removeClass("active");
                grid.addClass("active");
            }
            gameList.each(function () {
                if (!$(this).hasClass("style-grid")) {
                    $(this).removeClass("style-list");
                    $(this).addClass("style-grid");
                }
            });
            return false;
        });

        list.click(function () {
            if (!list.hasClass("active")) {
                grid.removeClass("active");
                list.addClass("active");
            }
            gameList.each(function () {
                if (!$(this).hasClass("style-list")) {
                    $(this).removeClass("style-grid");
                    $(this).addClass("style-list");
                }
            });
            return false;
        });

    };

    //----------------------------------------------------/
    // checkbox Filter
    //----------------------------------------------------/
    Playngo.checkboxFilter = function () {

        var filter = $(".checkbox-filter");

        filter.each(function () {
            var $this = $(this);

            var aInput = $this.find("input[type=checkbox]");

            if ($this.hasClass("disable")) {
                aInput.attr("disabled", "disabled");
            }
            

            if (aInput.eq(0).val() == "0") {

                var all = aInput.eq(0);
                all.change(function () {
                    if (all.is(':checked')) {
                        aInput.prop('checked', true);
                    } else {
                        aInput.prop('checked', false);
                    }
                });

                aInput.each(function (index) {
                    if (index != 0) {
                        $(this).change(function () {
                            var checked = $this.find("input[type=checkbox]:not(:first):checked");

                            if (checked.length == aInput.length - 1) {
                                all.prop('checked', true);

                            } else {
                                all.prop('checked', false);

                            }
                        });
                    }
                });
                
            }

        });


        var accountSetting = $(".account-setting-list");
        
        accountSetting.each(function () {
            var $this = $(this);
            var aInput = $this.find("input[type=checkbox]");

            if (aInput.eq(0).val() == "all") {

                var all = aInput.eq(0);
                all.change(function () {
                    if (all.is(':checked')) {
                        aInput.prop('checked', true);
                    } else {
                        aInput.prop('checked', false);
                    }
                });

                aInput.each(function (index) {
                    if (index != 0) {
                        $(this).change(function () {
                            var checked = $this.find("input[type=checkbox]:not(:first):checked");

                            if (checked.length == aInput.length - 1) {
                                all.prop('checked', true);

                            } else {
                                all.prop('checked', false);

                            }
                        });
                    }
                });

            }

        });




    };



    //----------------------------------------------------/
    // end
    //----------------------------------------------------/



    $(document).ready(function () {

        Playngo.clientZoneIframe();
        Playngo.gameList();
        Playngo.checkboxFilter();
    });


})(jQuery);


var Playngo = function () {
    // 默认参数
    var settings = {
        moduleId: ""
    };
    // Tbale 重构数组
    var htmlTable = {};
    // oTbale对象
    var oTable;
    // 发送的数据
    var dataAjax = {};
    // 初始化参数-页码
    var options = {
        "bootstrapMajorVersion": 3,
        "currentPage": 1,
        "totalPages": 1,
        "numberOfPages": 7,
        "useBootstrapTooltip": true,
        "itemTexts": function (type, page, current) {
            switch (type) {
                case "first":
                    return '<i class="fa fa-angle-double-left"></i>';
                case "prev":
                    return '<i class="fa fa-angle-left"></i>';
                case "next":
                    return '<i class="fa fa-angle-right"></i>';
                case "last":
                    return '<i class="fa fa-angle-double-right"></i>';
                case "page":
                    return page;
            }
        },
        "onPageClicked": function (e, originalEvent, type, page) {
            e.stopImmediatePropagation();

            var currentTarget = $(e.currentTarget);
            currentTarget.bootstrapPaginator("show", page);

            var pages = currentTarget.bootstrapPaginator("getPages");

            options["currentPage"] = pages.current;
            dataAjax["pageindex"] = pages.current;

            switch (module.data("token")) {
                case "GameSheets":
                case "Campaigns":
                case "Events":

                    var gameList = module.find(".client-zone-gameList");

                    if (gameList.length == 0) return false;

                    $('html,body').animate({ scrollTop: gameList.offset().top - 100 }, 800);

                    initGameList();
                    break;
                    
                case "Downloads":

                    var tableDownload = module.find("#table-download_wrapper");

                    if (tableDownload.length == 0) return false;

                    $('html,body').animate({ scrollTop: tableDownload.offset().top -100 }, 800);

                    oTable.ajax.reload();
                    break;

                default:
            }

        }
    }
    // OwlCarousel2
    //var owlCarousel2 = function () {
    //    module.find(".owl-carousel").each(initCarousel);
    //};
    function initCarousel() {
        var e = $(this);
    

        var item = e.data("items") ? e.data("items") : 4;

        var margins = null;
        if (e.data("margin")) {
            margins = e.data("margin")
        } else if (e.data("margin") === 0) {
            margins = 0;
        } else {
            margins = 36;
        }

        var carousel_default = {
            items: item,
            loop: true,
            center: false,
            rewind: false,
            mouseDrag: true,
            touchDrag: true,
            pullDrag: true,
            freeDrag: false,
            margin: margins,
            stagePadding: 0,
            merge: false,
            mergeFit: true,
            autoWidth: false,
            startPosition: 0,
            rtl: false,
            smartSpeed: 250,
            fluidSpeed: false,
            dragEndSpeed: false,
            responsiveRefreshRate: 200,
            responsiveBaseElement: window,
            fallbackEasing: 'swing',
            info: false,
            nestedItemSelector: false,
            MobileSmall: 1,
            autoplay: false,
            autoplayTimeout: 3000,
            autoplayHoverPause: true,
            autoHeight: false,
            nav: true,
            animateOut: '',
            animateIn: '',
            navText: ['<', '>'],
            navSpeed: false,
            navElement: 'div',
            navContainer: false,
            slideBy: 1,
            dots: true,
            dotsEach: false,
            dotsData: false,
            dotsSpeed: false,
            dotsContainer: false,
            maxHeight: false,
            model3D: false,
            mobile: Math.max(item - 3, 1),
            tablet: Math.max(item - 2, Math.min(2, item)),
            desktopSmall: Math.max(item - 1, Math.min(3, item)),
            desktop: Math.max(item - 1, Math.min(3, item)),
            mobileMargin: margins * .2,
            tabletMargin: margins * .3,
            desktopSmallMargin: margins * .6,
            desktopMargin: margins * .7
        };

        for (var i in carousel_default) {
            if (e.data(i) !== undefined) {
                carousel_default[i] = e.data(i);
            } else if (e.data(i.toLowerCase()) !== undefined) {
                carousel_default[i] = e.data(i.toLowerCase());
            }
        }
        var margin = carousel_default["margin"];
        carousel_default.responsive = {
            0: {
                items: carousel_default.MobileSmall,
                margin: 0
            },
            321: {
                items: carousel_default.mobile,
                margin: carousel_default.mobileMargin
            },
            481: {
                items: carousel_default.tablet,
                margin: carousel_default.tabletMargin
            },
            769: {
                items: carousel_default.desktopSmall,
                margin: carousel_default.desktopSmallMargin
            },
            993: {
                items: carousel_default.desktop,
                margin: carousel_default.desktopMargin
            },
            1201: {
                items: carousel_default.items,
                margin: margin
            }
        };

        if (carousel_default.model3D) {
            carousel_default.maxHeight = true;
            carousel_default.center = true;
            carousel_default.mouseDrag = false;

            var moves = 0,
                rotate = true;
            e.on("translated.owl.carousel", function () {
                rotate = true;

            });
            var prev = true,
                next = true;
            e.on("drag.owl.carousel", function (x) {
                e.addClass("owl-grab owl-drag");
                moves = moves === 0 ? x.originalEvent.offsetX : moves;
                if (x.originalEvent.offsetX - moves > 20 && rotate && prev) {
                    moves = x.originalEvent.offsetX;
                    e.trigger('prev.owl');
                    next = false;
                }
                if (x.originalEvent.offsetX - moves < -20 && rotate && next) {
                    moves = x.originalEvent.offsetX;
                    e.trigger('next.owl');
                    prev = false;
                }
                if (x.originalEvent.screenX === 0) {
                    moves = 0;
                    prev = true;
                    next = true;
                    e.removeClass("owl-grab owl-drag");
                }
            });
        }

        if (carousel_default.maxHeight) {
            e.on("initialized.owl.carousel resized.owl.carousel refreshed.owl.carousel", function () {
                var max_height = 0;
                e.find(".owl-item.center").addClass("temporary");
                e.find(".owl-item").addClass("center");


                e.find(".owl-item").removeClass("center");
                e.find(".owl-item.temporary").addClass("center").removeClass("temporary");
                function owl_height() {
                    max_height = 0;
                    e.find(".owl-item.center").each(function () {
                        max_height = Math.max(max_height, $(this).height());
                    });
                    e.find(".owl-stage-outer").css("height", max_height);
                }
                owl_height();
                setTimeout(function () {
                    owl_height();

                }, 600)

            });
        }

        if (carousel_default.center) {
            e.on("initialized.owl.carousel translate.owl.carousel", function (x) {
                e.find(".owl-item.next").removeClass("next");
                e.find(".owl-item.prev").removeClass("prev");
                e.find(".owl-item").eq(x.item.index).addClass("center");
                e.find(".owl-item").eq(x.item.index + 1).addClass("prev");
                e.find(".owl-item").eq(x.item.index - 1).addClass("next");

            });
        }


        e.addClass("loaded-before").owlCarousel(carousel_default);


        $(window).scroll();
    }
    // Client Zone Table 重构
    var clientZoneTable = function () {
        module.find("table").each(function (index) {
            var $this = $(this);

            if ($this.attr("id") != "table-download") {

                $this.wrap('<div class="client-zone-table"></div>');
                $this.addClass("table");

                // width -> max-width
                if ( typeof($this.attr("style")) == "string" ) {
                    var maxWidth = $this.attr("style");
                    maxWidth = maxWidth.replace("width", "max-width");
                    $this.attr("style", maxWidth);
                }
            
                $this.attr("data-index", index);   // 设置角标
                $this.desktop = $this.html();
                $this.mobile = $this.html();

                htmlTable["table_" + index] = $this.html();

                // 获得标题
                var caption = $this.find("caption");
                // 获得所有TR
                var aTr = $this.find("tr");

                // 有 Thead 就重构
                if ($this.find("thead").length != 0) {
                    $this.mobile = "";
                    if (caption.length != 0) {
                        $this.mobile += "<caption>"+caption.html()+"</caption>";
                    }
                    $this.mobile += "<tbody>";

                    // 次数为 第一行的列数
                    for (let i = 0, arr = new Array() ; i < $(aTr[0]).children().length; i++) {

                        aTr.each(function (index) {

                            //判断TD是否存在
                            if ( $(this).children().eq(i)[0] ) {
                                $this.mobile += $(this).children().eq(i)[0]["outerHTML"];

                                //存储跨列数
                                arr[index] = $(this).children().eq(i)[0].colSpan;
                            } else {
                                if(arr[index] !=1){
                                    let j = 0;
                                    while (!$(this).children().eq(i - j)[0]) {
                                        j++;
                                    }
                                    $this.mobile += $(this).children().eq(i - j)[0]["outerHTML"];
                                    arr[index] = arr[index]-1;
                                }
                            }

                
                        });
            

                    }

                    $this.mobile += "</tbody>";

                }

                function refactorTbale() {
                    if ($(window)[0].innerWidth < "767") {
                        $this.html($this.mobile);
                    } else {
                        $this.html($this.desktop);
                    }
                
                }
                refactorTbale();
                $(window).resize(refactorTbale);
            }


        });

    };
    // 显示更新的数量
    var clientZoneNav = function () {
        var nav = module.find(".client-zone-nav");

        if (nav.length == "0") return;

        var ajaxnavurl = module.find(".client-zone-jurisdictions").data("ajax-nav");
        var servertime = module.find(".client-zone-jurisdictions").data("servertime");

        var item = module.find(".client-zone-nav>.tool-list>li>span");

        $.getJSON(ajaxnavurl, function (json) {

            if (json.GameSheetCount != 0) {
                item.eq(1).html(json.GameSheetCount);
                item.eq(1).fadeIn();
            }
            if (json.DownloadFileCount != 0) {
                item.eq(2).html(json.DownloadFileCount);
                item.eq(2).fadeIn();
            }
            if (json.CampaignCount != 0) {
                item.eq(3).html(json.CampaignCount);
                item.eq(3).fadeIn();
            }
            if (json.EventCount != 0) {
                item.eq(4).html(json.EventCount);
                item.eq(4).fadeIn();
            }

            $.cookie(page + '-ClickTime', servertime, { expires: 365, path: '/' });
        });

        
    }

    //----------------------------------------------------/
    // Client Zone Modal
    //----------------------------------------------------/
    var clientZoneModal = function () {
        
        var ModalOpen = module.find(".ModalOpen");
        var ModalClose = module.find(".ModalClose");

        module.find(".Modal").each(function () {
            var Modal = $(this);
            

            var group = module.find(".client-zone-group");
            var jurisdictions = module.find(".client-zone-jurisdictions");
            var nav = module.find(".client-zone-nav");
            var active = module.find(".client-zone-nav .active");
            var filter = module.find(".checkbox-box.filter");
            var breadcrumb = module.find(".client-zone-breadcrumb");

            ModalOpen.click(function () {
                Modal.fadeIn();
                ModalOpen.addClass("None");
                zoneMain.css({ "box-shadow": "none" });
                $('html,body').css("overflow", "hidden");
                return false;
            });


            ModalClose.click(function () {
                Modal.fadeOut();
                ModalOpen.removeClass("None");
                zoneMain.css({ "box-shadow": "0 0 10px rgba(0,0,0,0.3)" });
                $('html,body').css("overflow", "visible");
                return false;
            });

            if ($(window)[0].innerWidth <= "768") {
                appendModal();
            }


            $(window).resize(function () {
                if ($(window)[0].innerWidth <= "768") {
                    appendModal();
                } else {
                    Modal.hide();
                    ModalOpen.removeClass("None");
                    $('html,body').css("overflow", "visible");
                    appendGroup();
                }
            });



            function appendModal() {
                Modal.append(jurisdictions);
                Modal.append(nav);
                if (filter.length != 0) {
                    active.after(filter);
                }
                
            }


            function appendGroup() {
                group.prepend(nav);
                group.prepend(jurisdictions);
                if (filter.length != 0) {
                    ajaxdata.prepend(filter);
                }
            }

        });

    };


    // jurisdictionsAjax
    var jurisdictionsAjax = function () {
        var jurisdictions = module.find(".client-zone-jurisdictions input[type=checkbox]");
        var jurisdictionsurl = module.find(".client-zone-jurisdictions").data("ajaxurl");

        //默认值
        var aChecked = new Array();
        for (var i = 0; i < jurisdictions.length; i++) {
            if (jurisdictions.eq(i).is(':checked')) {
                aChecked.push(jurisdictions.eq(i).val());
            }
        }
        dataAjax["jurisdictions"] = aChecked.join(",");


        // jurisdictions选择
        jurisdictions.each(function () {

            $(this).change(function () {
                var aChecked = new Array();

                for (var i = 0; i < jurisdictions.length; i++) {
                    if (jurisdictions.eq(i).is(':checked')) {
                        aChecked.push(jurisdictions.eq(i).val());
                    }
                }
                dataAjax["jurisdictions"] = aChecked.join(",");
                $.getJSON(jurisdictionsurl, { SelectJurisdictions: dataAjax["jurisdictions"] }, function (json) {
                    //console.log("Jurisdictions:", json);
                });

                switch (page) {
                    case "ProductRoadmap":
                        ajaxPage();

                        break;
                    case "GameSheets":
                    case "Campaigns":
                    case "Events":

                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        initGameList();

                        break;

                    case "Downloads":
                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        oTable.ajax.reload();

                        break;
                    default:
                }

            });
        });
    }

    // GategoryAjax
    var gategoryAjax = function () {
        var gameGategorys = module.find(".client-zone-main .checkbox-box .item").eq(0).find("input[type=checkbox]");
        var gategoryurl = module.find(".client-zone-main .checkbox-box .item").eq(0).data("ajaxurl");

        if (gameGategorys.length == 0) return false;

        //默认值
        var aChecked = new Array();
        for (var i = 0; i < gameGategorys.length; i++) {
            if (gameGategorys.eq(i).is(':checked')) {
                aChecked.push(gameGategorys.eq(i).val());
            }
        }
        dataAjax["GameGategorys"] = aChecked.join(",");

        // GameGategorys选择
        gameGategorys.each(function () {

            $(this).change(function () {
                var aChecked = new Array();

                for (var i = 0; i < gameGategorys.length; i++) {
                    if (gameGategorys.eq(i).is(':checked')) {
                        aChecked.push(gameGategorys.eq(i).val());
                    }
                }
                dataAjax["GameGategorys"] = aChecked.join(",");
                $.getJSON(gategoryurl, { SelectGameCategories: dataAjax["GameGategorys"] }, function (json) {
                    //console.log("GameGategorys:", json);
                });

                switch (page) {
                    case "ProductRoadmap":
                        ajaxPage();

                        break;
                    case "GameSheets":
                    case "Campaigns":

                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        initGameList();

                        break;

                    case "Downloads":
                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        oTable.ajax.reload();

                        break;
                    default:
                }

            });
        });

    }
    // FileTypesAjax
    var fileTypesAjax = function () {
        var fileTypes = module.find(".client-zone-main .checkbox-box .item").eq(1).find("input[type=checkbox]");
        var fileTypesurl = module.find(".client-zone-main .checkbox-box .item").eq(1).data("ajaxurl");

        if (fileTypes.length == 0) return false;

        //默认值
        var aChecked = new Array();
        for (var i = 0; i < fileTypes.length; i++) {
            if (fileTypes.eq(i).is(':checked')) {
                aChecked.push(fileTypes.eq(i).val());
            }
        }
        dataAjax["FileTypes"] = aChecked.join(",");

        // fileTypes选择
        fileTypes.each(function () {

            $(this).change(function () {
                var aChecked = new Array();

                for (var i = 0; i < fileTypes.length; i++) {
                    if (fileTypes.eq(i).is(':checked')) {
                        aChecked.push(fileTypes.eq(i).val());
                    }
                }
                dataAjax["FileTypes"] = aChecked.join(",");
                $.getJSON(fileTypesurl, { SelectFileTypes: dataAjax["FileTypes"] }, function (json) {
                    //console.log("FileTypes:", json);
                });

                switch (page) {

                    case "Downloads":
                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        oTable.ajax.reload();

                        break;
                    default:
                }

            });
        });

    }
    // 获取Gategory的相关内容
    var gategoryAjaxOld = function () {
        var gategory = module.find(".client-zone-main .checkbox-box .item");
        if (gategory.length == 0) return false;
        //默认值
        //var GameGategoryCookie = $.cookie('GameGategory');
        var GameGategoryCookie = null;
        var GameGategorys = gategory.eq(0).find("input[type=checkbox]");
        var aGameGategoryChecked = new Array();

        switch (page) {
            case "ProductRoadmap":

                if ( GameGategoryCookie == null ) {
                    GameGategorys.prop('checked', true);
                    for (var i = 0; i < GameGategorys.length; i++) {
                        aGameGategoryChecked.push(GameGategorys.eq(i).val());
                    }
                } else {
                    var arrGameGategory = GameGategoryCookie.split(",");
                    for (var i = 0; i < GameGategorys.length; i++) {
                        if ( arrGameGategory.indexOf(GameGategorys.eq(i).val()) != -1 ) {
                            GameGategorys.eq(i).prop('checked', true);
                            aGameGategoryChecked.push(GameGategorys.eq(i).val());
                        }
                        
                    }
                }
                //$.cookie('GameGategory', aGameGategoryChecked.join(","), { path: '/' });
                dataAjax["GameGategorys"] = aGameGategoryChecked.join(",");
                break;

            case "GameSheets":
            case "Campaigns":
                if (GameGategoryCookie == null) {
                    GameGategorys.prop('checked', true);
                    for (var i = 0; i < GameGategorys.length; i++) {
                        aGameGategoryChecked.push(GameGategorys.eq(i).val());
                    }
                } else {
                    var arrGameGategory = GameGategoryCookie.split(",");
                    for (var i = 0; i < GameGategorys.length; i++) {
                        if (arrGameGategory.indexOf(GameGategorys.eq(i).val()) != -1) {
                            GameGategorys.eq(i).prop('checked', true);
                            aGameGategoryChecked.push(GameGategorys.eq(i).val());
                        }

                    }
                }
                //$.cookie('GameGategory', aGameGategoryChecked.join(","), { path: '/' });
                dataAjax["GameGategorys"] = aGameGategoryChecked.join(",");
                break;

            case "Downloads":
                //var FileTypesCookie = $.cookie('FileTypes');
                var FileTypesCookie = null;
                var FileTypes = gategory.eq(1).find("input[type=checkbox]");
                var aFileTypesChecked = new Array();

                if ( GameGategoryCookie == null) {
                    GameGategorys.prop('checked', true);
                    for (var i = 0; i < GameGategorys.length; i++) {
                        aGameGategoryChecked.push(GameGategorys.eq(i).val());
                    }
                } else {
                    var arrGameGategory = GameGategoryCookie.split(",");
                    for (var i = 0; i < GameGategorys.length; i++) {
                        if (arrGameGategory.indexOf(GameGategorys.eq(i).val()) != -1) {
                            GameGategorys.eq(i).prop('checked', true);
                            aGameGategoryChecked.push(GameGategorys.eq(i).val());
                        }

                    }
                }

                //$.cookie('GameGategory', aGameGategoryChecked.join(","), { path: '/' });
                dataAjax["GameGategorys"] = aGameGategoryChecked.join(",");


                if (FileTypesCookie == null) {
                    FileTypes.prop('checked', true);
                    for (var i = 0; i < FileTypes.length; i++) {
                        aFileTypesChecked.push(FileTypes.eq(i).val());
                    }
                } else {
                    var arrFileTypes = FileTypesCookie.split(",");
                    for (var i = 0; i < FileTypes.length; i++) {
                        if (arrFileTypes.indexOf(FileTypes.eq(i).val()) != -1) {
                            FileTypes.eq(i).prop('checked', true);
                            aFileTypesChecked.push(FileTypes.eq(i).val());
                        }

                    }
                }
                
                //$.cookie('FileTypes', aFileTypesChecked.join(","), { path: '/' });
                dataAjax["FileTypes"] = aFileTypesChecked.join(",");
                break;
            default:
        }

        switch (page) {
            case "ProductRoadmap":
                GameGategorys.each(function () {
                    $(this).change(function () {
                        var aChecked = new Array();

                        for (var i = 0; i < GameGategorys.length; i++) {
                            if (GameGategorys.eq(i).is(':checked')) {
                                aChecked.push(GameGategorys.eq(i).val());
                            }
                        }

                        //$.cookie('GameGategory', aChecked.join(","), { path: '/' });
                        dataAjax["GameGategorys"] = aChecked.join(",");

                        ajaxPage();
                    });
                });
                

                break;
            case "GameSheets":
            case "Campaigns":
                GameGategorys.each(function () {
                    $(this).change(function () {
                        var aChecked = new Array();

                        for (var i = 0; i < GameGategorys.length; i++) {
                            if (GameGategorys.eq(i).is(':checked')) {
                                aChecked.push(GameGategorys.eq(i).val());
                            }
                        }

                        //$.cookie('GameGategory', aChecked.join(","), { path: '/' });
                        dataAjax["GameGategorys"] = aChecked.join(",");

                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        initGameList();
                    });
                });

                break;

            case "Downloads":
                GameGategorys.each(function () {
                    $(this).change(function () {
                        var aChecked = new Array();

                        for (var i = 0; i < GameGategorys.length; i++) {
                            if (GameGategorys.eq(i).is(':checked')) {
                                aChecked.push(GameGategorys.eq(i).val());
                            }
                        }

                        //$.cookie('GameGategory', aChecked.join(","), { path: '/' });
                        dataAjax["GameGategorys"] = aChecked.join(",");

                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        oTable.ajax.reload();
                    });
                });
                FileTypes.each(function () {
                    $(this).change(function () {
                        var aChecked = new Array();

                        for (var i = 0; i < FileTypes.length; i++) {
                            if (FileTypes.eq(i).is(':checked')) {
                                aChecked.push(FileTypes.eq(i).val());
                            }
                        }

                        //$.cookie('FileTypes', aChecked.join(","), { path: '/' });
                        dataAjax["FileTypes"] = aChecked.join(",");

                        options["currentPage"] = 1;
                        dataAjax["pageindex"] = 1;

                        oTable.ajax.reload();
                    });
                });

                break;
            default:

        }

        switch (page) {

            case "GameSheets":
            case "Campaigns":

                break;
            case "Downloads":

                break;
            default:
        }
    }


    var Escape2Html  = function(str) {
        var arrEntities = { 'lt': '<', 'gt': '>', 'nbsp': ' ', 'amp': '&', 'quot': '"' };
        return str.replace(/&(lt|gt|nbsp|amp|quot);/ig, function (all, t) { return arrEntities[t]; });
    }


    // Client Zone Seach
    var clientZoneSeach = function () {
        var search = module.find(".client-zone-search");
        if (search.length == 0) return false;

        var searchInput = module.find(".client-zone-search input");
        var searchBtn = module.find(".client-zone-search a.fa-search");
        var searchClear = module.find(".client-zone-search a.fa-times");


        // 按键搜索
        searchInput.autocomplete({
            minLength: 0,
            delay: 500,
            source: function (request, response) {
                dataAjax["search"] = searchInput.val();

                $.getJSON(ajaxsearchurl, dataAjax , function (json) {

                    response($.map(json.data, function (item) {
                        var Title = $("<div></div>");
                        Title.html(item.Title);
                        return {
                            label: Escape2Html(Title.html()),
                            value: item.ID
                        }
                    }));
                });

                if ( searchInput.val() != "") {
                    searchClear.fadeIn();
                } else {
                    searchClear.fadeOut();
                }
            },
            focus: function (event, ui) {
                if (event.charCode == 0) {
                    searchInput.val(ui.item.label);
                }
                return false;
            },
            select: function (event, ui) {
                searchInput.val(ui.item.label);

                dataAjax["search"] = searchInput.val();
                options["currentPage"] = 1;
                dataAjax["pageindex"] = 1;

                switch (module.data("token")) {
                    case "GameSheets":
                    case "Campaigns":
                    case "Events":
                        initGameList();
                        break;

                    case "Downloads":
                        oTable.ajax.reload();
                        break;
                    default:
                }

                return false;
            }
        });

        searchInput.keydown(function (event) {
            if (event.keyCode == 13) {
                dataAjax["search"] = searchInput.val();

                options["currentPage"] = 1;
                dataAjax["pageindex"] = 1;

                $(this).autocomplete("close");

                switch (module.data("token")) {
                    case "GameSheets":
                    case "Campaigns":
                    case "Events":
                        initGameList();
                        break;

                    case "Downloads":
                        oTable.ajax.reload();
                        break;
                    default:
                }

                event.preventDefault()
            }

        });
        // 点击搜索
        searchBtn.click(function () {
            dataAjax["search"] = searchInput.val();

            options["currentPage"] = 1;
            dataAjax["pageindex"] = 1;

            switch (module.data("token")) {
                case "GameSheets":
                case "Campaigns":
                case "Events":
                    initGameList();
                    break;

                case "Downloads":
                    oTable.ajax.reload();
                    break;
                default:
            }

            return false;
        });

        // 点击清楚
        searchClear.click(function () {
            searchInput.val("");

            dataAjax["search"] = searchInput.val();

            options["currentPage"] = 1;
            dataAjax["pageindex"] = 1;

            switch (module.data("token")) {
                case "GameSheets":
                case "Campaigns":
                case "Events":
                    initGameList();
                    break;

                case "Downloads":
                    oTable.ajax.reload();
                    break;
                default:
            }

            $(this).fadeOut();
            return false;
        });
    }

    // Client Zone Select
    var clientZoneSelect = function () {
        var term = module.find(".gameList-term");
        if (term.length == 0) return false;

        // 获取右侧筛选的元素
        var termLi = module.find(".gameList-term li");
        var selected = module.find(".gameList-term button:first");

        // 点击切换排序方式
        termLi.each(function () {
            $(this).click(function () {

                selected.text($(this)[0].innerText);

                dataAjax["sort"] = $(this).data("sort");


                initGameList();

            });
        });

    }

    // Client Zone Select
    var clientZonePageLength = function () {
        var term = module.find(".download-pageLength");
        if (term.length == 0) return false;

        // 获取右侧筛选的元素
        var pageLength = module.find(".download-pageLength li");
        var selected = module.find(".download-pageLength button:first");

        // 点击切换Page数量
        pageLength.each(function () {
            $(this).click(function () {

                selected.text($(this)[0].innerText);

                options["currentPage"] = 1;
                dataAjax["pageindex"] = 1;

                dataAjax["PageSize"] = $(this).data("pagesize");

                oTable.ajax.reload();

            });
        });

    }

    // Client Zone Download
    var clientZoneDownload = function () {

        // 选择
        var select = module.find("#table-download-select");
        if (select.length == 0) return false;
        select.click(function () {
            var allSelect = module.find(".client-zone-downloads tbody input[type=checkbox]");
            if (select.is(':checked')) {
                allSelect.prop('checked', true);
            } else {
                allSelect.prop('checked', false);
            }
        });

        // 下载
        var download = module.find(".table-download-all");
        download.click(function () {
            var list = [];
            var allSelect = module.find(".client-zone-downloads tbody input[type=checkbox]");
            allSelect.each(function (index) {
                if (module.find(this).is(':checked')) {
                    list.push(module.find(this).val());
                };
            });
            if (list.length != 0) {
                url = ajaxdownloadallurl + "&FileIds=" + encodeURI(list);
                window.open(url);
            } else {
                alert("Please select a file");
            }
            return false;
        });

    };

    // Client Zone Account Update
    var clientZoneAccountUpdate = function () {
        var update = module.find(".account-update");
        if (update.length == 0) return false;

        var plan = module.find(".client-content-myaccount");
        var ajaxLoader = module.find(".ajax-loader");
        var ajaxStatus = module.find(".ajax-status");
        // 声明计时器
        var timer = null;
        // 判断是否点击,false为没执行状态
        var clickBtn = false;

        update.click(function () {
            if (clickBtn) return false;

            var Password = $("input#Password");
            var confirmPassword = $("input#confirmPassword");


            if (Password.val() != "" && ( Password.val() != confirmPassword.val())) {
                alterHtml = '<div class="alert dg-alert01 border-danger color-danger">';
                alterHtml += '<span class="icon fa fa-warning"></span>Inconsistent password';
                alterHtml += '</div>';

                ajaxStatus.html(alterHtml);
                ajaxStatus.fadeIn();

                Password.addClass("danger");
                confirmPassword.addClass("danger");


                clearTimeout(timer);

                timer = setTimeout(function () {
                    ajaxStatus.fadeOut();
                }, 3000);

                Password.focus(function () {
                    Password.removeClass("danger");
                    confirmPassword.removeClass("danger");
                });
                confirmPassword.focus(function () {
                    Password.removeClass("danger");
                    confirmPassword.removeClass("danger");
                });

                return false;
            }

            var json = {
                "FirstName": $("input#FirstName").val(),
                "LastName": $("input#LastName").val(),
                "Company": $("input#Company").val(),
                "Password": $("input#Password").val(),
                "Newsletter_GameSheets": $("input#Newsletter_GameSheets").is(':checked'),
                "Newsletter_Downloads": $("input#Newsletter_Downloads").is(':checked'),
                "Newsletter_Campaigns": $("input#Newsletter_Campaigns").is(':checked'),
                "Newsletter_Events": $("input#Newsletter_Events").is(':checked')
            };

            $.ajax({
                url: ajaxurl,
                type: "POST",
                data: json,
                dataType: "json",
                beforeSend: function () {
                    clickBtn = true;
                    ajaxLoader.stop().fadeIn();

                },
                error: function (err) {
                    console.log(err);
                },
                success: function (data) {

                    Password.val('');
                    confirmPassword.val('');

                    if (data.Result) {
                        alterHtml = '<div class="alert dg-alert01 border-success color-success">';
                        alterHtml += '<span class="icon fa fa-warning"></span>' + data.Message;
                        alterHtml += '</div>';
                    } else {
                        alterHtml = '<div class="alert dg-alert01 border-danger color-danger">';
                        alterHtml += '<span class="icon fa fa-warning"></span>' + data.Message;
                        alterHtml += '</div>';
                    }
                    ajaxStatus.html(alterHtml);
                    ajaxStatus.fadeIn();

                    clearTimeout(timer);
                    timer = setTimeout(function () {
                        ajaxStatus.fadeOut();
                    }, 3000);
                },
                complete: function () {
                    clickBtn = false;
                    ajaxLoader.hide();

                }
            });
        });

    };

    // ajaxPage
    var ajaxPage = function () {

        switch (page) {
            case "ProductRoadmap":

                var date = new Date().getTime();

                var carouselZone = module.find(".client-content-productroadmap > .client-zone-carousel");
                var carousel = module.find(".client-content-productroadmap > .client-zone-carousel > .owl-carousel-zone");



                // 清除样式
                carouselZone.height(carouselZone.height());
                carousel.html("");
                var carouselWrap = carousel[0].outerHTML;
                carousel.remove();

                // 重新生成样式
                carouselZone.append(carouselWrap);
                carousel = module.find(".client-content-productroadmap > .client-zone-carousel > .owl-carousel-zone");

                carouselZone.append('<div class="ajax-loader"></div>');
                carouselZone.find(".ajax-loader").stop().fadeIn();


                $.getJSON(ajaxurl, dataAjax, function (json) {

                    module.find("#client-zone-carousel-tmpl").tmpl(json.data).appendTo(carousel);


                    carousel.each(initCarousel);
                    carousel.each(function () {
                        var owl = $(this);

                        for (i = 0; i < json.data.length; i++) {

                            if (date > json.data[i].ReleaseDate.replace(/[^0-9]/ig, "")) {

                                if ( i != json.data.length - 1) {
                                    if (date > json.data[i + 1].ReleaseDate.replace(/[^0-9]/ig, "")) {
                                        continue;
                                    } else {
                                        owl.trigger('to.owl.carousel', [i+1, 0])
                                    }
                                } else {
                                    owl.trigger('to.owl.carousel', [json.data.length - 1, 0]);
                                }


                            }
                        }



                    });

                    
                    carouselZone.height("auto");
                    carouselZone.find(".ajax-loader").remove();
                });
                break;
            default:
        }
        

    };

    // 初始化项目输出
    var initGameList = function () {

        var gameList = module.find(".client-zone-gameList");
        if (gameList.length == 0) return false;

        var Timeout = null;;

        //默认时间倒叙
        if( !dataAjax["sort"] ){
            dataAjax["sort"] = "1";
        }
        gameList.height(gameList.height());

        
        if (gameList.find(".ajax-loader").length == 0) {
            gameList.append('<div class="ajax-loader"></div>');
            gameList.find(".ajax-loader").stop().fadeIn();
        }


        $.getJSON(ajaxurl, dataAjax, function (json) {

            clearTimeout(Timeout);
            Timeout = setTimeout(function () {
                gameList.html("");
                module.find("#client-zone-gameList-tmpl").tmpl(json.data).appendTo(".client-zone-gameList");

                var aItem = module.find(".client-zone-gameList .gameList-item");

                aItem.each(function (index) {
                    var item = $(this)
                    setInterval(function () {
                        item.css("opacity", "1");
                    }, (index + 1) * 100);
                });
                gameList.height("auto");

                options["totalPages"] = json.Pages;

                module.find('#pagenavi').bootstrapPaginator(options);
            }, 800);


        });
        
    };

    // 初始化下载列表
    var downloadsList = function () {
        var download = module.find('#table-download');
        if (download.length == 0) return false;

        dataAjax["Orderfld"] = "ReleaseDate";
        dataAjax["OrderType"] = "1";

        oTable = download.DataTable({
            //"iDeferLoading": 0,
            "serverSide": true,
            "responsive": true,
            "info": false,
            "searching": false,
            "paging": false,
            "autoWidth": false,
            "ajax": {
                "url": ajaxurl,
                "data": function () {
                    return dataAjax
                }
            },
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "language": {
                "search": "",
                "lengthMenu": "_MENU_",
                "paginate": {
                    "previous": '<span class="fa fa-angle-left"></span>',
                    "next": '<span class="fa fa-angle-right"></span>',
                }
            },
            "columns": [
                {
                    "data": "ID",
                    "render": function (data, type, full, meta) {
                        return '<input id="table-download-id-' + data + '" type="checkbox" value="' + data + '">';
                    }
                },
                { "data": "ReleaseDateString" },
                { "data": "Title" },
                {
                    "data": "FileTypesString",
                    "render": function (data, type, full, meta) {
                        return '<span class="' + full.NotificationStatusClass + '">' + full.NotificationStatus + '</span>' + data;
                    }
                },
                { "data": "Version" },
                {
                    "data": "DownloadUrl",
                    "render": function (data, type, full, meta) {
                        return '<a class="table-download-link" target="_blank" href="' + data + '">Download<span class="lnr lnr-download2"></span></a>';
                    }
                }

            ],
            "columnDefs": [
                { "orderable": false, "targets": 0 },
                { "orderable": false, "targets": 4 },
                { "orderable": false, "targets": 5 },
                { "responsivePriority": 1, "targets": 0 },
                { "responsivePriority": 1, "targets": 2 }
            ],
            "deferRender": true
        });


        oTable.on('preXhr.dt', function (e, settings, data) {

            if (download.find(".ajax-loader").length == 0) {
                download.append('<div class="ajax-loader"></div>');
            }
            download.find(".ajax-loader").stop().fadeIn();
        })

        oTable.on('xhr.dt', function (e, settings, json, xhr) {

            download.find(".ajax-loader").remove();

            options["totalPages"] = json.Pages;
            module.find('#pagenavi').bootstrapPaginator(options);

        })


        oTable.on('processing.dt', function (e, settings, processing) {

            //事件正在处理，且，不是默认ID排序
            if (processing && settings.aaSorting[0][0] != "0") {

                switch (settings.aaSorting[0][0]) {
                    case 0:
                        dataAjax["Orderfld"] = "ID";
                        break;
                    case 1:
                        dataAjax["Orderfld"] = "ReleaseDate";
                        break;
                    case 2:
                        dataAjax["Orderfld"] = "Title";
                        break;
                    case 3:
                        dataAjax["Orderfld"] = "FileTypes";
                        break;
                    case 4:
                        dataAjax["Orderfld"] = "Version";
                        break;
                    case 5:
                        dataAjax["Orderfld"] = "DownloadUrl";
                        break;

                    default:
                }

                if (settings.aaSorting[0][1] == "asc") {
                    dataAjax["OrderType"] = "0";
                } else {
                    dataAjax["OrderType"] = "1";
                }

            } else {

                module.find(".client-zone-downloads tbody tr .sorting_1 input").each(function () {
                   
                    $(this).click(function (e) {
                        e.stopPropagation();
                    });

                });

            }




        });


    }

    // HTML Print
    var htmlPrint = function () {
        sheet = module.find(".client-zone-tool .sheet");
        if (sheet.length == 0) return false;

        sheet.click(function () {
            if (module.find(".htmlPrint").length == 0) {

                zoneMain.append('<div class="htmlPrint"></div>');
                var htmlPrint = module.find(".htmlPrint");
               
                $('html,body').css("overflow", "hidden");


                switch (module.data("token")) {
                    case "GameSheets":
                    case "Campaigns":
                        var zoneInfo = module.find(".client-zone-info");
                        zoneInfo.clone().prependTo(htmlPrint);

                        
                        break;

                    case "Events":

                        break;
                    default:
                }


                // 拷贝tab数据

                var zoneTabLi = module.find(".resp-tabs-list .Print-PDF");

                var zoneTabContainer = module.find(".resp-tabs-container .Print-PDF");

                var tabHtml = "";

                zoneTabLi.each(function (index) {
                    tabHtml += '<div class="htmlPrint-Page">';
                    tabHtml += '<div class="htmlPrint-Tltle">';
                    tabHtml += $(this).html();
                    tabHtml += '</div>';
                    tabHtml += zoneTabContainer.eq(index).html();
                    tabHtml += '</div>';
                });


                htmlPrint.append(tabHtml);


                // Table 重构

                htmlPrint.find(".client-zone-table .table").each(function () {
                    var $this = $(this);
                    var index = $this.data("index");

                    $this.html(htmlTable["table_" + index]);

                });


                // 旋转木马重构
                htmlPrint.find(".owl-carousel").each(function () {
                    var carousel = $(this);

                    var carouselHtml = "";
                    carousel.find(".owl-item:not(.cloned) .pic").each(function () {

                        carouselHtml += $(this).html();
                    });
                    carousel.html(carouselHtml);

                })


                if (htmlPrint.html() == "" ){
                    $('html,body').css("overflow", "visible");
                } else {
                    zoneMain.append('<div class="ajax-loader"></div>');
                    zoneMain.find(".ajax-loader").stop().fadeIn();



                    setTimeout(function () {

                        //svgToCanvas(htmlPrint);

                        htmlToPdf(htmlPrint);

                    }, 0);
                    
                }

            }




            return false;
        });




    }
    // SVG To Canvas
    var svgToCanvas = function (targetDom) {

        /*如果你的页面中有svg，请开启以下功能
        因为html2canvas 不能完全识别svg或者不识别svg中部分元素的属性，如:entirety.html页面中的filter、text-anchor ,所以要将svg先转成canvas*/
        var svg = targetDom.find('svg');
        var svgParentNode = svg.parent();
        function svg2canvas(targetElement) {
            var svgElement = targetElement.find('svg');
            svgElement.each(function (index, node) {
                var parentNode = node.parentNode;
                //因为ie浏览器不能直接取svg的内容，所以先新建一个临时div
                var temporary = document.createElement('div'); //临时div
                temporary.appendChild(node);
                var svg = temporary.innerHTML;
                var canvas = document.createElement('canvas');
                canvg(canvas, svg); //用引入的canvg2.js中的 canvg函数转化svg
                parentNode.insertBefore(canvas, parentNode.childNodes[0]);
            });

            //TODO:多个svg
        }
        if (svg.length > 0) {
            svg2canvas(targetDom);
        }
        setTimeout(function () { svgParentNode.empty().append(svg); }, 500);

    };

    // HTML TO PDF
    var htmlToPdf = function htmlToPdf(dom) {
        
        var PdfName = "content.pdf";
        switch (page) {
            case "ProductRoadmap":
                PdfName = "Product-Roadmap.pdf";

                break;
            case "GameSheets":
            case "Campaigns":
                PdfName = module.find(".client-zone-breadcrumb a:last").text()+".pdf";

                break;
            case "Help":
                PdfName = "Help.pdf"

                break;
            default:
        }

        sheet = module.find(".client-zone-tool .sheet.PDFPageBreak").length != 0;

        if ( !sheet ) {
            // 按白色间隙动态为A4纸分页
            domtoimage.toCanvas(dom[0], { bgcolor: "#FFF" })
                .then(function (canvas) {
                    module.find(".htmlPrint").remove();
                    //未生成pdf的html页面高度
                    var leftHeight = canvas.height;
                    //页面偏移
                    var position = 0
                    var a4Width = 595.28
                    var a4Height = 841.89
                    var a4HeightRef = Math.floor(canvas.width / a4Width * a4Height);
                    var pageData = canvas.toDataURL('image/jpeg', 1.0);

                    var pdf = new jsPDF('', 'pt', 'a4');
                    var index = 0,
                        canvas1 = document.createElement('canvas'),
                        height;

                    pdf.setDisplayMode('fullwidth', 'continuous', 'FullScreen');
                    zoneMain.find(".ajax-loader").append($(
                        '<div class="pdfTip">' +
                        '   <div>The <span class="pdfProgress">1st</span> page is being generated, there are <span class="pdfTotal"></span> pages in total' +
                        '</div>'));

                    function createImplA4(canvas) {
                        if (leftHeight > 0) {
                            index++;

                            var checkCount = 0;
                            if (leftHeight > a4HeightRef) {
                                var i = position + a4HeightRef;
                                for (i = position + a4HeightRef; i >= position; i--) {
                                    var isWrite = true;
                                    for (var j = 0; j < canvas.width; j++) {
                                        var c = canvas.getContext('2d').getImageData(j, i, 1, 1).data;
                                        if (c[0] != 0xff || c[1] != 0xff || c[2] != 0xff) {
                                            isWrite = false;
                                            break
                                        }
                                    }
                                    if (isWrite) {
                                        checkCount++
                                        if (checkCount >= 10) {
                                            break
                                        }
                                    } else {
                                        checkCount = 0
                                    }
                                }
                                height = Math.round(i - position) || Math.min(leftHeight, a4HeightRef);
                                if (height <= 0) {
                                    height = a4HeightRef;
                                }
                            } else {
                                height = leftHeight;
                            }
                            canvas1.width = canvas.width;
                            canvas1.height = height;
                            // console.log(index, 'height:', height, 'pos', position);
                            var ctx = canvas1.getContext('2d');
                            ctx.drawImage(canvas, 0, position, canvas.width, height, 0, 0, canvas.width, height);
                            var pageHeight = Math.round(a4Width / canvas.width * height);
                            pdf.setPage(index, pageHeight);
                            pdf.addPage();
                            pdf.addImage(canvas1.toDataURL('image/jpeg', 1.0), 'JPEG', 0, 0, a4Width, a4Width / canvas1.width * height)
                            leftHeight -= height;
                            position += height

                            if (index == 1) {
                                zoneMain.find('.pdfProgress').text(index + "st");
                            } else if (index == 2) {
                                zoneMain.find('.pdfProgress').text(index + "nd");
                            } else if (index == 6) {
                                zoneMain.find('.pdfProgress').text(index + "rd");
                            } else {
                                zoneMain.find('.pdfProgress').text(index + "th");
                            }

                            zoneMain.find('.pdfTotal').text(index + Math.ceil(leftHeight / a4HeightRef))
                            if (leftHeight > 0) {
                                setTimeout(createImplA4, 500, canvas);
                            } else {
                                pdf.save(PdfName);

                                zoneMain.find(".ajax-loader").remove();
                                $('html,body').css("overflow", "visible");
                            }
                        }
                    }

                    //当内容未超过pdf一页显示的范围，无需分页
                    if (leftHeight < a4HeightRef) {

                        pdf.addImage(pageData, 'JPEG', 0, 0, a4Width, a4Width / canvas.width * leftHeight);
                        pdf.save(PdfName);
                        zoneMain.find(".ajax-loader").remove();
                        $('html,body').css("overflow", "visible");

                    } else {
                        try {
                            pdf.deletePage(1);
                            $('.pdfTip').show();
                            $('.pdfTotal').text(index + Math.ceil(leftHeight / a4HeightRef));
                            setTimeout(createImplA4, 500, canvas);
                        } catch (err) {
                            console.log(err);
                        }
                    }
                })
                .catch(function (error) {
                    console.error('oops, something went wrong!', error);
                });
        }

        if ( sheet) {
        // 按动态模块分页
        domtoimage.toCanvas(dom[0], { bgcolor: "#FFF" })
            .then(function (canvas) {

                var modWidth = module.find(".htmlPrint").innerWidth();
                var infoHeight = module.find(".htmlPrint .client-zone-info").outerHeight(true);

                var arrModHeight = new Array();
                module.find(".htmlPrint .htmlPrint-Page").each(function () {
                    arrModHeight.push($(this).outerHeight(true) );

                });
                module.find(".htmlPrint").remove();
                    
                //未生成pdf的html页面高度
                var leftHeight = canvas.height;
                //页面偏移
                var position = 0;
                var a4Width = 595.28;
                var a4Height = 841.89;

                //宽度为A4，高度为等比例的 单个页面高度
                
                var modHeight = infoHeight + arrModHeight[0];
                var printHeight = a4Width * modHeight / modWidth;

                var HeightRef = Math.floor(canvas.width / a4Width * printHeight);
                var pageData = canvas.toDataURL('image/jpeg', 1.0);

                var PDFSize = [595.28, printHeight];
                var pdf = new jsPDF('', 'pt', PDFSize);
                var index = 0,
                    canvas1 = document.createElement('canvas'),
                    height;

                pdf.setDisplayMode('fullwidth', 'continuous', 'FullScreen');
                zoneMain.find(".ajax-loader").append($(
                    '<div class="pdfTip">' +
                    '   <div>The <span class="pdfProgress">1st</span> page is being generated, there are <span class="pdfTotal"></span> pages in total' +
                    '</div>'));

                function createImpl(canvas) {
                    if (leftHeight > 0) {
                        index++;


                        if (index != 1) {
                            modHeight = arrModHeight[index - 1];
                            printHeight = a4Width * modHeight / modWidth;
                            HeightRef = Math.floor(canvas.width / a4Width * printHeight);
                            PDFSize = [595.28, printHeight];
                        }


                        if (leftHeight > HeightRef) {
                            height = HeightRef;
                        } else {
                            height = leftHeight;
                        }
                        canvas1.width = canvas.width;
                        canvas1.height = height;
                        // console.log(index, 'height:', height, 'pos', position);
                        var ctx = canvas1.getContext('2d');
                        ctx.drawImage(canvas, 0, position, canvas.width, height, 0, 0, canvas.width, height);
                        var pageHeight = Math.round(a4Width / canvas.width * height);

                        if (modHeight !== undefined) {
                            pdf.setPage(index);
                            pdf.addPage(PDFSize);
                            pdf.addImage(canvas1.toDataURL('image/jpeg', 1.0), 'JPEG', 0, 0, a4Width, a4Width / canvas1.width * height)
                        }
                        leftHeight -= height;
                        position += height

                        if (modHeight !== undefined) {
                            if (index == 1) {
                                zoneMain.find('.pdfProgress').text(index + "st");
                            } else if (index == 2) {
                                zoneMain.find('.pdfProgress').text(index + "nd");
                            } else if (index == 6) {
                                zoneMain.find('.pdfProgress').text(index + "rd");
                            } else {
                                zoneMain.find('.pdfProgress').text(index + "th");
                            }
                        }

                        zoneMain.find('.pdfTotal').text(arrModHeight.length)
                        if (leftHeight > 0) {
                            setTimeout(createImpl, 500, canvas);
                        } else {
                            pdf.save(PdfName);

                            zoneMain.find(".ajax-loader").remove();
                            $('html,body').css("overflow", "visible");
                        }
                    }
                }

                //当内容未超过pdf一页显示的范围，无需分页
                if (leftHeight < HeightRef) {

                    pdf.addImage(pageData, 'JPEG', 0, 0, a4Width, a4Width / canvas.width * leftHeight);
                    pdf.save(PdfName);
                    zoneMain.find(".ajax-loader").remove();
                    $('html,body').css("overflow", "visible");

                } else {
                    try {
                        pdf.deletePage(1);
                        $('.pdfTip').show();
                        $('.pdfTotal').text(arrModHeight.length);
                        setTimeout(createImpl, 500, canvas);
                    } catch (err) {
                        console.log(err);
                    }
                }
            })
            .catch(function (error) {
                console.error('oops, something went wrong!', error);
            });

        }

    };






    return {
        //main function to initiate template pages
        init: function (options) {
            $.extend(settings, options);

            module = $(settings.moduleId);

            page = module.data("token");

            zoneMain = module.find(".client-zone-main");

            ajaxdata = module.find(".client-zone-ajax");
            ajaxurl = ajaxdata.data("ajaxurl");
            ajaxsearchurl = ajaxdata.data("ajax-search");
            ajaxdownloadallurl = ajaxdata.data("downloadall-url");

            //owlCarousel2();
            clientZoneTable();
            clientZoneNav();
      
            jurisdictionsAjax();
            gategoryAjax();
            fileTypesAjax();
            clientZoneSeach();
            clientZoneSelect();
            clientZonePageLength();
            clientZoneDownload();
            clientZoneAccountUpdate();
            
            setTimeout(function () {

                ajaxPage();

            }, 0);

            initGameList();
            downloadsList();

            clientZoneModal();

            htmlPrint();


        }
    };
}();

