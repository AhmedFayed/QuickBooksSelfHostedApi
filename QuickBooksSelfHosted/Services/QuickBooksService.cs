using System;
using System.Collections.Generic;
using System.Configuration;
using Interop.QBXMLRP2;
using NLog;
using QuickBooksSelfHostedApi.Helper;
using QuickBooksSelfHostedApi.Models;
using QuickBooksSelfHostedApi.Models.Queries;
using QuickBooksSelfHostedApi.Utilites;

namespace QuickBooksSelfHostedApi.Services
{
    public class QuickBooksService : IDisposable
    {
        private readonly string appID;
        private readonly string appName;
        private string ticket;
        private RequestProcessor2 rp;
        private readonly ILogger _logger;
        private readonly string companyFile;
        private QBFileMode mode = QBFileMode.qbFileOpenDoNotCare;

        //private readonly QBContext _qBContext;

        public QuickBooksService()
        {
            appID = ConfigurationReader.GetValue("AppId");
            appName = ConfigurationReader.GetValue("AppName");
            companyFile = ConfigurationReader.GetValue("QuickBooksCopmanyFile");
            _logger = LogManager.GetCurrentClassLogger();
        }

        private void ConnectToQB()
        {
            try
            {

                rp = new RequestProcessor2Class();
                _logger.Info($"Open Connection With Quick Books Copmany: {companyFile}");
                rp.OpenConnection(appID, appName);
                ticket = rp.BeginSession(companyFile, mode);
                string[] versions = rp.get_QBXMLVersionsForSession(ticket);
                _logger.Info($"Connection With Quick Books Succeeded Ticket:{ticket}");
            }
            catch (Exception exception)
            {
                _logger.Error("Can Not Start Connection With Quick Books");
                _logger.Fatal(exception.ToString());
            }
        }


        private void DisconnectFromQB()
        {
            if (ticket != null)
            {
                try
                {
                    rp.EndSession(ticket);
                    ticket = null;
                    rp.CloseConnection();
                    _logger.Info($"Connection With Quick Books Terminated Successfully");
                }
                catch (Exception e)
                {
                    _logger.Fatal($"Disconnect From Quick Books Error Message {e}");
                }
            }
        }

        public void Dispose()
        {
            //DisconnectFromQB();
        }

        private string ProcessRequestFromQB(string request)
        {
            
            try
            {
                ConnectToQB();
                if (ticket is null)
                {
                    _logger.Info($"Connection Does Not Exist");
                    return null;
                }
                request = request.Replace(" ", " ");

                _logger.Info($"Processing Quick Books Request : {request}");
                var response = rp.ProcessRequest(ticket, request);
                _logger.Info($"Quick Books Request : {request} Processed Successfully");
                _logger.Info($"Quick Books Request : {request} Processed Successfully With Response :{response}");
                return response;
            }
            catch (Exception e)
            {
                _logger.Error($"Exception When Processing Quick Books Request : {e}");
                return null;
            }
            finally
            {
               DisconnectFromQB();
            }
        }

        public ItemInventoryRet GetInventoryItem(string identifier)
        {
            _logger.Info($"Get InventoryItem With Full Name: {identifier}");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("ItemQueryRq", identifier, "FullName");
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBResponse<ItemInventoryRet>(response, "ItemInventoryRet");
            return result;
        }

        public List<ItemInventoryRet> GetInventoryItems()
        {
            _logger.Info($"Get All InventoryItems");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("ItemQueryRq", null);
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<ItemInventoryRet>>(response, "ItemQueryRs", "ItemInventoryRet");
            return result;
        }

        public CustomerRet GetCustomer(string identifier)
        {
            _logger.Info($"Get Customer With Full Name: {identifier}");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("CustomerQueryRq", identifier, "FullName");
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBResponse<CustomerRet>(response, "CustomerRet");
            return result;
        }

        public List<CustomerRet> GetCustomers()
        {
            _logger.Info($"Get All Customers");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("CustomerQueryRq", null);
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<CustomerRet>>(response, "CustomerQueryRs", "CustomerRet");
            return result;
        }

