//h+��չjs
var App = function () {
    var isFullScreen = false;
    var requestFullScreen = function () {//ȫ��
        var de = document.documentElement;
        if (de.requestFullscreen) {
            de.requestFullscreen();
        } else if (de.mozRequestFullScreen) {
            de.mozRequestFullScreen();
        } else if (de.webkitRequestFullScreen) {
            de.webkitRequestFullScreen();
        }
        else {
            alert("���������֧��ȫ��");
        }
    };

    //var requestFullScreen2 = function (element) {
    //    // �жϸ�����������ҵ���ȷ�ķ���
    //    var requestMethod = element.requestFullScreen || //W3C
    //        element.webkitRequestFullScreen ||    //Chrome��
    //        element.mozRequestFullScreen || //FireFox
    //        element.msRequestFullScreen; //IE11
    //    if (requestMethod) {
    //        requestMethod.call(element);
    //    }
    //    else if (typeof window.ActiveXObject !== "undefined") {//for Internet Explorer
    //        var wscript = new ActiveXObject("WScript.Shell");
    //        if (wscript !== null) {
    //            wscript.SendKeys("{F11}");
    //        }
    //    }
    //};

    //�˳�ȫ�� �ж����������
    var exitFull = function () {
        // �жϸ�����������ҵ���ȷ�ķ���
        var exitMethod = document.exitFullscreen || //W3C
            document.mozCancelFullScreen ||    //Chrome��
            document.webkitExitFullscreen || //FireFox
            document.webkitExitFullscreen; //IE11
        if (exitMethod) {
            exitMethod.call(document);
        }
        else if (typeof window.ActiveXObject !== "undefined") {//for Internet Explorer
            var wscript = new ActiveXObject("WScript.Shell");
            if (wscript !== null) {
                wscript.SendKeys("{F11}");
            }
        }
    };

    return {
        handleFullScreen: function () {
            if (isFullScreen) {
                exitFull();
                isFullScreen = false;
            } else {
                requestFullScreen();
                isFullScreen = true;
            }
        },
    };

}();

