using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trivia_Stage1.Models;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using System.Numerics;

namespace Trivia_Stage1.UI
{
    public enum QUESTION_STATUSES 
    {
        PENDING = 2,
        APPROVED = 1,
        REJECTED = 3
    }
    public class TriviaScreensImp:ITriviaScreens
    {

        
        private Player? currentPlayer;
        private Question? currentQuestion;
        private Random r = new Random();
        
        public bool ShowLogin()
        {
            char c = ' ';
            bool successLogin = true;
            this.currentPlayer = null;
            while (c != 'B' && c != 'b' && this.currentPlayer == null)
            {
                CleareAndTtile("Log In");
                Console.WriteLine("please enter your profile email");
                string? email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }
                Console.Write("Please enter your profile password: ");
                string? password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }

                Console.WriteLine("Connecting to Server...");
                
                try
                {
                    TriviaDbContext db = new TriviaDbContext();
                    this.currentPlayer = db.showLogIn(email, password);
                    Console.WriteLine("well done");
                    successLogin = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to log in! Email might not exist in DB!");
                    successLogin = false;
                }

                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                c = Console.ReadKey(true).KeyChar;
            }
            return successLogin;
        }
        public bool ShowSignup()
        {
            
            this.currentPlayer = null;

            
            char c = ' ';
            bool success = true;
            while (c != 'B' && c != 'b' && this.currentPlayer == null)
            {
               
                CleareAndTtile("Signup");

                Console.Write("Please Type your email: ");
                string? email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }

                Console.Write("Please Type your password: ");
                string? password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");
                string? name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.Write("name must be at least 3 characters! Please try again: ");
                    name = Console.ReadLine();
                }


                Console.WriteLine("Connecting to Server...");
                
                try
                {
                    TriviaDbContext db = new TriviaDbContext();
                    this.currentPlayer = db.AddSignUpUser(email, password, name);
                    Console.WriteLine("well done");
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                    success = false;
                }



                
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                
                c = Console.ReadKey(true).KeyChar;
            }
           
            return (success);
        }
        private int ShowSubjects()
        {
            TriviaDbContext db = new TriviaDbContext();
            List<QuestionSubject> subjects = db.Getubjects();
            int chosen = -1;
            string id = "";
            while (chosen < subjects[0].SubjectId || chosen > subjects[subjects.Count - 1].SubjectId)
            {
                CleareAndTtile("Add Question");
                foreach (QuestionSubject s in subjects)
                {
                    Console.WriteLine($"{s.SubjectId} - {s.Name}");
                }

                Console.WriteLine("Choose Subject:");
                id = Console.ReadLine();

                int.TryParse(id, out chosen);
            }

            return chosen;

        }
        public void ShowAddQuestion()
        {
            char c = ' ';
            bool successQ = true;
            Question Q = new Question();
            Q.StatusId = (int)QUESTION_STATUSES.PENDING;
            Q.PlayerId = this.currentPlayer.PlayerId;
            
            if (this.currentPlayer.PlayerScore <= 100 && this.currentPlayer.TypeId != 1)
            {
                Console.WriteLine("you cant add question");
                Console.WriteLine("Press (B)ack to go back or any other key to add question again...");
                c = Console.ReadKey(true).KeyChar;
            }
            else
            {
                while (c != 'B' && c != 'b')
                {
                    CleareAndTtile("add question");
                    
                    Q.SubjectId = ShowSubjects();
                    Console.Write("Please Type your question ");
                    Q.Text = Console.ReadLine();
                    while (Q.Text.Length < 1)
                    {
                        Console.Write("Question length cant be under 1,Please Type your question again ");
                        Q.Text = Console.ReadLine();
                    }
                    Console.Write("Please Type a correct answer ");
                    Q.CorrectAnswer = Console.ReadLine();
                    while (Q.CorrectAnswer.Length < 1)
                    {
                        Console.Write("correct Answer cant be under 1,Please Type your correct Answer again ");
                        Q.CorrectAnswer = Console.ReadLine();
                    }
                    Console.Write("Please Type 3 wrong answer ");
                    Q.WrongAnswer1 = Console.ReadLine();
                    while (Q.WrongAnswer1.Length < 1)
                    {
                        Console.Write("wrong Answer cant be under 1,Please Type your wrong Answer again ");
                        Q.WrongAnswer1 = Console.ReadLine();
                    }
                    Q.WrongAnswer2 = Console.ReadLine();
                    while (Q.WrongAnswer2.Length < 1)
                    {
                        Console.Write("wrong Answer cant be under 1,Please Type your wrong Answer again ");
                        Q.WrongAnswer2 = Console.ReadLine();
                    }
                    Q.WrongAnswer3 = Console.ReadLine();
                    while (Q.WrongAnswer3.Length < 1)
                    {
                        Console.Write("wrong Answer cant be under 1,Please Type your wrong Answer again ");
                        Q.WrongAnswer3 = Console.ReadLine();
                    }

                    
                    try
                    {
                        TriviaDbContext db = new TriviaDbContext();
                        db.AddNewQuestion(Q);
                        Console.WriteLine("well done, question is now waiting to be approved/rejected ");
                        successQ = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to add question!");
                        successQ = false;
                    }
                    Console.WriteLine("Press (B)ack to go back or any other key to add question again");
                    c = Console.ReadKey(true).KeyChar;

                }

            }

        }

        public void ShowPendingQuestions()
        {
            CleareAndTtile("Pending Questions");
            TriviaDbContext db = new TriviaDbContext();
            char c = ' ';
            List<Question> q = db.showPending();
            int cnt1 = 0;
            int cnt2 = 0;
            foreach (Question question in q)
            {
                cnt1++;
            }
            if(currentPlayer.TypeId == 1 || currentPlayer.TypeId == 2)
            {
                if(cnt1== 0)
                {
                    Console.WriteLine("no more pending questions");
                }
                else
                {
                    foreach(Question question in q)
                    {
                        while(c!='B' && c!='b')
                        {
                            Console.WriteLine("the question: " + question.Text);
                            Console.WriteLine("the subject of the question: " + question.SubjectId);
                            Console.WriteLine(  "worng answers: " + question.WrongAnswer1);
                            Console.WriteLine(question.WrongAnswer2);
                            Console.WriteLine(question.WrongAnswer3);
                            Console.WriteLine("correct asnwer: " + question.CorrectAnswer);
                            Console.WriteLine("press (A)pprove the question \n press (R) to reject \n press (S) to skip questions \n (B)ack ");
                            c = Console.ReadKey(true).KeyChar;
                            if(c == 'A' || c == 'a')
                            {
                                try
                                {
                                    db.pendingToApproved(question);
                                    Console.WriteLine("the question is approved");
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine("failed to approve");
                                }
                            }
                            if(c=='R' || c=='r')
                            {
                                try
                                {
                                    Console.WriteLine("the question is rejeted");
                                    db.pendingToRejected(question);

                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine("failed to reject");
                                }
                            }
                            if (c=='s'||c=='S')
                            {

                            }
                            cnt2--;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("only players at level master or manager can change questions' status");
            }
            Console.WriteLine("press (B)ack to go back to menu");
            c = Console.ReadKey(true).KeyChar;
        }



        public void ShowGame()
        {
            CleareAndTtile("Game");
            TriviaDbContext db = new TriviaDbContext();
            char c = ' ';
            bool success = false;
            List<Question> questions = db.getAprrovedQ();
            string answer = " ";
            int cnt1 = 0;
            int cnt2 = 0;
            foreach(Question question in questions)
            {
                cnt1++;
            }
            while (c!='B' && c!='b' && cnt2<cnt1)
            {
                foreach(Question question in questions)
                {
                    Console.WriteLine(question.Text);
                    Console.WriteLine(question.WrongAnswer1);
                    Console.WriteLine(question.WrongAnswer2);
                    Console.WriteLine(question.WrongAnswer3);
                    Console.WriteLine(question.CorrectAnswer);
                    Console.WriteLine("type your answer");
                    answer=Console.ReadLine();
                    if(answer == question.CorrectAnswer)
                    {
                        currentPlayer.PlayerScore += 10;
                        Console.WriteLine("well done!");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("you chose the wrong answer");
                        success = false;
                    }
                    cnt2++;
                }
                Console.WriteLine("press (B)ack to go back to menu");
                c = Console.ReadKey(true).KeyChar;
            }
            
        }
        public void ShowProfile()
        {
            char c = ' ';
            CleareAndTtile("Profile");
            TriviaDbContext db= new TriviaDbContext();
            while (c!= 'B'&& c!='b')
            {
                bool Updated = false;
                if (this.currentPlayer == null)
                {
                    Console.WriteLine(" you need to login first");
                    Console.ReadKey(true);
                    return;
                }
                Console.WriteLine($"Name:\t{currentPlayer.PlayerName}");
                Console.WriteLine($"Mail:\t{currentPlayer.PlayerMail}");
                Console.WriteLine($"password:\t{currentPlayer.Password}");
                Console.WriteLine($"TypePlayer:\t{currentPlayer.TypeId.ToString()}");
                Console.WriteLine($"PlayerID:\t{currentPlayer.PlayerId.ToString()}");
                Console.WriteLine($"Player score:\t{currentPlayer.PlayerScore}");
                Console.WriteLine(" Press (M) To Update The Mail Press (N) To Update The Name Press (P) To Update The Password");
                c = Console.ReadKey(true).KeyChar;
              
                if (c == 'M'||c=='m')
                {
                    Console.Write("Please Type your new  email: ");
                    string email = Console.ReadLine();
                    while (!IsEmailValid(email))
                    {
                        Console.Write("Bad Email Format! Please try again:");
                        email = Console.ReadLine();
                    }
                    this.currentPlayer.PlayerMail = email;
                    Updated = true;         

                }
                if (c == 'N' || c == 'n')
                {

                    Console.Write("Please Type your  NEW Name: ");
                    string name = Console.ReadLine();
                    while (!IsNameValid(name))
                    {
                        Console.Write("name must be at least 3 characters! Please try again: ");
                        name = Console.ReadLine();
                    }
                    this.currentPlayer.PlayerName = name;
                    Updated = true;

                }
                if (c == 'P'|| c=='p')
                {
                    Console.Write("Please Type your new  password: ");
                    string password = Console.ReadLine();
                    while (!IsPasswordValid(password))
                    {
                        Console.Write("password must be at least 4 characters! Please try again: ");
                        password = Console.ReadLine();
                    }

                    this.currentPlayer.Password = password;
                    Updated=true;
                   
                }
                try
                {
                    
                    Console.WriteLine("your changes succseeded");
                }
                catch (Exception e)
                {
                    Console.WriteLine("failed changes try again");
                }
                Console.WriteLine("Press (B)ack to go back to menu");
                c = Console.ReadKey(true).KeyChar;
                Console.Clear();    
            }

            
        }

        
        private void CleareAndTtile(string title)
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\t\t{title}");
            Console.WriteLine();
        }

        private bool IsEmailValid(string emailAddress)
        {
            var pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        private bool IsPasswordValid(string password)
        {
            return password != null && password.Length >= 3;
        }

        private bool IsNameValid(string name)
        {
            return name != null && name.Length >= 3;
        }

        private int DisplayQuestion(Question q)
        {
            
            int correct = r.Next(1, 5);
            Console.WriteLine(q.Text);
            switch (correct)
            {
                case 1:
                    Console.WriteLine("1:  "+ q.CorrectAnswer);
                    Console.WriteLine("2:  "  +q.WrongAnswer1);
                    Console.WriteLine("3:  "  + q.WrongAnswer2);
                    Console.WriteLine("4:  "  + q.WrongAnswer3);
                    break;
                case 2:
                    Console.WriteLine("1:  " + q.WrongAnswer1);
                    Console.WriteLine("2:  " + q.CorrectAnswer);
                    Console.WriteLine("3:  " + q.WrongAnswer2);
                    Console.WriteLine("4:  " + q.WrongAnswer3);
                    break;
                case 3:
                    Console.WriteLine("1:  " +q.WrongAnswer1);
                    Console.WriteLine("2:  " + q.WrongAnswer2);
                    Console.WriteLine("3:  " + q.CorrectAnswer);
                    Console.WriteLine("4:  " + q.WrongAnswer3);
                    
                    break;
                case 4:
                    Console.WriteLine("1:  " + q.WrongAnswer1);
                    Console.WriteLine("2:  " + q.WrongAnswer2);
                    Console.WriteLine("3:  " + q.WrongAnswer3);
                    Console.WriteLine("4:  " + q.CorrectAnswer);
                   
                    break;
            }

            return correct;
        }
    }
}
