using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Translator
{
    static class Translit
    {
        private static string[] patterns = new string[27];
        private static string[] RusRegisterReplace = new string[27];
        private static string[] DigitalRegisterReplace = new string[27];
        private static string pattern = "9ABWGDEVZIJKLMNOPRSTUFHYXQC";
        private static string RusRegister = " АБВГДЕЖЗИЙКЛМНОПРСТУФХЫЬЯЦ";
        private static string DigitalRegister = " -?2Ш*3=+8Ю().,904!57ЭЩ6/1:";
        
        static Translit()
        {
            for (int i = 0; i < 27; ++i)
            {
                patterns[i] = pattern[i].ToString();
                RusRegisterReplace[i] = RusRegister[i].ToString();
                DigitalRegisterReplace[i] = DigitalRegister[i].ToString();
            }
        }

        public static string GetTranslit(string telegram)
        {
            var splitsByRusRegister = Regex.Split(telegram, @"/");
            string resultStr = "";

            foreach (var str in splitsByRusRegister)
            {
                var splitsByDigitalRegister = Regex.Split(str, @"5");

                int i = 0;
                foreach (var s in splitsByDigitalRegister)
                {
                    var h = Regex.Replace(s, "43", "\n\t");

                    if (i == 0)
                    {
                        resultStr += PregReplace(h, patterns, RusRegisterReplace);
                    }
                    else
                    {
                        var splitsByLatRegister = Regex.Split(h, @"8");

                        int r = 0;
                        foreach (var st in splitsByLatRegister)
                        {
                            resultStr += r == 0
                                ? PregReplace(st, patterns, DigitalRegisterReplace)
                                : Regex.Replace(st, @"9", " ");
                            
                            ++r;
                        }
                    }
                    ++i;
                }
            }

            return resultStr;
        }

        private static string PregReplace(string input, string[] pattern, string[] replacements)
        {
            if (replacements.Length != pattern.Length)
                throw new ArgumentException("Replacement and Pattern Arrays must be balanced");

            for (int i = 0; i < pattern.Length; i++)
            {
                input = Regex.Replace(input, pattern[i], replacements[i]);
            }

            return input;
        }
    }

	//class Program
	//{
        
	//	static void Main()
	//	{
	//		string telegram = "3A9/GLAWNOKOMANDOWANIE9WOENNO5A/MORSKOGO9FLOTA95K8OBERKOMMANDO9DER9MARINE99OKM5L43A9/" +
	//			"IMPERSKOE9WEDOMSTWO9BEZOPASNOSTI95K8REICHSSICHERHEITSHAUPTAMT99RSHA5LM43/GERMANSKIE9WOENNYE9KRIPTOGRAFY" +
	//			"9TAK9I9NE9WYQSNILI9DO9KONCA5N9R/TO9PRIMENQEMYE9IMI95G/IFRMA5G/INY95SSF/NIGMA5SS9/I95G/IFROWALXNYE9" +
	//			"TELEPRINTERY9MOGUT9BYTX9DE5G/IFROWANY5M9/PRI5R/INOJ95F/TOGO9QWILOSX9TO5N9R/TO5N9/IZU5R/AQ9WOPROSY9STO" +
	//			"JKOSTI95G/IFROW5N9/ONI9NE9PO5G/LI9NA9ZNA5R/ITELXNYE9ZATRATY5N9/KOTORYH9TREBOWALO9PRAKTI5R/ESKOE9DE5G/IF" +
	//			"ROWANIE9UKAZANNYH95G/IFRSISTEM5M9/STOJKOSTX9OCENIWALASX95R/ISTO9TEORETI5R/ESKI5N9/TAK9KAK9SPECIALISTY5A/" +
	//			"KRIPTOANALITIKI9NE9RASPOLAGALI9DOSTATO5R/NYM9OBEMOM9PEREPISKI9DLQ9ANALIZA5M9/NEMCY9NE9IMELI9PRAKTI5R/ESKO" +
	//			"GO9PREDSTAWLENIQ9O9TOM5N9/KAK9T5H/ATELXNYJ9I9CELENAPRAWLENNYJ9ANALIZ9PEREHWATYWAEMOJ95G/IFROWANNOJ9INFOR" +
	//			"MACII9POZWOLQET9NAJTI9PODHODQ5H/IE95SS/STANDARTY5SS9/I9DRUGIE9DANNYE9DLQ9USPE5G/NOJ9KRIPTOANALITI5R/ESKOJ" +
	//			"9ATAKI5M9943/W9DOKLADE9O9WOENNO5A/MORSKIH95G/IFRAH5N9/DATIROWANNOM95QP9/I5J/LQ95QORR9/G5M9/I9PODGOTOWLENNO" +
	//			"M9SLUVBOJ9BEZOPASNOSTI9SWQZI9WERHOWNOGO9KOMANDOWANIQ9WMS95K8OKM5XR98SKL5X8II5LN9/UTWERVDALOSX5N9R/TO9WOZMO" +
	//			"VNOSTX9WZLOMA9WOEN";

	//		Console.WriteLine(Translit.GetTranslit(telegram));

	//		string telegram1 = "KAK9T5H/ATELXNYJ9I9CELENAPRAWLENNYJ9ANALIZ9PEREHWATYWAEMOJ95G/IFROWANNOJ9INFOR" +
	//			"MACII9POZWOLQET9NAJTI9PODHODQ5H/IE95SS/STANDARTY5SS9/I9DRUGIE9DANNYE9DLQ9USPE5G/NOJ9KRIPTOANALITI5R/ESKOJ" +
	//			"9ATAKI5M9943/W9DOKLADE9O9WOENNO5A/MORSKIH95G/IFRAH5N9/DATIROWANNOM95QP9/I5J/LQ95QORR9/G5M9/I9PODGOTOWLENNO" +
	//			"M9SLUVBOJ9BEZOPASNOSTI9SWQZI9WERHOWNOGO9KOMANDOWANIQ9WMS95K8OKM5XR98SKL5X8II5LN9/UTWERVDALOSX5N9R/TO9WOZMO" +
	//			"VNOSTX9WZLOMA9WOEN";

	//		Console.WriteLine(Translit.GetTranslit(telegram1));
	//	}
	//}
}
