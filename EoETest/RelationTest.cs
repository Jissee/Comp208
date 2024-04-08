using EoE.Server;
using EoE.Server.TradeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Test
{
    public class IntRelation
    {
        public Dictionary<int, List<int>> ProtectedBy;
        private List<int> AlreadyIn;
        public IntRelation()
        {
            ProtectedBy = new Dictionary<int, List<int>>();
            AlreadyIn = new List<int>();
        }
        public void AddProtector(int target, int protector)
        {
            if (!ProtectedBy.ContainsKey(target))
            {
                ProtectedBy[target] = [protector];
            }
            else
            {
                if (!ProtectedBy[target].Contains(protector))
                {
                    ProtectedBy[target].Add(protector);
                }
            }
        }
        public void RemoveProtector(int target, int protector)
        {
            if (!ProtectedBy.ContainsKey(target))
            {
                return;
            }
            if (ProtectedBy[target].Contains(protector))
            {
                ProtectedBy[target].Remove(protector);
            }
        }
        public List<int>? GetDirectProtectors(int target)
        {
            return ProtectedBy[target];
        }
        private void DeepSearch(int target)
        {
            foreach (int protector in ProtectedBy[target])
            {
                if (AlreadyIn.Contains(protector))
                {
                    continue;
                }
                else
                {
                    AlreadyIn.Add(protector);
                    DeepSearch(protector);
                }
            }
        }
        public List<int>? GetProtectorsRecursively(int target)
        {
            AlreadyIn = new List<int>();
            DeepSearch(target);
            return AlreadyIn;
        }
    }
    [TestClass]
    public class RelationTest
    {
        [TestMethod]
        public void TestRelation()
        {
            IntRelation relation = new IntRelation();
            relation.AddProtector(1, 2);
            relation.AddProtector(1, 3);
            relation.AddProtector(2, 3);
            relation.AddProtector(3, 4);
            relation.AddProtector(4, 1);
            relation.AddProtector(5, 6);
            relation.AddProtector(6, 5);
            var x = relation.GetProtectorsRecursively(1);
            foreach(var y in x)
            {
                Debug.WriteLine(y);
            }
            



        }


    }
}
