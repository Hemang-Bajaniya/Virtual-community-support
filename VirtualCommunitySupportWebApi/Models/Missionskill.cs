using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class Missionskill
{
    public int Id { get; set; }

    public string Skillname { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();
}
