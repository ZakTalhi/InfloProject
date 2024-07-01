
using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserAuditService _userAuditService;
    public UsersController(IUserService userService, IUserAuditService userAuditService)
    {
        _userService = userService;
        _userAuditService = userAuditService;
    }


    [HttpGet]
    public ViewResult List(bool? isActive = null)
    {
        var items = _userService.GetAll();

        if (isActive.HasValue)
        {
            items = items.Where(p => p.IsActive == isActive);
        }

        var userItems = items.Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = userItems.ToList()
        };

        return View(model);
    }

    [HttpGet("ViewDetail/{id}")]
    public IActionResult ViewDetail(long id)
    {
        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        _userAuditService.Save(new Models.UserAudit
        {
            AuditLogType = Data.Common.AuditLogType.View,
            DateOfBirth = user.DateOfBirth, 
            Email = user.Email,
            Forename= user.Forename,
            IsActive= user.IsActive,
            UserId = user.Id,
            Surname = user.Surname
        });


        var vm = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            IsActive = user.IsActive
        };
        return View(vm);
    }

    [HttpGet("Add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost("Add")]
    public IActionResult Add(UserListItemViewModel model)
    {
        if (ModelState.IsValid)
        {
            _userService.Upsert(new Models.User
            {
                Id = model.Id,
                Email = model.Email,
                IsActive = model.IsActive,
                DateOfBirth = model.DateOfBirth,
                Forename = model.Forename,
                Surname = model.Surname,
            });
            return RedirectToAction("Index"); // or wherever you want to redirect after saving
        }

        return View(model); // Return the same view with validation messages
    }

    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        Models.User? user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        _userAuditService.Save(new Models.UserAudit
        {
            AuditLogType = Data.Common.AuditLogType. GetForUpdate,
            DateOfBirth = user.DateOfBirth, 
            Email = user.Email,
            Forename = user.Forename,
            IsActive = user.IsActive,
            UserId = user.Id,
            Surname = user.Surname
        });

        var vm = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            IsActive = user.IsActive
        };
         

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_EditPartial", vm); // Assuming the partial view is named "_EditPartial.cshtml"
        }

        return View(vm);
    }

    [HttpPost("Edit/{id}")]
    public IActionResult Edit(long id, UserListItemViewModel model)
    {
        if (ModelState.IsValid)
        {
            _userService.Upsert(new Models.User
            {
                Id = model.Id, Email =model.Email, IsActive = model.IsActive,
                DateOfBirth=model.DateOfBirth,Forename = model.Forename,
                Surname = model.Surname,
            });
            return RedirectToAction("Index"); // or wherever you want to redirect after saving
        }

        return View(model); // Return the same view with validation messages
    }

    [HttpGet("Delete")]
    public IActionResult Delete(long id)
    { 
        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        _userAuditService.Save(new Models.UserAudit
        {
            AuditLogType = Data.Common.AuditLogType.GetForDelete,
            DateOfBirth = user.DateOfBirth, 
            Email = user.Email,
            Forename = user.Forename,
            IsActive = user.IsActive,
            UserId = user.Id,
            Surname = user.Surname
        });


        var vm = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            IsActive = user.IsActive
        };

        return View(vm);
    }

     
    [HttpPost("Delete")]
    public IActionResult DeleteConfirmed(long id)
    { 
        var user = _userService.GetById(id);

        if (user != null)
        {
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
            return RedirectToAction("Index"); // or wherever you want to redirect after deleting
        }

        return RedirectToAction("Delete", new { id = id });
    }
}
