; (function ($) {

    $.fn.extend({
        xhrUpload: function (options) {
            var $all = $(this);

            $all.each(function (index, item) {
                var $ths = $(item);
                //console.log($ths,item);
                new init($ths);
            });
            function init($ths) {
                var self = this;
                self.unique = 'xhr-' + (new Date()).valueOf();
                self.selector = "#" + self.unique;
                $ths.hide();
                var chosing = options.chosing || function () { return true; }
                var template = "<div class='xhr-upload pull-left' id='" + self.unique + "' style='display: table; height: 35px; width: 100px;margin-left:20px;margin-top:12px;'>"
                        + "<div class='xhr-upload-progress' style='border: 1px solid #ccc; height: 100%; display: table-cell; vertical-align: middle;'>"
                            + "<div class='xhr-upload-procceed' style='background-color: #ffaa00; width: 0%; height: 100%; vertical-align: middle; text-align: right; color: #fff;display:block;'>"
                                + "<div class='upload-progress-number' style='height:33px;text-align:right;width:100%;line-height:33px;'>0%</div>"
                            + "</div>"
                        + "</div>"
                        + "<div class='xhr-upload-button' style='display: table-cell; height: 100%; width: 32px;'>"
                    + "<button type='button' name='btn-xhr-file' class='btn btn-sm btn-xhr-file btn-success' style=' display: block; height: 100%; width: 100%;'>"
                                + "<i class='icon-upload' style='font-size:10px;'> Import</i>"
                            + "</button>"
                        + "</div>"
                    + "</div>";
                var $ele = $(template);
                $ele.find(".btn-xhr-file").click(function () {
                    if (!chosing()) {
                        return;
                    }
                    $ths.click();
                });
                $ths.after($ele);
                var uploadUrl = $ths.attr("data-upload-url");
                var xhr = new xhrUpload();
                xhr.progressEventHandler = function (e) {
                    var percentage = (e.loaded * 100 / e.total).toFixed(2) + "%";
                    //$ths.parent().find(".xhr-upload-procceed").css({ "width": percentage });
                    //$ths.parent().find(".xhr-upload-procceed .upload-progress-number").text(percentage)
                    setProgress(self.selector, percentage);
                };
                $ths.change(function () {
                    setProgress(self.selector, "0%");
                    var file = $ths[0].files[0];
                    var customeData = {};
                    if (typeof options.data == 'function') {
                        customeData = options.data();
                    } else {
                        customeData = options.data;
                    }
                    console.log("customeData", customeData);
                    
                    var data = $.extend(customeData, { "apk": file });

                    xhr.send(uploadUrl, data, function () {
                        if (this.readyState === 4) {
                            //console.log(this);
                            if (this.status == 200) {
                                options.completed(eval("(" + this.response + ")"));
                            } else {
                                setProgress("0%");
                                options.completed(null);
                            }
                        }
                    });
                });
            }
            function setProgress(selector, percentage) {
                $(selector).find(".xhr-upload-procceed").css({ "width": percentage });
                $(selector).find(".xhr-upload-procceed .upload-progress-number").text(percentage)
            }
            function xhrUpload() {
                this.send = function (url, formJsonData, onload) {
                    var form = new FormData();
                    for (var key in formJsonData) {
                        form.append(key, formJsonData[key]);
                    }
                    var xhr = new XMLHttpRequest();
                    if (xhr.onload) {
                        xhr.onload = function () { onload.apply(xhr, []); };
                    } else {
                        xhr.onreadystatechange = function () { onload.apply(xhr, []); };
                    }
                    //xhr.addEventListener("progress", this.progressEventHandler, false);
                    xhr.upload.addEventListener("progress", this.progressEventHandler, false);
                    xhr.open("POST", url);
                    xhr.send(form);
                };
                this.progressEventHandler = function (event) {

                }
            };
            return self;
        }
    });

})(jQuery);

