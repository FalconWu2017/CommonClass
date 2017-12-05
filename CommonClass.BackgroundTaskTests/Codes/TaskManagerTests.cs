using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonClass.BackgroundTask.Tests
{
    [TestClass()]
    public class TaskManagerTests
    {
        [TestMethod()]
        public void RunTasksTest() {
            var tm = new TaskManager(new List<ITask>() { new TaskTest() },1000);
            tm.RunTasks();

            Thread.Sleep(10 * 1000);
            Assert.IsTrue(TaskTest.Count > 8);

            tm.StopTasks();
        }
    }

}