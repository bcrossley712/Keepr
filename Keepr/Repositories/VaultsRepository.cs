using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, creatorId, isPrivate, img)
      VALUES
      (@Name, @Description, @CreatorId, @IsPrivate, @Img);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id
      ";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal void Update(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description, 
      isPrivate = @IsPrivate, 
      img = Img
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}