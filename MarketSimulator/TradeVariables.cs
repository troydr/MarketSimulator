using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class TradeVariables
    {
        public List<string> symbolList;

        public List<Trade> tradeList = new List<Trade>(50);

        //public List<TradeWatch> tradeWatchList = new List<TradeWatch>(10);

        public int dayNumOfLastPositionAdded = 0;
        public int nextSymbolIndex = 0;
      
        public TradeVariables()
        {
        }

        public void Clear()
        { 
            symbolList = null;
            tradeList.Clear();
            dayNumOfLastPositionAdded = 0;
            nextSymbolIndex = 0;
        }

    }
}
