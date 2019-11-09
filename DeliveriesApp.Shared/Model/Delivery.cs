using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp.Shared.Model
{
    public class Delivery
    {
        public string Id { get; set; }

        public static List<Delivery> GetDeliveries()
        {
            List<Delivery> deliveries=new List<Delivery>();

            Database.Connection.CreateTable<Delivery>();
            deliveries = Database.Connection.Table<Delivery>().ToList();

            return deliveries;
        }
    }
}
