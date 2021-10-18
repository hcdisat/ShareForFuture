using System.ComponentModel;

namespace ShareForFuture.Data;

 public class ModelBase
{
    public int Id { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}