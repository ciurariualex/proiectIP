using Core.Models;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Web.Models.Car;
using Web.Models.Showroom;

namespace Web.Utils
{
    public static class GenerateDocument
    {
        public static byte[] GenerateExcel(this IEnumerable<Car> cars, string password, string imagePath)
        {
            int count = 0;
            int line = 2;
            int col = 0;


            FileInfo fileInfo = new FileInfo(@"C:\Excel.xlsx");
            ExcelPackage xlPackage = new ExcelPackage(fileInfo);

            xlPackage.Workbook.Protection.SetPassword(password);

            var sheet = xlPackage.Workbook.Worksheets[DateTime.Now.ToString("yyyyMMdd")];

            if (sheet == null)
            {
                sheet = xlPackage.Workbook.Worksheets.Add(DateTime.Now.ToString("yyyyMMdd"));
            }

            Bitmap bitmap = new Bitmap(imagePath);
            if (bitmap != null)
            {
                int Height = 100;
                int Width = 50;
                ExcelPicture pic = sheet.Drawings.AddPicture("Sample", bitmap);
                pic.SetPosition(0, 0, 0, 0);
                pic.SetSize(Height, Width);
            }
            var cells = sheet.Cells;

            //title
            cells[4, 1].Value = "Cars";

            #region WriteToExcel
            line = 6;

            foreach (Car car in cars)
            {
                count = 0;
                col = 0;

                cells[line, ++col].Value = car.Brand;
                cells[line, ++col].Value = car.Model;
                cells[line, ++col].Value = car.VIN;
                cells[line, ++col].Value = car.Showroom?.Name;

                line++;
            }
            #endregion

            #region WriteHeaderToExcel
            count = 1;
            cells[5, count++].Value = "Brand";
            cells[5, count++].Value = "Name";
            cells[5, count++].Value = "VIN";
            cells[5, count++].Value = "Showroom";
            #endregion

            //style
            using (var range = cells[1, 1, 5, count])
            {
                range.Style.Font.Bold = true;
            }

            xlPackage.Encryption.Password = password;            //set password for Excel File

            System.IO.MemoryStream output = new System.IO.MemoryStream(xlPackage.GetAsByteArray());

            return output.ToArray();
        }

        public static byte[] GenerateExcel(this IEnumerable<Showroom> showrooms, string password, string imagePath)
        {
            int count = 0;
            int line = 2;
            int col = 0;


            FileInfo fileInfo = new FileInfo(@"C:\Excel.xlsx");
            ExcelPackage xlPackage = new ExcelPackage(fileInfo);

            xlPackage.Workbook.Protection.SetPassword(password);

            var sheet = xlPackage.Workbook.Worksheets[DateTime.Now.ToString("yyyyMMdd")];

            if (sheet == null)
            {
                sheet = xlPackage.Workbook.Worksheets.Add(DateTime.Now.ToString("yyyyMMdd"));
            }

            Bitmap bitmap = new Bitmap(imagePath);
            if (bitmap != null)
            {
                int Height = 100;
                int Width = 50;
                ExcelPicture pic = sheet.Drawings.AddPicture("Sample", bitmap);
                pic.SetPosition(0, 0, 0, 0);
                pic.SetSize(Height, Width);
            }
            var cells = sheet.Cells;

            //title
            cells[4, 1].Value = "Showrooms";

            int maxCarsFinal = 0;

            #region SetMaxForFields
            foreach (Showroom showroom in showrooms)
            {
                int k = 0;

                if (showroom.Cars != null)
                {
                    if (maxCarsFinal < showroom.Cars.Count)
                    {
                        maxCarsFinal = showroom.Cars.Count;
                    }
                }
                k++;
            }
            #endregion

            #region WriteToExcel
            line = 6;

            foreach (Showroom showroom in showrooms)
            {
                col = 0;
                count = 0;

                cells[line, ++col].Value = showroom.Name;

                if (showroom.Cars != null)
                {
                    foreach (var car in showroom.Cars)
                    {
                        count++;
                        string displayValue = "Brand: " + car.Brand + ", Model:" + car.Model + ", VIN: " + car.VIN;
                        cells[line, col + count].Value = displayValue;
                    }
                }

                line++;
            }
            #endregion

            count = 1;
            #region WriteHeaderToExcel
            cells[5, count++].Value = "Name";
            for (int j = 0; j < maxCarsFinal; j++)
            {
                cells[5, count++].Value = "Car " + (j + 1);
            }
            #endregion

            //style
            using (var range = cells[1, 1, 5, count])
            {
                range.Style.Font.Bold = true;
            }


            xlPackage.Encryption.Password = password;            //set password for Excel File

            System.IO.MemoryStream output = new System.IO.MemoryStream(xlPackage.GetAsByteArray());

            return output.ToArray();
        }

        public static byte[] GenerateXml(this IEnumerable<Car> cars)
        {
            List<CarListForXMLModel.Car> carsModel;
            System.IO.MemoryStream output;
            XmlSerializer serializer;
            CarListForXMLModel modelCar = new CarListForXMLModel();
            carsModel = new List<CarListForXMLModel.Car>();
            cars.ToList().ForEach(
                car => carsModel.Add(
                    new CarListForXMLModel.Car
                    {
                        Brand = car.Brand,
                        Model = car.Model,
                        VIN = car.VIN,
                        Showroom = car.Showroom?.Name ?? null
                    })
                );
            modelCar.Cars = carsModel.ToArray();
            output = new System.IO.MemoryStream();

            serializer = new XmlSerializer(typeof(CarListForXMLModel));
            serializer.Serialize(output, modelCar);

            return output.ToArray();
        }

        public static byte[] GenerateXml(this IEnumerable<Showroom> showrooms)
        {
            List<CarListForXMLModel.Car> carsModel;
            System.IO.MemoryStream output;
            XmlSerializer serializer;
            ShowroomListForXMLModel modelShowroom = new ShowroomListForXMLModel();
            List<ShowroomListForXMLModel.Showroom> showroomsModel = new List<ShowroomListForXMLModel.Showroom>();
            carsModel = new List<CarListForXMLModel.Car>();

            showrooms.ToList().ForEach(
                showroom => showroomsModel.Add(
                    new ShowroomListForXMLModel.Showroom
                    {
                        Name = showroom.Name,
                        Cars = showroom.Cars.Select(car =>
                          new CarListForXMLModel.Car
                          {
                              Brand = car.Brand,
                              Model = car.Model,
                              VIN = car.VIN,
                              Showroom = car.Showroom?.Name ?? null
                          }).ToArray()
                    }
                ));
            modelShowroom.Showrooms = showroomsModel.ToArray();
            output = new System.IO.MemoryStream();

            serializer = new XmlSerializer(typeof(ShowroomListForXMLModel));
            serializer.Serialize(output, modelShowroom);

            return output.ToArray();
        }
    }
}
