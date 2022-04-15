using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly VaultsRepository _vaultsRepository;
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(VaultsRepository vaultsRepository, KeepsRepository keepsRepository)
    {
      _vaultsRepository = vaultsRepository;
      _keepsRepository = keepsRepository;
    }

    internal Keep Create(string id, Keep keepData)
    {
      keepData.CreatorId = id;
      keepData.Views = 0;
      keepData.Kept = 0;
      return _keepsRepository.Create(keepData);
    }

    internal List<Keep> GetAll()
    {
      return _keepsRepository.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep found = _keepsRepository.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid keep Id");
      }
      // NOTE this is to update views with every view
      found.Views++;
      _keepsRepository.Update(found);
      return found;
    }

    internal List<VaultsKeepsViewModel> GetVaultsKeeps(int vaultId)
    {
      return _keepsRepository.GetVaultsKeeps(vaultId);
    }


    internal Keep Update(string userId, Keep updateData)
    {
      Keep original = GetById(updateData.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You are not allowed to update this keep");
      }
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      original.Name = updateData.Name ?? original.Name;
      original.Views = updateData.Views != null ? updateData.Views : original.Views;
      original.Kept = updateData.Kept != null ? updateData.Kept : original.Kept;
      _keepsRepository.Update(original);
      // NOTE this get is to return the correct updatedAt date and time
      return GetById(updateData.Id);
    }

    internal void Delete(string userId, int keepId)
    {
      Keep found = GetById(keepId);
      if (found.CreatorId != userId)
      {
        throw new Exception("You are not allowed to delete this keep");
      }
      _keepsRepository.Delete(keepId);
    }
  }
}