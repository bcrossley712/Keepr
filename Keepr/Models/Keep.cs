namespace Keepr.Models
{
  public class Keep : Virtual<int>
  {
    public string Img { get; set; }
    public int? Views { get; set; }
    public int? Kept { get; set; }
  }
  public class VaultsKeepsViewModel : Keep
  {
    public int? VaultId { get; set; }
  }
}