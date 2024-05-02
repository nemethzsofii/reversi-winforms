using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WinForms.Data;

namespace WinForms.Controller
{
    public class WinForms_Logic
    {
        //Fields
        private WinForm_Data _myData;
        private IReversiDataAccess _dataAccess;

        public WinForm_Data MyData { get { return _myData; } }

        public WinForms_Logic(WinForm_Data data, IReversiDataAccess dataAccess)
        {
            _myData = data;
            _myData.WhiteSecs = 0;
            _myData.BlackSecs = 0;
            _dataAccess = dataAccess;
        }


        public void InitTableData()
        {
            for (int yPos = 0; yPos < _myData.GetTableSize(); yPos++)
            {
                for (int xPos = 0; xPos < _myData.GetTableSize(); xPos++)
                {
                    int halfsize = (_myData.GetTableSize() / 2);
                    if ((xPos == (halfsize - 1) && yPos == (halfsize - 1)) || (xPos == halfsize && yPos == halfsize))
                    {
                        _myData.SetTableData(ButtonType.Black, xPos, yPos);
                    }
                    else if ((xPos == halfsize && yPos == (halfsize - 1)) || (xPos == (halfsize - 1) && yPos == halfsize))
                    {
                        _myData.SetTableData(ButtonType.White, xPos, yPos);
                    }
                    else
                    {
                        _myData.SetTableData(ButtonType.Empty, xPos, yPos);
                    }
                }
            }

            _myData.SetNext(Next.Black);
            _myData.BlackSecs = 0;
            _myData.WhiteSecs = 0;

            for (int ypos = 0; ypos < _myData.GetTableSize(); ypos++)
            {
                for (int xPos = 0; xPos < _myData.GetTableSize(); xPos++)
                {
                    if (isValidString(xPos, ypos, _myData.GetNext()) == "valid")
                    {
                        _myData.SetTableData(ButtonType.Candidate, xPos, ypos);
                    }
                }
            }
        }

        public void setTableInitData(int xPos, int yPos)
        {
            var _buttonType = ButtonType.Empty;
            if (xPos == (_myData.GetTableSize() / 2 - 1))
            {
                if (yPos == (_myData.GetTableSize() / 2 - 1))
                {
                    _buttonType = ButtonType.White;
                }
                else if (yPos == (_myData.GetTableSize() / 2))
                {
                    _buttonType = ButtonType.Black;
                }
            }
            else if (xPos == (_myData.GetTableSize() / 2))
            {
                if (yPos == (_myData.GetTableSize() / 2 - 1))
                {
                    _buttonType = ButtonType.Black;
                }
                else if (yPos == (_myData.GetTableSize() / 2))
                {
                    _buttonType = ButtonType.White;
                }
            }

            ButtonData button = new ButtonData();
            button.SetButtonType(_buttonType);
            button.SetXPos(xPos);
            button.SetYPos(yPos);
            //_myData.data[xPos, yPos] = button;
            _myData.SetOneData(xPos, yPos, button);
            
        }
        public void InvertNext()
        {
            if (_myData.GetNext() == Next.White)
            {
                _myData.SetNext(Next.Black);
            }
            else
            {
                _myData.SetNext(Next.White);
            }
        }

        public static bool IsMoveValid(ButtonData[,] board, int row, int col, Next next)
        {
            if (board[row, col].GetButtonType() != ButtonType.Empty && board[row, col].GetButtonType() != ButtonType.Candidate)
            {
                return false;
            }

            Next opponent = (next == Next.Black) ? Next.White : Next.Black;

            bool isValidMove = false;

            int[] dr = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] dc = { -1, 0, 1, 1, 1, 0, -1, -1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int r = row + dr[dir];
                int c = col + dc[dir];

                if (r >= 0 && r <  board.GetLength(0) && c >= 0 && c < board.GetLength(1) && board[r, c].GetButtonType().ToString() == opponent.ToString())
                {
                    r += dr[dir];
                    c += dc[dir];

                    while (r >= 0 && r < board.GetLength(0) && c >= 0 && c < board.GetLength(1) && board[r, c].GetButtonType().ToString() == opponent.ToString())
                    {
                        r += dr[dir];
                        c += dc[dir];
                    }

                    if (r >= 0 && r < board.GetLength(0) && c >= 0 && c < board.GetLength(1) && board[r, c].GetButtonType().ToString() == next.ToString())
                    {
                        isValidMove = true;
                        break;
                    }
                }
            }

