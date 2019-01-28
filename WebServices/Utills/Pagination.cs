
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServices.Utills
{
    public class Pagination
    {
        public List<PageNumber> GetPageination(decimal listCount, int pageNo, int take = 10)
        {
            List<PageNumber> lp = new List<PageNumber>();
            decimal val = listCount / take;
            int totalCount = Numerics.GetInt(Math.Ceiling(val));

            int limit = pageNo + 4;


            if (limit > totalCount)
                limit = totalCount;

            if (limit <= 1)
            {
                limit = 5;
                pageNo = 1;
            }

            if (pageNo < 1) pageNo = 1;

            ///////////////////////////////////////
            if (totalCount < 5 && totalCount >= 0)
                limit = totalCount;

            for (int i = pageNo; i <= limit; i++)
            {
                PageNumber pn = new PageNumber();
                pn.PageNo = i + "";

                if (pageNo == i)
                    pn.SelectedClass = "active";

                if (i != pageNo || i == 1)
                {

                    pn.Overflow = "grid-overflow";

                }
                else
                {
                    int prevPageNo = i - 5;

                    if (prevPageNo < 1)
                        prevPageNo = 1;

                    pn.PageNoPrev = prevPageNo + "";
                    pn.Overflow = "";


                }

                if (i < limit || i == totalCount)
                {
                    pn.OverflowForward = "grid-overflow";
                }


                else
                {
                    int forwdPageNo = limit + 1;
                    if (forwdPageNo > totalCount)
                    {
                        forwdPageNo = totalCount;
                    }

                    pn.PageNoFw = forwdPageNo + "";

                }





                lp.Add(pn);
            }

            return lp;

        }

        public List<PageNumber> GetPageination(string key, decimal listCount, int pageNo, int take = 10)
        {
            List<PageNumber> lp = new List<PageNumber>();

            decimal val = listCount / take;
            int totalCount = Numerics.GetInt(Math.Ceiling(val));

            int limit = pageNo + 4;


            if (limit > totalCount)
                limit = totalCount;

            if (limit <= 1)
            {
                limit = 5;
                pageNo = 1;
            }

            if (pageNo < 1) pageNo = 1;

            ///////////////////////////////////////
            if (totalCount < 5 && totalCount >= 0)
                limit = totalCount;

            for (int i = pageNo; i <= limit; i++)
            {
                PageNumber pn = new PageNumber();
                pn.Key = key + "";
                pn.PageNo = i + "";

                if (pageNo == i)
                    pn.SelectedClass = "active";

                if (i != pageNo || i == 1)
                {

                    pn.Overflow = "grid-overflow";

                }
                else
                {
                    int prevPageNo = i - 5;

                    if (prevPageNo < 1)
                        prevPageNo = 1;

                    pn.PageNoPrev = prevPageNo + "";
                    pn.Overflow = "";


                }

                if (i < limit || i == totalCount)
                {
                    pn.OverflowForward = "grid-overflow";
                }


                else
                {
                    int forwdPageNo = limit + 1;
                    if (forwdPageNo > totalCount)
                    {
                        forwdPageNo = totalCount;
                    }

                    pn.PageNoFw = forwdPageNo + "";

                }





                lp.Add(pn);
            }

            return lp;

        }
    }

     public class PageNumber
     {
         public String PageNoPrev { set; get; }
         public String PageNoFw { set; get; }
         public String PageNo { set; get; }
         public String SelectedClass { set; get; }

         public String Overflow { set; get; }
         public String OverflowForward { set; get; }
         public String Key { set; get; }// for get records with multiple selection
     }
}
