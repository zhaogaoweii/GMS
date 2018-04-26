/*
* 功能：结果与操作提示 [type] 1 info,2 wraning,3 error,4 success,5 confirm
* 日期：2013-5-6
*/
; (function ($) {
    $.fn.showTip = function (type, title, msg, fun, top) {
        var closeBG = true;
        if ($("#documentBG").html() == null) { ShowDocumentBG(); }
        var info = "";
        info += " <div class=\"div_showTip\">";
        info += "      <div class=\"div_showTip_01\">";
        info += "          <div class=\"div_showTip_title\">" + title + "</div>";
        info += "          <div class=\"div_showTip_close\" title=\"关闭\" id='div_close_tip'></div>";
        info += "      </div>";
        if (type == "1" || type == "info") {
            info += "      <div class=\"div_showTip_info\">" + msg + "</div>";
        }
        else if (type == "2" || type == "wraning") {
            info += "      <div class=\"div_showTip_wraning\">" + msg + "</div>";
        }
        else if (type == "3" || type == "error") {
            info += "      <div class=\"div_showTip_error\">" + msg + "</div>";
        }
        else if (type == "4" || type == "success") {
            info += "      <div class=\"div_showTip_success\">" + msg + "</div>";
        }
        else if (type == "5" || type == "confirm") {
            info += "      <div class=\"div_showTip_confirm\">" + msg + "</div>";
        }
        info += "      <div class=\"div_showTip_bottom\">";
        info += "          <input type=\"button\" class=\"btn_ok\" title=\"确定\" id='btn_Tip_OK' />";
        if (type == "5" || type == "confirm") {
            info += "          <input type=\"button\" class=\"bnt_cancel\" title=\"取消\" id='btn_close_tip' />";
        }
        info += "      </div>";
        info += "  </div>";
        $("body").append(info);
        $(".div_showTip_close").hover(function () { $(this).attr("class", "div_showTip_close_hover"); }, function () { $(this).attr("class", "div_showTip_close"); });
        if (top == null || top == "" || top == 0 || top == undefined || top == true) {
            top = 170 + $(window).scrollTop();
        }
        $(".div_showTip").animate({ left: (document.body.clientWidth - $(".div_showTip").width()) / 2, top: top }, { duration: 0, queue: false });
        $("#btn_Tip_OK").click(function () {
            $($("body").find(".div_showTip")).remove();
            if (closeBG) {
                CloseDocumentBG();
            }
            if (fun != undefined && fun != null && fun != "") {
                fun();
            }
        });
        $("#div_close_tip,#btn_close_tip").click(function () {
            $($("body").find(".div_showTip")).remove();
            if (closeBG) {
                CloseDocumentBG();
            }
        });
    }
}(jQuery));
/*
* 功能:遮住系统界面
* 日期:2012-7-31
*/
function ShowDocumentBG() {
    if ($("#documentBG").html() != "") {
        var showHeight = document.body.clientHeight;
        if (showHeight < document.documentElement.clientHeight) {
            showHeight = document.documentElement.clientHeight + 20;
        }
        var info = "<div id='documentBG' style='background:#000;position:absolute;opacity:0.3;filter:alpha(opacity=30);z-index:280;width:" + (parseInt(document.body.clientWidth)) + "px;height:" + (parseInt(showHeight) - 20) + "px;left:0px;top:0px;'></div>";
        $("body").append(info);
    }
    else {
        $("#documentBG").show();
    }
}

/*
* 功能:清除遮住系统界面DIV
* 日期:2013-5-6
*/
function CloseDocumentBG() {
    $($("body").find("#documentBG")).remove();
}