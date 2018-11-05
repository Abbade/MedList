using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class Payment
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int Installments { get; set; }
        public string SoftDescriptor { get; set; }
        public RecurrentPayment RecurrentPayment { get; set; }
        public CreditCard CreditCard { get; set; }
        public int ServiceTaxAmount { get; set; }
        public string Interest { get; set; }
        public bool Capture { get; set; }
        public bool Authenticate { get; set; }
        public bool Recurrent { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public List<object> ExtraDataCollection { get; set; }
        public int Status { get; set; }
        public string PaymentId { get; set; }
        public string ProofOfSale { get; set; }
        public string Provider { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string Tid { get; set; }
    }
}