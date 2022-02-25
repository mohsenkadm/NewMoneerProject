
const baseUrl = "/";

function call_ajax(method, url, object, call_back_func) { 
    mouseevent("progress");
    $('.preloader').fadeIn();
    $.ajax({
        method: method,
        url: baseUrl + url,
        cache: true, async: true,
        data: object, 
        success: (respons) => {
            //   /  debugger;
            mouseevent("default");
            $('.preloader').fadeOut();
            if (respons.success) {
                if (typeof (call_back_func) == 'function') {
                    if (respons.data != undefined)
                        call_back_func(respons.data);
                    else
                        call_back_func();
                }
                else if (typeof (call_back_func) == 'string'
                    && call_back_func == 'return') {
                    return respons.data;
                }
                if (respons.msg != null && respons.msg != undefined)
                    toust.success(respons.msg);
            }
            else {
                if (respons.msg != null && respons.msg != undefined)
                    toust.error(respons.msg);
            }

        },
        error: (e) => {
            mouseevent("default");
            $('.preloader').fadeOut();
            toust.error('حدث خطأ عند الأتصال');
        }
    });
}
function call_Action(url, i) {
    //debugger;  
    mouseevent("progress");
    $.ajax({
        method: 'GET',
        url: baseUrl + url,
        cache: true, async: true, 
        success: (respons) => {
            $('.preloader').fadeIn();
            mouseevent("default");
            var from = respons.indexOf("<!-- CUT FROM HERE -->");
            document.body.innerHTML = respons.substring(from, respons.length - 14);
            window.scrollTo(0, 0);
            var mydiv = document.getElementById("mydiv");
            var scripts = mydiv.getElementsByTagName("script");

            for (var i = 0; i < scripts.length; i++) {

                eval(scripts[i].innerText);
            } 
            $('.preloader').fadeOut();
        }
    });

}
function mouseevent(type) {
    $("body").css("cursor", type);
    //type =progress ,default
}


function fillSection2Data(data) {

    $('#section2table').empty();
    if (data.length === 0) {
        toust.error("لا توجد معلومات");
        return;
    }
    $.each(data, function (i, item) { 
        var rows = " <div class='col-sm-2' style='margin-bottom:2%;'>"+
            "<div class='card'  onclick=\"playsound('" + item.pathMp3 + "')\">" +
            "   <img class='card-img-top' border=3 src='" + item.image + "' alt='Card image cap'>"
            +"<div class='card-body text-center'>" +
            "<h5 class='card-title'>" + item.title + "</h5>" 
                     +" </div>"+
                            "</div>"+
                       " </div >  ";
        $('#section2table').append(rows);
    });
}

function fillSection3Data(data) {

    $('#section3table').empty();
    if (data.length === 0) {
        toust.error("لا توجد معلومات");
        return;
    }
    $.each(data, function (i, item) {
        var rows = " <div class='col-sm-2' style='margin-bottom:2%;'>" +
            "<div class='card' onclick=\"playsound('" + item.pathMp3 + "')\">" +
            "   <img class='card-img-top' border=3 src='" + item.image + "' alt='Card image cap'>"
            + "<div class='card-body text-center' >" +
            "<h5 class='card-title'>" + item.title + "</h5>"
            + " </div>" +
            "</div>" +
            " </div >  ";
        $('#section3table').append(rows);
    });
}

//table 2

function filltableSection2(data) {

    $('#tableSection2').empty();
    if (data.length === 0) {
        toust.error("لا توجد معلومات");
        return;
    }
    $.each(data, function (i, item) {
        var rows = "<tr>" +
            "<td>" + item.pathMp3 + "</td>" +
            "<td><img class='card-img-top' border=3 src='" + item.image + "' alt='Card image cap'></td>" +
            "<td>" + item.title + "</td>"
            + "<td> <button type='button' class='btn btn-danger' onclick='deleteSection2(" + item.section2Id + ")'  >حذف</button>" +
            "  |  <button type='button' class='btn btn-primary' onclick='updateSection2(" + item.section2Id + ")'  data-toggle='modal' data-target='#Section2Modal'>تعديل</button></td></tr>";

        $('#tableSection2').append(rows);
    });
}

