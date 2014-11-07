﻿!function (d, y, k, j) {
    function s(a, b) { var c = Math.max(0, a[0] - b[0], b[0] - a[1]), e = Math.max(0, a[2] - b[1], b[1] - a[3]); return c + e } function t(a, b, c, e) { for (var h = a.length, e = e ? "offset" : "position", c = c || 0; h--;) { var f = a[h].el ? a[h].el : d(a[h]), i = f[e](); i.left += parseInt(f.css("margin-left"), 10); i.top += parseInt(f.css("margin-top"), 10); b[h] = [i.left - c, i.left + f.outerWidth() + c, i.top - c, i.top + f.outerHeight() + c] } } function m(a, b) { var c = b.offset(); return { left: a.left - c.left, top: a.top - c.top } } function u(a, b, c) {
        for (var b = [b.left, b.top],
        c = c && [c.left, c.top], e, h = a.length, d = []; h--;) e = a[h], d[h] = [h, s(e, b), c && s(e, c)]; return d = d.sort(function (a, b) { return b[1] - a[1] || b[2] - a[2] || b[0] - a[0] })
    } function n(a) { this.options = d.extend({}, l, a); this.containers = []; this.options.rootGroup || (this.scrollProxy = d.proxy(this.scroll, this), this.dragProxy = d.proxy(this.drag, this), this.dropProxy = d.proxy(this.drop, this), this.placeholder = d(this.options.placeholder), a.isValidTarget || (this.options.isValidTarget = j)) } function q(a, b) {
        this.el = a; this.options = d.extend({},
        w, b); this.group = n.get(this.options); this.rootGroup = this.options.rootGroup || this.group; this.handle = this.rootGroup.options.handle || this.rootGroup.options.itemSelector; var c = this.rootGroup.options.itemPath; this.target = c ? this.el.find(c) : this.el; this.target.on(o.start, this.handle, d.proxy(this.dragInit, this)); this.options.drop && this.group.containers.push(this)
    } var o, w = { drag: !0, drop: !0, exclude: "", nested: !0, vertical: !0 }, l = {
        afterMove: function () { }, containerPath: "", containerSelector: "ol, ul", distance: 0, delay: 0,
        handle: "", itemPath: "", itemSelector: "li", isValidTarget: function () { return !0 }, onCancel: function () { }, onDrag: function (a, b) { a.css(b) }, onDragStart: function (a) { a.css({ height: a.height(), width: a.width() }); a.addClass("dragged"); d("body").addClass("dragging") }, onDrop: function (a) { a.removeClass("dragged").removeAttr("style"); d("body").removeClass("dragging") }, onMousedown: function (a, b, c) { if (!c.target.nodeName.match(/^(input|select)$/i)) return c.preventDefault(), !0 }, placeholder: '<li class="placeholder"/>', pullPlaceholder: !0,
        serialize: function (a, b, c) { a = d.extend({}, a.data()); if (c) return [b]; b[0] && (a.children = b); delete a.subContainers; delete a.sortable; return a }, tolerance: 0
    }, p = {}, v = 0, x = { left: 0, top: 0, bottom: 0, right: 0 }; o = { start: "touchstart.sortable mousedown.sortable", drop: "touchend.sortable touchcancel.sortable mouseup.sortable", drag: "touchmove.sortable mousemove.sortable", scroll: "scroll.sortable" }; n.get = function (a) { p[a.group] || (a.group === j && (a.group = v++), p[a.group] = new n(a)); return p[a.group] }; n.prototype = {
        dragInit: function (a,
        b) { this.$document = d(b.el[0].ownerDocument); this.item = d(a.target).closest(this.options.itemSelector); this.itemContainer = b; !this.item.is(this.options.exclude) && this.options.onMousedown(this.item, l.onMousedown, a) && (this.setPointer(a), this.toggleListeners("on"), this.setupDelayTimer(), this.dragInitDone = !0) }, drag: function (a) {
            if (!this.dragging) {
                if (!this.distanceMet(a) || !this.delayMet) return; this.options.onDragStart(this.item, this.itemContainer, l.onDragStart, a); this.item.before(this.placeholder); this.dragging =
                !0
            } this.setPointer(a); this.options.onDrag(this.item, m(this.pointer, this.item.offsetParent()), l.onDrag, a); var b = a.pageX || a.originalEvent.pageX, a = a.pageY || a.originalEvent.pageY, c = this.sameResultBox, e = this.options.tolerance; if (!c || c.top - e > a || c.bottom + e < a || c.left - e > b || c.right + e < b) this.searchValidTarget() || this.placeholder.detach()
        }, drop: function (a) {
            this.toggleListeners("off"); this.dragInitDone = !1; if (this.dragging) {
                if (this.placeholder.closest("html")[0]) this.placeholder.before(this.item).detach(); else this.options.onCancel(this.item,
                this.itemContainer, l.onCancel, a); this.options.onDrop(this.item, this.getContainer(this.item), l.onDrop, a); this.clearDimensions(); this.clearOffsetParent(); this.lastAppendedItem = this.sameResultBox = j; this.dragging = !1
            }
        }, searchValidTarget: function (a, b) {
            a || (a = this.relativePointer || this.pointer, b = this.lastRelativePointer || this.lastPointer); for (var c = u(this.getContainerDimensions(), a, b), e = c.length; e--;) {
                var d = c[e][0]; if (!c[e][1] || this.options.pullPlaceholder) if (d = this.containers[d], !d.disabled) {
                    if (!this.$getOffsetParent()) var f =
                    d.getItemOffsetParent(), a = m(a, f), b = m(b, f); if (d.searchValidTarget(a, b)) return !0
                }
            } this.sameResultBox && (this.sameResultBox = j)
        }, movePlaceholder: function (a, b, c, e) { var d = this.lastAppendedItem; if (e || !(d && d[0] === b[0])) b[c](this.placeholder), this.lastAppendedItem = b, this.sameResultBox = e, this.options.afterMove(this.placeholder, a, b) }, getContainerDimensions: function () { this.containerDimensions || t(this.containers, this.containerDimensions = [], this.options.tolerance, !this.$getOffsetParent()); return this.containerDimensions },
        getContainer: function (a) { return a.closest(this.options.containerSelector).data(k) }, $getOffsetParent: function () { if (this.offsetParent === j) { var a = this.containers.length - 1, b = this.containers[a].getItemOffsetParent(); if (!this.options.rootGroup) for (; a--;) if (b[0] != this.containers[a].getItemOffsetParent()[0]) { b = !1; break } this.offsetParent = b } return this.offsetParent }, setPointer: function (a) {
            a = this.getPointer(a); if (this.$getOffsetParent()) {
                var b = m(a, this.$getOffsetParent()); this.lastRelativePointer = this.relativePointer;
                this.relativePointer = b
            } this.lastPointer = this.pointer; this.pointer = a
        }, distanceMet: function (a) { a = this.getPointer(a); return Math.max(Math.abs(this.pointer.left - a.left), Math.abs(this.pointer.top - a.top)) >= this.options.distance }, getPointer: function (a) { return { left: a.pageX || a.originalEvent.pageX, top: a.pageY || a.originalEvent.pageY } }, setupDelayTimer: function () {
            var a = this; this.delayMet = !this.options.delay; this.delayMet || (clearTimeout(this._mouseDelayTimer), this._mouseDelayTimer = setTimeout(function () {
                a.delayMet =
                !0
            }, this.options.delay))
        }, scroll: function () { this.clearDimensions(); this.clearOffsetParent() }, toggleListeners: function (a) { var b = this; d.each(["drag", "drop", "scroll"], function (c, e) { b.$document[a](o[e], b[e + "Proxy"]) }) }, clearOffsetParent: function () { this.offsetParent = j }, clearDimensions: function () { this.traverse(function (a) { a._clearDimensions() }) }, traverse: function (a) { a(this); for (var b = this.containers.length; b--;) this.containers[b].traverse(a) }, _clearDimensions: function () { this.containerDimensions = j }, _destroy: function () {
            p[this.options.group] =
            j
        }
    }; q.prototype = {
        dragInit: function (a) { var b = this.rootGroup; !this.disabled && !b.dragInitDone && this.options.drag && this.isValidDrag(a) && b.dragInit(a, this) }, isValidDrag: function (a) { return 1 == a.which || "touchstart" == a.type && 1 == a.originalEvent.touches.length }, searchValidTarget: function (a, b) {
            var c = u(this.getItemDimensions(), a, b), e = c.length, d = this.rootGroup, f = !d.options.isValidTarget || d.options.isValidTarget(d.item, this); if (!e && f) return d.movePlaceholder(this, this.target, "append"), !0; for (; e--;) if (d = c[e][0],
            !c[e][1] && this.hasChildGroup(d)) { if (this.getContainerGroup(d).searchValidTarget(a, b)) return !0 } else if (f) return this.movePlaceholder(d, a), !0
        }, movePlaceholder: function (a, b) {
            var c = d(this.items[a]), e = this.itemDimensions[a], h = "after", f = c.outerWidth(), i = c.outerHeight(), g = c.offset(), g = { left: g.left, right: g.left + f, top: g.top, bottom: g.top + i }; this.options.vertical ? b.top <= (e[2] + e[3]) / 2 ? (h = "before", g.bottom -= i / 2) : g.top += i / 2 : b.left <= (e[0] + e[1]) / 2 ? (h = "before", g.right -= f / 2) : g.left += f / 2; this.hasChildGroup(a) && (g = x);
            this.rootGroup.movePlaceholder(this, c, h, g)
        }, getItemDimensions: function () { this.itemDimensions || (this.items = this.$getChildren(this.el, "item").filter(":not(.placeholder, .dragged)").get(), t(this.items, this.itemDimensions = [], this.options.tolerance)); return this.itemDimensions }, getItemOffsetParent: function () { var a = this.el; return "relative" === a.css("position") || "absolute" === a.css("position") || "fixed" === a.css("position") ? a : a.offsetParent() }, hasChildGroup: function (a) { return this.options.nested && this.getContainerGroup(a) },
        getContainerGroup: function (a) { var b = d.data(this.items[a], "subContainers"); if (b === j) { var c = this.$getChildren(this.items[a], "container"), b = !1; c[0] && (b = d.extend({}, this.options, { rootGroup: this.rootGroup, group: v++ }), b = c[k](b).data(k).group); d.data(this.items[a], "subContainers", b) } return b }, $getChildren: function (a, b) { var c = this.rootGroup.options, e = c[b + "Path"], c = c[b + "Selector"], a = d(a); e && (a = a.find(e)); return a.children(c) }, _serialize: function (a, b) {
            var c = this, e = this.$getChildren(a, b ? "item" : "container").not(this.options.exclude).map(function () {
                return c._serialize(d(this),
                !b)
            }).get(); return this.rootGroup.options.serialize(a, e, b)
        }, traverse: function (a) { d.each(this.items || [], function () { var b = d.data(this, "subContainers"); b && b.traverse(a) }); a(this) }, _clearDimensions: function () { this.itemDimensions = j }, _destroy: function () { var a = this; this.target.off(o.start, this.handle); this.el.removeData(k); this.options.drop && (this.group.containers = d.grep(this.group.containers, function (b) { return b != a })); d.each(this.items || [], function () { d.removeData(this, "subContainers") }) }
    }; var r = {
        enable: function () {
            this.traverse(function (a) {
                a.disabled =
                !1
            })
        }, disable: function () { this.traverse(function (a) { a.disabled = !0 }) }, serialize: function () { return this._serialize(this.el, !0) }, refresh: function () { this.traverse(function (a) { a._clearDimensions() }) }, destroy: function () { this.traverse(function (a) { a._destroy() }) }
    }; d.extend(q.prototype, r); d.fn[k] = function (a) { var b = Array.prototype.slice.call(arguments, 1); return this.map(function () { var c = d(this), e = c.data(k); if (e && r[a]) return r[a].apply(e, b) || this; !e && (a === j || "object" === typeof a) && c.data(k, new q(c, a)); return this }) }
}(jQuery,
window, "sortable");