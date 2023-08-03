using NUnit.Framework;
using RPG.Util;
using Unity.PerformanceTesting;

namespace UnitTests
{
    public class UtilTests
    {
        [Test, Performance]
        public void PerfToIndex()
        {
            Measure
                .Method(() => Utils.ToIndex(10, 50))
                .WarmupCount(10)
                .MeasurementCount(5)
                .IterationsPerMeasurement(100)
                .GC()
                .Run();
        }

        [Test]
        public void TestToIndex()
        {
            Assert.AreEqual(10, Utils.ToIndex(10, 50));
            Assert.AreEqual(0, Utils.ToIndex(0, 50));
            Assert.AreEqual(49, Utils.ToIndex(49, 50));
            Assert.AreEqual(49, Utils.ToIndex(50, 50));
            Assert.AreEqual(49, Utils.ToIndex(51, 50));
        }
    }
}
