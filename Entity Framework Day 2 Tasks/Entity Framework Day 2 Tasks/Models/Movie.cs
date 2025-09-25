using System;
using System.Collections.Generic;

namespace Entity_Framework_Day_2_Tasks.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public short? DurationMin { get; set; }

    public string? IsActive { get; set; }

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
