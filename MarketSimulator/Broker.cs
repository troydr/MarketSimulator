using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class Broker
    {
        public DateTime date;
        public int day;

        public Account account;
        public double orderFee;
        public double orderSplippagePercent;
        
        public Broker(Account account, double orderFee, double orderSplippagePercent)
        {
            try
            {
                SetStartingValues(account, orderFee, orderSplippagePercent);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void SetStartingValues(Account account, double orderFee, double orderSplippagePercent)
        {
            try
            {
                this.account = account;
                this.orderFee = orderFee;
                this.orderSplippagePercent = orderSplippagePercent;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void Clear()
        {
            try
            {
                day = 0;
                account = null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public Quote GetQuote(string symbol)
        {
            try
            {
                return QuoteDataManager.GetQuote(symbol, date);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }          
        }

        public AccountBalance GetBalance()
        {
            try
            {
                return account.balance;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }

        public Position GetPosition(string symbol)
        {
            try
            {
                return account.GetPosition(symbol);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }

        public void DoDailyProcessing(DateTime date, int day)
        {
            try
            {
                this.date = date;
                this.day = day;
                account.day = day;
                account.date = date;

                //Logger.WriteLine(MarketSimulator.console, "Day " + day + "  " + date.ToString());

                ProcessPositions();
                account.CalculateBalance();

                ProcessOrders();
                
                MarkClosedPositions();
                account.CalculateBalance();       
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void FinishDailyProcessing()
        {
            try
            {
                MarkClosedPositions();
                account.FinalizeTradingDay();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void ProcessPositions()
        {
            try
            {
                foreach (Position position in account.positionList)
                {
                    ProcessPosition(position);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void ProcessPosition(Position p)
        {
            try
            {
                p.day = day;
                p.date = date;
                //position.todaysOrderList.Clear();

                p.yesterdaysClose = p.close;
                p.yesterdaysOpen = p.open;
                p.yesterdaysLow = p.low;
                p.yesterdaysHigh = p.high;
                p.yesterdaysVolume = p.volume;

                QuoteData qd = QuoteDataManager.quoteDataHash[p.symbol];
                Quote q = qd.GetQuote(date);
                p.open = q.open;
                p.high = q.high;
                p.low = q.low;
                p.close = q.close;
                p.volume = q.volume;

                // handle split days
                p.splitDay = false;
                p.splitRatio = 1;
                double splitMultiplier = 1;
                foreach (Split split in qd.splitList)
                {
                    if (split.date == date)
                    {
                        splitMultiplier = split.multiplier;
                        break;
                    }
                }
                if (splitMultiplier != 1)
                {
                    p.UpdateForSplit(splitMultiplier);
                    
                    foreach (Order order in account.orderList)
                    {
                        if (order.symbol == p.symbol)
                        {
                            order.UpdateOrderForSplit(splitMultiplier);
                        }
                    }
                }

                p.UpdateStats();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void ProcessOrders()
        {
            foreach (Order order in account.orderList)
            {
                CheckOrder(order);
            }

            // removed closed orders
            for (int i = 0; i < account.orderList.Count; i++)
            {
                if (account.orderList[i].orderStatus != OrderStatus.Open)
                {
                    account.orderList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void MarkClosedPositions()
        {
            foreach (Position position in account.positionList)
            {
                if (position.quantity < 1)
                {
                    position.closedDate = date;
                    position.closedDay = day;
                    position.status = PositionStatus.Closed;
                }
            }
        }

        public void CancelAllOrders(string symbol)
        {
            try
            {
                foreach (Order order in account.orderList)
                {
                    if (order.symbol == symbol)
                    {
                        order.orderStatus = OrderStatus.Cancelled;
                        order.cancelledDay = day;
                        order.cancelledDate = date;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void GiveBrokerNewOrder(Order order)
        {
            try
            {
                order.openedDate = date;
                order.openedDay = day;

                //Logger.WriteLine(MarketSimulator.console, order.TextDelimited());
                Position p = account.GetPosition(order.symbol); // check for existing position
                if (p == null) // new position need to create it
                {
                    if (order.orderAction == OrderAction.Buy)
                    {
                        p = new Position(Side.Long, order.symbol);
                        InitializeNewPosition(p);
                        account.positionList.Add(p); 
                    }
                    else if (order.orderAction == OrderAction.SellShort)
                    {
                        p = new Position(Side.Short, order.symbol);
                        InitializeNewPosition(p);
                        account.positionList.Add(p);
                    }
                    else
                    {
                        order.orderStatus = OrderStatus.Cancelled;
                        order.cancelledDate = date;
                        order.cancelledDay = day;
                        return;
                    }
                }

                if (order.orderType == OrderType.Market)
                {
                    if (order.orderAction == OrderAction.Buy)
                    {
                        order.filledPrice = p.close * (1 + orderSplippagePercent);
                        Buy(order, p);
                    }
                    else if (order.orderAction == OrderAction.Sell)
                    {
                        order.filledPrice = p.close * (1 - orderSplippagePercent);
                        Sell(order, p);
                    }
                    else if (order.orderAction == OrderAction.SellShort)
                    {
                        order.filledPrice = p.close * (1 - orderSplippagePercent);
                        SellShort(order, p);
                    }
                    else if (order.orderAction == OrderAction.BuyToCover)
                    {
                        order.filledPrice = p.close * (1 + orderSplippagePercent);
                        BuyToCover(order, p);
                    }
                }
                else  //handle stop orders, limit orders, trailing stop orders
                {
                    order.orderStatus = OrderStatus.Open;
                    if (order.orderType == OrderType.TrailingStopDollar || order.orderType == OrderType.TrailingStopPercent)
                    {
                        SetInitialActivationPrice(order, p);
                    }

                    account.orderList.Add(order);
                }
                account.CalculateBalance();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void SetInitialActivationPrice(Order order, Position p)
        {
            if (order.orderAction == OrderAction.Sell)
            {
                if (order.orderType == OrderType.TrailingStopPercent)
                {
                    order.activationPrice = p.close * (1 - order.trailingStopAmount);
                }
                else if (order.orderType == OrderType.TrailingStopDollar)
                {
                    order.activationPrice = p.close - order.trailingStopAmount;
                }
            }
            else if (order.orderAction == OrderAction.BuyToCover)
            {
                if (order.orderType == OrderType.TrailingStopPercent)
                {
                    order.activationPrice = p.close * (1 + order.trailingStopAmount);
                }
                else if (order.orderType == OrderType.TrailingStopDollar)
                {
                    order.activationPrice = p.close + order.trailingStopAmount;
                }
            }
        }

        private void InitializeNewPosition(Position position)
        {
            try
            {
                position.day = day;
                position.date = date;

                Quote q = QuoteDataManager.GetQuote(position.symbol, date);

                position.close = q.close;
                position.open = q.open;
                position.high = q.high;
                position.low = q.low;
                position.volume = q.volume;

                position.yesterdaysClose = q.close;
                position.yesterdaysOpen = q.open;
                position.yesterdaysHigh = q.high;
                position.yesterdaysLow = q.low;
                position.yesterdaysVolume = q.volume;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void CheckOrder(Order order)
        {
            if (order.orderStatus != OrderStatus.Open)
            {
                return;
            }

            Position p = account.GetPosition(order.symbol);
            if (p == null)
            {
                order.orderStatus = OrderStatus.Cancelled;
                return;
            }

            try
            {
                if (order.orderAction == OrderAction.Sell)
                {
                    CheckSellOrder(order, p);
                }
                else if (order.orderAction == OrderAction.Buy)
                {
                    CheckBuyOrder(order, p);
                }
                else if (order.orderAction == OrderAction.BuyToCover)
                {
                    CheckBuyToCoverOrder(order, p);
                }
                else if (order.orderAction == OrderAction.SellShort)
                {
                    CheckSellShortOrder(order, p);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void CheckSellOrder(Order order, Position p)
        {
            switch (order.orderType)
            {
                case OrderType.StopMarket:
                    if (p.low <= order.activationPrice)
                    {
                        if (p.open < order.activationPrice) // cant stop out pre-market
                        {
                            order.filledPrice = p.open * (1 - orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.activationPrice * (1 - orderSplippagePercent);
                        }
                        Sell(order, p);
                    }
                    break;
                case OrderType.TrailingStopPercent:
                    if (p.high * (1 - order.trailingStopAmount) > order.activationPrice)
                    {
                        order.activationPrice = p.high * (1 - order.trailingStopAmount);
                        order.numberOfTrailingStopUpdates++;
                    }

                    if (p.low <= order.activationPrice)
                    {
                        if (p.open < order.activationPrice) // cant stop out pre-market
                        {
                            order.filledPrice = p.open * (1 - orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.activationPrice * (1 - orderSplippagePercent);
                        }
                        Sell(order, p);
                    }
                    break;
                case OrderType.TrailingStopDollar:
                    if (p.high - order.trailingStopAmount > order.activationPrice)
                    {
                        order.activationPrice = p.high - order.trailingStopAmount;
                        order.numberOfTrailingStopUpdates++;
                    }

                    if (p.low <= order.activationPrice)
                    {
                        if (p.open < order.activationPrice) // cant stop out pre-market
                        {
                            order.filledPrice = p.open * (1 - orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.activationPrice * (1 - orderSplippagePercent);
                        }
                        Sell(order, p);
                    }
                    break;
                case OrderType.Limit:
                    if (p.high >= order.limitPrice)
                    {
                        order.filledPrice = order.limitPrice;
                        Sell(order, p);
                    }
                    break;
                case OrderType.Market:
                    order.filledPrice = p.close * (1 - orderSplippagePercent);
                    Sell(order, p);
                    break;
                default:
                    break;
            }
        }
       
        private void CheckBuyOrder(Order order, Position p)
        {
            switch (order.orderType)
            {
                case OrderType.StopMarket:
                    if (p.high >= order.activationPrice)
                    {
                        if (p.open > order.activationPrice) // cant buy pre-market
                        {
                            order.filledPrice = p.open * (1 + orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.activationPrice * (1 + orderSplippagePercent);
                        }
                        Buy(order, p);
                    }
                    break;
                case OrderType.Limit:
                    if (p.low <= order.limitPrice)
                    {
                        order.filledPrice = order.limitPrice;
                        Buy(order, p);
                    }
                    break;
                case OrderType.Market:
                    order.filledPrice = p.close * (1 + orderSplippagePercent);
                    Buy(order, p);
                    break;
                default:
                    break;
            }
        }

        private void Buy(Order order, Position p)
        {
            try
            {
                if (order.filledPrice > p.high) // incase the calc slippage price is higher than the high
                {
                    order.filledPrice = p.high;
                }

                order.filledQuantity = order.quantity; // set full requesed quantity
                double cost = order.filledQuantity * order.filledPrice; // see how much it costs        
                if (cost + orderFee > account.balance.buyingPower) // check if enough funds to buy requested quantity
                {
                    // this is how much can buy
                    order.filledQuantity = Math.Truncate((account.balance.buyingPower - orderFee) / order.filledPrice);
                    
                    // new cost
                    cost = order.filledQuantity * order.filledPrice;
                }

                account.balance.cashBalance -= cost;
                account.balance.cashBalance -= orderFee;

                p.cost += cost;
                p.quantity += order.filledQuantity;

                // calculate averaged purchase price
                p.purchasePrice = p.cost / p.quantity;     // old stupid way: ((order.filledPrice * order.filledQuantity) + (p.quantity * p.purchasePrice)) / (p.quantity + order.filledQuantity);

                order.filledDate = date;
                order.filledDay = day;

                if (p.lastAddDay == 0)
                {
                    p.openedDate = date;
                    p.openedDay = day;
                }
                p.lastAddDay = day;
                p.lastAddPrice = order.filledPrice;
                order.orderStatus = OrderStatus.Filled;

                //p.filledBuyList.Add(order);
                //p.todaysOrderList.Add(order);
                p.UpdateStats();
                account.CalculateBalance();
                if (order.conditionalOrder != null)
                {
                    GiveBrokerNewOrder(order.conditionalOrder);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void Sell(Order order, Position p)
        {
            try
            {
                if (order.filledPrice < p.low) // incase the calc slippage price is lower than the low
                {
                    order.filledPrice = p.low;
                }

                order.filledQuantity = order.quantity;
                if (order.filledQuantity > p.quantity)
                {
                    order.filledQuantity = p.quantity;
                }

                p.quantity -= order.filledQuantity;

                double saleRevenue = order.filledQuantity * order.filledPrice;
                account.balance.cashBalance += saleRevenue;
                account.balance.cashBalance -= orderFee;

                order.orderStatus = OrderStatus.Filled;
                order.filledDate = date;
                order.filledDay = day;

                //p.filledSellList.Add(order);
                //p.todaysOrderList.Add(order);

                if (p.quantity == 0)
                {
                    p.closedDate = date;
                    p.closedDay = day;
                    p.status = PositionStatus.Closed;
                    //account.positionList.Remove(p);
                }
                else if(p.quantity < 0)
                {
                    throw new Exception("p.quantity < 0");
                }

                p.UpdateStats();
                account.CalculateBalance();
                if (order.conditionalOrder != null)
                {
                    GiveBrokerNewOrder(order);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }













        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        private void BuyToCover(Order order, Position p)
        {
            try
            {
                if (order.filledPrice > p.high) // incase the calc slippage price is higher than the high
                {
                    order.filledPrice = p.high;
                }

                order.filledQuantity = order.quantity;
                if (order.filledQuantity > p.quantity)
                {
                    order.filledQuantity = p.quantity;
                }

                p.quantity -= order.filledQuantity;
                double saleRevenue = (order.filledQuantity * p.purchasePrice) + ((p.purchasePrice - order.filledPrice) * order.filledQuantity);

                account.balance.cashBalance += saleRevenue;
                account.balance.cashBalance -= orderFee;

                order.orderStatus = OrderStatus.Filled;
                order.filledDate = date;
                order.filledDay = day;

                //p.filledBuyToCoverList.Add(order);
                //p.todaysOrderList.Add(order);

                p.UpdateStats();
                account.CalculateBalance();
                if (order.conditionalOrder != null)
                {
                    GiveBrokerNewOrder(order);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void SellShort(Order order, Position p)
        {
            try
            {
                if (order.filledPrice < p.low) // incase the calc slippage price is lower than the low
                {
                    order.filledPrice = p.low;
                }
                double cost = order.quantity * order.filledPrice;
                order.filledQuantity = order.quantity;
                if (cost + orderFee > account.balance.cashBalance)
                {
                    order.filledQuantity = (account.balance.cashBalance - orderFee) / order.filledPrice;
                    order.filledQuantity = Math.Truncate(p.quantity);
                }

                account.balance.cashBalance -= cost;
                account.balance.cashBalance -= orderFee;

                p.purchasePrice = ((order.filledPrice * order.filledQuantity) + (p.quantity * p.purchasePrice)) / (p.quantity + order.filledQuantity);
                p.quantity += order.quantity;

                order.orderStatus = OrderStatus.Filled;
                order.filledDate = date;
                order.filledDay = day;
                p.lastAddDay = day;
                p.lastAddPrice = order.filledPrice;

                //p.filledSellShortList.Add(order);
                //p.todaysOrderList.Add(order);

                p.UpdateStats();
                account.CalculateBalance();
                if (order.conditionalOrder != null)
                {
                    GiveBrokerNewOrder(order);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }



        private void CheckBuyToCoverOrder(Order order, Position p)
        {
            switch (order.orderType)
            {
                case OrderType.StopMarket:
                    if (p.high >= order.activationPrice)
                    {
                        if (p.open > order.activationPrice) // cant stop out pre-market
                        {
                            order.filledPrice = p.open * (1 + orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.activationPrice * (1 + orderSplippagePercent);
                        }
                        BuyToCover(order, p);
                    }
                    break;
                case OrderType.TrailingStopPercent:
                    if (p.low * order.trailingStopAmount < order.activationPrice)
                    {
                        order.activationPrice = p.low * order.trailingStopAmount;
                        order.numberOfTrailingStopUpdates++;
                    }

                    if (p.high >= order.activationPrice)
                    {
                        if (p.open > order.activationPrice) // cant stop out pre-market
                        {
                            order.filledPrice = p.open * (1 + orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.activationPrice * (1 + orderSplippagePercent);
                        }
                        BuyToCover(order, p);
                    }
                    break;

                case OrderType.TrailingStopDollar:
                    if (p.low + order.trailingStopAmount < order.activationPrice)
                    {
                        order.activationPrice = p.low + order.trailingStopAmount;
                        order.numberOfTrailingStopUpdates++;
                    }

                    if (p.high >= order.activationPrice)
                    {
                        if (p.open > order.activationPrice) // cant stop out pre-market
                        {
                            order.filledPrice = p.open * (1 + orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.activationPrice * (1 + orderSplippagePercent);
                        }
                        BuyToCover(order, p);
                    }
                    break;
                case OrderType.Limit:
                    if (p.low <= order.limitPrice)
                    {
                        order.filledPrice = order.limitPrice;
                        BuyToCover(order, p);
                    }
                    break;
                case OrderType.Market:
                    order.filledPrice = p.close * (1 + orderSplippagePercent);
                    BuyToCover(order, p);
                    break;
                default:
                    break;
            }
        }

        private void CheckSellShortOrder(Order order, Position p)
        {
            switch (order.orderType)
            {
                case OrderType.StopMarket:
                    if (p.high >= order.activationPrice)
                    {
                        if (p.open > order.activationPrice) // cant buy pre-market
                        {
                            order.filledPrice = p.open * (1 - orderSplippagePercent);
                        }
                        else
                        {
                            order.filledPrice = order.limitPrice * (1 - orderSplippagePercent);
                        }
                        SellShort(order, p);
                    }
                    break;
                case OrderType.Limit:
                    if (p.low <= order.limitPrice)
                    {
                        order.filledPrice = order.limitPrice;
                        SellShort(order, p);
                    }
                    break;
                case OrderType.Market:
                    order.filledPrice = p.close * (1 - orderSplippagePercent);
                    SellShort(order, p);
                    break;
                default:
                    break;
            }
        }


    }//class
}//namespace

/*
        private void SellShort(Order order)
        {
            try
            {
                Position p = GetPosition(order.symbol);
                if (p == null)
                {
                    p = new Position(Side.Short, order.symbol);
                    account.positionList.Add(p);
                }

                p.quantity += order.quantity;

                p.price = ((order.price * order.quantity) + (p.quantity * p.price)) / (p.quantity + order.quantity);

                account.cashBalance -= orderFee;
                account.CalculateBalance();

                if (account.recordHistory)
                {
                    order.orderStatus = OrderStatus.Filled;
                    account.RecordOrder(order);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void BuyToCover(Order order)
        {
            try
            {
                Position p = GetPosition(order.symbol);
                if (p == null)
                {
                    order.orderStatus = OrderStatus.Cancelled;
                    if (account.recordHistory)
                    {
                        account.RecordOrder(order);
                    }
                    return;
                }

                if (order.quantity > p.quantity)
                {
                    order.quantity = p.quantity;
                }

                p.quantity -= order.quantity;
                order.orderStatus = OrderStatus.Filled;

                double cost = order.quantity * order.price;
                account.cashBalance -= cost;
                account.cashBalance -= orderFee;

                account.CalculateBalance();

                if (account.recordHistory)
                {
                    order.orderStatus = OrderStatus.Filled;
                    account.RecordOrder(order);
                }

                if (p.quantity == 0)
                {
                    account.positionList.Remove(p);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
         private void MarketSellShort(Order order)
        {
            try
            {
                QuoteData qd = quoteDataHash[order.symbol];
                Quote q = qd.quoteList//[day];

                double order.price = q.close - (q.close * orderSplippagePercent);
                if (order.price < q.low)
                {
                    order.price = q.low;
                }

                SellShort(order);

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void MarketBuyToCover(Order order)
        {
            try
            {
                QuoteData qd = quoteDataHash[order.symbol];
                Quote q = qd.quoteList//[day];

                double pricePerShare = q.close + (q.close * orderSplippagePercent);
                if (pricePerShare > q.high)
                {
                    pricePerShare = q.high;
                }
                order.price = pricePerShare;

                BuyToCover(order);

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
 * 
 * 
 * 
 * 
 */
