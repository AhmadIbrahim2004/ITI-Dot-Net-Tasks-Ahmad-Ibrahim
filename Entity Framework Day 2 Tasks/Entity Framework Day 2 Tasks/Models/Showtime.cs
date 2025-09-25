using System;
using System.Collections.Generic;

namespace Entity_Framework_Day_2_Tasks.Models;

public partial class Showtime
{
    public int ShowtimeId { get; set; }

    public int MovieId { get; set; }

    public int HallId { get; set; }

    public DateTime StartAt { get; set; }

    public decimal BasePrice { get; set; }

    public virtual Hall Hall { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
