function EditWorks() {
    if (CheckForm())
        __doPostBack('lnbSave', '');
}

function EditGoWorks() {
    alert("1");
}

function CheckForm() {
    if (document.getElementById("txtTitle").value == "") {
        alert("제목을 입력하세요");
        document.getElementById("txtTitle").focus();
        return false;
    }
    if (document.getElementById("DL_Manager").value == "") {
        alert("담당자를 입력하세요");
        document.getElementById("DL_Manager").focus();
        return false;
    }
    if (document.getElementById("DL_Version").value == "") {
        alert("목표버전을 입력하세요");
        document.getElementById("DL_Version").focus();
        return false;
    }

    var contents = {
        biz_content: oEditors_price.getById["ir_MakeWork"].getIR()
    };
    oEditors_price.getById["ir_MakeWork"].exec("UPDATE_CONTENTS_FIELD", []);



    document.getElementById("biz_MakeWork").value = contents.biz_content;
  
    return true;
}