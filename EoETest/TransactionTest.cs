using EoE.Server;
using EoE.Server.TradeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Test
{
    public class TransactionTest
    {
        
        public void Test(ServerPlayer player)
        {
            Transaction transaction = new Transaction(player);

            transaction.AddOfferorOffer(null);
            transaction.AddOfferorOffer(null);

            ServerPlayer rec = null;

            transaction.SetRecipient(rec);

            transaction.AddRecipientOffer(null);
            transaction.RemoveRecipientOffer(null, null);

            transaction.RecipientDeal();

            //transaction.RecipientCancelDeal();

            transaction.OfferorDeal();



        }


    }
}
