using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MarketSimulator
{
    [XmlRootAttribute("Scenario", Namespace = "", IsNullable = false)]
    public class Scenario
    {
        public string name;
        public DateTime startDate;
        public DateTime endDate;
        public int numDays;

        public List<string> symbolList;
        public string refSymbol;

        public Scenario()
        {
            this.symbolList = new List<string>(500);
        }

        public Scenario(string name, DateTime startDate, DateTime endDate, int numDays, List<string> symbolList)
        {
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
            this.numDays = numDays;
            this.symbolList = symbolList;
        }

        public void RandomizeSymbolList()
        {
            try
            {
                List<string> oldList = symbolList;
                List<string> newList = new List<string>(oldList.Count);

                while (oldList.Count > 0)
                {
                    int removeAtIndex = Randomizer.Generate(oldList.Count);
                    newList.Add(oldList[removeAtIndex]);
                    oldList.RemoveAt(removeAtIndex);
                }
                symbolList = newList;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }

    class ScenarioManager
    {
        public List<Scenario> scenarioList = new List<Scenario> (400);
        public string fileDirectory;
        public ScenarioManager()
        {
        }

        public ScenarioManager(string fileDirectory)
        {
            this.fileDirectory = fileDirectory;
        }

        public Scenario LoadScenario(string scenarioName)
        {
            Scenario result = new Scenario();
            try
            {
                TextReader textReader = new StreamReader(fileDirectory + "\\" + scenarioName + ".txt");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Scenario));
                result = (Scenario)xmlSerializer.Deserialize(textReader);
                textReader.Close();

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;
        }

        public void SaveScenario(Scenario scenario)
        {
            try
            {
                TextWriter textWriter = new StreamWriter(fileDirectory+ "\\" + scenario.name + ".txt");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Scenario));
                xmlSerializer.Serialize(textWriter, scenario);
                textWriter.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public Scenario MakeRandomScenario(string id, DateTime minStartDate, DateTime maxStartDate, int minNumDays, int maxNumDays)
        {
            Scenario result = null; 
            try
            {
                TimeSpan ts = maxStartDate.Subtract(minStartDate);
                int dayDiff = ts.Days;

                DateTime startDate = minStartDate.AddDays(Randomizer.Generate(dayDiff));

                int numDays = Randomizer.Generate(minNumDays, maxNumDays);
                DateTime endDate = QuoteDataManager.GetEndDate(startDate, numDays);
                List<string> symbolList = new List<string>(200);
                QuoteDataManager.GetValidSymbols(startDate, endDate, symbolList);

                result = new Scenario(id, startDate, endDate, numDays, symbolList);
                result.RandomizeSymbolList();
                
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return result;

        }




    }

}
