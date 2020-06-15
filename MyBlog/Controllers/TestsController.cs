
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyBlog.Controllers
{


    public class TestsController : Controller
    {


        public static List<string> GetWebsafeFonts()
        {
            List<string> fonts = new List<string>();
            // All rest: Sans Serif
            fonts.Add("Arial");
            fonts.Add("Arial Black");
            fonts.Add("Book Antiqua");
            fonts.Add("Comic Sans MS");
            fonts.Add("Courier New"); //Monospace Fonts
            fonts.Add("Georgia"); // Serif Fonts
            fonts.Add("Impact");
            fonts.Add("Lucida Console");// Monospace Fonts
            fonts.Add("Lucida Sans Unicode");
            fonts.Add("Palatino Linotype"); // Serif Fonts
            fonts.Add("Tahoma");
            fonts.Add("Times New Roman"); // Serif Fonts
            fonts.Add("Trebuchet MS");
            fonts.Add("Verdana");

            return fonts;
        } // End Function GetWebsafeFonts


        public string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) 
                throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");

            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            
            throw new ArgumentOutOfRangeException("something bad happened");
        }
        
        
        // http://www.periodni.com/roman_numerals_converter.html
        // In the Roman numeral system, the basic "digits" are the letters I, V, X, L, C, D,
        // and M which represent the same numbers regardless of their position.
        // Symbols are placed in order of value, starting with the largest values.
        // When the higher numeral is placed before a lower numeral, the values of each Roman numeral are added.
        // When smaller values precede larger values, the smaller values are subtracted from the larger values, and the result is added to the total.
        // Do not repeat I, X, and C more than three times in a row. (Number 4 on a Roman numeral clock is usually written as IIII. )
        // Symbols V, L, and D cannot appear more than once consecutively.
        // Do not subtract a number from one that is more than 10 times greater:
        // I may only precede V and X, X may only precede L and C, and C may only precede D and M.
        public long ToArabic(string strRomanNumeral)
        {
            long lngTotal = 0;

            char[] aFivers = new char[] { 'V', 'L', 'D', 'A' };
            // Symbols V, L, and D cannot appear more than once consecutively.

            char[] aTenners = new char[] { 'I', 'X', 'C', 'M' };
            // Do not repeat I, X, and C more than three times in a row

            char[] aAllDigits = new char[aFivers.Length + aTenners.Length];
            long[] aFiversValue = new long[aFivers.Length];
            long[] aTennersValue = new long[aTenners.Length];

            for (int i = 0; i < aFivers.Length; ++i)
            {
                aFiversValue[i] = (int)(5.0 * Math.Pow((double)10, (double)i));
            } // Next i

            for (int i = 0; i < aTenners.Length; ++i)
            {
                aTennersValue[i] = (int)(Math.Pow((double)10, (double)i));
            } // Next i

            Array.Copy(aFivers, 0, aAllDigits, 0, aFivers.Length);
            Array.Copy(aTenners, 0, aAllDigits, aFivers.Length, aTenners.Length);


            foreach (char cMustContain in strRomanNumeral.ToCharArray())
            {
                bool bDoesContain = false;
                foreach (char cDoesContain in aAllDigits)
                {
                    if (cDoesContain == cMustContain)
                    {
                        bDoesContain = true;
                        break;
                    }
                }

                if (!bDoesContain)
                    throw new InvalidCastException(cMustContain.ToString() + " is not a defined roman numeral.");
            } // Next cMustContain


            long[] ba = new long[strRomanNumeral.Length];
            for (int i = 0; i < strRomanNumeral.Length; ++i)
            {
                bool bFound = false;

                for (int j = 0; j < aFivers.Length; ++j)
                {
                    if (aFivers[j] == strRomanNumeral[i])
                    {
                        ba[i] = aFiversValue[j];
                        bFound = true;
                        break;
                    }
                } // Next j

                if (bFound)
                    continue;

                for (int j = 0; j < aTenners.Length; ++j)    
                {
                    if (aTenners[j] == strRomanNumeral[i])
                    {
                        ba[i] = aTennersValue[j];
                        break;
                    }
                } // Next j

            } // Next i

            for (int i = 0; i < ba.Length; ++i)
            {

                // Prevents LM, VX, IVC, MDM
                if (ba[i].ToString().StartsWith("5"))
                {
                    for (int v = i; v < ba.Length; ++v)
                    {
                        if (ba[v] > ba[i])
                            throw new ArgumentException("In a valid roman numeral, subtracting any fiver is prohibited.");
                    }

                    // may not contain greater number to the right
                } // End no lower to fiver check

                
                // Prevents CMC, but allows XLIX
                for (int j = 0; j < i; ++j)
                { 
                    if(ba[j] < ba[i])
                    {
                        for(int k= i; k < ba.Length; ++k)
                        {

                            if (ba[j] == ba[k] && ba[k-1] >= ba[k] )
                                throw new ArgumentException("In a valid roma numeral, a subtracted number may not reappear later for adding.");
                        }
                    }
                } // End Redundant subtract check
                
                
                if (i > 0)
                {
                    // Prevents CMIC, MIM
                    if (ba[i - 1] * 10 < ba[i])
                        throw new ArgumentException("In a valid roman numeral, you cannot subtract a number from one that is more than 10 times greater.");


                    // Prevents DD, VV, LL
                    //if (ba[i - 1].ToString().StartsWith("5") && ba[i].ToString().StartsWith("5")  )
                    if (ba[i - 1].ToString().StartsWith("5") && ba[i] == ba[i-1])
                    {
                        throw new ArgumentException("In a valid roman numeral, the same fiver cannot appear more than once consecutively.");
                    }

                } // End Redundant 5er check


                
                if (i > 1)
                {

                    // Prevents CMM
                    if (ba[i - 2] < ba[i - 1] && ba[i] == ba[i - 1])
                        throw new ArgumentException("In a valid roman numeral, values for subtraction must precede the LAST larger value.");

                    // Prevents CCM, XCM
                    if (ba[i - 1] < ba[i])
                    {
                        if (ba[i - 2] < ba[i])
                            throw new ArgumentException("In a valid roman numeral, having two or more values for subtraction from a greater number is wrong.");
                    }
                } // May not contain multiple subtract values check


                // Prevents MMMM, XVIIII
                if (i > 2)
                {
                    if(ba[i] == ba[i-1] && ba[i] == ba[i-2] && ba[i] == ba[i-3])
                        throw new ArgumentException("In a valid roman numeral, more than 3 same digits in a row are prohibited.");
                }


                // Subtract negative value
                if (i + 1 < ba.Length)
                {
                    if (ba[i] < ba[i + 1])
                    {
                        lngTotal -= ba[i];

                        // Allows CMC
                        continue;
                    }
                    
                }

                lngTotal += ba[i];
            } // Next i

            return lngTotal;
        }


        public string ConvertAll()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < 4000; ++i)
            {
                string strNumeral = ArabicToRoman(i);
                long lol = ToArabic(strNumeral);
                sb.AppendFormat("{0}\t{1}\t{2}", i, strNumeral, lol);
                sb.AppendLine();
            }

            return sb.ToString();
        }


        // http://blogs.msdn.com/b/danteg/archive/2006/03/22/558450.aspx
        public string ArabicToRoman(long lngNum)
        {
            string strRomanNumeral = "";

            char[] aFivers = new char[] { 'V', 'L', 'D', 'A' };
            // Symbols V, L, and D cannot appear more than once consecutively.

            char[] aTenners = new char[] { 'I', 'X', 'C', 'M'};
            // Do not repeat I, X, and C more than three times in a row


            long[] aFiversValue = new long[aFivers.Length];
            long[] aTennersValue = new long[aTenners.Length];

            for (int i = 0; i < aFivers.Length; ++i)
            {
                aFiversValue[i] = (int)(5.0 * Math.Pow((double)10, (double)i));
            } // Next i

            for (int i = 0; i < aTenners.Length; ++i)
            {
                aTennersValue[i] = (int)(Math.Pow((double)10, (double)i));
            } // Next i


            long nMax = 3 * aTennersValue[aTennersValue.Length - 1] + aTennersValue[aTennersValue.Length - 1] - 1;

            if (lngNum > nMax)
                throw new InvalidCastException("Range 0 - " + nMax.ToString() + " exceeded.");


            for (int i = aTennersValue.Length - 1; i > -1; --i)
            {
                int j = i - 1;
                int iTennerCount = (int)(lngNum / aTennersValue[i]);
                lngNum -= iTennerCount * aTennersValue[i];

                if (iTennerCount > 0)
                {
                    
                    if (iTennerCount > 3)
                    {
                        strRomanNumeral += aTenners[i].ToString() + aFivers[i].ToString();
                    }
                    else
                        strRomanNumeral += new string(aTenners[i], iTennerCount);
                } // End if (iTennerCount > 0)

                if (j > -1)
                {
                    long lngMaxFiverTreshold = aFiversValue[j] + 4 * aTennersValue[j];

                    if (lngNum >= lngMaxFiverTreshold)
                    {
                        strRomanNumeral += aTenners[i - 1].ToString() + aTenners[i].ToString();
                        lngNum -= lngMaxFiverTreshold;
                    }


                    int iFiverCount = (int)(lngNum / aFiversValue[j]);
                    lngNum -= iFiverCount * aFiversValue[j];

                    if (iFiverCount > 0)
                    {
                        if (iFiverCount > 3)
                        {
                            strRomanNumeral += new string(aFivers[j], 3);
                            strRomanNumeral += aFivers[j - 1].ToString() + aFivers[j].ToString();
                        }
                        else
                            strRomanNumeral += new string(aFivers[j], iFiverCount);
                    } // End if (iFiverCount > 0)

                } // End if (j > -1)

                if (lngNum == 0)
                    break;
            } // Next i

            // When smaller values precede larger values, the smaller values are subtracted from the larger values, 
            // and the result is added to the total.
            
            // Do not subtract a number from one that is more than 10 times greater: 
            //I may only precede V and X, X may only precede L and C, and C may only precede D and M.

            return strRomanNumeral;
        } // End Function Conversion


        public static string ListFonts()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            List<string> fonts = new List<string>();
            List<string> WebsafeFonts = GetWebsafeFonts();


            string strHTMLTemplate = @"

    <h5>{0}</h5>

    <span style=""font-family: {0}; font-size: 26px; font-weight: normal; font-style: normal; display: block; float: left;min-width: 200px;"">
        Search here
    </span>

    <input type=""text"" value=""Search here"" style=""font-family: {0}; font-size: 25px; font-weight: normal; font-style: normal; padding-left: 0.25cm; display: block; height: 32px;"" />

    <hr />

";


            foreach (string strFont in WebsafeFonts)
            {
                sb.AppendFormat(strHTMLTemplate, strFont);
            }

            foreach (System.Drawing.FontFamily font in System.Drawing.FontFamily.Families)
            {
                fonts.Add(font.Name);

                if (!WebsafeFonts.Contains(font.Name, StringComparer.OrdinalIgnoreCase))
                    sb.AppendFormat(strHTMLTemplate, font.Name);
                //sb.AppendLine();
            } // Next font

            return sb.ToString();
        } // End Function FontTest


        //
        // GET: /Tests/Index
        public ActionResult Index()
        {

            string strHTML = @"
<!DOCTYPE html>
<html>
    <head>
        <title></title>
    </head>
<body>

"
 + ListFonts() +
@"


</body>
</html>
";


            return Content(strHTML);
        }

    }
}
