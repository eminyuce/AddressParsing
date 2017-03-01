using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AddressParsing
{
    class Program
    {
        static void Main(string[] args)
        {

            GeocodeResponse result = FromXml(GetAddressResponseFromApi("40.714224,-73.961452"));
            foreach (var item in result.result)
            {
                Console.WriteLine(item.formatted_address);
            }

            Console.WriteLine("Press any key to close.");
            Console.Read();  
        }

        public static XDocument GetAddressResponseFromApi(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0}&sensor=false",address                         );

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());
            return xdoc;
        }
        public static GeocodeResponse FromXml(XDocument xd)
        {
            XmlSerializer s = new XmlSerializer(typeof(GeocodeResponse));
            return (GeocodeResponse)s.Deserialize(xd.CreateReader());
        }
    }
}
