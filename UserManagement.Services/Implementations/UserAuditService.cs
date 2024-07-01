
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserAuditService : IUserAuditService
{
    private readonly IDataContext _dataAccess;
    public UserAuditService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<UserAudit> GetAll() => _dataAccess.GetAll<UserAudit>();

    public UserAudit? GetById(long id)
    {
        var result = _dataAccess.GetAll<UserAudit>().FirstOrDefault(u => u.Id == id);
        if (result == null)
            return null;

        return (UserAudit?) result;
    }

    public IEnumerable<UserAudit> GetByUserId(long userId)
    {
        var result = _dataAccess.GetAll<UserAudit>().Where(u => u.UserId == userId);
        return result;
    }

    public UserAudit Save(UserAudit userAudit)
    {
        userAudit.CreatedDate = DateTime.Now;
        _dataAccess.Create<UserAudit>(userAudit);
        return userAudit;
    }
}
