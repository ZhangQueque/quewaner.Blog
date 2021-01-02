






var layerHelper = {
    /**
     * 关闭loading
     * */
    closeAllLoading: function () {
        layer.closeAll('loading'); //关闭loading
    },

    /**
     * 询问框
     * @param {function} event
     */
    confirm: function (title,content,event) {
        //eg1
        layer.confirm(content, { icon: 3, title: title||'系统提示' }, function (index) {
            //do something
            checkFunction(event);
            layer.close(index);
        });
    }    

}

function checkFunction(handler) {
    if (!handler || typeof handler !== "function") {
        handler = function () { };
    }
    return handler;
}