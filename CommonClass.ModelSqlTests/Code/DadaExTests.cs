using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections;

namespace CommonClass.ModelSql.Tests
{
    [TestClass()]
    public class DadaExTests
    {
        [TestMethod()]
        public void RunSqlTest() {
            var db = new TestDb();
            db.CreateDb();
            try {
                var pin = new PIn { p1 = 1,};
                var i = db.RunSql(pin);
                var r = db.RunSql<PIn,POut>(pin).ToList();
                Console.WriteLine(r.GetType().FullName);
                Console.WriteLine(i);
                Console.WriteLine(r.First().pr);
                Assert.AreEqual(2,r.First().pr);
                #region 执行效率测试
                var sw = new Stopwatch();
                sw.Start();
                for(var x = 0;x < 100;x++) {
                    r = db.RunSql<PIn,POut>(pin).ToList();
                }
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds.ToString());
                long timeSpan = 1 * 60 * 1000;
                Assert.IsTrue(sw.ElapsedMilliseconds < timeSpan,$"执行时间大于{timeSpan.ToString()}");
                #endregion

                #region 通过传入参数数组进行测试
                var pas = new Hashtable();
                pas.Add("p1",1);
                r = db.RunSql<POut>("PTest",pas).ToList();
                Assert.AreEqual(2,r.First().pr);
                #endregion
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                Assert.Fail("执行报错");
            }
            finally {
                db.RemoveDb();
                db.Dispose();
            }
        }
    }

    public class TestDb:DbContext
    {
        public TestDb() {
            Database.SetInitializer<TestDb>(null);
        }

        public void CreateDb() {
            if(!this.Database.CreateIfNotExists()) {
                Console.WriteLine("创建数据库失败");
                return;
            }

            this.Database.ExecuteSqlCommand(
                @"
-- =============================================
-- Author:		Falcon
-- Create date: 2017年6月15日
-- Description:	测试用存储过程
-- =============================================
CREATE PROCEDURE PTest 
	@p1 int = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT @p1+1 pr
END
");
            Console.WriteLine("创建数据库完成");
        }

        public void RemoveDb() {
            if(this.Database.Exists()) {
                this.Database.Connection.Close();
                this.Database.Connection.Dispose();
                Database.Delete(nameof(TestDb));
                Console.WriteLine("删除数据库完成");
            }
        }
    }

    [ProcuderName("PTest")]
    public class PIn
    {
        public int p1 { get; set; }
    }
    public class POut
    {
        public int pr { get; set; }
    }
}