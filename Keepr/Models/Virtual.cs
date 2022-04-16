using System;

namespace Keepr.Models
{
  public class Virtual<T>
  {
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}