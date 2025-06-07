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
        int invoiceCount = DAL_Invoice.Instance.get_Invoice(Session.Cur_cus.Cus_ID).Count;
        string user_id = Session.Cur_cus.Cus_ID;
        string username = Session.Cur_cus.UserName;
        string invoice_id = $"INV{invoiceCount:D3}_{user_id}";
        DateTime now = DateTime.Now;
        var itemsInCart = BUS_Cart.Instance.GetCart();
        List<Cart> bookincart = itemsInCart;
        double total = BUS_Cart.Instance.GetTotalPrice();
        Invoice invoice = new Invoice(invoice_id, user_id, now, bookincart, total);
        // Lưu vào summary.csv cua tung user
        DAL_Invoice.Instance.update_UserID_summary(user_id, invoice);
        // Lưu hóa đơn tổng quát, và chi tiết hóa đơn lần lượt vào 2 file Invoice.csv và Invoice_Info.csv
        DAL_Invoice.Instance.update_Invoice(invoice);
        // Cập nhật các sách được mua ở file SoldProduct.csv
        DAL_Invoice.Instance.update_SoldProduct(bookincart);
        // Lưu chi tiết hóa đơn và lấy đường dẫn
        DAL_Invoice.Instance.create_Invoice_Info(user_id, username, invoice);
        //lay duong dan
        return GetInvoiceText(user_id,invoice_id);
    }

    public List<Invoice> GetInvoice(string userId) //lay danh sach hoa don
    {
        return DAL_Invoice.Instance.get_Invoice(userId);
    }
    public string GetInvoiceText(string userId, string invoiceId) //lay duong dan file hoa don 
    {
        return DAL_Invoice.Instance.ReadInvoiceTextFile(userId, invoiceId);
    }

    public List<Invoice> GetInvoice()
    {
        return DAL_Invoice.Instance.get_Total_Invoice();
    }

    public List<Cart> GetSoldBook()
    {
        return DAL_Invoice.Instance.get_SoldProduct();
    }
    #endregion
}