






var layerHelper = {

    CloseAllLoading: function () {
        layer.closeAll('loading'); //关闭loading
    }
         

}

function checkFunction(handler) {
    if (!handler || typeof handler !== "function") {
        handler = function () { };
    }
    return handler;
}