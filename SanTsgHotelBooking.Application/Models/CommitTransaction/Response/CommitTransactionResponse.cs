using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgHotelBooking.Application.Models.CommitTransaction.Response
{
    public class CommitTransactionResponse
    {
        public Header header { get; set; }
        public Body body { get; set; }
    }

    public class Body
    {
        public string reservationNumber { get; set; }
        public string encryptedReservationNumber { get; set; }
        public string transactionId { get; set; }
    }

    public class Header
    {
        public string requestId { get; set; }
        public bool success { get; set; }
        public DateTime responseTime { get; set; }
        public List<Message> messages { get; set; }
    }

    public class Message
    {
        public int id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
    }

}
