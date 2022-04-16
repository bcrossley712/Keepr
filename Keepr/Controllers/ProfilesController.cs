using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountsService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(AccountService accountsService, KeepsService keepsService, VaultsService vaultsService)
    {
      _accountsService = accountsService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {
        Profile profile = _accountsService.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetProfilesVaults(string profileId)
    {
      try
      {
        List<Vault> vaults = _vaultsService.GetProfilesVaults(profileId);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetProfilesKeeps(string profileId)
    {
      try
      {
        List<Keep> keeps = _keepsService.GetProfilesKeeps(profileId);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}