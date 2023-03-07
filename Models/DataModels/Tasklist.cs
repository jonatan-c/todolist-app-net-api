using System;
using System.Collections.Generic;

namespace todolistapi.Models.DataModels;

public partial class Tasklist
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Priority { get; set; }

    public bool Status { get; set; }
}
