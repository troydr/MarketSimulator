using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.IO;

namespace MarketSimulator
{
    public static class MarketSimulator
    {
        public static DateTime minQuoteDate = new DateTime(1990, 1, 1);
        public static DateTime maxQuoteDate = new DateTime(2010, 12, 31);

        public static string connectionString;

        public static string simulationDirectory;
        public static string quotesDirectory;
        public static string logsDirectory;
        public static string resultsDirectory;
        public static string scenariosDirectory;
        public static string strategiesDirectory;
        public static string systemsDirectory;
        public static string binaryQuoteDataDirectory;

        public static string compareDirectory1;
        public static string compareDirectory2;
        public static StringBuilder console = new StringBuilder(2000);


        static void ReadConfigSettings()
        {
            simulationDirectory = ConfigurationManager.AppSettings["SimulationDataDirectory"].ToString();
        }
        static void SetDirectories()
        {
            quotesDirectory = simulationDirectory + "\\quotes\\yahoo sp500 1-2-1998 to 11-5-2010 - Full";
            binaryQuoteDataDirectory = simulationDirectory + "\\quotedata\\yahoo sp500 1-2-1998 to 11-5-2010 - Full";
            logsDirectory = simulationDirectory + "\\logs";
            resultsDirectory = simulationDirectory + "\\results";
            scenariosDirectory = simulationDirectory + "\\scenarios";
            strategiesDirectory = simulationDirectory + "\\strategies";
            systemsDirectory = simulationDirectory + "\\systems";
            compareDirectory1 = simulationDirectory + "\\compare1";
            compareDirectory2 = simulationDirectory + "\\compare2";

            if (!Directory.Exists(simulationDirectory))
            {
                Directory.CreateDirectory(simulationDirectory);
            }
            if (!Directory.Exists(quotesDirectory))
            {
                Directory.CreateDirectory(quotesDirectory);
            }
            if (!Directory.Exists(binaryQuoteDataDirectory))
            {
                Directory.CreateDirectory(binaryQuoteDataDirectory);
            }
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }
            if (!Directory.Exists(resultsDirectory))
            {
                Directory.CreateDirectory(resultsDirectory);
            }
            if (!Directory.Exists(scenariosDirectory))
            {
                Directory.CreateDirectory(scenariosDirectory);
            }
            if (!Directory.Exists(strategiesDirectory))
            {
                Directory.CreateDirectory(strategiesDirectory);
            }
            if (!Directory.Exists(systemsDirectory))
            {
                Directory.CreateDirectory(systemsDirectory);
            }
            if (!Directory.Exists(compareDirectory1))
            {
                Directory.CreateDirectory(compareDirectory1);
            }
            if (!Directory.Exists(compareDirectory2))
            {
                Directory.CreateDirectory(compareDirectory2);
            }

        }
        static MarketSimulator()
        {
            ReadConfigSettings();
            SetDirectories();
            Logger.Init(logsDirectory);
            Logger.Write("Start");
        }

        public static void LoadQuoteData()
        {
            LoadQuoteDataFromBinaryFiles();
            //LoadQuoteDataFromCSVFiles();
        }
        public static void LoadQuoteDataFromBinaryFiles()
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            QuoteDataManager.ReadAllQuoteDataFromBinaryFiles(binaryQuoteDataDirectory);
            sw.Stop();
            Logger.WriteLine("timing", "LoadAllQuoteDataFromBinaryFiles: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");

            QuoteDataManager.LoadReferenceMarketDatesFromQuoteData("ge");
            QuoteDataManager.BuildStockSplitLists();
            QuoteDataManager.AlignAllQuoteDataDates();

