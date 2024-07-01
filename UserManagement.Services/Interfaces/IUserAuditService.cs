using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserAuditService 
{
    IEnumerable<UserAudit> GetAll();
    UserAudit? GetById(long id);
    IEnumerable<UserAudit> GetByUserId(long userId);
    UserAudit Save(UserAudit userAudit);
}
