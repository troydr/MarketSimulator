using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class Account
    {
        public DateTime date;
        public int day;

        public AccountBalance balance = new AccountBalance();
        public List<Position> positionList = new List<Position>(50);
        public List<Order> orderList = new List<Order>(50);

        //public List<AccountBalance> accountBalanceHistoryList = new List<AccountBalance>(100);
        //public List<Position> closedPositionList = new List<Position>(100);
        //public List<Order> closedOrderList = new List<Order>(100);
        //todo: dailyOrdersfilled list

        public Account()
        {
        }

        public Account(double cash)
        {
            SetStartingValues(cash);
        }

        public void Clear()
        {
            day = 0;
            balance.Clear();
            positionList.Clear();
            orderList.Clear();
            //accountBalanceHistoryList.Clear();
            //closedPositionList.Clear();
            //closedOrderList.Clear();
        }

        public void SetStartingValues(double cash)
        {
            balance.cashBalance = cash;
            balance.accountValue = cash;
            balance.bankBalance = 0;
            balance.buyingPower = cash;
            balance.totalValue = balance.accountValue + balance.bankBalance;
        }

        public void CalculateBalance()
        {
            try
            {
                balance.date = date;
                balance.day = day;
                balance.accountValue = balance.cashBalance;
                balance.longStockValue = 0;
                balance.shortStockValue = 0;

                foreach (Position position in positionList)
                {
                    if (position.status != PositionStatus.Open)
                    {
                        continue;
                    }
                    if (position.side == Side.Long)
                    {
                        balance.longStockValue += position.value;
                    }
                    else if (position.side == Side.Short)
                    {
                        balance.shortStockValue += position.value;
                    }
                }

                balance.accountValue += balance.longStockValue;
                balance.accountValue += balance.shortStockValue;
                balance.totalValue = balance.bankBalance + balance.accountValue;

                // need to look at open orders when calculating buying power?
                balance.buyingPower = balance.cashBalance;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void FinalizeTradingDay()
        {
            RemoveClosedPositions();
            CalculateBalance();
            //StoreBalance();
        }

       // public void StoreBalance()
       // {
       //     AccountBalance currentBalance = new AccountBalance(balance);
       //     accountBalanceHistoryList.Add(currentBalance);
       // }

        public void RemoveClosedPositions()
        {
            try
            {
                for (int i = 0; i < positionList.Count; i++)
                {
                    if (positionList[i].status == PositionStatus.Closed)
                    {
                        //closedPositionList.Add(p);
                        positionList.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void BankDeposit(double amount)
        {
            try
            {
                if (amount > 0)
                {
                    if (balance.cashBalance - amount < 0)
                    {
                        amount = balance.cashBalance;                      
                    }
                    balance.bankBalance += amount;
                    balance.cashBalance -= amount;

                    CalculateBalance();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void BankWithdrawal(double amount)
        {
            try
            {
                if (amount > 0)
                {
                    double amountWithdrawn = 0;
                    if (balance.bankBalance - amount > 0)
                    {
                        amountWithdrawn = amount;
                        balance.bankBalance -= amountWithdrawn;
                        balance.cashBalance += amountWithdrawn;
                    }
                    else
                    {
                        amountWithdrawn = balance.bankBalance;
                        balance.bankBalance = 0;
                        balance.cashBalance += amountWithdrawn;
                    }

                    CalculateBalance();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public Position GetPosition(string symbol)
        {
            try
            {
                Position p = null;
                p = positionList.Find(pos => pos.symbol == symbol);
                return p;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return null;
            }
        }
    }
}


