using System;
using System.Collections.Generic;

namespace Entity_Framework_Day_2_Tasks.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int ShowtimeId { get; set; }

    public int CustomerId { get; set; }

    public string SeatLabel { get; set; } = null!;

    public decimal FinalPrice { get; set; }

    public string? Status { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime BookedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Showtime Showtime { get; set; } = null!;
}
