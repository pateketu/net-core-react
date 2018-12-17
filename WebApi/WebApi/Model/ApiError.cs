﻿using Newtonsoft.Json;

namespace WebApi.Model
{
    public class ApiError   
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}