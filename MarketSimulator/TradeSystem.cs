using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MarketSimulator
{
    public class TradeSystem
    {
        public DateTime date;
        public int day;

        private Broker broker;
        private AccountBalance accountBalance;

        public TradeAttributes a; // how a trader trades
        public TradeStatistics s = new TradeStatistics(); // outcomes / results / score
        public TradeVariables v = new TradeVariables(); // things a trader needs to store


        public TradeSystem()
        {

        }

        public TradeSystem(TradeAttributes tradeAttributes)
        {
            this.a = tradeAttributes;
        }

        public void Clear()
        {
            v.Clear();
            s = new TradeStatistics();
        }

        public void SetBroker(Broker broker)
        {
            try
            {
                this.broker = broker;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void SetSymbolList(List<string> symbolList)
        {
            v.symbolList = symbolList;
            v.nextSymbolIndex = 0;
        }

        public void DoDailyProcessing(DateTime date, int day)
        {
            this.date = date;
            this.day = day;

            accountBalance = broker.GetBalance();

            ProcessTrades();
            UpdateTradeStatistics();

            AlgorithmAddRemovePositions();

        }

        public void ProcessTrades()
        {
            try
            {
                //look through positions updating plans
                //foreach (Trade t in v.tradeList)
                //{
                 //   ProcessTrade(t);
                //}
                for (int i = v.tradeList.Count - 1; i >= 0; i--)
                {
                    Trade t = v.tradeList[i];
                    ProcessTrade(t);
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void ProcessTrade(Trade t)
        {
            try
            {
                Position p = GetPosition(t);
                if (p == null)
                {
                    return;
                }

                UpdateTradeStates(t, p);
                AlgorithmProcessTrade(t, p);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private Position GetPosition(Trade t)
        {
            try
            {
                Position p = t.p;
                if (p == null)
                {
                    t.p = broker.GetPosition(t.symbol);
                    p = t.p;
                    if (t.p == null)
                    {
                        return null;
                    }
                }

                // remove trades of closed positions
                if (p.status == PositionStatus.Closed)
                {
                    RemoveTrade(t);
                    return null;
                }
                return p;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }

        private void UpdateTradeStates(Trade t, Position p)
        {
            //update trade stats
            if (p.percentProfit >= a.percentageDefinedAsProfitZone)
            {
                t.profitZone = true;
                t.lossZone = false;
            }
            else if (p.percentProfit <= a.percentageDefinedAsLossZone)
            {
                t.profitZone = false;
                t.lossZone = true;
            }
            else
            {
                t.profitZone = false;
                t.lossZone = false;
            }
        }
  
  //////////////////////////////////////////////////////////////////////// //////////////////////////
  
        public void AlgorithmAddRemovePositions()
        {
            try
            {
                AlgorithmAddPositions();
                AlgorithmRemovePositions();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void AlgorithmAddPositions()
        {
            try
            {
                while (AlgorithmDecideToAddNewPosition())
                {
                    AddNewPosition();
                    v.dayNumOfLastPositionAdded = day;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void AlgorithmRemovePositions()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void AlgorithmProcessTrade(Trade t, Position p)
        {
            // handle profit/loss targets
            if (p.percentProfit >= a.percentageProfitBeforeClosing)
            {
                SellOrCoverAllOfTradeAtMarket(t);
                return;
            }
            if (p.percentProfit <= a.percentageLossBeforeClosing)
            {
                SellOrCoverAllOfTradeAtMarket(t);
                return;
            }

            // handle trade probe if still adding to position

            if (t.p.quantity >= t.targetQuantity && t.p.cost >= t.targetCost)
            {
                t.doneAdding = true;
            }

            if (t.doneAdding || t.currentTradeProbeIndex >= a.tradeProbeList.Count)
            {
                t.doneAdding = true;
                return;
            }

            TradeProbe probe = a.tradeProbeList[t.currentTradeProbeIndex];

            if (day - p.lastAddDay >= probe.maxNumDaysToWait)
            {
                SellOrCoverAllOfTradeAtMarket(t);
                return;
            }

            double probeProfit;
            probeProfit = (p.close - p.lastAddPrice) / p.lastAddPrice;
            if (t.side == Side.Short)
            {
                probeProfit = -probeProfit;
            }

            if (probe.percentMoveTrigger <= probeProfit)
            {
                if (day - p.lastAddDay >= probe.minNumDaysToWait)
                {
                    double numShares;
                    if (a.useTargetShares)
                    {
                        numShares = Math.Truncate(t.targetQuantity * probe.percentPosition);
                    }
                    else
                    {
                        double cost = t.targetCost * probe.percentPosition;

                        numShares = Math.Truncate(cost / p.close);
                    }

                    BuyOrSellShortAtMarket(numShares, t);

                    if (t.currentTradeProbeIndex + 2 <= a.tradeProbeList.Count())
                    {
                        t.currentTradeProbeIndex++;
                    }
                    else
                    {
                        t.doneAdding = true;
                    }
                }
            }  
        }

        public void BuyOrSellShortAtMarket(double numShares, Trade t)
        {
            Order order = new Order();
            order.symbol = t.symbol;
            order.quantity = numShares;
            order.orderType = OrderType.Market;
            if (t.side == Side.Long)
            {
                order.orderAction = OrderAction.Buy;
            }
            else
            {
                order.orderAction = OrderAction.SellShort;
            }

            broker.GiveBrokerNewOrder(order);
        }
        public void SellOrCoverAllOfTradeAtMarket(Trade t)
        {
            Order order = new Order();
            order.symbol = t.symbol;
            if (t.side == Side.Long)
            {
                order.orderAction = OrderAction.Sell;
            }
            else
            {
                order.orderAction = OrderAction.BuyToCover;
            }
            order.orderType = OrderType.Market;
            order.quantity = t.p.quantity;

            broker.GiveBrokerNewOrder(order);
        }

        public void RemoveTrade(Trade t)
        {
            try
            {
                for (int i = 0; i < v.tradeList.Count; i++)
                {
                    if (v.tradeList[i].symbol == t.symbol)
                    {
                            v.tradeList.RemoveAt(i);
                            i--;

                            // update final stats of campaign and trader statistics
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void CloseAllTrades()
        {
        }

        public void CloseTrade()
        {

        }

        public bool AlgorithmDecideToAddNewPosition()
        {

            int currentNumTrades = v.tradeList.Count();
            if (currentNumTrades >= a.maxNumPositions)
            {
                return false;
            }

            int numDaysSinceLastTrade = day - v.dayNumOfLastPositionAdded;
            if (numDaysSinceLastTrade < a.minNumDaysBetweenAddingAnotherPosition)
            {
                return false;
            }


            // min cash balance

            // min pos size avail

            // min profit before adding
            
            
            return true;
        }

        public double CalculateTargetQuantity(double pricePerShare)
        {
            double numShares;
            numShares = Math.Truncate((accountBalance.accountValue * a.percentOfAccountToTrade * a.percentagePerPosition) / pricePerShare );
            return numShares;
        }

        public double CalculateTargetCost(double pricePerShare)
        {
            double targetCost;
            targetCost = accountBalance.accountValue * a.percentOfAccountToTrade * a.percentagePerPosition;
            return targetCost;
        }

        public void AddNewPosition()
        {
            string symbol = GetNextSymbol();
            Quote q = broker.GetQuote(symbol);

            Order order = new Order();
            order.symbol = symbol;
            order.orderAction = OrderAction.Buy;
            order.orderType = OrderType.Market;

            Trade t = new Trade();
            t.symbol = symbol;
            t.side = Side.Long;
            t.currentTradeProbeIndex = 1;

            TradeProbe probe = a.tradeProbeList.First();
            double numShares;
            if (a.useTargetShares)
            {
                t.targetQuantity = CalculateTargetQuantity(q.close);
                numShares = Math.Truncate(t.targetQuantity * probe.percentPosition);
                order.quantity = numShares;
            }
            else
            {
                t.targetCost = CalculateTargetCost(q.close);
                double cost = t.targetCost * probe.percentPosition;

                numShares = Math.Truncate(cost / q.close);
                order.quantity = numShares;
            }
            v.tradeList.Add(t);
            v.dayNumOfLastPositionAdded = day;


            if (a.useTrailingStopPercentOrders)
            {
                Order order2 = new Order();
                order2.symbol = symbol;
                order2.orderAction = OrderAction.Sell;
                order2.quantity = numShares;
                order2.orderType = OrderType.TrailingStopPercent;
                order2.trailingStopAmount = a.trailingStopPercent;

                order.conditionalOrder = order2;
            }

            broker.GiveBrokerNewOrder(order);          
        }

        private string GetNextSymbol()
        {
            string result = "";
            try
            {  
                if (v.nextSymbolIndex >= v.symbolList.Count)
                {
                    v.nextSymbolIndex = 0;
                }
                result = v.symbolList[v.nextSymbolIndex];
                v.nextSymbolIndex++;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;
        }

        public void UpdateTradeStatistics()
        {
            try
            {
                if (s.startingBalance == 0)
                {
                    s.startingBalance = accountBalance.totalValue;
                    s.highestBalance = s.startingBalance;
                    s.lowestBalance = s.startingBalance;
                }

                // endingBalance
                s.endingBalance = accountBalance.totalValue;

                // highestBalance
                if (accountBalance.totalValue > s.highestBalance)
                {
                    s.highestBalance = accountBalance.totalValue;
                }

                // lowestBalance
                if (accountBalance.totalValue < s.lowestBalance)
                {
                    s.lowestBalance = accountBalance.totalValue;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

    }
}

/*
public void AddSymbolToWatchList(string symbol)
{
    TradeWatch tw = new TradeWatch();
    tw.symbol = symbol;
    tradeWatchList.Add(tw);
}

public void RemoveSymbolFromWatchList(string symbol)
{

}

public void RemoveAllSymbolsFromWatchList()
{
    tradeWatchList.Clear();
}
 * */

//public int dayNumOfLastTradeCampaign;
//public TradeCampaign tc = null;

/*  public void ProcessCampaign()
  {
      if (tc == null)
      {
          InitializeNewTradeCampaign();
      }
  }
  public void InitializeNewTradeCampaign()
  {
      tc = new TradeCampaign();
      tc.startDate = a.date;
      tc.startDay = a.day;
      tc.startingBalance = a.balance;
      tc.day = a.day;
      tc.date = a.date;
  }
 * */