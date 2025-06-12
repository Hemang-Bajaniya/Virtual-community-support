using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class Userdetail
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Employeeid { get; set; }

    public string? Manager { get; set; }

    public string? Title { get; set; }

    public string? Department { get; set; }

    public string? Myprofile { get; set; }

    public string? Whyivolunteer { get; set; }

    public int? Countryid { get; set; }

    public int? Cityid { get; set; }

    public string? Avilability { get; set; }

    public string? Linkdinurl { get; set; }

    public string? Myskills { get; set; }

    public string? Userimage { get; set; }

    public bool Status { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual User User { get; set; } = null!;
}
