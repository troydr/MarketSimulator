using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class TradeCampaign
    {
        public DateTime startDate;
        public int startDay;
        public DateTime endDate;
        public int endDay;

        public int day;
        public DateTime date;

        public int numberOfWinners;
        public int numberOfLosers;

        public int numberOfLongWinners;
        public int numberOfLongLosers;

        public int numberOfShortWinners;
        public int numberOfShortLosers;

        public double averagePercentGainOfWinners;
        public double averagePercentLossOfLosers;

        public AccountBalance startingBalance;
        public AccountBalance endingBalance;

        public double currentProfitPercent;
        
        public TradeCampaign()
        {

        }

    }
}
