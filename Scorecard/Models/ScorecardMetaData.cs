using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Scorecard.Models
{
    public class ScorecardMetaData
    {
        [Display(Name = "Master Category")]
        public string MasterCatCode { get; set; }

 
    }

    public class MinorcatMetaData
    {
        [Display(Name = "Minor Category")]
        public string MinorCatCode { get; set; }
    }

    public class PlantMetaData
    {
        [Display(Name = "Site Code")]
        public string Plant { get; set; }

        [Display(Name = "Site")]
        public string PlantDesc { get; set; }
    }

    public class TemplateMetaData
    {
        [Display(Name = "Site Code")]
        public string SiteCode { get; set; }

        [Display(Name = "Master Category")]
        public string MasterCatID { get; set; }

        [Display(Name = "Minor Category")]
        public string MinorCatID { get; set; }

        [Display(Name = "Actual")]
        [Range(-99999999, 99999999, ErrorMessage = "Please enter a valid Number between {1} and {2}")]
        public string Actual { get; set; }

        [Display(Name = "Target")]
        [Range(-99999999, 99999999, ErrorMessage = "Please enter a valid Number between {1} and {2}")]
        public string Target { get; set; }

    }

    public class ScorecardDetailsMetaData
    {
        [Display(Name = "Site Code")]
        public string SiteCode { get; set; }

        [Display(Name = "Master Category")]
        public string MasterCatID { get; set; }

        [Display(Name = "Minor Category")]
        public string MinorCatID { get; set; }

        [Display(Name = "Actual")]
        [Range(-99999999, 99999999, ErrorMessage = "Please enter a valid Number between {1} and {2}")]
        public string Actual { get; set; }

        [Display(Name = "Target")]
        [Range(-99999999, 99999999, ErrorMessage = "Please enter a valid Number between {1} and {2}")]
        public string Target { get; set; }

        [Display(Name = "User Name")]
        public string ChangedBy { get; set; }

        [Display(Name = "Date Last Changed")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ChangedDate { get; set; }
    }

    public class UserSiteMetaData
    {
        [Display(Name = "Site Code")]
        public string UserSiteCode { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }
    }
}