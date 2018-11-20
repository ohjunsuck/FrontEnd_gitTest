nhn.husky.SE2M_MovieLink = jindo.$Class({
    name: "SE2M_MovieLink",
    acceptbutton: null,
    closebutton: null,
    $init: function (elAppContainer) {
        this._assignHTMLObjects(elAppContainer);
    },
    _assignHTMLObjects: function (elAppContainer) {
        this.oDropdownLayer = cssquery.getSingle("DIV.husky_seditor_movielinker_layer", elAppContainer);
        this.acceptbutton = cssquery.getSingle(".se2_apply", this.oDropdownLayer);
        this.closebutton = cssquery.getSingle(".se2_cancel", this.oDropdownLayer);
        this.Input = cssquery.getSingle("TEXTAREA", this.oDropdownLayer);
    },
    $ON_MSG_APP_READY: function () {
        this.oApp.exec("REGISTER_UI_EVENT", ["movielinker", "click", "SE2_TOGGLE_MOVIELINK_LAYER"]);
        this.oApp.registerBrowserEvent(this.acceptbutton, "click", "MSG_MOVIELINK_ACCEPT");
        this.oApp.registerBrowserEvent(this.closebutton, "click", "MSG_MOVIELINK_CLOSE");
    },
    $ON_SE2_TOGGLE_MOVIELINK_LAYER: function () {
        this.oApp.exec("TOGGLE_TOOLBAR_ACTIVE_LAYER", [this.oDropdownLayer]);
    },
    $ON_MSG_MOVIELINK_ACCEPT: function () {
        this.oApp.exec("PASTE_HTML", [this.Input.value]);
        this.oApp.exec("SE2_TOGGLE_MOVIELINK_LAYER", []);
    },
    $ON_MSG_MOVIELINK_CLOSE: function () {
        this.oApp.exec("SE2_TOGGLE_MOVIELINK_LAYER", []);
    }
});