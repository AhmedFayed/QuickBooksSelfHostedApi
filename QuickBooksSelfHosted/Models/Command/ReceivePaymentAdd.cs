using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models
{
    public class ReceivePaymentAdd
    {
        public ClassRef CustomerRef { get; set; }

        public ClassRef ARAccountRef { get; set; }

        public string TxnDate { get; set; }

        public string RefNumber { get; set; }

        public string TotalAmount { get; set; }

        public decimal ExchangeRate { get; set; }

        public ClassRef PaymentMethodRef { get; set; }

        public ClassRef DepositToAccountRef { get; set; }

        public CreditCardTxnInfo CreditCardTxnInfo { get; set; }

        public List<AppliedToTxn> AppliedToTxnAdd { set; get; }
    }


    public class AppliedToTxn
    {
        public string TxnID { get; set; }

        public string PaymentAmount { get; set; }
    }

    public class CreditCardTxnInfo
    {
        public CreditCardTxnInputInfo CreditCardTxnInputInfo { get; set; }
    }

    public class CreditCardTxnResultInfo
    {
        public string ResultMessage { get; set; }
    }

    public class CreditCardTxnInputInfo
    {
        public string CreditCardNumber { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string CreditCardAddress { get; set; }

        public string CreditCardPostalCode { get; set; }

        public string TransactionMode { get; set; }
    }
}