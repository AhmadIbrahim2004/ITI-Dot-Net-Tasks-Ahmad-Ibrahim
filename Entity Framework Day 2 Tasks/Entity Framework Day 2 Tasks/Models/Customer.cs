using System;
using System.Collections.Generic;

namespace Entity_Framework_Day_2_Tasks.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
