using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using KT.Extensions;
using CZAOSCore.Logging;

namespace CZAOSWeb.api
{
    public static class ModalStateHelpers
    {
        public static List<string> ToJson(this ModelStateDictionary modalState)
        {
            var errors = new List<string>();
            foreach (var prop in modalState.Values)
            {
                if (prop.Errors.Any())
                {
                    errors.Add(prop.Errors.First().ErrorMessage);
                }
            }

            return errors;
        }

        public static string EncodeString(this string value)
        {
            if (value.IsNullOrEmpty())
                return string.Empty;

            string encodedText = string.Empty;

            try
            {
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(value);
                encodedText = Convert.ToBase64String(bytesToEncode);
            }
            catch (Exception ex)
            {
                Logger.LogError(ErrorLevel.Error, ex, false);
                encodedText = value;
            }

            return encodedText;            

        }

        public static string DecodeString(this string encodedValue)
        {
            if (encodedValue.IsNullOrEmpty())
                return string.Empty;

            string decodedText = string.Empty;

            try
            {
                byte[] decodedBytes = Convert.FromBase64String(encodedValue);
                decodedText = Encoding.UTF8.GetString(decodedBytes);
            }
            catch (Exception ex)
            {
                Logger.LogError(ErrorLevel.Error, ex, false);
                decodedText = encodedValue;
            }

            return decodedText;
        }


        //public static HttpResponseMessage CreateInvalidObjectResponse(this HttpResponseMessage response, string objectname)
        //{
        //    response = new HttpResponseMessage(HttpStatusCode.NotFound);

        //    response.Content = new StringContent(objectname);

        //    return response;
        //}
    }
}