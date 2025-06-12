using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string? Phonenumber { get; set; }

    public string Emailaddress { get; set; } = null!;

    public string? Usertype { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Missionapplication> Missionapplications { get; set; } = new List<Missionapplication>();

    public virtual ICollection<Userdetail> Userdetails { get; set; } = new List<Userdetail>();

    public virtual ICollection<Userskill> Userskills { get; set; } = new List<Userskill>();
}
