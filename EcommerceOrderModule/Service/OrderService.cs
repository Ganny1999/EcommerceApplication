using ClosedXML.Excel;
using EcommerceOrderModule.Models;
using EcommerceOrderModule.Service.Iservice;

namespace EcommerceOrderModule.Service
{
    public class OrderService : IOrderService
    {
        public byte[] GenerateDailyOrderReportExcel(DateTime dateTime)
        {
            throw new NotImplementedException();

            //var orderList = new List<Order>
            //{
            //    new Order{OrderNumber="1",OrderDate=DateTime.Now,CustomerID="111",TotalAmount= 1000 },
            //    new Order{OrderNumber="2",OrderDate=DateTime.Now,CustomerID="222",TotalAmount= 2000 },
            //    new Order{OrderNumber="3",OrderDate=DateTime.Now,CustomerID="333",TotalAmount= 3000 },
            //    new Order{OrderNumber="4",OrderDate=DateTime.Now,CustomerID="444",TotalAmount= 4000 }
            //};

            //var workbook =new XLWorkbook();

            //var workSheet = workbook.AddWorksheet("Daily Orders");
            //workSheet.Cell(1,1).Value = "Order ID";
            //workSheet.Cell(1,2).Value = "Date Time";
            //workSheet.Cell(1,3).Value = "Customer D=ID";
            //workSheet.Cell(1,4).Value = "Total Amount";

            //int row = 2;
            //foreach (var i in orderList)
            //{
            //    workSheet.Cell(row, 1).Value = i.OrderNumber;
            //    workSheet.Cell(row, 2).Value = i.OrderDate;
            //    workSheet.Cell(row, 3).Value = i.CustomerID;
            //    workSheet.Cell(row, 4).Value = i.TotalAmount;
            //    row++;
            //}

            //var stream = new MemoryStream();
            //workbook.SaveAs(stream);
            //return stream.ToArray();
        }
    }
}
