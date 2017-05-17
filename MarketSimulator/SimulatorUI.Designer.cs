namespace MarketSimulator
{
    partial class SimulatorUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numberOfDays = new System.Windows.Forms.Label();
            this.numberOfDaysValue = new System.Windows.Forms.Label();
            this.dateRangeValue = new System.Windows.Forms.Label();
            this.NewRandomButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.turnSizeLabel = new System.Windows.Forms.Label();
            this.turnSizeTextBox = new System.Windows.Forms.TextBox();
            this.nextTurnButton = new System.Windows.Forms.Button();
            this.executeOrderButton = new System.Windows.Forms.Button();
            this.priceLabel = new System.Windows.Forms.Label();
            this.runTradeSystem = new System.Windows.Forms.Button();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.symbolLabel = new System.Windows.Forms.Label();
            this.symbolTextBox = new System.Windows.Forms.TextBox();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.orderTypeLabel = new System.Windows.Forms.Label();
            this.actionLabel = new System.Windows.Forms.Label();
            this.orderTypeComboBox = new System.Windows.Forms.ComboBox();
            this.orderActionComboBox = new System.Windows.Forms.ComboBox();
            this.buyingPowerValue = new System.Windows.Forms.Label();
            this.shortStockValue = new System.Windows.Forms.Label();
            this.longStockValue = new System.Windows.Forms.Label();
            this.buyingPowerLabel = new System.Windows.Forms.Label();
            this.shortStockValueLabel = new System.Windows.Forms.Label();
            this.longStockValueLabel = new System.Windows.Forms.Label();
            this.totalBalanceAmount = new System.Windows.Forms.Label();
            this.bankBalanceAmount = new System.Windows.Forms.Label();
            this.accountBalanceAmount = new System.Windows.Forms.Label();
            this.cashBalanceAmount = new System.Windows.Forms.Label();
            this.totalBalanceLabel = new System.Windows.Forms.Label();
            this.bankBalanceLabel = new System.Windows.Forms.Label();
            this.accountBalanceLabel = new System.Windows.Forms.Label();
            this.cashBalanceLabel = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.monthLabel = new System.Windows.Forms.Label();
            this.dayOfMonthLabel = new System.Windows.Forms.Label();
            this.dayOfWeekLabel = new System.Windows.Forms.Label();
            this.simulationDayLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.positionListView = new System.Windows.Forms.ListView();
            this.side = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueDollar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gainDollar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gainPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.purchasePrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayOpen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayHigh = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayLow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayClose = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayDollarChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayPercentChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openOrdersListView = new System.Windows.Forms.ListView();
            this.oOrderAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oOrderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oLimitPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oActivationPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oTrailingStopAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oOpenDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.oOpenDay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.marketListView = new System.Windows.Forms.ListView();
            this.qSymbol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.qOpen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.qHigh = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.qLow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.qClose = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.averageTotalBalance = new System.Windows.Forms.Label();
            this.batchSizeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.batchRunButton = new System.Windows.Forms.Button();
            this.symbolsListBox = new System.Windows.Forms.ListBox();
            this.empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.averageTotalBalance);
            this.splitContainer1.Panel2.Controls.Add(this.batchSizeTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.batchRunButton);
            this.splitContainer1.Panel2.Controls.Add(this.symbolsListBox);
            this.splitContainer1.Size = new System.Drawing.Size(1279, 730);
            this.splitContainer1.SplitterDistance = 878;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.executeOrderButton);
            this.splitContainer2.Panel1.Controls.Add(this.priceLabel);
            this.splitContainer2.Panel1.Controls.Add(this.runTradeSystem);
            this.splitContainer2.Panel1.Controls.Add(this.priceTextBox);
            this.splitContainer2.Panel1.Controls.Add(this.symbolLabel);
            this.splitContainer2.Panel1.Controls.Add(this.symbolTextBox);
            this.splitContainer2.Panel1.Controls.Add(this.quantityLabel);
            this.splitContainer2.Panel1.Controls.Add(this.quantityTextBox);
            this.splitContainer2.Panel1.Controls.Add(this.orderTypeLabel);
            this.splitContainer2.Panel1.Controls.Add(this.actionLabel);
            this.splitContainer2.Panel1.Controls.Add(this.orderTypeComboBox);
            this.splitContainer2.Panel1.Controls.Add(this.orderActionComboBox);
            this.splitContainer2.Panel1.Controls.Add(this.buyingPowerValue);
            this.splitContainer2.Panel1.Controls.Add(this.shortStockValue);
            this.splitContainer2.Panel1.Controls.Add(this.longStockValue);
            this.splitContainer2.Panel1.Controls.Add(this.buyingPowerLabel);
            this.splitContainer2.Panel1.Controls.Add(this.shortStockValueLabel);
            this.splitContainer2.Panel1.Controls.Add(this.longStockValueLabel);
            this.splitContainer2.Panel1.Controls.Add(this.totalBalanceAmount);
            this.splitContainer2.Panel1.Controls.Add(this.bankBalanceAmount);
            this.splitContainer2.Panel1.Controls.Add(this.accountBalanceAmount);
            this.splitContainer2.Panel1.Controls.Add(this.cashBalanceAmount);
            this.splitContainer2.Panel1.Controls.Add(this.totalBalanceLabel);
            this.splitContainer2.Panel1.Controls.Add(this.bankBalanceLabel);
            this.splitContainer2.Panel1.Controls.Add(this.accountBalanceLabel);
            this.splitContainer2.Panel1.Controls.Add(this.cashBalanceLabel);
            this.splitContainer2.Panel1.Controls.Add(this.yearLabel);
            this.splitContainer2.Panel1.Controls.Add(this.monthLabel);
            this.splitContainer2.Panel1.Controls.Add(this.dayOfMonthLabel);
            this.splitContainer2.Panel1.Controls.Add(this.dayOfWeekLabel);
            this.splitContainer2.Panel1.Controls.Add(this.simulationDayLabel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(874, 726);
            this.splitContainer2.SplitterDistance = 224;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numberOfDays);
            this.groupBox2.Controls.Add(this.numberOfDaysValue);
            this.groupBox2.Controls.Add(this.dateRangeValue);
            this.groupBox2.Controls.Add(this.NewRandomButton);
            this.groupBox2.Location = new System.Drawing.Point(661, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 111);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Date Range";
            // 
            // numberOfDays
            // 
            this.numberOfDays.AutoSize = true;
            this.numberOfDays.Location = new System.Drawing.Point(7, 21);
            this.numberOfDays.Name = "numberOfDays";
            this.numberOfDays.Size = new System.Drawing.Size(89, 13);
            this.numberOfDays.TabIndex = 35;
            this.numberOfDays.Text = "Number of Days: ";
            // 
            // numberOfDaysValue
            // 
            this.numberOfDaysValue.AutoSize = true;
            this.numberOfDaysValue.Location = new System.Drawing.Point(120, 21);
            this.numberOfDaysValue.Name = "numberOfDaysValue";
            this.numberOfDaysValue.Size = new System.Drawing.Size(13, 13);
            this.numberOfDaysValue.TabIndex = 36;
            this.numberOfDaysValue.Text = "0";
            // 
            // dateRangeValue
            // 
            this.dateRangeValue.AutoSize = true;
            this.dateRangeValue.Location = new System.Drawing.Point(7, 50);
            this.dateRangeValue.Name = "dateRangeValue";
            this.dateRangeValue.Size = new System.Drawing.Size(58, 13);
            this.dateRangeValue.TabIndex = 37;
            this.dateRangeValue.Text = "date range";
            // 
            // NewRandomButton
            // 
            this.NewRandomButton.Location = new System.Drawing.Point(10, 76);
            this.NewRandomButton.Name = "NewRandomButton";
            this.NewRandomButton.Size = new System.Drawing.Size(91, 23);
            this.NewRandomButton.TabIndex = 33;
            this.NewRandomButton.Text = "New Range";
            this.NewRandomButton.UseVisualStyleBackColor = true;
            this.NewRandomButton.Click += new System.EventHandler(this.NewRandomButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.turnSizeLabel);
            this.groupBox1.Controls.Add(this.turnSizeTextBox);
            this.groupBox1.Controls.Add(this.nextTurnButton);
            this.groupBox1.Location = new System.Drawing.Point(443, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 111);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manual Simulation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "day(s)";
            // 
            // turnSizeLabel
            // 
            this.turnSizeLabel.AutoSize = true;
            this.turnSizeLabel.Location = new System.Drawing.Point(21, 24);
            this.turnSizeLabel.Name = "turnSizeLabel";
            this.turnSizeLabel.Size = new System.Drawing.Size(55, 13);
            this.turnSizeLabel.TabIndex = 1;
            this.turnSizeLabel.Text = "Turn Size:";
            // 
            // turnSizeTextBox
            // 
            this.turnSizeTextBox.Location = new System.Drawing.Point(82, 21);
            this.turnSizeTextBox.Name = "turnSizeTextBox";
            this.turnSizeTextBox.Size = new System.Drawing.Size(29, 20);
            this.turnSizeTextBox.TabIndex = 2;
            this.turnSizeTextBox.Text = "1";
            // 
            // nextTurnButton
            // 
            this.nextTurnButton.Location = new System.Drawing.Point(24, 55);
            this.nextTurnButton.Name = "nextTurnButton";
            this.nextTurnButton.Size = new System.Drawing.Size(107, 23);
            this.nextTurnButton.TabIndex = 0;
            this.nextTurnButton.Text = "Advance One Day";
            this.nextTurnButton.UseVisualStyleBackColor = true;
            this.nextTurnButton.Click += new System.EventHandler(this.nextTurnButton_Click);
            // 
            // executeOrderButton
            // 
            this.executeOrderButton.Location = new System.Drawing.Point(551, 152);
            this.executeOrderButton.Name = "executeOrderButton";
            this.executeOrderButton.Size = new System.Drawing.Size(92, 23);
            this.executeOrderButton.TabIndex = 32;
            this.executeOrderButton.Text = "Execute Order";
            this.executeOrderButton.UseVisualStyleBackColor = true;
            this.executeOrderButton.Click += new System.EventHandler(this.executeOrderButton_Click);
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(474, 140);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(31, 13);
            this.priceLabel.TabIndex = 31;
            this.priceLabel.Text = "Price";
            // 
            // runTradeSystem
            // 
            this.runTradeSystem.Location = new System.Drawing.Point(666, 152);
            this.runTradeSystem.Name = "runTradeSystem";
            this.runTradeSystem.Size = new System.Drawing.Size(96, 69);
            this.runTradeSystem.TabIndex = 38;
            this.runTradeSystem.Text = "Automatically Trade One Day";
            this.runTradeSystem.UseVisualStyleBackColor = true;
            this.runTradeSystem.Click += new System.EventHandler(this.runTradeSystem_Click);
            // 
            // priceTextBox
            // 
            this.priceTextBox.Location = new System.Drawing.Point(477, 156);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(56, 20);
            this.priceTextBox.TabIndex = 30;
            // 
            // symbolLabel
            // 
            this.symbolLabel.AutoSize = true;
            this.symbolLabel.Location = new System.Drawing.Point(245, 139);
            this.symbolLabel.Name = "symbolLabel";
            this.symbolLabel.Size = new System.Drawing.Size(41, 13);
            this.symbolLabel.TabIndex = 29;
            this.symbolLabel.Text = "Symbol";
            // 
            // symbolTextBox
            // 
            this.symbolTextBox.Location = new System.Drawing.Point(248, 155);
            this.symbolTextBox.Name = "symbolTextBox";
            this.symbolTextBox.Size = new System.Drawing.Size(56, 20);
            this.symbolTextBox.TabIndex = 28;
            // 
            // quantityLabel
            // 
            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Location = new System.Drawing.Point(171, 139);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(46, 13);
            this.quantityLabel.TabIndex = 27;
            this.quantityLabel.Text = "Quantity";
            // 
            // quantityTextBox
            // 
            this.quantityTextBox.Location = new System.Drawing.Point(174, 155);
            this.quantityTextBox.Name = "quantityTextBox";
            this.quantityTextBox.Size = new System.Drawing.Size(56, 20);
            this.quantityTextBox.TabIndex = 26;
            // 
            // orderTypeLabel
            // 
            this.orderTypeLabel.AutoSize = true;
            this.orderTypeLabel.Location = new System.Drawing.Point(327, 139);
            this.orderTypeLabel.Name = "orderTypeLabel";
            this.orderTypeLabel.Size = new System.Drawing.Size(60, 13);
            this.orderTypeLabel.TabIndex = 25;
            this.orderTypeLabel.Text = "Order Type";
            // 
            // actionLabel
            // 
            this.actionLabel.AutoSize = true;
            this.actionLabel.Location = new System.Drawing.Point(21, 139);
            this.actionLabel.Name = "actionLabel";
            this.actionLabel.Size = new System.Drawing.Size(37, 13);
            this.actionLabel.TabIndex = 24;
            this.actionLabel.Text = "Action";
            // 
            // orderTypeComboBox
            // 
            this.orderTypeComboBox.FormattingEnabled = true;
            this.orderTypeComboBox.Items.AddRange(new object[] {
            "Limit",
            "Market",
            "StopMarket",
            "TrailingStopPercent",
            "TrailingStopDollar"});
            this.orderTypeComboBox.Location = new System.Drawing.Point(330, 155);
            this.orderTypeComboBox.Name = "orderTypeComboBox";
            this.orderTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.orderTypeComboBox.TabIndex = 23;
            // 
            // orderActionComboBox
            // 
            this.orderActionComboBox.FormattingEnabled = true;
            this.orderActionComboBox.Items.AddRange(new object[] {
            "Buy",
            "BuyToCover",
            "Sell",
            "SellShort"});
            this.orderActionComboBox.Location = new System.Drawing.Point(24, 155);
            this.orderActionComboBox.Name = "orderActionComboBox";
            this.orderActionComboBox.Size = new System.Drawing.Size(121, 21);
            this.orderActionComboBox.TabIndex = 22;
            // 
            // buyingPowerValue
            // 
            this.buyingPowerValue.AutoSize = true;
            this.buyingPowerValue.Location = new System.Drawing.Point(372, 81);
            this.buyingPowerValue.Name = "buyingPowerValue";
            this.buyingPowerValue.Size = new System.Drawing.Size(34, 13);
            this.buyingPowerValue.TabIndex = 21;
            this.buyingPowerValue.Text = "$0.00";
            // 
            // shortStockValue
            // 
            this.shortStockValue.AutoSize = true;
            this.shortStockValue.Location = new System.Drawing.Point(372, 60);
            this.shortStockValue.Name = "shortStockValue";
            this.shortStockValue.Size = new System.Drawing.Size(34, 13);
            this.shortStockValue.TabIndex = 20;
            this.shortStockValue.Text = "$0.00";
            // 
            // longStockValue
            // 
            this.longStockValue.AutoSize = true;
            this.longStockValue.Location = new System.Drawing.Point(372, 39);
            this.longStockValue.Name = "longStockValue";
            this.longStockValue.Size = new System.Drawing.Size(34, 13);
            this.longStockValue.TabIndex = 19;
            this.longStockValue.Text = "$0.00";
            // 
            // buyingPowerLabel
            // 
            this.buyingPowerLabel.AutoSize = true;
            this.buyingPowerLabel.Location = new System.Drawing.Point(245, 81);
            this.buyingPowerLabel.Name = "buyingPowerLabel";
            this.buyingPowerLabel.Size = new System.Drawing.Size(75, 13);
            this.buyingPowerLabel.TabIndex = 18;
            this.buyingPowerLabel.Text = "Buying Power:";
            // 
            // shortStockValueLabel
            // 
            this.shortStockValueLabel.AutoSize = true;
            this.shortStockValueLabel.Location = new System.Drawing.Point(245, 60);
            this.shortStockValueLabel.Name = "shortStockValueLabel";
            this.shortStockValueLabel.Size = new System.Drawing.Size(96, 13);
            this.shortStockValueLabel.TabIndex = 17;
            this.shortStockValueLabel.Text = "Short Stock Value:";
            // 
            // longStockValueLabel
            // 
            this.longStockValueLabel.AutoSize = true;
            this.longStockValueLabel.Location = new System.Drawing.Point(245, 39);
            this.longStockValueLabel.Name = "longStockValueLabel";
            this.longStockValueLabel.Size = new System.Drawing.Size(95, 13);
            this.longStockValueLabel.TabIndex = 16;
            this.longStockValueLabel.Text = "Long Stock Value:";
            // 
            // totalBalanceAmount
            // 
            this.totalBalanceAmount.AutoSize = true;
            this.totalBalanceAmount.Location = new System.Drawing.Point(119, 102);
            this.totalBalanceAmount.Name = "totalBalanceAmount";
            this.totalBalanceAmount.Size = new System.Drawing.Size(34, 13);
            this.totalBalanceAmount.TabIndex = 15;
            this.totalBalanceAmount.Text = "$0.00";
            // 
            // bankBalanceAmount
            // 
            this.bankBalanceAmount.AutoSize = true;
            this.bankBalanceAmount.Location = new System.Drawing.Point(119, 81);
            this.bankBalanceAmount.Name = "bankBalanceAmount";
            this.bankBalanceAmount.Size = new System.Drawing.Size(34, 13);
            this.bankBalanceAmount.TabIndex = 14;
            this.bankBalanceAmount.Text = "$0.00";
            // 
            // accountBalanceAmount
            // 
            this.accountBalanceAmount.AutoSize = true;
            this.accountBalanceAmount.Location = new System.Drawing.Point(119, 60);
            this.accountBalanceAmount.Name = "accountBalanceAmount";
            this.accountBalanceAmount.Size = new System.Drawing.Size(34, 13);
            this.accountBalanceAmount.TabIndex = 13;
            this.accountBalanceAmount.Text = "$0.00";
            // 
            // cashBalanceAmount
            // 
            this.cashBalanceAmount.AutoSize = true;
            this.cashBalanceAmount.Location = new System.Drawing.Point(119, 39);
            this.cashBalanceAmount.Name = "cashBalanceAmount";
            this.cashBalanceAmount.Size = new System.Drawing.Size(34, 13);
            this.cashBalanceAmount.TabIndex = 12;
            this.cashBalanceAmount.Text = "$0.00";
            // 
            // totalBalanceLabel
            // 
            this.totalBalanceLabel.AutoSize = true;
            this.totalBalanceLabel.Location = new System.Drawing.Point(11, 101);
            this.totalBalanceLabel.Name = "totalBalanceLabel";
            this.totalBalanceLabel.Size = new System.Drawing.Size(76, 13);
            this.totalBalanceLabel.TabIndex = 11;
            this.totalBalanceLabel.Text = "Total Balance:";
            // 
            // bankBalanceLabel
            // 
            this.bankBalanceLabel.AutoSize = true;
            this.bankBalanceLabel.Location = new System.Drawing.Point(11, 80);
            this.bankBalanceLabel.Name = "bankBalanceLabel";
            this.bankBalanceLabel.Size = new System.Drawing.Size(77, 13);
            this.bankBalanceLabel.TabIndex = 10;
            this.bankBalanceLabel.Text = "Bank Balance:";
            // 
            // accountBalanceLabel
            // 
            this.accountBalanceLabel.AutoSize = true;
            this.accountBalanceLabel.Location = new System.Drawing.Point(11, 59);
            this.accountBalanceLabel.Name = "accountBalanceLabel";
            this.accountBalanceLabel.Size = new System.Drawing.Size(92, 13);
            this.accountBalanceLabel.TabIndex = 9;
            this.accountBalanceLabel.Text = "Account Balance:";
            // 
            // cashBalanceLabel
            // 
            this.cashBalanceLabel.AutoSize = true;
            this.cashBalanceLabel.Location = new System.Drawing.Point(11, 38);
            this.cashBalanceLabel.Name = "cashBalanceLabel";
            this.cashBalanceLabel.Size = new System.Drawing.Size(76, 13);
            this.cashBalanceLabel.TabIndex = 8;
            this.cashBalanceLabel.Text = "Cash Balance:";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearLabel.Location = new System.Drawing.Point(326, 7);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(49, 20);
            this.yearLabel.TabIndex = 7;
            this.yearLabel.Text = "2010";
            // 
            // monthLabel
            // 
            this.monthLabel.AutoSize = true;
            this.monthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthLabel.Location = new System.Drawing.Point(187, 7);
            this.monthLabel.Name = "monthLabel";
            this.monthLabel.Size = new System.Drawing.Size(89, 20);
            this.monthLabel.TabIndex = 6;
            this.monthLabel.Text = "November";
            // 
            // dayOfMonthLabel
            // 
            this.dayOfMonthLabel.AutoSize = true;
            this.dayOfMonthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayOfMonthLabel.Location = new System.Drawing.Point(291, 7);
            this.dayOfMonthLabel.Name = "dayOfMonthLabel";
            this.dayOfMonthLabel.Size = new System.Drawing.Size(29, 20);
            this.dayOfMonthLabel.TabIndex = 5;
            this.dayOfMonthLabel.Text = "15";
            // 
            // dayOfWeekLabel
            // 
            this.dayOfWeekLabel.AutoSize = true;
            this.dayOfWeekLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayOfWeekLabel.Location = new System.Drawing.Point(82, 7);
            this.dayOfWeekLabel.Name = "dayOfWeekLabel";
            this.dayOfWeekLabel.Size = new System.Drawing.Size(71, 20);
            this.dayOfWeekLabel.TabIndex = 4;
            this.dayOfWeekLabel.Text = "Monday";
            // 
            // simulationDayLabel
            // 
            this.simulationDayLabel.AutoSize = true;
            this.simulationDayLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.simulationDayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulationDayLabel.Location = new System.Drawing.Point(10, 7);
            this.simulationDayLabel.Name = "simulationDayLabel";
            this.simulationDayLabel.Size = new System.Drawing.Size(55, 20);
            this.simulationDayLabel.TabIndex = 3;
            this.simulationDayLabel.Text = "Day 0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(874, 498);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.positionListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(866, 472);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Positions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // positionListView
            // 
            this.positionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.side,
            this.symbol,
            this.quantity,
            this.valueDollar,
            this.gainDollar,
            this.gainPercent,
            this.purchasePrice,
            this.dayOpen,
            this.dayHigh,
            this.dayLow,
            this.dayClose,
            this.dayDollarChange,
            this.dayPercentChange,
            this.openedDate,
            this.empty});
            this.positionListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.positionListView.FullRowSelect = true;
            this.positionListView.GridLines = true;
            this.positionListView.Location = new System.Drawing.Point(3, 3);
            this.positionListView.Name = "positionListView";
            this.positionListView.Size = new System.Drawing.Size(860, 466);
            this.positionListView.TabIndex = 0;
            this.positionListView.UseCompatibleStateImageBehavior = false;
            this.positionListView.View = System.Windows.Forms.View.Details;
            this.positionListView.SelectedIndexChanged += new System.EventHandler(this.positionListView_SelectedIndexChanged_1);
            // 
            // side
            // 
            this.side.Text = "Side";
            this.side.Width = 47;
            // 
            // symbol
            // 
            this.symbol.Text = "Symbol";
            this.symbol.Width = 58;
            // 
            // quantity
            // 
            this.quantity.Text = "Quantity";
            this.quantity.Width = 54;
            // 
            // valueDollar
            // 
            this.valueDollar.Text = "Value";
            this.valueDollar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.valueDollar.Width = 109;
            // 
            // gainDollar
            // 
            this.gainDollar.Text = "Gain($)";
            this.gainDollar.Width = 57;
            // 
            // gainPercent
            // 
            this.gainPercent.Text = "Gain(%)";
            this.gainPercent.Width = 57;
            // 
            // purchasePrice
            // 
            this.purchasePrice.Text = "Purchase Price";
            this.purchasePrice.Width = 89;
            // 
            // dayOpen
            // 
            this.dayOpen.Text = "Open";
            this.dayOpen.Width = 50;
            // 
            // dayHigh
            // 
            this.dayHigh.Text = "High";
            this.dayHigh.Width = 50;
            // 
            // dayLow
            // 
            this.dayLow.Text = "Low";
            this.dayLow.Width = 50;
            // 
            // dayClose
            // 
            this.dayClose.Text = "Close";
            this.dayClose.Width = 50;
            // 
            // dayDollarChange
            // 
            this.dayDollarChange.Text = "Day ($)";
            // 
            // dayPercentChange
            // 
            this.dayPercentChange.Text = "Day (%)";
            this.dayPercentChange.Width = 52;
            // 
            // openedDate
            // 
            this.openedDate.Text = "Opened";
            this.openedDate.Width = 80;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.openOrdersListView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(877, 472);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Open Orders";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openOrdersListView
            // 
            this.openOrdersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.oOrderAction,
            this.oOrderType,
            this.oSymbol,
            this.oQuantity,
            this.oLimitPrice,
            this.oActivationPrice,
            this.oTrailingStopAmount,
            this.oOpenDate,
            this.oOpenDay});
            this.openOrdersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openOrdersListView.GridLines = true;
            this.openOrdersListView.Location = new System.Drawing.Point(3, 3);
            this.openOrdersListView.Name = "openOrdersListView";
            this.openOrdersListView.Size = new System.Drawing.Size(871, 466);
            this.openOrdersListView.TabIndex = 0;
            this.openOrdersListView.UseCompatibleStateImageBehavior = false;
            this.openOrdersListView.View = System.Windows.Forms.View.Details;
            // 
            // oOrderAction
            // 
            this.oOrderAction.Text = "Order Action";
            this.oOrderAction.Width = 100;
            // 
            // oOrderType
            // 
            this.oOrderType.Text = "Order Type";
            this.oOrderType.Width = 100;
            // 
            // oSymbol
            // 
            this.oSymbol.Text = "Symbol";
            // 
            // oQuantity
            // 
            this.oQuantity.Text = "Quantity";
            // 
            // oLimitPrice
            // 
            this.oLimitPrice.Text = "Limit Price";
            this.oLimitPrice.Width = 65;
            // 
            // oActivationPrice
            // 
            this.oActivationPrice.Text = "Activation Price";
            this.oActivationPrice.Width = 90;
            // 
            // oTrailingStopAmount
            // 
            this.oTrailingStopAmount.Text = "Trailing Stop";
            this.oTrailingStopAmount.Width = 75;
            // 
            // oOpenDate
            // 
            this.oOpenDate.Text = "Open Date";
            this.oOpenDate.Width = 90;
            // 
            // oOpenDay
            // 
            this.oOpenDay.Text = "Open Day";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.marketListView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(877, 472);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Market";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // marketListView
            // 
            this.marketListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.qSymbol,
            this.qOpen,
            this.qHigh,
            this.qLow,
            this.qClose});
            this.marketListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marketListView.FullRowSelect = true;
            this.marketListView.GridLines = true;
            this.marketListView.Location = new System.Drawing.Point(0, 0);
            this.marketListView.Name = "marketListView";
            this.marketListView.Size = new System.Drawing.Size(877, 472);
            this.marketListView.TabIndex = 0;
            this.marketListView.UseCompatibleStateImageBehavior = false;
            this.marketListView.View = System.Windows.Forms.View.Details;
            // 
            // qSymbol
            // 
            this.qSymbol.Text = "Symbol";
            // 
            // qOpen
            // 
            this.qOpen.Text = "Open";
            // 
            // qHigh
            // 
            this.qHigh.Text = "High";
            // 
            // qLow
            // 
            this.qLow.Text = "Low";
            // 
            // qClose
            // 
            this.qClose.Text = "Close";
            // 
            // averageTotalBalance
            // 
            this.averageTotalBalance.AutoSize = true;
            this.averageTotalBalance.Location = new System.Drawing.Point(72, 101);
            this.averageTotalBalance.Name = "averageTotalBalance";
            this.averageTotalBalance.Size = new System.Drawing.Size(109, 13);
            this.averageTotalBalance.TabIndex = 42;
            this.averageTotalBalance.Text = "averageTotalBalance";
            // 
            // batchSizeTextBox
            // 
            this.batchSizeTextBox.Location = new System.Drawing.Point(139, 72);
            this.batchSizeTextBox.Name = "batchSizeTextBox";
            this.batchSizeTextBox.Size = new System.Drawing.Size(42, 20);
            this.batchSizeTextBox.TabIndex = 41;
            this.batchSizeTextBox.Text = "1000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Batch Size:";
            // 
            // batchRunButton
            // 
            this.batchRunButton.Location = new System.Drawing.Point(72, 14);
            this.batchRunButton.Name = "batchRunButton";
            this.batchRunButton.Size = new System.Drawing.Size(89, 45);
            this.batchRunButton.TabIndex = 39;
            this.batchRunButton.Text = "Batch Run The Entire Range";
            this.batchRunButton.UseVisualStyleBackColor = true;
            this.batchRunButton.Click += new System.EventHandler(this.batchRunButton_Click);
            // 
            // symbolsListBox
            // 
            this.symbolsListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.symbolsListBox.FormattingEnabled = true;
            this.symbolsListBox.Location = new System.Drawing.Point(0, 0);
            this.symbolsListBox.Name = "symbolsListBox";
            this.symbolsListBox.Size = new System.Drawing.Size(66, 726);
            this.symbolsListBox.TabIndex = 3;
            // 
            // empty
            // 
            this.empty.Text = "";
            // 
            // SimulatorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 730);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SimulatorUI";
            this.Text = "SimulatorUI";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button nextTurnButton;
        private System.Windows.Forms.Label turnSizeLabel;
        private System.Windows.Forms.Label simulationDayLabel;
        private System.Windows.Forms.TextBox turnSizeTextBox;
        private System.Windows.Forms.Label dayOfWeekLabel;
        private System.Windows.Forms.Label monthLabel;
        private System.Windows.Forms.Label dayOfMonthLabel;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label totalBalanceLabel;
        private System.Windows.Forms.Label bankBalanceLabel;
        private System.Windows.Forms.Label accountBalanceLabel;
        private System.Windows.Forms.Label cashBalanceLabel;
        private System.Windows.Forms.Label buyingPowerLabel;
        private System.Windows.Forms.Label shortStockValueLabel;
        private System.Windows.Forms.Label longStockValueLabel;
        private System.Windows.Forms.Label totalBalanceAmount;
        private System.Windows.Forms.Label bankBalanceAmount;
        private System.Windows.Forms.Label accountBalanceAmount;
        private System.Windows.Forms.Label cashBalanceAmount;
        private System.Windows.Forms.Label buyingPowerValue;
        private System.Windows.Forms.Label shortStockValue;
        private System.Windows.Forms.Label longStockValue;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.Label orderTypeLabel;
        private System.Windows.Forms.Label actionLabel;
        private System.Windows.Forms.ComboBox orderTypeComboBox;
        private System.Windows.Forms.ComboBox orderActionComboBox;
        private System.Windows.Forms.Label symbolLabel;
        private System.Windows.Forms.TextBox symbolTextBox;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.Button executeOrderButton;
        private System.Windows.Forms.ListBox symbolsListBox;
        private System.Windows.Forms.Button NewRandomButton;
        private System.Windows.Forms.Label dateRangeValue;
        private System.Windows.Forms.Label numberOfDaysValue;
        private System.Windows.Forms.Label numberOfDays;
        private System.Windows.Forms.Button runTradeSystem;
        private System.Windows.Forms.Button batchRunButton;
        private System.Windows.Forms.Label averageTotalBalance;
        private System.Windows.Forms.TextBox batchSizeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView positionListView;
        private System.Windows.Forms.ColumnHeader side;
        private System.Windows.Forms.ColumnHeader symbol;
        private System.Windows.Forms.ColumnHeader quantity;
        private System.Windows.Forms.ColumnHeader valueDollar;
        private System.Windows.Forms.ColumnHeader gainDollar;
        private System.Windows.Forms.ColumnHeader gainPercent;
        private System.Windows.Forms.ColumnHeader purchasePrice;
        private System.Windows.Forms.ColumnHeader dayOpen;
        private System.Windows.Forms.ColumnHeader dayHigh;
        private System.Windows.Forms.ColumnHeader dayLow;
        private System.Windows.Forms.ColumnHeader dayClose;
        private System.Windows.Forms.ColumnHeader dayDollarChange;
        private System.Windows.Forms.ColumnHeader dayPercentChange;
        private System.Windows.Forms.ColumnHeader openedDate;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView openOrdersListView;
        private System.Windows.Forms.ColumnHeader oOrderAction;
        private System.Windows.Forms.ColumnHeader oOrderType;
        private System.Windows.Forms.ColumnHeader oSymbol;
        private System.Windows.Forms.ColumnHeader oQuantity;
        private System.Windows.Forms.ColumnHeader oLimitPrice;
        private System.Windows.Forms.ColumnHeader oActivationPrice;
        private System.Windows.Forms.ColumnHeader oTrailingStopAmount;
        private System.Windows.Forms.ColumnHeader oOpenDate;
        private System.Windows.Forms.ColumnHeader oOpenDay;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView marketListView;
        private System.Windows.Forms.ColumnHeader qSymbol;
        private System.Windows.Forms.ColumnHeader qOpen;
        private System.Windows.Forms.ColumnHeader qHigh;
        private System.Windows.Forms.ColumnHeader qLow;
        private System.Windows.Forms.ColumnHeader qClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader empty;
    }
}