
function actionClick(lessionId) {
    var lesstionStatus = $('#'+lessionId).val();

    if (lesstionStatus == 1) {
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    if (xmlhttp.responseText != "") {
                        $(".modal-body").html(xmlhttp.responseText);
                        $("#alertDialog").modal();
                    }
                }
            }
            xmlhttp.open("GET", "../Ajax.aspx?action=Enable&lessionId=" + lessionId, true);
            xmlhttp.send();
    }

 
    if (lesstionStatus == 2) {
        updateLessionBecomeRead(lessionId)
        /*
        if (lessionId == 1) { //is first lession
            //update become 3
            updateLessionBecomeRead(lessionId)
        } else {
            var pre = lessionId - 1;
            previousLessionStatus = $('#' + pre).val();
            if (previousLessionStatus == 3) {
                //update current lession become 3 and open 
                updateLessionBecomeRead(lessionId)
            } else {
                //is second lession or up... => need check previous lession have not read yet ?
                var xmlhttp;
                if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                    xmlhttp = new XMLHttpRequest();
                }
                else {// code for IE6, IE5
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        if (xmlhttp.responseText != "") {
                            //show message request finish previous lession
                            $(".modal-body").html(xmlhttp.responseText);
                            $("#alertDialog").modal();
                        }
                    }
                }
                xmlhttp.open("GET", "../Ajax.aspx?action=Active&lessionId=" + lessionId, true);
                xmlhttp.send();
            }
        }
        */
    }

    if (lesstionStatus == 3) {
        openLession(lessionId)
    }

}

function updateLessionBecomeRead(lessionId) {
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            if (xmlhttp.responseText == "success") {
                $('#' + lessionId).css("background", "#FF7272");
                $('#' + lessionId).val("3")
                $('#MainContent_appent_count_number').text(lessionId)
                $('#MainContent_appent_count_number_progress').text(parseInt(lessionId) + 1)
                openLession(lessionId)
            }
        }
    }
    xmlhttp.open("GET", "../Ajax.aspx?action=Read&lessionId=" + lessionId, true);
    xmlhttp.send();
}

function openLession(lessionId) {
    $('.zeroDayIframe').attr('src', "../Lesson.ashx?LessonId=" + lessionId + "")
    $('html, body').animate({
        scrollTop: $('#anchorDayContent').offset().top
    }, 2000);
}
