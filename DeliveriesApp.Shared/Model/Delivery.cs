using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveriesApp.Shared.Model
{
    public class Delivery
    {
        #region Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public double OriginLatitude { get; set; }

        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        #endregion Properties

        /// <summary>
        /// 0 - Waiting delivery person
        /// 1 - being delivered
        /// 2 - delivered
        /// </summary>
        public int Status { get; set; }

        public static List<Delivery> GetDeliveries()
        {
            List<Delivery> deliveries=new List<Delivery>();

            Database.Connection.CreateTable<Delivery>();
            deliveries = Database.Connection.Table<Delivery>().ToList();

            return deliveries;
        }

        public static int InsertDelivery(Delivery delivery)
        {
            Database.Connection.CreateTable<Delivery>();
            return Database.Connection.Insert(delivery);
        }
    }
}
