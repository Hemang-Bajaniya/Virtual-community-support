using System;
using System.Collections.Generic;

namespace VirtualCommunitySupportWebApi.Models;

public partial class Userskill
{
    public int Id { get; set; }

    public string Skill { get; set; } = null!;

    public int Userid { get; set; }

    public virtual User User { get; set; } = null!;
}
