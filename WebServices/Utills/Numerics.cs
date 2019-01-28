using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WebServices.Utills
{
    public static class Numerics
    {
        public static int GetInt(object input)
        {
            if (input == null || input + "" == "") return 0;
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double GetDouble(object input)
        {
            if (input == null || input + "" == "") return 0;
            try
            {
                return Convert.ToDouble(input);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool GetBool(object input)
        {
            if (input == null || input + "" == "") return false;
            try
            {
                return Convert.ToBoolean(input);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static decimal Getdecimal(object input)
        {
            if (input == null || input + "" == "") return 0;
            try
            {
                return Convert.ToDecimal(input);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string GetString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return input;
        }

        public static string GetDate(DateTime dt)
        {

            var ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one sec ago" : ts.Seconds
                + " seconds ago";
            }
            if (delta < 120)
            {
                return "a min ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " min ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400)
            { // 24 * 60 * 60
                return ts.Hours + " hours ago";
            }
            if (delta < 172800)
            { // 48 * 60 * 60
                return "yesterday";
            }
            if (delta < 2592000)
            { // 30 * 24 * 60 * 60
                return ts.Days + " days ago";
            }
            if (delta < 31104000)
            { // 12 * 30 * 24 * 60 * 60
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }

        public static string AddComas(string data)
        {
            try
            {
                data = ReverseString(data);
                char[] value = data.ToArray();
                int strLength = data.Length;
                string tempaar = "";

                for (int i = strLength; i > 0; i--)
                {
                    if (i % 3 == 0 && i != strLength)
                    {
                        tempaar += ",";
                        tempaar += value[i - 1].ToString();
                    }
                    else
                    {
                        tempaar += value[i - 1].ToString();
                    }
                }

                return tempaar;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        public static DateTime ConvertDateTime(string date)
        {
    //        DateTime date2 = Convert.ToDateTime(dateString,
    //System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);

            var datetime = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return datetime;
        }


        public static DateTime ConvertDateFromddMMyyyy(string date_in_ddMMyyyy)
        {
            DateTime dt = DateTime.ParseExact(date_in_ddMMyyyy, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return dt;
        }

        public static DateTime ConvertDateFromddMMyyyyDashed(string date_in_ddMMyyyy)
        {
            DateTime dt = DateTime.ParseExact(date_in_ddMMyyyy, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return dt;
        }

        public static DateTime ConvertToDateTimeddMMMyyyy(DateTime dt)
        {
            DateTime dt2 = DateTime.ParseExact(dt.ToString("dd/MM/yyyy"), "dd MM yyyy", CultureInfo.InvariantCulture);
            return dt2;

        }


        //public static string ToJSON(this object obj)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(obj);
        //}

        //public static string ToJSON(this object obj, int recursionDepth)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    serializer.RecursionLimit = recursionDepth;
        //    return serializer.Serialize(obj);
        //}

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }


        public static List<string> MonthsIntToString(List<int?> list)
        {
            var returnList = new List<string>();
            foreach (var item in list)
            {
                if (item != null)
                {
                    var name = GetMonthName(GetInt(item));
                    returnList.Add(name);
                }
            }

            return returnList;
        }

        public static string GetMonthName(int monthid)
        {
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthid);
            return month;

        }

        public static int GetDaysInMonth(int year, int month, string day)
        {
            DayOfWeek dayName = StringToDayOfWeek(day);
            int days = 0;
            CultureInfo ci = new CultureInfo("en-US");
            for (int i = 1; i <= ci.Calendar.GetDaysInMonth(year, month); i++)
            {
                var date = new DateTime(year, month, i);
                if (date.DayOfWeek == dayName && date.Date <= DateTime.Now.Date)
                    days++;
            }
            return days;
        }
        public static int GetDaysInTwoDates(DateTime dt1, DateTime dt2, string strgday)
        {
            DayOfWeek dayName = StringToDayOfWeek(strgday);
            int days = 0;
            CultureInfo ci = new CultureInfo("en-US");
            for (var day = dt1.Date; day <= dt2.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek == dayName && day.Date <= DateTime.Now.Date)
                    days++;
            }
            return days;
        }


        public static DayOfWeek StringToDayOfWeek(string day)
        {
            var d = day.ToLower();
            DayOfWeek dayOfWeek = DateTime.Today.DayOfWeek;
            switch (d)
            {
                case "monday":
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case "tuesday":
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case "wednesday":
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case "thursday":
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case "friday":
                    dayOfWeek = DayOfWeek.Friday;
                    break;
                case "saturday":
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                case "sunday":
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
            }
            return dayOfWeek;
        }

        public static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }

            catch (Exception exReleaseObject)
            {
                obj = null;
                //   Console.WriteLine(CMSResourceFile.REALESE_FAILED+ exReleaseObject);  

            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static string Base64Encode(string input)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string input)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        
    }
}
