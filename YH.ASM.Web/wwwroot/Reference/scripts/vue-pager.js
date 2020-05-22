!function (a) {
    "wui" in window || (window.wui = {});
}(),
    function (a, b) {
        if (!b) {
            console.log("required jQuery reference");
            return;
        }
        a.pagination = function (opt) {
            var vcp = new VueChangePage(opt);
            return vcp;
        };
    }(wui, jQuery);


function VueChangePage(options) {
    var self = this;
    self.setting = options;
    this.show = function () {
        _loadData();
        return self;
    },
    this.reload = function (cachecontrol) {
            _loadData(undefined, cachecontrol);
            return self;
        };
    function _pageLoading() {
        this.show = function () {
            $(self.setting.el).mask();
        };
        this.close = function () {
            $(self.setting.el).mask("close");
        };
    }
    function _complete(data) {
        if (data && self.setting.onComplete && typeof self.setting.onComplete == 'function') {
            self.setting.onComplete.call(data);
        }
    }
    function _loadData(arg, cachecontrol) {
        var loading = new _pageLoading();
        if (!self.setting.loading) {
            loading = {
                show: function () { },
                close: function () { }
            };
        }
        loading.show();
        var cusData = {};
        if(typeof self.setting.query =='function'){
            cusData = self.setting.query();
        }else{
            cusData = self.setting.query;
        }
        var ajaxdata = cusData;
        if (arg) {
            ajaxdata = $.extend(arg, cusData);
            ajaxdata.pageindex = arg.pageindex || ajaxdata.pageindex || 1;
            ajaxdata.pagesize = ajaxdata.pagesize;
        } else {
            ajaxdata = $.extend({ "pagesize": self.setting.pagesize, "pageindex": 1 }, cusData);
        }
        ajaxdata.pagesize = ajaxdata.pagesize || 10;
        $.ajax({
            url: self.setting.url,
            type: "POST",
            data: ajaxdata,
            beforeSend: function (xhr) {
                if (cachecontrol) {
                    xhr.setRequestHeader("Cache-Control", "no-cache");
                }
            },
            success: function (json) {
                loading.close();
                if (json.Success) {
                    _refreshUI(json.Content);
                } else {
                    _showTips(json.Message);
                }
            },
            error: function () {
                loading.close();
                _showTips("连接服务器失败");
            }
        });
    }
    function _showTips(message) {
        var action = self.setting.showTips || window.alert;
        action(message);
    }
    function _refreshUI(content) {
        if (content.Data) {
            self.Model.Data = [];
            self.Model.Data = content.Data;
        }
        __render(content.PageIndex, content.Count, content.PageCount);
        _complete(content);
    }
    function __render(currentIndex, totalRecords, totalPageCount) {
        if (self.setting.enablePaginate === false) {
            return;
        }
        if (totalPageCount <= 0) {
            return;
        }
        var currentPageIndex = currentIndex;
        var pagesize = self.setting.pagesize || 10;
        var ul = $("<ul></ul>").addClass("pagination");
        var prev = $("<li class='page-item'></li>").append($("<a class='page-link'></a>").append($("<i class='icon-double-angle-left fa fa-angle-double-left'></i>"))).addClass("prev");
        if (currentPageIndex == 1) {
            prev.addClass("disabled");
        }

        var next = $("<li class='page-item'></li>").append($("<a class='page-link'></a>").append($("<i class='icon-double-angle-right fa fa-angle-double-right'></i>"))).addClass("next");
        if (totalPageCount <= 1 || currentPageIndex == totalPageCount) {
            next.addClass("disabled");
        }
        var recordsinfo = $("<li class='page-item disabled'></li>").append($("<a class='page-link'></a>")
            .append("<span>共</span><span class='totalrecords'>" + totalRecords + "</span><span>条，每页</span>" +
                "<span class='pagesize'>" + pagesize + "</span>条，<span></span>" + "<span class='maxpageindex'>" + totalPageCount + "</span><span>页</span>"));

        var minIndex = currentPageIndex - 5;
        var plus = 0;
        if (minIndex <= 0) {
            plus = 0 - minIndex;
            minIndex = 1;
        }
        var maxIndex = currentPageIndex + 5 + plus;
        if (maxIndex > totalPageCount) {
            maxIndex = totalPageCount;
        }
        ul.append(prev);
        for (var i = minIndex; i <= maxIndex; i++) {
            var li_html = $("<li class='page-item'></li>");
            if (currentPageIndex == i)
                li_html.addClass("active disabled");
            var item = li_html.append($("<a class='page-link'></a>").html(i));
            ul.append(item);
        }
        ul.append(next);
        ul.append(recordsinfo);
        //ul.append(pagesizeoptions);
        ul.children("li").click(function () {
            if ($(this).is(".disabled")) {
                return;
            }
            var index = $(this).text();
            if (index.length == 0) {
                index = ul.find(".active").text();
                if ($(this).is(".prev") && index > 1) {
                    index--;
                } else if ($(this).is(".next") && index < totalPageCount) {
                    index++;
                }
            }
            var arg = { "pageindex": parseInt(index), "pagesize": pagesize };
            _loadData(arg);
        });
        if ($(self.setting.el).parent().find(".dataTables_paginate").length > 0) {
            $(self.setting.el).parent().find(".dataTables_paginate").empty().append(ul);
        } else {
            var div_pagination = $("<div class=\"dataTables_paginate paging_bootstrap pull-right\" style=\"margin-right:20px\"></div>");
            div_pagination.append(ul);
            $(self.setting.el).after(div_pagination);//.empty().append(ul);
        }
    }
    function __buildModelObject() {
        var vue = new Vue({
            el: self.setting.el,
            data: {
                Data: []
            },
            methods: self.setting.methods || {}
        });
        return vue;
    }
    self.Model = __buildModelObject();
    this.show();
    return self;
}