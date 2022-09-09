using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace Course.Shared.DTOs
{
    public class Response<T>
    {
        public T Data { get;private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        public bool IsSuccesful { get; private set; }
        public List<string> Errors { get; set; }

        //Static Factory Meethod
        public static Response<T> Success(T data, int statusCode) // Data donmuyor
        {
            return new Response<T> { Data = data, StatusCode = statusCode,IsSuccesful= true };
        }
        public static Response<T> Success(int statusCode)//Data bos donme durumu
        {
            return new Response<T> {Data=default(T),StatusCode = statusCode,IsSuccesful = true};
        }
        public static Response<T>Fail(List<string>Errors,int statusCode)
        {
            return new Response<T>
            {
                Errors = Errors,
                StatusCode = statusCode,
                IsSuccesful = false
            };
        }
        public static Response<T> Fail(string Error, int statusCode)
        {

            return new Response<T> { Errors = new List<string>() { Error }, StatusCode = statusCode, IsSuccesful = false };
        }

   
    }
}