function aftersaveSection2(Section2Id) {
    // uplaud image
    var fileUpload = $("#Image").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    data.append(files[0].name, files[0]);
    $.ajax({
        type: "POST", url: "/Home/UploadFile/" + Section2Id.section2Id, contentType: false, processData: false,
        data: data, async: false,
        success: function (message) {
            toust.success("تم  تحميل الصورة بنجاح");
        },
        error: function () {
            toust.error("عذرا حدث خطا اثناء  تحميل الصورة");
        },
    });

    // upluad mp3

    var fileUpload1 = $("#PathMp3").get(0);
    var files1 = fileUpload1.files;
    var data1 = new FormData();
    data1.append(files1[0].name, files1[0]);
    $.ajax({
        type: "POST", url: "/Home/UploadMp3/" + Section2Id.section2Id, contentType: false, processData: false,
        data: data1, async: false,
        success: function (message) {
            toust.success("تم  تحميل ملف صوتي بنجاح");
        },
        error: function () {
            toust.error("عذرا حدث خطا اثناء  تحميل الملف");
        },
    });

    section2Id = 0;
    $("#Title").val('');
    $("#Image").val('');
    $("#PathMp3").val('');
    call_ajax("GET", "Home/GetSection2Data", null, filltableSection2);
}


function deleteSection2(id) {

    var result = confirm("هل تريد الحذف؟!");
    if (result == true) {
        var object = {
            section2Id: id,
        };
        call_ajax("DELETE", "Home/DeleteSection2", object, afterdelSection2);
    }
}
function afterdelSection2() {
    call_ajax("GET", "Home/GetSection2Data", null, filltableSection2);
}
function updateSection2(id) {
    var object = {
        section2Id: id,
    };
    call_ajax("GET", "Home/GetSection2ById", object, SetDataSection2);
}
var section2Id = 0;
function SetDataSection2(data) {
    $("#Title").val(data.title);
    section2Id = data.section2Id;
}


// table 3


function filltableSection3(data) {

    $('#tableSection3').empty();
    if (data.length === 0) {
        toust.error("لا توجد معلومات");
        return;
    }
    $.each(data, function (i, item) {
        var rows = "<tr>" +
            "<td>" + item.pathMp3 + "</td>" +
            "<td><img class='card-img-top' border=3 src='" + item.image + "' alt='Card image cap'></td>" +
            "<td>" + item.title + "</td>"
            + "<td> <button type='button' class='btn btn-danger' onclick='deleteSection3(" + item.section3Id + ")'  >حذف</button>" +
            "  |  <button type='button' class='btn btn-primary' onclick='updateSection3(" + item.section3Id + ")'  data-toggle='modal' data-target='#Section3Modal'>تعديل</button></td></tr>";

        $('#tableSection3').append(rows);
    });
}

function aftersaveSection3(Section3Id) {

    /// uplaud image
    var fileUpload = $("#Image").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    data.append(files[0].name, files[0]);
    $.ajax({
        type: "POST", url: "/Home/UploadFile2/" + Section3Id.section3Id, contentType: false, processData: false,
        data: data, async: false,
        success: function (message) {
            toust.success("تم  تحميل الصورة بنجاح");
        },
        error: function () {
            toust.error("عذرا حدث خطا اثناء  تحميل الصورة");
        },
    });

    // upluad mp3

    var fileUpload1 = $("#PathMp3").get(0);
    var files1 = fileUpload1.files;
    var data1 = new FormData();
    data1.append(files1[0].name, files1[0]);
    $.ajax({
        type: "POST", url: "/Home/UploadMp32/" + Section3Id.section3Id, contentType: false, processData: false,
        data: data1, async: false,
        success: function (message) {
            toust.success("تم  تحميل الملف صوتي بنجاح");
        },
        error: function () {
            toust.error("عذرا حدث خطا اثناء  تحميل الملف");
        },
    });
    section3Id = 0;
    $("#Title").val('');
    $("#Image").val('');
    $("#PathMp3").val('');
    call_ajax("GET", "Home/GetSection3Data", null, filltableSection3);
}


function deleteSection3(id) {

    var result = confirm("هل تريد الحذف؟!");
    if (result == true) {
        var object = {
            section3Id: id,
        };
        call_ajax("DELETE", "Home/DeleteSection3", object, afterdelSection3);
    }
}
function afterdelSection3() {
    call_ajax("GET", "Home/GetSection3Data", null, filltableSection3);
}
function updateSection3(id) {
    var object = {
        section3Id: id,
    };
    call_ajax("GET", "Home/GetSection3ById", object, SetDataSection3);
}
var section3Id = 0;
function SetDataSection3(data) {
    $("#Title").val(data.title);
    section3Id = data.section3Id;
}
