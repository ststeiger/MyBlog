
namespace WikiPlex.Legacy
{
    
    
    /// <summary><para>Specifies the horizontal alignment of items within a container.</para></summary>
    //[TypeConverter("System.Web.UI.WebControls.HorizontalAlignConverter")]
    public enum HorizontalAlign
    {
        /// <summary><para>The horizontal alignment is not set.</para></summary>
        NotSet,
        /// <summary><para>The contents of a container are left justified.</para></summary>
        Left,
        /// <summary><para>The contents of a container are centered.</para></summary>
        Center,
        /// <summary><para>The contents of a container are right justified.</para></summary>
        Right,
        /// <summary><para>The contents of a container are uniformly spread out and aligned with both the left and right margins.</para></summary>
        Justify,
    }
    
    
}