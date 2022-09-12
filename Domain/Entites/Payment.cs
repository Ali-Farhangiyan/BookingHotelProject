using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Payment
    {
        public Guid Id { get; set; }

        public int Amount { get; private set; }

        public bool IsPay { get; private set; } = false;

        public DateTime? DatePay { get; private set; } = null;

        public string? Authority { get; private set; } 

        public long RefId { get; private set; } = 0;

        public Booking Booking { get;private set; }

        public int BookingId { get; private set; }

        public Payment()
        {

        }

        public Payment(int amount, int bookingId)
        {
            Amount = amount;
            BookingId = bookingId;
        }

        public void PaymentIsDone(string authority, long refId)
        {
            IsPay = true;
            DatePay = DateTime.Now;
            Authority = authority;
            RefId = refId;
        }
    }
}
