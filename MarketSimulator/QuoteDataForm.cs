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
    public partial class QuoteDataForm : Form
    {
        public QuoteDataForm()
        {
            InitializeComponent();
        }

        private void QuoteDataForm_Load(object sender, EventArgs e)
        {
            //MarketSimulator.LoadQuoteData();
            PopulateSymbolListView();

        }

        private void PopulateSymbolListView()
        {
           // symbolListView
            QuoteData qd;
            foreach(string symbol in QuoteDataManager.symbolList)
            {
                try
                {
                    qd = QuoteDataManager.quoteDataHash[symbol];

                    ListViewItem row = new ListViewItem(symbol);
                    row.SubItems.Add(qd.startDate.ToShortDateString());

                    row.SubItems.Add(qd.endDate.ToShortDateString());
                    row.SubItems.Add(qd.quoteList.Count.ToString());
                    row.SubItems.Add(qd.splitList.Count.ToString());
                    row.SubItems.Add(qd.missingDays.ToString());
                    row.SubItems.Add(qd.extraDays.ToString());
                    row.SubItems.Add(qd.maxConsecutiveMissingDays.ToString());

                    symbolListView.Items.Add(row);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

        private void symbolListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //symbolListView.selectedin
        }

    }
}
