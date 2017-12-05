using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonClass.SqlExecuteString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.SqlExecuteStringTests
{
    [TestClass()]
    public class DefaultExecuteStringTests
    {
        [TestMethod()]
        public void GetStringTest() {
            IExecuteString es = new DefaultExecuteString();
            var str = es.GetString(new PName {
                id = 1,Name = "name",p1 = "p1",paaa = "p2",
            });
            string sql = "exec testP @id=1,@Name='name',@p2='p2'";
            Assert.AreEqual(sql,str);
        }
    }
}