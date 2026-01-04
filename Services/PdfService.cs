using iTextSharp.text;
using iTextSharp.text.pdf;
using BabyShopWeb2.Models;
using System.Text;

namespace BabyShopWeb2.Services
{
    public interface IPdfService
    {
        byte[] GenerateReceipt(Order order);
    }
    
    public class PdfService : IPdfService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public PdfService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        public byte[] GenerateReceipt(Order order)
        {
            using var memoryStream = new MemoryStream();
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var writer = PdfWriter.GetInstance(document, memoryStream);
            
            document.Open();
            
            // Fonts
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, new BaseColor(255, 107, 157));
            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, new BaseColor(0, 0, 0));
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, new BaseColor(0, 0, 0));
            var smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, new BaseColor(128, 128, 128));
            
            // Header
            var headerTable = new PdfPTable(2) { WidthPercentage = 100 };
            headerTable.SetWidths(new float[] { 70, 30 });
            
            // Company Info
            var companyCell = new PdfPCell();
            companyCell.Border = Rectangle.NO_BORDER;
            companyCell.AddElement(new Paragraph("BabyShop3Berlian", titleFont));
            companyCell.AddElement(new Paragraph("Jl. Ganesha No. 99, Kp. Rengasbandung, Desa Karangsambung, Kec. Kedungwaringin, Bekasi", normalFont));
            companyCell.AddElement(new Paragraph("Telp: +62 812-3456-7890", normalFont));
            companyCell.AddElement(new Paragraph("Email: info@babyshop3berlian.com", normalFont));
            headerTable.AddCell(companyCell);
            
            // Invoice Info
            var invoiceCell = new PdfPCell();
            invoiceCell.Border = Rectangle.NO_BORDER;
            invoiceCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            invoiceCell.AddElement(new Paragraph("STRUK PEMBELIAN", headerFont));
            invoiceCell.AddElement(new Paragraph($"No: {order.OrderNumber}", normalFont));
            invoiceCell.AddElement(new Paragraph($"Tanggal: {order.OrderDate:dd/MM/yyyy HH:mm}", normalFont));
            invoiceCell.AddElement(new Paragraph($"Status: {GetStatusText(order.Status)}", normalFont));
            headerTable.AddCell(invoiceCell);
            
            document.Add(headerTable);
            document.Add(new Paragraph(" ")); // Space
            
            // Customer Info
            document.Add(new Paragraph("INFORMASI PELANGGAN", headerFont));
            document.Add(new Paragraph($"Nama: {order.CustomerName}", normalFont));
            document.Add(new Paragraph($"Telepon: {order.CustomerPhone}", normalFont));
            document.Add(new Paragraph($"Alamat: {order.ShippingAddress}", normalFont));
            document.Add(new Paragraph(" ")); // Space
            
            // Items Table
            document.Add(new Paragraph("DETAIL PEMBELIAN", headerFont));
            var itemsTable = new PdfPTable(4) { WidthPercentage = 100 };
            itemsTable.SetWidths(new float[] { 50, 15, 15, 20 });
            
            // Table Headers
            itemsTable.AddCell(new PdfPCell(new Phrase("Produk", headerFont)) { BackgroundColor = new BaseColor(211, 211, 211), Padding = 5 });
            itemsTable.AddCell(new PdfPCell(new Phrase("Qty", headerFont)) { BackgroundColor = new BaseColor(211, 211, 211), Padding = 5, HorizontalAlignment = Element.ALIGN_CENTER });
            itemsTable.AddCell(new PdfPCell(new Phrase("Harga", headerFont)) { BackgroundColor = new BaseColor(211, 211, 211), Padding = 5, HorizontalAlignment = Element.ALIGN_RIGHT });
            itemsTable.AddCell(new PdfPCell(new Phrase("Total", headerFont)) { BackgroundColor = new BaseColor(211, 211, 211), Padding = 5, HorizontalAlignment = Element.ALIGN_RIGHT });
            
            // Table Rows
            foreach (var item in order.OrderItems)
            {
                // Product info without image reference
                var productInfo = $"{item.Product.Name}\n({item.Product.Category.Name})";
                
                itemsTable.AddCell(new PdfPCell(new Phrase(productInfo, normalFont)) { Padding = 5 });
                itemsTable.AddCell(new PdfPCell(new Phrase(item.Quantity.ToString(), normalFont)) { Padding = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                itemsTable.AddCell(new PdfPCell(new Phrase($"Rp {item.UnitPrice:N0}", normalFont)) { Padding = 5, HorizontalAlignment = Element.ALIGN_RIGHT });
                itemsTable.AddCell(new PdfPCell(new Phrase($"Rp {item.Subtotal:N0}", normalFont)) { Padding = 5, HorizontalAlignment = Element.ALIGN_RIGHT });
            }
            
            document.Add(itemsTable);
            document.Add(new Paragraph(" ")); // Space
            
            // Summary
            var summaryTable = new PdfPTable(2) { WidthPercentage = 50, HorizontalAlignment = Element.ALIGN_RIGHT };
            summaryTable.SetWidths(new float[] { 70, 30 });
            
            summaryTable.AddCell(new PdfPCell(new Phrase("Subtotal:", normalFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 3 });
            summaryTable.AddCell(new PdfPCell(new Phrase($"Rp {order.SubTotal:N0}", normalFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 3 });
            
            summaryTable.AddCell(new PdfPCell(new Phrase("Ongkos Kirim:", normalFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 3 });
            summaryTable.AddCell(new PdfPCell(new Phrase($"Rp {order.ShippingCost:N0}", normalFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 3 });
            
            summaryTable.AddCell(new PdfPCell(new Phrase("TOTAL:", headerFont)) { Border = Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 3 });
            summaryTable.AddCell(new PdfPCell(new Phrase($"Rp {order.TotalAmount:N0}", headerFont)) { Border = Rectangle.TOP_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 3 });
            
            document.Add(summaryTable);
            document.Add(new Paragraph(" ")); // Space
            
            // Footer
            document.Add(new Paragraph("Terima kasih telah berbelanja di BabyShop3Berlian!", normalFont) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("Untuk pertanyaan, hubungi customer service kami", smallFont) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph($"Kasir: {order.CashierName ?? "Admin BabyShop"} | Dicetak pada: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", smallFont) { Alignment = Element.ALIGN_CENTER });
            
            document.Close();
            return memoryStream.ToArray();
        }
        
        private string GetStatusText(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "Menunggu",
                OrderStatus.Processing => "Selesai",
                OrderStatus.Shipped => "Dikirim",
                OrderStatus.Delivered => "Terkirim",
                OrderStatus.Cancelled => "Dibatalkan",
                _ => "Unknown"
            };
        }
    }
}