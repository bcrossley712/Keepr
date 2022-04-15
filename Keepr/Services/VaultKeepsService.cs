using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
    }

    internal VaultKeep Create(string id, VaultKeep vaultKeepData)
    {
      throw new NotImplementedException();
    }

    internal List<VaultKeep> GetAll()
    {
      throw new NotImplementedException();
    }

    internal void Delete(string id1, int id2)
    {
      throw new NotImplementedException();
    }
  }
}