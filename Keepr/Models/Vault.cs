namespace Keepr.Models
{
  public class Vault : Virtual<int>
  {
    public bool IsPrivate { get; set; } = false;
    public string Img { get; set; }

  }
}