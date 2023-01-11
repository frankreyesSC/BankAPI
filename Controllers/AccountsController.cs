using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTO;
using BankAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountsController(AccountService context)
        {
            _service = context;
        }

        // GET: Accounts
        [HttpGet]
        public async Task<IEnumerable<AccountDtoOut>> Get()
        {
            return await _service.GetAll();
        }
    }
}
