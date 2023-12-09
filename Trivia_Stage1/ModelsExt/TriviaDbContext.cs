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
    public Question AddNewQuestion(Question q)
    {
       
        this.Questions.Add(q);
        this.SaveChanges();

        return q;
    }
    public Player showLogIn(string email, string pass)
    {
        Player? p = this.Players.Where(e => e.PlayerMail == email && e.Password == pass).FirstOrDefault();
        if (p == null)
        {
            throw new Exception("email and password do not exist in DB");
        }
        else
        {
            return p;
        }

    }
    public List<QuestionSubject> Getubjects()
    {
        return  this.QuestionSubjects.ToList();
    }

    
    public void  UpdatePlayer(Player p)
    {
        Entry(p).State=EntityState.Modified;
        SaveChanges();
    }
     public List < Question> GetQuestions()
     {
      
        return this.Questions.ToList ();
     }

    public List<Question> getPendingQ()
    {
        List<Question> pending = this.Questions.Where(q => q.StatusId == 2).ToList();
        return pending;
    }

    public List<Question> getAprrovedQ()
    {
        List<Question> approved = this.Questions.Where(q => q.StatusId == 1).ToList();
        return approved;
    }
    public void updateStatusQ(Question q)
    {
        Entry(q).State = EntityState.Modified;
        this.SaveChanges();
    }
}
