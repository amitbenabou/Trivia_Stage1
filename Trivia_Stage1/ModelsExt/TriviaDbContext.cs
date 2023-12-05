using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDbContext : DbContext
{
    const int PLAYER_ROOKIE = 3;
    const int PLAYER_MANAGER = 1;
    const int PLAYER_MASTER = 2;

    const int Qstatus_Approved = 1;
    const int Qstatus_Waiting = 2;
    const int Qstatus_failed = 3;
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
    public Question AddNewQuestion(Question Q)
    {
        Question question = new Question
        {
            SubjectId = questionSubject, Text = questionText, CorrectAnswer = correctAnswer, WrongAnswer1 = wrongAnswer1,
            WrongAnswer2 = wrongAnswer2, WrongAnswer3 = wrongAnswer3, StatusId = Qstatus_Waiting, PlayerId = playerID 
            
        };
        this.Questions.Add(question);
        this.SaveChanges();

        return question;
    }
    public Player showLogIn(string email, string pass)
    {
        Player p = this.Players.Where(e => e.PlayerMail == email && e.Password == pass).FirstOrDefault();
        if (p == null)
        {
            throw new Exception("email and password do not exist in DB");
        }
        else
        {
            return p;
        }

    }
}
