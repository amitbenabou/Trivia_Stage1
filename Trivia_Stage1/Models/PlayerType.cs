using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("PlayerType")]
public partial class PlayerType
{
    [Key]
    public int TypeId { get; set; }

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
