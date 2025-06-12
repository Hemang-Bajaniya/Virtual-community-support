using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class City
{
    public int Id { get; set; }

    public int Countryid { get; set; }

    public string Cityname { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    public virtual ICollection<Userdetail> Userdetails { get; set; } = new List<Userdetail>();
}
