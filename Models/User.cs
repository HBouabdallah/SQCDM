using System;
using System.Collections.Generic;

namespace IndustryIncident.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string FamillyName { get; set; } = null!;

    public string Title { get; set; }
}
