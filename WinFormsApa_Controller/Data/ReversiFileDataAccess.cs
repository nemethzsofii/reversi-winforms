using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.Data
{
    public class ReversiFileDataAccess : IReversiDataAccess
    {
        public bool SaveState(WinForm_Data data, string path)
        {
            bool success = false;
            string filePath = path;
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(data.GetTableSize());
                    writer.WriteLine(data.GetNext().ToString());
                    writer.WriteLine(data.WhiteSecs);
                    writer.WriteLine(data.BlackSecs);
                    for (int i = 0; i < data.GetTableSize(); i++)
                    {
                        for (int j = 0; j < data.GetTableSize(); j++)
                        {
                            writer.Write(WinForm_Data.ButtonTypeToInt(data.GetTableData(i, j).GetButtonType()));
                            if (j < data.GetTableSize() - 1)
                            {
                                writer.Write(' ');
                            }

                        }
                        writer.Write('\n');
                    }
                }

                Console.WriteLine("File write successful.");
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ReversiDataException();
            }
            return success;
        }
        public WinForm_Data LoadState(string path)
        {
            //bool success = false;
            try
            {
                WinForm_Data data = new WinForm_Data();
                string filePath = path;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line = "";
                    line = reader.ReadLine();
                    Debug.WriteLine(line);
                    int a = 0;
                    bool jo = int.TryParse(line, out a);
                    data.SetTableSize(a);
                    line = reader.ReadLine();
                    Debug.WriteLine(line);
                    if (line == "Black")
                    {
                        data.SetNext(Next.Black);
                    }
                    else
                    {
                        data.SetNext(Next.White);
                    }
                    line = reader.ReadLine();
                    Debug.WriteLine(line);
                    jo = int.TryParse(line, out a);
                    data.WhiteSecs = a;
                    line = reader.ReadLine();
                    Debug.WriteLine(line);
                    jo = int.TryParse(line, out a);
                    data.BlackSecs = a;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Debug.WriteLine(i + ". sor: " + line);
                        line = line.TrimEnd('\r', '\n');
                        string[] splitted_line = line.Split(' ');
                        for (int j = 0; j < splitted_line.Length; j++)
                        {
                            data.SetTableData(WinForm_Data.IntToButtonType(int.Parse(splitted_line[j])), i, j);
                        }
                        i++;
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.ToString());
                throw new ReversiDataException();
            }
        }
    }
}
