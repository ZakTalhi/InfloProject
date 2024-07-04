
using Microsoft.AspNetCore.Mvc; 
using UserManagement.Services.Domain.Interfaces; 

namespace UserManagement.Api.Controllers;

[Route("usersAudit")]
[ApiController]
public class UserAuditController : Controller
{
  
    private readonly IUserAuditService _userAuditService;
    public UserAuditController( IUserAuditService userAuditService)
    { 
        _userAuditService = userAuditService;
    }
     

    [HttpGet]
    public IActionResult List()
    {
        var audits = _userAuditService.GetAll();
        return Ok(audits);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        var userAudit = _userAuditService.GetById(id);
        if (userAudit == null)
        {
            return NotFound();
        }
        return Ok(userAudit);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUser(long userId)
    {
        var items = _userAuditService.GetByUserId(userId).OrderByDescending(o => o.CreatedDate);

        return Ok(items);
    }
    // Other CRUD operations here...
}

