var Timer = {};
Timer.paused = true;

Timer.startTime = function (time, saveFunction) {
    var count = 0;
    var interval = setInterval(function () {
        if (!Timer.paused) {
            updateTimeBar(count, time);
            displayTime(count);
            if (count == time) {
                saveFunction();
                clearInterval(interval);
                Timer.startTime(time, saveFunction);
            }
            count++;
        }
    }, 1000);
};

Timer.pause = function() {
    if (Timer.paused) {
        this.src = "/assets/images/icons/observation/pause.png";
    } else {
        this.src = "/assets/images/icons/observation/play.png";
    }

    Timer.paused = !Timer.paused;
};

var displayTime = function(count) {
    var time = timerInterval - count;
    var minutes = Math.floor(time / 60);
    var seconds = time % 60;
    $('.time').html(minutes + ':' + ((seconds < 10) ? "0" : "") + seconds);
};

var updateTimeBar = function(count, time) {
    var timeLeft = time - count;

    $(".timebar").css('width', ((count / time) * 100) + '%');
    if (10 < timeLeft && timeLeft < 20) {
        $('.timebar').css('background', '#dba022');
    } else if (timeLeft < 10) {
        $('.timebar').css('background', '#c93d22');
    } else if (timeLeft == time) {
        $('.timebar').css('background', '#9e9e26');
    }
};