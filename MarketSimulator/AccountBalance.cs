using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class AccountBalance
    {
        public DateTime date;
        public int day;

        public double accountValue;
        public double cashBalance;
        public double buyingPower; // could be higher than accountValue if margin is used
        public double longStockValue;
        public double shortStockValue;
        public double totalValue; // bankBalance + account.accountValue
        public double bankBalance;

        public AccountBalance()
        {
        }

        public AccountBalance(AccountBalance balance)
        {
            date = balance.date;
            day = balance.day;

            accountValue = balance.accountValue;
            cashBalance = balance.cashBalance;
            buyingPower = balance.buyingPower;
            longStockValue = balance.longStockValue;
            shortStockValue = balance.shortStockValue;
            totalValue = balance.totalValue;
            bankBalance = balance.bankBalance;
        }

        public void Clear()
        {
            day = 0;
            accountValue = 0;
            cashBalance = 0;
            buyingPower = 0;
            longStockValue = 0;
            shortStockValue = 0;
            totalValue = 0;
            bankBalance = 0;
        }

        public string TextDelimited()
        {
            string output = "";
            try
            {
                output += day.ToString() + ",";
                output += date.ToString("yyyy-MM-dd") + ",";
                output += accountValue.ToString("N2") + ",";
                output += cashBalance.ToString("N2") + ",";
                output += longStockValue.ToString("N2") + ",";
                output += shortStockValue.ToString("N2");
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return output;
        }
    }
}

/*
Cash Balance - The amount of liquid funds in your account, including the monetary value of trades that may not have settled, but excluding any money market funds.
Margin balance - A negative number that represents a debit balance or the amount that is on loan. The debit balance is subject to margin interest charges. Margin balance is only displayed if your account is approved for margin.
Short balance - The balance in your short account if you have short positions. The short account credit balance is initially equal to the sales proceeds of each short sell. After the short sale(s) settles, journal entries called "mark to market" adjustments are done daily to keep the short account credit equal to the cost of buying back the short position based on the previous day's closing price. Closing out all short positions may still result in a debit or credit in the short account until all trades have settled. Short balance is only displayed if your account is approved for margin.
Long Stock Value - The long stock value is the total value of your individual long stock positions based on the last price for those stocks.
Short Stock Value - The short stock value is the total value of your individual short stock positions based on the last price for those stocks.

                //public double availableFunds;
                //public double marginBalance;
                //public double shortBalance;// amount to buyToClose the short positions
                //public double shortStockValue; //profit/loss only
                //private double marginPercent;// ie 100%(1:1)
                //private double marginMaintenceRequirement;// 30% to 35%

                //else
                //{
                //    shortStockValue += (position.purchasePrice - position.price) * position.quantity;
                //}
                //if (marginBalance > 0 && cashBalance > 0)
                //{
                //    marginBalance -= cashBalance;
                //    cashBalance = 0;
                //}
                //accountValue += shortStockValue;
                //accountValue -= marginBalance;
                //accountValue -= shortBalance;
                //availableFunds = (cashBalance + longStockValue + shortStockValue) * marginPercent;
                //availableFunds -= marginBalance;
                //availableFunds -= shortBalance
 * 
 * 
 * 
 * 
 * Long marginable value  	 
	Short marginable value 			
	Margin equity 		
	Equity percentage 		
	Maint. requirement
 */