        public VendorRet GetVendor(string identifier)
        {
            _logger.Info($"Get Vendor With Full Name: {identifier}");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("VendorQueryRq", identifier, "FullName");
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBResponse<VendorRet>(response, "VendorRet");
            return result;
        }

        public List<VendorRet> GetVendors()
        {
            _logger.Info($"Get All Vendors ");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("VendorQueryRq", null);
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<VendorRet>>(response, "VendorQueryRs", "VendorRet");
            return result;
        }

        public InvoiceRet GetInvoice(string identifier)
        {
            _logger.Info($"Get Invoice With Id: {identifier}");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("InvoiceQueryRq", identifier, "TxnID", QBParser.PropertiesFromType<InvoiceRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBResponse<InvoiceRet>(response, "InvoiceRet", "InvoiceLineRet");
            return result;
        }

        public List<InvoiceRet> GetInvoices()
        {
            _logger.Info($"Get All Invoices");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("InvoiceQueryRq", QBParser.PropertiesFromType<InvoiceRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<InvoiceRet>>(response, "InvoiceQueryRs", "InvoiceRet", "InvoiceLineRet", true);
            return result;
        }

        public List<InvoiceRet> GetInvoices(IReadOnlyCollection<string> identifiers)
        {
            _logger.Info($"Get Invoices With Ids: {String.Join(" ,", identifiers)}");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("InvoiceQueryRq", identifiers, "TxnID", QBParser.PropertiesFromType<InvoiceRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<InvoiceRet>>(response, "InvoiceQueryRs", "InvoiceRet", "InvoiceLineRet", true);
            if (result is null)
            {
                return default;
            }
            result.RemoveAll(item => item is null);
            return result;
        }

        public PurchaseOrderRet GetPurchaseOrder(string identifier)
        {
            _logger.Info($"Get Purchase Order With Id: {identifier}");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("PurchaseOrderQueryRq", identifier, "TxnID", QBParser.PropertiesFromType<PurchaseOrderRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBResponse<PurchaseOrderRet>(response, "PurchaseOrderRet", "PurchaseOrderLineRet");
            if (result is null)
            {
                return default;
            }
            result.PurchaseOrderLineRet.RemoveAll(item => item is null);
            return result;
        }

        public List<PurchaseOrderRet> GetPurchaseOrders()
        {
            _logger.Info($"Get All Purchase Orders");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("PurchaseOrderQueryRq", QBParser.PropertiesFromType<PurchaseOrderRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<PurchaseOrderRet>>(response, "PurchaseOrderQueryRs", "PurchaseOrderRet", "PurchaseOrderLineRet");
            return result;

        }

        public ItemReceiptRet GetItemReceipt(string identifier)
        {
            _logger.Info($"Get All Item Receipt");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("ItemReceiptQueryRq", identifier, "TxnID", QBParser.PropertiesFromType<ItemReceiptRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<ItemReceiptRet>(response, "ItemReceiptQueryRs", "ItemReceiptRet", "ItemLineRet");
            return result;
        }

        public List<ItemReceiptRet> GetItemReceipts()
        {
            _logger.Info($"Get All Item Receipt");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("ItemReceiptQueryRq", identefir: null, null, QBParser.PropertiesFromType<ItemReceiptRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<ItemReceiptRet>>(response, "ItemReceiptQueryRs", "ItemReceiptRet", "ItemLineRet");
            return result;
        }

        public BillRet GetBill(string identifier)
        {
            _logger.Info($"Get All Item Receipt");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("BillQueryRq", identifier, "TxnID", QBParser.PropertiesFromType<BillRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<BillRet>(response, "BillQueryRs", "BillRet", "ItemLineRet");
            return result;
        }

        public List<BillRet> GetBills()
        {
            _logger.Info($"Get All Item Receipt");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("BillQueryRq", QBParser.PropertiesFromType<BillRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<BillRet>>(response, "BillQueryRs", "BillRet", "ItemLineRet", true);
            if (result is null)
            {
                return default;
            }
            result.RemoveAll(item => item is null);
            return result;
        }

        public List<BillRet> GetBills(IReadOnlyCollection<string> identifiers)
        {
            _logger.Info($"Get Bills With IDs {String.Join(" ,", identifiers)}");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("BillQueryRq", identifiers, "TxnID", QBParser.PropertiesFromType<BillRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<BillRet>>(response, "BillQueryRs", "BillRet", "ItemLineRet", true);
            if (result is null)
            {
                return default;
            }
            result.RemoveAll(item => item is null);
            return result;
        }

        public List<ReceivePaymentRet> GetReceivePayment()
        {
            _logger.Info($"Get All Item Receipt");
            var requestQuery = QBRequestBuilder.buildQueryRequestXML("ReceivePaymentQueryRq", QBParser.PropertiesFromType<ReceivePaymentRet>());
            var response = ProcessRequestFromQB(requestQuery);
            var result = QBParser.ParseQBListResponse<List<ReceivePaymentRet>>(response, "ReceivePaymentQueryRs", "ReceivePaymentRet", "AppliedToTxnRet", true);

            if (result is null)
            {
                return default;
            }
            result.RemoveAll(item => item is null);
            return result;
        }
        public Tuple<PurchaseOrderRet, QBRequestStatus> AddPurchaseOrder(PurchaseOrderAdd purchaseOrder)
        {
            var purchaseOrderXml = QBRequestBuilder.BuildAddRequest(purchaseOrder);

            string response = ProcessRequestFromQB(purchaseOrderXml);

            QBRequestStatus status = null;
            PurchaseOrderRet savedPurchaseOrder = null;

            if (response != null)
                status = QBParser.ParseRequestRs(response, "PurchaseOrderAddRs");

            if (response != null & status?.StatusCode == "0")
            {
                _logger.Info($"Purchase Order : {purchaseOrderXml} Added Successfully");
                savedPurchaseOrder = QBParser.ParseQBResponse<PurchaseOrderRet>(response, "PurchaseOrderRet", "PurchaseOrderLineRet");
            }
            else
            {
                _logger.Error($"Error When Adding Purchase Order : {purchaseOrderXml}");
                if (status is null)
                {
                    status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                }
            }

            return new Tuple<PurchaseOrderRet, QBRequestStatus>(savedPurchaseOrder, status);
        }

        public Tuple<ItemReceiptRet, QBRequestStatus> AddItemReceipt(ItemReceiptAdd itemReceiptAdd)
        {
            var itemReceiptAddRequestXml = QBRequestBuilder.BuildAddRequest(itemReceiptAdd);

            string response = ProcessRequestFromQB(itemReceiptAddRequestXml);

            QBRequestStatus status = null;
            ItemReceiptRet savedItemReceipt = null;

            if (response != null)
                status = QBParser.ParseRequestRs(response, "ItemReceiptAddRs");

            if (response != null & status?.StatusCode == "0")
            {
                _logger.Info($"Purchase Order : {itemReceiptAddRequestXml} Added Successfully");
                savedItemReceipt = QBParser.ParseQBResponse<ItemReceiptRet>(response, "ItemReceiptRet", "ItemLineRet");
            }
            else
            {
                _logger.Error($"Error When Adding Purchase Order : {itemReceiptAddRequestXml}");
                if (status is null)
                {
                    status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                }
            }

            return new Tuple<ItemReceiptRet, QBRequestStatus>(savedItemReceipt, status);
        }

        public Tuple<BillRet, QBRequestStatus> AddBill(BillAdd billAdd)
        {
            var itemReceiptBillAddRequestXml = QBRequestBuilder.BuildAddRequest(billAdd);

            string response = ProcessRequestFromQB(itemReceiptBillAddRequestXml);

            QBRequestStatus status = null;
            BillRet savedBill = null;

            if (response != null)
                status = QBParser.ParseRequestRs(response, "BillAddRs");

            if (response != null & status?.StatusCode == "0")
            {
                _logger.Info($"Item Receipt Bill : {itemReceiptBillAddRequestXml} Added Successfully");
                savedBill = QBParser.ParseQBResponse<BillRet>(response, "BillRet", "ItemLineRet");
            }
            else
            {
                _logger.Error($"Error When Adding Item Receipt Bill : {itemReceiptBillAddRequestXml}");
                if (status is null)
                {
                    status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                }
            }

            return new Tuple<BillRet, QBRequestStatus>(savedBill, status);
        }

        public Tuple<InvoiceRet, QBRequestStatus> AddInvoice(InvoiceAdd invoiceAdd)
        {
            var invoiceXml = QBRequestBuilder.BuildAddRequest(invoiceAdd);

            string response = ProcessRequestFromQB(invoiceXml);

            QBRequestStatus status = null;
            InvoiceRet savedInvoice = null;
            if (response != null)
                status = QBParser.ParseRequestRs(response, "InvoiceAddRs");

            if (response != null & status?.StatusCode == "0")
            {
                _logger.Info($"Invoice : {invoiceXml} Added Successfully");
                savedInvoice = QBParser.ParseQBResponse<InvoiceRet>(response, "InvoiceRet", "InvoiceLineRet");
            }
            else
            {
                _logger.Error($"Error When Adding Invoice : {invoiceXml}");
                if (status is null)
                {
                    status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                }
            }


            return new Tuple<InvoiceRet, QBRequestStatus>(savedInvoice, status);
        }

        public Tuple<InvoiceRet, QBRequestStatus> ModifyInvoice(InvoiceMod invoiceMod)
        {
            var invoiceXml = QBRequestBuilder.BuildAddRequest(invoiceMod);

            string response = ProcessRequestFromQB(invoiceXml);

            QBRequestStatus status = null;
            InvoiceRet savedInvoice = null;
            if (response != null)
                status = QBParser.ParseRequestRs(response, "InvoiceModRs");

            if (response != null & status?.StatusCode == "0")
            {
                _logger.Info($"Invoice : {invoiceXml} Added Successfully");
                savedInvoice = QBParser.ParseQBResponse<InvoiceRet>(response, "InvoiceRet", "InvoiceLineRet");
            }
            else
            {
                _logger.Error($"Error When Adding Invoice : {invoiceXml}");
                if (status is null)
                {
                    status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                }
            }


            return new Tuple<InvoiceRet, QBRequestStatus>(savedInvoice, status);
        }

        public Tuple<VendorRet, QBRequestStatus> AddVendor(VendorAdd vendorAdd)
        {
            var existVendor = GetVendor(vendorAdd.Name);
            if (existVendor is null)
            {
                var vendorAddRequest = QBRequestBuilder.BuildAddRequest(vendorAdd);

                string response = ProcessRequestFromQB(vendorAddRequest);
                VendorRet vendorRet = null;
                QBRequestStatus status = null;

                if (response != null)
                    status = QBParser.ParseRequestRs(response, "VendorAddRs");

                if (response != null & status?.StatusCode == "0")
                {
                    _logger.Info($"Vendor : {vendorAddRequest} Added Successfully");
                    vendorRet = QBParser.ParseQBResponse<VendorRet>(response, "VendorRet");
                }
                else
                {
                    _logger.Error($"Error When Adding Vendor : {vendorAddRequest}");
                    if (status is null)
                    {
                        status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                    }
                }
                return new Tuple<VendorRet, QBRequestStatus>(vendorRet, status);
            }
            return new Tuple<VendorRet, QBRequestStatus>(existVendor, null);
        }

        public Tuple<CustomerRet, QBRequestStatus> AddCustomer(CustomerAdd customerAdd)
        {
            var existCustomer = GetCustomer(customerAdd.Name);
            if (existCustomer is null)
            {
                var vendorAddRequest = QBRequestBuilder.BuildAddRequest(customerAdd);

                string response = ProcessRequestFromQB(vendorAddRequest);

                QBRequestStatus status = null;

                if (response != null)
                    status = QBParser.ParseRequestRs(response, "CustomerAddRs");

                CustomerRet savedCustomer = null;

                if (response != null & status?.StatusCode == "0")
                {
                    _logger.Info($"Customer : {vendorAddRequest} Added Successfully");
                    savedCustomer = QBParser.ParseQBResponse<CustomerRet>(response, "CustomerRet");
                }
                else
                {
                    _logger.Error($"Error When Adding Customer : {vendorAddRequest}");
                    if (status is null)
                    {
                        status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                    }
                }

                return new Tuple<CustomerRet, QBRequestStatus>(savedCustomer, status);
            }
            return new Tuple<CustomerRet, QBRequestStatus>(existCustomer, null);
        }

        public Tuple<ItemInventoryRet, QBRequestStatus> AddInventoryItem(ItemInventoryAdd itemInventoryAdd)
        {
            var existItem = GetInventoryItem(itemInventoryAdd.Name);
            if (existItem is null)
            {
                var itemInventoryAddRequest = QBRequestBuilder.BuildAddRequest(itemInventoryAdd);

                string response = ProcessRequestFromQB(itemInventoryAddRequest);
                ItemInventoryRet savedItem = null;
                QBRequestStatus status = null;

                if (response != null)
                    status = QBParser.ParseRequestRs(response, "ItemInventoryAddRs");

                if (response != null & status?.StatusCode == "0")
                {
                    _logger.Info($"Item Inventory : {itemInventoryAddRequest} Added Successfully");
                    savedItem = QBParser.ParseQBResponse<ItemInventoryRet>(response, "ItemInventoryRet");
                }
                else
                {
                    _logger.Error($"Error When Adding Item Inventory : {itemInventoryAddRequest}");
                    if (status is null)
                    {
                        status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                    }
                }

                return new Tuple<ItemInventoryRet, QBRequestStatus>(savedItem, status);
            }
            return new Tuple<ItemInventoryRet, QBRequestStatus>(existItem, null);

        }

        public List<ItemInventoryRet> AddInventoryItems(List<ItemInventoryAdd> itemsInventoryAdd)
        {
            var savedItems = new List<ItemInventoryRet>();

            foreach (var item in itemsInventoryAdd)
            {
                var existItem = GetInventoryItem(item.Name);

                if (existItem is null)
                {
                    var itemInventoryAddRequest = QBRequestBuilder.BuildAddRequest(item);

                    string response = ProcessRequestFromQB(itemInventoryAddRequest);

                    QBRequestStatus status = null;

                    if (response != null)
                        status = QBParser.ParseRequestRs(response, "ItemInventoryAddRs");

                    if (response != null & status?.StatusCode == "0")
                    {
                        _logger.Info($"Item Inventory : {itemInventoryAddRequest} Added Successfully");
                        var savedItem = QBParser.ParseQBResponse<ItemInventoryRet>(response, "ItemInventoryRet");
                        savedItems.Add(savedItem);
                    }
                    else
                    {
                        _logger.Error($"Error When Adding Item Inventory : {itemInventoryAddRequest}");
                        if (status is null)
                        {
                            status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                        }
                    }
                }
                else
                {
                    savedItems.Add(existItem);
                }
            }
            if (savedItems.Count == 0)
            {
                savedItems = null;
            }
            return savedItems;
        }

        public Tuple<ReceivePaymentRet, QBRequestStatus> AddReceivePayment(ReceivePaymentAdd receivePaymentAdd)
        {
            var receivePaymentAddRequestXml = QBRequestBuilder.BuildAddRequest(receivePaymentAdd);

            string response = ProcessRequestFromQB(receivePaymentAddRequestXml);

            QBRequestStatus status = null;
            ReceivePaymentRet savedReceivePaymentRet = null;

            if (response != null)
                status = QBParser.ParseRequestRs(response, "ReceivePaymentAddRs");

            if (response != null & status?.StatusCode == "0")
            {
                _logger.Info($"Receive Payment : {receivePaymentAddRequestXml} Added Successfully");
                savedReceivePaymentRet = QBParser.ParseQBResponse<ReceivePaymentRet>(response, "ReceivePaymentRet", "AppliedToTxnRet");
            }
            else
            {
                _logger.Error($"Error When Adding Receive Payment : {receivePaymentAddRequestXml}");
                if (status is null)
                {
                    status = new QBRequestStatus { StatusCode = "-1", StatusMessage = "unprocessable request", StatusSeverity = "Exception" };
                }
            }

            return new Tuple<ReceivePaymentRet, QBRequestStatus>(savedReceivePaymentRet, status);
        }

        //public void test()
        //{
        //    string d = System.IO.File.ReadAllText(@"D:\testy.txt");
        //    string response = ProcessRequestFromQB(d);
        //}
    }
}
