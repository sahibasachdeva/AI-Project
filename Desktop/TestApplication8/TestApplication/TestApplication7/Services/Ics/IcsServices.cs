using RestSharp;
using System.Linq;
using System.Xml.Linq;

namespace TestApplication7.Services.Ics
{
    public class IcsServices : IIcsServices
    {
        public decimal GetEstimatedCharges(int weight, int width, int length, int height, string postalCode)
        {
            // what: Perform a SOAP call to ICS, to get back a response with the estimated freight cost
            // whom: ml
            // when: 2016/09/07
            // modified (who/when/why):  
            // Moved from ApiClient into Test Application (2019/03/01)
            // Note: Refactoring this is outside the scope of this particular TestApplication

            var client = new RestClient("http://www1.icscourier.ca/icsapiwebservice/service.asmx");
            RestRequest apiRequest = CreateRequestHeader(Method.POST, "GetEstimatedCharges");
            decimal result = 0;

            string service = "ND";

            // Create "XML" SOAP Request
            string xmlBody =
                GetIcsSoapRequestHeader("GetEstimatedCharges") +
                GetAuthenticationHeader() +
               "<PkgInfo> " +
                "<Product>" + service + "</Product> " +
                    "       <Pieces> " +
                    "         <PieceInfo> " +
                    "           <Weight>" + weight.ToString() + "</Weight> " +
                    "           <WeightUnit>LB</WeightUnit> " +
                    "           <Length>" + length + "</Length> " +
                    "           <Width>" + width + "</Width> " +
                    "           <Height>" + height + "</Height> " +
                    "           <DeclaredValue>0</DeclaredValue> " +
                    "         </PieceInfo> " +
                    "       </Pieces> " +
                    "       <FromPost>" + "M9W7J2" + "</FromPost> " +
                    "       <ToPost>" + postalCode + " </ToPost> " +
                    "     </PkgInfo> " +
                GetIcsSoapRequestFooter("GetEstimatedCharges");

            apiRequest.AddParameter("text/xml", xmlBody, ParameterType.RequestBody);

            // Execute Request
            var response = client.Execute(apiRequest);
            result = GetEstimatedCostFromXmlResponse(response.Content);

            return result;

        }

        private static RestRequest CreateRequestHeader(Method method, string serviceName)
        {
            // Add header request for ICS
            var request = new RestRequest()
            {
                Method = method,
                RequestFormat = DataFormat.Xml
            };
            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("SOAPAction", "http://www.icscourier.ca/" + serviceName);
            return request;
        }

        private static string GetIcsSoapRequestFooter(string ServiceName)
        {
            // Add closing SOAP envelope

            string response =
                 $"    </{ServiceName}>" +
                "  </soap:Body>" +
                "</soap:Envelope>";

            return response;
        }

        private string GetAuthenticationHeader()
        {
            // These are test credentials for commerical rates
            string response =
                    "<AuthenicateAccount>" +
                    "<AccountID>555555</AccountID>" +
                    "<Password>555555</Password>" +
                    "</AuthenicateAccount>";

            return response;
        }

        private static string GetIcsSoapRequestHeader(string ServiceName)
        {
            // Create Soap envelope
            string response;

            response =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                "<soap:Body>" +
                "<" + ServiceName + " xmlns=\"http://www.icscourier.ca/\">";

            return response;
        }

        private decimal GetEstimatedCostFromXmlResponse(string soapXML)
        {
            // what: Extracts and Returns estimated amount to ship
            // whom: ml
            // when: 2019/02/24
            // modified (who/when/why):

            decimal result = 0;
            XNamespace ns = "http://www.icscourier.ca/";

            // Load SOAP response
            XDocument xdoc = XDocument.Parse(soapXML);

            if (IsErrorInResponse(xdoc) == false)
            {
                // Get main fees
                var estimatedCostResponse =
                        from resultObject in xdoc.Descendants(ns + "GetEstimatedChargesResult")
                        select new
                        {
                            BaseAmount = (decimal)resultObject.Element(ns + "BaseCharges"),
                            InsuranceAmount = (decimal)resultObject.Element(ns + "InsuranceCharges"),
                            FuelSurcharge = (decimal)resultObject.Element(ns + "FuelCharges"),
                        };

                decimal netAmount = estimatedCostResponse.FirstOrDefault().BaseAmount;
                decimal fuelSurchargeAmount = estimatedCostResponse.FirstOrDefault().FuelSurcharge;
                result = netAmount + fuelSurchargeAmount;
            }

            return result;
        }

        private static bool IsErrorInResponse(XDocument xdoc)
        {
            // is there an error message in the response document?

            XNamespace ns = "http://www.icscourier.ca/";

            var errors = from errorCodes in xdoc.Descendants(ns + "Err")
                         select new
                         {
                             ErrorCode = (string)errorCodes.Element(ns + "_ErrCode")
                         };

            return errors.Count() > 0;
        }
    }
}