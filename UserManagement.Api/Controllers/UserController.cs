
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces; 

namespace UserManagement.Api.Controllers;

[Route("users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserAuditService _userAuditService;
    public UserController(IUserService userService, IUserAuditService userAuditService)
    {
        _userService = userService;
        _userAuditService = userAuditService;
    }


    // GET: api/User
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }


    // GET: api/User/5
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetById(id);
        if(user == null)
        {
            return NotFound();
        }
        _userAuditService.Save(new Models.UserAudit
        {
            AuditLogType = Data.Common.AuditLogType.View,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Forename = user.Forename,
            IsActive = user.IsActive,
            UserId = user.Id,
            Surname = user.Surname
        });
        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _userService.Upsert(user);
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult UpdateUser(long id, [FromBody] User user)
    {
        _userService.Upsert(user);
        return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteUser(long id)
    {
        var user = _userService.GetById(id);
        if(user == null)
        {
            return NotFound();
        }

        _userAuditService.Save(new Models.UserAudit
        {
            AuditLogType = Data.Common.AuditLogType.Delete,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Forename = user.Forename,
            IsActive = user.IsActive,
            UserId = user.Id,
            Surname = user.Surname
        });

        _userService.Delete(user);
        return Ok();
    }


    [HttpGet("Audits")]
    public IActionResult GetUserAudits()
    {
        var audits = _userAuditService.GetAll();
        return Ok(audits);
    }
    // Other CRUD operations here...
}

