using System.ComponentModel.DataAnnotations.Schema;

namespace Comanda.Database.Entities;

[Table("OrderLineSide")]
public class OrderLineSideDatabaseEntity
{
    // Many-to-one relationships
    public int OrderLineId { get; set; }
    public virtual OrderLineDatabaseEntity OrderLine { get; set; } = null!;

    // Many-to-one relationships
    public int SideId { get; set; }
    public virtual SideDatabaseEntity Side { get; set; } = null!;
}







