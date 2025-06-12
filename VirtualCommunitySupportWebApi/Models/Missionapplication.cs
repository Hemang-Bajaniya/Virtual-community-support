using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class Missionapplication
{
    public int Id { get; set; }

    public int Missionid { get; set; }

    public int Userid { get; set; }

    public DateTime Applieddate { get; set; }

    public bool Status { get; set; }

    public int? Sheet { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
