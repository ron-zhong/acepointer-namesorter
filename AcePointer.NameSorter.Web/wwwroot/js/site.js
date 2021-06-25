
function countDown() {
    $('#timer').html(--i);

    if (i == 1) {
        $('#loadingBar').hide(1000);
    }

    if (i == 0) {
        location.href = './';
    }
}

function MainLayoutLoaded() {
    $('#blazor-host-ui').hide();
    clearInterval(interval);
}