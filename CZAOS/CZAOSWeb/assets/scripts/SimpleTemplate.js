﻿var SimpleTemplate = {
    ver: '1.2',
    template: {}
};

SimpleTemplate._fill = function (who, where, props) {
    var temp = SimpleTemplate.template[who] || who;
    var identifier;
    for (var i in where) {
        if (typeof (where[i]) == 'object') {
            for (var j in where[i]) {
                identifier = new RegExp("\\%" + i + "." + j + "\\%", "g");
                temp = temp.replace(identifier, where[i][j]);
            }
        } else {
            var sim = where[i];
            if (typeof (sim) == 'boolean' || typeof (sim) == 'number') sim.toString(); // convert booleans & numbers to strings for keying
            var simCheck = sim;
            identifier = new RegExp("\\%" + i + "\\%", "g");
            if (simCheck == 'Yes' || simCheck == 'true' || simCheck == 'false' || simCheck == 'No') {
                if (props && props[i]) {
                    temp = temp.replace(identifier, props[i][simCheck]);
                }
            } else {
                temp = temp.replace(identifier, where[i]);
            }
        }
    }
    return temp;
}

SimpleTemplate.fill = function (who, where, props) {
    var temp = [];
    console.log(typeof (where))
    if (where.length && typeof (where) != 'string') {
        for (var i = 0; i != where.length; i++) {
            temp.push(SimpleTemplate._fill(who, where[i], props));
        }
    } else {
        temp.push(SimpleTemplate._fill(who, where, props));
    }
    return (temp.join(''));
}

SimpleTemplate.loadTemplate = function (who) {
    console.log(who);
    if (document.getElementById(who)) {
        SimpleTemplate.template[who] = document.getElementById(who).innerHTML;
    } else {
        console.log('failed to load template [' + who + ']')
    }
}

SimpleTemplate.loadTemplates = function () {
    var temp = document.getElementsByTagName('script');
    console.log(temp)
    for (var i = 0; i != temp.length; i++) {
        console.log(temp[i].type)
        if (temp[i].type == 'simple/template') {
            var who = temp[i].id;
            SimpleTemplate.template[who] = temp[i].innerHTML;
        }
    }
}




