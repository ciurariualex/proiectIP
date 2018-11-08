using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Car
{
    public class CarListForXMLModel
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Car", IsNullable = false)]
        public Car[] Cars { get; set; }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Car
        {
            private string brand;

            private string model;

            private string vin;

            private string showroom;

            /// <remarks/>
            public string Brand
            {
                get
                {
                    return this.brand;
                }
                set
                {
                    this.brand = value;
                }
            }
            /// <remarks/>
            public string Model
            {
                get
                {
                    return this.model;
                }
                set
                {
                    this.model = value;
                }
            }
            /// <remarks/>
            public string VIN
            {
                get
                {
                    return this.vin;
                }
                set
                {
                    this.vin = value;
                }
            }
            /// <remarks/>
            public string Showroom
            {
                get
                {
                    return this.showroom;
                }
                set
                {
                    this.showroom = value;
                }
            }

        }
    }
}
