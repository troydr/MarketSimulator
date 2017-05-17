using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarketSimulator
{
    public partial class SimulatorUI : Form
    {
        //LogForm logForm = new LogForm();
        public SimulatorUI()
        {
            InitializeComponent();

            try
            {               
                //logForm.MdiParent = this.MdiParent;
                //logForm.Text = "Simulator Log";
                //logForm.Show();

                MarketSimulator.LoadQuoteData();
                Simulation.InitializeSimulation();

                RefreshScreen();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void nextTurnButton_Click(object sender, EventArgs e)
        {
            try
            {
                int turnSize = 1;
                try
                {
                    turnSize = Convert.ToInt32(turnSizeTextBox.Text);
                }
                catch
                {
                    turnSize = 1;
                }
                for (int i = 0; i < turnSize; i++)
                {
                    Simulation.broker.FinishDailyProcessing();
                    Simulation.NextIteration();
                }
                RefreshScreen();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }


        private void executeOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                string orderTypeValue = orderTypeComboBox.SelectedItem.ToString();
                string orderActionValue = orderActionComboBox.SelectedItem.ToString();

                OrderAction orderAction = (OrderAction)Enum.Parse(typeof(OrderAction), orderActionValue);
                OrderType orderType = (OrderType)Enum.Parse(typeof(OrderType), orderTypeValue);

                Order order = new Order();
                if (quantityTextBox.Text.Trim() != "")
                {
                    order.quantity = Convert.ToDouble(quantityTextBox.Text.Trim());
                }
                order.symbol = symbolTextBox.Text.Trim().ToUpper();
                order.orderAction = orderAction;
                order.orderType = orderType;
                if (priceTextBox.Text.Trim() != "")
                {
                    order.limitPrice = Convert.ToDouble(priceTextBox.Text.Trim());
                    if (order.orderType == OrderType.StopMarket)
                    {
                        order.activationPrice = order.limitPrice;
                        order.limitPrice = 0;
                    }
                    else if(order.orderType == OrderType.TrailingStopDollar || order.orderType == OrderType.TrailingStopPercent)
                    {
                        order.trailingStopAmount = order.limitPrice;
                        order.limitPrice = 0;
                    }
                }
                
                Simulation.broker.GiveBrokerNewOrder(order);
                RefreshScreen();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void NewRandomButton_Click(object sender, EventArgs e)
        {
            try
            {
                Simulation.Randomize();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void RefreshScreen()
        {
            try
            {
                DisplayDate();
                DisplayAccountBalance();
                DisplaySettings();
                DisplayPositions();
                DisplayOpenOrders();
                DisplaySymbols();
                DisplayQuoteBoard();
                
                orderActionComboBox.SelectedIndex = 0;
                orderTypeComboBox.SelectedIndex = 1;
                symbolTextBox.Text = "";
                quantityTextBox.Text = "";
                priceTextBox.Text = "";
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public void DisplayDate()
        {
            simulationDayLabel.Text = "Day " + Simulation.simulationDay.ToString();
            monthLabel.Text = Simulation.simulationDate.ToString("MMMM");
            yearLabel.Text = Simulation.simulationDate.ToString("yyyy");
            dayOfMonthLabel.Text = Simulation.simulationDate.ToString("dd");
            dayOfWeekLabel.Text = Simulation.simulationDate.DayOfWeek.ToString();
        }

        public void DisplaySettings()
        {
            numberOfDaysValue.Text = Simulation.numberOfSimulationDays.ToString();
            dateRangeValue.Text = Simulation.simulationStartDate.ToShortDateString() + " - " + Simulation.simulationEndDate.ToShortDateString();
        }

        public void DisplaySymbols()
        {
            symbolsListBox.Items.Clear();
            foreach (string symbol in Simulation.symbolList)
            {
                QuoteData qd = QuoteDataManager.GetQuoteData(symbol);
                if (qd.startDate <= Simulation.simulationStartDate && qd.endDate >= Simulation.simulationEndDate)
                {
                    symbolsListBox.Items.Add(symbol);
                }
            }
        }

        public void DisplayAccountBalance()
        {
            AccountBalance ab = Simulation.account.balance;
            bankBalanceAmount.Text = ab.bankBalance.ToString("C");
            accountBalanceAmount.Text = ab.accountValue.ToString("C");
            totalBalanceAmount.Text = ab.totalValue.ToString("C");
            cashBalanceAmount.Text = ab.cashBalance.ToString("C");
            longStockValue.Text = ab.longStockValue.ToString("C");
            shortStockValue.Text = ab.shortStockValue.ToString("C");
            buyingPowerValue.Text = ab.buyingPower.ToString("C");
        }

        public void DisplayPositions()
        {
            positionListView.Items.Clear();
            foreach (Position p in Simulation.account.positionList)
            {
                try
                {
                    if (p.quantity <= 0)
                    {
                        continue;
                    }
                    ListViewItem row = new ListViewItem(p.side.ToString());
                    row.SubItems.Add(p.symbol);
                    row.SubItems.Add(p.quantity.ToString());
                    row.SubItems.Add(p.value.ToString("C"));
                    row.SubItems.Add(p.profit.ToString("C"));
                    row.SubItems.Add(p.percentProfit.ToString("0.0%"));
                    row.SubItems.Add(p.purchasePrice.ToString("C"));
                    row.SubItems.Add(p.open.ToString("C"));
                    row.SubItems.Add(p.high.ToString("C"));
                    row.SubItems.Add(p.low.ToString("C"));
                    row.SubItems.Add(p.close.ToString("C"));
                    row.SubItems.Add(p.dayChange.ToString("C"));
                    row.SubItems.Add(p.percentDayChange.ToString("0.0%"));
                    row.SubItems.Add(p.openedDate.ToString("MM/dd/yyyy"));
                    positionListView.Items.Add(row);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public void DisplayOpenOrders()
        {
            openOrdersListView.Items.Clear();
            foreach (Order o in Simulation.account.orderList)
            {
                try
                {
                    ListViewItem row = new ListViewItem(o.orderAction.ToString());
                    row.SubItems.Add(o.orderType.ToString());
                    row.SubItems.Add(o.symbol);
                    row.SubItems.Add(o.quantity.ToString());
                    row.SubItems.Add(o.limitPrice.ToString("C"));
                    row.SubItems.Add(o.activationPrice.ToString("C"));
                    if (o.orderType == OrderType.TrailingStopPercent)
                    {
                        row.SubItems.Add(o.trailingStopAmount.ToString("0.0%"));
                    }
                    else
                    {
                        row.SubItems.Add(o.trailingStopAmount.ToString("C"));
                    }
                    row.SubItems.Add(o.openedDate.ToString("MM/dd/yyyy"));
                    row.SubItems.Add(o.openedDay.ToString());
                    openOrdersListView.Items.Add(row);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        public void DisplayQuoteBoard()
        {
            marketListView.Items.Clear();
            foreach (String symbol in Simulation.symbolList)
            {
                try
                {
                    QuoteData qd = QuoteDataManager.GetQuoteData(symbol);
                    if (qd.startDate > Simulation.simulationStartDate || qd.endDate < Simulation.simulationEndDate)
                    {
                        continue;
                    }

                    Quote q = qd.GetQuote(Simulation.simulationDate);

                    ListViewItem row = new ListViewItem(symbol);
                    row.SubItems.Add(q.open.ToString("C"));
                    row.SubItems.Add(q.high.ToString("C"));
                    row.SubItems.Add(q.low.ToString("C"));
                    row.SubItems.Add(q.close.ToString("C"));
                    marketListView.Items.Add(row);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        private void marketListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (marketListView.SelectedItems.Count > 0)
            {
                string symbol = marketListView.SelectedItems[0].Text.ToString();
                symbolTextBox.Text = symbol;
            }
        }

        private void positionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (positionListView.SelectedItems.Count > 0)
            {
                string symbol = positionListView.SelectedItems[0].SubItems[1].Text.ToString();
                symbolTextBox.Text = symbol;
            }
        }

        private void runTradeSystem_Click(object sender, EventArgs e)
        {
            Simulation.RunOneIterationOfTradeSystemThroughSimulation();
            RefreshScreen();
           // logForm.DumpBufferIntoTextbox();

        }

        private void batchRunButton_Click(object sender, EventArgs e)
        {
            int batchSize = Convert.ToInt32(batchSizeTextBox.Text.Trim());
            Simulation.RunTradeSystemThroughMultipleRandomSimulations(batchSize);
            RefreshScreen();
            averageTotalBalance.Text = Simulation.averageEndingAccountValue.ToString("C");
            
        }

        private void positionListView_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
