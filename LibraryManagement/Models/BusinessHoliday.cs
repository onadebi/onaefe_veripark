using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BusinessHoliday
    {
        [Key]
        public int HolidayId { get; set; }


        [Required]
        [StringLength(10)]
        public string HolidayOccassion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public bool Active { get; set; } = true;

        public bool IsEdited { get; set; } = false;

        public DateTime? DateLastUpdated { get; set; }

        public BusinessHoliday()
        {
            //if (!string.IsNullOrWhiteSpace(HolidayOccassion))
            //{
            //    HolidayOccassion = HolidayOccassion.Substring(0, 1).ToUpper() + HolidayOccassion.Substring(1);
            //}
        }
    }
}