using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("QuestionSubject")]
public partial class QuestionSubject
{
    [Key]
    public int SubjectId { get; set; }

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
