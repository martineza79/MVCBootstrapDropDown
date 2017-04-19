using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBootstrapDropDown.Models
{
    public class DropDownViewModel
    {
        [Display(Name="Choose Country")]
        public int? SelectedCountryId { get; set; }
    }
}