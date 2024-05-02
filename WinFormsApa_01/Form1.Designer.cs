namespace WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            statusStrip1 = new StatusStrip();
            nextToolStripMenuItem = new ToolStripStatusLabel();
            blackTimeToolStripMenuItem = new ToolStripStatusLabel();
            whiteTimeToolStripMenuItem = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            newGimeToolStripMenuItem = new ToolStripMenuItem();
            tableSize10ToolStripMenuItem = new ToolStripMenuItem();
            tablToolStripMenuItem = new ToolStripMenuItem();
            tableSize30ToolStripMenuItem = new ToolStripMenuItem();
            actionToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            pauseToolStripMenuItem1 = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1 = new OpenFileDialog();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90.125F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.875F));
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 46);
            tableLayoutPanel1.Margin = new Padding(6, 4, 6, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1300, 633);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { nextToolStripMenuItem, blackTimeToolStripMenuItem, whiteTimeToolStripMenuItem });
            statusStrip1.Location = new Point(0, 679);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(2, 0, 22, 0);
            statusStrip1.Size = new Size(1300, 42);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // nextToolStripMenuItem
            // 
            nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            nextToolStripMenuItem.Size = new Size(237, 32);
            nextToolStripMenuItem.Text = "toolStripStatusLabel1";
            // 
            // blackTimeToolStripMenuItem
            // 
            blackTimeToolStripMenuItem.Name = "blackTimeToolStripMenuItem";
            blackTimeToolStripMenuItem.Size = new Size(237, 32);
            blackTimeToolStripMenuItem.Text = "toolStripStatusLabel2";
            // 
            // whiteTimeToolStripMenuItem
            // 
            whiteTimeToolStripMenuItem.Name = "whiteTimeToolStripMenuItem";
            whiteTimeToolStripMenuItem.Size = new Size(237, 32);
            whiteTimeToolStripMenuItem.Text = "toolStripStatusLabel3";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { newGimeToolStripMenuItem, actionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(9, 4, 0, 4);
            menuStrip1.Size = new Size(1300, 46);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // newGimeToolStripMenuItem
            // 
            newGimeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tableSize10ToolStripMenuItem, tablToolStripMenuItem, tableSize30ToolStripMenuItem });
            newGimeToolStripMenuItem.Name = "newGimeToolStripMenuItem";
            newGimeToolStripMenuItem.Size = new Size(149, 38);
            newGimeToolStripMenuItem.Text = "New game";
            // 
            // tableSize10ToolStripMenuItem
            // 
            tableSize10ToolStripMenuItem.Name = "tableSize10ToolStripMenuItem";
            tableSize10ToolStripMenuItem.Size = new Size(359, 44);
            tableSize10ToolStripMenuItem.Text = "Table size 10";
            tableSize10ToolStripMenuItem.Click += table_size_click_10;
            // 
            // tablToolStripMenuItem
            // 
            tablToolStripMenuItem.Name = "tablToolStripMenuItem";
            tablToolStripMenuItem.Size = new Size(359, 44);
            tablToolStripMenuItem.Text = "Table size 4";
            tablToolStripMenuItem.Click += table_size_click_08;
            // 
            // tableSize30ToolStripMenuItem
            // 
            tableSize30ToolStripMenuItem.Name = "tableSize30ToolStripMenuItem";
            tableSize30ToolStripMenuItem.Size = new Size(359, 44);
            tableSize30ToolStripMenuItem.Text = "Table size 30";
            tableSize30ToolStripMenuItem.Click += table_size_click_06;
            // 
            // actionToolStripMenuItem
            // 
            actionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, pauseToolStripMenuItem1 });
            actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            actionToolStripMenuItem.Size = new Size(102, 38);
            actionToolStripMenuItem.Text = "Action";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(208, 44);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click_1;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(208, 44);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click_1;
            // 
            // pauseToolStripMenuItem1
            // 
            pauseToolStripMenuItem1.Name = "pauseToolStripMenuItem1";
            pauseToolStripMenuItem1.Size = new Size(208, 44);
            pauseToolStripMenuItem1.Text = "Pause";
            pauseToolStripMenuItem1.Click += pauseToolStripMenuItem_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "Reversi játék mentés  (*.rvs)|*.rvs";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Reversi játék mentés  (*.rvs)|*.rvs";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1300, 721);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(6, 4, 6, 4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem newGimeToolStripMenuItem;
        private ToolStripMenuItem tableSize10ToolStripMenuItem;
        private ToolStripMenuItem tablToolStripMenuItem;
        private ToolStripMenuItem tableSize30ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private ToolStripMenuItem actionToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem pauseToolStripMenuItem1;
        private StatusStrip statusStrip1;
        //private Label label1;
        private ToolStripStatusLabel nextToolStripMenuItem;
        private ToolStripStatusLabel blackTimeToolStripMenuItem;
        private ToolStripStatusLabel whiteTimeToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
    }
}