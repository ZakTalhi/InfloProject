using System;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    public required string Forename { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}
