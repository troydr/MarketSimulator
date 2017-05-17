using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MarketSimulator
{
    [XmlRootAttribute("TradeAttributes", Namespace = "MarketSimulator", IsNullable = false)]
    public class TradeAttributes
    {
        public string name;

        public int maxNumPositions = 2;
        public int minNumPositions = 1;
        public int maxNumPositionAddsPerDay = 1;
        public int minNumDaysBetweenAddingAnotherPosition = 1;
        public double percentagePerPosition = 0.50;
        public double percentOfAccountToTrade = 1.00;

        public bool useStopLossOrders = false;
        
        public bool useTrailingStopPercentOrders = true;
        public double trailingStopPercent = 0.10;

        public double percentageLossBeforeClosing = -0.20;
        public double percentageProfitBeforeClosing = 4.99;

        public double percentagePositionsInProfitBeforeAddingNewPosition;

        public double percentageDefinedAsProfitZone = 0.05;
        public double percentageDefinedAsLossZone = -0.05;

        public double numDaysTimeStopDeadzone = 10;
        public double numDaysTimeStopProfitZone = 300;
        public double numDaysTimeStopLossZone = 10;

        public bool useTargetCost = true;       // ie $10,000
        public bool useTargetShares = false;    // ie 100 shares
        public List<TradeProbe> tradeProbeList;

        //public int maxNumDaysOfCampaign;
        //public int minNumDaysWaitAfterWinningCampaign;
        //public int minNumDaysWaitAfterLosingCampaign;
        //public float maxPercentageLossBeforeEndingCampaign;
        //public float maxPercentGainBeforeEndingCampaign;

        // timestops
        /*public int noProfitOrLossTimeStop;
        public int smallProfitTimeStop;
        public int smallLossTimeStop;
        public int mediumProfitTimeStop;
        public int mediumLossTimeStop;

        // profit loss zones
        public double smallProfitZone;
        public double smallLossZone;
        public double mediumProfitZone;
        public double mediumLossZone;
        public double deepProfitZone;
        public double deepLossZone;
         * */

        public TradeAttributes()
        {
            tradeProbeList = new List<TradeProbe>(20);

            TradeProbe probe = new TradeProbe();
            probe.percentPosition = 0.20;
            probe.percentMoveTrigger = 0.00;
            probe.minNumDaysToWait = 0;
            probe.maxNumDaysToWait = 0;

            tradeProbeList.Add(probe);
            
            probe = new TradeProbe();
            probe.percentPosition = 0.20;
            probe.percentMoveTrigger = 0.05;
            probe.minNumDaysToWait = 1;
            probe.maxNumDaysToWait = 5;

            tradeProbeList.Add(probe);

            probe = new TradeProbe();
            probe.percentPosition = 0.20;
            probe.percentMoveTrigger = 0.05;
            probe.minNumDaysToWait = 3;
            probe.maxNumDaysToWait = 15;

            tradeProbeList.Add(probe);

            probe = new TradeProbe();
            probe.percentPosition = 0.40;
            probe.percentMoveTrigger = 0.05;
            probe.minNumDaysToWait = 5;
            probe.maxNumDaysToWait = 20;

            tradeProbeList.Add(probe);
            /*
                        probe = new TradeProbe();
                        probe.percentPosition = 0.20;
                        probe.percentMoveTrigger = 0.02;
                        probe.minNumDaysToWait = 30;
                        probe.maxNumDaysToWait = 9999;

                        tradeProbeList.Add(probe);

                        probe = new TradeProbe();
                        probe.percentPosition = 0.40;
                        probe.percentMoveTrigger = 0.02;
                        probe.minNumDaysToWait = 30;
                        probe.maxNumDaysToWait = 9999;

                        tradeProbeList.Add(probe);
            */
        }

        public void RandomizeAttributes()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }

    class TradeAttributesManager
    {
        public List<TradeAttributes> TradeAttributesList = new List<TradeAttributes> (400);

        public string fileDirectory;
        public TradeAttributesManager()
        {
        }

        public TradeAttributesManager(string fileDirectory)
        {
            this.fileDirectory = fileDirectory;
        }

        public TradeAttributes LoadTraderAttributes(string TradeAttributesName)
        {
            TradeAttributes result = new TradeAttributes();
            try
            {
                TextReader textReader = new StreamReader(fileDirectory + "\\" + TradeAttributesName + ".txt");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TradeAttributes));
                result = (TradeAttributes)xmlSerializer.Deserialize(textReader);
                textReader.Close();

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;
        }

        public void SaveTraderAttributes(TradeAttributes tradeAttributes)
        {
            try
            {
                TextWriter textWriter = new StreamWriter(fileDirectory + "\\" + tradeAttributes.name + ".txt");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TradeAttributes));
                xmlSerializer.Serialize(textWriter, tradeAttributes);
                textWriter.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public TradeAttributes MakeRandomTradeAttributes()
        {
            TradeAttributes result = null; 
            try
            {
              
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;
        }
    }
}
