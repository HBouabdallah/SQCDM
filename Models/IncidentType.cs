using System;
using System.Collections.Generic;

namespace IndustryIncident.Models;

public partial class IncidentType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
}
