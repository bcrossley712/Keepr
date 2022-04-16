using System;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]

  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly VaultsService _vaultsService;

    public AccountController(AccountService accountService, VaultsService vaultsService)
    {
      _accountService = accountService;
      _vaultsService = vaultsService;
    }

    [HttpGet]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("vaults")]
    public async Task<ActionResult<List<Vault>>> GetAccountsVaults()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vaultsService.GetAccountsVaults(userInfo.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}