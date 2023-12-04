using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDbContext : DbContext
{
    const int PLAYER_ROOKIE = 3;
    const int PLAYER_MANAGER = 1;
    const int PLAYER_MASTER = 2;
    
    public Player AddSignUpUser(string email,  string password,  string name)
    {
        Player player = new Player
        {
           PlayerMail = email,
            Password = password,
            PlayerName = name,
            PlayerScore = 0,
            TypeId = PLAYER_ROOKIE
            
       };
        this.Players.Add(player);
        this.SaveChanges();

        return player;
    }
    public Question AddNewQuestion(int questionSubject, string questionText, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
    {
        Question question = new Question
        {
            SubjectId = questionSubject, Text = questionText,CorrectAnswer = correctAnswer,WrongAnswer1 = wrongAnswer1,
            WrongAnswer2 = wrongAnswer2,WrongAnswer3 = wrongAnswer3

        };
        this.Questions.Add(question);
        this.SaveChanges();

        return question;
    }
}
