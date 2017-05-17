using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MarketSimulator
{
    public static class Simulation
    {
        public static Broker broker;
        public static Account account;
        public static TradeSystem ts;
        public static DateTime simulationStartDate;
        public static DateTime simulationEndDate;
        public static double orderFee = 10;
        public static double orderSlippagePercent = 0.003;
        public static List<string> symbolList = new List<string>(500); // master list that is randomized
        public static List<string> simulationSymbolList = new List<string>(500); // list that only contains symbols in current simulation date range
        public static int minNumberOfSimulationDays = 51;
        public static int maxNumberOfSimulationDays = 180;

        public static DateTime minStartDate; // determined by whats in QDM
        public static DateTime maxStartDate; // determined by whats in QDM

        public static int simulationDay = 0;
        public static DateTime simulationDate;
        public static int nextSymbolIndex = 0;
        public static int startIndex = 0;
        public static int endIndex = 0;
        public static int currentDateIndex = 0;

        public static int numberOfSimulationDays = 0;
        public static bool UseTradeSystem = true;

        public static int runNumber = 0;

        public static double averageEndingAccountValue = 0;


        public static void InitializeSimulation()
        {
            try
            {
                symbolList.Clear();
                foreach (string symbol in QuoteDataManager.symbolList)
                {
                    symbolList.Add(symbol);
                }

                account = new Account(100000);
                broker = new Broker(account, orderFee, orderSlippagePercent);

                TradeAttributes ta = new TradeAttributes();
                ts = new TradeSystem(ta);
                ts.SetBroker(broker);

                minStartDate = QuoteDataManager.dateList.First();
                maxStartDate = QuoteDataManager.dateList.Last();
                
                Calc.RandomSeed(987654321098);
                Randomize();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void NextIteration()
        {
            try
            {
                if (simulationDay > numberOfSimulationDays)
                {
                    return;
                }
                simulationDay++;
                currentDateIndex++;
                simulationDate = QuoteDataManager.dateList[currentDateIndex];

                broker.DoDailyProcessing(simulationDate, simulationDay);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void RunTradeSystemThroughSimulation()
        {
            while (simulationDay < numberOfSimulationDays)
            {
                try
                {
                    simulationDate = QuoteDataManager.dateList[currentDateIndex];

                    broker.DoDailyProcessing(simulationDate, simulationDay);
                    ts.DoDailyProcessing(simulationDate, simulationDay);
                    broker.FinishDailyProcessing();

                    simulationDay++;
                    currentDateIndex++;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public static void RunOneIterationOfTradeSystemThroughSimulation()
        {
            try
            {
                if (simulationDay > numberOfSimulationDays)
                {
                    return;
                }

                simulationDate = QuoteDataManager.dateList[currentDateIndex];

                broker.DoDailyProcessing(simulationDate, simulationDay);
                ts.DoDailyProcessing(simulationDate, simulationDay);
                broker.FinishDailyProcessing();

                simulationDay++;
                currentDateIndex++;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void RunTradeSystemThroughMultipleRandomSimulations(int numberOfSimulations)
        {
            //Calc.RandomSeed(987654321098);

            Stopwatch sw1 = new Stopwatch();
            sw1.Reset();

            Stopwatch sw2 = new Stopwatch();
            sw2.Reset();
            
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            List<TradeStatistics> resultList = new List<TradeStatistics>(numberOfSimulations);
            averageEndingAccountValue = 0;
            double summationOfAccountValues = 0;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                //    sw1.Start();
                Randomize();
                 //   sw1.Stop();

               //     sw2.Start();
                RunTradeSystemThroughSimulation();
               //     sw2.Stop();

                summationOfAccountValues += account.balance.accountValue;
                resultList.Add(ts.s);
            }

            averageEndingAccountValue = Math.Truncate(summationOfAccountValues / numberOfSimulations);

            sw.Stop();
            MarketSimulator.LogStopWatch(sw, "RunTradeSystemThroughMultipleRandomSimulations(" + numberOfSimulations.ToString() + ")  ");


            foreach (TradeStatistics s in resultList)
            {
            //    Logger.WriteLine("run " + runNumber.ToString(), s.startingBalance.ToString("F2")+","+s.endingBalance.ToString("F2")+","+s.highestBalance.ToString("F2")+","+s.lowestBalance.ToString("F2"));
            }


            //Logger.WriteLine("timing", "RunTradeSystemThroughMultipleRandomSimulations("+ numberOfSimulations.ToString() + ")  " + sw.Elapsed.Seconds.ToString() + "sec " + sw.Elapsed.Milliseconds.ToString() + "ms");           
           // Logger.WriteLine("timing", "Randomize() total: " + sw1.Elapsed.Seconds.ToString() + "sec " + sw1.Elapsed.Milliseconds.ToString() + "ms");
           // Logger.WriteLine("timing", "RunTradeSystemThroughSimulation() total: " + sw2.Elapsed.Seconds.ToString() + "sec " + sw2.Elapsed.Milliseconds.ToString() + "ms");

        }





        public static void Clear()
        {
            try
            {
                simulationDay = 0;
                nextSymbolIndex = 0;

                account.Clear();
                account.SetStartingValues(100000);
                broker.Clear();
                broker.SetStartingValues(account, orderFee, orderSlippagePercent);

                ts.Clear();
                ts.SetBroker(broker);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void Randomize()
        {
            try
            {
                Clear();
                // randomize symbol order
                symbolList.Shuffle();

                // randomize number of days
                numberOfSimulationDays = Calc.RandomInt(minNumberOfSimulationDays, maxNumberOfSimulationDays);

                // randomize start and end dates
                int highestIndex = QuoteDataManager.dateList.Count - 1 - numberOfSimulationDays;
                startIndex = Calc.RandomInt(highestIndex);
                endIndex = startIndex + numberOfSimulationDays;
                currentDateIndex = startIndex;
                simulationStartDate = QuoteDataManager.dateList[startIndex];
                simulationEndDate = QuoteDataManager.dateList[endIndex];

                simulationDate = simulationStartDate;

                broker.day = 0;
                broker.date = simulationStartDate;

                BuildSimulationSymbolList();
                ts.SetSymbolList(simulationSymbolList);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void BuildSimulationSymbolList()
        {
            try
            {
                simulationSymbolList.Clear();
                foreach (string symbol in symbolList)
                {
                    QuoteData qd = QuoteDataManager.GetQuoteData(symbol);
                    if (qd.startDate <= Simulation.simulationStartDate && qd.endDate >= Simulation.simulationEndDate)
                    {
                        simulationSymbolList.Add(symbol);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

    }
}
