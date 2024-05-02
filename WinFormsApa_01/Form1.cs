using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Xml.Linq;
using WinForms.Controller;
using WinForms.Data;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private IReversiDataAccess _dataAccess;
        private WinForms_Logic _logic;
        private System.Windows.Forms.Timer timer;
        //int black_seconds = 0;
        //int white_seconds = 0;
        int passcounter = 0;
        public Form1()
        {
            InitializeComponent();

            _dataAccess = new ReversiFileDataAccess();
            _logic = new WinForms_Logic(new WinForm_Data(), _dataAccess);
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Set the interval in milliseconds (1 second)
            timer.Tick += new EventHandler(timer1_Tick); // Attach the event handler
            timer.Start(); // Start the timer
            _logic.GameOver += GameOver;
        }

        private void GameOver(object? sender, EventArgs e)
        {
            if (IsTie())
            {
                MessageBox.Show("Board is full! It's a tie:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (WhiteWon())
            {
                MessageBox.Show("Board is full! White won:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Board is full! Black won:)", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //_logic.setTableSize();
            //itt a kövi sorban a paraméterek nem jók. ugyanazok a paraméterek kellenének mint ami a form1_loadnak van. Hol van az meghívva????
            if (sender != null)
            {
                Form1_Load(sender, e);
            }
        }

        public bool IsTie()
        {
            int wcounter = 0;
            int bcounter = 0;
            foreach (var button in this.tableLayoutPanel1.Controls)
            {
                Button? b = button as Button;
                int actXPos = 0;
                int actYPos = 0;
                if (b != null)
                {
                    actXPos = GetXPos(b.Name);
                    actYPos = GetYPos(b.Name);
                }
                if (_logic.MyData.GetTableData(actXPos, actYPos).GetButtonType() == ButtonType.White)
                {
                    wcounter++;
                }
                else if (_logic.MyData.GetTableData(actXPos, actYPos).GetButtonType() == ButtonType.Black)
                {
                    bcounter++;
                }
            }
            return bcounter == wcounter;
        }

        public bool WhiteWon()
        {
            int wcounter = 0;
            int bcounter = 0;
            foreach (var button in this.tableLayoutPanel1.Controls)
            {
                Button? b = button as Button;
                int actXPos = 0;
                int actYPos = 0;
                if (b != null)
                {
                    actXPos = GetXPos(b.Name);
                    actYPos = GetYPos(b.Name);
                }
                if (_logic.MyData.GetTableData(actXPos, actYPos).GetButtonType() == ButtonType.White)
                {
                    wcounter++;
                }
                else if (_logic.MyData.GetTableData(actXPos, actYPos).GetButtonType() == ButtonType.Black)
                {
                    bcounter++;
                }
            }
            return wcounter > bcounter;
        }

        public bool AreTherePotentialTiles()
        {
            bool potential = false;
            foreach (var button in this.tableLayoutPanel1.Controls)
            {
                Button? b = button as Button;
                int actXPos = 0;
                int actYPos = 0;
                if (b != null)
                {
                    actXPos = GetXPos(b.Name);
                    actYPos = GetYPos(b.Name);
                }
                //itt +1-et én írtam be //töröltem
                if (_logic.isValidString(actXPos, actYPos, _logic.MyData.GetNext()) == "valid")
                {
                    return true;
                }
                //if(b.BackColor != Color.Black && b.BackColor != Color.White ) { }
            }
            return potential;
        }

        public void CheckPotential()
        {
            //reset candidates to empty
            foreach (var button in this.tableLayoutPanel1.Controls)
            {
                Button? b = button as Button;
                int actXPos;
                int actYPos;
                if (b != null)
                {
                    actXPos = GetXPos(b.Name);
                    actYPos = GetYPos(b.Name);
                    ButtonType actType = _logic.MyData.GetTableData(actXPos, actYPos).GetButtonType();
                    if (actType != ButtonType.White && actType != ButtonType.Black)
                    {
                        b.BackColor = Color.Gray;
                        _logic.MyData.SetTableData(ButtonType.Empty, actXPos, actYPos);
                    }
                }

            }
            //find the new candidates
            foreach (var button in this.tableLayoutPanel1.Controls)
            {
                Button? b = button as Button;
                int actXPos = 0;
                int actYPos = 0;
                if (b != null)
                {
                    actXPos = GetXPos(b.Name);
                    actYPos = GetYPos(b.Name);
                    if (_logic.isValidString(actXPos, actYPos, _logic.MyData.GetNext()) == "valid")
                    {
                        b.BackColor = Color.Pink;
                        _logic.MyData.SetTableData(ButtonType.Candidate, actXPos, actYPos);
                    }
                }

                //if(b.BackColor != Color.Black && b.BackColor != Color.White ) { }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var rowCount = _logic.MyData.GetTableSize();
            var columnCount = _logic.MyData.GetTableSize();

            if (rowCount == 10 || rowCount == 4)
            {
                this.Size = new Size(850, 850);
            }
            else if (rowCount == 20)
            {
                this.Size = new Size(1200, 1200);
            }
            else
            {
                this.Size = new Size(1500, 1500);
            }

            this.tableLayoutPanel1.ColumnCount = columnCount;
            this.tableLayoutPanel1.RowCount = rowCount;

            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Clear();


            for (int xPos = 0; xPos < columnCount; xPos++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / columnCount));
            }
            for (int yPos = 0; yPos < rowCount; yPos++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / rowCount));
            }

            for (int yPos = 0; yPos < rowCount; yPos++)
            {
                for (int xPos = 0; xPos < columnCount; xPos++)
                {

                    var button = new Button();
                    /*Button button = new Button()
                    {
                        xPos = j;
                        yPos = i;
                    };

                    button.Click += new EventHandler((s, e) => dynamic_button_click(s, e));*/
                    //button.Text = string.Format("{0}_{1}", yPos, xPos);
                    button.Name = string.Format("button_{0}_{1}", xPos, yPos);
                    button.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(button, xPos, yPos);
                    button.Click += new EventHandler((s, e) => dynamic_button_Click(s, e));
                    button.MouseHover += new EventHandler((s, e) => dynamic_MouseHover(s, e));

                    button.MouseLeave += new EventHandler((s, e) => dynamic_MouseLeave(s, e));
                    _logic.setTableInitData(xPos, yPos);
                    //button.Text = _logic.getTableData(xPos, yPos).buttonType.ToString();
                    button.BackColor = Color.Gray;
                    _logic.MyData.SetTableData(ButtonType.Empty, xPos, yPos);
                    //button.Text = string.Format("{0}_{1}_{2}", yPos, xPos, _logic.getTableData(xPos, yPos).buttonType.ToString());
                    int halfsize = (_logic.MyData.GetTableSize() / 2);
                    if ((xPos == (halfsize - 1) && yPos == (halfsize - 1)) || (xPos == halfsize && yPos == halfsize))
                    {
                        button.BackColor = Color.Black;
                        _logic.MyData.SetTableData(ButtonType.Black, xPos, yPos);
                    }
                    if ((xPos == halfsize && yPos == (halfsize - 1)) || (xPos == (halfsize - 1) && yPos == halfsize))
                    {
                        button.BackColor = Color.White;
                        _logic.MyData.SetTableData(ButtonType.White, xPos, yPos);
                    }
                }
            }
            //CheckPotential();
            _logic.MyData.SetNext(Next.Black);
            CheckPotential();
            nextToolStripMenuItem.Text = string.Format("Next: {0}", _logic.MyData.GetNext().ToString());
            _logic.MyData.BlackSecs = 0;
            blackTimeToolStripMenuItem.Text = "black time: 0 s";
            _logic.MyData.WhiteSecs = 0;
            whiteTimeToolStripMenuItem.Text = "white time: 0 s";
        }

        public void FormInvertNext()
        {
            _logic.InvertNext();
            nextToolStripMenuItem.Text = string.Format("Next: {0}", _logic.MyData.GetNext().ToString());
        }

        private void dynamic_button_Click(object? sender, EventArgs e)
        {
            var actButton = sender as Button;
            int actXPos = 0;
            int actYPos = 0;
            if (actButton != null)
            {
                actXPos = GetXPos(actButton.Name);
                actYPos = GetYPos(actButton.Name);
            }
            int temp = 1000;
            bool valid = false;


            if (_logic.MyData.GetTableData(actXPos, actYPos).GetButtonType() != ButtonType.Candidate)
            {

            }
            else
            {

                foreach (var button in this.tableLayoutPanel1.Controls)
                {

                    Button? b = button as Button;

                    if (b != null)
                    {
                        int colXPos = GetXPos(b.Name);
                        int colYPos = GetYPos(b.Name);
                        if (_logic.MyData.GetTableData(colXPos, colYPos).GetButtonType() == ButtonType.Candidate)
                        {
                            _logic.MyData.SetTableData(ButtonType.Empty, colXPos, colYPos);
                            b.BackColor = Color.Gray;
                        }
                    }
                }

                List<int> recolorable = _logic.MakeMove(actXPos, actYPos);
                foreach (var i in recolorable)
                {
                    if (temp == 1000)
                    {
                        temp = i;
                    }
                    else
                    {
                        foreach (var button in this.tableLayoutPanel1.Controls)
                        {

                            Button? b = button as Button;
                            int colXPos = 0;
                            int colYPos = 0;
                            if (b != null)
                            {
                                colXPos = GetXPos(b.Name);
                                colYPos = GetYPos(b.Name);
                                if (colXPos == temp && colYPos == i)
                                {
                                    if (_logic.MyData.GetNext() == Next.Black)
                                    {
                                        b.BackColor = Color.Black;
                                        _logic.MyData.SetTableData(ButtonType.Black, colXPos, colYPos);
                                        valid = true;
                                    }
                                    else
                                    {
                                        b.BackColor = Color.White;
                                        _logic.MyData.SetTableData(ButtonType.White, colXPos, colYPos);
                                        valid = true;
                                    }
                                }
                            }


                        }
                        temp = 1000;
                    }
                    if (valid)
                    {
                        if (_logic.MyData.GetNext() == Next.Black)
                        {
                            if (actButton != null)
                            {
                                actButton.BackColor = Color.Black;
                                _logic.MyData.SetTableData(ButtonType.Black, actXPos, actYPos);
                                valid = true;
                            }
                        }
                        else
                        {
                            if (actButton != null)
                            {
                                actButton.BackColor = Color.White;
                                _logic.MyData.SetTableData(ButtonType.White, actXPos, actYPos);
                                valid = true;
                            }

                        }
                        /*
                        FormInvertNext();
                        CheckPotential();
                        */
                    }

                }
                /*
                if (_logic.IsTableFull())
                {
                    
                    if (IsTie())
                    {
                        MessageBox.Show("Board is full! It's a tie:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (WhiteWon())
                    {
                        MessageBox.Show("Board is full! White won:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Board is full! Black won:)", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //_logic.setTableSize();
                    //itt a kövi sorban a paraméterek nem jók. ugyanazok a paraméterek kellenének mint ami a form1_loadnak van. Hol van az meghívva????
                    if(sender != null)
                    {
                        Form1_Load(sender, e);
                    }
                    
                    
                }*/
                FormInvertNext();
                if (!AreTherePotentialTiles())
                {
                    passcounter++;
                    MessageBox.Show(_logic.MyData.GetNext().ToString() + " passed!", "Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_logic.InvertNext();

                }
                else
                {
                    passcounter = 0;
                }
                if (passcounter == 1)
                {
                    FormInvertNext();
                    if (!(AreTherePotentialTiles()))
                    {
                        //MessageBox.Show("You both passed!.", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (IsTie())
                        {
                            MessageBox.Show("You both passed! It's a tie:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (WhiteWon())
                        {
                            MessageBox.Show("You both passed! White won:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("You both passed! Black won:)", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //itt a kövi sorban a paraméterek nem jók. ugyanazok a paraméterek kellenének mint ami a form1_loadnak van. Hol van az meghívva????
                        if (sender != null)
                        {
                            Form1_Load(sender, e);
                        }

                    }
                    else
                    {
                        passcounter = 0;
                        CheckPotential();
                    }
                }
                else
                {
                    //FormInvertNext();
                    CheckPotential();
                }
                /*
                if (passcounter == 2)
                {
                    //MessageBox.Show("You both passed!.", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (IsTie())
                    {
                        MessageBox.Show("You both passed! It's a tie:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (WhiteWon())
                    {
                        MessageBox.Show("You both passed! White won:).", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("You both passed! Black won:)", "End of game!:)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //itt a kövi sorban a paraméterek nem jók. ugyanazok a paraméterek kellenének mint ami a form1_loadnak van. Hol van az meghívva????
                    Form1_Load(sender, e);

                }*/
            }
            //actButton.Text = _logic.getTableSize().ToString();
            //actButton.Text = GetXPos(actButton.Name).ToString() + "_" + GetYPos(actButton.Name).ToString();
            //actButton.Text = _logic.getTableData(GetXPos(actButton.Name), GetYPos(actButton.Name)).buttonType.ToString();
            //actButton.Text = _logic.isValidString(actXPos, actYPos, _logic.getNext());

            //idáig


        }
        private int GetXPos(string name)
        {
            int start = 7;
            string s = name.Substring(start, name.Length - start);
            int end = s.IndexOf('_');
            string final = name.Substring(start, end);
            return int.Parse(final);
        }
        private int GetYPos(string name)
        {
            int start = 7;
            string s = name.Substring(start, name.Length - start);
            int end = s.IndexOf('_');
            string final = name.Substring(start + end + 1);
            return int.Parse(final);
        }
        private void table_size_click_10(object sender, EventArgs e)
        {
            _logic.MyData.SetTableSize(10);
            Form1_Load(sender, e);
        }

        private void table_size_click_08(object sender, EventArgs e)
        {
            _logic.MyData.SetTableSize(4);
            Form1_Load(sender, e);
        }

        private void table_size_click_06(object sender, EventArgs e)
        {
            _logic.MyData.SetTableSize(30);
            Form1_Load(sender, e);
        }

        private void newGimeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dynamic_MouseHover(object? sender, EventArgs e)
        {
            int actXPos = 0;
            int actYPos = 0;
            Button? actButton = sender as Button;
            if (actButton != null)
            {
                actXPos = GetXPos(actButton.Name);
                actYPos = GetYPos(actButton.Name);
                if (_logic.MyData.GetTableData(actXPos, actYPos).GetButtonType() == ButtonType.Candidate)
                {
                    actButton.BackColor = Color.DeepPink;
                }
            }

            //actButton.Text = _logic.isValidString(actXPos, actYPos, _logic.getNext());



        }

        private void dynamic_MouseLeave(object? sender, EventArgs e)
        {
            var actButton = sender as Button;
            if (actButton != null)
            {
                //actButton.Text = "";
                int actXPos = GetXPos(actButton.Name);
                int actYPos = GetYPos(actButton.Name);

                if (_logic.MyData.GetTableData(actXPos, actYPos).GetButtonType() == ButtonType.Candidate)
                {
                    actButton.BackColor = Color.Pink;
                }
            }

        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            if (_logic.MyData.GetNext() == Next.White)
            {
                //_logic.MyData.SetWhiteSecs(_logic.GetWhiteSecs()+1);
                _logic.MyData.WhiteSecs = _logic.MyData.WhiteSecs + 1;
                whiteTimeToolStripMenuItem.Text = "white time: " + _logic.MyData.WhiteSecs + " s";
            }
            else
            {
                //_logic.SetBlackSecs(_logic.GetBlackSecs()+1);
                _logic.MyData.BlackSecs = _logic.MyData.BlackSecs + 1;
                blackTimeToolStripMenuItem.Text = "black time: " + _logic.MyData.BlackSecs + " s";
            }

        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            //MessageBox.Show("Pause", "Game Paused!", MessageBoxButtons.OK);
            DialogResult result = MessageBox.Show("Press OK to resume!", "Game Paused", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Check the result of the message box
            if (result == DialogResult.OK)
            {
                timer.Start();
            }


        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool success = _logic.SaveState("reversi_saved.txt");
            if (success)
            {
                MessageBox.Show("Game saved!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Something went wrong. Game not saved!", "Oops", MessageBoxButtons.OK);
            }

        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool success = _logic.LoadState("reversi_saved.txt");
            if (success)
            {
                MessageBox.Show("Game successfully loaded!", "Yay", MessageBoxButtons.OK);
                Form1_Load(sender, e);
                _logic.LoadState("reversi_saved.txt");
                blackTimeToolStripMenuItem.Text = "black time: " + _logic.MyData.BlackSecs + "s";
                whiteTimeToolStripMenuItem.Text = "white time: " + _logic.MyData.WhiteSecs + "s";
                foreach (var button in this.tableLayoutPanel1.Controls)
                {
                    int actXPos = 0;
                    int actYPos = 0;
                    Button? b = button as Button;
                    if (b != null)
                    {
                        actXPos = GetXPos(b.Name);
                        actYPos = GetYPos(b.Name);
                        ButtonType actType = _logic.MyData.GetTableData(actXPos, actYPos).GetButtonType();
                        if (actType == ButtonType.Empty)
                        {
                            b.BackColor = Color.Gray;
                        }
                        else if (actType == ButtonType.Candidate)
                        {
                            b.BackColor = Color.Pink;
                        }
                        else if (actType == ButtonType.White)
                        {
                            b.BackColor = Color.White;
                        }
                        else
                        {
                            b.BackColor = Color.Black;
                        }
                    }


                }
            }
            else
            {
                MessageBox.Show("Something went wrong. Game not loaded!", "Oops", MessageBoxButtons.OK);
            }

        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            timer.Stop();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _logic.SaveState(saveFileDialog1.FileName);
                    MessageBox.Show("Game saved!", "Success", MessageBoxButtons.OK);
                }
                catch (ReversiDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Sikertelen mentés!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    timer.Start();
                }
            }
        }

        private void loadToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    _logic.LoadState(openFileDialog1.FileName);
                    Form1_Load(sender, e);
                    _logic.LoadState(openFileDialog1.FileName);
                    blackTimeToolStripMenuItem.Text = "black time: " + _logic.MyData.BlackSecs + "s";
                    whiteTimeToolStripMenuItem.Text = "white time: " + _logic.MyData.WhiteSecs + "s";
                    foreach (var button in this.tableLayoutPanel1.Controls)
                    {
                        int actXPos = 0;
                        int actYPos = 0;
                        Button? b = button as Button;
                        if (b != null)
                        {
                            actXPos = GetXPos(b.Name);
                            actYPos = GetYPos(b.Name);
                            ButtonType actType = _logic.MyData.GetTableData(actXPos, actYPos).GetButtonType();
                            if (actType == ButtonType.Empty)
                            {
                                b.BackColor = Color.Gray;
                            }
                            else if (actType == ButtonType.Candidate)
                            {
                                b.BackColor = Color.Pink;
                            }
                            else if (actType == ButtonType.White)
                            {
                                b.BackColor = Color.White;
                            }
                            else
                            {
                                b.BackColor = Color.Black;
                            }
                        }

                    }
                    MessageBox.Show("Game successfully loaded!", "Yay", MessageBoxButtons.OK);
                }
                catch (ReversiDataException)
                {
                    MessageBox.Show("Something went wrong. Game not loaded!", "Oops", MessageBoxButtons.OK);
                }
            }
        }
    }
}