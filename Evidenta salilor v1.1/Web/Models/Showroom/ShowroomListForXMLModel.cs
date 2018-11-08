using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Showroom
{
    public class ShowroomListForXMLModel
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Showroom", IsNullable = false)]
        public Showroom[] Showrooms { get; set; }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Showroom
        {
            private string name;

            /// <remarks/>
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Car", IsNullable = false)]
            public Car.CarListForXMLModel.Car[] Cars { get; set; }
        }
    }
}
