using RestSharp;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace TestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            RestClient client = new RestClient("https://digitalapi.auspost.com.au");
            RestRequest request = new RestRequest("postcode/search.json", Method.Get);



            request.AddHeader("auth-key", "930240b4-8353-42a7-b6fd-18d3d6d5b512");
            request.AddParameter("q", "Oxley");
            request.AddParameter("state", "QLD");


            // act
            RestResponse response = client.Execute(request);


            // var response1 = client.Execute(request);
            // dynamic json = Newtonsoft.Json.Linq.JObject.Parse(response1.Content);
            // int postcode1 = Convert.ToInt32(json.localities.locality.postcode);
            //  Console.WriteLine(json);
            // Console.WriteLine(postcode1);


            JObject jsonData = JObject.Parse(response.Content);
            Assert.AreEqual(4075, jsonData.SelectToken("localities.locality.postcode"));
        }
        [TestMethod]
        public void TestMethod2()
        {
            RestClient client1 = new RestClient("https://digitalapi.auspost.com.au");
            RestRequest request1 = new RestRequest("postage/parcel/domestic/calculate.json", Method.Get);
            request1.AddHeader("auth-key", "930240b4-8353-42a7-b6fd-18d3d6d5b512");
            request1.AddParameter("from_postcode", "4075");
            request1.AddParameter("to_postcode", "2250");
            request1.AddParameter("length", "25");
            request1.AddParameter("width", "25");
            request1.AddParameter("height", "5");
            request1.AddParameter("weight", "1.5");
            request1.AddParameter("service_code", "AUS_PARCEL_REGULAR"); 
            RestResponse response1 = client1.Execute(request1);
            JObject jsonData1 = JObject.Parse(response1.Content);
            Assert.AreEqual(16.65, jsonData1.SelectToken("postage_result.costs.cost.cost"));

        }
    }
}
