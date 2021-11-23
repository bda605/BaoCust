var _me = {

    init: function () {        
        //datatable config
        var config = {
            columns: [
                { data: 'Name' },
                { data: 'StartTime' },
                { data: 'EndTime' },
                { data: 'IsBatch' },
                { data: 'IsMove' },
                { data: 'GiftType' },
                { data: 'GiftName' },
                { data: 'LevelCount' },
                { data: '_Fun' },
            ],
            columnDefs: [
				{ targets: [1], render: function (data, type, full, meta) {
                    return _date.mmToUiDt2(data);
                }},
				{ targets: [2], render: function (data, type, full, meta) {
                    return _date.mmToUiDt2(data);
                }},
				{ targets: [3], render: function (data, type, full, meta) {
                    return (data == '1') ? '是' : '';
                }},
				{ targets: [4], render: function (data, type, full, meta) {
                    return (data == '1') ? '是' : '';
                }},
				{ targets: [5], render: function (data, type, full, meta) {
                    return (data == 'G') ? '獎品' :
                        (data == 'M') ? '獎金' :
                        '' ;
                }},
				{ targets: [8], render: function (data, type, full, meta) {
                    return _crud.dtCrudFun(full.Id, full.Name, true, true, true);
                }},
            ],
        };

        //initial
        _me.edit0 = new EditOne();
        _me.mStage = new EditMany('Id', 'eformStage', 'tplStage', 'tr', 'Sort');
        _crud.init(config, [_me.edit0, _me.mStage]);

        //custom function
        _me.edit0.fnAfterOpenEdit = _me.edit0_afterOpenEdit;
    },

    //set stage answer word
    edit0_afterOpenEdit: function (fun, json) {
        $('#tbodyStage tr').each(function () {
            _me.setAnswer2($(this));
        });
    },

    //tr: object
    setAnswer2: function (tr) {
        _obj.getN('Answer2', tr).text(_itext.get('Answer', tr) == '' ? '' : '(已設定)');
    },

    //TODO: add your code
    //onclick viewFile, called by XiFile component
    onViewFile: function (table, fid, elm) {
        _me.mStage.onViewFile(table, fid, elm);
    },

    //on set answer of stage
    onAnswer: function (me) {
        var tr = $(me).closest('tr');
        _tool.showArea(title, _itext.get(fid, tr), _crud.isEditMode(), function (result) {
            _itext.set(fid, result, tr);
        });
    },

}; //class