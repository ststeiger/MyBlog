
namespace MyBlogCore
{


    internal class fb_implements
    {

        // Firebird 2.5, 3.5 supports SQL-Std. paging.
        internal static string PagingTemplate(ulong offset, ulong rows)
        {
            offset++;

            // rows (10 + 1) to 20
            return string.Concat(
                  "\r\nrows("
                , offset.ToString(System.Globalization.CultureInfo.InvariantCulture)
                , ") to "
                , rows.ToString(System.Globalization.CultureInfo.InvariantCulture)
                , " \r\n"
            );
        }
    }


}
