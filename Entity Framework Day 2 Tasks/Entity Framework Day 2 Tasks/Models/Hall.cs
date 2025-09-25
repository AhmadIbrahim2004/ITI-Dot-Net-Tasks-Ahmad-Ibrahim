using System;
using System.Collections.Generic;

namespace Entity_Framework_Day_2_Tasks.Models;

public partial class Hall
{
    public int HallId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
