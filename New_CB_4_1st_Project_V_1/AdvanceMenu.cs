using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_CB_4_1st_Project_V_1
{
    class AdvanceMenu : Menu 
    {

        

        





        private int menuAccess;
        string userMenuLoggedInInput;





        public void MenuLoggedInBaseLevel()
        {

            do
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Logged In Menu-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 for message/Data menu.");
                Console.WriteLine("Choose 2 for data menu.");
                Console.WriteLine("Choose 3 to Print Users.");
                Console.WriteLine("Choose 4 to Exit.");
                userMenuLoggedInInput = Console.ReadLine();

                do
                {
                    while ((int.TryParse(userMenuLoggedInInput, out menuAccess) == false))
                    {

                        Console.WriteLine("incorect input, use a number from 1 to 4: ");

                        userMenuLoggedInInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuLoggedInInput);
                    userMenuLoggedInInput = "";
                } while (menuAccess <= 0 || menuAccess > 4);




                switch (menuAccess)
                {
                    case 1:
                       basicMessageMenu();
                        break;
                    case 2:
                        Console.WriteLine("Under Construction");
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








        public void MenuLoggedinLevelThree()
        {


            do
            {

                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Logged In Menu Access Level 3-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 for message / Data menu (Basic).") ;
                Console.WriteLine("Choose 2 to Print Users.");
                Console.WriteLine("Choose 3 For -<Level 3>- Access (Messages).");
                Console.WriteLine("Choose 4 to Exit.");
                userMenuLoggedInInput = Console.ReadLine();

                do
                {
                    while ((int.TryParse(userMenuLoggedInInput, out menuAccess) == false))
                    {

                        Console.WriteLine("incorect input, use a number from 1 to 4: ");

                        userMenuLoggedInInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuLoggedInInput);
                    userMenuLoggedInInput = "";
                } while (menuAccess <= 0 || menuAccess > 4);




                switch (menuAccess)
                {
                    case 1:
                        basicMessageMenu();
                        break;
                    case 2:
                        PrintUsers();
                        break;
                    case 3:
                        Level3AccsessFeature();
                        break;

                    default:
                        break;
                }
            } while (menuAccess != 4);

        }








        public void MenuLoggedinLevelFour()
        {

            do
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Logged In Menu Access Level 4-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 to Message/Data Menu.");
                Console.WriteLine("Choose 2 to Print Users.");
                Console.WriteLine("Choose 3 to Level 3 Access (Message).");
                Console.WriteLine("Choose 4 to Level 4 Access (Message).");
                Console.WriteLine("Choose 5 to Exit.");
                userMenuLoggedInInput = Console.ReadLine();

                do
                {
                    while ((int.TryParse(userMenuLoggedInInput, out menuAccess) == false))
                    {

                        Console.WriteLine("incorect input, use a number from 1 to 5: ");

                        userMenuLoggedInInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuLoggedInInput);
                    userMenuLoggedInInput = "";
                } while (menuAccess <= 0 || menuAccess > 5);




                switch (menuAccess)
                {
                    case 1:
                        basicMessageMenu();
                        break;
                    case 2:
                        PrintUsers();
                        break;
                    case 3:
                        Level3AccsessFeature();
                        break;
                    case 4:
                        Level4AccsessFeature();
                        break;

                    default:
                        Console.WriteLine("exit");
                        
                        break;
                }
            } while (menuAccess != 5);



        }




        public void MenuLoggedinLevelFive()
        {


            do
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Logged In Menu Access Level 5-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 to Message/Data Menu.");
                Console.WriteLine("Choose 2 to Message/Data Menu.");
                Console.WriteLine("Choose 3 to Level 3 Access (Message).");
                Console.WriteLine("Choose 4 to Level 4 Access (Message).");
                Console.WriteLine("Choose 5 to Level 5 Access (Message).");
                Console.WriteLine("Choose 6 to Exit.");
                userMenuLoggedInInput = Console.ReadLine();

                do
                {
                    while ((int.TryParse(userMenuLoggedInInput, out menuAccess) == false))
                    {

                        Console.WriteLine("incorect input, use a number from 1 to 6: ");

                        userMenuLoggedInInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuLoggedInInput);
                    userMenuLoggedInInput = "";
                } while (menuAccess <= 0 || menuAccess > 6);




                switch (menuAccess)
                {
                    case 1:
                        basicMessageMenu();
                        break;
                    case 2:
                        PrintUsers();
                        break;
                    case 3:
                        Level3AccsessFeature();
                        break;
                    case 4:
                        Level4AccsessFeature();
                        break;
                    case 5:
                        Level5AccsessFeature();
                        break;

                    default:
                        Console.WriteLine("exit");
                        
                        break;
                }
            } while (menuAccess != 6);



        }



        
        











    }
}
