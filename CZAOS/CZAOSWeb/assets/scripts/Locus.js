// Local Storage wrangler... trying to make life easier.
// you give the Locus an id, to make sure its our local storage.


var Locus = function (id) {
    this.data = {};
    this.local = window.localStorage || {};

    // if the local storage has never been accessed by us... lets delete whatever might be in here.
    if (!this.local[id] && id) {
        this.local.clear();
        this.data[id] = Date.now();
    }
}

Locus.prototype.get = function (what) {
    this.data[what] || undefined
    return this;
};

Locus.prototype.set = function (what, value) {
    this.data[what] = value
    return this;
};

Locus.prototype.clear = function () {
    this.data = {};
    this.local.clear();
    return this;
}

Locus.prototype.loadFromLocal = function () {
    if (this.local) {
        for (var i in this.local) {
            if (this.local[i].split('{').length != 1) {
                this.data[i] = JSON.parse(this.local[i]);
            } else {
                if (parseFloat(this.local[i]) == this.local[i]) {
                    this.data[i] = parseFloat(this.local[i]);
                } else {
                    this.data[i] = this.local[i];
                }
            }
        }
    }
}

Locus.prototype.saveToLocal = function () {
    if (this.data) {
        for (var i in this.data) {
            if (typeof this.data[i] == 'object') {
                this.local[i] = JSON.stringify(this.data[i]);
            } else {
                this.local[i] = this.data[i];
            }
        }
    }
}
