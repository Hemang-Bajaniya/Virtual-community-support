using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Countryname { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    public virtual ICollection<Userdetail> Userdetails { get; set; } = new List<Userdetail>();
}
