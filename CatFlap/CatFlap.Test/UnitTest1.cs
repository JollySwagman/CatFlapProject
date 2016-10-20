using CatFlap.Models;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CatFlap.Test
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var passage = new Passage(Passage.DirectionType.IN);
            CatFlapData.Save(passage);
            //CatFlap.Models.Flap.test();
        }

        [Test]
        public void GetAll()
        {
            var passage = new Passage(Passage.DirectionType.IN);
            var x = CatFlapData.GetAll();
            //CatFlap.Models.Flap.test();

            foreach (var item in x)
            {
                Trace.WriteLine(item.id);
                Trace.WriteLine(item.Direction);
            }
        }
    }
}
