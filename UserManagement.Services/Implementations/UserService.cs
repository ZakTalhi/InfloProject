using System;
using System.Collections.Generic;
using System.Linq;

using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IUserAuditService _userAuditService;
    private readonly IDataContext _dataAccess; 


    public UserService(IDataContext dataAccess, IUserAuditService userAuditService) 
    {
        _dataAccess = dataAccess;
        _userAuditService = userAuditService; 
    }

  
    public bool Delete(User user)
    {
        try
        {
            _dataAccess.Delete(user);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public User? GetById(long id)
    {
        // Implement the logic to retrieve a user by id
        // For example:
        var result = _dataAccess.GetAll<User>().FirstOrDefault(u => u.Id == id);
        if (result == null)
            return null;

        return (User?)result;
    }

    public User Upsert(User user)
    {
        var existingUser = GetById(user.Id);
        if (existingUser == null)
        {
            _dataAccess.Create<User>(user);
            return user;
        }

        _userAuditService.Save(new UserAudit
        {
            AuditLogType = Data.Common.AuditLogType.Update,
            UserId = existingUser.Id, 
            DateOfBirth = existingUser.DateOfBirth,
            Email = existingUser.Email,
            Forename = existingUser.Forename,
            IsActive = existingUser.IsActive,
            Surname = existingUser.Surname,
        });

        existingUser.Surname = user.Surname;
        existingUser.Email = user.Email;
        existingUser.IsActive = user.IsActive;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.Forename = user.Forename;
        _dataAccess.Update<User>(existingUser);
        return user;
    }
}
