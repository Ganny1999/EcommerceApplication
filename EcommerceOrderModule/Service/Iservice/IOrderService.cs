namespace EcommerceOrderModule.Service.Iservice
{
    public interface IOrderService
    {
        public byte[] GenerateDailyOrderReportExcel(DateTime dateTime);
    }
}
