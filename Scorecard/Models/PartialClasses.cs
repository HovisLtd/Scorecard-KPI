using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Scorecard.Models
{
    [MetadataType(typeof(ScorecardMetaData))]
    public partial class t_Scorecard_Master_Categories
    {
    }

    [MetadataType(typeof(MinorcatMetaData))]
    public partial class t_Scorecard_Minor_Categories
    {
    }

    [MetadataType(typeof(PlantMetaData))]
    public partial class v_PTLStaff_MasterData_Plants
    {
    }

    [MetadataType(typeof(TemplateMetaData))]
    public partial class t_Scorecard_Master_Template
    {
    }

    [MetadataType(typeof(ScorecardDetailsMetaData))]
    public partial class t_Scorecard_Details
    {
    }

    [MetadataType(typeof(UserSiteMetaData))]
    public partial class t_Scorecard_Master_UserSites
    {
    }
}