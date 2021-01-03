






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
        layer.confirm(content, { icon: 3, title: title || '系统提示' }, checkFunction(event));
    },
    /**
     * 提示弹框
     * @param {any} msg 提示语句
     * @param {any} event 提示后执行的事件
     * @param {any} icon  提示图标 1：对勾 2：叉 3：问号 4：锁 5：哭脸  6：笑脸 7：感叹号
     * @param {any} time  弹框显示的时间
     */
    msg: function (msg,event,icon,time) {
        layer.msg(msg, {
            icon: icon|| 1,
            time: time || 2000 //2秒关闭（如果不配置，默认是3秒）
        }, checkFunction(event)); 
    } 

}

function checkFunction(handler) {
    if (!handler || typeof handler !== "function") {
        handler = function () { };
    }
    return handler;
}