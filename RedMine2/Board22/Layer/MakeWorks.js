function MakeWorks() {
    if (CheckForm())
        __doPostBack('lnbSave', '');
}

function MakeGoWorks() {
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
    //내용 입력
    //for (var name in contents) {
    //    if (contents[name].toLowerCase().indexOf("<script") > 0) {
    //        alert("에디터 본문 중 보안에 위협적인 내용이 있습니다.\nhtml모드로 확인하기 바라며, 궁금한 사항은 관리자에게 문의하세요.");
    //        return false;
    //    }
    //    else
    //        document.getElementById(name).value = contents[name];
    //}

    return true;
}