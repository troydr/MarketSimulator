using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public enum OrderStatus { Open, Filled, Cancelled };
    public enum OrderAction { Buy, Sell, SellShort, BuyToCover };
    public enum OrderType { Limit, Market, StopMarket, TrailingStopPercent, TrailingStopDollar };

    public class Order
    {
        public OrderStatus orderStatus;
        public OrderAction orderAction;
        public OrderType orderType;
        
        public double quantity;
        public string symbol;

        public double limitPrice;           // holds limit price
        public double activationPrice;      // holds StopMarket price
        public double trailingStopAmount;   // holds trailingStop percent amount or trailingStop dollar amount

        public DateTime openedDate;
        public int openedDay;

        public DateTime filledDate;
        public int filledDay;
        public double filledPrice;
        public double filledQuantity;

        public DateTime cancelledDate;
        public int cancelledDay;

        public int numberOfTrailingStopUpdates;

        public Order conditionalOrder = null; // order that gets triggered if this order is filled - example: for a stoploss

        public Order()
        {
        }
        /*public Order(OrderAction orderAction, OrderType orderType, OrderStatus orderStatus, string symbol, double quantity, double price, double trailingStop, DateTime date, int day)
        {
            try
            {
                this.orderStatus = orderStatus;
                this.orderAction = orderAction;
                this.orderType = orderType;

                this.quantity = quantity;
                this.symbol = symbol;
                this.price = price;

                this.activationPrice = trailingStop;

                this.openDate = date;
                this.openDay = day;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }*/

        public Order(Order order)
        {
            try
            {
                this.orderStatus = order.orderStatus;
                this.orderAction = order.orderAction;
                this.orderType = order.orderType;

                this.quantity = order.quantity;
                this.symbol = order.symbol;
                this.limitPrice = order.limitPrice;

                this.activationPrice = order.activationPrice;

                this.openedDate = order.openedDate;
                this.openedDay = order.openedDay;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void UpdateOrderForSplit(double multiplier)
        {
            try
            {
                quantity = Math.Truncate(quantity * multiplier);
                limitPrice = limitPrice / multiplier;
                activationPrice = limitPrice / multiplier;

                if (orderType == OrderType.TrailingStopDollar || orderType == OrderType.TrailingStopPercent)
                {
                    trailingStopAmount = trailingStopAmount / multiplier;
                }

                if (this.conditionalOrder != null)
                {
                    conditionalOrder.UpdateOrderForSplit(multiplier);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        // Bought           X shares of Y symbol at Z price
        // Sold             X shares of Y symbol at Z price
        // Sold Short       X shares of Y symbol at Z price
        // Bought to Cover  X shares of Y symbol at Z price
        // Sell_Stop triggered for X shares of symbol Y filled at Z price
        // Buy_Stop triggered for X shares of symbol Y filled at Z price
        // Order to Buy X shares of Y symbol at Z price
        // Cancelled: Order to Buy X shares of Y symbol at Z price
        // Stop market order to Sell 200 X, with an activation price of Y.

        public string TextVerbose()
        {
            string output = "";
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return output;
        }

        // Open, Filled, Cancelled 
        // Buy, Sell, SellShort, BuyToCover
        // Limit, Market, StopMarket
        // day, date, status, action, type, quantity, symbol, price
        public string TextDelimited()
        {
            string output = "";
            try
            {
                output += openedDay.ToString() + ",";
                output += openedDate.ToString("yyyy-MM-dd") + ",";
                output += orderStatus.ToString() + ",";
                output += orderAction.ToString() + ",";
                output += orderType.ToString() + ",";               
                output += quantity.ToString("f0") + ",";
                output += symbol + ",";
                output += limitPrice.ToString("N2");
                output += activationPrice.ToString("N2");

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return output;
        }
    }
}

