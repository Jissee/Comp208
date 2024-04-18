using EoE.GovernanceSystem;
using EoE.Server.WarSystem;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Test
{
    [TestClass]
    public class ARMYTEST
    {
        [TestMethod]
        public void test()
        {
            ResourceStack Informative = new ResourceStack(GameResourceType.InformativeArmy, 60);
            Informative.Split(Informative.Count);
            ResourceStack Mechanism = new ResourceStack(GameResourceType.MechanismArmy, 60);
            Mechanism.Split(Mechanism.Count);
            
            int number = Mechanism.Count;
            Mechanism.Split(number);
            int numberAfter = Mechanism.Count;

            Assert.AreEqual(0, Informative.Count);
            Assert.AreEqual(0, Mechanism.Count);
        }
    }
}
