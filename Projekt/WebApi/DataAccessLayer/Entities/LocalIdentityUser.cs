using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;
public class LocalIdentityUser : IdentityUser
{
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual UserEntity? User { get; set; }
}