            //QuoteDataManager.WriteAllQuoteDataToCSVFiles(compareDirectory2);
        }

        public static void LoadQuoteDataFromBinarySerializedObjects()
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            QuoteDataManager.LoadAllQuoteDataFromBinaryFiles(binaryQuoteDataDirectory);
            sw.Stop();
            Logger.WriteLine("timing", "LoadAllQuoteDataFromBinaryFiles: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");

            QuoteDataManager.LoadReferenceMarketDatesFromQuoteData("ge");
        }

        public static void LoadQuoteDataFromCSVFiles()
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            QuoteDataManager.LoadDataFromFiles(quotesDirectory, minQuoteDate, maxQuoteDate);
            sw.Stop();
            Logger.WriteLine("timing", "LoadDataFromFiles: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");

            sw.Reset();
            sw.Start();
            QuoteDataManager.LoadReferenceMarketDatesFromQuoteData("ge");
            //sw.Stop();
            //Logger.WriteLine("timing", "LoadReferenceMarketDatesFromQuoteData: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");

            //sw.Reset();
            //sw.Start();
            //QuoteDataManager.CheckDataForMissingAndExtraDates("ge");
            //sw.Stop();
            //Logger.WriteLine("timing", "CheckDataForMissingAndExtraDates: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");

            //sw.Reset();
            //sw.Start();
            QuoteDataManager.BuildStockSplitLists();
            //sw.Stop();
            //Logger.WriteLine("timing", "BuildStockSplitLists: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");

            //sw.Reset();
            //sw.Start();
            QuoteDataManager.AlignAllQuoteDataDates();
            sw.Stop();
            Logger.WriteLine("timing", "afterloadingprocessing: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");

            /*
            sw.Reset();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                QuoteDataManager.symbolList.Shuffle();
            }
            sw.Stop();
            Logger.WriteLine("timing", "Shuffle 1000 times: " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");
            */

            //SpeedTest();

            //QuoteDataManager.WriteAllQuoteDataToBinaryFiles(quoteDataDirectory);
            //QuoteDataManager.SaveAllQuoteDataToBinaryFiles(quoteDataDirectory);
            //QuoteDataManager.WriteAllQuoteDataToCSVFiles(compareDirectory1);
        }


        public static void LogStopWatch(Stopwatch sw, string description)
        {
            try
            {
                Logger.WriteLine("timing", description + ": " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }


        public static void Start()
        {
            /*
             * 
             * 
            QuoteDataManager.LoadDataFromFiles(quoteDirectory, minQuoteDate, maxQuoteDate);
            QuoteDataManager.LoadReferenceMarketDatesFromQuoteData("ge");

            ScenarioManager sm = new ScenarioManager("C:\\VS Projects\\Scenario");
            Scenario s = sm.MakeRandomScenario("test", new DateTime(2002, 1, 1), new DateTime(2007, 1, 1), 90, 300);
            sm.SaveScenario(s);
            s = sm.LoadScenario("test");

            B = new Broker(10, 0.00075);
            Trader t1 = new Trader();
            t1.SetSymbolList(s.symbolList);
            Account account = new Account(99999);
            B.AddAccount(account);



            */


            /*
            Stopwatch sw = new Stopwatch();

            sw.Reset();
            sw.Start();
            b.StressTestMemoryAlloctation2(2101000);
            sw.Stop();
            Logger.WriteLine("timing", "test B: " + sw.ElapsedMilliseconds);      
            sw.Reset();
            sw.Start();
            b.StressTestMemoryAlloctation(2101000);
            sw.Stop();
            Logger.WriteLine("timing", "test A: " + sw.ElapsedMilliseconds);
             * */

            /*
            qdm.LoadQuoteData("xto", minQuoteDate, maxQuoteDate);

            //qdm.WriteQuoteDataToFile("GE", "ge");
            qdm.WriteQuoteDataToFile("z_1", "xto");
            qdm.AlignQuoteDataDates("xto");
            qdm.WriteQuoteDataToFile("z_2", "xto");
             * */



            //

            //qdm.LoadQuoteData("msft", minQuoteDate, maxQuoteDate);
            //qdm.CheckSymbolForStockSplits("msft");

            /* all CHECKED with no differences
             * 
            qdm.CheckSymbolForMissingDates("GE", "CAT");
            qdm.CheckSymbolForMissingDates("GE", "JNJ");
            qdm.CheckSymbolForMissingDates("GE", "KO");
            qdm.CheckSymbolForMissingDates("KO", "BA");
            qdm.CheckSymbolForMissingDates("GE", "BA");
            qdm.CheckSymbolForMissingDates("GE", "XOM");
            qdm.CheckSymbolForMissingDates("^GSPC", "GE");
            qdm.CheckSymbolForMissingDates("^GSPC", "CAT");
             * */
        }

        public static void SpeedTest()
        {
            Stopwatch sw = new Stopwatch();

            DateTime someDate = new DateTime(1800, 2, 2);
            QuoteData qd = QuoteDataManager.GetQuoteData("GE");
            int i = 0;
            int j = 0;

            //---------------------------------------------------------
            sw.Reset();
            sw.Start();
            i = 0;
            j = 0;
            while (i < 10000)
            {
                foreach (Quote q in qd.quoteList)
                {
                    if (q.date == someDate)
                    {
                        j++;
                    }
                }
                i++;
            }

            if (j > 10000)
            {
                Logger.WriteLine("timing", j.ToString());
            }
            sw.Stop();
            LogStopWatch(sw, "foreach");


            sw.Reset();
            sw.Start();
            i = 0;
            j = 0;
            List<Quote> list;
            int length;
            while (i < 10000)
            {
                list = qd.quoteList;
                length = list.Count;
                for (int k = 0; k < length; k++)
                {
                    if (list[k].date == someDate)
                    {
                        j++;
                    }
                }
                i++;
            }

            if (j > 10000)
            {
                Logger.WriteLine("timing", j.ToString());
            }
            sw.Stop();
            LogStopWatch(sw, "for");

        }
        public static void RandomCheckerTest()
        {
            /*    int rand = 0;
                bool low = false;
                bool high = false;
                bool outOfBounds = false;
                int highNum = 21;
                int lowNum = 2;
                for (int i = 0; i < 2000; i++)
                {
                    rand = Calc.RandomInt(lowNum, highNum);
                    if (rand == lowNum)
                    {
                        low = true;
                    }
                    else if (rand == highNum)
                    {
                        high = true;
                    }
                    if (rand > highNum || rand < lowNum)
                    {
                        outOfBounds = true;
                    }
        
                }

                Logger.WriteLine("foobar", high.ToString() + low.ToString() + outOfBounds.ToString());
             *  */
        }
    }

}
