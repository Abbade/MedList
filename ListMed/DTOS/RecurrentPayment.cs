using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class RecurrentPayment
    {
        public string RecurrentPaymentId { get; set; }
        public string NextRecurrency { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Interval { get; set; }
        public Link Link { get; set; }
        public bool AuthorizeNow { get; set; }
        public int Amount { get; set; }
        public string Country { get; set; }
        public DateTime CreateDate { get; set; }
        public int CurrentRecurrencyTry { get; set; }
        public string Provider { get; set; }
        public int RecurrencyDay { get; set; }
        public int SuccessfulRecurrences { get; set; }
        public List<RecurrentTransaction> RecurrentTransactions { get; set; }
        public int Status { get; set; }
    }
}