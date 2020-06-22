var SearchTools;
(function (SearchTools) {
    var InstantSearch = (function () {
        function InstantSearch(container, tokens, defaultClassName, defaultCaseSensitivity) {
            this.iterator = this.iterator.bind(this);
            this.checkAndReplace = this.checkAndReplace.bind(this);
            this.highlight = this.highlight.bind(this);
            this.highlightNode = this.highlightNode.bind(this);
            this.m_container = container;
            this.m_defaultClassName = defaultClassName || "highlight";
            this.m_defaultCaseSensitivity = defaultCaseSensitivity || false;
            this.m_highlightTokens = tokens || [{
                    token: "test",
                    className: this.m_defaultClassName,
                    sensitiveSearch: this.m_defaultCaseSensitivity
                }];
        }
        InstantSearch.prototype.checkAndReplace = function (node) {
            var nodeVal = node.nodeValue;
            var parentNode = node.parentNode;
            var textNode = null;
            for (var i = 0, j = this.m_highlightTokens.length; i < j; i++) {
                var curToken = this.m_highlightTokens[i];
                var textToHighlight = curToken.token;
                var highlightClassName = curToken.className || this.m_defaultClassName;
                var caseSensitive = curToken.sensitiveSearch || this.m_defaultCaseSensitivity;
                var isFirst = true;
                while (true) {
                    var foundIndex = caseSensitive ?
                        nodeVal.indexOf(textToHighlight)
                        : nodeVal.toLowerCase().indexOf(textToHighlight.toLowerCase());
                    if (foundIndex < 0) {
                        if (isFirst)
                            break;
                        if (nodeVal) {
                            textNode = document.createTextNode(nodeVal);
                            parentNode.insertBefore(textNode, node);
                        }
                        parentNode.removeChild(node);
                        break;
                    }
                    isFirst = false;
                    var begin = nodeVal.substring(0, foundIndex);
                    var matched = nodeVal.substr(foundIndex, textToHighlight.length);
                    if (begin) {
                        textNode = document.createTextNode(begin);
                        parentNode.insertBefore(textNode, node);
                    }
                    var span = document.createElement("span");
                    if (!span.classList.contains(highlightClassName))
                        span.classList.add(highlightClassName);
                    span.appendChild(document.createTextNode(matched));
                    parentNode.insertBefore(span, node);
                    nodeVal = nodeVal.substring(foundIndex + textToHighlight.length);
                }
            }
        };
        InstantSearch.prototype.iterator = function (p) {
            if (p == null)
                return;
            var children = Array.prototype.slice.call(p.childNodes);
            if (children.length) {
                for (var i = 0; i < children.length; i++) {
                    var cur = children[i];
                    if (cur.nodeType === Node.TEXT_NODE) {
                        this.checkAndReplace(cur);
                    }
                    else if (cur.nodeType === Node.ELEMENT_NODE) {
                        this.iterator(cur);
                    }
                }
            }
        };
        InstantSearch.prototype.highlightNode = function (n) {
            this.iterator(n);
        };
        InstantSearch.prototype.highlight = function () {
            this.iterator(this.m_container);
        };
        return InstantSearch;
    }());
    SearchTools.InstantSearch = InstantSearch;
})(SearchTools || (SearchTools = {}));
