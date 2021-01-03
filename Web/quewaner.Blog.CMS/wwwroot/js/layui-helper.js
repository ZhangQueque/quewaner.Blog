






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
            var table = layui.table;
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
    tableToolEvent: function (tableFilter, eventType, eventName, event) {
        layui.use(['table'], function () {
            var table = layui.table;
            //监听事件
            table.on(eventType + '(' + tableFilter + ')', function (obj) {
                var data = null;
                if (eventType == 'toolbar') { //表头工具栏 选中数据
                    var checkStatus = table.checkStatus(obj.config.id);
                    data = checkStatus.data;
                } else {  //单元格工具栏 选中数据
                    data = obj.data;
                }
                if (obj.event == eventName) {
                    (checkFunction(event))(data, obj);
                }
            });
        });
    },


    /**
     * 文件上传-拖拽上传
     * @param {string} elem  要绑定的元素ID ID选择器，需要带#号)
     * @param {string} url   上传文件的接口地址
     * @param {string} img   上传成功后要显示ImgId ID选择器，需要带#号) （此逻辑不一定符合你的要求，可以自己修改）
     */
    uploadFile: function (elem, url, img) {
        /*该接口返回的相应信息（response）必须是一个标准的 JSON 格式，如：
         {
              "code": 0
              ,"msg": ""
              ,"data": {
                "src": "http://cdn.layui.com/123.jpg"
              }
         }
         */
        layui.use('upload', function () {
            var upload = layui.upload;
            //拖拽上传
            upload.render({
                elem: elem
                , url: url //改成您自己的上传接口   
                //选择文件的回调
                , accept: 'images' //指定允许上传时校验的文件类型，可选值有：images（图片）、file（所有文件）、video（视频）、audio（音频）
                , choose: function (obj) {
                    //将每次选择的文件追加到文件队列
                    //var files = obj.pushFile();

                    //预读本地文件，如果是多文件，则会遍历。(不支持ie8/9)
                    //obj.preview(function (index, file, result) {
                    //    console.log(index); //得到文件索引
                    //    console.log(file); //得到文件对象
                    //    console.log(result); //得到文件base64编码，比如图片

                    //    //obj.resetFile(index, file, '123.jpg'); //重命名文件名，layui 2.3.0 开始新增

                    //    //这里还可以做一些 append 文件列表 DOM 的操作

                    //    //obj.upload(index, file); //对上传失败的单个文件重新上传，一般在某个事件中使用
                    //    //delete files[index]; //删除列表中对应的文件，一般在某个事件中使用
                    //});
                }
                , before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
                    layer.load(); //上传loading
                }
                , done: function (res, index, upload) {
                    layer.closeAll('loading'); //关闭loading
                   
                    $(img).attr('src', res.data.src);
                }
                , error: function (index, upload) {
                    layer.closeAll('loading'); //关闭loading
                }

            });



        });
    },


    /**
     * 表单渲染  
     */
    formRender: function () {
        layui.use('form', function () {
            var form = layui.form;

            form.render(); //更新全部  ，为了防止动态插入的数据丢失
        });

    },


    /**
     * 表单事件
     * @param {any} eventFilter 即 class="layui-form" 所在元素属性 lay-filter="" 对应的值
     * @param {any} eventType   select	监听select下拉选择事件
checkbox	监听checkbox复选框勾选事件
switch	监听checkbox复选框开关事件
radio	监听radio单选框事件
submit	监听表单提交事件
     * @param {any} event 获取数据后的事件
     */
    formEvent: function (eventFilter, eventType, event) {
        layui.use('form', function () {
            var form = layui.form;

            form.on(eventType + '(' + eventFilter + ')', function (data) {

                //console.log(data.elem); //被执行事件的元素DOM对象，一般为button对象
                // console.log(data.form); //被执行提交的form对象，一般在存在form标签时才会返回
                //console.log(data.field); //当前容器的全部表单字段，名值对形式：{name: value}

                //你的逻辑
                (checkFunction(event))(data);
                return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
            });
        });
    },


    /**
     * AJAX Post请求
     * @param {any} url     请求地址
     * @param {any} data    请求数据
     * @param {any} successFunction 成功回调
     * @param {any} errorFunction   失败回调
     */
    ajaxPostAsync: function (url, data, successFunction, errorFunction) {
        layer.load(); //loading
        $.ajax({
            url: url, // 发送的路径
            type: "post", // 发送方式
            data: JSON.stringify(data),   // 发送的数据，注意，这里一定要序列化，因为已经声明了。
            contentType: 'application/json',  // 声明发送数据格式
            success: checkFunction(successFunction),
            error: checkFunction(errorFunction)
        })
    },

    /**
     * AJAX Get请求
     * @param {any} url 请求地址
     * @param {any} successFunction   成功回调
     * @param {any} errorFunction     失败回调
     */
    ajaxGetAsync: function (url, successFunction, errorFunction) {
        layer.load(); //loading
        $.ajax({
            url: url, // 发送的路径
            type: "get", // 发送方式
            success: checkFunction(successFunction),
            error: checkFunction(errorFunction)
        })
    },

}

function checkFunction(handler) {
    if (!handler || typeof handler !== "function") {
        handler = function () { };
    }
    return handler;
}