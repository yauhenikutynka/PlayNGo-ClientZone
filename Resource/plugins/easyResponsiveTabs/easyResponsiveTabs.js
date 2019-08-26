// Easy Responsive Tabs Plugin
// Author: Samson.Onna <Email : samson3d@gmail.com>

(function (e) {
  e.fn.extend({
    easyResponsiveTabs: function (t) {
      var n = {
          type: "default",
          width: "auto",
          fit: !0,
          closed: !1,
          activate: function () {}
        },
        t = e.extend(n, t),
        r = t,
        i = r.type,
        s = r.fit,
        o = r.width,
        u = "vertical",
        a = "accordion",
        f = window.location.hash,
        l = !!window.history && !!history.replaceState;
      e(this).bind("tabactivate", function (e, n) {
        typeof t.activate == "function" && t.activate.call(n, e)
      }), this.each(function () {
        function c() {
          i == u && n.addClass("resp-vtabs"), s == 1 && n.css({
            width: "100%",
            margin: "0px"
          }), i == a && (n.addClass("resp-easy-accordion"), n.find(".resp-tabs-list").css("display", "none"))
        }
        var n = e(this),
          r = n.find("ul.resp-tabs-list"),
          l = n.attr("id");
        n.find("ul.resp-tabs-list li").addClass("resp-tab-item"), n.css({
          display: "block",
          width: o
        }), n.find(".resp-tabs-container > div").addClass("resp-tab-content"), c();
        var h;
        n.find(".resp-tab-content").before("<h2 class='resp-accordion' role='tab'><span class='resp-arrow'></span></h2>");
        var p = 0;
        n.find(".resp-accordion").each(function () {
          h = e(this);
          var t = n.find(".resp-tab-item:eq(" + p + ")"),
            r = n.find(".resp-accordion:eq(" + p + ")");
          r.append(t.html()), r.data(t.data()), h.attr("aria-controls", "tab_item-" + p), p++
        });
        var d = 0,
          v;
        n.find(".resp-tab-item").each(function () {
          $tabItem = e(this), $tabItem.attr("aria-controls", "tab_item-" + d), $tabItem.attr("role", "tab");
          var t = 0;
          n.find(".resp-tab-content").each(function () {
            v = e(this), v.attr("aria-labelledby", "tab_item-" + t), t++
          }), d++
        });
        var m = e(this).find(".resp-tab-active").length != 0 ? e(this).find(".resp-tab-active").index() : "0";
        if (f != "") {
          var g = f.match(new RegExp(l + "([0-9]+)"));
          g !== null && g.length === 2 && (m = parseInt(g[1], 10) - 1, m > d && (m = 0))
        }
        e(n.find(".resp-tab-item")[m]).addClass("resp-tab-active"), t.closed === !0 || t.closed === "accordion" && !r.is(":visible") || t.closed === "tabs" && !!r.is(":visible") ? e(n.find(".resp-tab-content")[m]).addClass("resp-tab-content-active resp-accordion-closed") : (e(n.find(".resp-accordion")[m]).addClass("resp-tab-active"), e(n.find(".resp-tab-content")[m]).addClass("resp-tab-content-active").attr("style", "display:block")), n.find("[role=tab]").each(function () {
          var t = e(this);
          t.click(function () {
            var tc = e(this);
            if (e(this).hasClass("resp-tab-active")) {
              if (e(this)[0].tagName == "H2") {
                e(this).removeClass("resp-tab-active");
                e(this).siblings(".resp-tab-content-active").hide(200).removeClass("resp-tab-content-active");
              };
              return false;
            };
            var t = e(this),
              r = t.attr("aria-controls");
            if (t.hasClass("resp-accordion") && t.hasClass("resp-tab-active"))
              return n.find(".resp-tab-content-active").hide(200, function () {
                e(this).addClass("resp-accordion-closed")
              }), t.removeClass("resp-tab-active"), !1;
            !t.hasClass("resp-tab-active") && t.hasClass("resp-accordion") ? (n.find(".resp-tab-active").removeClass("resp-tab-active"), n.find(".resp-tab-content-active").hide(200).removeClass("resp-tab-content-active resp-accordion-closed"), n.find("[aria-controls=" + r + "]").addClass("resp-tab-active"), n.find(".resp-tab-content[aria-labelledby = " + r + "]").show(200, function () {
              if (tc.offset().top < e(window).scrollTop()) {

                if(jQuery(".mobile-header-replace").css("display")!=="none" && jQuery(".mobile-header").css("position")=="fixed" ){
                  floatingOffset= jQuery(".mobile-header-replace").height();
                }else{
                  floatingOffset=0
                }
                jQuery('body,html').stop().animate({
                  scrollTop: tc.offset().top - floatingOffset
                  
                }, 200)
              }
            }).addClass("resp-tab-content-active")) : (n.find(".resp-tab-active").removeClass("resp-tab-active"), n.find(".resp-tab-content-active").removeAttr("style").removeClass("resp-tab-content-active").removeClass("resp-accordion-closed"), n.find("[aria-controls=" + r + "]").addClass("resp-tab-active"), n.find(".resp-tab-content[aria-labelledby = " + r + "]").addClass("resp-tab-content-active").fadeIn(400)), t.trigger("tabactivate", t)
          })
        }), e(window).resize(function () {
          n.find(".resp-accordion-closed").removeAttr("style")
        })
      })
    }
  })
})(jQuery);



