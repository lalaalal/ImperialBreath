function toggleTaskDone(index, taskId) {
    if ($('#tasks .custom-control-input')[index].checked) {
        $('#tasks .card')[index].classList.add('checked');
        $('#tasks .card-title')[index].classList.add('checked');
        $('#tasks .card-body')[index].classList.add('collapsable');
        $.get("/users/toggle-task?taskId=" + taskId);
    } else {
        $('#tasks .card')[index].classList.remove('checked');
        $('#tasks .card-title')[index].classList.remove('checked');
        $('#tasks .card-body')[index].classList.remove('collapsable');
        $.get("/users/toggle-task?taskId=" + taskId);
    }
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