nhn.husky.SE2M_PreviewDocument = jindo.$Class({
    name: "SE2M_PreviewDocument",
    $init: function (elAppContainer) {
        this._assignHTMLObjects(elAppContainer);
    },
    _assignHTMLObjects: function (elAppContainer) {
        this.oPreviewLayer = cssquery.getSingle("DIV.husky_seditor_ui_preview", elAppContainer);
    },
    $ON_MSG_APP_READY: function () {
        this.oApp.exec("REGISTER_UI_EVENT", ["preview", "click", "SE2_TOGGLE_PREVIEW_LAYER"]);
    },
    $ON_SE2_TOGGLE_PREVIEW_LAYER: function () {
        //미리보기 레이어를 띄운다.
        top.preview_open(this.oApp.getIR());   
    }
});