/*dng */

(function ($) {
  $(document).ready(function () {
    $(".verticalTab_Left,.verticalTab_Right,.horizontalTab_Bottom,.horizontalTab_Top,.dg-tabs-top,.dg-tabs-bottom,.dg-tabs-left,.dg-tabs-right").easyResponsiveTabs({
      type: "vertical",
      width: "auto",
      fit: !0
    })
  });
})(jQuery);
(function ($) {
  $.fn.OpenTab = function () {
    var url = window.location.search,
      e = $(this);
    if (url.indexOf("?") != -1) {
      var str = url.substr(1);
      strs = str.split("&");
      for (i = 0; i < strs.length; i++) {
        if (e.attr("id") == strs[i].split("=")[0]) {
          var info = strs[i].split("=")[1];
          e.find(".resp-tabs-list .resp-tab-item").eq(info - 1).addClass("resp-tab-active").siblings(".resp-tab-item").removeClass("resp-tab-active");
          e.find(".resp-tabs-container .resp-accordion").eq(info - 1).addClass("resp-tab-active").siblings(".resp-accordion").removeClass("resp-tab-active");
          e.find(".resp-tabs-container .resp-tab-content").eq(info - 1).addClass("resp-tab-content-active").show().siblings(".resp-tab-content").removeClass("resp-tab-content-active").hide();
        }
      }
    }
  }
})(jQuery);
(function ($) {
  $(document).ready(function () {
    $(".verticalTab_Left,.verticalTab_Right,.horizontalTab_Bottom,.horizontalTab_Top,.dg-tabs-top,.dg-tabs-bottom,.dg-tabs-left,.dg-tabs-right").each(function () {
      $(this).OpenTab();
    });
    
    $(".verticalTab_Left,.verticalTab_Right,.dg-tabs-left,.dg-tabs-right").each(function () {
      var e = $(this);
      e.find(".resp_margin").css("min-height", e.find(".resp-tabs-list").height())
    });
    
    $(".verticalTab_Left,.verticalTab_Right,.horizontalTab_Bottom,.horizontalTab_Top,.dg-tabs-top,.dg-tabs-bottom,.dg-tabs-left,.dg-tabs-right").each(function () {
      var e = $(this),
        itm = e.find(".resp-tab-item"),
        interval;
      if (e.data("autoplay")) {
        var time = parseInt(e.data("autoplay")) >= 1 ? e.data("autoplay") : 3000;
        var autoplays = function (n) {
          int = e.find(".resp-tabs-list .resp-tab-active").index() + n < itm.length ? e.find(".resp-tabs-list .resp-tab-active").index() + n : 0;
          itm.eq(int).click();
        };
        interval = setTimeout(function () {
          autoplays(1)
        }, time);
        itm.on("click", function () {
          clearTimeout(interval);
          interval = setTimeout(function () {
            autoplays(1)
          }, time);
        })
      }
      if (e.data("navigation")) {
        e.append("<div class='resp-tab-arrow'><a  href='javascript:;' class='last-page'><</a><a  href='javascript:;' class='next-page'>></a></div>");
        e.find(".next-page").click(function () {
          autoplays(1)
        });
        e.find(".last-page").click(function () {
          autoplays(-1)
        })
      }
    });

    $(".horizontalTab_Top,.horizontalTab_Bottom,.verticalTab_Left,.verticalTab_Right,.dg-tabs-top,.dg-tabs-bottom,.dg-tabs-left,.dg-tabs-right").each(function () {
      var e = $(this),
        itm = e.find(".resp-tab-item"),
        interval;
      if (e.data("autoplay")) {
        var time = parseInt(e.data("autoplay")) >= 1 ? e.data("autoplay") : 3000;
        var autoplays = function (n) {
          int = e.find(".resp-tabs-list .resp-tab-active").index() + n < itm.length ? e.find(".resp-tabs-list .resp-tab-active").index() + n : 0;
          itm.eq(int).click();
        };
        interval = setTimeout(function () {
          autoplays(1)
        }, time);
        itm.on("click", function () {
          clearTimeout(interval);
          interval = setTimeout(function () {
            autoplays(1)
          }, time);
        })
      }
      if (e.data("navigation")) {
        e.append("<div class='resp-tab-arrow'><a  href='javascript:;' class='last-page'><</a><a  href='javascript:;' class='next-page'>></a></div>");
        e.find(".next-page").click(function () {
          autoplays(1)
        });
        e.find(".last-page").click(function () {
          autoplays(-1)
        })
      }
    });
  })
})(jQuery);
