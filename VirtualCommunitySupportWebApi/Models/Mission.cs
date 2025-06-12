using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class Mission
{
    public int Id { get; set; }

    public string Missiontitle { get; set; } = null!;

    public string? Missiondescription { get; set; }

    public string? Missionorganisationname { get; set; }

    public string? Missionorganisationdetail { get; set; }

    public int? Countryid { get; set; }

    public int? Cityid { get; set; }

    public DateOnly? Startdate { get; set; }

    public DateOnly? Enddate { get; set; }

    public string? Missiontype { get; set; }

    public int? Totalsheets { get; set; }

    public DateOnly? Registrationdeadline { get; set; }

    public int? Missionthemeid { get; set; }

    public int? Missionskillid { get; set; }

    public string? Missionimages { get; set; }

    public string? Missiondocuments { get; set; }

    public string? Missionavilability { get; set; }

    public string? Missionvideourl { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Missionapplication> Missionapplications { get; set; } = new List<Missionapplication>();

    public virtual Missionskill? Missionskill { get; set; }

    public virtual Missiontheme? Missiontheme { get; set; }
}
