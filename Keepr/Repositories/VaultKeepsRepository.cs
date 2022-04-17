using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (keepId, vaultId, creatorId)
      VALUES
      (@KeepId, @VaultId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM vaultKeeps
      WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal VaultKeep Get(int vaultId, int keepId)
    {
      string sql = @"
      SELECT * FROM vaultKeeps
      WHERE vaultId = @vaultId AND keepId = @keepId
      ";

      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultId, keepId });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}