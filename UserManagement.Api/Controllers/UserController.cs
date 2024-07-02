
using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Domain.Interfaces; 

namespace UserManagement.Api.Controllers;

[Route("api/users")]
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
        return Ok(user);
    }

    [HttpGet("Audits")]
    public IActionResult GetUserAudits()
    {
        var audits = _userAuditService.GetAll();
        return Ok(audits);
    }
    // Other CRUD operations here...
}

