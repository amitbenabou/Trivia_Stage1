﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {

        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        private Player currentPlayer;
        private Question currentQuestion;

        //Implememnt interface here
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
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            this.currentPlayer = null;

            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
            char c = ' ';
            bool success = true;
            while (c != 'B' && c != 'b' && this.currentPlayer == null)
            {
                //Clear screen
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
                //Create instance of Business Logic and call the signup method
                // *For example:
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



                //Provide a proper message for example:
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                //Get another input from user
                c = Console.ReadKey(true).KeyChar;
            }
            //return true if signup suceeded!
            return (success);
        }

        public void ShowAddQuestion()
        {
            char c = ' ';
            bool successQ = true;
            Question Q = new Question();
            
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
                    Console.Write("Please chose your question subject: History (type 1), Sports (type 2), Politics(type 3), Ramon(type 4),Science (type 5)  ");
                    int QuestionSubject = int.Parse(Console.ReadLine());
                    while (QuestionSubject < 1)
                    {
                        Console.Write("Question length cant be under 1,Please Type your question subject again ");
                        QuestionSubject = int.Parse(Console.ReadLine());
                    }
                    Console.Write("Please Type your question ");
                    string? QuestionText = Console.ReadLine();
                    while (QuestionText.Length < 1)
                    {
                        Console.Write("Question length cant be under 1,Please Type your question again ");
                        QuestionText = Console.ReadLine();
                    }
                    Console.Write("Please Type a correct answer ");
                    string? correctAnswer = Console.ReadLine();
                    while (correctAnswer.Length < 1)
                    {
                        Console.Write("correct Answer cant be under 1,Please Type your correct Answer again ");
                        correctAnswer = Console.ReadLine();
                    }
                    Console.Write("Please Type 3 wrong answer ");
                    string? wrongAnswer1 = Console.ReadLine();
                    while (wrongAnswer1.Length < 1)
                    {
                        Console.Write("wrong Answer cant be under 1,Please Type your wrong Answer again ");
                        wrongAnswer1 = Console.ReadLine();
                    }
                    string? wrongAnswer2 = Console.ReadLine();
                    while (wrongAnswer2.Length < 1)
                    {
                        Console.Write("wrong Answer cant be under 1,Please Type your wrong Answer again ");
                        wrongAnswer2 = Console.ReadLine();
                    }
                    string? wrongAnswer3 = Console.ReadLine();
                    while (wrongAnswer3.Length < 1)
                    {
                        Console.Write("wrong Answer cant be under 1,Please Type your wrong Answer again ");
                        wrongAnswer3 = Console.ReadLine();
                    }

                    Console.WriteLine("Connecting to Server...");
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
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowProfile()
        {
            char c = ' ';
            CleareAndTtile("Profile");
            TriviaDbContext db= new TriviaDbContext();  
            if(this.currentPlayer == null ) 
            {
                Console.WriteLine(" you need to login first");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine($"Name:\t{currentPlayer.PlayerName}");
            Console.WriteLine($"Mail:\t{currentPlayer.PlayerMail}");
            Console.WriteLine($"password:\t{currentPlayer.Password}");
            Console.WriteLine($"TypePlayer:\t{currentPlayer.PlayerId}");
            Console.WriteLine($"Player score:\t{currentPlayer.PlayerScore}");
            Console.WriteLine(" Press (M) To Update The Mail");
            c = Console.ReadKey(true).KeyChar;
            Console.WriteLine(" Press (N) To Update The Name");
            c = Console.ReadKey(true).KeyChar;
            Console.WriteLine(" Press (P) To Update The Password");
            c = Console.ReadKey(true).KeyChar;
            if (c == 'M')
            {
                Console.Write("Please Type your new  email: ");
                string email  = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }
                this.currentPlayer.PlayerMail = email;
                db.Add(email);
                db.SaveChanges();

            }
            if (c == 'N')
            {

                Console.Write("Please Type your Name: ");
                string name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.Write("name must be at least 3 characters! Please try again: ");
                    name = Console.ReadLine();
                }
                this.currentPlayer.PlayerName = name;
                db.Add(name);
                db.SaveChanges();
            }
            if (c == 'P')
            {
                Console.Write("Please Type your new  password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }
               
                this.currentPlayer.Password = password;
                db.Add(password);
                
                db.SaveChanges();
            }


            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }

        //Private helper methodfs down here...
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

    }
}
