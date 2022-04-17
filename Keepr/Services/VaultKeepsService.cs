using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly KeepsService _keepsService;
    private readonly KeepsRepository _keepsRepository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, KeepsService keepsService, KeepsRepository keepsRepository, VaultsService vaultsService)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
      _keepsService = keepsService;
      _keepsRepository = keepsRepository;
      _vaultsService = vaultsService;
    }

    internal VaultKeep Create(string userId, VaultKeep vaultKeepData)
    {
      VaultKeep exists = _vaultKeepsRepository.Get(vaultKeepData.VaultId, vaultKeepData.KeepId);
      if(exists != null){
        return exists;
      }
      vaultKeepData.CreatorId = userId;
      Vault foundVault = _vaultsService.GetById(userId, vaultKeepData.VaultId);
      Keep foundKeep = _keepsService.GetById(userId, vaultKeepData.KeepId);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("You did not create this vault so you cannot put something in it");
      }
      foundKeep.Kept++;
      _keepsRepository.Update(foundKeep);
      return _vaultKeepsRepository.Create(vaultKeepData);
    }



    internal void Delete(string userId, int id)
    {
      VaultKeep toDelete = _vaultKeepsRepository.GetById(id);
      Vault foundVault = _vaultsService.GetById(userId, toDelete.VaultId);
      if (userId != foundVault.CreatorId)
      {
        throw new Exception("You did not create this vault so you cannot remove keeps from it");
      }
      Keep foundKeep = _keepsService.GetById(userId, toDelete.KeepId);
      foundKeep.Kept--;
      _keepsRepository.Update(foundKeep);
      _vaultKeepsRepository.Delete(id);
    }
  }
}