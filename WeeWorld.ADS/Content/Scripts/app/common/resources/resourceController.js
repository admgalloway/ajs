function Resource(name) {
    this.name = name;
}

Human.prototype.sayHi = function () {
    console.log("Hello, I'm " + this.name);
};