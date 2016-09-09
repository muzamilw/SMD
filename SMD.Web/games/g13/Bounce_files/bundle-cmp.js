window.buildTimestamp = 1423838986;
var requestAnimFrame = window.requestAnimFrame = function() {
    return window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame || function(callback) {
        window.setTimeout(callback, 1e3 / 60)
    }
}();
var cancelAnimFrame = window.cancelAnimFrame = function() {
    return window.cancelAnimationFrame || window.webkitCancelAnimationFrame || window.mozCancelAnimationFrame || window.oCancelAnimationFrame || window.msCancelAnimationFrame || function() {
        window.clearTimeout.apply(window, arguments)
    }
}();
navigator.vibrate = function() {
    return navigator.vibrate || navigator.mozVibrate || navigator.webkitVibrate || navigator.oVibrate || navigator.msVibrate || (navigator.notification ? function(l) {
        navigator.notification.vibrate(l)
    }
     : null ) || new Function
}();
var console = function() {
    return window.console || {
        log: new Function,
        debug: new Function,
        warn: new Function,
        error: new Function,
        clear: new Function
    }
}();
var DOM = {
    get: function(el) {
        r = el == document || el == window || el instanceof HTMLElement ? el : document.getElementById(el);
        if (r == null ) {
            console.log(el)
        }
        return r
    },
    attr: function(el, attr, value) {
        if (value) {
            this.get(el).setAttribute(attr, value)
        } else {
            return this.get(el).getAttribute(attr)
        }
    },
    on: function(el, evt, handler) {
        var split = evt.split(" ");
        for (var i in split) {
            this.get(el).addEventListener(split[i], handler, false)
        }
    },
    un: function(el, evt, handler) {
        var split = evt.split(" ");
        for (var i in split) {
            this.get(el).removeEventListener(split[i], handler, false)
        }
    },
    show: function(el) {
        this.get(el).style.display = "block"
    },
    hide: function(el) {
        this.get(el).style.display = "none"
    },
    offset: function(el) {
        el = this.get(el);
        return {
            x: el.clientLeft + window.scrollLeft,
            y: el.clientTop + window.scrollTop
        };
        var pos = {
            x: 0,
            y: 0
        };
        do {
            pos.x += el.offsetLeft || 0;
            pos.y += el.offsetTop || 0
        } while ((el = el.parentNode) !== null );return pos
    },
    query: function(query) {
        if (!document.querySelectorAll)
            return null ;
        var q = document.querySelectorAll(query);
        return q
    },
    queryOne: function(query) {
        if (!document.querySelector)
            return null ;
        var q = document.querySelector(query);
        return q
    },
    create: function(type) {
        return document.createElement(type)
    },
    positionRelativeTo: function(element, clientX, clientY) {
        var offset = DOM.offset(element);
        return {
            x: clientX - offset.x,
            y: clientY - offset.y
        }
    },
    fitScreen: function(element, ratio) {
        var clientRatio = window.innerWidth / window.innerHeight;
        var width, height;
        if (clientRatio <= ratio) {
            width = window.innerWidth;
            height = width / ratio
        } else {
            height = window.innerHeight;
            width = height * ratio
        }
        element = DOM.get(element);
        element.style.width = width + "px";
        element.style.height = height + "px";
        return {
            width: width,
            height: height
        }
    },
    saveCanvas: function(element) {
        var src = this.get(element);
        var can = this.create("canvas");
        can.width = src.width;
        can.height = src.height;
        var c = can.getContext("2d");
        c.drawImage(src, 0, 0);
        return can
    },
    fadeIn: function(element, duration, callback) {
        element = this.get(element);
        duration = duration || 1e3;
        this.show(element);
        element.style.opacity = 0;
        Util.interpolate(element.style, {
            opacity: 1
        }, duration, callback)
    },
    fadeOut: function(element, duration, callback) {
        element = this.get(element);
        duration = duration || 1e3;
        this.show(element);
        element.style.opacity = 1;
        Util.interpolate(element.style, {
            opacity: 0
        }, duration, function() {
            DOM.hide(element);
            if (callback)
                callback()
        })
    },
    notify: function(htmlMessage, duration, container) {
        container = container ? this.get(container) : document.body;
        this.notification = this.notification || function() {
            var block = DOM.create("div");
            container.appendChild(block);
            DOM.applyStyle(block, {
                zIndex: 999999,
                position: "absolute",
                bottom: "10px",
                width: "100%",
                textAlign: "center"
            });
            var message = DOM.create("span");
            block.appendChild(message);
            DOM.applyStyle(message, {
                backgroundColor: "rgba(0,0,0,0.7)",
                border: "1px solid white",
                borderRadius: "3px",
                margin: "auto",
                color: "white",
                padding: "2px",
                paddingLeft: "10px",
                paddingRight: "10px",
                width: "50%",
                fontSize: "0.7em",
                boxShadow: "0px 0px 2px black"
            });
            return {
                block: block,
                message: message,
                queue: [],
                add: function(message, duration) {
                    this.queue.push({
                        message: message,
                        duration: duration
                    });
                    if (this.queue.length == 1) {
                        this.applyOne()
                    }
                },
                applyOne: function() {
                    var notif = this.queue[0];
                    this.message.innerHTML = notif.message;
                    DOM.fadeIn(this.block, 500);
                    setTimeout(function() {
                        DOM.fadeOut(DOM.notification.block, 500, function() {
                            DOM.notification.queue.shift();
                            if (DOM.notification.queue.length > 0) {
                                DOM.notification.applyOne()
                            }
                        })
                    }, notif.duration + 500)
                }
            }
        }();
        duration = duration || 3e3;
        this.notification.add(htmlMessage, duration)
    },
    applyStyle: function(element, style) {
        element = this.get(element);
        for (var i in style) {
            element.style[i] = style[i]
        }
    },
    populate: function(elements) {
        var res = {};
        for (var i in elements) {
            res[i] = DOM.get(elements[i]);
            if (!res[i])
                console.log("Element #" + elements[i] + " not found")
        }
        return res
    }
};
var Util = {
    preload: function(images, callbackProgress, callbackEnd, callbackError) {
        var loadOne = function() {
            if (remaining.length == 0) {
                end(loaded)
            } else {
                var img = new Image;
                img.onerror = function() {
                    console.log("Couldn't load " + src);
                    error(src)
                }
                ;
                img.onload = function() {
                    if (this.complete) {
                        progress(this, 1 - remaining.length / nbImages);
                        setTimeout(loadOne, document.location.search.indexOf("fakelag") >= 0 ? 1e3 : 1)
                    }
                }
                ;
                var src = remaining.pop();
                img.src = src;
                loaded[src] = img
            }
        }
        ;
        var remaining = images.slice(0);
        var end = callbackEnd || new Function;
        var progress = callbackProgress || new Function;
        var error = callbackError || new Function;
        var nbImages = remaining.length;
        var loaded = {};
        setTimeout(loadOne, 1)
    },
    rand: function(min, max) {
        return Math.random() * (max - min) + min
    },
    randomPick: function() {
        var i = parseInt(Util.rand(0, arguments.length));
        return arguments[i]
    },
    limit: function(n, min, max) {
        if (n < min)
            return min;
        else if (n > max)
            return max;
        else
            return n
    },
    sign: function(n) {
        if (n > 0)
            return 1;
        else if (n == 0)
            return 0;
        else
            return -1
    },
    cookie: {
        set: function(name, value, ttl) {
            if (ttl == undefined)
                ttl = 1e3 * 3600 * 24 * 365;
            document.cookie = name + "=;path=/;expires=Thu, 01-Jan-1970 00:00:01 GMT";
            var expires = new Date;
            expires.setTime(expires.getTime() + ttl);
            document.cookie = [name + "=" + value + "; ", "expires=" + expires.toGMTString() + "; ", "path=/"].join("")
        },
        get: function(name) {
            var cookie = document.cookie.split("; ");
            for (var i in cookie) {
                var spl = cookie[i].split("=");
                if (spl.length == 2 && spl[0] == name) {
                    return spl[1]
                }
            }
            return undefined
        }
    },
    storage: window.localStorage ? {
        getItem: function(item) {
            try {
                return window.localStorage.getItem(item)
            } catch (e) {
                return null 
            }
        },
        setItem: function(item, value) {
            try {
                window.localStorage.setItem(item, value)
            } catch (e) {
                console.log("Local storage issue: " + e)
            }
        }
    } : {
        getItem: function(item) {
            return Util.cookie.get(item)
        },
        setItem: function(item, value) {
            Util.cookie.set(item, value)
        }
    },
    merge: function(template, object) {
        if (!object) {
            return template
        }
        for (var i in template) {
            if (!(i in object)) {
                object[i] = template[i]
            } else {
                if (typeof template[i] == "object" && !(object[i] instanceof Array)) {
                    object[i] = arguments.callee.call(this, template[i], object[i])
                }
            }
        }
        return object
    },
    copyObject: function(obj) {
        var res = {};
        for (var i in obj) {
            res[i] = obj[i]
        }
        return res
    },
    isTouchScreen: function() {
        var bool = "orientation" in window || "orientation" in window.screen || "mozOrientation" in window.screen || "ontouchstart" in window || window.DocumentTouch && document instanceof DocumentTouch || "ontouchstart" in document.documentElement;
        if (bool) {
            bool = bool && Detect.isMobile()
        }
        return bool || window.location.search.indexOf("touch") >= 0
    },
    distance: function(x1, y1, x2, y2) {
        return Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))
    },
    arrayUnique: function(a) {
        for (var i = 0; i < a.length; i++) {
            var j = i + 1;
            while (a[j]) {
                if (a[i] == a[j]) {
                    a.splice(j, 1)
                } else {
                    j++
                }
            }
        }
    },
    analyzeParameters: function() {
        var res = {};
        var tmp;
        var params = window.location.search.substr(1).split("&");
        for (var i = 0; i < params.length; i++) {
            tmp = params[i].split("=");
            res[tmp[0]] = tmp[1]
        }
        return res
    },
    interpolate: function(obj, props, duration, callback) {
        var before = {};
        for (var i in props) {
            before[i] = parseFloat(obj[i])
        }
        var tStart = Date.now();
        (function() {
            var now = Date.now();
            var prct = Math.min(1, (now - tStart) / duration);
            for (var i in props) {
                obj[i] = prct * (props[i] - before[i]) + before[i]
            }
            if (prct < 1) {
                requestAnimFrame(arguments.callee)
            } else {
                if (callback) {
                    callback.call(obj)
                }
            }
        })()
    },
    addZeros: function(n, length) {
        var res = n.toString();
        while (res.length < length)
            res = "0" + res;
        return res
    },
    addCommas: function(n) {
        var s = n.toString()
          , res = ""
          , c = 0;
        for (var i = s.length - 1; i >= 0; i--) {
            res = s.charAt(i) + res;
            c++;
            if (c == 3 && i > 0) {
                c = 0;
                res = "," + res
            }
        }
        return res
    },
    formatDate: function(format, date, options) {
        date = date || new Date;
        options = Util.merge({
            months: ["January", "February", "March", "April", "May", "June", "August", "September", "October", "November", "December"]
        }, options);
        var res = "";
        var formatNext = false;
        for (var i = 0; i < format.length; i++) {
            if (format.charAt(i) == "%") {
                formatNext = true
            } else if (formatNext) {
                formatNext = false;
                switch (format.charAt(i)) {
                case "%":
                    res += "%";
                    break;
                case "M":
                    res += options.months[date.getMonth()];
                    break;
                case "d":
                    res += date.getDate();
                    break;
                case "Y":
                    res += date.getFullYear();
                    break;
                case "m":
                    res += date.getMonth();
                    break
                }
            } else {
                res += format.charAt(i)
            }
        }
        return res
    },
    keyOf: function(object, element) {
        for (var i in object) {
            if (object[i] == element) {
                return i
            }
        }
        return null 
    }
};
var Ajax = {
    send: function(url, method, params, success, fail) {
        var xhr;
        if (window.XMLHttpRequest) {
            xhr = new XMLHttpRequest
        } else if (window.ActiveXObject) {
            try {
                xhr = new ActiveXObject("Msxml2.XMLHTTP")
            } catch (e) {
                xhr = new ActiveXObject("Microsoft.XMLHTTP")
            }
        } else {
            console.log("AJAX not supported by your browser.");
            return false
        }
        success = success || new Function;
        fail = fail || new Function;
        method = method.toUpperCase();
        params = params || {};
        var paramsArray = [];
        for (var i in params) {
            paramsArray.push(i + "=" + params[i])
        }
        var paramsString = paramsArray.join("&");
        if (method == "GET") {
            url += "?" + paramsString
        }
        xhr.open(method, url, true);
        xhr.onreadystatechange = function() {
            if (xhr.readyState != 4)
                return;
            if (xhr.status < 200 || xhr.status >= 300) {
                fail(xhr.status, xhr.responseText)
            } else {
                success(xhr.status, xhr.responseText)
            }
        }
        ;
        if (method == "POST") {
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded; charset=UTF-8");
            xhr.send(paramsString)
        } else {
            xhr.send(null )
        }
    }
};
var ArrayManager = {
    elements: [],
    arrays: [],
    remove: function(array, element) {
        this.arrays.push(array);
        this.elements.push(element)
    },
    flush: function() {
        var ind;
        for (var i in this.arrays) {
            ind = this.arrays[i].indexOf(this.elements[i]);
            if (ind >= 0) {
                this.arrays[i].splice(ind, 1)
            }
        }
        this.arrays = [];
        this.elements = []
    },
    init: function() {
        this.arrays = [];
        this.elements = []
    }
};
var Encoder = {
    buildString: function(tab) {
        var s = "", content;
        for (var i in tab) {
            content = (tab[i] || "").toString();
            content = content.replace(/=/g, " ");
            content = content.replace(/\|/g, " ");
            s += i + "=" + content + "|"
        }
        return s
    },
    encode: function(hash) {
        var str = Encoder.buildString(hash);
        var key = ~~Util.rand(1, 255);
        var encodedString = Encoder.encodeString(str, key);
        return encodeURIComponent(encodedString)
    },
    encodeString: function(s, cle) {
        var enc = "", c;
        for (var i = 0; i < s.length; i++) {
            c = s.charCodeAt(i);
            enc += String.fromCharCode((c + cle) % 256)
        }
        enc = String.fromCharCode(cle) + enc;
        return enc
    }
};
var Detect = {
    agent: navigator.userAgent.toLowerCase(),
    isMobile: function() {
        return this.isAndroid() || this.isFirefoxOS() || this.isWindowsMobile() || this.isIOS()
    },
    isAndroid: function() {
        return this.agent.indexOf("android") >= 0
    },
    isFirefoxOS: function() {
        return !this.isAndroid() && this.agent.indexOf("firefox") >= 0 && this.agent.indexOf("mobile") >= 0
    },
    isIOS: function() {
        return this.agent.indexOf("ios") >= 0 || this.agent.indexOf("ipod") >= 0 || this.agent.indexOf("ipad") >= 0 || this.agent.indexOf("iphone") >= 0
    },
    isWindowsMobile: function() {
        return this.agent.indexOf("windows") >= 0 && this.agent.indexOf("mobile") >= 0 || this.agent.indexOf("iemobile") >= 0
    },
    isTizen: function() {
        return this.agent.indexOf("tizen") >= 0
    }
};
var resourceManager = {
    processImages: function(images) {
        var canvas = DOM.create("canvas");
        var c = canvas.getContext("2d");
        resources.folder = resources.folder || "";
        R.image = R.image || {};
        if (resources.image) {
            for (var i in resources.image) {
                R.image[i] = images[resources.folder + resources.image[i]]
            }
        }
        R.pattern = R.pattern || {};
        if (resources.pattern) {
            for (var i in resources.pattern) {
                R.pattern[i] = c.createPattern(images[resources.folder + resources.pattern[i]], "repeat")
            }
        }
        R.sprite = R.sprite || {};
        if (resources.sprite) {
            for (var i in resources.sprite) {
                R.sprite[i] = this.createSprite(images[resources.folder + resources.sprite[i].sheet], resources.sprite[i]);
                if (resources.sprite[i].pattern) {
                    R.pattern[i] = c.createPattern(R.sprite[i], "repeat")
                }
            }
        }
        R.animation = R.animation || {};
        if (resources.animation) {
            for (var i in resources.animation) {
                R.animation[i] = [];
                for (var j = 0; j < resources.animation[i].length; j++) {
                    if (R.sprite[resources.animation[i][j]]) {
                        R.animation[i].push(R.sprite[resources.animation[i][j]])
                    } else {
                        console.log("Error for animation " + i + ': sprite "' + resources.animation[i][j] + '" not found')
                    }
                }
            }
        }
        R.raw = R.raw || {};
        if (resources.raw) {
            for (var i in resources.raw) {
                R.raw[i] = resources.raw[i] instanceof Function ? resources.raw[i]() : resources.raw[i]
            }
        }
        R.string = R.string || {};
        if (resources.string) {
            var lang = this.getLanguage(resources.string);
            if (!resources.string[lang]) {
                var pp = function(obj) {
                    if (typeof obj == "string") {
                        return
                    } else {
                        var o = {};
                        for (var i in obj) {
                            if (typeof obj[i] == "string") {
                                o[i] = "{" + i + "}"
                            } else {
                                o[i] = pp(obj[i])
                            }
                        }
                        return o
                    }
                }
                ;
                resources.string[lang] = pp(resources.string.en)
            }
            for (var i in resources.string[lang]) {
                R.string[i] = resources.string[lang][i]
            }
            for (var i in R.string) {
                if (i.charAt(0) == "$") {
                    try {
                        DOM.get(i.substring(1)).innerHTML = R.string[i]
                    } catch (e) {
                        console.log("DOM element " + i + " does not exist")
                    }
                }
            }
        }
        resources = null ;
        resourceManager = null 
    },
    createSprite: function(image, details) {
        var canvas = DOM.create("canvas");
        var c = canvas.getContext("2d");
        canvas.width = details.width;
        canvas.height = details.height;
        c.drawImage(image, details.x, details.y, details.width, details.height, 0, 0, details.width, details.height);
        return canvas
    },
    getNecessaryImages: function() {
        var res = [];
        for (var i in resources.image) {
            res.push(resources.folder + resources.image[i])
        }
        for (var i in resources.pattern) {
            res.push(resources.folder + resources.pattern[i])
        }
        for (var i in resources.sprite) {
            res.push(resources.folder + resources.sprite[i].sheet)
        }
        Util.arrayUnique(res);
        return res
    },
    getLanguage: function(languages) {
        var lang = null ;
        var browser_language = null ;
        var params = Util.analyzeParameters();
        if (params.lang) {
            return params.lang
        }
        if (navigator && navigator.userAgent && (browser_language = navigator.userAgent.match(/android.*\W(\w\w)-(\w\w)\W/i))) {
            browser_language = browser_language[1]
        }
        if (!browser_language && navigator) {
            if (navigator.language) {
                browser_language = navigator.language
            } else if (navigator.browserLanguage) {
                browser_language = navigator.browserLanguage
            } else if (navigator.systemLanguage) {
                browser_language = navigator.systemLanguage
            } else if (navigator.userLanguage) {
                browser_language = navigator.userLanguage
            }
            browser_language = browser_language.substr(0, 2)
        }
        for (var i in languages) {
            if (browser_language.indexOf(i) >= 0) {
                lang = i;
                break
            } else if (!lang) {
                lang = i
            }
        }
        return lang
    }
};
var cycleManager = {
    init: function(cycle, fpsMin) {
        this.pause = false;
        this.oncycle = cycle;
        var hidden, visibilityChange;
        if (typeof document.hidden !== "undefined") {
            hidden = "hidden";
            visibilityChange = "visibilitychange"
        } else if (typeof document.mozHidden !== "undefined") {
            hidden = "mozHidden";
            visibilityChange = "mozvisibilitychange"
        } else if (typeof document.msHidden !== "undefined") {
            hidden = "msHidden";
            visibilityChange = "msvisibilitychange"
        } else if (typeof document.webkitHidden !== "undefined") {
            hidden = "webkitHidden";
            visibilityChange = "webkitvisibilitychange"
        }
        this.focus = true;
        if (!hidden) {
            DOM.on(window, "focus", function() {
                cycleManager.focus = true
            });
            DOM.on(window, "blur", function() {
                cycleManager.focus = false
            })
        } else {
            DOM.on(document, visibilityChange, function() {
                cycleManager.focus = !document[hidden]
            })
        }
        this.lastCycle = Date.now();
        this.fpsMin = fpsMin || 10;
        this.framesUntilNextStat = 0;
        this.lastStat = 0;
        this.fakeLag = document.location.search.indexOf("fakelag") >= 0;
        this.fps = 0;
        this.requestId = null ;
        this.init = null ;
        this.resume();
        if (window.kik && kik.browser && kik.browser.on) {
            kik.browser.on("background", function() {
                cycleManager.stop()
            });
            kik.browser.on("foreground", function() {
                cycleManager.resume()
            })
        }
        document.body.addEventListener("touchstart", function() {
            var now = Date.now();
            if (cycleManager.lastCycle - now > 2e3) {
                cycleManager.resume()
            }
        }, false)
    },
    stop: function() {
        this.pause = true;
        cancelAnimFrame(this.requestId)
    },
    resume: function() {
        this.pause = false;
        cancelAnimFrame(this.requestId);
        (function() {
            cycleManager.cycle();
            cycleManager.requestId = requestAnimFrame(arguments.callee)
        })()
    },
    cycle: function() {
        var now = Date.now();
        var elapsed = Math.min((now - this.lastCycle) / 1e3, 1 / this.fpsMin);
        this.lastCycle = now;
        if (!this.pause) {
            this.oncycle(elapsed);
            this.framesUntilNextStat--;
            if (this.framesUntilNextStat <= 0) {
                this.framesUntilNextStat = 60;
                this.fps = ~~(60 * 1e3 / (Date.now() - this.lastStat + elapsed));
                this.lastStat = Date.now()
            }
        }
    }
};
var resizer = {
    init: function(width, height, element, desktop) {
        this.enabled = Util.isTouchScreen() || desktop;
        this.targetWidth = width;
        this.targetHeight = height;
        this.element = element;
        this.dimensions = {
            width: width,
            height: height
        };
        this.scale = 1;
        if (Util.isTouchScreen() || desktop) {
            DOM.on(window, "resize orientationchange", function() {
                resizer.resize()
            });
            this.resize();
            this.toResize = null 
        }
        this.init = null 
    },
    resize: function() {
        if (!this.toResize && this.enabled) {
            this.toResize = setTimeout(function() {
                if (!resizer.enabled)
                    return;
                window.scrollTo(0, 1);
                resizer.toResize = null ;
                resizer.dimensions = DOM.fitScreen(resizer.element, resizer.targetWidth / resizer.targetHeight);
                resizer.scale = resizer.dimensions.height / resizer.targetHeight
            }, 1e3)
        }
    }
};
if (window.cordova) {
    document.addEventListener("deviceready", function() {
        cordova.exec(null , null , "SplashScreen", "hide", []);
        DOM.notify('More HTML5 games available at <a style="color:white" href="' + GameParams.moregamesurl + '">' + GameParams.moregamesurl + "</a>", 3e3)
    }, false)
}
if (!Function.prototype.bind) {
    Function.prototype.bind = function(oThis) {
        if (typeof this !== "function") {
            throw new TypeError("Function.prototype.bind - what is trying to be bound is not callable")
        }
        var aArgs = Array.prototype.slice.call(arguments, 1)
          , fToBind = this
          , fNOP = function() {}
          , fBound = function() {
            return fToBind.apply(this instanceof fNOP && oThis ? this : oThis, aArgs.concat(Array.prototype.slice.call(arguments)))
        }
        ;
        fNOP.prototype = this.prototype;
        fBound.prototype = new fNOP;
        return fBound
    }
}
window.originalOpen = window.open;
Number.prototype.mod = function(n) {
    return (this % n + n) % n
}
;
window.noop = new Function;
window.identity = function(v) {
    return v
}
;
function ResourceLoader(settings) {
    this.settings = settings;
    this.appCache = window.applicationCache;
    this.finished = false;
    this.message = null 
}
ResourceLoader.prototype.load = function(end, canvas) {
    this.endCallback = end;
    this.canvasOutput = canvas;
    if (!this.appCache || this.appCache.status === this.appCache.UNCACHED) {
        this.loadResources()
    } else {
        this.loadCache()
    }
}
;
ResourceLoader.prototype.loadCache = function() {
    this.message = "Updating...";
    this.appCache.addEventListener("checking", this.checkingCache.bind(this), false);
    this.appCache.addEventListener("noupdate", this.loadResources.bind(this), false);
    this.appCache.addEventListener("obsolete", this.loadResources.bind(this), false);
    this.appCache.addEventListener("error", this.loadResources.bind(this), false);
    this.appCache.addEventListener("cached", this.loadResources.bind(this), false);
    this.appCache.addEventListener("downloading", this.updatingCache.bind(this), false);
    this.appCache.addEventListener("progress", this.updatingCacheProgress.bind(this), false);
    this.appCache.addEventListener("updateready", this.updatingCacheReady.bind(this), false);
    if (this.appCache.status === this.appCache.IDLE) {
        try {
            this.appCache.update()
        } catch (e) {
            this.loadResources()
        }
    }
}
;
ResourceLoader.prototype.checkingCache = function() {
    if (!this.finished) {
        this.showProgress(this.canvasOutput, 0)
    }
}
;
ResourceLoader.prototype.updatingCache = function(e) {
    if (this.canvasOutput && !this.finished) {
        this.showProgress(this.canvasOutput, 0)
    }
}
;
ResourceLoader.prototype.updatingCacheProgress = function(e) {
    if (this.canvasOutput && !this.finished) {
        this.showProgress(this.canvasOutput, e.loaded / e.total || 0)
    }
}
;
ResourceLoader.prototype.updatingCacheReady = function(e) {
    if (!this.finished) {
        this.finished = true;
        try {
            this.appCache.swapCache()
        } catch (e) {}
        location.reload()
    }
}
;
ResourceLoader.prototype.loadResources = function() {
    this.message = "Loading assets. Please wait...";
    this.R = {};
    this.processLanguage(this.R);
    var images = this.getNecessaryImages();
    var loader = this;
    Util.preload(images, this.resourcesProgress.bind(this), this.resourcesLoaded.bind(this), this.resourcesError.bind(this))
}
;
ResourceLoader.prototype.resourcesError = function(imageSrc) {
    alert("Could not load " + imageSrc + ".\nUnable to launch.")
}
;
ResourceLoader.prototype.resourcesProgress = function(img, progress) {
    if (this.canvasOutput && !this.finished) {
        this.showProgress(this.canvasOutput, progress)
    }
}
;
ResourceLoader.prototype.resourcesLoaded = function(loadedImages) {
    if (!this.finished) {
        this.finished = true;
        this.processImages(loadedImages, this.R);
        this.endCallback(this.R)
    }
}
;
ResourceLoader.prototype.showProgress = function(canvas, progress) {
    var ctx = canvas.getContext("2d");
    ctx.fillStyle = "#000";
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    ctx.font = "10px Arial";
    ctx.fillStyle = "gray";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillText(this.message, canvas.width / 2, canvas.height / 2 - 20);
    ctx.fillRect(0, canvas.height / 2 - 5, canvas.width, 10);
    ctx.fillStyle = "white";
    ctx.fillRect(0, canvas.height / 2 - 5, progress * canvas.width, 10);
    ctx.fillStyle = "black";
    ctx.textAlign = "right";
    ctx.fillText(~~(progress * 100) + "%", progress * canvas.width - 2, canvas.height / 2)
}
;
ResourceLoader.prototype.createSprite = function(image, details) {
    var canvas = document.createElement("canvas");
    var c = canvas.getContext("2d");
    canvas.width = details.width;
    canvas.height = details.height;
    c.drawImage(image, details.x, details.y, details.width, details.height, 0, 0, details.width, details.height);
    return canvas
}
;
ResourceLoader.prototype.getNecessaryImages = function() {
    var res = [];
    for (var i in this.settings.image) {
        res.push(this.settings.folder + this.settings.image[i])
    }
    for (var i in this.settings.pattern) {
        res.push(this.settings.folder + this.settings.pattern[i])
    }
    for (var i in this.settings.sprite) {
        res.push(this.settings.folder + this.settings.sprite[i].sheet)
    }
    Util.arrayUnique(res);
    return res
}
;
ResourceLoader.prototype.getLanguage = function(languages) {
    var lang = null ;
    var browser_language = null ;
    var params = Util.analyzeParameters();
    if (params.lang) {
        return params.lang
    }
    if (navigator && navigator.userAgent && (browser_language = navigator.userAgent.match(/android.*\W(\w\w)-(\w\w)\W/i))) {
        browser_language = browser_language[1]
    }
    if (!browser_language && navigator) {
        if (navigator.language) {
            browser_language = navigator.language
        } else if (navigator.browserLanguage) {
            browser_language = navigator.browserLanguage
        } else if (navigator.systemLanguage) {
            browser_language = navigator.systemLanguage
        } else if (navigator.userLanguage) {
            browser_language = navigator.userLanguage
        }
        browser_language = browser_language.substr(0, 2)
    }
    for (var i in languages) {
        if (browser_language.indexOf(i) >= 0) {
            lang = i;
            break
        } else if (!lang) {
            lang = i
        }
    }
    return lang
}
;
ResourceLoader.prototype.processImages = function(images, R) {
    var canvas = DOM.create("canvas");
    var c = canvas.getContext("2d");
    this.settings.folder = this.settings.folder || "";
    R.image = R.image || {};
    if (this.settings.image) {
        for (var i in this.settings.image) {
            R.image[i] = images[this.settings.folder + this.settings.image[i]]
        }
    }
    R.pattern = R.pattern || {};
    if (this.settings.pattern) {
        for (var i in this.settings.pattern) {
            R.pattern[i] = c.createPattern(images[this.settings.folder + this.settings.pattern[i]], "repeat");
            R.pattern[i].width = images[this.settings.folder + this.settings.pattern[i]].width;
            R.pattern[i].height = images[this.settings.folder + this.settings.pattern[i]].height
        }
    }
    R.sprite = R.sprite || {};
    if (this.settings.sprite) {
        for (var i in this.settings.sprite) {
            R.sprite[i] = this.createSprite(images[this.settings.folder + this.settings.sprite[i].sheet], this.settings.sprite[i]);
            if (this.settings.sprite[i].pattern) {
                R.pattern[i] = c.createPattern(R.sprite[i], "repeat");
                R.pattern[i].width = R.sprite[i].width;
                R.pattern[i].height = R.sprite[i].height
            }
        }
    }
    R.animation = R.animation || {};
    if (this.settings.animation) {
        for (var i in this.settings.animation) {
            R.animation[i] = [];
            for (var j = 0; j < this.settings.animation[i].length; j++) {
                if (R.sprite[this.settings.animation[i][j]]) {
                    R.animation[i].push(R.sprite[this.settings.animation[i][j]])
                } else {
                    console.log("Error for animation " + i + ': sprite "' + this.settings.animation[i][j] + '" not found')
                }
            }
        }
    }
    R.raw = R.raw || {};
    if (this.settings.raw) {
        for (var i in this.settings.raw) {
            R.raw[i] = this.settings.raw[i] instanceof Function ? this.settings.raw[i]() : this.settings.raw[i]
        }
    }
}
;
ResourceLoader.prototype.processLanguage = function(R) {
    R.string = R.string || {};
    if (this.settings.string) {
        this.language = this.getLanguage(this.settings.string);
        if (!this.settings.string[this.language]) {
            var pp = function(obj) {
                if (typeof obj == "string") {
                    return
                } else {
                    var o = {};
                    for (var i in obj) {
                        if (typeof obj[i] == "string") {
                            o[i] = "{" + i + "}"
                        } else {
                            o[i] = pp(obj[i])
                        }
                    }
                    return o
                }
            }
            ;
            this.settings.string[this.language] = pp(this.settings.string.en)
        }
        for (var i in this.settings.string[this.language]) {
            R.string[i] = this.settings.string[this.language][i]
        }
        for (var i in R.string) {
            if (i.charAt(0) == "$") {
                try {
                    DOM.get(i.substring(1)).innerHTML = R.string[i]
                } catch (e) {
                    console.log("DOM element " + i + " does not exist")
                }
            }
        }
    }
}
;
function extend(subClass, superClass) {
    if (!subClass.extendsClasses || !subClass.extendsClasses[superClass]) {
        for (var i in superClass.prototype) {
            if (!subClass.prototype[i]) {
                subClass.prototype[i] = superClass.prototype[i]
            }
        }
        subClass.extendsClasses = subClass.extendsClasses || {};
        subClass.extendsClasses[superClass] = true
    }
}
function extendPrototype(superClasses, proto) {
    superClasses = superClasses instanceof Array ? superClasses : [superClasses];
    var subProto = {
        superior: {}
    };
    for (var i in superClasses) {
        for (var j in superClasses[i].prototype) {
            subProto[j] = superClasses[i].prototype[j];
            subProto.superior[j] = superClasses[i].prototype[j]
        }
    }
    if (proto) {
        for (var i in proto) {
            subProto[i] = proto[i]
        }
    }
    return subProto
}
function quickImplementation(object, prototype) {
    for (var i in prototype) {
        object[i] = prototype[i]
    }
    return object
}
Math.linearTween = function(t, b, c, d) {
    return c * t / d + b
}
;
Math.easeInQuad = function(t, b, c, d) {
    return c * (t /= d) * t + b
}
;
Math.easeOutQuad = function(t, b, c, d) {
    return -c * (t /= d) * (t - 2) + b
}
;
Math.easeInOutQuad = function(t, b, c, d) {
    if ((t /= d / 2) < 1)
        return c / 2 * t * t + b;
    return -c / 2 * (--t * (t - 2) - 1) + b
}
;
Math.easeInCubic = function(t, b, c, d) {
    return c * (t /= d) * t * t + b
}
;
Math.easeOutCubic = function(t, b, c, d) {
    return c * ((t = t / d - 1) * t * t + 1) + b
}
;
Math.easeInOutCubic = function(t, b, c, d) {
    if ((t /= d / 2) < 1)
        return c / 2 * t * t * t + b;
    return c / 2 * ((t -= 2) * t * t + 2) + b
}
;
Math.easeInQuart = function(t, b, c, d) {
    return c * (t /= d) * t * t * t + b
}
;
Math.easeOutQuart = function(t, b, c, d) {
    return -c * ((t = t / d - 1) * t * t * t - 1) + b
}
;
Math.easeInOutQuart = function(t, b, c, d) {
    if ((t /= d / 2) < 1)
        return c / 2 * t * t * t * t + b;
    return -c / 2 * ((t -= 2) * t * t * t - 2) + b
}
;
Math.easeInQuint = function(t, b, c, d) {
    return c * (t /= d) * t * t * t * t + b
}
;
Math.easeOutQuint = function(t, b, c, d) {
    return c * ((t = t / d - 1) * t * t * t * t + 1) + b
}
;
Math.easeInOutQuint = function(t, b, c, d) {
    if ((t /= d / 2) < 1)
        return c / 2 * t * t * t * t * t + b;
    return c / 2 * ((t -= 2) * t * t * t * t + 2) + b
}
;
Math.easeInSine = function(t, b, c, d) {
    return -c * Math.cos(t / d * (Math.PI / 2)) + c + b
}
;
Math.easeOutSine = function(t, b, c, d) {
    return c * Math.sin(t / d * (Math.PI / 2)) + b
}
;
Math.easeInOutSine = function(t, b, c, d) {
    return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b
}
;
Math.easeInExpo = function(t, b, c, d) {
    return t == 0 ? b : c * Math.pow(2, 10 * (t / d - 1)) + b
}
;
Math.easeOutExpo = function(t, b, c, d) {
    return t == d ? b + c : c * (-Math.pow(2, -10 * t / d) + 1) + b
}
;
Math.easeInOutExpo = function(t, b, c, d) {
    if (t == 0)
        return b;
    if (t == d)
        return b + c;
    if ((t /= d / 2) < 1)
        return c / 2 * Math.pow(2, 10 * (t - 1)) + b;
    return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b
}
;
Math.easeInCirc = function(t, b, c, d) {
    return -c * (Math.sqrt(1 - (t /= d) * t) - 1) + b
}
;
Math.easeOutCirc = function(t, b, c, d) {
    return c * Math.sqrt(1 - (t = t / d - 1) * t) + b
}
;
Math.easeInOutCirc = function(t, b, c, d) {
    if ((t /= d / 2) < 1)
        return -c / 2 * (Math.sqrt(1 - t * t) - 1) + b;
    return c / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + b
}
;
Math.easeInElastic = function(t, b, c, d, a, p) {
    if (t == 0)
        return b;
    if ((t /= d) == 1)
        return b + c;
    if (!p)
        p = d * .3;
    if (a < Math.abs(c)) {
        a = c;
        var s = p / 4
    } else
        var s = p / (2 * Math.PI) * Math.asin(c / a);
    return -(a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b
}
;
Math.easeOutElastic = function(t, b, c, d, a, p) {
    if (t == 0)
        return b;
    if ((t /= d) == 1)
        return b + c;
    if (!p)
        p = d * .3;
    if (a < Math.abs(c)) {
        a = c;
        var s = p / 4
    } else
        var s = p / (2 * Math.PI) * Math.asin(c / a);
    return a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b
}
;
Math.easeInOutElastic = function(t, b, c, d, a, p) {
    if (t == 0)
        return b;
    if ((t /= d / 2) == 2)
        return b + c;
    if (!p)
        p = d * (.3 * 1.5);
    if (a < Math.abs(c)) {
        a = c;
        var s = p / 4
    } else
        var s = p / (2 * Math.PI) * Math.asin(c / a);
    if (t < 1)
        return -.5 * (a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
    return a * Math.pow(2, -10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b
}
;
Math.easeInBack = function(t, b, c, d, s) {
    if (s == undefined)
        s = 1.70158;
    return c * (t /= d) * t * ((s + 1) * t - s) + b
}
;
Math.easeOutBack = function(t, b, c, d, s) {
    if (s == undefined)
        s = 1.70158;
    return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b
}
;
Math.easeInOutBack = function(t, b, c, d, s) {
    if (s == undefined)
        s = 1.70158;
    if ((t /= d / 2) < 1)
        return c / 2 * (t * t * (((s *= 1.525) + 1) * t - s)) + b;
    return c / 2 * ((t -= 2) * t * (((s *= 1.525) + 1) * t + s) + 2) + b
}
;
Math.easeInBounce = function(t, b, c, d) {
    return c - Math.easeOutBounce(d - t, 0, c, d) + b
}
;
Math.easeOutBounce = function(t, b, c, d) {
    if ((t /= d) < 1 / 2.75) {
        return c * (7.5625 * t * t) + b
    } else if (t < 2 / 2.75) {
        return c * (7.5625 * (t -= 1.5 / 2.75) * t + .75) + b
    } else if (t < 2.5 / 2.75) {
        return c * (7.5625 * (t -= 2.25 / 2.75) * t + .9375) + b
    } else {
        return c * (7.5625 * (t -= 2.625 / 2.75) * t + .984375) + b
    }
}
;
Math.easeInOutBounce = function(t, b, c, d) {
    if (t < d / 2)
        return Math.easeInBounce(t * 2, 0, c, d) * .5 + b;
    return Math.easeOutBounce(t * 2 - d, 0, c, d) * .5 + c * .5 + b
}
;
function Resizer(options) {
    this.delay = options.delay || 0;
    this.element = options.element || null ;
    this.baseWidth = options.baseWidth;
    this.baseHeight = options.baseHeight;
    this.onResize = options.onResize;
    this.enabled = true;
    this.scale = 1;
    this.resizeTimeout = null 
}
Resizer.prototype = {
    needsResize: function(maxWidth, maxHeight) {
        clearTimeout(this.resizeTimeout);
        if (this.enabled) {
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            this.resizeTimeout = setTimeout(this.resize.bind(this), this.delay)
        }
    },
    resize: function() {
        this.resizeTimeout = null ;
        var dimensions = this.getFittingDimensions(this.maxWidth, this.maxHeight);
        this.element.style.width = dimensions.width + "px";
        this.element.style.height = dimensions.height + "px";
        if (this.onResize) {
            this.onResize.call(this)
        }
    },
    scaleX: function() {
        var rect = this.element.getBoundingClientRect();
        return rect.width / this.baseWidth || 1
    },
    scaleY: function() {
        var rect = this.element.getBoundingClientRect();
        return rect.height / this.baseHeight || 1
    },
    getFittingDimensions: function(maxWidth, maxHeight) {
        var availableRatio = maxWidth / maxHeight;
        var baseRatio = this.baseWidth / this.baseHeight;
        var ratioDifference = Math.abs(availableRatio - baseRatio);
        var width, height;
        if (ratioDifference <= .17) {
            width = maxWidth;
            height = maxHeight
        } else if (availableRatio <= baseRatio) {
            width = maxWidth;
            height = width / baseRatio
        } else {
            height = maxHeight;
            width = height * baseRatio
        }
        return {
            width: width,
            height: height
        }
    }
};
function Screen(game) {
    this.game = game;
    this.areas = [];
    this.currentActionArea = null ;
    this.view = null 
}
Screen.prototype = {
    getId: function() {
        return "unnamed"
    },
    cycle: function(elapsed) {},
    touchStart: function(x, y) {
        for (var i in this.areas) {
            if (this.areas[i].enabled && this.areas[i].contains(x, y)) {
                this.currentActionArea = this.areas[i];
                this.currentActionArea.actionStart(x, y);
                break
            }
        }
    },
    touchMove: function(x, y) {
        if (this.currentActionArea) {
            if (!this.currentActionArea.contains(x, y)) {
                this.currentActionArea.actionCancel(x, y);
                this.currentActionArea = null 
            } else {
                this.currentActionArea.actionMove(x, y)
            }
        }
    },
    touchEnd: function(x, y) {
        if (this.currentActionArea && this.currentActionArea.contains(x, y)) {
            this.currentActionArea.actionPerformed(x, y)
        }
        this.currentActionArea = null 
    },
    keyDown: function(keyCode) {},
    keyUp: function(keyCode) {},
    mouseWheel: function(delta) {},
    backButton: function() {},
    create: function() {},
    destroy: function() {},
    addArea: function(area) {
        this.areas.push(area)
    },
    areaContains: function(x, y) {
        for (var i in this.areas) {
            if (this.areas[i].enabled && this.areas[i].contains(x, y)) {
                return this.areas[i]
            }
        }
        return null 
    }
};
function Area(settings) {
    settings = settings || {};
    this.x = settings.x || 0;
    this.y = settings.y || 0;
    this.width = settings.width || 0;
    this.height = settings.height || 0;
    this.cursor = settings.cursor || "pointer";
    this.onactionperformed = settings.actionPerformed || noop;
    this.onactionstart = settings.actionStart || noop;
    this.onactioncancel = settings.actionCancel || noop;
    this.onactionmove = settings.actionMove || noop;
    this.enabled = true
}
Area.prototype = {
    contains: function(x, y) {
        return x >= this.x && y >= this.y && x <= this.x + this.width && y <= this.y + this.height
    },
    actionPerformed: function(x, y) {
        this.onactionperformed(x, y)
    },
    actionStart: function(x, y) {
        this.onactionstart(x, y)
    },
    actionCancel: function(x, y) {
        this.onactioncancel(x, y)
    },
    actionMove: function(x, y) {
        this.onactionmove(x, y)
    }
};
function DisplayableObject() {
    this.parent = null ;
    this.x = this.y = 0;
    this.rotation = 0;
    this.scaleX = this.scaleY = 1;
    this.alpha = 1;
    this.visible = true
}
DisplayableObject.prototype = {
    applyTransforms: function(c) {
        if (this.x != 0 || this.y != 0)
            c.translate(this.x, this.y);
        if (this.scaleX != 1 || this.scaleY != 1)
            c.scale(this.scaleX, this.scaleY);
        if (this.rotation != 0)
            c.rotate(this.rotation);
        if (this.alpha != 1)
            c.globalAlpha *= this.alpha
    },
    doRender: function(c) {
        if (this.visible && this.alpha > .01 && this.scaleX != 0 && this.scaleY != 0) {
            c.save();
            this.applyTransforms(c);
            this.render(c);
            c.restore()
        }
    },
    render: function(c) {
        throw new Error("Rendering undefined")
    },
    remove: function() {
        if (this.parent) {
            this.parent.removeChild(this)
        }
    },
    leaves: function() {
        return 1
    }
};
function DisplayableShape(drawFunction) {
    DisplayableObject.call(this);
    this.drawFunction = drawFunction
}
DisplayableShape.prototype = extendPrototype(DisplayableObject, {
    render: function(c) {
        this.drawFunction(c)
    }
});
function DisplayableContainer() {
    DisplayableObject.call(this);
    this.children = []
}
DisplayableContainer.prototype = extendPrototype(DisplayableObject, {
    render: function(c) {
        var i = -1;
        while (this.children[++i]) {
            this.children[i].doRender(c)
        }
    },
    addChild: function(child) {
        if (child.parent) {
            child.parent.removeChild(child)
        }
        this.children.push(child);
        child.parent = this;
        child.parentIndex = this.children.length - 1
    },
    removeChild: function(child) {
        if (!isNaN(child.parentIndex)) {
            this.children.splice(child.parentIndex, 1);
            for (var i = child.parentIndex; i < this.children.length; i++) {
                this.children[i].parentIndex--
            }
            child.parent = null ;
            child.parentIndex = null 
        }
    },
    clear: function() {
        for (var i in this.children) {
            this.children[i].parent = null ;
            this.children[i].parentIndex = null 
        }
        this.children = []
    },
    leaves: function() {
        var total = 0;
        for (var i in this.children) {
            total += this.children[i].leaves()
        }
        return total
    }
});
function DisplayableRectangle() {
    DisplayableContainer.call(this);
    this.color = "#000";
    this.width = 0;
    this.height = 0
}
DisplayableRectangle.prototype = extendPrototype(DisplayableContainer, {
    render: function(c) {
        c.fillStyle = this.color;
        c.fillRect(0, 0, this.width, this.height);
        DisplayableContainer.prototype.render.call(this, c)
    }
});
function DisplayableTextField() {
    DisplayableObject.call(this);
    this.text = null ;
    this.font = "12pt Arial";
    this.textAlign = "left";
    this.textBaseline = "top";
    this.color = "#000";
    this.shadowColor = null ;
    this.shadowOffsetX = 0;
    this.shadowOffsetY = 0;
    this.shadowBlur = 0
}
DisplayableTextField.prototype = extendPrototype(DisplayableObject, {
    render: function(c) {
        if (this.text != null  && this.text.toString().length > 0) {
            c.font = this.font;
            if (this.shadowBlur) {
                c.shadowBlur = this.shadowBlur;
                c.shadowOffsetX = this.shadowOffsetX;
                c.shadowOffsetY = this.shadowOffsetY;
                c.shadowColor = this.shadowColor
            }
            c.textAlign = this.textAlign;
            c.textBaseline = this.textBaseline;
            if (this.shadowColor && !this.shadowBlur) {
                c.fillStyle = this.shadowColor;
                c.fillText(this.text, this.shadowOffsetX, this.shadowOffsetY)
            }
            c.fillStyle = this.color;
            c.fillText(this.text, 0, 0)
        }
    }
});
function DisplayableImage() {
    DisplayableObject.call(this);
    this.image = null ;
    this.anchorX = this.anchorY = 0
}
DisplayableImage.prototype = extendPrototype(DisplayableObject, {
    render: function(c) {
        c.drawImage(this.image, this.anchorX, this.anchorY)
    }
});
function DisplayablePattern() {
    DisplayableObject.call(this);
    this.width = 0;
    this.height = 0;
    this.pattern = null ;
    this.patternWidth = 100;
    this.patternHeight = 100
}
DisplayablePattern.prototype = extendPrototype(DisplayableObject, {
    mod: function(n, mod) {
        return n % mod;
        console.log(n, mod, (n % mod + mod) % mod);
        return (n % mod + mod) % mod
    },
    applyTransforms: function(c) {
        c.translate(-this.mod(~~this.x, this.patternWidth * this.scaleX), -this.mod(~~this.y, this.patternHeight * this.scaleY));
        c.scale(this.scaleX, this.scaleY);
        c.globalAlpha = this.alpha
    },
    render: function(c) {
        c.fillStyle = this.pattern;
        c.fillRect(this.mod(~~this.x, this.patternWidth * this.scaleX), this.mod(~~this.y, this.patternHeight * this.scaleY), this.width, this.height)
    }
});
function MultilineTextField() {
    DisplayableTextField.call(this);
    this.maxWidth = 100;
    this.lineHeight = 20
}
MultilineTextField.prototype = extendPrototype(DisplayableTextField, {
    render: function(c) {
        c.font = this.font;
        c.textAlign = this.textAlign;
        c.textBaseline = "top";
        if (this.text != this.previouslyComputedText) {
            var lines = this.text.toString().split("\n");
            this.finalLines = [];
            var curLineWidth, words, metrics;
            for (var i = 0; i < lines.length; i++) {
                words = lines[i].split(" ");
                for (var j = 0; j < words.length; j++) {
                    metrics = c.measureText(words[j] + " ");
                    if (j == 0 || metrics.width + curLineWidth > this.maxWidth) {
                        this.finalLines.push("");
                        curLineWidth = 0
                    }
                    curLineWidth += metrics.width;
                    this.finalLines[this.finalLines.length - 1] += words[j] + " "
                }
            }
            this.previousComputedText = this.text
        }
        var totalHeight = this.finalLines.length * this.lineHeight;
        var y, step;
        if (this.baseline == "top") {
            y = 0;
            step = this.lineHeight
        } else if (this.baseline == "bottom") {
            y = totalHeight;
            step = -this.lineHeight
        } else {
            y = -totalHeight / 2;
            step = this.lineHeight
        }
        for (var i = 0; i < this.finalLines.length; i++,
        y += step) {
            if (this.shadowColor) {
                c.fillStyle = this.shadowColor;
                c.fillText(this.finalLines[i], this.shadowOffsetX, this.shadowOffsetY + y)
            }
            c.fillStyle = this.color;
            c.fillText(this.finalLines[i], 0, y)
        }
    }
});
function CachedContainer() {
    DisplayableContainer.call(this);
    this.width = 0;
    this.height = 0;
    this.offsetX = 0;
    this.offsetY = 0;
    this.cache = null ;
    this.cachingEnabled = true
}
CachedContainer.prototype = extendPrototype(DisplayableContainer, {
    render: function(c) {
        if (this.cachingEnabled) {
            c.drawImage(this.getCache(), -this.offsetX, -this.offsetY)
        } else {
            DisplayableContainer.prototype.render.call(this, c)
        }
    },
    renewCache: function() {
        this.cache = null 
    },
    addChild: function(child) {
        DisplayableContainer.prototype.addChild.call(this, child);
        this.cache = null 
    },
    removeChild: function(child) {
        DisplayableContainer.prototype.removeChild.call(this, child);
        this.cache = null 
    },
    getCache: function() {
        if (this.cache == null ) {
            this.cache = document.createElement("canvas");
            this.cache.width = this.width;
            this.cache.height = this.height;
            var c = this.cache.getContext("2d");
            c.translate(this.offsetX, this.offsetY);
            DisplayableContainer.prototype.render.call(this, c)
        }
        return this.cache
    }
});
function ClippedContainer() {
    DisplayableContainer.call(this);
    this.width = this.height = 100;
    this.borderWidth = 0
}
ClippedContainer.prototype = extendPrototype(DisplayableContainer, {
    render: function(c) {
        c.beginPath();
        c.rect(0, 0, this.width, this.height);
        if (this.borderWidth > 0) {
            c.lineWidth = this.borderWidth;
            c.strokeStyle = this.borderColor;
            c.stroke()
        }
        c.clip();
        DisplayableContainer.prototype.render.call(this, c)
    }
});
function ScrollableArea(settings) {
    Area.call(this, settings);
    this.content = settings.content;
    this.allowedMovement = settings.allowedMovement || 10;
    this.areas = [];
    this.totalMovement = 0
}
ScrollableArea.prototype = extendPrototype(Area, {
    actionStart: function(x, y) {
        this.lastPos = {
            x: x,
            y: y
        };
        this.totalMovement = 0;
        this.currentActionArea = null ;
        var sx = x + this.content.scrollX - this.x;
        var sy = y + this.content.scrollY - this.y;
        for (var i in this.areas) {
            if (this.areas[i].contains(sx, sy)) {
                this.currentActionArea = this.areas[i];
                this.currentActionArea.actionStart(sx, sy);
                break
            }
        }
    },
    actionMove: function(x, y) {
        var pos = {
            x: x,
            y: y
        };
        var pX = pos.x - this.lastPos.x;
        var pY = pos.y - this.lastPos.y;
        this.totalMovement += Math.sqrt(pX * pX + pY * pY);
        var sx = x + this.content.scrollX - this.x;
        var sy = y + this.content.scrollY - this.y;
        if (this.totalMovement > this.allowedMovement) {
            this.content.scrollTo(this.content.scrollX - pX, this.content.scrollY - pY);
            if (this.currentActionArea) {
                this.currentActionArea.actionCancel(sx, sy);
                this.currentActionArea = null 
            }
        } else if (this.currentActionArea) {
            if (this.currentActionArea.contains(sx, sy)) {
                this.currentActionArea.actionMove(sx, sy)
            } else {
                this.currentActionArea.actionCancel(sx, sy);
                this.currentActionArea = null 
            }
        }
        this.lastPos = pos
    },
    actionPerformed: function(x, y) {
        if (this.currentActionArea) {
            var sx = x + this.content.scrollX - this.x;
            var sy = y + this.content.scrollY - this.y;
            this.currentActionArea.actionPerformed(sx, sy)
        }
    },
    addSubArea: function(area) {
        this.areas.push(area)
    }
});
function ScrollableContainer() {
    ClippedContainer.call(this);
    this.contentWidth = 100;
    this.contentHeight = 100;
    this.scrollX = 0;
    this.scrollY = 0;
    this.elevator = new DisplayableRectangle;
    this.elevator.color = "#999999";
    this.addChild(this.elevator);
    this.content = new DisplayableContainer;
    this.addChild(this.content)
}
ScrollableContainer.prototype = extendPrototype(ClippedContainer, {
    scrollTo: function(x, y) {
        x = Util.limit(x, 0, this.contentWidth - this.width);
        y = Util.limit(y, 0, this.contentHeight - this.height);
        this.scrollX = x;
        this.scrollY = y;
        this.content.x = -x;
        this.content.y = -y;
        var prct = this.scrollY / (this.contentHeight - this.height) || 0;
        this.elevator.y = Util.limit(prct * (this.height - this.elevator.height), 5, this.height - this.elevator.height - 5)
    },
    setContent: function(c, w, h) {
        this.content.clear();
        this.content.addChild(c);
        this.contentWidth = Math.max(w, this.width);
        this.contentHeight = Math.max(h, this.height);
        this.elevator.width = 10;
        this.elevator.height = Util.limit(this.height * (1 - this.contentHeight / 500), 50, this.height);
        this.elevator.x = this.width - 15;
        this.elevator.y = 0;
        this.scrollTo(this.scrollX, this.scrollY)
    },
    getScrollableArea: function() {
        return new ScrollableArea({
            content: this,
            x: this.x,
            y: this.y,
            width: this.width,
            height: this.height,
            allowedMovement: 30,
            cursor: "default"
        })
    }
});
function ScrolledContent() {
    DisplayableContainer.call(this);
    this.container = null 
}
ScrolledContent.prototype = extendPrototype(DisplayableContainer, {
    render: function(c) {
        if (!this.container) {
            DisplayableContainer.prototype.render.call(this, c)
        } else {
            var i = -1, ch;
            while (this.children[++i]) {
                ch = this.children[i];
                if ((!ch.width || ch.x + ch.width >= this.container.scrollX && ch.x <= this.container.scrollX + this.container.width) && (!ch.height || ch.y + ch.height >= this.container.scrollY && ch.y <= this.container.scrollY + this.container.height)) {
                    this.children[i].doRender(c)
                }
            }
        }
    }
});
function Tween(object, property, from, to, duration, delay, onFinish) {
    this.object = object;
    this.delayLeft = delay || 0;
    this.duration = duration;
    this.elapsed = 0;
    this.property = property;
    this.from = from;
    this.to = to;
    this.onFinish = onFinish;
    this.cancelled = false;
    object[property] = from
}
Tween.prototype = {
    cycle: function(e) {
        if (this.delayLeft > 0) {
            this.delayLeft -= e;
            this.object[this.property] = this.from
        }
        if (this.delayLeft <= 0) {
            this.elapsed += e;
            if (this.elapsed >= this.duration) {
                this.finish()
            } else {
                this.progress()
            }
        }
    },
    finish: function() {
        this.object[this.property] = this.to;
        if (this.onFinish) {
            this.onFinish.call(this)
        }
    },
    cancel: function() {
        this.cancelled = true
    },
    isFinished: function() {
        return this.elapsed >= this.duration || this.cancelled
    },
    progress: function() {
        var prct = this.duration > 0 ? this.elapsed / this.duration : 1;
        this.object[this.property] = prct * (this.to - this.from) + this.from
    }
};
function Interpolation(settings) {
    this.object = settings.object;
    this.property = settings.property;
    this.delay = settings.delay || 0;
    this.duration = settings.duration || 1;
    this.from = settings.from;
    this.to = settings.to;
    this.easing = settings.easing || Math.linearTween;
    this.easingParameter = settings.easingParameter || null ;
    this.onFinish = settings.onFinish || noop;
    this.applyFunction = settings.applyFunction || function(easing, duration, from, to, elapsed, easingParam) {
        return easing(elapsed, from, to - from, duration, easingParam)
    }
    ;
    this.delayLeft = this.delay;
    this.elapsed = 0;
    this.cancelled = false
}
Interpolation.prototype = {
    cycle: function(e) {
        if (!this.cancelled) {
            if (this.delayLeft > 0) {
                this.delayLeft -= e;
                this.object[this.property] = this.from
            }
            if (this.delayLeft <= 0) {
                this.elapsed += e;
                if (this.elapsed >= this.duration) {
                    this.finish()
                } else {
                    this.progress()
                }
            }
        }
    },
    finish: function() {
        this.object[this.property] = this.to;
        this.cancelled = true;
        this.onFinish.call(this)
    },
    cancel: function() {
        this.cancelled = true
    },
    isFinished: function() {
        return this.elapsed >= this.duration || this.cancelled
    },
    progress: function() {
        this.object[this.property] = this.applyFunction(this.easing, this.duration, this.from, this.to, this.elapsed, this.easingParameter)
    },
    invert: function() {
        this.elapsed = 0;
        var from = this.from;
        this.from = this.to;
        this.to = from;
        this.cancelled = false
    }
};
var TweenPool = {
    tweens: [],
    speedFactor: 1,
    cycle: function(e) {
        var i = 0;
        while (this.tweens[i]) {
            this.tweens[i].cycle(e * this.speedFactor);
            if (!this.tweens[i].isFinished()) {
                i++
            } else {
                this.tweens.splice(i, 1)
            }
        }
    },
    remove: function(tw) {
        var index = this.tweens.indexOf(tw);
        if (index >= 0) {
            this.tweens.splice(index, 1)
        }
    },
    add: function(tw) {
        this.tweens.push(tw)
    },
    clear: function() {
        this.tweens = []
    }
};
var ColorUtils = {
    fromString: function(s) {
        if (s.charAt(0) == "#") {
            return this.fromHex(s)
        } else if (s.indexOf("rgba") == 0) {
            return this.fromRGBA(s)
        }
        return null 
    },
    fromRGBA: function(rgba) {
        rgba = rgba.replace("rgba(", "");
        rgba = rgba.replace(")", "");
        spl = rgba.split(",");
        return {
            r: parseInt(spl[0]),
            g: parseInt(spl[1]),
            b: parseInt(spl[2]),
            a: parseFloat(spl[3])
        }
    },
    fromHex: function(hex) {
        hex = hex.replace("#", "");
        var sr = hex.substr(0, 2);
        var sg = hex.substr(2, 2);
        var sb = hex.substr(4, 2);
        var sa = hex.substr(6, 2);
        var a = sa.length > 0 ? parseInt(sa, 16) / 255 || 0 : 1;
        return {
            r: parseInt(sr, 16) || 0,
            g: parseInt(sg, 16) || 0,
            b: parseInt(sb, 16) || 0,
            a: a
        }
    },
    toString: function(c) {
        return "rgba(" + ~~c.r + "," + ~~c.g + "," + ~~c.b + "," + c.a + ")"
    },
    easingApply: function(easing, duration, from, to, elapsed, easingParam) {
        var c1 = ColorUtils.fromString(from);
        var c2 = ColorUtils.fromString(to);
        var c3 = {
            r: Util.limit(easing(elapsed, c1.r, c2.r - c1.r, duration, easingParam), 0, 255),
            g: Util.limit(easing(elapsed, c1.g, c2.g - c1.g, duration, easingParam), 0, 255),
            b: Util.limit(easing(elapsed, c1.b, c2.b - c1.b, duration, easingParam), 0, 255),
            a: Util.limit(easing(elapsed, c1.a, c2.a - c1.a, duration, easingParam), 0, 1)
        };
        return ColorUtils.toString(c3)
    }
};
var P = {
    width: 640,
    height: 920,
    clay: false,
    loadState: location.search.indexOf("newgame") == -1,
    sound: location.search.indexOf("nosound") == -1,
    showFrameRate: location.search.indexOf("fps") >= 0,
    moreGamesURL: "www.cash4ads.com",
    adURL: "www.cash4ads.com",
    stateKey: "tap2play yo",
    ajaxPrefix: window.isCocoon || window.Clay ? "www.cash4ads.com" : "",
    leaderboardGetURL: "leaderboard",
    leaderboardSubmitURL: "submit",
    kikTokenCollectURL: "www.cash4ads.com",
    cocoon: window.isCocoon || location.search.indexOf("cocoon") >= 0
};
window.addToHomeConfig = {
    touchIcon: true,
    autostart: false
};
var resources = {
    folder: "img/",
    image: {
        logo_tap2play: "blk.jpg",
        more: "more.png",
        kik: "kik.png"
    },
    raw: {
        arrow: function() {
            var cache = document.createElement("canvas");
            cache.width = 90;
            cache.height = 90;
            var cacheCtx = cache.getContext("2d");
            cacheCtx.scale(2, 2);
            with (cacheCtx) {
                fillStyle = "#000";
                strokeStyle = "white";
                beginPath();
                moveTo(0, 20);
                lineTo(20, 0);
                lineTo(20, 10);
                lineTo(40, 10);
                lineTo(40, 30);
                lineTo(20, 30);
                lineTo(20, 40);
                closePath();
                fill();
                stroke()
            }
            return cache
        }
    },
    string: {
        en: {
            menu: {
                play: "Play",
                leaderboard: "Leaderboard"
            },
            gameplay: {
                instruction_move: "Hold left or right to move the ball",
                instruction_fall: "Now try not to fall down!"
            },
            leaderboard: {
                title: "Leaderboard",
                today: "Today",
                week: "Week",
                month: "Month",
                all: "All",
                back: "Back",
                next: ">",
                previous: "<"
            },
            end: {
                score: "Your score:",
                best: "Best:",
                rank: "Rank:",
                retry: "Try again",
                leaderboard: "Leaderboard",
                loading: "Loading...",
                share: "Share"
            },
            modal: {
                username: "Please enter a name to submit your score:",
                exitprompt: "Exit the game?"
            }
        }
    }
};
var R = {};
DOM.on(window, "load", function() {
    DOM.un(window, "load", arguments.callee);
    Tracker.beginStage("loading-" + (window.kik && kik.send ? "kik" : "nonkik"));
    can = DOM.get("gamecanvas");
    if (!can) {
        if (!P.cocoon) {
            can = document.createElement("canvas")
        } else {
            can = CocoonJS.App.createScreenCanvas()
        }
        can.id = "gamecanvas";
        DOM.get("viewport").appendChild(can)
    }
    can.width = P.width;
    can.height = P.height;
    ctx = can.getContext("2d");
    if (!Util.isTouchScreen()) {
        var link = document.createElement("link");
        link.setAttribute("rel", "stylesheet");
        link.setAttribute("type", "text/css");
        link.setAttribute("href", "css/desktop.css");
        document.head.appendChild(link)
    }
    window.resizer = new Resizer({
        element: DOM.get("viewport"),
        delay: 50,
        baseWidth: P.width,
        baseHeight: P.height,
        onResize: function() {
            window.scrollTo(0, 1)
        }
    });
    window.getDimensionsAndResize = function() {
        if (!P.cocoon) {
            var w = window.innerWidth;
            var h = window.innerHeight;
            var dimensions = window.getAvailableCanvasDimensions();
            if (!Util.isTouchScreen()) {
                dimensions.width *= .85;
                dimensions.height *= .85
            }
            this.resizer.needsResize(dimensions.width, dimensions.height)
        }
    }
    ;
    window.getAvailableCanvasDimensions = function() {
        return {
            width: window.innerWidth,
            height: window.innerHeight
        }
    }
    ;
    DOM.on(window, "resize orientationchange", getDimensionsAndResize);
    getDimensionsAndResize();
    if (document.location.search.indexOf("domconsole") >= 0) {
        window.console = new DOMConsole
    }
    var loader = new ResourceLoader(resources);
    loader.load(function(res) {
        R = res;
        if (Util.isTouchScreen()) {
            window.scrollTo(0, 1)
        }
        new Game(resizer)
    }, can);
    if (P.cocoon) {
        CocoonJS.App.setAppShouldFinishCallback(function() {
            return false
        })
    }
    var d = new Date(window.buildTimestamp * 1e3);
    console.log("Build: " + d)
});
function Game() {
    Game.instance = this;
    window.G = this;
    this.requestedUsername = false;
    this.curScreen = null ;
    this.currentTouches = [];
    this.framesMeasured = 0;
    this.timeMeasured = 0;
    this.state = new GameState(this);
    if (P.loadState)
        this.state.load();
    this.stage = new DisplayableContainer;
    this.soundManager = new SoundManager({
        volume: P.sound ? 1 : 0,
        sounds: []
    });
    this.setScreen(new LogoScreen(this), true);
    this.inputType = Util.isTouchScreen() ? "touch" : "mouse";
    cycleManager.init(this.cycle.bind(this));
    DOM.on(document.body, "touchstart mousedown", this.handleDownEvent.bind(this));
    DOM.on(document.body, "touchmove mousemove", this.handleMoveEvent.bind(this));
    DOM.on(document.body, "touchend mouseup", this.handleUpEvent.bind(this));
    DOM.on(document.body, "keydown", this.handleKeyDownEvent.bind(this));
    DOM.on(document.body, "keyup", this.handleKeyUpEvent.bind(this));
    DOM.on(document.body, "mousewheel DOMMouseScroll", this.handleWheelEvent.bind(this));
    this.kikInit();
    this.initAdManager();
    if (P.cocoon) {
        CocoonJS.App.setAppShouldFinishCallback(this.handleBackButton.bind(this))
    }
    if (Detect.isIOS()) {
        this.addToHome = addToHomescreen({
            autostart: false
        })
    }
    if (window.devicePixelRatio < 2) {
        this.setActualCanvasScale(.5)
    }
}
Game.prototype = {
    setScreen: function(screen, isMain) {
        this.closeOverlay();
        if (this.curScreen) {
            this.curScreen.destroy();
            TweenPool.clear()
        }
        this.curScreen = screen;
        this.curScreen.create();
        this.stage.clear();
        this.stage.addChild(this.curScreen.view);
        if (isMain) {
            Tracker.beginStage("screen-" + screen.getId())
        }
        window.getDimensionsAndResize()
    },
    setOverlay: function(overlay) {
        this.closeOverlay();
        this.curOverlay = overlay;
        this.curOverlay.create();
        this.stage.addChild(this.curOverlay.view);
        Tracker.beginStage("overlay-" + overlay.getId())
    },
    closeOverlay: function() {
        if (this.curOverlay) {
            this.curOverlay.view.remove();
            this.curOverlay.destroy();
            this.curOverlay = null 
        }
    },
    cycle: function(elapsed) {
        var before = Date.now();
        if (this.curOverlay)
            this.curOverlay.cycle(elapsed);
        this.curScreen.cycle(elapsed);
        TweenPool.cycle(elapsed);
        var between = Date.now();
        this.stage.doRender(ctx);
        var after = Date.now();
        if (P.showFrameRate) {
            ctx.textAlign = "left";
            ctx.fillStyle = "#ffffff";
            ctx.font = "9pt Arial";
            ctx.fillText("FPS: " + cycleManager.fps, 10, 20);
            ctx.fillText("Total: " + (after - before), 10, 40);
            ctx.fillText("Cycle: " + (between - before), 10, 60);
            ctx.fillText("Render: " + (after - between), 10, 80);
            ctx.fillText("Theoretical: " + Math.round(1e3 / Math.max(1, after - before)), 10, 100);
            ctx.fillText("Size: " + this.stage.leaves(), 10, 120)
        }
        if (!this.adaptedScaleWithPerformance) {
            this.timeMeasured += elapsed;
            this.framesMeasured++;
            if (this.timeMeasured > 8) {
                this.adaptedScaleWithPerformance = true;
                console.log("Framerate: " + this.framesMeasured / this.timeMeasured);
                if (this.framesMeasured / this.timeMeasured < 25) {
                    this.setActualCanvasScale(this.stage.scaleX / 2)
                }
            }
        }
    },
    getPosition: function(e) {
        if (!e.touches)
            e = {
                touches: [e]
            };
        var canRect = can.getBoundingClientRect();
        var scaleX = P.cocoon ? 1 : canRect.width / P.width;
        var scaleY = P.cocoon ? 1 : canRect.height / P.height;
        this.currentTouches = [];
        for (var i = 0; i < e.touches.length; i++) {
            this.currentTouches.push({
                x: (e.touches[i].clientX - canRect.left) / scaleX,
                y: (e.touches[i].clientY - canRect.top) / scaleY
            })
        }
        return this.currentTouches[this.currentTouches.length - 1]
    },
    handleDownEvent: function(e) {
        if (!InputManager.hasModalOpen()) {
            var evtType = e.type.indexOf("touch") >= 0 ? "touch" : "mouse";
            if (evtType != this.inputType)
                return;
            this.currentEvent = e;
            this.down = true;
            this.lastEvent = this.getPosition(e);
            (this.curOverlay || this.curScreen).touchStart(this.lastEvent.x, this.lastEvent.y);
            if (evtType == "touch") {
                e.preventDefault()
            }
        }
    },
    handleMoveEvent: function(e) {
        if (!InputManager.hasModalOpen()) {
            var evtType = e.type.indexOf("touch") >= 0 ? "touch" : "mouse";
            if (evtType != this.inputType)
                return;
            this.currentEvent = e;
            if (this.down) {
                this.lastEvent = this.getPosition(e);
                (this.curOverlay || this.curScreen).touchMove(this.lastEvent.x, this.lastEvent.y)
            }
            if (this.lastEvent) {
                var area = (this.curOverlay || this.curScreen).areaContains(this.lastEvent.x, this.lastEvent.y);
                if (!area) {
                    can.style.cursor = "default"
                } else {
                    can.style.cursor = area.cursor
                }
            }
            if (this.inputType == "touch") {
                e.preventDefault()
            }
        }
    },
    handleUpEvent: function(e) {
        if (!InputManager.hasModalOpen()) {
            var evtType = e.type.indexOf("touch") >= 0 ? "touch" : "mouse";
            if (evtType != this.inputType)
                return;
            this.currentEvent = e;
            if (this.down) {
                this.getPosition(e);
                (this.curOverlay || this.curScreen).touchEnd(this.lastEvent.x, this.lastEvent.y);
                this.down = this.currentTouches.length > 0;
                this.lastEvent = e
            }
        }
        window.scrollTo(0, 1)
    },
    handleKeyDownEvent: function(e) {
        (this.curOverlay || this.curScreen).keyDown(e.keyCode)
    },
    handleKeyUpEvent: function(e) {
        (this.curOverlay || this.curScreen).keyUp(e.keyCode)
    },
    handleWheelEvent: function(e) {
        var delta = Util.limit(e.wheelDelta || -e.detail, -1, 1);
        (this.curOverlay || this.curScreen).mouseWheel(delta)
    },
    handleBackButton: function() {
        (this.curOverlay || this.curScreen).backButton();
        return false
    },
    end: function () { // game end program here 

        var successparam = getParameterByName("Success");
        
        if (successparam == '')
        {
            if (this.curScreen.score >= 20)
                window.location.href = window.location.href + "?Success=1";
            else {
                //window.location.href = window.location.href + "?Success=0";
            }
        }
        else
        {
            if (this.curScreen.score >= 20)
            {
                window.location.href = updateQueryStringParameter(window.location.href,'Success','1');
            }
            else
            {
               // window.location.href = updateQueryStringParameter(window.location.href, 'Success', '0');
            }


        }

        var score = this.curScreen.score;
        var frames = this.curScreen.frames;
        var time = this.curScreen.time;
        this.lastRank = 0;
        this.previousHighscore = this.state.highscore;
        this.state.highscore = Math.max(this.state.highscore, score || 0);
        this.state.save();
        this.lastScore = score;
        this.setOverlay(new EndScreen(this));
        var me = this;
        this.requestUsername(function() {
			debugger;
            var stats = {
                name: me.state.username,
                frames: frames,
                score: score,
                check: Math.random() * 256,
                time: time
            };
            var encodedStats;
            if (!P.cocoon) {
                encodedStats = Encoder.encode(stats)
            } else {
                encodedStats = encodeURIComponent(Encoder.buildString(stats))
            }
            //Ajax.send(P.ajaxPrefix + P.leaderboardSubmitURL, P.cocoon ? "GET" : "POST", {
            //    params: encodedStats,
            //    format: "json",
            //    mode: P.cocoon ? "raw" : "encrypted"
            //}, function(code, content) {
            //    json = JSON.parse(content);
            //    me.lastRank = parseInt(json.rank);
            //    me.state.addAttemptId(parseInt(json.entity.id));
            //    me.state.save()
            //});
            if (!me.showedAddToHome && me.addToHome) {
                me.showedAddToHome = true;
                me.addToHome.show()
            }
        })
    },
    mainMenu: function() {
        this.setScreen(new GameplayScreen(this,{
            auto: true
        }));
        this.setOverlay(new MenuScreen(this))
    },
    newAttempt: function() {
        this.setScreen(new GameplayScreen(this, {}), true);

      
    },
    leaderboard: function() {
        this.setOverlay(new LeaderboardScreen(this))
    },
    openMoreGames: function() {
        if (window.kik && kik.send) {
            kik.open(P.moreGamesURL)
        } else if (P.cocoon) {
            CocoonJS.App.openURL(P.moreGamesURL)
        } else {
            window.originalOpen(P.moreGamesURL)
        }
    },
    kikInit: function() {
        if (window.kik) {
            if (kik.push && kik.push.handler) {
                kik.push.handler(function(data) {
                    Tracker.event("push-notification", "push-notification-open");
                    if (data.slug) {
                        Tracker.event("push-notification", "push-msg-" + data.slug)
                    }
                    if (data.date) {
                        var diff = (~~(+new Date / 1e3) - data.date) / 3600;
                        var chunkSize = 1;
                        var chunks = ~~(diff / chunkSize);
                        var hours = chunks * chunkSize;
                        Tracker.event("push-notification", "push-delay-" + hours + "-hours")
                    }
                })
            }
            if (kik.browser) {
                if (kik.browser.setOrientationLock) {
                    kik.browser.setOrientationLock("portrait")
                }
                if (kik.browser.statusBar) {
                    kik.browser.statusBar(false)
                }
                if (kik.browser.back) {
                    kik.browser.back(this.handleBackButton.bind(this))
                }
            }
            if (kik.metrics && kik.metrics.enableGoogleAnalytics) {
                kik.metrics.enableGoogleAnalytics()
            }
            if (kik.message) {
                Tracker.event("message", "message-open")
            }
            this.collectKikPushToken()
        }
    },
    collectKikPushToken: function() {
        if (!this.kikPushToken && !this.triedKikPushToken && window.kik && kik.push && kik.push.getToken) {
            this.triedKikPushToken = true;
            kik.push.getToken(this.kikPushTokenReceived.bind(this))
        }
    },
    kikPushTokenReceived: function(token) {
        if (token) {
            this.kikPushToken = token;
            var params = {
                token: this.kikPushToken,
                entity: "bounce"
            };
            var cbSuccess = function(code, content) {
                Tracker.event("token-save", "token-save-success")
            }
            ;
            var cbError = function(code, content) {
                Tracker.event("token-save", "token-save-error")
            }
            ;
            Ajax.send(P.kikTokenCollectURL, "post", params, cbSuccess, cbError)
        }
    },
    initAdManager: function() {
        this.adManager = null ;
        if (P.cocoon) {
            this.adManager = new CocoonJSAdManager(this)
        } else {
            this.adManager = new WebBannerAdManager(this)
        }
        if (this.adManager) {
            this.adManager.init()
        } else {
            console.log("No ads for this environment")
        }
        return;
        adContainerElement = document.getElementById("adContainer");
        adDisplayContainer = new google.ima.AdDisplayContainer(adContainerElement);
        adsLoader = new google.ima.AdsLoader(adDisplayContainer);
        var adsRequest = new google.ima.AdsRequest;
        adsRequest.adTagUrl = "http://pubads.g.doubleclick.net/gampad/ads?sz=640x360&iu=/6062/iab_vast_samples/skippable&ciu_szs=300x250,728x90&impl=s&gdfp_req=1&env=vp&output=xml_vast2&unviewed_position_start=1&url=[referrer_url]&correlator=[timestamp]";
        adsRequest.linearAdSlotWidth = 640;
        adsRequest.linearAdSlotHeight = 400;
        adsRequest.nonLinearAdSlotWidth = 640;
        adsRequest.nonLinearAdSlotHeight = 150;
        adsLoader.requestAds(adsRequest)
    },
    requestUsername: function(callback) {
        if (!this.state.username && !this.requestedUsername) {
            callback = callback || noop;
            var me = this;
            var cb = function(value) {
                me.state.username = value;
                me.state.save();
                callback.call()
            }
            ;
            InputManager.requestString({
                cancellable: true,
                message: R.string.modal.username,
                callback: cb
            });
            this.requestedUsername = true
        } else {
            setTimeout(callback, 1)
        }
    },
    setActualCanvasScale: function(scale) {
        can.width = P.width * scale;
        can.height = P.height * scale;
        this.stage.scaleX = this.stage.scaleY = scale
    },
    roundToScale: function(x) {
        var interv = 1 / this.stage.scaleX;
        return ~~(x / interv) * interv
    }
};
function GameState(game) {
    this.game = game;
    this.attempts = 0;
    this.username = null ;
    this.highscore = 0;
    this.attemptsIds = [];
    this.settings = {}
}
GameState.prototype = {
    addAttemptId: function(id) {
        if (parseInt(id)) {
            this.attemptsIds.push(parseInt(id))
        }
    },
    containsAttempt: function(id) {
        return this.attemptsIds.indexOf(parseInt(id)) >= 0
    },
    toJson: function() {
        return {
            attempts: this.attempts,
            username: this.username,
            highscore: this.highscore,
            attemptsIds: this.attemptsIds,
            settings: this.settings
        }
    },
    fromJson: function(json) {
        this.attempts = parseInt(json.attempts) || 0;
        this.username = json.username || null ;
        this.highscore = parseInt(json.highscore) || 0;
        this.attemptsIds = json.attemptsIds || [];
        this.settings = json.settings || []
    },
    save: function() {
        var json = this.toJson();
        var s = JSON.stringify(json);
        Util.storage.setItem(P.stateKey, s)
    },
    load: function() {
        var s = Util.storage.getItem(P.stateKey) || null ;
        if (s) {
            try {
                var json = JSON.parse(s);
                if (json)
                    this.fromJson(json)
            } catch (e) {
                console.log("Error while parsing game state data: " + s + " : " + e)
            }
        }
    }
};
(function() {
    var cache = {};
    var ctx = null 
      , usingWebAudio = true
      , noAudio = false;
    try {
        if (typeof AudioContext !== "undefined") {
            ctx = new AudioContext
        } else if (typeof webkitAudioContext !== "undefined") {
            ctx = new webkitAudioContext
        } else {
            usingWebAudio = false
        }
    } catch (e) {
        usingWebAudio = false
    }
    if (!usingWebAudio) {
        if (typeof Audio !== "undefined") {
            try {
                new Audio
            } catch (e) {
                noAudio = true
            }
        } else {
            noAudio = true
        }
    }
    if (usingWebAudio) {
        var masterGain = typeof ctx.createGain === "undefined" ? ctx.createGainNode() : ctx.createGain();
        masterGain.gain.value = 1;
        masterGain.connect(ctx.destination)
    }
    var HowlerGlobal = function() {
        this._volume = 1;
        this._muted = false;
        this.usingWebAudio = usingWebAudio;
        this.noAudio = noAudio;
        this._howls = []
    }
    ;
    HowlerGlobal.prototype = {
        volume: function(vol) {
            var self = this;
            vol = parseFloat(vol);
            if (vol >= 0 && vol <= 1) {
                self._volume = vol;
                if (usingWebAudio) {
                    masterGain.gain.value = vol
                }
                for (var key in self._howls) {
                    if (self._howls.hasOwnProperty(key) && self._howls[key]._webAudio === false) {
                        for (var i = 0; i < self._howls[key]._audioNode.length; i++) {
                            self._howls[key]._audioNode[i].volume = self._howls[key]._volume * self._volume
                        }
                    }
                }
                return self
            }
            return usingWebAudio ? masterGain.gain.value : self._volume
        },
        mute: function() {
            this._setMuted(true);
            return this
        },
        unmute: function() {
            this._setMuted(false);
            return this
        },
        _setMuted: function(muted) {
            var self = this;
            self._muted = muted;
            if (usingWebAudio) {
                masterGain.gain.value = muted ? 0 : self._volume
            }
            for (var key in self._howls) {
                if (self._howls.hasOwnProperty(key) && self._howls[key]._webAudio === false) {
                    for (var i = 0; i < self._howls[key]._audioNode.length; i++) {
                        self._howls[key]._audioNode[i].muted = muted
                    }
                }
            }
        }
    };
    var Howler = new HowlerGlobal;
    var audioTest = null ;
    if (!noAudio) {
        audioTest = new Audio;
        var codecs = {
            mp3: !!audioTest.canPlayType("audio/mpeg;").replace(/^no$/, ""),
            opus: !!audioTest.canPlayType('audio/ogg; codecs="opus"').replace(/^no$/, ""),
            ogg: !!audioTest.canPlayType('audio/ogg; codecs="vorbis"').replace(/^no$/, ""),
            wav: !!audioTest.canPlayType('audio/wav; codecs="1"').replace(/^no$/, ""),
            aac: !!audioTest.canPlayType("audio/aac;").replace(/^no$/, ""),
            m4a: !!(audioTest.canPlayType("audio/x-m4a;") || audioTest.canPlayType("audio/m4a;") || audioTest.canPlayType("audio/aac;")).replace(/^no$/, ""),
            mp4: !!(audioTest.canPlayType("audio/x-mp4;") || audioTest.canPlayType("audio/mp4;") || audioTest.canPlayType("audio/aac;")).replace(/^no$/, ""),
            weba: !!audioTest.canPlayType('audio/webm; codecs="vorbis"').replace(/^no$/, "")
        }
    }
    var Howl = function(o) {
        var self = this;
        self._autoplay = o.autoplay || false;
        self._buffer = o.buffer || false;
        self._duration = o.duration || 0;
        self._format = o.format || null ;
        self._loop = o.loop || false;
        self._loaded = false;
        self._sprite = o.sprite || {};
        self._src = o.src || "";
        self._pos3d = o.pos3d || [0, 0, -.5];
        self._volume = o.volume !== undefined ? o.volume : 1;
        self._urls = o.urls || [];
        self._rate = o.rate || 1;
        self._model = o.model || null ;
        self._onload = [o.onload || function() {}
        ];
        self._onloaderror = [o.onloaderror || function() {}
        ];
        self._onend = [o.onend || function() {}
        ];
        self._onpause = [o.onpause || function() {}
        ];
        self._onplay = [o.onplay || function() {}
        ];
        self._onendTimer = [];
        self._webAudio = usingWebAudio && !self._buffer;
        self._audioNode = [];
        if (self._webAudio) {
            self._setupAudioNode()
        }
        Howler._howls.push(self);
        self.load()
    }
    ;
    Howl.prototype = {
        load: function() {
            var self = this
              , url = null ;
            if (noAudio) {
                self.on("loaderror");
                return
            }
            for (var i = 0; i < self._urls.length; i++) {
                var ext, urlItem;
                if (self._format) {
                    ext = self._format
                } else {
                    urlItem = self._urls[i].toLowerCase().split("?")[0];
                    ext = urlItem.match(/.+\.([^?]+)(\?|$)/);
                    ext = ext && ext.length >= 2 ? ext : urlItem.match(/data\:audio\/([^?]+);/);
                    if (ext) {
                        ext = ext[1]
                    } else {
                        self.on("loaderror");
                        return
                    }
                }
                if (codecs[ext]) {
                    url = self._urls[i];
                    break
                }
            }
            if (!url) {
                self.on("loaderror");
                return
            }
            self._src = url;
            if (self._webAudio) {
                loadBuffer(self, url)
            } else {
                var newNode = new Audio;
                newNode.addEventListener("error", function() {
                    if (newNode.error && newNode.error.code === 4) {
                        HowlerGlobal.noAudio = true
                    }
                    self.on("loaderror", {
                        type: newNode.error ? newNode.error.code : 0
                    })
                }, false);
                self._audioNode.push(newNode);
                newNode.src = url;
                newNode._pos = 0;
                newNode.preload = "auto";
                newNode.volume = Howler._muted ? 0 : self._volume * Howler.volume();
                cache[url] = self;
                var listener = function() {
                    self._duration = Math.ceil(newNode.duration * 10) / 10;
                    if (Object.getOwnPropertyNames(self._sprite).length === 0) {
                        self._sprite = {
                            _default: [0, self._duration * 1e3]
                        }
                    }
                    if (!self._loaded) {
                        self._loaded = true;
                        self.on("load")
                    }
                    if (self._autoplay) {
                        self.play()
                    }
                    newNode.removeEventListener("canplaythrough", listener, false)
                }
                ;
                newNode.addEventListener("canplaythrough", listener, false);
                newNode.load()
            }
            return self
        },
        urls: function(urls) {
            var self = this;
            if (urls) {
                self.stop();
                self._urls = typeof urls === "string" ? [urls] : urls;
                self._loaded = false;
                self.load();
                return self
            } else {
                return self._urls
            }
        },
        play: function(sprite, callback) {
            var self = this;
            if (typeof sprite === "function") {
                callback = sprite
            }
            if (!sprite || typeof sprite === "function") {
                sprite = "_default"
            }
            if (!self._loaded) {
                self.on("load", function() {
                    self.play(sprite, callback)
                });
                return self
            }
            if (!self._sprite[sprite]) {
                if (typeof callback === "function")
                    callback();
                return self
            }
            self._inactiveNode(function(node) {
                node._sprite = sprite;
                var pos = node._pos > 0 ? node._pos : self._sprite[sprite][0] / 1e3;
                var duration = 0;
                if (self._webAudio) {
                    duration = self._sprite[sprite][1] / 1e3 - node._pos;
                    if (node._pos > 0) {
                        pos = self._sprite[sprite][0] / 1e3 + pos
                    }
                } else {
                    duration = self._sprite[sprite][1] / 1e3 - (pos - self._sprite[sprite][0] / 1e3)
                }
                var loop = !!(self._loop || self._sprite[sprite][2]);
                var soundId = typeof callback === "string" ? callback : Math.round(Date.now() * Math.random()) + "", timerId;
                (function() {
                    var data = {
                        id: soundId,
                        sprite: sprite,
                        loop: loop
                    };
                    timerId = setTimeout(function() {
                        if (!self._webAudio && loop) {
                            self.stop(data.id).play(sprite, data.id)
                        }
                        if (self._webAudio && !loop) {
                            self._nodeById(data.id).paused = true;
                            self._nodeById(data.id)._pos = 0
                        }
                        if (!self._webAudio && !loop) {
                            self.stop(data.id)
                        }
                        self.on("end", soundId)
                    }, duration * 1e3);
                    self._onendTimer.push({
                        timer: timerId,
                        id: data.id
                    })
                })();
                if (self._webAudio) {
                    var loopStart = self._sprite[sprite][0] / 1e3
                      , loopEnd = self._sprite[sprite][1] / 1e3;
                    node.id = soundId;
                    node.paused = false;
                    refreshBuffer(self, [loop, loopStart, loopEnd], soundId);
                    self._playStart = ctx.currentTime;
                    node.gain.value = self._volume;
                    if (typeof node.bufferSource.start === "undefined") {
                        node.bufferSource.noteGrainOn(0, pos, duration)
                    } else {
                        node.bufferSource.start(0, pos, duration)
                    }
                } else {
                    if (node.readyState === 4 || !node.readyState && navigator.isCocoonJS) {
                        node.readyState = 4;
                        node.id = soundId;
                        node.currentTime = pos;
                        node.muted = Howler._muted || node.muted;
                        node.volume = self._volume * Howler.volume();
                        setTimeout(function() {
                            node.play()
                        }, 0)
                    } else {
                        self._clearEndTimer(soundId);
                        (function() {
                            var sound = self
                              , playSprite = sprite
                              , fn = callback
                              , newNode = node;
                            var listener = function() {
                                sound.play(playSprite, fn);
                                newNode.removeEventListener("canplaythrough", listener, false)
                            }
                            ;
                            newNode.addEventListener("canplaythrough", listener, false)
                        })();
                        return self
                    }
                }
                self.on("play");
                if (typeof callback === "function")
                    callback(soundId);
                return self
            });
            return self
        },
        pause: function(id) {
            var self = this;
            if (!self._loaded) {
                self.on("play", function() {
                    self.pause(id)
                });
                return self
            }
            self._clearEndTimer(id);
            var activeNode = id ? self._nodeById(id) : self._activeNode();
            if (activeNode) {
                activeNode._pos = self.pos(null , id);
                if (self._webAudio) {
                    if (!activeNode.bufferSource || activeNode.paused) {
                        return self
                    }
                    activeNode.paused = true;
                    if (typeof activeNode.bufferSource.stop === "undefined") {
                        activeNode.bufferSource.noteOff(0)
                    } else {
                        activeNode.bufferSource.stop(0)
                    }
                } else {
                    activeNode.pause()
                }
            }
            self.on("pause");
            return self
        },
        stop: function(id) {
            var self = this;
            if (!self._loaded) {
                self.on("play", function() {
                    self.stop(id)
                });
                return self
            }
            self._clearEndTimer(id);
            var activeNode = id ? self._nodeById(id) : self._activeNode();
            if (activeNode) {
                activeNode._pos = 0;
                if (self._webAudio) {
                    if (!activeNode.bufferSource || activeNode.paused) {
                        return self
                    }
                    activeNode.paused = true;
                    if (typeof activeNode.bufferSource.stop === "undefined") {
                        activeNode.bufferSource.noteOff(0)
                    } else {
                        activeNode.bufferSource.stop(0)
                    }
                } else if (!isNaN(activeNode.duration)) {
                    activeNode.pause();
                    activeNode.currentTime = 0
                }
            }
            return self
        },
        mute: function(id) {
            var self = this;
            if (!self._loaded) {
                self.on("play", function() {
                    self.mute(id)
                });
                return self
            }
            var activeNode = id ? self._nodeById(id) : self._activeNode();
            if (activeNode) {
                if (self._webAudio) {
                    activeNode.gain.value = 0
                } else {
                    activeNode.muted = true
                }
            }
            return self
        },
        unmute: function(id) {
            var self = this;
            if (!self._loaded) {
                self.on("play", function() {
                    self.unmute(id)
                });
                return self
            }
            var activeNode = id ? self._nodeById(id) : self._activeNode();
            if (activeNode) {
                if (self._webAudio) {
                    activeNode.gain.value = self._volume
                } else {
                    activeNode.muted = false
                }
            }
            return self
        },
        volume: function(vol, id) {
            var self = this;
            vol = parseFloat(vol);
            if (vol >= 0 && vol <= 1) {
                self._volume = vol;
                if (!self._loaded) {
                    self.on("play", function() {
                        self.volume(vol, id)
                    });
                    return self
                }
                var activeNode = id ? self._nodeById(id) : self._activeNode();
                if (activeNode) {
                    if (self._webAudio) {
                        activeNode.gain.value = vol
                    } else {
                        activeNode.volume = vol * Howler.volume()
                    }
                }
                return self
            } else {
                return self._volume
            }
        },
        loop: function(loop) {
            var self = this;
            if (typeof loop === "boolean") {
                self._loop = loop;
                return self
            } else {
                return self._loop
            }
        },
        sprite: function(sprite) {
            var self = this;
            if (typeof sprite === "object") {
                self._sprite = sprite;
                return self
            } else {
                return self._sprite
            }
        },
        pos: function(pos, id) {
            var self = this;
            if (!self._loaded) {
                self.on("load", function() {
                    self.pos(pos)
                });
                return typeof pos === "number" ? self : self._pos || 0
            }
            pos = parseFloat(pos);
            var activeNode = id ? self._nodeById(id) : self._activeNode();
            if (activeNode) {
                if (pos >= 0) {
                    self.pause(id);
                    activeNode._pos = pos;
                    self.play(activeNode._sprite, id);
                    return self
                } else {
                    return self._webAudio ? activeNode._pos + (ctx.currentTime - self._playStart) : activeNode.currentTime
                }
            } else if (pos >= 0) {
                return self
            } else {
                for (var i = 0; i < self._audioNode.length; i++) {
                    if (self._audioNode[i].paused && self._audioNode[i].readyState === 4) {
                        return self._webAudio ? self._audioNode[i]._pos : self._audioNode[i].currentTime
                    }
                }
            }
        },
        pos3d: function(x, y, z, id) {
            var self = this;
            y = typeof y === "undefined" || !y ? 0 : y;
            z = typeof z === "undefined" || !z ? -.5 : z;
            if (!self._loaded) {
                self.on("play", function() {
                    self.pos3d(x, y, z, id)
                });
                return self
            }
            if (x >= 0 || x < 0) {
                if (self._webAudio) {
                    var activeNode = id ? self._nodeById(id) : self._activeNode();
                    if (activeNode) {
                        self._pos3d = [x, y, z];
                        activeNode.panner.setPosition(x, y, z);
                        activeNode.panner.panningModel = self._model || "HRTF"
                    }
                }
            } else {
                return self._pos3d
            }
            return self
        },
        fade: function(from, to, len, callback, id) {
            var self = this
              , diff = Math.abs(from - to)
              , dir = from > to ? "down" : "up"
              , steps = diff / .01
              , stepTime = len / steps;
            if (!self._loaded) {
                self.on("load", function() {
                    self.fade(from, to, len, callback, id)
                });
                return self
            }
            self.volume(from, id);
            for (var i = 1; i <= steps; i++) {
                (function() {
                    var change = self._volume + (dir === "up" ? .01 : -.01) * i
                      , vol = Math.round(1e3 * change) / 1e3
                      , toVol = to;
                    setTimeout(function() {
                        self.volume(vol, id);
                        if (vol === toVol) {
                            if (callback)
                                callback()
                        }
                    }, stepTime * i)
                })()
            }
        },
        fadeIn: function(to, len, callback) {
            return this.volume(0).play().fade(0, to, len, callback)
        },
        fadeOut: function(to, len, callback, id) {
            var self = this;
            return self.fade(self._volume, to, len, function() {
                if (callback)
                    callback();
                self.pause(id);
                self.on("end")
            }, id)
        },
        _nodeById: function(id) {
            var self = this
              , node = self._audioNode[0];
            for (var i = 0; i < self._audioNode.length; i++) {
                if (self._audioNode[i].id === id) {
                    node = self._audioNode[i];
                    break
                }
            }
            return node
        },
        _activeNode: function() {
            var self = this
              , node = null ;
            for (var i = 0; i < self._audioNode.length; i++) {
                if (!self._audioNode[i].paused) {
                    node = self._audioNode[i];
                    break
                }
            }
            self._drainPool();
            return node
        },
        _inactiveNode: function(callback) {
            var self = this
              , node = null ;
            for (var i = 0; i < self._audioNode.length; i++) {
                if (self._audioNode[i].paused && self._audioNode[i].readyState === 4) {
                    callback(self._audioNode[i]);
                    node = true;
                    break
                }
            }
            self._drainPool();
            if (node) {
                return
            }
            var newNode;
            if (self._webAudio) {
                newNode = self._setupAudioNode();
                callback(newNode)
            } else {
                self.load();
                newNode = self._audioNode[self._audioNode.length - 1];
                var listenerEvent = navigator.isCocoonJS ? "canplaythrough" : "loadedmetadata";
                var listener = function() {
                    newNode.removeEventListener(listenerEvent, listener, false);
                    callback(newNode)
                }
                ;
                newNode.addEventListener(listenerEvent, listener, false)
            }
        },
        _drainPool: function() {
            var self = this, inactive = 0, i;
            for (i = 0; i < self._audioNode.length; i++) {
                if (self._audioNode[i].paused) {
                    inactive++
                }
            }
            for (i = self._audioNode.length - 1; i >= 0; i--) {
                if (inactive <= 5) {
                    break
                }
                if (self._audioNode[i].paused) {
                    if (self._webAudio) {
                        self._audioNode[i].disconnect(0)
                    }
                    inactive--;
                    self._audioNode.splice(i, 1)
                }
            }
        },
        _clearEndTimer: function(soundId) {
            var self = this
              , index = 0;
            for (var i = 0; i < self._onendTimer.length; i++) {
                if (self._onendTimer[i].id === soundId) {
                    index = i;
                    break
                }
            }
            var timer = self._onendTimer[index];
            if (timer) {
                clearTimeout(timer.timer);
                self._onendTimer.splice(index, 1)
            }
        },
        _setupAudioNode: function() {
            var self = this
              , node = self._audioNode
              , index = self._audioNode.length;
            node[index] = typeof ctx.createGain === "undefined" ? ctx.createGainNode() : ctx.createGain();
            node[index].gain.value = self._volume;
            node[index].paused = true;
            node[index]._pos = 0;
            node[index].readyState = 4;
            node[index].connect(masterGain);
            node[index].panner = ctx.createPanner();
            node[index].panner.panningModel = self._model || "equalpower";
            node[index].panner.setPosition(self._pos3d[0], self._pos3d[1], self._pos3d[2]);
            node[index].panner.connect(node[index]);
            return node[index]
        },
        on: function(event, fn) {
            var self = this
              , events = self["_on" + event];
            if (typeof fn === "function") {
                events.push(fn)
            } else {
                for (var i = 0; i < events.length; i++) {
                    if (fn) {
                        events[i].call(self, fn)
                    } else {
                        events[i].call(self)
                    }
                }
            }
            return self
        },
        off: function(event, fn) {
            var self = this
              , events = self["_on" + event]
              , fnString = fn.toString();
            for (var i = 0; i < events.length; i++) {
                if (fnString === events[i].toString()) {
                    events.splice(i, 1);
                    break
                }
            }
            return self
        },
        unload: function() {
            var self = this;
            var nodes = self._audioNode;
            for (var i = 0; i < self._audioNode.length; i++) {
                if (!nodes[i].paused) {
                    self.stop(nodes[i].id)
                }
                if (!self._webAudio) {
                    nodes[i].src = ""
                } else {
                    nodes[i].disconnect(0)
                }
            }
            for (i = 0; i < self._onendTimer.length; i++) {
                clearTimeout(self._onendTimer[i].timer)
            }
            var index = Howler._howls.indexOf(self);
            if (index !== null  && index >= 0) {
                Howler._howls.splice(index, 1)
            }
            delete cache[self._src];
            self = null 
        }
    };
    if (usingWebAudio) {
        var loadBuffer = function(obj, url) {
            if (url in cache) {
                obj._duration = cache[url].duration;
                loadSound(obj)
            } else {
                var xhr = new XMLHttpRequest;
                xhr.open("GET", url, true);
                xhr.responseType = "arraybuffer";
                xhr.onload = function() {
                    ctx.decodeAudioData(xhr.response, function(buffer) {
                        if (buffer) {
                            cache[url] = buffer;
                            loadSound(obj, buffer)
                        }
                    }, function(err) {
                        obj.on("loaderror")
                    })
                }
                ;
                xhr.onerror = function() {
                    if (obj._webAudio) {
                        obj._buffer = true;
                        obj._webAudio = false;
                        obj._audioNode = [];
                        delete obj._gainNode;
                        obj.load()
                    }
                }
                ;
                try {
                    xhr.send()
                } catch (e) {
                    xhr.onerror()
                }
            }
        }
        ;
        var loadSound = function(obj, buffer) {
            obj._duration = buffer ? buffer.duration : obj._duration;
            if (Object.getOwnPropertyNames(obj._sprite).length === 0) {
                obj._sprite = {
                    _default: [0, obj._duration * 1e3]
                }
            }
            if (!obj._loaded) {
                obj._loaded = true;
                obj.on("load")
            }
            if (obj._autoplay) {
                obj.play()
            }
        }
        ;
        var refreshBuffer = function(obj, loop, id) {
            var node = obj._nodeById(id);
            node.bufferSource = ctx.createBufferSource();
            node.bufferSource.buffer = cache[obj._src];
            node.bufferSource.connect(node.panner);
            node.bufferSource.loop = loop[0];
            if (loop[0]) {
                node.bufferSource.loopStart = loop[1];
                node.bufferSource.loopEnd = loop[1] + loop[2]
            }
            node.bufferSource.playbackRate.value = obj._rate
        }
    }
    if (typeof define === "function" && define.amd) {
        define(function() {
            return {
                Howler: Howler,
                Howl: Howl
            }
        })
    }
    if (typeof exports !== "undefined") {
        exports.Howler = Howler;
        exports.Howl = Howl
    }
    if (typeof window !== "undefined") {
        window.Howler = Howler;
        window.Howl = Howl
    }
})();
function SoundManager(settings) {
    this.soundMap = {};
    this.loadSettings(settings)
}
SoundManager.prototype = {
    loadSettings: function(settings) {
        this.volume = isNaN(settings.volume) ? 1 : settings.volume;
        for (var i in settings.sounds) {
            this.soundMap[settings.sounds[i].id] = this.prepareSound(settings.sounds[i])
        }
    },
    prepareSound: function(settings) {
        return new Howl({
            urls: settings.urls,
            volume: (settings.volume || 1) * this.volume,
            loop: !!settings.loop,
            preload: true
        })
    },
    play: function(id) {
        if (this.soundMap[id]) {
            var soundObject = this.soundMap[id];
            this.soundMap[id].play(function(id) {
                soundObject.instanceId = id
            })
        }
    },
    stop: function(id) {
        if (this.soundMap[id]) {
            this.soundMap[id].stop(this.soundMap[id].instanceId)
        }
    },
    pause: function(id) {
        if (this.soundMap[id]) {
            this.soundMap[id].pause(this.soundMap[id].instanceId)
        }
    },
    fadeOut: function(id) {
        if (this.soundMap[id]) {
            this.soundMap[id].fadeOut(this.soundMap[id].instanceId)
        }
    }
};
(function() {
    if (!window.gaProperties) {
        console.warn("Google Analytics properties not set. Please setup window.gaProperties to enable tracking.")
    } else {
        if (!P.cocoon) {
            (function(i, s, o, g, r, a, m) {
                i["GoogleAnalyticsObject"] = r;
                i[r] = i[r] || function() {
                    (i[r].q = i[r].q || []).push(arguments)
                }
                ,
                i[r].l = 1 * new Date;
                a = s.createElement(o),
                m = s.getElementsByTagName(o)[0];
                a.async = 1;
                a.src = g;
                m.parentNode.insertBefore(a, m)
            })(window, document, "script", "//www.google-analytics.com/analytics.js", "gao")
        } else {
            var interfaceReady = false;
            var queue = [];
            var flushQueue = function() {
                var cmd;
                while (cmd = queue.shift()) {
                    forwardCmd(cmd)
                }
            }
            ;
            var forwardCmd = function(cmd) {
                CocoonJS.App.forwardAsync(cmd)
            }
            ;
            var addToQueue = function(cmd) {
                queue.push(cmd);
                if (interfaceReady) {
                    flushQueue()
                }
            }
            ;
            window.gaInterfaceIsReady = function() {
                console.log("Interface is ready");
                interfaceReady = true;
                flushQueue()
            }
            ;
            console.log("Creating GAI interface");
            CocoonJS.App.loadInTheWebView("http://www.tap2play.io/ga-proxy/index.html");
            window.gao = function() {
                var args = "";
                for (var i = 0; i < arguments.length; i++) {
                    if (i > 0) {
                        args += ","
                    }
                    args += JSON.stringify(arguments[i])
                }
                var cmd = "window.ga(" + args + ")";
                addToQueue(cmd)
            }
        }
        gao("require", "displayfeatures");
        for (var i = 0; i < window.gaProperties.length; i++) {
            gao("create", window.gaProperties[i].property, "none", {
                name: "tracker" + i
            })
        }
        window.ga = function() {
            var action = arguments[0], args, trackerAction, otherArgs = Array.prototype.slice.call(arguments, 1);
            for (var i = 0; i < window.gaProperties.length; i++) {
                trackerAction = "tracker" + i + "." + action;
                args = [trackerAction].concat(otherArgs);
                for (var j = 2; j < args.length; j++) {
                    if (typeof args[j] === "string") {
                        if (args[j].indexOf("/") == 0) {
                            args[j] = "/" + (window.gaProperties[i].prefix || "") + args[j].substr(1)
                        } else {
                            args[j] = (window.gaProperties[i].prefix || "") + args[j]
                        }
                    }
                }
                gao.apply(window, args)
            }
            clearInterval(hbInterval);
            hbInterval = setInterval(hb, 15e3)
        }
        ;
        var lastInteraction = 0;
        var interacted = function() {
            lastInteraction = Date.now()
        }
        ;
        var elts = [document, window];
        var evts = ["touchstart", "touchend", "mousedown", "mouseup", "keyup", "keydown"];
        for (var i in elts) {
            for (var j in evts) {
                elts[i].addEventListener(evts[j], interacted)
            }
        }
        var hb = function() {
            if (Date.now() - lastInteraction <= 1e4) {
                window.ga("send", "event", "hb", "hb-" + hbs++)
            }
        }
        ;
        var hbs = 0;
        var hbInterval = setInterval(hb, 15e3)
    }
})();
var Tracker = {
    event: function(eventCategory, eventLabel, eventValue) {
        if (window.ga) {
            ga("send", "event", "gameevent", eventCategory, eventLabel, eventValue)
        }
        if (P.clay) {
            Clay.ready(function() {
                Clay.Stats.logStat({
                    name: eventLabel,
                    quantity: eventValue || 1
                })
            })
        }
    },
    beginStage: function(stageLabel) {
        if (window.ga) {
            ga("send", "pageview", "/stage-" + stageLabel)
        }
        if (P.clay) {
            Clay.ready(function() {
                Clay.Stats.level({
                    action: "start",
                    level: stageLabel
                })
            })
        }
    }
};
var InputManager = {
    isOpen: false,
    modal: null ,
    label: null ,
    input: null ,
    cancel: null ,
    button: null ,
    currentCallBack: null ,
    currentValidation: null ,
    requestString: function(settings) {
        settings = settings || {};
        this.currentSettings = settings || {};
        if (!P.cocoon) {
            this.createModal();
            this.showModal();
            this.label.innerHTML = this.currentSettings.message || "";
            this.button.innerHTML = this.currentSettings.button || "OK";
            this.cancel.style.display = this.currentSettings.cancellable ? "inline-block" : "none";
            this.input.value = this.currentSettings.defaultValue || "";
            this.input.select()
        } else {
            this.initCocoon();
            CocoonJS.App.showTextDialog("", this.currentSettings.message, this.currentSettings.defaultValue || "", CocoonJS.App.KeyboardType.TEXT, "Cancel", this.currentSettings.button || "OK")
        }
        this.currentCallBack = this.currentSettings.callback || noop;
        this.currentValidation = this.currentSettings.validation || function() {
            return true
        }
    },
    initCocoon: function() {
        if (!this.cocoonInitted) {
            this.cocoonInitted = true;
            CocoonJS.App.onTextDialogFinished.addEventListener(this.cocoonSubmitted.bind(this));
            CocoonJS.App.onTextDialogCancelled.addEventListener(this.cocoonCancelled.bind(this))
        }
    },
    createModal: function() {
        if (!this.modal) {
            this.modal = document.createElement("div");
            document.body.appendChild(this.modal);
            DOM.applyStyle(this.modal, {
                width: "100%",
                height: "100%",
                backgroundColor: "black",
                position: "absolute",
                display: "none",
                top: 0,
                left: 0
            });
            var table = document.createElement("div");
            this.modal.appendChild(table);
            DOM.applyStyle(table, {
                width: "100%",
                height: "100%",
                display: "table"
            });
            var cell = document.createElement("div");
            table.appendChild(cell);
            DOM.applyStyle(cell, {
                display: "table-cell",
                verticalAlign: "top",
                paddingTop: "10px"
            });
            this.label = document.createElement("span");
            cell.appendChild(this.label);
            DOM.applyStyle(this.label, {
                display: "block",
                color: "white",
                fontSize: "1.5em",
                textAlign: "center",
                marginBottom: "15px"
            });
            this.input = document.createElement("input");
            this.input.type = "text";
            this.input.setAttribute("maxlength", 15);
            cell.appendChild(this.input);
            DOM.applyStyle(this.input, {
                width: "90%",
                margin: "auto",
                display: "block",
                height: "40px",
                fontSize: "2em",
                maxWidth: "300px",
                textAlign: "center"
            });
            var me = this;
            DOM.on(this.input, "keyup", function(e) {
                if (e.keyCode == 13) {
                    me.inputSubmitted()
                }
            });
            var buttonsContainer = document.createElement("div");
            cell.appendChild(buttonsContainer);
            DOM.applyStyle(buttonsContainer, {
                textAlign: "center"
            });
            this.button = document.createElement("button");
            this.button.innerHTML = "OK";
            DOM.on(this.button, "click", this.inputSubmitted.bind(this));
            buttonsContainer.appendChild(this.button);
            DOM.applyStyle(this.button, {
                width: "30%",
                margin: "5px",
                marginTop: "10px",
                display: "inline-block",
                height: "40px",
                fontSize: "1.5em",
                backgroundColor: "black",
                border: "2px solid white",
                fontFamily: "ComfortaaRegular",
                cursor: "pointer",
                maxWidth: "150px",
                color: "white",
                borderRadius: "10px"
            });
            this.cancel = document.createElement("button");
            this.cancel.innerHTML = "CANCEL";
            DOM.on(this.cancel, "click", this.cancelled.bind(this));
            buttonsContainer.appendChild(this.cancel);
            DOM.applyStyle(this.cancel, {
                width: "30%",
                margin: "5px",
                marginTop: "10px",
                display: "inline-block",
                height: "40px",
                fontSize: "1.5em",
                backgroundColor: "black",
                border: "2px solid white",
                fontFamily: "ComfortaaRegular",
                cursor: "pointer",
                maxWidth: "150px",
                color: "white",
                borderRadius: "10px"
            })
        }
    },
    showModal: function() {
        this.isOpen = true;
        this.modal.style.display = "block"
    },
    hideModal: function() {
        this.isOpen = false;
        this.input.blur();
        var d = Util.isTouchScreen() ? 250 : 0;
        var me = this;
        setTimeout(function() {
            me.input.blur();
            me.button.focus()
        }, d);
        setTimeout(function() {
            me.modal.style.display = "none"
        }, d * 2)
    },
    inputSubmitted: function() {
        if (this.currentValidation(this.input.value)) {
            this.hideModal();
            this.currentCallBack.call(this, this.input.value)
        }
    },
    cancelled: function() {
        this.hideModal();
        this.currentCallBack.call(this, null )
    },
    cocoonSubmitted: function(value) {
        if (this.currentValidation(value)) {
            this.currentCallBack.call(this, value)
        } else {
            this.requestString(this.currentSettings)
        }
    },
    cocoonCancelled: function() {
        this.currentCallBack.call(this, null )
    },
    hasModalOpen: function() {
        return this.isOpen
    }
};
function LogoScreen(game) {
    Screen.call(this, game)
}
LogoScreen.prototype = extendPrototype(Screen, {
    getId: function() {
        return "logo"
    },
    create: function() {
        this.view = new DisplayableContainer;
        this.logo = new DisplayableImage;
        this.logo.image = R.image.logo_tap2play;
        this.logo.scaleX = 1 / (this.logo.image.width / P.width);
        this.logo.scaleY = 1 / (this.logo.image.height / P.height);
        this.view.addChild(this.logo);
        this.fade = new DisplayableRectangle;
        this.fade.color = "#000";
        this.fade.width = P.width;
        this.fade.height = P.height;
        this.view.addChild(this.fade);
        TweenPool.add(new Tween(this.fade,"alpha",1,0,1,0,function() {
            this.object.remove()
        }
        ));
        setTimeout(this.game.mainMenu.bind(this.game), window.location.search.indexOf("nocredit") >= 0 ? 0 : 3500)
    }
});
function MenuScreen(game) {
    Screen.call(this, game)
}
MenuScreen.prototype = extendPrototype(Screen, {
    getId: function() {
        return "menu"
    },
    create: function() {
        this.view = new DisplayableContainer;
        this.moreGames = null;
       // this.moreGames.x = P.width - this.moreGames.width;
//this.view.addChild(this.moreGames);
      //  this.addArea(this.moreGames);
        this.logo = new DisplayableTextField;
        with (this.logo) {
            text = "Bounce";
            color = "#ffffff";
            textAlign = "center";
            textBaseline = "middle";
            font = "90pt ComfortaaRegular";
            x = P.width / 2;
            y = P.height * .25;
            shadowColor = "#000";
            shadowOffsetX = 4;
            shadowOffsetY = 4
        }
        this.view.addChild(this.logo);
        this.playButton = new Button({
            content: R.string.menu.play,
            bgColor: "black",
            borderColor: "black",
            fontColor: "white",
            action: this.play.bind(this)
        });
        this.playButton.x = (P.width - this.playButton.width) / 2;
        this.playButton.y = 650;
        this.view.addChild(this.playButton);
        this.addArea(this.playButton);
        this.leaderboardButton = null;
  //      this.leaderboardButton.x = this.playButton.x;
    //    this.leaderboardButton.y = this.playButton.y + this.playButton.height + 25;
    //    this.view.addChild(this.leaderboardButton);
     //   this.addArea(this.leaderboardButton)
    },
    play: function() {
        this.game.newAttempt()
    },
    leaderboard: function() {
        this.game.leaderboard()
    }
});
function EndScreen(game) {
    Screen.call(this, game)
}
EndScreen.prototype = extendPrototype(Screen, {
    getId: function() {
        return "end"
    },
    create: function() {
        this.view = new DisplayableContainer;
        this.moreGames = new Button({
            content: R.image.more,
            bgColor: "rgba(0,0,0,0)",
            borderColor: "rgba(0,0,0,0)",
            width: R.image.more.width,
            height: R.image.more.height,
            action: this.game.openMoreGames.bind(this.game)
        });
        this.moreGames.x = P.width - this.moreGames.width;
        this.view.addChild(this.moreGames);
        this.addArea(this.moreGames);
        this.scoreLabel = new DisplayableTextField;
        with (this.scoreLabel) {
            text = R.string.end.score;
            color = "#ffffff";
            textAlign = "center";
            textBaseline = "middle";
            font = "60pt ComfortaaRegular";
            x = P.width / 2;
            y = 150;
            shadowColor = "#000";
            shadowOffsetX = 4;
            shadowOffsetY = 4
        }
        this.view.addChild(this.scoreLabel);
        this.scoreTf = new DisplayableTextField;
        with (this.scoreTf) {
            text = this.game.lastScore.toString();
            color = "#ffffff";
            textAlign = "center";
            textBaseline = "middle";
            font = "90pt ComfortaaRegular";
            x = P.width / 2;
            y = this.scoreLabel.y + 130;
            shadowColor = "#000";
            shadowOffsetX = 4;
            shadowOffsetY = 4
        }
        this.view.addChild(this.scoreTf);
        this.bestLabel = new DisplayableTextField;
        with (this.bestLabel) {
            text = R.string.end.best;
            color = "#ffffff";
            textAlign = "center";
            textBaseline = "middle";
            font = "30pt ComfortaaRegular";
            x = ~~(P.width / 3);
            y = this.scoreTf.y + 100;
            shadowColor = "#000";
            shadowOffsetX = 4;
            shadowOffsetY = 4
        }
        this.view.addChild(this.bestLabel);
        this.bestTf = new DisplayableTextField;
        with (this.bestTf) {
            text = this.game.state.highscore.toString();
            color = "#ffffff";
            textAlign = "center";
            textBaseline = "middle";
            font = "30pt ComfortaaRegular";
            x = this.bestLabel.x;
            y = this.bestLabel.y + 70;
            shadowColor = "#000";
            shadowOffsetX = 4;
            shadowOffsetY = 4
        }
        this.view.addChild(this.bestTf);
        this.rankLabel = new DisplayableTextField;
        with (this.rankLabel) {
            text = R.string.end.rank;
            color = "#ffffff";
            textAlign = "center";
            textBaseline = "middle";
            font = this.bestLabel.font;
            x = ~~(P.width * 2 / 3);
            y = this.bestLabel.y;
            shadowColor = "#000";
            shadowOffsetX = 4;
            shadowOffsetY = 4
        }
        this.view.addChild(this.rankLabel);
        this.rankTf = new DisplayableTextField;
        with (this.rankTf) {
            text = R.string.end.loading;
            color = "#ffffff";
            textAlign = "center";
            textBaseline = "middle";
            font = this.bestTf.font;
            x = this.rankLabel.x;
            y = this.bestTf.y;
            shadowColor = "#000";
            shadowOffsetX = 4;
            shadowOffsetY = 4
        }
        this.view.addChild(this.rankTf);
        var buttons = [];
        buttons.push(this.retryButton = new Button({
            content: "Try again",
            bgColor: "black",
            borderColor: "#ffffff",
            fontColor: "white",
            action: this.retry.bind(this)
        }));
        buttons.push(this.leaderboardButton = new Button({
            content: R.string.menu.leaderboard,
            bgColor: "black",
            borderColor: "white",
            fontColor: "white",
            action: this.leaderboard.bind(this)
        }));
        if (window.kik && kik.send) {
            buttons.push(this.kikButton = new Button({
                content: R.image.kik,
                bgColor: "black",
                borderColor: "#ffffff",
                fontColor: "white",
                action: this.kik.bind(this),
                id: "kik"
            }))
        }
        if (window.Clay) {
            buttons.push(this.kikButton = new Button({
                content: R.string.end.share,
                bgColor: "black",
                borderColor: "#ffffff",
                fontColor: "white",
                action: this.clayShare.bind(this)
            }))
        }
        var nextY = 550;
        for (var i in buttons) {
            buttons[i].x = (P.width - buttons[i].width) / 2;
            buttons[i].y = nextY;
            nextY += buttons[i].height + 25;
            this.view.addChild(buttons[i]);
            this.addArea(buttons[i])
        }
        if (this.game.lastScore > this.game.previousHighscore) {
            TweenPool.add(new Interpolation({
                object: this.bestLabel,
                property: "color",
                from: "#ff0000",
                to: "#ffffff",
                duration: 1,
                applyFunction: ColorUtils.easingApply,
                onFinish: function() {
                    this.invert()
                }
            }));
            TweenPool.add(new Interpolation({
                object: this.bestTf,
                property: "color",
                from: "#ff0000",
                to: "#ffffff",
                duration: 1,
                applyFunction: ColorUtils.easingApply,
                onFinish: function() {
                    this.invert()
                }
            }));
            TweenPool.add(new Interpolation({
                object: this.scoreLabel,
                property: "color",
                from: "#ff0000",
                to: "#ffffff",
                duration: 1,
                applyFunction: ColorUtils.easingApply,
                onFinish: function() {
                    this.invert()
                }
            }));
            TweenPool.add(new Interpolation({
                object: this.scoreTf,
                property: "color",
                from: "#ff0000",
                to: "#ffffff",
                duration: 1,
                applyFunction: ColorUtils.easingApply,
                onFinish: function() {
                    this.invert()
                }
            }))
        }
    },
    retry: function() {
        this.game.newAttempt()
    },
    kik: function() {
        kik.send({
            title: "I scored " + this.game.lastScore + " on Bounce!",
            text: "Try to beat me!",
            pic: "promo/icon-256x256.png"
        })
    },
    leaderboard: function() {
        this.game.leaderboard()
    },
    cycle: function(e) {
        if (this.game.lastRank) {
            this.rankTf.text = "#" + this.game.lastRank
        }
    },
    backButton: function() {
        this.game.mainMenu()
    },
    clayShare: function() {
        Clay("client.share.any", {
            text: "I scored " + this.game.lastScore + " on Bounce! Play at http://bounce.tap2play.io/"
        })
    }
});
function LeaderboardScreen(game) {
    Screen.call(this, game);
    this.lines = 30
}
LeaderboardScreen.prototype = extendPrototype(Screen, {
    getId: function() {
        return "leaderboard"
    },
    create: function() {
        this.view = new DisplayableContainer;
        this.title = new DisplayableTextField;
        this.view.addChild(this.title);
        with (this.title) {
            x = P.width / 2;
            y = 70;
            textAlign = "center";
            textBaseline = "middle";
            color = "#ffffff";
            text = R.string.leaderboard.title;
            font = "40pt ComfortaaBold"
        }
        this.scrollContent = new ScrollableContainer;
        this.scrollContent.width = P.width * .9;
        this.scrollContent.height = 570;
        this.scrollContent.borderWidth = 4;
        this.scrollContent.borderColor = "#ffffff";
        this.scrollContent.y = 180;
        this.scrollContent.x = (P.width - this.scrollContent.width) / 2;
        this.halo = new DisplayableRectangle;
        this.halo.color = "#000";
        this.halo.width = this.scrollContent.width;
        this.halo.height = this.scrollContent.height;
        this.halo.alpha = .5;
        this.halo.x = this.scrollContent.x;
        this.halo.y = this.scrollContent.y;
        this.view.addChild(this.halo);
        this.view.addChild(this.scrollContent);
        var scrollableArea = this.scrollContent.getScrollableArea();
        this.addArea(scrollableArea);
        this.content = new CachedContainer;
        this.content.width = this.scrollContent.width;
        var tabs = [{
            type: "all",
            label: R.string.leaderboard.all
        }, {
            type: "today",
            label: R.string.leaderboard.today
        }, {
            type: "week",
            label: R.string.leaderboard.week
        }, {
            type: "month",
            label: R.string.leaderboard.month
        }];
        var tabTf, tabArea;
        this.tabTfs = [];
        for (var i = 0; i < tabs.length; i++) {
            tabTf = new DisplayableTextField;
            this.view.addChild(tabTf);
            with (tabTf) {
                text = tabs[i].label;
                x = (i + 1) / (tabs.length + 1) * P.width;
                y = 140;
                textAlign = "center";
                textBaseline = "middle";
                color = "#ffffff";
                font = "20pt ComfortaaRegular"
            }
            this.tabTfs.push(tabTf);
            tabArea = new Area({
                x: i * (P.width / tabs.length),
                y: tabTf.y - 30,
                width: P.width / tabs.length,
                height: 60,
                actionPerformed: function(screen, data, tf) {
                    return function() {
                        screen.selectTab(data, tf)
                    }
                }(this, tabs[i], tabTf)
            });
            this.addArea(tabArea)
        }
        this.menuButton = new Button({
            content: R.string.leaderboard.back,
            width: 300,
            fontSize: 35,
            bgColor: "black",
            borderColor: "white",
            fontColor: "white",
            action: this.back.bind(this)
        });
        this.menuButton.x = (P.width - this.menuButton.width) / 2;
        this.menuButton.y = this.scrollContent.y + this.scrollContent.height + 30;
        this.view.addChild(this.menuButton);
        this.addArea(this.menuButton);
        this.nextButton = new Button({
            content: R.string.leaderboard.next,
            width: 100,
            bgColor: "black",
            borderColor: "white",
            fontColor: "white",
            action: this.next.bind(this)
        });
        this.nextButton.x = P.width - this.nextButton.width - 40;
        this.nextButton.y = this.scrollContent.y + this.scrollContent.height + 30;
        this.view.addChild(this.nextButton);
        this.addArea(this.nextButton);
        this.previousButton = new Button({
            content: R.string.leaderboard.previous,
            width: 100,
            bgColor: "black",
            borderColor: "white",
            fontColor: "white",
            action: this.previous.bind(this)
        });
        this.previousButton.x = 40;
        this.previousButton.y = this.scrollContent.y + this.scrollContent.height + 30;
        this.view.addChild(this.previousButton);
        this.addArea(this.previousButton);
        this.selectTab(tabs[0], this.tabTfs[0])
    },
    back: function() {
        this.game.mainMenu()
    },
    mouseWheel: function(delta) {
        this.scrollContent.scrollTo(0, this.scrollContent.scrollY - delta * 10)
    },
    selectTab: function(tabData, tf) {
        this.type = tabData.type;
        this.page = 0;
        for (var i in this.tabTfs) {
            this.tabTfs[i].alpha = this.tabTfs[i] == tf ? 1 : .5
        }
        this.loadContent()
    },
    loadContent: function() {
        var params = {
            format: "json",
            page: this.page,
            size: this.lines,
            period: this.type
        };
        var me = this;
        var cbOk = function(code, content) {
            me.contentLoaded(content)
        }
        ;
        var cbError = function() {
            alert("Error :(")
        }
        ;
        Ajax.send(P.ajaxPrefix + P.leaderboardGetURL, "GET", params, cbOk, cbError);
        this.setContent([]);
        this.previousButton.enabled = false;
        this.nextButton.enabled = false
    },
    contentLoaded: function(content) {
        var json;
        try {
            json = JSON.parse(content);
            this.setContent(json)
        } catch (e) {
            this.setContent([])
        }
    },
    setContent: function(json) {
        this.content.clear();
        this.itemViews = [];
        this.previousButton.enabled = this.page > 0;
        this.nextButton.enabled = !!json[this.lines - 1];
        var nextY = 0, lineHeight = 40, rankTf, nickTf, scoreTf;
        for (var i = 0; i < this.lines; i++) {
            rankTf = new DisplayableTextField;
            this.content.addChild(rankTf);
            with (rankTf) {
                color = "#ffffff";
                font = "20pt Arial";
                textBaseline = "middle";
                textAlign = "right";
                x = 80;
                y = nextY + lineHeight / 2
            }
            nickTf = new DisplayableTextField;
            this.content.addChild(nickTf);
            with (nickTf) {
                color = "#ffffff";
                font = "20pt Arial";
                textBaseline = "middle";
                textAlign = "left";
                x = 100;
                y = nextY + lineHeight / 2
            }
            scoreTf = new DisplayableTextField;
            this.content.addChild(scoreTf);
            with (scoreTf) {
                color = "#ffffff";
                font = "20pt Arial";
                textBaseline = "middle";
                textAlign = "right";
                x = this.content.width - 40;
                y = nextY + lineHeight / 2
            }
            if (json[i]) {
                rankTf.text = json[i].rank.toString();
                nickTf.text = (json[i].name || "").substr(0, 15);
                scoreTf.text = json[i].score.toString();
                if (this.game.state.containsAttempt(json[i].id)) {
                    rankTf.color = nickTf.color = scoreTf.color = "#ff0000"
                }
            } else {
                rankTf.text = "-";
                nickTf.text = "-";
                scoreTf.text = "-"
            }
            nextY += lineHeight
        }
        this.content.height = nextY;
        this.content.renewCache();
        this.scrollContent.setContent(this.content, this.content.width, this.content.height);
        this.scrollContent.scrollTo(0, 0)
    },
    next: function() {
        this.page++;
        this.loadContent()
    },
    previous: function() {
        this.page--;
        this.loadContent()
    },
    backButton: function() {
        this.back()
    }
});
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};
function GameplayScreen(game, settings) {
	
    Screen.call(this, game);
	if (getUrlParameter('Success') == 1) {
		
		  	if (getUrlParameter('isCalledFrom') == "Sencha") {
			  parent.CallSenchaEvent();
			}
      }
    this.settings = settings || {}
}
GameplayScreen.prototype = extendPrototype(Screen, {
    getId: function() {
        return "gameplay"
    },
    create: function() {
        this.view = new DisplayableContainer;
        this.downKeys = {};
        this.bg = new DisplayableRectangle;
        this.bg.color = "#000";
        this.bg.width = P.width;
        this.bg.height = P.height;
        this.view.addChild(this.bg);
        this.divisions = 8;
        this.divisionsArray = [];
        var d;
        for (var i = 0; i < this.divisions; i++) {
            d = new Platform(this);
            d.width = P.width / this.divisions;
            d.x = i * P.width / this.divisions;
            this.view.addChild(d);
            this.divisionsArray.push(d)
        }
        this.ball = new Ball(this);
        this.view.addChild(this.ball);
        this.score = 0;
        this.bounces = 0;
        if (!this.settings.auto) {
            this.isInTutorial = true;
            this.moveInstructions = new MultilineTextField;
            this.view.addChild(this.moveInstructions);
            with (this.moveInstructions) {
                x = P.width / 2;
                y = P.height * .25;
                textAlign = "center";
                textBaseline = "middle";
                font = "60pt ComfortaaRegular";
                color = "white";
                text = "Hold left or right to move the ball";
                maxWidth = P.width;
                lineHeight = 100
            }
            TweenPool.add(new Interpolation({
                object: this.moveInstructions,
                property: "y",
                from: -P.width,
                to: this.moveInstructions.y,
                duration: 1,
                easing: Math.easeOutBack
            }));
            this.arrowLeft = new DisplayableImage;
            this.arrowLeft.image = R.raw.arrow;
            this.arrowLeft.x = 50;
            this.arrowLeft.y = P.height - this.arrowLeft.image.height - 50;
            this.view.addChild(this.arrowLeft);
            this.arrowRight = new DisplayableImage;
            this.arrowRight.image = R.raw.arrow;
            this.arrowRight.x = P.width - 50;
            this.arrowRight.y = P.height - this.arrowRight.image.height - 50;
            this.arrowRight.scaleX = -1;
            this.view.addChild(this.arrowRight)
        }
        this.scoreTf = new DisplayableTextField;
        this.view.addChild(this.scoreTf);
        with (this.scoreTf) {
            x = P.width / 2;
            y = P.height * .2;
            textAlign = "center";
            textBaseline = "middle";
            font = "120pt ComfortaaRegular";
            color = "white";
            text = "0";
            maxWidth = P.width;
            lineHeight = 100;
            alpha = .5
        }
        this.scoreTf.visible = false;
        this.changeDivisions();
        this.launched = true;
        this.time = 0;
        this.frames = 0
    },
    changeDivisions: function() {
        var activated = [];
        for (var i in this.divisionsArray) {
            activated.push(true)
        }
        var deactivated = Math.min(this.divisionsArray.length - 2, (this.bounces - 2) / 5);
        for (var i = 0; i < deactivated; i++) {
            activated[~~(Math.random() * activated.length)] = false
        }
        for (var i in this.divisionsArray) {
            if (activated[i] || this.settings.auto || this.isInTutorial) {
                this.divisionsArray[i].activate()
            } else {
                this.divisionsArray[i].deactivate()
            }
        }
    },
    cycle: function(e) {
        this.ball.direction = 0;
        if (!this.settings.auto) {
            if (this.downKeys[37]) {
                this.ball.direction = -1
            } else if (this.downKeys[39]) {
                this.ball.direction = 1
            }
            if (Util.isTouchScreen()) {
                for (var i in this.game.currentTouches) {
                    if (this.game.currentTouches[i].x < P.width / 2) {
                        this.ball.direction = -1
                    } else {
                        this.ball.direction = 1
                    }
                }
            }
            this.arrowLeft.alpha = this.ball.direction < 0 ? 1 : .5;
            this.arrowRight.alpha = this.ball.direction > 0 ? 1 : .5
        }
        this.ball.cycle(e);
        if (this.isInTutorial && this.ball.maxMovement > P.width / 4) {
            this.isInTutorial = false;
            TweenPool.add(new Interpolation({
                object: this.moveInstructions,
                property: "y",
                from: this.moveInstructions.y,
                to: -P.height,
                duration: 1,
                easing: Math.easeInBack,
                onFinish: function() {
                    this.object.remove()
                }
            }));
            var me = this;
            this.fallInstructions = new MultilineTextField;
            this.view.addChild(this.fallInstructions);
            with (this.fallInstructions) {
                x = P.width / 2;
                y = P.height * .25;
                textAlign = "center";
                textBaseline = "middle";
                font = "60pt ComfortaaRegular";
                color = "white";
                text = "Now try not to fall down!";
                maxWidth = P.width;
                lineHeight = 100
            }
            TweenPool.add(new Interpolation({
                object: this.fallInstructions,
                property: "y",
                from: -P.height,
                to: this.fallInstructions.y,
                duration: 1,
                easing: Math.easeOutBack,
                delay: 1,
                onFinish: function() {
                    TweenPool.add(new Interpolation({
                        object: this.object,
                        property: "y",
                        from: this.object.y,
                        to: -P.height,
                        duration: 1,
                        easing: Math.easeInBack,
                        delay: 2,
                        onFinish: function() {
                            this.object.remove();
                            me.launched = true;
                            me.score = 0;
                            me.scoreTf.text = "0";
                            me.scoreTf.visible = true;
                            TweenPool.add(new Tween(me.scoreTf,"alpha",0,me.scoreTf.alpha,1))
                        }
                    }))
                }
            }))
        }
        this.view.x = this.view.y = 0;
        if (this.shakeTime > 0) {
            this.shakeTime -= e;
            this.view.x = ~~Util.rand(-10, 10);
            this.view.y = ~~Util.rand(-10, 10)
        }
        this.time += e;
        this.frames++
    },
    bounced: function() {
        this.changeDivisions();
        if (!this.isInTutorial) {
            this.bounces++;
            if (this.launched) {
                this.score++
            }
            this.scoreTf.text = this.score.toString();
            TweenPool.add(new Tween(this.scoreTf,"alpha",1,.5,.2))
        }
    },
    columnPlatform: function(x) {
        var w = P.width / this.divisions;
        return this.divisionsArray[~~(x / w)]
    },
    keyDown: function(k) {
        this.downKeys[k] = true
    },
    keyUp: function(k) {
        this.downKeys[k] = false
    },
    gameOver: function() {
        if (!this.ended) {
            this.ended = true;
            this.scoreTf.remove();
            setTimeout(this.game.end.bind(this.game), 1e3);
            this.shake(.3);
            navigator.vibrate(500);
            this.arrowLeft.remove();
            this.arrowRight.remove()
        }
    },
    shake: function(d) {
        this.shakeTime = d
    }
});
function Platform(screen) {
    DisplayableRectangle.call(this);
    this.screen = screen;
    this.color = "#ffffff";
    this.height = P.height - Platform.ACTIVATE_Y;
    this.y = Platform.ACTIVATE_Y
}
Platform.ACTIVATE_Y = ~~(P.height * .666);
Platform.prototype = extendPrototype(DisplayableRectangle, {
    activate: function() {
        TweenPool.add(new Interpolation({
            object: this,
            property: "y",
            from: this.y,
            to: Platform.ACTIVATE_Y,
            duration: .3
        }))
    },
    deactivate: function() {
        TweenPool.add(new Interpolation({
            object: this,
            property: "y",
            from: this.y,
            to: P.height,
            duration: .3
        }))
    }
});
function Ball(screen) {
    DisplayableObject.call(this);
    this.screen = screen;
    this.x = P.width / 2;
    this.y = P.height / 2;
    this.jumpTopTime = .5;
    this.jumpHeight = 350;
    this.jumpTimer = 0;
    this.jumpBaseY = this.y;
    this.radius = 15;
    this.vX = 0;
    this.maxMovement = 0
}
Ball.prototype = extendPrototype(DisplayableObject, {
    render: function(c) {
        c.fillStyle = "#ffffff";
        c.beginPath();
        c.arc(0, 0, 15, 0, Math.PI * 2, true);
        c.fill()
    },
    cycle: function(e) {
        var tmpY = this.y;
        this.jumpTimer += e;
        this.y = this.calculateY();
        if (this.y > tmpY) {
            if (this.y > P.height) {} else {
                for (var i in this.screen.divisionsArray) {
                    if (tmpY <= this.screen.divisionsArray[i].y && this.y >= this.screen.divisionsArray[i].y && this.x > this.screen.divisionsArray[i].x - this.radius && this.x < this.screen.divisionsArray[i].x + this.screen.divisionsArray[i].width + this.radius) {
                        this.bounce(this.screen.divisionsArray[i]);
                        break
                    }
                }
            }
        }
        if (this.direction != 0) {
            this.vX += this.direction * e * 1500
        } else {
            var absV = Math.abs(this.vX);
            var speedLoss = Util.limit(Util.sign(this.vX) * e * 400, -absV, absV);
            this.vX -= speedLoss
        }
        this.x += this.vX * e;
        this.x = Util.limit(this.x, 0, P.width);
        if (this.x == 0 || this.x == P.width) {
            this.vX = 0
        }
        var vY = Math.abs(this.y - tmpY) / e;
        this.scaleY = 1 + vY / 4e3;
        this.maxMovement = Math.max(Math.abs(this.x - P.width / 2), this.maxMovement);
        if (this.y > P.height) {
            this.screen.gameOver()
        }
    },
    bounce: function(platform) {
        this.y = platform.y;
        this.jumpTimer = 0;
        this.jumpBaseY = this.y;
        this.screen.bounced()
    },
    calculateY: function() {
        var x = this.jumpTimer;
        var h = this.jumpHeight;
        var tTop = this.jumpTopTime;
        var sqrtH = Math.sqrt(h);
        var k = sqrtH / tTop;
        return this.jumpBaseY - (h - Math.pow(k * x - sqrtH, 2))
    }
});
function Button(settings) {
    DisplayableContainer.call(this);
    Area.call(this, 0, 0, 0, 0);
    this.enabled = true;
    this.pressed = false;
    this.id = settings.id || null ;
    this.setup(settings)
}
Button.prototype = extendPrototype([DisplayableContainer, Area], {
    setup: function(settings) {
        this.action = settings.action || this.action || noop;
        this.width = settings.width || this.width || 400;
        this.height = settings.height || this.height || 100;
        this.borderWidth = settings.borderWidth || this.borderWidth || 4;
        this.borderRadius = settings.borderRadius || this.borderRadius || 10;
        this.borderColor = settings.borderColor || this.borderColor || "#0083a0";
        this.bgColor = settings.bgColor || this.bgColor || "#00abcd";
        this.bgOffsetX = settings.bgOffsetX || this.bgOffsetX || 8;
        this.bgOffsetY = settings.bgOffsetY || this.bgOffsetX || 8;
        if ("enabled" in settings) {
            this.enabled = settings.enabled
        }
        this.fontSize = settings.fontSize || this.fontSize || 40;
        this.fontColor = settings.fontColor || this.fontColor || "#ffffff";
        this.fontShadowColor = settings.fontShadowColor || this.fontShadowColor || null ;
        this.fontShadowOffsetX = settings.fontShadowOffsetX || this.fontShadowOffsetX || 0;
        this.fontShadowOffsetY = settings.fontShadowOffsetY || this.fontShadowOffsetY || 0;
        if ("content" in settings) {
            if (this.content) {
                this.content.remove()
            }
            if (settings.content.length) {
                this.content = new DisplayableTextField;
                this.addChild(this.content);
                with (this.content) {
                    x = this.width / 2;
                    y = this.height / 2;
                    text = settings.content;
                    textAlign = "center";
                    textBaseline = "middle";
                    color = this.fontColor;
                    font = "bold " + this.fontSize + "pt ComfortaaRegular";
                    shadowColor = this.fontShadowColor;
                    shadowOffsetX = this.fontShadowOffsetX;
                    shadowOffsetY = this.fontShadowOffsetY
                }
            } else {
                this.content = new DisplayableImage;
                this.content.image = settings.content;
                this.content.anchorX = -this.content.image.width / 2;
                this.content.anchorY = -this.content.image.height / 2;
                this.content.x = this.width / 2;
                this.content.y = this.height / 2;
                this.addChild(this.content)
            }
        }
    },
    render: function(c) {
        if (this.pressed || !this.enabled) {
            c.globalAlpha *= .5
        }
        c.beginPath();
        c.lineWidth = this.borderWidth;
        c.fillStyle = this.bgColor;
        c.strokeStyle = this.borderColor;
        c.moveTo(0, this.borderRadius);
        c.arc(this.borderRadius, this.borderRadius, this.borderRadius, Math.PI, -Math.PI / 2, false);
        c.arc(this.width - this.borderRadius, this.borderRadius, this.borderRadius, -Math.PI / 2, 0, false);
        c.arc(this.width - this.borderRadius, this.height - this.borderRadius, this.borderRadius, 0, Math.PI / 2, false);
        c.arc(this.borderRadius, this.height - this.borderRadius, this.borderRadius, Math.PI / 2, Math.PI, false);
        c.closePath();
        c.fill();
        c.stroke();
        DisplayableContainer.prototype.render.call(this, c)
    },
    actionPerformed: function(x, y) {
        this.pressed = false;
        if (this.enabled) {
            this.action();
            Game.instance.soundManager.play("click");
            Tracker.event("button", this.id || this.generateId())
        }
    },
    actionStart: function(x, y) {
        this.pressed = true
    },
    actionCancel: function(x, y) {
        this.pressed = false
    },
    generateId: function() {
        if (this.content.text) {
            return this.content.text.toLowerCase().replace(" ", "-")
        }
        return "unnamed"
    }
});
function GameplayScreen(game, settings) {
    Screen.call(this, game);
    this.settings = settings || {}
}
GameplayScreen.prototype = extendPrototype(Screen, {
    getId: function() {
        return "gameplay"
    },
    create: function() {
        this.view = new DisplayableContainer;
        this.downKeys = {};
        this.bg = new DisplayableRectangle;
        this.bg.color = "#000";
        this.bg.width = P.width;
        this.bg.height = P.height;
        this.view.addChild(this.bg);
        this.divisions = 8;
        this.divisionsArray = [];
        var d;
        for (var i = 0; i < this.divisions; i++) {
            d = new Platform(this);
            d.width = P.width / this.divisions;
            d.x = i * P.width / this.divisions;
            this.view.addChild(d);
            this.divisionsArray.push(d)
        }
        this.ball = new Ball(this);
        this.view.addChild(this.ball);
        this.score = 0;
        this.bounces = 0;
        if (!this.settings.auto) {
            this.isInTutorial = true;
            this.moveInstructions = new MultilineTextField;
            this.view.addChild(this.moveInstructions);
            with (this.moveInstructions) {
                x = P.width / 2;
                y = P.height * .25;
                textAlign = "center";
                textBaseline = "middle";
                font = "60pt ComfortaaRegular";
                color = "white";
                text = "Hold left or right to move the ball";
                maxWidth = P.width;
                lineHeight = 100
            }
            TweenPool.add(new Interpolation({
                object: this.moveInstructions,
                property: "y",
                from: -P.width,
                to: this.moveInstructions.y,
                duration: 1,
                easing: Math.easeOutBack
            }));
            this.arrowLeft = new DisplayableImage;
            this.arrowLeft.image = R.raw.arrow;
            this.arrowLeft.x = 50;
            this.arrowLeft.y = P.height - this.arrowLeft.image.height - 50;
            this.view.addChild(this.arrowLeft);
            this.arrowRight = new DisplayableImage;
            this.arrowRight.image = R.raw.arrow;
            this.arrowRight.x = P.width - 50;
            this.arrowRight.y = P.height - this.arrowRight.image.height - 50;
            this.arrowRight.scaleX = -1;
            this.view.addChild(this.arrowRight)
        }
        this.scoreTf = new DisplayableTextField;
        this.view.addChild(this.scoreTf);
        with (this.scoreTf) {
            x = P.width / 2;
            y = P.height * .2;
            textAlign = "center";
            textBaseline = "middle";
            font = "120pt ComfortaaRegular";
            color = "white";
            text = "0";
            maxWidth = P.width;
            lineHeight = 100;
            alpha = .5
        }
        this.scoreTf.visible = false;
        this.changeDivisions();
        this.launched = true;
        this.time = 0;
        this.frames = 0
    },
    changeDivisions: function() {
        var activated = [];
        for (var i in this.divisionsArray) {
            activated.push(true)
        }
        var deactivated = Math.min(this.divisionsArray.length - 2, (this.bounces - 2) / 5);
        for (var i = 0; i < deactivated; i++) {
            activated[~~(Math.random() * activated.length)] = false
        }
        for (var i in this.divisionsArray) {
            if (activated[i] || this.settings.auto || this.isInTutorial) {
                this.divisionsArray[i].activate()
            } else {
                this.divisionsArray[i].deactivate()
            }
        }
    },
    cycle: function(e) {
        this.ball.direction = 0;
        if (!this.settings.auto) {
            if (this.downKeys[37]) {
                this.ball.direction = -1
            } else if (this.downKeys[39]) {
                this.ball.direction = 1
            }
            if (Util.isTouchScreen()) {
                for (var i in this.game.currentTouches) {
                    if (this.game.currentTouches[i].x < P.width / 2) {
                        this.ball.direction = -1
                    } else {
                        this.ball.direction = 1
                    }
                }
            }
            this.arrowLeft.alpha = this.ball.direction < 0 ? 1 : .5;
            this.arrowRight.alpha = this.ball.direction > 0 ? 1 : .5
        }
        this.ball.cycle(e);
        if (this.isInTutorial && this.ball.maxMovement > P.width / 4) {
            this.isInTutorial = false;
            TweenPool.add(new Interpolation({
                object: this.moveInstructions,
                property: "y",
                from: this.moveInstructions.y,
                to: -P.height,
                duration: 1,
                easing: Math.easeInBack,
                onFinish: function() {
                    this.object.remove()
                }
            }));
            var me = this;
            this.fallInstructions = new MultilineTextField;
            this.view.addChild(this.fallInstructions);
            with (this.fallInstructions) {
                x = P.width / 2;
                y = P.height * .25;
                textAlign = "center";
                textBaseline = "middle";
                font = "60pt ComfortaaRegular";
                color = "white";
                text = "Now try not to fall down!";
                maxWidth = P.width;
                lineHeight = 100
            }
            TweenPool.add(new Interpolation({
                object: this.fallInstructions,
                property: "y",
                from: -P.height,
                to: this.fallInstructions.y,
                duration: 1,
                easing: Math.easeOutBack,
                delay: 1,
                onFinish: function() {
                    TweenPool.add(new Interpolation({
                        object: this.object,
                        property: "y",
                        from: this.object.y,
                        to: -P.height,
                        duration: 1,
                        easing: Math.easeInBack,
                        delay: 2,
                        onFinish: function() {
                            this.object.remove();
                            me.launched = true;
                            me.score = 0;
                            me.scoreTf.text = "0";
                            me.scoreTf.visible = true;
                            TweenPool.add(new Tween(me.scoreTf,"alpha",0,me.scoreTf.alpha,1))
                        }
                    }))
                }
            }))
        }
        this.view.x = this.view.y = 0;
        if (this.shakeTime > 0) {
            this.shakeTime -= e;
            this.view.x = ~~Util.rand(-10, 10);
            this.view.y = ~~Util.rand(-10, 10)
        }
        this.time += e;
        this.frames++
    },
    bounced: function() {
        this.changeDivisions();
        if (!this.isInTutorial) {
            this.bounces++;
            if (this.launched) {
                this.score++
            }
            this.scoreTf.text = this.score.toString();
            TweenPool.add(new Tween(this.scoreTf,"alpha",1,.5,.2))
        }
    },
    columnPlatform: function(x) {
        var w = P.width / this.divisions;
        return this.divisionsArray[~~(x / w)]
    },
    keyDown: function(k) {
        this.downKeys[k] = true
    },
    keyUp: function(k) {
        this.downKeys[k] = false
    },
    gameOver: function() {
        if (!this.ended) {
            this.ended = true;
            this.scoreTf.remove();
            setTimeout(this.game.end.bind(this.game), 1e3);
            this.shake(.3);
            navigator.vibrate(500);
            this.arrowLeft.remove();
            this.arrowRight.remove()
        }
    },
    shake: function(d) {
        this.shakeTime = d
    }
});
function AdManager(game) {
    this.game = game
}
AdManager.prototype = {
    init: noop,
    attemptStart: noop,
    attemptEnd: noop
};
function WebBannerAdManager(game) {
    AdManager.call(this, game)
}
WebBannerAdManager.prototype = extendPrototype(AdManager, {
    init: function() {
      
    }
});

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}


function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}