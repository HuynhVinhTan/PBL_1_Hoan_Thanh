# 📚 PBL2 - BookStore Management System

Quản lý nhà sách chưa bao giờ dễ dàng đến thế!  
**BookStore Management System** là phần mềm quản lý cửa hàng sách được xây dựng trong khuôn khổ đồ án PBL2, giúp tự động hóa các nghiệp vụ bán hàng, quản lý khách hàng, hóa đơn và kho sách một cách hiệu quả.

---

## 🔧 Tính năng nổi bật

- 🛒 **Quản lý giỏ hàng**: Thêm/xóa sách vào giỏ hàng, cập nhật số lượng linh hoạt.
- 📦 **Quản lý kho sách**: Thêm, sửa, xóa sách; phân loại theo thể loại, tác giả.
- 🧾 **Quản lý hóa đơn**: Tự động tạo, lưu trữ và thống kê hóa đơn bán hàng.
- 👤 **Quản lý người dùng**:
  - **Khách hàng**: Lưu thông tin, lịch sử mua hàng.
  - **Quản lý**: Phân quyền, theo dõi hoạt động hệ thống.
- 📊 **Thống kê doanh thu**:
  - Doanh thu theo ngày, sản phẩm, khách hàng.
  - Top sản phẩm bán chạy, khách hàng thân thiết.

---

## 🖥️ Giao diện & Công nghệ

- 🔹 Ngôn ngữ: **C# (Windows Forms)**
- 🔹 IDE: **Visual Studio 2022**
- 🔹 Dữ liệu: CSV hoặc file text
- 🔹 Biểu đồ trực quan: **LiveCharts**

---

## 🏗️ Kiến trúc dự án

- **DTO (Data Transfer Objects)**: Book, Customer, Invoice, Manager...
- **BUS (Business Logic)**: Xử lý logic chính (thống kê, lọc, tính toán...)
- **GUI (WinForms)**: Giao diện người dùng dễ sử dụng, hiện đại
- **Data Layer**: Đọc/ghi dữ liệu từ file `.csv` và `.txt`

---

## 📁 Cấu trúc thư mục
├── DTO/ # Các lớp dữ liệu
├── BUS/ # Xử lý nghiệp vụ
├── View/ # Giao diện WinForms
├── Data/ # File dữ liệu (CSV, TXT)
├── Resources/ # Hình ảnh, icon
└── README.md

---

## 🚀 Hướng dẫn chạy thử

1. Clone dự án về:
   ```bash
   git clone https://github.com/HuynhVinhTan/PBL_1_Hoan_Thanh.git
2. Mở bằng Visual Studio 2022
3. Chạy trực tiếp bằng nút Start (F5)
4. Dữ liệu demo có sẵn trong thư mục /Data
