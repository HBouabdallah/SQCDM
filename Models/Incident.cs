using System;
using System.Collections.Generic;

namespace IndustryIncident.Models;

public partial class Incident
{
    public int Id { get; set; }

    public string Iduser { get; set; } = null!;

    public int Type { get; set; }

    public string Description { get; set; } = null!;

    public int Zone { get; set; }

    public DateTime Date { get; set; }

    public int Indicator { get; set; }
}
