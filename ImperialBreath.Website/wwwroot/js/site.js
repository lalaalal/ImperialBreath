// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// Home

function toggleTaskDone(index, taskId) {
    if ($('#tasks .custom-control-input')[index].checked) {
        $('#tasks .card-title')[index].classList.add('checked');
        $('#tasks .card-body')[index].classList.add('collapsable');
        $.get("/users/toggle-task?taskId=" + taskId);
    } else {
        $('#tasks .card-title')[index].classList.remove('checked');
        $('#tasks .card-body')[index].classList.remove('collapsable');
        $.get("/users/toggle-task?taskId=" + taskId);
    }
}

for (var index = 0; index < $('#tasks .custom-control-input').length; index++) {
    if ($('#tasks .custom-control-input')[index].checked) {
        $('#tasks .card-title')[index].classList.add('checked');
        $('#tasks .card-body')[index].classList.add('collapsable');
    }
}