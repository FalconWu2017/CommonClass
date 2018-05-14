using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonClass.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass.DataStructure.Tests
{
    [TestClass()]
    public class TreeTests
    {
        [TestMethod()]
        public void FromListTest() {
            var list = new List<Data> {
                new Data{Id=1,FatherId=0 },
                new Data{Id=2,FatherId=1 },
                new Data{Id=3,FatherId=1 },
                new Data{Id=4,FatherId=2 },
            };
            var tree = new Tree<Data>();
            var root = tree.FromList(list,new IsChild<Data>((f,c) => f.Id == c.FatherId),c => c.FatherId == 0);
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Children);
            var rc = root.Children;
            Assert.IsTrue(rc.All(m => m.Data.FatherId == 0));
            Assert.IsTrue(rc.Count == 1);
            Assert.IsTrue(rc.First().Data.Id == 1);
            var i1 = rc.First();
            Assert.IsTrue(rc.Where(m => m.Data.Id == 1).All(m => m.Children.Count == 2));
            Assert.IsTrue(rc.Where(m => m.Data.Id == 2).All(m => m.Children.Count == 1));
            Assert.IsTrue(rc.Where(m => m.Data.Id == 3).All(m => m.Children.Count == 0));
            Assert.IsTrue(rc.Where(m => m.Data.Id == 4).All(m => m.Children.Count == 0));

            var l = root.ToList();
            Assert.IsNotNull(l);
            Assert.IsTrue(l.Count() == 4);
            Assert.IsTrue(l.Any(m => m.Id == 1));
            Assert.IsTrue(l.Any(m => m.Id == 2));
            Assert.IsTrue(l.Any(m => m.Id == 3));
            Assert.IsTrue(l.Any(m => m.Id == 4));
        }
    }

    class Data
    {
        public int Id { get; set; }
        public int FatherId { get; set; }
    }
}