using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public static class TradeMath
    {
        public static double PercentChange(double newValue, double initialValue)
        {
            return (newValue - initialValue) / initialValue;
        }
    }
}
