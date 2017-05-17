using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class Trade
    {
        public Side side;
        public string symbol;
        public Position p;

        // adding position targets
        public double targetCost = 0;       // total dollar amount
        //     ---- OR -----
        public double targetQuantity = 0;   // number of shares
        
        public int currentTradeProbeIndex = 0;
        public bool doneAdding = false;

        //profit / loss targets
        //public double targetPercentProfit;
        //public double targetPercentLoss;

        public bool profitZone = false;
        public bool lossZone = false;
        // statistics
        //public double numDaysTotal;


 
        /*public double numTradeAdds;
        public double numTradeSubtracts;

        public double numUpDays;
        public double numDownDays;

        public double largestPercentJump;
        public double averagePercentMove;
        public double averagePercentMoveUpDays;
        public double averagePercentMoveDownDays;
         */

        // moving day averages

        public Trade()
        {
        }
    }

}

