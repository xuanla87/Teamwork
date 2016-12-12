var Cookies = {
    init: function () {
        var allCookies = document.cookie.split('; ');
        for (var i = 0; i < allCookies.length; i++) {
            var cookiePair = allCookies[i].split('=');
            this[cookiePair[0]] = cookiePair[1];
        }
    },
    create: function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        document.cookie = name + "=" + value + expires + "; path=/";
        this[name] = value;
    },
    erase: function (name) {
        this.create(name, '', -1);
        this[name] = undefined;
    },
    delall: function () {
        var allCookies = document.cookie.split('; ');
        for (var i = 0; i < allCookies.length; i++)
            this.erase(allCookies[i].split('=')[0]);
    }
};
Cookies.init();
function SidebarClickEvent(value, id) {
    Cookies.erase(id);
    Cookies.create(id, value, 0);
};