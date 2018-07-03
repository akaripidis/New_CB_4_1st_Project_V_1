using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_CB_4_1st_Project_V_1
{
    class Program 
    {
        static void Main(string[] args)
        {

            //MainApplication mainApplication = new MainApplication();


            StartingPage startingPage  = new StartingPage();
            startingPage.setUpUsers();
            startingPage.setUpMessages();
            Menu menu = new Menu();
            menu.userMenu();


            //StartingPage startingPage = new StartingPage();

            //Console.WriteLine(startingPage.MaxUsers);
            //startingPage.SignUp();
            //Console.WriteLine(startingPage.MaxUsers);
            //startingPage.SignIn();

            //startingPage.PrintUsers();
            //Console.WriteLine(startingPage.MaxUsers);

            Console.ReadKey();
        }



    }
}
