using PBL2_BookStoreManagement.DTO;
using System.Collections.Generic;
using PBL2_BookStoreManagement.DAL;
using PBL2_BookStoreManagement.BUS;
using System;
using System.Dynamic;
using System.Windows.Forms;

class BUS_Invoice
{
    
    #region khoi tao
    private static BUS_Invoice _instance;
    public static BUS_Invoice Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BUS_Invoice();
            }
            return _instance;
        }
        private set { }
    }
    private BUS_Invoice() { }
    #endregion

    #region Method
    public string create_Invoice_Info()  //tao hoa don -> ghi vao file csv
    {
        int invoiceCount = DAL_Invoice.Instance.get_Invoice(Session.Cur_cus.User_id).Count;
        string user_id = Session.Cur_cus.User_id;
        string username = Session.Cur_cus.User_name;
        string invoice_id = $"INV{invoiceCount.ToString("D3")}";
        DateTime now = DateTime.Now;
        var itemsInCart = BUS_Cart.Instance.GetCart();
        List<Cart> bookincart = itemsInCart;
        double total = BUS_Cart.Instance.GetTotalPrice();
        Invoice invoice = new Invoice(invoice_id, user_id, now, bookincart, total);
        // Lưu vào summary
        DAL_Invoice.Instance.update_UserID_summary(user_id, invoice);
        // Lưu chi tiết hóa đơn và lấy đường dẫn
        DAL_Invoice.Instance.create_Invoice_Info(user_id, username, invoice);
        //lay duong dan
        return GetInvoiceText(user_id,invoice_id);
    }

    public string GetInvoiceText(string userId, string invoiceId) //lay duong dan file hoa don 
    {
        return DAL_Invoice.Instance.ReadInvoiceTextFile(userId, invoiceId);
    }

    #endregion
}