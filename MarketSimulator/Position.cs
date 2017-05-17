using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public enum Side { Long, Short };
    public enum PositionStatus { Open, Closed };
    public class Position
    {
        public Side side;
        public string symbol;
        public double quantity;
        public double purchasePrice;
        public PositionStatus status;

        public int day;
        public DateTime date;

        public double open;
        public double high;
        public double low;
        public double close; // last trade
        public double volume;
        
        public double yesterdaysClose;
        public double yesterdaysOpen;
        public double yesterdaysLow;
        public double yesterdaysHigh;
        public double yesterdaysVolume;

        // high and low since the position was openned
        public double positionHigh;
        public DateTime positionHighDate;
        public int positionHighDay;
        public double positionLow;
        public DateTime positionLowDate;
        public int positionLowDay;

        public DateTime openedDate;
        public int openedDay;
        public DateTime closedDate;
        public int closedDay;

        public bool splitDay = false;
        public double splitRatio = 1;

        //public List<Order> filledBuyList = new List<Order>(20);
        //public List<Order> filledSellList = new List<Order>(20);
        //public List<Order> filledSellShortList = new List<Order>(20);
        //public List<Order> filledBuyToCoverList = new List<Order>(20);

        //public List<Order> todaysOrderList = new List<Order>(10);

        public int lastAddDay;
        public double lastAddPrice;

        // stats that can be derived from the other fields
        public double cost;   // purchasePrice * quantity
        public double value; // if(Long){value = last * quantity} else if(Short){value = cost + (cost - (last * quantity))}
        public double profit; // value - cost
        public double percentProfit;    // profit / totalCost
        public double dayChange;        // close - yesterdaysClose
        public double percentDayChange; // dayChange / yesterdaysClose
        //%change = (newVal - intialVal) / intialVal;

        //public List<Order> orderHistoryList = new List<Order>(20);

        // statistics
        //public double _52weekHigh;
        //public double _52weekLow;
        //public double _50dma;
        //public double _200dma;

        public Position()
        {
        }

        public Position(Side side, string symbol)
        {
            this.side = side;
            this.symbol = symbol;
        }
        
        public void UpdateForSplit(double multiplier)
        {
            try
            {
                if (multiplier == 1)
                {
                    return;
                }
               // foreach (Order order in filledBuyList)
               // {
               //     order.UpdateOrderForSplit(multiplier);
               // }
               // foreach (Order order in filledSellList)
               // {
               //     order.UpdateOrderForSplit(multiplier);
               // }
               // foreach (Order order in filledBuyToCoverList)
               // {
               //     order.UpdateOrderForSplit(multiplier);
               // }
               // foreach (Order order in filledSellShortList)
               // {
               //     order.UpdateOrderForSplit(multiplier);
               // }

                splitDay = true;
                splitRatio = multiplier;
                quantity = Math.Truncate(quantity * multiplier);

                purchasePrice *= multiplier;
                positionHigh *= multiplier;
                positionLow *= multiplier;
                yesterdaysClose *= multiplier;
                yesterdaysOpen *= multiplier;
                yesterdaysLow *= multiplier;
                yesterdaysHigh *= multiplier;
                yesterdaysVolume *= multiplier;
                lastAddPrice *= multiplier;

                UpdateStats();

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void UpdateStats()
        {
            try
            {
                if (side == Side.Long)
                {
                    value = close * quantity;
                }
                else if (side == Side.Short)
                {
                    value = cost + (cost - (close * quantity));
                }
                profit = value - cost;
                percentProfit = profit / cost;

                dayChange = close - yesterdaysClose;
                percentDayChange = dayChange / yesterdaysClose;

                if (close >= positionHigh)
                {
                    positionHigh = close;
                    positionHighDate = date;
                    positionHighDay = day;
                }
                if (close <= positionLow)
                {
                    positionLow = close;
                    positionLowDate = date;
                    positionLowDay = day;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

    }
}

