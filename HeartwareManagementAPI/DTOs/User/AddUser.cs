using System;

namespace HeartwareManagementAPI.DTOs.User;
public class AddUser
{
    public Guid UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }


    public DateTime? DateOfBirth { get; set; }

    public int? Gender { get; set; }

    public int? RoleId { get; set; }


}
