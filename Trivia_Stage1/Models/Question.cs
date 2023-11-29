using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Question
{
    [Key]
    public int QuestionId { get; set; }

    public int StatusId { get; set; }

    public int PlayerId { get; set; }

    public int SubjectId { get; set; }

    [Column("correctAnswer")]
    [StringLength(256)]
    public string CorrectAnswer { get; set; } = null!;

    [Column("wrongAnswer1")]
    [StringLength(256)]
    public string WrongAnswer1 { get; set; } = null!;

    [Column("wrongAnswer2")]
    [StringLength(256)]
    public string WrongAnswer2 { get; set; } = null!;

    [Column("wrongAnswer3")]
    [StringLength(256)]
    public string WrongAnswer3 { get; set; } = null!;

    [StringLength(256)]
    public string Text { get; set; } = null!;

    [ForeignKey("PlayerId")]
    [InverseProperty("Questions")]
    public virtual Player Player { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Questions")]
    public virtual QuestionStatus Status { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("Questions")]
    public virtual QuestionSubject Subject { get; set; } = null!;
}
