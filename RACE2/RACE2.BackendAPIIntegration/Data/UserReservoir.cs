using System;
using System.Collections.Generic;

namespace RACE2.BackendAPIIntegration.Data;

public partial class UserReservoir
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ReservoirId { get; set; }

    public string? AppointmentType { get; set; }

    public DateTime? AppointmentStartDate { get; set; }

    public DateTime? AppointmentEndDate { get; set; }

    public string? BackEndAppointmentId { get; set; }

    public virtual Reservoir? Reservoir { get; set; }

    public virtual AspNetUser? User { get; set; }
}
