namespace UserManagement.Web.Models.Users;
using System.ComponentModel.DataAnnotations;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Forename is required")]
    public required string Forename { get; set; }
    [Required(ErrorMessage = "Surname is required")]
    public required string Surname { get; set; }
    public required string Email { get; set; }    
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}
