(function ($) {
    $.ClientZone_PostService = { version: '1.0.0' };

    $.fn.ClientZone_PostService = function (s) {
        s = jQuery.extend({
            ModulePath: '',
            ModuleId: 0,
            TabId: 0,
            PortalId: 0,
            PageIndex: 0,
            FirstScreen: 0,
            LoadDisplay: 0,
            StartTime: null,
            EndTime: null,
            Thumbnail_Width: 200,
            Thumbnail_Height: 200,
            Thumbnail_Mode: 'W',
            CategoryID: 0,
            Author:0,
            SearchTag: '',
            Sountry: '',
            State: '',
            City: '',
            AjaxType: 'AjaClientZones',
            callback: function (Items, Pages, isEnd) { },
            error: function () { }
        }, s);

        var cov = function (v1, v2) {
            return v2 ? v2 : v1;
        };
        return this.each(function () {
            var $this = $(this);
            var pageindex, pagesize, moduleid, tabid, portalid, isEnd;
            var thumbnail_width, thumbnail_height, thumbnail_mode;
            var plussize, categoryid, searchtag, modulepath;
            var country, state, city,text;

            pageindex = cov(s.PageIndex, $this.data('pageindex'));
            firstscreen = cov(s.FirstScreen, $this.data('firstscreen'));
            loaddisplay = cov(s.LoadDisplay, $this.data('loaddisplay'));
            categoryid = cov(s.CategoryID, $this.data('categoryid'));
            searchtag = cov(s.SearchTag, $this.data('searchtag'));
            author = cov(s.Author, $this.data('author'));
            moduleid = cov(s.ModuleId, $this.data('moduleid'));
            tabid = cov(s.TabId, $this.data('tabid'));
            portalid = cov(s.PortalId, $this.data('portalid'));
            modulepath = cov(s.ModulePath, $this.data('modulepath'));
            thumbnail_width = cov(s.Thumbnail_Width, $this.data('thumbnail_width'));
            thumbnail_height = cov(s.Thumbnail_Height, $this.data('thumbnail_height'));
            thumbnail_mode = cov(s.Thumbnail_Mode, $this.data('thumbnail_mode'));
            starttime = cov(s.StartTime, $this.data('starttime'));
            endtime = cov(s.EndTime, $this.data('endtime'));
            country = cov(s.Country, $this.data('country'));
            state = cov(s.State, $this.data('state'));
            city = cov(s.City, $this.data('city'));
            text = cov(s.Text, $this.data('text'));
            isEnd = cov(false, $this.data('isend'));
            jQuery.getJSON(modulepath + "Resource_Service.aspx?Token=" + s.AjaxType + "&ModuleId=" + moduleid + "&TabId=" + tabid + "&PortalId=" + portalid,
            {
                Author: author,
                CategoryID: categoryid,
                SearchTag: searchtag,
                PageIndex: pageindex + 1,
                LoadDisplay: loaddisplay,
                FirstScreen: firstscreen,
                Thumbnail_Width: thumbnail_width,
                Thumbnail_Height: thumbnail_height,
                Thumbnail_Mode: thumbnail_mode,
               
                State: state,
                City: city,
                Country: country,
                Text: text,
                StartTime: starttime,
                EndTime: endtime
            },
            function (data) {
                var Return_Items = new Array();
                var Pages = 0;
                if (!isEnd) {
                    jQuery.each(data, function (i, item) {
                        Pages = item.Pages;
                        Return_Items.push(item);
                    });
                }
                if (Pages > pageindex) {
                    $this.data("pageindex", $this.data("pageindex") + 1);
                }
                if (Pages <= (pageindex + 1) && starttime == null) {
                    isEnd = true;
                    $this.data('isend', true);
                }
                s.callback(Return_Items, Pages, isEnd);
            });
        });
    }

})(jQuery);
