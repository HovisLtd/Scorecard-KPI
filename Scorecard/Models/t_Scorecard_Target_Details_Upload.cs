//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Scorecard.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_Scorecard_Target_Details_Upload
    {
        public long Recid { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Period { get; set; }
        public string SiteCode { get; set; }
        public Nullable<int> MinorCatID { get; set; }
        public Nullable<double> Target { get; set; }
        public Nullable<double> Budget { get; set; }
        public Nullable<double> Target2 { get; set; }
        public Nullable<double> Budget2 { get; set; }
        public Nullable<System.DateTime> ChangedDate { get; set; }
    }
}
