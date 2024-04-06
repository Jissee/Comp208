using EoE.GovernanceSystem;

namespace EoE.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual(1, 0);
        }
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(10,10);
            GameResourceType type1 = GameResourceType.Silicon;
            GameResourceType type2 = GameResourceType.Copper;
            GameResourceType type3 = type1 | type2;
            Enum
            var hasSilicon = type3 & GameResourceType.Silicon;
        }
    }
}