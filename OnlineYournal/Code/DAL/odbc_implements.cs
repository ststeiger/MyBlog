
namespace MyBlogCore
{


    internal class odbc_implements
    {
        // Credits: http://web.archive.org/web/20100123183531/http://blogs.msdn.com/bclteam/archive/2005/03/15/396452.aspx
        internal const string strOdbcMatchingPattern = "({fn\\s*(.+?)\\s*\\(([^{}]*(((?<Open>{)[^{}]*)+((?<Close-Open>})[^{}]*)+)*(?(Open)(?!)))\\s*\\)\\s*})";



        // MsgBox(String.Join(Environment.NewLine, GetArguments("bla")));
        internal static string[] GetArguments(string strAllArguments)
        {
            string EscapeCharacter = System.Convert.ToChar(8).ToString();

            strAllArguments = strAllArguments.Replace("''", EscapeCharacter);

            bool bInString = false;
            int iLastSplitAt = 0;

            System.Collections.Generic.List<string> lsArguments = new System.Collections.Generic.List<string>();


            int iInFunction = 0;

            for (int i = 0; i < strAllArguments.Length; i++)
            {
                char strCurrentChar = strAllArguments[i];

                if (strCurrentChar == '\'')
                    bInString = !bInString;


                if (bInString)
                    continue;


                if (strCurrentChar == '(')
                    iInFunction += 1;


                if (strCurrentChar == ')')
                    iInFunction -= 1;



                if (strCurrentChar == ',')
                {

                    if (iInFunction == 0)
                    {
                        string strExtract = "";
                        if (iLastSplitAt != 0)
                        {
                            strExtract = strAllArguments.Substring(iLastSplitAt + 1, i - iLastSplitAt - 1);
                        }
                        else
                        {
                            strExtract = strAllArguments.Substring(iLastSplitAt, i - iLastSplitAt);
                        }

                        strExtract = strExtract.Replace(EscapeCharacter, "''");
                        lsArguments.Add(strExtract);
                        iLastSplitAt = i;
                    } // End if (iInFunction == 0)

                } // End if (strCurrentChar == ',')

            } // Next i


            string strExtractLast = "";
            if (lsArguments.Count > 0)
            {
                strExtractLast = strAllArguments.Substring(iLastSplitAt + 1);
            }
            else
            {
                strExtractLast = strAllArguments.Substring(iLastSplitAt);
            }

            strExtractLast = strExtractLast.Replace(EscapeCharacter, "''");
            lsArguments.Add(strExtractLast);

            string[] astrResult = lsArguments.ToArray();
            lsArguments.Clear();
            lsArguments = null;

            return astrResult;
        } // End Function GetArguments

    }


}
