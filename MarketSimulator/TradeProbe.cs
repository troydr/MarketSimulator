using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class TradeProbe
    {
        public double percentPosition;
        public double percentMoveTrigger;
        public int minNumDaysToWait;
        public int maxNumDaysToWait;

        public TradeProbe()
        {
        }

        public TradeProbe(double percentPosition, double percentMoveTrigger, int minNumDaysToWait, int maxNumDaysToWait)
        {
            this.percentPosition = percentPosition;
            this.percentMoveTrigger = percentMoveTrigger;
            this.minNumDaysToWait = minNumDaysToWait;
            this.maxNumDaysToWait = maxNumDaysToWait;
        }

    }
}
