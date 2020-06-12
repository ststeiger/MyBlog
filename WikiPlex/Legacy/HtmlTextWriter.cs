
namespace WikiPlex.Legacy 
{
    
    
    public class HtmlTextWriter
        : System.IO.TextWriter
    {
        private int styles_pos = -1;
        private int attrs_pos = -1;
        private int tagstack_pos = -1;

        private static HtmlTextWriter.HtmlTag[] tags = new HtmlTextWriter.HtmlTag[97]
        {
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Unknown, string.Empty, HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.A, "a", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Acronym, "acronym", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Address, "address", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Area, "area", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.B, nameof(b), HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Base, "base", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Basefont, "basefont", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Bdo, "bdo", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Bgsound, "bgsound", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Big, "big", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Blockquote, "blockquote", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Body, "body", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Br, "br", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Button, "button", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Caption, "caption", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Center, "center", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Cite, "cite", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Code, "code", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Col, "col", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Colgroup, "colgroup", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dd, "dd", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Del, "del", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dfn, "dfn", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dir, "dir", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Div, "div", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dl, "dl", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Dt, "dt", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Em, "em", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Embed, "embed", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Fieldset, "fieldset", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Font, "font", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Form, "form", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Frame, "frame", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Frameset, "frameset", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H1, "h1", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H2, "h2", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H3, "h3", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H4, "h4", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H5, "h5", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.H6, "h6", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Head, "head", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Hr, "hr", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Html, "html", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.I, "i", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Iframe, "iframe", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Img, "img", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Input, "input", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ins, "ins", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Isindex, "isindex", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Kbd, "kbd", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Label, "label", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Legend, "legend", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Li, "li", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Link, "link", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Map, "map", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Marquee, "marquee", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Menu, "menu", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Meta, "meta", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Nobr, "nobr", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Noframes, "noframes", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Noscript, "noscript", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Object, "object", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ol, "ol", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Option, "option", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.P, "p", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Param, "param", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Pre, "pre", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Q, "q", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Rt, "rt", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ruby, "ruby", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.S, "s", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Samp, "samp", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Script, "script", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Select, "select", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Small, "small", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Span, "span", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Strike, "strike", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Strong, "strong", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Style, "style", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Sub, "sub", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Sup, "sup", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Table, "table", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tbody, "tbody", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Td, "td", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Textarea, "textarea", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tfoot, "tfoot", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Th, "th", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Thead, "thead", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Title, "title", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tr, "tr", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Tt, "tt", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.U, "u", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Ul, "ul", HtmlTextWriter.TagType.Block),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Var, "var", HtmlTextWriter.TagType.Inline),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Wbr, "wbr", HtmlTextWriter.TagType.SelfClosing),
            new HtmlTextWriter.HtmlTag(HtmlTextWriterTag.Xml, "xml", HtmlTextWriter.TagType.Block)
        };

        private static HtmlTextWriter.HtmlAttribute[] htmlattrs = new HtmlTextWriter.HtmlAttribute[54]
        {
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Accesskey, "accesskey"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Align, "align"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Alt, "alt"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Background, "background"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Bgcolor, "bgcolor"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Border, "border"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Bordercolor, "bordercolor"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cellpadding, "cellpadding"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cellspacing, "cellspacing"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Checked, "checked"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Class, "class"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Cols, "cols"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Colspan, "colspan"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Disabled, "disabled"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.For, "for"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Height, "height"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Href, "href"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Id, "id"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Maxlength, "maxlength"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Multiple, "multiple"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Name, "name"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Onchange, "onchange"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Onclick, "onclick"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rows, "rows"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rowspan, "rowspan"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rules, "rules"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Selected, "selected"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Size, "size"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Src, "src"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Style, "style"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Tabindex, "tabindex"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Target, "target"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Title, "title"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Type, "type"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Valign, "valign"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Value, "value"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Width, "width"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Wrap, "wrap"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Abbr, "abbr"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.AutoComplete, "autocomplete"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Axis, "axis"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Content, "content"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Coords, "coords"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.DesignerRegion, "_designerregion"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Dir, "dir"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Headers, "headers"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Longdesc, "longdesc"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Rel, "rel"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Scope, "scope"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Shape, "shape"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.Usemap, "usemap"),
            new HtmlTextWriter.HtmlAttribute(HtmlTextWriterAttribute.VCardName, "vcard_name")
        };

        private static HtmlTextWriter.HtmlStyle[] htmlstyles = new HtmlTextWriter.HtmlStyle[43]
        {
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BackgroundColor, "background-color"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BackgroundImage, "background-image"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderCollapse, "border-collapse"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderColor, "border-color"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderStyle, "border-style"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.BorderWidth, "border-width"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Color, "color"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontFamily, "font-family"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontSize, "font-size"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontStyle, "font-style"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontWeight, "font-weight"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Height, "height"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextDecoration, "text-decoration"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Width, "width"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ListStyleImage, "list-style-image"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ListStyleType, "list-style-type"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Cursor, "cursor"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Direction, "direction"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Display, "display"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Filter, "filter"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.FontVariant, "font-variant"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Left, "left"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Margin, "margin"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginBottom, "margin-bottom"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginLeft, "margin-left"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginRight, "margin-right"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.MarginTop, "margin-top"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Overflow, "overflow"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.OverflowX, "overflow-x"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.OverflowY, "overflow-y"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Padding, "padding"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingBottom, "padding-bottom"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingLeft, "padding-left"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingRight, "padding-right"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.PaddingTop, "padding-top"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Position, "position"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextAlign, "text-align"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.VerticalAlign, "vertical-align"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.TextOverflow, "text-overflow"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Top, "top"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.Visibility, "visibility"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.WhiteSpace, "white-space"),
            new HtmlTextWriter.HtmlStyle(HtmlTextWriterStyle.ZIndex, "z-index")
        };

        private static readonly System.Collections.Hashtable _tagTable = 
            new System.Collections.Hashtable(HtmlTextWriter.tags.Length,
            (System.Collections.IEqualityComparer) System.StringComparer.OrdinalIgnoreCase);

        
        private static readonly System.Collections.Hashtable _attributeTable = 
            new System.Collections.Hashtable(HtmlTextWriter.htmlattrs.Length,
            (System.Collections.IEqualityComparer) System.StringComparer.OrdinalIgnoreCase);

        private static readonly System.Collections.Hashtable _styleTable = 
            new System.Collections.Hashtable(HtmlTextWriter.htmlstyles.Length,
            (System.Collections.IEqualityComparer) System.StringComparer.OrdinalIgnoreCase);
        
        private int indent;
        private System.IO.TextWriter b;
        private string tab_string;
        private bool newline;
        private HtmlTextWriter.AddedStyle[] styles;
        private HtmlTextWriter.AddedAttr[] attrs;
        private HtmlTextWriter.AddedTag[] tagstack;
        public const string DefaultTabString = "\t";
        public const char DoubleQuoteChar = '"';
        public const string EndTagLeftChars = "</";
        public const char EqualsChar = '=';
        public const string EqualsDoubleQuoteString = "=\"";
        public const string SelfClosingChars = " /";
        public const string SelfClosingTagEnd = " />";
        public const char SemicolonChar = ';';
        public const char SingleQuoteChar = '\'';
        public const char SlashChar = '/';
        public const char SpaceChar = ' ';
        public const char StyleEqualsChar = ':';
        public const char TagLeftChar = '<';
        public const char TagRightChar = '>';

        static HtmlTextWriter()
        {
            foreach (HtmlTextWriter.HtmlTag tag in HtmlTextWriter.tags)
                HtmlTextWriter._tagTable.Add((object) tag.name, (object) tag);
            foreach (HtmlTextWriter.HtmlAttribute htmlattr in HtmlTextWriter.htmlattrs)
                HtmlTextWriter._attributeTable.Add((object) htmlattr.name, (object) htmlattr);
            foreach (HtmlTextWriter.HtmlStyle htmlstyle in HtmlTextWriter.htmlstyles)
                HtmlTextWriter._styleTable.Add((object) htmlstyle.name, (object) htmlstyle);
        }

        public HtmlTextWriter(System.IO.TextWriter writer)
            : this(writer, "\t")
        {
        }

        public HtmlTextWriter(System.IO.TextWriter writer, string tabString)
        {
            this.b = writer;
            this.tab_string = tabString;
        }

        internal static string StaticGetStyleName(HtmlTextWriterStyle styleKey)
        {
            return styleKey < (HtmlTextWriterStyle) HtmlTextWriter.htmlstyles.Length
                ? HtmlTextWriter.htmlstyles[(int) styleKey].name
                : (string) null;
        }

        //[MonoTODO("Does nothing")]
        //protected static void RegisterAttribute(string name, HtmlTextWriterAttribute key)
        //{}

        //[MonoTODO("Does nothing")]
        //protected static void RegisterStyle(string name, HtmlTextWriterStyle key)
        //{ }

        //[MonoTODO("Does nothing")]
        //protected static void RegisterTag(string name, HtmlTextWriterTag key)
        //{}

        public virtual void AddAttribute(HtmlTextWriterAttribute key, string value, bool fEncode)
        {
            if (fEncode)
                value = System.Web.HttpUtility.HtmlAttributeEncode(value);
            this.AddAttribute(this.GetAttributeName(key), value, key);
        }

        public virtual void AddAttribute(HtmlTextWriterAttribute key, string value)
        {
            if (key != HtmlTextWriterAttribute.Name && key != HtmlTextWriterAttribute.Id)
                value = System.Web.HttpUtility.HtmlAttributeEncode(value);
            this.AddAttribute(this.GetAttributeName(key), value, key);
        }

        public virtual void AddAttribute(string name, string value, bool fEndode)
        {
            if (fEndode)
                value = System.Web.HttpUtility.HtmlAttributeEncode(value);
            this.AddAttribute(name, value, this.GetAttributeKey(name));
        }

        public virtual void AddAttribute(string name, string value)
        {
            HtmlTextWriterAttribute attributeKey = this.GetAttributeKey(name);
            switch (attributeKey)
            {
                case HtmlTextWriterAttribute.Id:
                case HtmlTextWriterAttribute.Name:
                    this.AddAttribute(name, value, attributeKey);
                    break;
                default:
                    value = System.Web.HttpUtility.HtmlAttributeEncode(value);
                    goto case HtmlTextWriterAttribute.Id;
            }
        }

        protected virtual void AddAttribute(string name, string value, HtmlTextWriterAttribute key)
        {
            this.NextAttrStack();
            this.attrs[this.attrs_pos].name = name;
            this.attrs[this.attrs_pos].value = value;
            this.attrs[this.attrs_pos].key = key;
        }

        protected virtual void AddStyleAttribute(string name, string value, HtmlTextWriterStyle key)
        {
            this.NextStyleStack();
            this.styles[this.styles_pos].name = name;
            value = System.Web.HttpUtility.HtmlAttributeEncode(value);
            this.styles[this.styles_pos].value = value;
            this.styles[this.styles_pos].key = key;
        }

        public virtual void AddStyleAttribute(string name, string value)
        {
            this.AddStyleAttribute(name, value, this.GetStyleKey(name));
        }

        public virtual void AddStyleAttribute(HtmlTextWriterStyle key, string value)
        {
            this.AddStyleAttribute(this.GetStyleName(key), value, key);
        }

        public override void Close()
        {
            this.b.Close();
        }

        protected virtual string EncodeAttributeValue(HtmlTextWriterAttribute attrKey, string value)
        {
            return System.Web.HttpUtility.HtmlAttributeEncode(value);
        }

        protected string EncodeAttributeValue(string value, bool fEncode)
        {
            return fEncode ? System.Web.HttpUtility.HtmlAttributeEncode(value) : value;
        }

        protected string EncodeUrl(string url)
        {
            return System.Web.HttpUtility.UrlPathEncode(url);
        }

        protected virtual void FilterAttributes()
        {
            HtmlTextWriter.AddedAttr addedAttr = new HtmlTextWriter.AddedAttr();
            for (int index = 0; index <= this.attrs_pos; ++index)
            {
                HtmlTextWriter.AddedAttr attr = this.attrs[index];
                if (this.OnAttributeRender(attr.name, attr.value, attr.key))
                {
                    if (attr.key == HtmlTextWriterAttribute.Style)
                        addedAttr = attr;
                    else
                        this.WriteAttribute(attr.name, attr.value, false);
                }
            }

            if (this.styles_pos != -1 || addedAttr.value != null)
            {
                this.Write(' ');
                this.Write("style");
                this.Write("=\"");
                for (int index = 0; index <= this.styles_pos; ++index)
                {
                    HtmlTextWriter.AddedStyle style = this.styles[index];
                    if (this.OnStyleAttributeRender(style.name, style.value, style.key))
                    {
                        if (style.key == HtmlTextWriterStyle.BackgroundImage)
                            style.value = "url(" + System.Web.HttpUtility.UrlPathEncode(style.value) + ")";
                        this.WriteStyleAttribute(style.name, style.value, false);
                    }
                }

                this.Write(addedAttr.value);
                this.Write('"');
            }

            this.styles_pos = this.attrs_pos = -1;
        }

        public override void Flush()
        {
            this.b.Flush();
        }

        protected HtmlTextWriterAttribute GetAttributeKey(string attrName)
        {
            object obj = HtmlTextWriter._attributeTable[(object) attrName];
            return obj == null ? ~HtmlTextWriterAttribute.Accesskey : ((HtmlTextWriter.HtmlAttribute) obj).key;
        }

        protected string GetAttributeName(HtmlTextWriterAttribute attrKey)
        {
            return attrKey < (HtmlTextWriterAttribute) HtmlTextWriter.htmlattrs.Length
                ? HtmlTextWriter.htmlattrs[(int) attrKey].name
                : (string) null;
        }

        protected HtmlTextWriterStyle GetStyleKey(string styleName)
        {
            object obj = HtmlTextWriter._styleTable[(object) styleName];
            return obj == null ? ~HtmlTextWriterStyle.BackgroundColor : ((HtmlTextWriter.HtmlStyle) obj).key;
        }

        protected string GetStyleName(HtmlTextWriterStyle styleKey)
        {
            return HtmlTextWriter.StaticGetStyleName(styleKey);
        }

        protected virtual HtmlTextWriterTag GetTagKey(string tagName)
        {
            object obj = HtmlTextWriter._tagTable[(object) tagName];
            return obj == null ? HtmlTextWriterTag.Unknown : ((HtmlTextWriter.HtmlTag) obj).key;
        }

        internal static string StaticGetTagName(HtmlTextWriterTag tagKey)
        {
            return tagKey < (HtmlTextWriterTag) HtmlTextWriter.tags.Length
                ? HtmlTextWriter.tags[(int) tagKey].name
                : (string) null;
        }

        protected virtual string GetTagName(HtmlTextWriterTag tagKey)
        {
            return tagKey < (HtmlTextWriterTag) HtmlTextWriter.tags.Length
                ? HtmlTextWriter.tags[(int) tagKey].name
                : (string) null;
        }

        protected bool IsAttributeDefined(HtmlTextWriterAttribute key)
        {
            return this.IsAttributeDefined(key, out string _);
        }

        protected bool IsAttributeDefined(HtmlTextWriterAttribute key, out string value)
        {
            for (int index = 0; index <= this.attrs_pos; ++index)
            {
                if (this.attrs[index].key == key)
                {
                    value = this.attrs[index].value;
                    return true;
                }
            }

            value = (string) null;
            return false;
        }

        protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key)
        {
            return this.IsStyleAttributeDefined(key, out string _);
        }

        protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key, out string value)
        {
            for (int index = 0; index <= this.styles_pos; ++index)
            {
                if (this.styles[index].key == key)
                {
                    value = this.styles[index].value;
                    return true;
                }
            }

            value = (string) null;
            return false;
        }

        protected virtual bool OnAttributeRender(
            string name,
            string value,
            HtmlTextWriterAttribute key)
        {
            return true;
        }

        protected virtual bool OnStyleAttributeRender(
            string name,
            string value,
            HtmlTextWriterStyle key)
        {
            return true;
        }

        protected virtual bool OnTagRender(string name, HtmlTextWriterTag key)
        {
            return true;
        }

        protected virtual void OutputTabs()
        {
            if (!this.newline)
                return;
            this.newline = false;
            for (int index = 0; index < this.Indent; ++index)
                this.b.Write(this.tab_string);
        }

        protected string PopEndTag()
        {
            if (this.tagstack_pos == -1)
                throw new System.InvalidOperationException();
            string tagName = this.TagName;
            --this.tagstack_pos;
            return tagName;
        }

        protected void PushEndTag(string endTag)
        {
            this.NextTagStack();
            this.TagName = endTag;
        }

        private void PushEndTag(HtmlTextWriterTag t)
        {
            this.NextTagStack();
            this.TagKey = t;
        }

        protected virtual string RenderAfterContent()
        {
            return (string) null;
        }

        protected virtual string RenderAfterTag()
        {
            return (string) null;
        }

        protected virtual string RenderBeforeContent()
        {
            return (string) null;
        }

        protected virtual string RenderBeforeTag()
        {
            return (string) null;
        }

        public virtual void RenderBeginTag(string tagName)
        {
            bool flag = !this.OnTagRender(tagName, this.GetTagKey(tagName));
            this.PushEndTag(tagName);
            this.TagIgnore = flag;
            this.DoBeginTag();
        }

        public virtual void RenderBeginTag(HtmlTextWriterTag tagKey)
        {
            bool flag = !this.OnTagRender(this.GetTagName(tagKey), tagKey);
            this.PushEndTag(tagKey);
            this.DoBeginTag();
            this.TagIgnore = flag;
        }

        private void WriteIfNotNull(string s)
        {
            if (s == null)
                return;
            this.Write(s);
        }

        private void DoBeginTag()
        {
            this.WriteIfNotNull(this.RenderBeforeTag());
            if (!this.TagIgnore)
            {
                this.WriteBeginTag(this.TagName);
                this.FilterAttributes();
                HtmlTextWriterTag htmlTextWriterTag = this.TagKey < (HtmlTextWriterTag) HtmlTextWriter.tags.Length
                    ? this.TagKey
                    : HtmlTextWriterTag.Unknown;
                switch (HtmlTextWriter.tags[(int) htmlTextWriterTag].tag_type)
                {
                    case HtmlTextWriter.TagType.Block:
                        this.Write('>');
                        this.WriteLine();
                        ++this.Indent;
                        break;
                    case HtmlTextWriter.TagType.Inline:
                        this.Write('>');
                        break;
                    case HtmlTextWriter.TagType.SelfClosing:
                        this.Write(" />");
                        break;
                }
            }

            this.WriteIfNotNull(this.RenderBeforeContent());
        }

        public virtual void RenderEndTag()
        {
            this.WriteIfNotNull(this.RenderAfterContent());
            if (!this.TagIgnore)
            {
                HtmlTextWriterTag htmlTextWriterTag = this.TagKey < (HtmlTextWriterTag) HtmlTextWriter.tags.Length
                    ? this.TagKey
                    : HtmlTextWriterTag.Unknown;
                switch (HtmlTextWriter.tags[(int) htmlTextWriterTag].tag_type)
                {
                    case HtmlTextWriter.TagType.Block:
                        --this.Indent;
                        this.WriteLineNoTabs(string.Empty);
                        this.WriteEndTag(this.TagName);
                        break;
                    case HtmlTextWriter.TagType.Inline:
                        this.WriteEndTag(this.TagName);
                        break;
                }
            }

            this.WriteIfNotNull(this.RenderAfterTag());
            this.PopEndTag();
        }

        public virtual void WriteAttribute(string name, string value, bool fEncode)
        {
            this.Write(' ');
            this.Write(name);
            if (value == null)
                return;
            this.Write("=\"");
            value = this.EncodeAttributeValue(value, fEncode);
            this.Write(value);
            this.Write('"');
        }

        public virtual void WriteBeginTag(string tagName)
        {
            this.Write('<');
            this.Write(tagName);
        }

        public virtual void WriteEndTag(string tagName)
        {
            this.Write("</");
            this.Write(tagName);
            this.Write('>');
        }

        public virtual void WriteFullBeginTag(string tagName)
        {
            this.Write('<');
            this.Write(tagName);
            this.Write('>');
        }

        public virtual void WriteStyleAttribute(string name, string value)
        {
            this.WriteStyleAttribute(name, value, false);
        }

        public virtual void WriteStyleAttribute(string name, string value, bool fEncode)
        {
            this.Write(name);
            this.Write(':');
            this.Write(this.EncodeAttributeValue(value, fEncode));
            this.Write(';');
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.OutputTabs();
            this.b.Write(buffer, index, count);
        }

        public override void Write(double value)
        {
            this.OutputTabs();
            this.b.Write(value);
        }

        public override void Write(char value)
        {
            this.OutputTabs();
            this.b.Write(value);
        }

        public override void Write(char[] buffer)
        {
            this.OutputTabs();
            this.b.Write(buffer);
        }

        public override void Write(int value)
        {
            this.OutputTabs();
            this.b.Write(value);
        }

        public override void Write(string format, object arg0)
        {
            this.OutputTabs();
            this.b.Write(format, arg0);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            this.OutputTabs();
            this.b.Write(format, arg0, arg1);
        }

        public override void Write(string format, params object[] arg)
        {
            this.OutputTabs();
            this.b.Write(format, arg);
        }

        public override void Write(string s)
        {
            this.OutputTabs();
            this.b.Write(s);
        }

        public override void Write(long value)
        {
            this.OutputTabs();
            this.b.Write(value);
        }

        public override void Write(object value)
        {
            this.OutputTabs();
            this.b.Write(value);
        }

        public override void Write(float value)
        {
            this.OutputTabs();
            this.b.Write(value);
        }

        public override void Write(bool value)
        {
            this.OutputTabs();
            this.b.Write(value);
        }

        public virtual void WriteAttribute(string name, string value)
        {
            this.WriteAttribute(name, value, false);
        }

        public override void WriteLine(char value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public override void WriteLine(long value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public override void WriteLine(object value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public override void WriteLine(double value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            this.OutputTabs();
            this.b.WriteLine(buffer, index, count);
            this.newline = true;
        }

        public override void WriteLine(char[] buffer)
        {
            this.OutputTabs();
            this.b.WriteLine(buffer);
            this.newline = true;
        }

        public override void WriteLine(bool value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public override void WriteLine()
        {
            this.OutputTabs();
            this.b.WriteLine();
            this.newline = true;
        }

        public override void WriteLine(int value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.OutputTabs();
            this.b.WriteLine(format, arg0, arg1);
            this.newline = true;
        }

        public override void WriteLine(string format, object arg0)
        {
            this.OutputTabs();
            this.b.WriteLine(format, arg0);
            this.newline = true;
        }

        public override void WriteLine(string format, params object[] arg)
        {
            this.OutputTabs();
            this.b.WriteLine(format, arg);
            this.newline = true;
        }
        
        public override void WriteLine(uint value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public override void WriteLine(string s)
        {
            this.OutputTabs();
            this.b.WriteLine(s);
            this.newline = true;
        }

        public override void WriteLine(float value)
        {
            this.OutputTabs();
            this.b.WriteLine(value);
            this.newline = true;
        }

        public void WriteLineNoTabs(string s)
        {
            this.b.WriteLine(s);
            this.newline = true;
        }

        public override System.Text.Encoding Encoding
        {
            get { return this.b.Encoding; }
        }

        public int Indent
        {
            get { return this.indent; }
            set { this.indent = value; }
        }

        public System.IO.TextWriter InnerWriter
        {
            get { return this.b; }
            set { this.b = value; }
        }

        public override string NewLine
        {
            get { return this.b.NewLine; }
            set { this.b.NewLine = value; }
        }

        protected HtmlTextWriterTag TagKey
        {
            get
            {
                if (this.tagstack_pos == -1)
                    throw new System.InvalidOperationException();
                return this.tagstack[this.tagstack_pos].key;
            }
            set
            {
                this.tagstack[this.tagstack_pos].key = value;
                this.tagstack[this.tagstack_pos].name = this.GetTagName(value);
            }
        }

        protected string TagName
        {
            get
            {
                if (this.tagstack_pos == -1)
                    throw new System.InvalidOperationException();
                return this.tagstack[this.tagstack_pos].name;
            }
            set
            {
                this.tagstack[this.tagstack_pos].name = value;
                this.tagstack[this.tagstack_pos].key = this.GetTagKey(value);
                if (this.tagstack[this.tagstack_pos].key == HtmlTextWriterTag.Unknown)
                    return;
                this.tagstack[this.tagstack_pos].name = this.GetTagName(this.tagstack[this.tagstack_pos].key);
            }
        }

        private bool TagIgnore
        {
            get
            {
                if (this.tagstack_pos == -1)
                    throw new System.InvalidOperationException();
                return this.tagstack[this.tagstack_pos].ignore;
            }
            set
            {
                if (this.tagstack_pos == -1)
                    throw new System.InvalidOperationException();
                this.tagstack[this.tagstack_pos].ignore = value;
            }
        }

        /*
        internal HttpWriter GetHttpWriter()
        {
          return this.b as HttpWriter;
        }
        */

        private void NextStyleStack()
        {
            if (this.styles == null)
                this.styles = new HtmlTextWriter.AddedStyle[16];
            if (++this.styles_pos < this.styles.Length)
                return;
            HtmlTextWriter.AddedStyle[] addedStyleArray = new HtmlTextWriter.AddedStyle[this.styles.Length * 2];
            System.Array.Copy((System.Array) this.styles, (System.Array) addedStyleArray, this.styles.Length);
            this.styles = addedStyleArray;
        }

        private void NextAttrStack()
        {
            if (this.attrs == null)
                this.attrs = new HtmlTextWriter.AddedAttr[16];
            if (++this.attrs_pos < this.attrs.Length)
                return;
            HtmlTextWriter.AddedAttr[] addedAttrArray = new HtmlTextWriter.AddedAttr[this.attrs.Length * 2];
            System.Array.Copy((System.Array) this.attrs, (System.Array) addedAttrArray, this.attrs.Length);
            this.attrs = addedAttrArray;
        }

        private void NextTagStack()
        {
            if (this.tagstack == null)
                this.tagstack = new HtmlTextWriter.AddedTag[16];
            if (++this.tagstack_pos < this.tagstack.Length)
                return;
            HtmlTextWriter.AddedTag[] addedTagArray = new HtmlTextWriter.AddedTag[this.tagstack.Length * 2];
            System.Array.Copy((System.Array) this.tagstack, (System.Array) addedTagArray, this.tagstack.Length);
            this.tagstack = addedTagArray;
        }

        public virtual bool IsValidFormAttribute(string attribute)
        {
            return true;
        }

        public virtual void WriteBreak()
        {
            this.WriteBeginTag(this.GetTagName(HtmlTextWriterTag.Br));
            this.Write(" />");
        }

        public virtual void WriteEncodedText(string text)
        {
            this.Write(System.Web.HttpUtility.HtmlEncode(text));
        }


        public virtual void WriteEncodedUrl(string url)
        {
            int i = url.IndexOf('?');
            if (i != -1)
            {
                WriteUrlEncodedString(url.Substring(0, i), false);
                Write(url.Substring(i));
            }
            else
            {
                WriteUrlEncodedString(url, false);
            }
        }


        public virtual void WriteEncodedUrlParameter(string urlText)
        {
            WriteUrlEncodedString(urlText, true);
        }


        protected void WriteUrlEncodedString(string text, bool argument)
        {
            int length = text.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = text[i];
                if (IsUrlSafeChar(ch))
                {
                    Write(ch);
                }
                else if (!argument &&
                         (ch == '/' ||
                          ch == ':' ||
                          ch == '#' ||
                          ch == ','
                         )
                )
                {
                    Write(ch);
                }
                else if (ch == ' ' && argument)
                {
                    Write('+');
                }
                // for chars that their code number is less than 128 and have
                // not been handled above
                else if ((ch & 0xff80) == 0)
                {
                    Write('%');
                    Write(IntToHex((ch >> 4) & 0xf));
                    Write(IntToHex((ch) & 0xf));
                }
                else
                {
                    // VSWhidbey 448625: For DBCS characters, use UTF8 encoding
                    // which can be handled by IIS5 and above.
                    Write(UrlEncodeNonAscii(char.ToString(ch), System.Text.Encoding.UTF8));
                }
            }
        }


        // Set of safe chars, from RFC 1738.4 minus '+'
        private static bool IsUrlSafeChar(char ch)
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
                return true;

            switch (ch)
            {
                case '-':
                case '_':
                case '.':
                case '!':
                case '*':
                case '(':
                case ')':
                    return true;
            }

            return false;
        }

        public static char IntToHex(int n)
        {
            if (n <= 9)
                return (char) (n + (int) '0');
            else
                return (char) (n - 10 + (int) 'a');
        }

        private static bool IsNonAsciiByte(byte b)
        {
            return (b >= 0x7F || b < 0x20);
        }


        private static byte[] UrlEncodeBytesToBytesInternalNonAscii(byte[] bytes, int offset, int count,
            bool alwaysCreateReturnValue)
        {
            int cNonAscii = 0;

            // count them first
            for (int i = 0; i < count; i++)
            {
                if (IsNonAsciiByte(bytes[offset + i]))
                {
                    cNonAscii++;
                }
            }

            // nothing to expand?
            if (!alwaysCreateReturnValue && cNonAscii == 0)
            {
                return bytes;
            }

            // expand not 'safe' characters into %XX, spaces to +s
            byte[] expandedBytes = new byte[count + cNonAscii * 2];
            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];

                if (IsNonAsciiByte(b))
                {
                    expandedBytes[pos++] = (byte) '%';
                    expandedBytes[pos++] = (byte) IntToHex((b >> 4) & 0xf);
                    expandedBytes[pos++] = (byte) IntToHex(b & 0x0f);
                }
                else
                {
                    expandedBytes[pos++] = b;
                }
            }

            return expandedBytes;
        }


        //  Helper to encode the non-ASCII url characters only
        private static string UrlEncodeNonAscii(string str, System.Text.Encoding e)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (e == null)
            {
                e = System.Text.Encoding.UTF8;
            }

            byte[] bytes = e.GetBytes(str);
            bytes = UrlEncodeBytesToBytesInternalNonAscii(bytes, 0, bytes.Length, false);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }


        //[MonoNotSupported("")]
        //public virtual void EnterStyle(Style style)
        //{
        //  throw new NotImplementedException();
        //}

        //[MonoNotSupported("")]
        //public virtual void EnterStyle(Style style, HtmlTextWriterTag tag)
        //{
        //  throw new NotImplementedException();
        //}

        //[MonoNotSupported("")]
        //public virtual void ExitStyle(Style style)
        //{
        //  throw new NotImplementedException();
        //}


        //[MonoNotSupported("")]
        //public virtual void ExitStyle(Style style, HtmlTextWriterTag tag)
        //{
        //  throw new NotImplementedException();
        //}


        public virtual void BeginRender()
        {
        }

        public virtual void EndRender()
        {
        }

        private struct AddedTag
        {
            public string name;
            public HtmlTextWriterTag key;
            public bool ignore;
        }

        private struct AddedStyle
        {
            public string name;
            public HtmlTextWriterStyle key;
            public string value;
        }

        private struct AddedAttr
        {
            public string name;
            public HtmlTextWriterAttribute key;
            public string value;
        }

        private enum TagType
        {
            Block,
            Inline,
            SelfClosing,
        }

        private sealed class HtmlTag
        {
            public readonly HtmlTextWriterTag key;
            public readonly string name;
            public readonly HtmlTextWriter.TagType tag_type;

            public HtmlTag(HtmlTextWriterTag k, string n, HtmlTextWriter.TagType tt)
            {
                this.key = k;
                this.name = n;
                this.tag_type = tt;
            }
        }

        private sealed class HtmlStyle
        {
            public readonly HtmlTextWriterStyle key;
            public readonly string name;

            public HtmlStyle(HtmlTextWriterStyle k, string n)
            {
                this.key = k;
                this.name = n;
            }
        }

        private sealed class HtmlAttribute
        {
            public readonly HtmlTextWriterAttribute key;
            public readonly string name;

            public HtmlAttribute(HtmlTextWriterAttribute k, string n)
            {
                this.key = k;
                this.name = n;
            }
        }
    }
}