using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showroom = Core.Models.Showroom;

namespace Web.Models.Car
{
    public class CarEditViewModel
    {
        public CarEditViewModel()
        {

        }

        public CarEditViewModel(IEnumerable<showroom> showrooms)
        {
            SetList(showrooms);
        }
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public IEnumerable<SelectListItem> Showrooms { get; set; }

        public Guid ShowroomId { get; set; }

        public void SetList(IEnumerable<showroom> showrooms)
        {
            Showrooms = showrooms.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });

            var selectedShowroom = ShowroomId.ToString();
            if (showrooms.Count() > 0 && selectedShowroom != Guid.Empty.ToString())
                Showrooms.Where(x => x.Value == selectedShowroom).FirstOrDefault().Selected = true;
        }
    }
}
