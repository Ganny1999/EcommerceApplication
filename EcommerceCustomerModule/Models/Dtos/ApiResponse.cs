using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Net.NetworkInformation;

namespace EcommerceCustomerModule.Models.Dtos
{
    public class ApiResponse<T>
    {
        
        public T Data { get; set; }
        public bool Status {  get; set; }
        public string Massage { get; set; }
        public int StatusCode { get;set; }
        public ApiResponse( int _StatusCode,string _error,bool _Status)
        {
            StatusCode = _StatusCode;
            Massage = _error;
            Status = _Status;
        }
        public ApiResponse(T _Data,int _StatusCode, string _error,bool _status)
        {
            Data = _Data;
            StatusCode = _StatusCode;
            Massage = _error;
            Status = _status;
        }
    }
    
}
