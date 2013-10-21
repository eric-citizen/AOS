var bar = document.getElementById('progress'),
    time = 0, max = 5,
    int = setInterval(function () {
        bar.style.width = Math.floor(100 * time++ / max) + '%';
        time - 1 == max && clearInterval(int);
    }, 1000);

function countdown(callback) {
    var bar = document.getElementById('progress'),
    time = 0, max = 5,
    int = setInterval(function () {
        bar.style.width = Math.floor(100 * time++ / max) + '%';
        if (time - 1 == max) {
            clearInterval(int);
            // 600ms - width animation time
            callback && setTimeout(callback, 600);
        }
    }, 1000);
}

var count = 0;
var numberOfIntervals;
var intervalLength;

countdown(checkIfDone);

function checkIfDone() {
    if (count < numberOfIntervals) {
        countdown(checkIfDone);
    } else {
        alert('done');
    }
}