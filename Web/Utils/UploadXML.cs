using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Web.Models.Car;
using Web.Models.Showroom;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Web.Utils
{
    public static class UploadXML
    {
        public static List<ShowroomListForXMLModel.Showroom> UploadShowroomXML(IFormFile file)
        {
            MemoryStream output;

            var serializer = new XmlSerializer(typeof(ShowroomListForXMLModel));
            var xml = (ShowroomListForXMLModel)serializer.Deserialize(new StreamReader(file.OpenReadStream()));

            return xml.Showrooms.ToList();
        }

        public static List<CarListForXMLModel.Car> UploadCarXML(IFormFile file)
        {
            MemoryStream output;

            var serializer = new XmlSerializer(typeof(CarListForXMLModel));
            var xml = (CarListForXMLModel)serializer.Deserialize(new StreamReader(file.OpenReadStream()));

            return xml.Cars.ToList();
        }
    }
}
