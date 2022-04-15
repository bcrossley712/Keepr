using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
      _vaultsRepository = vaultsRepository;
    }

    internal Vault Create(string id, Vault vaultData)
    {
      vaultData.CreatorId = id;
      return _vaultsRepository.Create(vaultData);
    }


    internal Vault GetById(int id)
    {
      Vault found = _vaultsRepository.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid vault Id");
      }
      return found;
    }

    internal Vault Update(string id, Vault updateData)
    {
      Vault original = GetById(updateData.Id);
      if (original.CreatorId != id)
      {
        throw new Exception("You are not allowed to update this vault");
      }
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      original.Name = updateData.Name ?? original.Name;
      original.IsPrivate = updateData.IsPrivate != null ? updateData.IsPrivate : original.IsPrivate;
      _vaultsRepository.Update(original);
      // NOTE this get is to return the correct updatedAt date and time
      return GetById(updateData.Id);
    }

    internal void Delete(string userId, int id)
    {
      Vault found = GetById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("You cannot delete this vault");
      }
      _vaultsRepository.Delete(id);
    }
  }
}