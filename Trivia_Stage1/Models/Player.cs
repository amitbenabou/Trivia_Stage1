using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("Player")]
[Index("PlayerMail", Name = "UQ__Player__CD6856E268F3F47C", IsUnique = true)]
public partial class Player
{
    [Key]
    public int PlayerId { get; set; }

    [StringLength(20)]
    public string PlayerName { get; set; } = null!;

    [StringLength(20)]
    public string PlayerMail { get; set; } = null!;

    [StringLength(20)]
    public string Password { get; set; } = null!;

    [Column("typeID")]
    public int TypeId { get; set; }

    public int PlayerScore { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [ForeignKey("TypeId")]
    [InverseProperty("Players")]
    public virtual PlayerType Type { get; set; } = null!;
}
