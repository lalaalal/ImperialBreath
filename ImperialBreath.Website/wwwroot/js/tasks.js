function toggleTaskDone(index, taskId) {
    if ($('#tasks .custom-control-input')[index].checked) {
        $('#tasks .card')[index].classList.add('checked');
        $('#tasks .card-title')[index].classList.add('checked');
        $('#tasks .card-body')[index].classList.add('collapsable');
    } else {
        $('#tasks .card')[index].classList.remove('checked');
        $('#tasks .card-title')[index].classList.remove('checked');
        $('#tasks .card-body')[index].classList.remove('collapsable');
    }
    $.get("/users/toggle-task?taskId=" + taskId).fail(function () {
        alert("다시 로그인해 주세요");
        window.location.replace("/Login");
    });
}

function changeShow() {
    var select = $('#select')[0];
    $('#tasks .container').show();
    $('#nothing-here').hide();
    if (select.value == "all") {
        $('#tasks .card').show();
    } else if (select.value == "done") {
        $('#tasks .card').hide();
        $('#tasks .card.checked').show();
    } else if (select.value == "undone") {
        $('#tasks .card').show();
        $('#tasks .card.checked').hide();
    }
    hideTaskContainer();
}

function isTaskContainerEmpty(index) {
    var children = $('#tasks .container')[index].children;
    for (var i = 1; i < children.length; i++) {
        if (children[i].style.display != "none")
            return false;
    }
    return true;
}

function hideTaskContainer() {
    var emptyContainerCount = 0;
    for (var i = 0; i < $('#tasks .container').length; i++) {
        if (isTaskContainerEmpty(i)) {
            $('#tasks .container')[i].style.display = "none";
            emptyContainerCount++;
        }
    }
    if ($('#tasks .container').length == emptyContainerCount)
        $('#nothing-here').show();
}

for (var index = 0; index < $('#tasks .custom-control-input').length; index++) {
    if ($('#tasks .custom-control-input')[index].checked) {
        $('#tasks .card')[index].classList.add('checked');
        $('#tasks .card-title')[index].classList.add('checked');
        $('#tasks .card-body')[index].classList.add('collapsable');
    }
}

$('#nothing-here').hide();
changeShow();

var today = new Date();
var year = today.getFullYear();
var month = today.getMonth() + 1;
var date = today.getDate();

var url = "https://schoolmenukr.ml/api/high/J100000772?year=" + year + "&month=" + month + "&date=" + date;
$.getJSON(url, function (data) {
    var menu = data["menu"][0]
    var lunch = menu["lunch"];
    var dinner = menu["dinner"];

    $("#lunch-menu > .card-body")[0].innerHTML = "";
    $("#dinner-menu > .card-body")[0].innerHTML = "";
    for (var i = 0; i < lunch.length; i++)
        $("#lunch-menu > .card-body")[0].innerHTML += lunch[i] + "<br />";
    if (lunch.length == 0)
        $("#lunch-menu > .card-body")[0].innerHTML = "없어요...";

    for (var i = 0; i < dinner.length; i++)
        $("#dinner-menu > .card-body")[0].innerHTML += dinner[i];
    if (dinner.length == 0)
        $("#dinner-menu > .card-body")[0].innerHTML = "없어요...";
})