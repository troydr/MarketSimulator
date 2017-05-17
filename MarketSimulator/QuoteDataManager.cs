using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading; // for threads
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;



namespace MarketSimulator
{
    public static class QuoteDataManager
    {
        public static Dictionary<string, QuoteData> quoteDataHash = new Dictionary<string, QuoteData>(600, StringComparer.CurrentCultureIgnoreCase);

        public static List<string> symbolList = new List<string>(600);

        private static List<string> workingSymbols = new List<string>(600); // for multithreaded loads
        private static Object workingSymbolsLock = new Object(); // for multithreaded loads
        private static Object quoteDataHashLock = new Object(); // for multithreaded loads
        private static string quoteDirectory;
        private static DateTime startDate;
        private static DateTime endDate;
        //public static List<string> indexList = new List<string>(10);

        public static List<DateTime> dateList = new List<DateTime>(5000);

        public static List<double> splitList = new List<double> { 1.25, 1.3333333333, 1.5, 2, 2.5, 2.75, 3, 3.3333333333, 3.25, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        public static List<double> reverseSplitList = new List<double> { 0.01, 0.0125, 0.01428571429, 0.01666666666, 0.02, 0.025, 0.0333333333, 0.05, 0.0625, 0.1, 0.1111111111, 0.125, 0.1428571429, 0.1666666666, 0.2, 0.25, 0.3333333333, 0.5, 0.6666666666, 0.75, 0.8 };
        public static double splitThreshold = 0.20; //20 percent

        // temp stats
        private static int maxSplitCount = 0;
        private static int totalSplitCount = 0;
        private static int numStocksWithSplits = 0;

        // temp stats
        //public List<string> ExtraDateList = new List<string>(100);
        //public List<string> MissingDateList = new List<string>(100);
        //public List<string> OkayMissingDateList = new List<string>(20);

        public static void SaveAllQuoteDataToBinaryFiles(string filepath)
        {
            foreach(string symbol in symbolList)
            {
                try
                {
                    QuoteData qd = GetQuoteData(symbol);
                    if (qd != null && qd.quoteList.Count > 0)
                    {
                        SaveQuoteDataToBinaryFile(filepath, qd);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public static void SaveQuoteDataToBinaryFile(string filepath, QuoteData qd)
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(filepath + "\\" + qd.symbol + ".bin", FileMode.Create, FileAccess.Write, FileShare.None);
                //formatter.Serialize(stream, VERSION);
                formatter.Serialize(stream, qd);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static void LoadAllQuoteDataFromBinaryFiles(string quoteDirectory)
        {
            if (Directory.Exists(quoteDirectory))
            {
                foreach (string filename in Directory.GetFiles(quoteDirectory, "*.bin"))
                {
                    string symbolName = filename.Remove(0, quoteDirectory.Length + 1);
                    symbolName = symbolName.Remove(symbolName.Length - 4, 4);
                    symbolList.Add(symbolName);
                }
            }

            foreach (string symbol in symbolList)
            {
                try
                {
                    string filename = quoteDirectory + "\\" + symbol + ".bin";

                     QuoteData qd = LoadQuoteDataFromBinaryFile(filename);

                    if (qd != null && qd.quoteList.Count > 0)
                    {
                        lock (quoteDataHashLock)
                        {
                            quoteDataHash.Add(symbol, qd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public static QuoteData LoadQuoteDataFromBinaryFile(string filename)
        {
            Stream stream = null;
            QuoteData qd = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                //int version = (int)formatter.Deserialize(stream);
                //Debug.Assert(version == VERSION);
                qd = (QuoteData)formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                
            }
            return qd;
        }



        static QuoteDataManager()
        {
        }

        public static Quote GetQuote(string symbol, int index)
        {
            try
            {
                QuoteData qd = quoteDataHash[symbol];
                if (index > qd.quoteList.Count || index < 0)
                {
                    return null;
                }
                return qd.quoteList[index];
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }

        public static Quote GetQuote(string symbol, DateTime date)
        {
            try
            {
                QuoteData qd = quoteDataHash[symbol];
                if (date > qd.endDate || date < qd.startDate)
                {
                    return null;
                }

                return qd.GetQuote(date);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }

        public static Quote GetPreviousQuote(string symbol, DateTime date)
        {
            try
            {
                QuoteData qd = quoteDataHash[symbol];
                if (date > qd.endDate || date <= qd.startDate)
                {
                    return null;
                }

                return qd.GetPreviousQuote(date);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }

        public static QuoteData GetQuoteData(string symbol)
        {
            try
            {
                QuoteData qd = quoteDataHash[symbol];
                return qd;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }

        public static DateTime GetEndDate(DateTime startDate, int numDays)
        {
            DateTime result = startDate;
            try
            {
                int startingIndex = dateList.IndexOf(startDate);

                if (startingIndex >= 0 && startingIndex < dateList.Count)
                {
                    int endingIndex = startingIndex + numDays;
                    if (endingIndex >= dateList.Count)
                    {
                        result = dateList.Last();
                    }
                    else
                    {
                        result = dateList[endingIndex];
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;
        }

        public static void GetValidSymbols(DateTime startDate, DateTime endDate, List<string> symbolList)
        {
            try
            {
                symbolList.Clear();

                QuoteData qd;
                foreach (string symbol in symbolList)
                {
                    qd = quoteDataHash[symbol];
                    if (qd.startDate > startDate || qd.endDate < endDate)
                    {
                        continue;
                    }
                    foreach (Split split in qd.splitList)
                    {
                        if (split.date >= startDate && split.date <= endDate)
                        {
                            continue;
                        }
                    }
                    symbolList.Add(qd.symbol);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static double GetSplitMultiplier(string symbol, DateTime date)
        {
            double result = 1;
            try
            {
                QuoteData qd = quoteDataHash[symbol];

                foreach (Split split in qd.splitList)
                {
                    if (split.date == date)
                    {
                        result = split.multiplier;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;
        }

        public static void PrintStats()
        {
            try
            {
                Logger.WriteLine("stats", "symbol count: " + symbolList.Count);
                Logger.WriteLine("stats", "date count: " + dateList.Count);
                Logger.WriteLine("stats", "max split count: " + maxSplitCount);
                Logger.WriteLine("stats", "numStocksWithSplits: " + numStocksWithSplits);
                Logger.WriteLine("stats", "totalSplitCount: " + totalSplitCount);
                Logger.WriteLine("stats", "average number of splits per stock: " + totalSplitCount / numStocksWithSplits);

                foreach (string symbol in symbolList)
                {
                    QuoteData qd = quoteDataHash[symbol];
                    Logger.WriteLine("stats", symbol + "," + qd.quoteList.Count);

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void BuildStockSplitLists()
        {
            try
            {
                foreach (string symbol in symbolList)
                {
                    BuildStockSplitList(symbol);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void BuildStockSplitList(string symbol)
        {
            try
            {
                QuoteData quoteData = quoteDataHash[symbol];
                List<Quote> quotes = quoteData.quoteList;
                Quote prevQuote;
                Quote quote;
                int splitCount = 0;
                for (int i = 1; i < quotes.Count; i++)
                {
                    prevQuote = quotes[i - 1];
                    quote = quotes[i];

                    double percentChangeClose = Calc.PercentChange(quote.close, prevQuote.close);
                    //double percentChangeAdjClose = Calc.PercentChange(quote.adjclose, prevQuote.adjclose);
                    double percentDifference = Calc.RelativePercentDifference(quote.close / prevQuote.close, quote.adjclose / prevQuote.adjclose);

                    if (Math.Abs(percentChangeClose) >= splitThreshold) // this check will skip dividend payouts--on dividend payouts close doesn't change much but adjClose can change alot
                    {
                        if (percentDifference >= splitThreshold) // this condition happens on stock split days
                        {
                            splitCount++;
                            Double splitFactor = FindSplitFactor(quote.close, prevQuote.close, quote.adjclose, prevQuote.adjclose);
                            quoteData.splitList.Add(new Split(quote.date, splitFactor));
                            //LogSymbolSplit(symbol, quotes[i].date, splitFactor);
                            //Logger.WriteLine("splitMultiplier", splitFactor + "," + symbol + "," + quotes[i].date.ToString("yyyy-MM-dd"));
                        }
                    }
                }

                if (splitCount > maxSplitCount)
                {
                    maxSplitCount = splitCount;
                }
                numStocksWithSplits++;
                totalSplitCount += splitCount;

            }
            catch (Exception ex)
            {
                Logger.Write("symbol: " + symbol);
                ExceptionHandler.Handle(ex);
            }
        }

        private static double FindSplitFactor(double close, double prevClose, double adjClose, double prevAdjClose)
        {
            double result = 1;
            try
            {
                double diff = 9999;
                double splitFactor = prevClose / (close * (prevAdjClose / adjClose));

                if (splitFactor > 1)
                {
                    foreach (double roundSplitFactor in splitList)
                    {
                        if (Math.Abs(roundSplitFactor - splitFactor) < diff)
                        {
                            diff = Math.Abs(roundSplitFactor - splitFactor);
                            result = roundSplitFactor;
                        }
                    }
                }
                else
                {
                    foreach (double roundSplitFactor in reverseSplitList)
                    {
                        if (Math.Abs(roundSplitFactor - splitFactor) < diff)
                        {
                            diff = Math.Abs(roundSplitFactor - splitFactor);
                            result = roundSplitFactor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;
        }

        private static void LogSymbolSplit(string symbol, DateTime date, double splitFactor)
        {
            try
            {
                if (splitFactor >= 1)
                {
                    Logger.WriteLine(symbol + "_splits", date.ToString("yyyy-MM-dd") + "," + splitFactor);
                }
                else
                {
                    Logger.WriteLine("ReverseSplit_" + symbol, date.ToString("yyyy-MM-dd") + "," + splitFactor);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void AlignAllQuoteDataDates()
        {
            try
            {
                foreach (string symbol in symbolList)
                {
                    AlignQuoteDataDates(symbol);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }



        public static void AlignQuoteDataDates(string symbol)
        {
            try
            {
                QuoteData symQd = quoteDataHash[symbol];
                List<Quote> symList = symQd.quoteList;

                DateTime startDate = symQd.startDate;
                int refOffsetIndex = -1;
                for (int i = 0; i < dateList.Count; i++) //find reference starting index
                {
                    if (dateList[i] >= startDate)
                    {
                        refOffsetIndex = i;
                        break;
                    }
                }
                if (refOffsetIndex >= dateList.Count || refOffsetIndex < 0)
                {
                    return;
                }

                for (int i = 0; i < symList.Count; i++)
                {
                    DateTime symDate = symList[i].date;
                    DateTime refDate = dateList[i + refOffsetIndex];
                    if (symDate != refDate)
                    {
                        if (symDate > refDate)
                        {
                            if (i == 0)
                            {
                                symList.Insert(i, new Quote(symList[i + 1]));
                            }
                            else
                            {
                                symList.Insert(i, new Quote(refDate, symList[i - 1].close, symList[i - 1].close, symList[i - 1].close, symList[i - 1].close, symList[i - 1].volume, symList[i - 1].adjclose));
                            }
                            i--;
                        }
                        else
                        {
                            symList.RemoveAt(i);
                            i--;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }


        public static void CheckDataForMissingAndExtraDates(string referenceSymbol)
        {
            try
            {
                foreach (string symbol in symbolList)
                {
                    CheckSymbolForMissingAndExtraDates(referenceSymbol, symbol);
                }

                /*foreach (string date in MissingDateList)
                {
                    Logger.WriteLine("MissingDateList", date);
                }
                foreach (string date in ExtraDateList)
                {
                    Logger.WriteLine("ExtraDateList", date);
                */

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void CheckSymbolForMissingAndExtraDates(string referenceSymbol, string symbol)
        {
            try
            {
                QuoteData refQd = quoteDataHash[referenceSymbol];
                QuoteData symQd = quoteDataHash[symbol];

                List<Quote> refList = refQd.quoteList;
                List<Quote> symList = symQd.quoteList;

                DateTime startDate = symQd.startDate;
                int refOffsetIndex = -1;
                for (int i = 0; i < refList.Count; i++) //try find reference starting index
                {
                    if (refList[i].date >= startDate)
                    {
                        refOffsetIndex = i;
                        break;
                    }
                }
                if (refOffsetIndex >= refList.Count || refOffsetIndex < 0)
                {
                    return;
                }

                int consecutiveMissingDays = 0;
                //maxConsecutiveMissingDays
                for (int i = 0; i < symList.Count; i++)
                {
                    DateTime symDate = symList[i].date;
                    DateTime refDate = refList[i + refOffsetIndex].date;
                    if (symDate != refDate)
                    {
                        if (symDate > refDate)
                        {
                            refOffsetIndex++;
                            i--;

                            symQd.missingDays++;
                            consecutiveMissingDays++;
                            if (consecutiveMissingDays > symQd.maxConsecutiveMissingDays)
                            {
                                symQd.maxConsecutiveMissingDays = consecutiveMissingDays;
                            }

                            //if (!MissingDateList.Contains(refDate.ToString("yyyy-MM-dd")))
                            //{
                            //    MissingDateList.Add(refDate.ToString("yyyy-MM-dd"));
                            //}

                            //Logger.WriteLine("missing_days", symbol + "," + refDate.ToString("yyyy-MM-dd"));

                            //if (!OkayMissingDateList.Contains(refDate.ToString("yyyy-MM-dd")))
                            //{
                            //    Logger.WriteLine(symbol + "_missing", refDate.ToString("yyyy-MM-dd"));
                            //}
                        }
                        else
                        {
                            i++;
                            refOffsetIndex--;

                            symQd.extraDays++;

                            //Logger.WriteLine("Extra_days", symbol + "," + refDate.ToString("yyyy-MM-dd"));

                            //if (!ExtraDateList.Contains(symDate.ToString("yyyy-MM-dd")))
                            //{
                            //    ExtraDateList.Add(symDate.ToString("yyyy-MM-dd"));
                            //}
                            //Logger.Write("missing_ref", symbol + " missing: " + symDate.ToString("yyyy-MM-dd"));
                            //Logger.WriteLine(referenceSymbol + "-" + symbol + "_extra", symDate.ToString("yyyy-MM-dd"));

                        }
                    }
                    else
                    {
                        consecutiveMissingDays = 0;
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.Write("referenceSymbol: " + referenceSymbol + "    " + "symbol: " + symbol);
                ExceptionHandler.Handle(ex);
            }
        }

        private static void LoadSymbolListFromDirectory(string quoteDirectory)
        {
            if (Directory.Exists(quoteDirectory))
            {
                foreach (string filename in Directory.GetFiles(quoteDirectory, "*.csv"))
                {
                    string symbolName = filename.Remove(0, quoteDirectory.Length + 1);
                    symbolName = symbolName.Remove(symbolName.Length - 4, 4);
                    symbolList.Add(symbolName);
                }
            }
        }
        public static void LoadDataFromFiles(string QuoteDirectory, DateTime StartDate, DateTime EndDate)
        {
            quoteDirectory = QuoteDirectory;
            startDate = StartDate;
            endDate = EndDate;
            LoadSymbolListFromDirectory(quoteDirectory);
            SingleThreadedLoadDataFromFiles(quoteDirectory, startDate, endDate);
            //MultiThreadedLoadDataFromFiles(quoteDirectory, startDate, endDate);

        }
        private static void SingleThreadedLoadDataFromFiles(string quoteDirectory, DateTime startDate, DateTime endDate)
        {
            string filepath;
            foreach (string symbol in symbolList)
            {
                try
                {
                    filepath = quoteDirectory + "\\" + symbol + ".csv";
                    LoadQuoteDataFromFile(filepath, symbol, startDate, endDate);
                    //Logger.Write("progress", symbol + " finished loading");
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        private static void MultiThreadedLoadDataFromFiles(string quoteDirectory, DateTime startDate, DateTime endDate)
        {
            workingSymbols.Clear();
            foreach (string symbol in symbolList)
            {
                workingSymbols.Add(symbol);
            }

            int numThreads = 1;
            for (int i = 0; i < numThreads; i++)
            {
                Thread workerThread = new Thread(new ThreadStart(MultiThreadedLoadDataFromFile));
                workerThread.Start();
            }

            while (workingSymbols.Count > 0)
            {
                Thread.Sleep(50);
            }
        }
        private static void MultiThreadedLoadDataFromFile()
        {
            string filepath;
            string symbol;
            bool done = false;
            while (!done)
            {
                try
                {
                    symbol = GetNextWorkingSymbol();

                    if (symbol == null || symbol == "")
                    {
                        done = true;
                    }
                    else
                    {
                        filepath = quoteDirectory + "\\" + symbol + ".csv";
                        LoadQuoteDataFromFile(filepath, symbol, startDate, endDate);
                    }
                }
                catch (Exception ex)
                {

                    ExceptionHandler.Handle(ex);
                }
            }
        }
        
        private static string GetNextWorkingSymbol()
        {
            string nextSymbol = "";
            lock (workingSymbolsLock)
            {
                try
                {
                    if (workingSymbols.Count > 0)
                    {
                        nextSymbol = workingSymbols[0];
                        workingSymbols.RemoveAt(0);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
            return nextSymbol;
        }
        public static void LoadQuoteDataFromFile1(string filepath, string symbol, DateTime startDate, DateTime endDate)
        {
            int count = 0;
            try
            {
                if (filepath == null || filepath == "")
                {
                    return;
                }
                if (symbol == null || symbol == "")
                {
                    return;
                }
                if (!File.Exists(filepath))
                {
                    return;
                }

                QuoteData qd = new QuoteData(symbol);

                 string[] columns;
                string delimiter = ",";

                string[] allLines = File.ReadAllLines(filepath);
              /*  foreach (string line in allLines)
                {
                    try
                    {
                        if (count == 0)
                        {
                            continue; //skip column header line
                        }
                        columns = line.Split(delimiter.ToCharArray());

                        DateTime date = Convert.ToDateTime(columns[0]);
                        if (startDate > date)
                        {
                            // continue; // skip to next line
                        }
                        else if (date > endDate)
                        {
                            //  break; // done with file completely
                        }

                        double open = Convert.ToDouble(columns[1]);
                        double high = Convert.ToDouble(columns[2]);
                        double low = Convert.ToDouble(columns[3]);
                        double close = Convert.ToDouble(columns[4]);
                        long volume = Convert.ToInt64(columns[5]);
                        double adjClose = Convert.ToDouble(columns[6]);

                        Quote q = new Quote(date, open, high, low, close, volume, adjClose);
                        if (q != null)
                        {
                            qd.quoteList.Insert(0, q); //use insert to unreverse list-files start with latest date
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(symbol, "Line reading or type conversion error at line:" + (count + 1));
                        ExceptionHandler.Handle(ex);
                    }
                    finally
                    {
                        count++;
                    }
                }
                if (qd.quoteList.Count > 0)
                {
                    qd.startDate = qd.quoteList[0].date;
                    qd.endDate = qd.quoteList[qd.quoteList.Count - 1].date;

                    lock (quoteDataHashLock)
                    {
                        quoteDataHash.Add(symbol, qd);
                    }
                }
               * */
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void LoadQuoteDataFromFile(string filepath, string symbol, DateTime startDate, DateTime endDate)
        {
            int count = 0;
            try
            {
                if (filepath == null || filepath == "")
                {
                    return;
                }
                if (symbol == null || symbol == "")
                {
                    return;
                }
                if (!File.Exists(filepath))
                {
                    return;
                }

                string delimiterString = ",";
                char[] delimiter = delimiterString.ToCharArray();

                QuoteData qd = new QuoteData(symbol);
                List<Quote> backwardsList = qd.quoteList;
                DateTime date;
                double open;
                double high;
                double low;
                double close;
                long volume;
                double adjClose;

                Quote q;

                using (StreamReader sr = new StreamReader(filepath))//, System.Text.Encoding.ASCII, false, 50000))
                {
                    string line;
                    string[] columns;
                    
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    //CultureInfo enUS = new CultureInfo("en-US");

                    while (sr.Peek() >= 0)
                    {
                       try
                        {
                            line = sr.ReadLine();
                            if (count == 0)
                            {
                             //   count++;
                                continue; //skip column header line
                            }

                            columns = line.Split(delimiter);

                            date = DateTime.Parse(columns[0]);
                            //DateTime.TryParseExact(columns[0], "yyyy-MM-dd", provider, DateTimeStyles.None, out date);//DateTime.Parse(columns[0]);// Convert.ToDateTime(columns[0]);
                            //DateTime date = new DateTime(2010, 1, 1);

                            //if (startDate > date)
                            //{
                                // continue; // skip to next line
                            //}
                            //else if (date > endDate)
                            //{
                                //  break; // done with file completely
                            //}

                            /*open     = Convert.ToDouble(columns[1]);
                            high     = Convert.ToDouble(columns[2]);
                            low      = Convert.ToDouble(columns[3]);
                            close    = Convert.ToDouble(columns[4]);
                            volume   = Convert.ToInt64(columns[5]);
                            adjClose = Convert.ToDouble(columns[6]);
                           */
                           /* open = double.Parse(columns[1], provider);
                            high = double.Parse(columns[2], provider);
                            low = double.Parse(columns[3], provider);
                            close = double.Parse(columns[4], provider);
                            volume = long.Parse(columns[5], provider);
                            adjClose = double.Parse(columns[6], provider);
                            */

                            open = double.Parse(columns[1]);
                            high = double.Parse(columns[2]);
                            low = double.Parse(columns[3]);
                            close = double.Parse(columns[4]);
                            volume = long.Parse(columns[5]);
                            adjClose = double.Parse(columns[6]);

                            /*double.TryParse(columns[1], System.Globalization.NumberStyles.AllowDecimalPoint, provider, out open);// Convert.ToDouble(columns[1]);
                            double.TryParse(columns[2], System.Globalization.NumberStyles.AllowDecimalPoint, provider, out high);// Convert.ToDouble(columns[2]);
                            double.TryParse(columns[3], System.Globalization.NumberStyles.AllowDecimalPoint, provider, out low);// Convert.ToDouble(columns[3]);
                            double.TryParse(columns[4], System.Globalization.NumberStyles.AllowDecimalPoint, provider, out close);// Convert.ToDouble(columns[4]);
                            long.TryParse(columns[5], System.Globalization.NumberStyles.Integer, provider, out volume);// Convert.ToInt64(columns[5]);
                            double.TryParse(columns[6], System.Globalization.NumberStyles.AllowDecimalPoint, provider, out adjClose);// Convert.ToDouble(columns[6]);
                      */
                        /*
                            double open = 121.12;// Convert.ToDouble(columns[1]);
                            double high = 121.12; //Convert.ToDouble(columns[2]);
                            double low = 121.12; //Convert.ToDouble(columns[3]);
                            double close = 121.12;// Convert.ToDouble(columns[4]);
                            long volume = 12351235;// Convert.ToInt64(columns[5]);
                            double adjClose = 121.12;// Convert.ToDouble(columns[6]);
                        */
                            
                           q = new Quote(date, open, high, low, close, volume, adjClose);
                           if (q != null)
                           {
                               backwardsList.Add(q);
                           }
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(symbol, "Line reading or type conversion error at line:" + (count + 1));
                            ExceptionHandler.Handle(ex);
                        }
                        finally
                        {
                            count++;
                        }
                    }
                }
                if (backwardsList.Count > 0)
                {
                    //for(int i=backwardsList.Count-1; i>=0; i--)
                    //{
                    //    qd.quoteList.Add(backwardsList[i]);
                    //}

                    qd.quoteList.Reverse();

                    qd.startDate = qd.quoteList[0].date;
                    qd.endDate = qd.quoteList[qd.quoteList.Count - 1].date;

                    lock (quoteDataHashLock)
                    {
                        quoteDataHash.Add(symbol, qd);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void LoadReferenceMarketDatesFromQuoteData(string referenceSymbol)
        {
            try
            {
                dateList.Clear();
                QuoteData qd = quoteDataHash[referenceSymbol];
                foreach (Quote q in qd.quoteList)
                {
                    try
                    {
                        dateList.Add(q.date);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void LoadDataFromSQL(string category, DateTime startDate, DateTime endDate)
        {
            //symbolcategory_getSymbols 'Russell 1000 Dec2008'
            SqlConnection sqlConnection = null;
            SqlDataReader dataReader = null;
            try
            {
                sqlConnection = new SqlConnection(MarketSimulator.connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "symbolcategory_getSymbols";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@category", SqlDbType.VarChar);
                sqlCommand.Parameters["@category"].Value = category.Trim();

                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                dataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    try
                    {
                        string symbol = (string)dataReader["Symbol"];
                        symbol = symbol.Trim().ToLower();
                        LoadQuoteDataFromSQL(symbol, startDate, endDate);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        public static void LoadQuoteDataFromSQL(string symbol, DateTime startDate, DateTime endDate)
        {
            SqlConnection sqlConnection = null;
            SqlDataReader dataReader = null;
            try
            {
                sqlConnection = new SqlConnection(MarketSimulator.connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                string sql = "select [date], [open], [high], [low], [close], [volume], [adjclose] from quote where [Symbol] = '" + symbol.Trim() + "'";
                sql += " and [date] >= '" + startDate.ToString("yyyy-MM-dd") + "'";
                sql += " and [date] <= '" + endDate.ToString("yyyy-MM-dd") + "'";
                sql += " order by [date] asc";
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                dataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                QuoteData qd = new QuoteData(symbol);
                while (dataReader.Read())
                {
                    try
                    {
                        DateTime date = (DateTime)dataReader[0];
                        double open = Convert.ToDouble(dataReader[1]);
                        double high = Convert.ToDouble(dataReader[2]);
                        double low = Convert.ToDouble(dataReader[3]);
                        double close = Convert.ToDouble(dataReader[4]);
                        long volume = (long)dataReader[5];
                        double adjclose = Convert.ToDouble(dataReader[6]);

                        Quote q = new Quote(date, open, high, low, close, volume, adjclose);
                        if (q != null)
                        {
                            qd.quoteList.Add(q);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(ex);
                    }
                }
                if (qd.quoteList.Count > 0)
                {
                    qd.startDate = qd.quoteList[0].date;
                    qd.endDate = qd.quoteList[qd.quoteList.Count - 1].date;

                    quoteDataHash.Add(symbol, qd);
                    symbolList.Add(symbol);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        public static void LoadReferenceMarketDatesFromSQL(string referenceSymbol, DateTime startDate, DateTime endDate)
        {
            SqlConnection sqlConnection = null;
            SqlDataReader dataReader = null;
            try
            {
                dateList.Clear();
                sqlConnection = new SqlConnection(MarketSimulator.connectionString);
                SqlCommand sqlCommand = new SqlCommand();
                string sql = "select [date] from quote where [Symbol] = '" + referenceSymbol.Trim() + "'";
                sql += " and [date] >= '" + startDate.ToString("yyyy-MM-dd") + "'";
                sql += " and [date] <= '" + endDate.ToString("yyyy-MM-dd") + "'";
                sql += " order by [date] asc";
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                dataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    try
                    {
                        if (dataReader[0] != null)
                        {
                            dateList.Add((DateTime)dataReader[0]);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        public static void WriteQuoteDataToFile(string filename, string symbol)
        {
            try
            {
                QuoteData qd = quoteDataHash[symbol];
                foreach (Quote q in qd.quoteList)
                {
                    Logger.WriteLine(filename, q.ToDelimitedString());
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }


        public static void WriteAllQuoteDataToCSVFiles(string fileDirectory)
        {
            foreach (string symbol in symbolList)
            {
                try
                {
                    WriteQuoteDataToCSVFile(fileDirectory + "\\" + symbol + ".csv", symbol);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public static void WriteQuoteDataToCSVFile(string filename, string symbol)
        {
            try
            {
                QuoteData qd = quoteDataHash[symbol];
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    foreach (Quote q in qd.quoteList)
                    {
                        sw.WriteLine(q.ToDelimitedString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void WriteAllQuoteDataToBinaryFiles(string filepath)
        {
            foreach (string symbol in symbolList)
            {
                try
                {
                    WriteQuoteDataToBinaryFile(filepath + "\\" + symbol + ".bin", symbol);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public static void WriteQuoteDataToBinaryFile(string filename, string symbol)
        {
            Stream stream = null;
            try
            {
                //IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                BinaryWriter bw = new BinaryWriter(stream);
                
                QuoteData qd = quoteDataHash[symbol];
                int numQuotes = qd.quoteList.Count();
                bw.Write(numQuotes);
                foreach (Quote q in qd.quoteList)
                {
                    bw.Write(q.date.Ticks);
                    bw.Write(q.open);
                    bw.Write(q.high);
                    bw.Write(q.low);
                    bw.Write(q.close);
                    bw.Write(q.volume);
                    bw.Write(q.adjclose);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }

            }
        }

        public static void ReadAllQuoteDataFromBinaryFiles(string quoteDirectory)
        {
            if (Directory.Exists(quoteDirectory))
            {
                foreach (string filename in Directory.GetFiles(quoteDirectory, "*.bin"))
                {
                    string symbolName = filename.Remove(0, quoteDirectory.Length + 1);
                    symbolName = symbolName.Remove(symbolName.Length - 4, 4);
                    symbolList.Add(symbolName);
                }
            }
            foreach (string symbol in symbolList)
            {
                try
                {
                    ReadQuoteDataFromBinaryFile(quoteDirectory + "\\" + symbol + ".bin", symbol);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public static void ReadQuoteDataFromBinaryFile(string filename, string symbol)
        {
            Stream stream = null;
            try
            {
                //IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                BinaryReader br = new BinaryReader(stream);

                QuoteData qd = new QuoteData(symbol);
                List<Quote> qList = qd.quoteList;
                Quote q = null;

                int numQuotes = br.ReadInt32();
                for (int i = 0; i < numQuotes; i++)  //while (br.PeekChar() != -1)   
                {
                    try
                    {
                        q = new Quote();
                        q.date = new DateTime(br.ReadInt64());
                        q.open = br.ReadDouble();
                        q.high = br.ReadDouble();
                        q.low = br.ReadDouble();
                        q.close = br.ReadDouble();
                        q.volume = br.ReadInt64();
                        q.adjclose = br.ReadDouble();                 
                    }
                    catch (Exception ex)
                    {
                        q = null;
                        ExceptionHandler.Handle(ex);
                    }
                    if (q != null)
                    {
                        qList.Add(q);
                    }
                }
                if (qd.quoteList.Count > 0)
                {
                    qd.startDate = qd.quoteList[0].date;
                    qd.endDate = qd.quoteList[qd.quoteList.Count - 1].date;
                    if (!quoteDataHash.ContainsKey(symbol))
                    {
                        quoteDataHash.Add(symbol, qd);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }

            }
        }

    }//class QuoteDataManager
}
