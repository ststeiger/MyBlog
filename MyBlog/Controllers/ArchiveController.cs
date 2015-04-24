
#if false


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace MyBlog.Controllers
{
    

    public class xxx : WebMatrix.WebData.SimpleMembershipProvider
    { 
        public static WebMatrix.WebData.SimpleMembershipProvider xy = new     WebMatrix.WebData.SimpleMembershipProvider();


        public void xxxxxx()
        { 
            
        }
    
    }


    public class ArchiveController : Controller
    {


        // http://www.omniglot.com/language/numerals.htm
        // http://en.wikipedia.org/wiki/Thai_numerals
        public int GetNumeral(string strNumeral)
        {
            List<string[]> ls = new List<string[]>();

            // http://paul.wad.homepage.dk/number_converter.html

            uint a = 0;
            if (uint.TryParse(strNumeral, out a))
                return (int) a;

            // 0-9 simplified mandarin
            string[] mandarin_traditional = new string[] { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

            // 0-9 traditional mandarin
            string[] mandarin_formal = new string[] { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖", "拾" };

            // 0-9 the Suzhou numerals, or huā mă, a positional system.
            string[] mandarin_huā_mă = new string[] { "〇", "〡", "〢", "〣", "〤", "〥", "〦", "〧", "〨", "〩" };

            string[] Bengali = new string[] { "০", "১", "২", "৩", "৪", "৫", "৬", "৭", "৮", "৯" };
            string[] Devanagari = new string[] { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", "१०", "११", "१२" };
            string[] eastern_arabic = new string[] { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
            string[] Geez = new string[] { "", "፩", "፪", "፫", "፬", "፭", "፮", "፯", "፰", "፱" };
            string[] Gujarati = new string[] { "૦", "૧", "૨", "૩", "૪", "૫", "૬", "૭", "૮", "૯" };
            string[] Gurmukhi = new string[] { "੦", "੧", "੨", "੩", "੪", "੫", "੬", "੭", "੮", "੯" };
            string[] Kannada = new string[] { "೦", "೧", "೨", "೩", "೪", "೫", "೬", "೭", "೮", "೯" };
            string[] Khmer = new string[] { "០", "១", "២", "៣", "៤", "៥", "៦", "៧", "៨", "៩" };
            string[] Laos = new string[] { "໐", "໑", "໒", "໓", "໔", "໕", "໖", "໗", "໘", "໙" };
            string[] Malayalam = new string[] { "൦", "൧", "൨", "൩", "൪", "൫", "൬", "൭", "൮", "൯" };
            string[] Oriya = new string[] { "୦", "୧", "୨", "୩", "୪", "୫", "୬", "୭", "୮", "୯" };
            string[] Roman = new string[] { " ", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
            string[] Thai = new string[] { "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙", "๑๐", "๑๑", "๑๒" };
            string[] Tibetan = new string[] { "༠", "༡", "༢", "༣", "༤", "༥", "༦", "༧", "༨", "༩" };
            string[] Tamil = new string[] { "௦", "௧", "௨", "௩", "௪", "௫", "௬", "௭", "௮", "௯" };
            string[] Telugu = new string[] { "౦", "౧", "౨", "౩", "౪", "౫", "౬", "౭", "౮", "౯" };
            string[] Urdu = new string[] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            ls.Add(mandarin_traditional);
            ls.Add(mandarin_formal);
            ls.Add(mandarin_huā_mă);
            ls.Add(Bengali);
            ls.Add(Devanagari);
            ls.Add(eastern_arabic);
            ls.Add(Geez);
            ls.Add(Gujarati);
            ls.Add(Gurmukhi);
            ls.Add(Kannada);
            ls.Add(Khmer);
            ls.Add(Laos);
            ls.Add(Malayalam);
            ls.Add(Oriya);
            ls.Add(Roman);
            ls.Add(Thai);
            ls.Add(Tibetan);
            ls.Add(Tamil);
            ls.Add(Telugu);
            ls.Add(Urdu);


            foreach (string[] astrNumerals in ls)
            {
                for (int i = 0; i < astrNumerals.Length; ++i)
                {
                    if (astrNumerals[i] == strNumeral)
                        return i;
                }
            } // Next 

            return 0;
        }


        //
        // GET: /Archive/
        public ActionResult Index(string year, string month, string day)
        {
            int iMonth = GetNumeral(month);
            int iDay = GetNumeral(day);

            Console.WriteLine(month);
            Console.WriteLine(day);
            //return View();
            return Content("Month: " + iMonth.ToString() + Environment.NewLine + "Day: " + iDay.ToString());
        }


    }


}


#endif
