using System;
using UserManagement.Data.Common;

namespace UserManagement.Web.Models.UserAudit;

public class UserAuditListViewModel
{
    public List<UserAuditListItemViewModel> Items { get; set; } = new List<UserAuditListItemViewModel>();
}

public class UserAuditListItemViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public   string Forename { get; set; } = default!;
    public   string Surname { get; set; } = default!;
    public   string Email { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }

    public AuditLogType AuditLogType { get; set; }
} 
