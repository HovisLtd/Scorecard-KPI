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
    
    public partial class v_PTLStaff_MasterData_Plants
    {
        public v_PTLStaff_MasterData_Plants()
        {
            this.t_Scorecard_Master_Template = new HashSet<t_Scorecard_Master_Template>();
            this.t_Scorecard_Master_UserSites = new HashSet<t_Scorecard_Master_UserSites>();
        }
    
        public int AutoIDCode { get; set; }
        public string Plant { get; set; }
        public string PlantDesc { get; set; }
    
        public virtual ICollection<t_Scorecard_Master_Template> t_Scorecard_Master_Template { get; set; }
        public virtual ICollection<t_Scorecard_Master_UserSites> t_Scorecard_Master_UserSites { get; set; }
    }
}
