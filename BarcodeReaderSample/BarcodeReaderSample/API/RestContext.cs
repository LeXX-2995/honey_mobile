using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using TraceIQ.Expeditor.Models;

namespace TraceIQ.Expeditor.API
{
    public class RestContext
    {
        public static string Port = string.Empty;
        public static string ApiResource = "/api/";
        public static string Ip = string.Empty;
        public static User User;
        public static string Url = string.Empty;
        public static RestClient Client => new RestClient(Url);

        public static RestRequest CreateAuthorizedRequest(string resource, Dictionary<string, string> parameters, Method method, object body = null)
        {
            var request = new RestRequest(resource, method)
            { RequestFormat = DataFormat.Json };

            if (method == Method.POST)
                request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters) 
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }

            request.JsonSerializer = new JsonNetSerializer(new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                },
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            if (body != null && method == Method.POST)
            {
                var serializedBody = JsonConvert.SerializeObject(body);
                request.AddParameter("application/json", serializedBody, ParameterType.RequestBody);
                //request.AddJsonBody(body);
            }

            return request;
        }

        public static IRestResponse<T> Execute<T>(string resource, Dictionary<string, string> parameters, Method method, object body = null)
            where T : new()
        {
            var request = CreateAuthorizedRequest(resource, parameters, method, body);
            if (request == null)
                return null;

            var url = Client.BuildUri(request);

            Client.FailOnDeserializationError = true;

            var response = Client.Execute<T>(request);

            if(response.StatusCode == HttpStatusCode.BadRequest)
                return response;

            if (response.Data == null && !string.IsNullOrEmpty(response.Content))
                response.Data = JsonConvert.DeserializeObject<T>(response.Content, new StringEnumConverter());

            return response;
        }

        public static OperationResult<T> ExecuteScalar<T>(string resource, Dictionary<string, string> parameters, Method method, object body = null) where T : new()
        {
            if (string.IsNullOrWhiteSpace(resource))
                return OperationResult<T>.Fail("Не указан ресурс");

            var response = Execute<T>(resource, parameters, method, body);

            if (response == null)
                return OperationResult<T>.Fail("Response empty");

            if(response.StatusCode == HttpStatusCode.BadRequest)
                return OperationResult<T>.Fail(response.Content);

            if (response.StatusCode != HttpStatusCode.OK)
                return OperationResult<T>.Fail(response.StatusCode.ToString());

            return response.Data == null ?
                OperationResult<T>.Fail(response.Content) :
                OperationResult<T>.Success(response.Data);
        }
    }
}
