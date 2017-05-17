using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MarketSimulator
{
    [Serializable]
    public class QuoteData
    {
        public string symbol;
        public DateTime startDate;
        public DateTime endDate;
        public List<Quote> quoteList = new List<Quote>(4000);
        public List<Split> splitList = new List<Split>(8); //max splits 11, avg 2

        public int missingDays = 0;
        public int maxConsecutiveMissingDays = 0;
        public int extraDays = 0;

        public QuoteData()
        {
        }

        public QuoteData(string symbol)
        {
            this.symbol = symbol;
        }

        public Quote GetQuote(DateTime date)
        {
            int low = 0;
            int high = quoteList.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                DateTime midVal = quoteList[mid].date;

                if (midVal < date)
                {
                    low = mid + 1;
                }
                else if (midVal > date)
                {
                    high = mid - 1;
                }
                else
                {
                    return quoteList[mid]; // key found
                }
            }
            return null;  // key not found.
        }

        public Quote GetPreviousQuote(DateTime date)
        {
            int low = 0;
            int high = quoteList.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                DateTime midVal = quoteList[mid].date;

                if (midVal < date)
                {
                    low = mid + 1;
                }
                else if (midVal > date)
                {
                    high = mid - 1;
                }
                else
                {
                    if (mid - 1 < 0)
                    {
                        return null;
                    }
                    return quoteList[mid-1]; // key found
                }
            }
            return null;  // key not found.
        }

        public int GetIndex(DateTime date)
        {
            int low = 0;
            int high = quoteList.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                DateTime midVal = quoteList[mid].date;

                if (midVal < date)
                {
                    low = mid + 1;
                }
                else if (midVal > date)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid; // key found
                }
            }
            return -1;  // key not found.
        }
    }

    [Serializable]
    public class Quote
    {
        public DateTime date;
        public double open;
        public double high;
        public double low;
        public double close;
        public long volume;
        public double adjclose;

        public Quote()
        {
        }

        public Quote(DateTime date, double open, double high, double low, double close, long volume, double adjclose)
        {
            this.date = date;
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
            this.volume = volume;
            this.adjclose = adjclose;
        }

        public Quote(Quote quote)
        {
            this.date = quote.date;
            this.open = quote.open;
            this.high = quote.high;
            this.low = quote.low;
            this.close = quote.close;
            this.volume = quote.volume;
            this.adjclose = quote.adjclose;
        }

        public string ToDelimitedString()
        {
            string delimited = date.ToString("yyyy-MM-dd");
            delimited += "," + open.ToString("F2");
            delimited += "," + high.ToString("F2");
            delimited += "," + low.ToString("F2");
            delimited += "," + close.ToString("F2");
            delimited += "," + volume;
            delimited += "," + adjclose.ToString("F2");

            return delimited;
        }
    }

    [Serializable]
    public class Split
    {
        public DateTime date;
        public double multiplier;
        public Split(DateTime date, double multiplier)
        {
            this.date = date;
            this.multiplier = multiplier;
        }
    }

}