using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_CB_4_1st_Project_V_1
{
    class Menu :StartingPage
    {


        static List<Messages> messagesList = new List<Messages>();

        


        private int menuAccess;


        public void userMenu()
        {

            do
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Starting Menu-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 to SignUp.");
                Console.WriteLine("Choose 2 to SignIn.");
                Console.WriteLine("Choose 3 to Print Users.");
                Console.WriteLine("Choose 4 to Exit.");

               string userMenuInput = Console.ReadLine();
                do
                {
                    while ((int.TryParse(userMenuInput, out menuAccess) == false))
                    {

                        Console.WriteLine("incorect input, use a number from 1 to 4: ");

                        userMenuInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuInput);
                    userMenuInput = "";                   
                } while (menuAccess<=0||menuAccess>3);
                  

                        
                    

                    switch (menuAccess)
                    {
                        case 1:
                            SignUp();
                            break;
                        case 2:
                            SignIn();
                            break;
                        case 3:
                            PrintUsers();
                            break;

                        default:
                            Console.WriteLine("exit");

                            break;
                    }
            } while (menuAccess != 4);


        }



        public void basicMessageMenu ()
        {

            do
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Message Menu-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 to Send a Message.");
                Console.WriteLine("Choose 2 to Read or Delete a Message.");
                Console.WriteLine("Choose 3 to Print Users.");
                Console.WriteLine("Choose 4 to Exit.");

                string userMenuInput = Console.ReadLine();
                do
                {
                    while ((int.TryParse(userMenuInput, out menuAccess) == false))
                    {

                        Console.WriteLine("incorect input, use a number from 1 to 4: ");

                        userMenuInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuInput);
                    userMenuInput = "";
                } while (menuAccess <= 0 || menuAccess > 4);





                switch (menuAccess)
                {
                    case 1:
                        messageCreation();
                        break;
                    case 2:
                        ReadMessages();
                        break;
                    case 3:
                        PrintUsers();
                        break;

                    default:
                        Console.WriteLine("exit");

                        break;
                }
            } while (menuAccess != 4);



        }



        public void extraMessageMenu()
        {
                
           
               

                
                switch (menuAccess)
                {
                    case 1:
                        Level3AccsessFeature();
                        break;
                    case 2:
                        Level4AccsessFeature();
                        break;
                    case 3:
                        Level5AccsessFeature();                       
                        break;
                    case 4:
                       
                        break;
                        

                    default:
                        Console.WriteLine("exit");

                        break;
                }
           



        }



    }
}
