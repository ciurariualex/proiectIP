using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using showroom = Core.Models.Showroom;

namespace Web.Models.Showroom
{
    public class ShowroomListViewModel : Pager<showroom>
    {
        public ShowroomListViewModel(IEnumerable<showroom> showrooms, int pageNumber, int pageSize)
             : base(showrooms.AsQueryable(), pageNumber, pageSize)
        { }
    } 
}
