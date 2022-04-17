using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep Create(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, creatorId, img, views, kept)
      VALUES
      (@Name, @Description, @CreatorId, @Img, @Views, @Kept);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }).ToList();
    }

    internal List<VaultsKeepsViewModel> GetVaultsKeeps(int vaultId)
    {
      string sql = @"
      SELECT
        a.*,
        k.*,
        vk.*
      FROM vaultKeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.vaultId = @vaultId;
      ";
      return _db.Query<Profile, VaultsKeepsViewModel, VaultKeep, VaultsKeepsViewModel>(sql, (a, k, vk) =>
      {
        k.Creator = a;
        k.VaultId = vk.VaultId;
        return k;
      }, new { vaultId }).ToList();
    }

    internal List<Keep> GetProfilesKeeps(string profileId)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @profileId
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { profileId }).ToList();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }

    internal void Update(Keep original)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img,
      views = @Views,
      kept = @Kept
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
    }

    internal void Delete(int keepId)
    {
      string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
      _db.Execute(sql, new { keepId });
    }
  }
}