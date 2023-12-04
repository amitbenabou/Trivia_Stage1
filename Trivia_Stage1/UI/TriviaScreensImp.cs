using System;
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
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
            return true;
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
            this.currentQuestion = null;
            if (this.currentPlayer.PlayerScore <= 100 || this.currentPlayer.TypeId != 1)
            {
                Console.WriteLine("you cant add question");
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                c = Console.ReadKey(true).KeyChar;
            }
            else
            {
                while (c != 'B' && c != 'b')
                {
                    CleareAndTtile("add question");
                    Console.Write("Please Type your question subject ");
                    int QuestionSubject = int.Parse(Console.ReadLine());
                    while (QuestionSubject < 1)
                    {
                        Console.Write("Question length cant be under 1,Please Type your question subject again ");
                        QuestionSubject = int.Parse(Console.ReadLine());
                    }
                    Console.Write("Please Type your question");
                    string? QuestionText = Console.ReadLine();
                    while (QuestionText.Length < 5)
                    {
                        Console.Write("Question length cant be under 5,Please Type your question again ");
                        QuestionText = Console.ReadLine();
                    }
                    Console.Write("Please Type a correct answer ");
                    string? correctAnswer = Console.ReadLine();
                    while (correctAnswer.Length < 5)
                    {
                        Console.Write("correct Answer cant be under 5,Please Type your correct Answer again ");
                        correctAnswer = Console.ReadLine();
                    }
                    Console.Write("Please Type 3 wrong answer ");
                    string? wrongAnswer1 = Console.ReadLine();
                    while (wrongAnswer1.Length < 5)
                    {
                        Console.Write("wrong Answer cant be under 5,Please Type your wrong Answer again ");
                        wrongAnswer1 = Console.ReadLine();
                    }
                    string? wrongAnswer2 = Console.ReadLine();
                    while (wrongAnswer2.Length < 5)
                    {
                        Console.Write("wrong Answer cant be under 5,Please Type your wrong Answer again ");
                        wrongAnswer2 = Console.ReadLine();
                    }
                    string? wrongAnswer3 = Console.ReadLine();
                    while (wrongAnswer3.Length < 5)
                    {
                        Console.Write("wrong Answer cant be under 5,Please Type your wrong Answer again ");
                        wrongAnswer3 = Console.ReadLine();
                    }

                    try
                    {
                        TriviaDbContext db = new TriviaDbContext();
                        this.currentQuestion = db.AddNewQuestion(QuestionSubject, QuestionText, correctAnswer,wrongAnswer1,wrongAnswer2,wrongAnswer3);
                        Console.WriteLine("well done, question is now pending");
                        successQ = true;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to add question!");
                        successQ = false;
                    }
                }
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                c = Console.ReadKey(true).KeyChar;
               
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