            return isValidMove;
        }
        public string isValidString(int x, int y, Next next)
        {
            if (IsMoveValid(_myData.GetData(), x, y, next))
            {
                return "valid";
            }
            else
            {
                return "notvalid";
            }
        }
        /*
        public void ColorPotentialTiles()
        {
            foreach(var button in _myData.data)
            {

            }
        }
        */
        public bool IsTableFull()
        {
            ButtonData[,] board = _myData.GetData();
            foreach(var d in board)
            {
                if (!(d.GetButtonType() == ButtonType.White || d.GetButtonType() == ButtonType.Black))
                {
                    return false;
                }
            }
            return true;
        }

        public event EventHandler? GameOver;
        public List<int> MakeMove(int row, int col)
        {
            List<int> recolorable = new List<int>();
            List<int> tmp = new List<int>();
            ButtonData[,] board = _myData.GetData();
            Next next = _myData.GetNext();

            if (board[row, col].GetButtonType() != ButtonType.Empty && board[row, col].GetButtonType() != ButtonType.Candidate)
            {
                return recolorable;
            }

            Next opponent = (next == Next.Black) ? Next.White : Next.Black;

            int[] dr = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] dc = { -1, 0, 1, 1, 1, 0, -1, -1 };

            for (int dir = 0; dir < 8; dir++)
            {
                tmp.Clear();
                int r = row + dr[dir];
                int c = col + dc[dir];

                if (r >= 0 && r < board.GetLength(0) && c >= 0 && c < board.GetLength(1) && board[r, c].GetButtonType().ToString() == opponent.ToString())
                {
                    tmp.Add(r);
                    tmp.Add(c);
                    r += dr[dir];
                    c += dc[dir];

                    while (r >= 0 && r < board.GetLength(0) && c >= 0 && c < board.GetLength(1) && board[r, c].GetButtonType().ToString() == opponent.ToString())
                    {
                        tmp.Add(r);
                        tmp.Add(c);
                        r += dr[dir];
                        c += dc[dir];
                    }

                    if (r >= 0 && r < board.GetLength(0) && c >= 0 && c < board.GetLength(1) && board[r, c].GetButtonType().ToString() == next.ToString())
                    {
                        //isValidMove = true;
                        recolorable.AddRange(tmp);
                        //tmp.Clear();
                    }
                    /*
                    else if(tmp.Count() > 2)
                    {
                        recolorable.AddRange(tmp);
                    }
                    */
                }
            }
            int temp = 1000;
            bool valid = false;
            int actXPos = row;
            int actYPos = col;
            foreach (var i in recolorable)
            {
                if (temp == 1000)
                {
                    temp = i;
                }
                else
                {
                    foreach (var b in _myData.GetData())
                    {
                        int colXPos = b.getXPos();
                        int colYPos = b.getYPos();
                        if (colXPos == temp && colYPos == i)
                        {
                            if (_myData.GetNext() == Next.Black)
                            {
                                _myData.SetTableData(ButtonType.Black, colXPos, colYPos);
                                valid = true;
                            }
                            else
                            {
                                _myData.SetTableData(ButtonType.White, colXPos, colYPos);
                                valid = true;
                            }
                        }
                    }
                    temp = 1000;
                }
                if (valid)
                {
                    if (_myData.GetNext() == Next.Black)
                    {
                        _myData.SetTableData(ButtonType.Black, actXPos, actYPos);
                        valid = true;
                        
                    }
                    else
                    {
                        _myData.SetTableData(ButtonType.White, actXPos, actYPos);
                        valid = true;

                    }
                }

            }
            if (IsTableFull())
            {
                GameOver?.Invoke(this, EventArgs.Empty);

            }

            return recolorable;
        }        
        public bool SaveState(string path)
        {
            return _dataAccess.SaveState(_myData, path);
        }

        public bool LoadState(string path)
        {
            try
            {
                _myData = _dataAccess.LoadState(path);
                return true;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
                throw new ReversiDataException();

            }
        }

    }
}
