using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.UserAudit;

namespace UserManagement.WebMS.Controllers;

[Route("usersAudit")]
public class UsersAuditController : Controller
{
    private readonly IUserAuditService _userAuditService;
    private readonly IUserService _userService;
    public UsersAuditController(IUserAuditService userAuditService, IUserService userService)
    {
        _userAuditService = userAuditService;
        _userService = userService;
    }


    [HttpGet]
    public ViewResult List()
    {
        var items = _userAuditService.GetAll();

        var userAuditListItems = items.Select(p => new UserAuditListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            CreatedDate = p.CreatedDate,
            Email = p.Email,
            IsActive = p.IsActive,
            AuditLogType = p.AuditLogType,
            UserId = p.UserId,
        });

        var model = new UserAuditListViewModel
        {
            Items = userAuditListItems.ToList()
        };

        return View(model);
    }

    [HttpGet("ViewDetail/{id}")]
    public IActionResult ViewDetail(long id)
    {
        var userAudit = _userAuditService.GetById(id);
        if (userAudit == null)
        {
            return NotFound();
        }

        var vm = new UserAuditListItemViewModel
        {
            Id = userAudit.Id,
            Forename = userAudit.Forename,
            Surname = userAudit.Surname,
            CreatedDate = userAudit.CreatedDate,
            DateOfBirth = userAudit.DateOfBirth,
            Email = userAudit.Email,
            IsActive = userAudit.IsActive,
            UserId = userAudit.UserId,
            AuditLogType = userAudit.AuditLogType,
        };
        return View(vm);
    }



    [HttpGet("AuditByUser/{userId}")]
    public IActionResult AuditByUser(long userId)
    {
        var items = _userAuditService.GetByUserId(userId).OrderByDescending(o => o.CreatedDate);


        var userAuditListItems = items.Select(p => new UserAuditListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            CreatedDate = p.CreatedDate,
            IsActive = p.IsActive,
            AuditLogType = p.AuditLogType,
            UserId = p.UserId,
        });
        var model = new UserAuditListViewModel
        {
            Items = userAuditListItems.ToList()
        };

        return View(model);
    }

    [HttpGet("AuditByUser2/{userId}")]
    public IActionResult AuditByUser2(long userId)
    {
        var items = _userAuditService.GetByUserId(userId).OrderByDescending(o => o.CreatedDate);


        var userAuditListItems = items.Select(p => new UserAuditListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            CreatedDate = p.CreatedDate,
            IsActive = p.IsActive,
            AuditLogType = p.AuditLogType,
            UserId = p.UserId,
        });
        var model = new UserAuditListViewModel();
        var user = _userService.GetById(userId);
        if (user != null)
        {
            model.Items.Add(new UserAuditListItemViewModel
            {
                Email = user.Email,
                Forename = user.Forename,
                DateOfBirth = user.DateOfBirth,
                Surname = user.Surname,
                UserId = user.Id,
                IsActive = user.IsActive,

            });

        }
        model.Items.AddRange(userAuditListItems.ToList());

        return View(model);
    }

    [HttpGet("AuditByUser3/{userId}")]
    public IActionResult AuditByUser3(long userId)
    {
        var items = _userAuditService.GetByUserId(userId).OrderByDescending(o => o.CreatedDate);


        var userAuditListItems = items.Select(p => new UserAuditListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            CreatedDate = p.CreatedDate,
            IsActive = p.IsActive,
            AuditLogType = p.AuditLogType,
            UserId = p.UserId,
        });
        var model = new UserAuditListViewModel();
        var user = _userService.GetById(userId);
        if (user != null)
        {
            model.Items.Add(new UserAuditListItemViewModel
            {
                Email = user.Email,
                Forename = user.Forename,
                DateOfBirth= user.DateOfBirth,
                Surname= user.Surname,
                UserId = user.Id,
                IsActive= user.IsActive,
                
            });

        }
        model.Items.AddRange(userAuditListItems.ToList());


        return View(model);
    }


}
