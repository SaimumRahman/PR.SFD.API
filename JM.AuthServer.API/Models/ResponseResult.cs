﻿using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Models
{
    public class ResponseResult
    {
        public ResponseResult()
        {

        }
        public ResponseResult(IEnumerable<IdentityError> errors)
        {
            if (errors != null)
            {
                List<ErrorDetails> msgList = new();
                foreach (var error in errors)
                {
                    msgList.Add(new ErrorDetails { Message = error.Description, StatusCode = error.Code });
                }
                ErrorMessages = msgList;
            }
            IsSuccessStatus = false;

        }
        public ResponseResult(IEnumerable<string> errorMessages)
        {
            if (errorMessages != null)
            {
                List<ErrorDetails> msgList = new();
                foreach (var error in errorMessages)
                {
                    msgList.Add(new ErrorDetails { Message = error, StatusCode = ((int)HttpStatusCode.InternalServerError).ToString() });
                }
                ErrorMessages = msgList;
            }
            IsSuccessStatus = false;

        }
        public ResponseResult(string message, int? statusCode = null) : this(new List<ErrorDetails>() { new ErrorDetails { Message = message, StatusCode = statusCode == null ? ((int)HttpStatusCode.OK).ToString() : statusCode.ToString() } })
        {


        }
        public ResponseResult(string message, string statusCode) : this(new List<ErrorDetails>() { new ErrorDetails { Message = message, StatusCode = statusCode ?? ((int)HttpStatusCode.OK).ToString() } })
        {

        }
        public ResponseResult(string message, bool isSuccessStatus)
        {
            IsSuccessStatus = isSuccessStatus;
            Message = message;
            if (IsSuccessStatus)
                StatusCode = (int)HttpStatusCode.OK;
            else
                StatusCode = (int)HttpStatusCode.InternalServerError;


        }
        public ResponseResult(IEnumerable<ErrorDetails> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessStatus { get; set; }
        public int EffectedRow { get; set; }
        public string Id { get; set; }
        public dynamic Data { get; set; }
        public IEnumerable<ErrorDetails> ErrorMessages { get; set; }


    }

    public class ErrorDetails
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
