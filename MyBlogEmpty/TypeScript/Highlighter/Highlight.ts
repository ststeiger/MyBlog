
namespace SearchTools 
{


    export interface IToken
    {
        token: string;
        className: string;
        sensitiveSearch: boolean;
    }


    export class InstantSearch 
    {

        protected m_container: Node;
        protected m_defaultClassName: string;
        protected m_defaultCaseSensitivity: boolean;
        protected m_highlightTokens: IToken[];


        constructor(container: Node, tokens: IToken[], defaultClassName?: string, defaultCaseSensitivity?: boolean)
        {
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


        protected checkAndReplace(node: Node)
        {
            let nodeVal: string = node.nodeValue;
            let parentNode: Node = node.parentNode;
            let textNode: Text = null;

            for (let i = 0, j = this.m_highlightTokens.length; i < j; i++)
            {
                let curToken: IToken = this.m_highlightTokens[i];
                let textToHighlight: string = curToken.token;
                let highlightClassName: string = curToken.className || this.m_defaultClassName;
                let caseSensitive: boolean = curToken.sensitiveSearch || this.m_defaultCaseSensitivity;

                let isFirst: boolean = true;
                while (true)
                {
                    let foundIndex: number = caseSensitive ?
                        nodeVal.indexOf(textToHighlight)
                        : nodeVal.toLowerCase().indexOf(textToHighlight.toLowerCase());

                    if (foundIndex < 0)
                    {
                        if (isFirst)
                            break;

                        if (nodeVal)
                        {
                            textNode = document.createTextNode(nodeVal);
                            parentNode.insertBefore(textNode, node);
                        } // End if (nodeVal)

                        parentNode.removeChild(node);
                        break;
                    } // End if (foundIndex < 0)

                    isFirst = false;


                    let begin: string = nodeVal.substring(0, foundIndex);
                    let matched: string = nodeVal.substr(foundIndex, textToHighlight.length);

                    if (begin)
                    {
                        textNode = document.createTextNode(begin);
                        parentNode.insertBefore(textNode, node);
                    } // End if (begin)

                    let span: HTMLSpanElement = document.createElement("span");

                    if (!span.classList.contains(highlightClassName))
                        span.classList.add(highlightClassName);

                    span.appendChild(document.createTextNode(matched));
                    parentNode.insertBefore(span, node);

                    nodeVal = nodeVal.substring(foundIndex + textToHighlight.length);
                } // Whend

            } // Next i 

        } // End Sub checkAndReplace 


        protected iterator(p: Node)
        {
            if (p == null)
                return;

            let children: Node[] = Array.prototype.slice.call(p.childNodes);

            if (children.length)
            {
                for (let i = 0; i < children.length; i++)
                {
                    let cur: Node = children[i];

                    // https://developer.mozilla.org/en-US/docs/Web/API/Node/nodeType
                    if (cur.nodeType === Node.TEXT_NODE) 
                    {
                        this.checkAndReplace(cur);
                    }
                    else if (cur.nodeType === Node.ELEMENT_NODE) 
                    {
                        this.iterator(cur);
                    }
                } // Next i 

            } // End if (children.length) 

        } // End Sub iterator


        public highlightNode(n:Node)
        {
            this.iterator(n);
        } // End Sub highlight 


        public highlight()
        {
            this.iterator(this.m_container);
        } // End Sub highlight 


    } // End Class InstantSearch 


} // End Namespace SearchTools 
