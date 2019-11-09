using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliveriesApp.Shared.Model
{
    public class DeliveryPerson
    {
        public string Id { get; set; }

        public static DeliveryPerson GetDeliveryPerson(string id)
        {
            DeliveryPerson deliveryPerson=new DeliveryPerson();

            Database.Connection.CreateTable<DeliveryPerson>();
            var data = Database.Connection.Table<DeliveryPerson>();

            //If we have rows in delivery person table
            if (Database.Connection.Table<DeliveryPerson>().Any())
            {
                deliveryPerson = data.FirstOrDefault(person => person.Id == id);
            }

            return deliveryPerson;
        }
    }
}
