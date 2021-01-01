var layuiHelper = {

    /**
    * 菜单初始化
    * @param tableId   {string}   表格ID(ID选择器，需要带#号)
    * @param toolBarId {string}   工具栏ID(ID选择器，需要带#号)
    * @param url       {string}   数据接口
    * @param cols      {object[] }   表头
    * @param page      {number }   是否启动分页，默认true
    * @param limit     {number  }   每页显示数量，默认10
    * @param skin      {string}   皮肤，默认line
    */
    tableRender: function (tableId, toolBarId, url, cols, page = true, limit = 10, skin = "line") {
        layui.use(['table'], function () {
            var  table = layui.table;
            table.render({
                elem: tableId,
                url: url,
                toolbar: toolBarId ?? false,
                defaultToolbar: ['filter', 'exports', 'print', {
                    title: '提示',
                    layEvent: 'LAYTABLE_TIPS',
                    icon: 'layui-icon-tips'
                }],
                cols: [cols],
                limits: [10, 15, 20, 25, 50, 100],
                limit: limit,
                page: page,
                //skin: skin
            });
        });
    },
    /**
     * 工具栏事件
     * @param {string} tableFilter 表格lay-filter 名称
     * @param {string} eventType   表头工具栏事件=toolbar/单元格工具栏事件=tool
     * @param {string} eventName   按钮lay-event 名称
     * @param {function} event     点击事件
     */
    tableClickEvent: function (tableFilter,eventType,eventName, event ) {
        layui.use(['table'], function () {
            var table = layui.table;
            //监听事件
            table.on(eventType + '(' + tableFilter + ')', function (obj) {              
                var data = null;
                if (eventType == 'toolbar') { //表头工具栏 选中数据
                    var checkStatus = table.checkStatus(obj.config.id);
                    data= checkStatus.data;
                } else {  //单元格工具栏 选中数据
                    data = obj.data;
                }
                if (obj.event == eventName) {
                    (checkFunction(event))(data);
                }
            });
        });       
    }

}

function checkFunction(handler) {
    if (!handler || typeof handler !== "function") {
        handler = function () { };
    }
    return handler;
}