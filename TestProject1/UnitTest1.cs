using Moq;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using WinForms.Controller;
using WinForms.Data;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IReversiDataAccess> _mock = null!;
        private WinForms_Logic _logic = null!;
        private WinForm_Data _myData = null!;
        
        [TestInitialize]
        public void Initialize()
        {
            
            _myData = new WinForm_Data();
            _myData.BlackSecs = 1;


            _mock = new Mock<IReversiDataAccess>();
            _mock.Setup(x => x.LoadState(It.IsAny<string>())).Returns(() => _myData);
            _logic = new WinForms_Logic(new WinForm_Data(), _mock.Object);
        }

        [TestMethod]
        public void LoadGame()
        {
            _logic.LoadState(String.Empty);

            Assert.AreEqual(_myData.GetTableSize(), 10);
        }
        
        [TestMethod]
        public void TestMethod2()
        {
            _logic.MyData.SetTableSize(4);
            for (int i = 0; i < _logic.MyData.GetTableSize(); i++)
            {
                for (int i2 = 0; i2 < _logic.MyData.GetTableSize(); i2++)
                {
                    _logic.setTableInitData(i, i2);
                }
            }

            string isValid = _logic.isValidString(0, 0, _logic.MyData.GetNext());

            Assert.AreEqual(isValid, "notvalid");
        }
        
        [TestMethod]
        public void TestMethod3()
        {
            _logic.MyData.SetTableSize(4);
            for (int i = 0; i < _logic.MyData.GetTableSize(); i++)
            {
                for (int i2 = 0; i2 < _logic.MyData.GetTableSize(); i2++)
                {
                    _logic.setTableInitData(i, i2);
                }
            }
            List<int> recolorables = _logic.MakeMove(0, 0);


            Assert.IsTrue(recolorables.Count == 0);
        }

        [TestMethod]
        public void TestMethod4()
        {
            _logic.MyData.SetTableSize(4);
            for (int i = 0; i < _logic.MyData.GetTableSize(); i++)
            {
                for (int i2 = 0; i2 < _logic.MyData.GetTableSize(); i2++)
                {
                    _logic.setTableInitData(i, i2);
                }
            }
            _logic.InvertNext();


            Assert.IsTrue(_logic.MyData.GetNext()== WinForms.Data.Next.White);
        }
        [TestMethod]
        public void TestMethod5()
        {
            _logic.MyData.SetTableSize(4);
            for (int i = 0; i < _logic.MyData.GetTableSize(); i++)
            {
                for (int i2 = 0; i2 < _logic.MyData.GetTableSize(); i2++)
                {
                    _logic.MyData.SetTableData(WinForms.Data.ButtonType.Black, i, i2);
                }
            }
            bool jo = _logic.IsTableFull();


            Assert.IsTrue(jo);
        }
        [TestMethod]
        public void TestMethod6()
        {
            _logic.MyData.SetTableSize(4);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 3);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 1, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Black, 1, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.White, 1, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 1, 3);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 2, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.White, 2, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Black, 2, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 2, 3);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 3);

            List<int> r = _logic.MakeMove(1, 3);
            List<int> n = new List<int> { 2, 1, };


            Assert.AreEqual(n.Count, r.Count);
        }
        [TestMethod]
        public void TestMethod7()
        {
            _logic.MyData.SetTableSize(4);

            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 0, 3);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 1, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Black, 1, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.White, 1, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 1, 3);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 2, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.White, 2, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Black, 2, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 2, 3);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 0);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 1);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 2);
            _logic.MyData.SetTableData(WinForms.Data.ButtonType.Empty, 3, 3);

            List<int> r = _logic.MakeMove(1, 3);
            List<int> n = new List<int> { 1, 2, };
            bool jo2 = n.SequenceEqual(r);


            Assert.IsTrue(jo2);
        }
        
    }
}