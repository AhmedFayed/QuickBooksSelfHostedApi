using System.Collections.Generic;

namespace QuickBooksSelfHostedApi.Models.Queries
{
    public class ReceivePaymentRet
	{
		public string TxnID { get; set; }
	 
		public string TimeCreated { get; set; }
	 
		public string TimeModified { get; set; }
	 
		public string EditSequence { get; set; }
	 
		public string TxnNumber { get; set; }
	 
		public ClassRef CustomerRef { get; set; }
	 
		public ClassRef ARAccountRef { get; set; }
		
		public string TxnDate { get; set; }
		
		public string TotalAmount { get; set; }
		
		public ClassRef CurrencyRef { get; set; }
		
		public string ExchangeRate { get; set; }
		
		public string TotalAmountInHomeCurrency { get; set; }
		
		public ClassRef PaymentMethodRef { get; set; }
		
		public ClassRef DepositToAccountRef { get; set; }
		
		public string UnusedPayment { get; set; }
		
		public string UnusedCredits { get; set; }

        public CreditCardTxnInfo CreditCardTxnInfo { get; set; }

        public List<AppliedToTxnRet> AppliedToTxnRet { get; set; }
    }

	public class AppliedToTxnRet
	{
		public string TxnID { get; set; }

		public string TxnType { get; set; }

		public string RefNumber { get; set; }

		public string Amount { get; set; }
	}
}