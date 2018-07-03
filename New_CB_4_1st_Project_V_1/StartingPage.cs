using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace New_CB_4_1st_Project_V_1
{
     public class StartingPage
     {

        
        private static string userInCharge;
        


       
        

        
        static List<User> users = new List<User>();
        static List<Messages> messagesList = new List<Messages>();


        public StartingPage()
        {

            
            
        }





        public void setUpUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * from UserData", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User(reader["UserName"].ToString(), (int)reader["UserAccess"]));
                            }
                        }
                    }
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine($"Exception! {e.Message}");
                for (int i=0; i<e.Errors.Count; i++)
                {
                    Console.WriteLine($"{e.Errors[i]}");
                }
            }
            





                
        }



        public void setUpMessages()
        {



            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * from MessageData", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                messagesList.Add(new Messages((int)reader["Reference"], reader["Date"].ToString(), reader["Senter"].ToString(),reader["Receiver"].ToString(),reader["Title"].ToString(),reader["Message"].ToString() ));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Exception! {e.Message}");
                for (int i = 0; i < e.Errors.Count; i++)
                {
                    Console.WriteLine($"{e.Errors[i]}");
                }
            }


            //messagesList.Add(new Messages(0, DateTime.Now.ToLongDateString(), "exit", "exit", "", ""));
            //messagesList.Add(new Messages(1, DateTime.Now.ToLongDateString(), "exit", "exit", "", ""));
            //messagesList.Add(new Messages(2, DateTime.Now.ToLongDateString(), "anestis", "papi", "My message", "Bla bla bla...."));
            //messagesList.Add(new Messages(3, DateTime.Now.ToLongDateString(), "anestis", "papi", "My message", "talk talk talk...."));
            //messagesList.Add(new Messages(4, DateTime.Now.ToLongDateString(), "papi", "anestis", "Reverse Message", "Reverse Reverse Reverse...."));
            //messagesList.Add(new Messages(5, DateTime.Now.ToLongDateString(), "admin", "papi", "admin", "Admin Admin Admin Admin...."));
            //messagesList.Add(new Messages(6, DateTime.Now.ToLongDateString(), "admin", "anestis", "admin", "Admin Admin Admin Admin........"));
            //messagesList.Add(new Messages(7, DateTime.Now.ToLongDateString(), "anestis", "admin", "anestis", "anestis anestis anestis........"));
            ////messagesList.Add(new Messages(1, DateTime.Now, "", "", "", ""));
            ////messagesList.Add(new Messages(2, DateTime.Now, "", "", "", ""));
        }


        
        public void SignUp()
        {


            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Please enter a UserName or \"exit\" to exit ");
            Console.WriteLine("-For Securiry Reasons All UserNames must have Lower Case Letters-");
            Console.WriteLine("-Your Upper Case Letters will automaticly Transform to Lower Case-  ");
            string userDefinedUserName = Console.ReadLine();
            userDefinedUserName = userDefinedUserName.ToLower();

            for (int i = 0; i < users.Count && (!userDefinedUserName.Equals("exit", StringComparison.Ordinal)); i++)
            {
                
                while (userDefinedUserName.Equals(users[i].userDataList.tkey, StringComparison.Ordinal)) 
                {
                    Console.WriteLine($"UserName {userDefinedUserName} is used by another User or the System."); 
                    Console.WriteLine("please type a new UserName or type \"exit\" to exit to the menu.");
                    userDefinedUserName = Console.ReadLine();
                    userDefinedUserName = userDefinedUserName.ToLower();
                    i = 0;
           
                }
            }


            if (!userDefinedUserName.Equals("exit", StringComparison.Ordinal))
            {
                Console.WriteLine("Please enter UserPassword: ");
                string userDefinedPassWord = Console.ReadLine();


               




                int hashCodeSum = 0;
                int hashPasscode = userDefinedPassWord.GetHashCode();
                string stringHashPass = hashPasscode.ToString();
                char[] passArray = userDefinedPassWord.ToCharArray();

                foreach (var i in passArray)
                {
                    int hashCode = i.GetHashCode();
                    hashCodeSum = hashCodeSum + hashCode;
                }
                string stringCodeSum = hashCodeSum.ToString();

                try
                {
                    using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        conn.Open();
                        string insertCommand = "INSERT INTO UserData(UserName,UserPassword,UserSecondPassword,UserAccess) VALUES ( @name, @password, @secondPasword, @accessLevel)";
                        using (SqlCommand cmd = new SqlCommand(insertCommand, conn))
                        {
                            cmd.Parameters.Add(new SqlParameter("name", userDefinedUserName));
                            cmd.Parameters.Add(new SqlParameter("password", stringHashPass));
                            cmd.Parameters.Add(new SqlParameter("secondPasword", stringCodeSum));
                            cmd.Parameters.Add(new SqlParameter("accessLevel", 2));
                            cmd.ExecuteNonQuery();
                            Console.WriteLine($"User {userDefinedUserName} created.");
                            users.Add(new User(userDefinedUserName, 2));
                            
                            
                        }
                    }
                }

                catch (SqlException e)
                {
                    Console.WriteLine($"Exception! {e.Message}");
                    for (int i = 0; i < e.Errors.Count; i++)
                    {
                        Console.WriteLine($"{e.Errors[i]}");
                    }
                }

            }
            



        }



        public void SignIn()
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Please Enter your UserName: ");
            Console.WriteLine("-Lower Case Letters Only-");
            string userNameInput = Console.ReadLine();

            Console.WriteLine("Please Enter your PassWord: ");
            string userPassWordInput = Console.ReadLine();

            int hashCodeSum = 0;
            int hashPasscode = userPassWordInput.GetHashCode();
            string stringHashPass = hashPasscode.ToString();
            char[] passArray = userPassWordInput.ToCharArray();

            foreach (var i in passArray)
            {
                int hashCode = i.GetHashCode();
                hashCodeSum = hashCodeSum + hashCode;
            }
            string stringCodeSum = hashCodeSum.ToString();
            string dataHash="";
            string dataHashSecond="";

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM UserData WHERE UserName = @name", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("name", userNameInput));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataHash = reader["UserPassword"].ToString();
                                dataHashSecond = reader["UserSecondPassword"].ToString();
                            }
                        }
                    }
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine($"Exception! {e.Message}");
                for (int i = 0; i < e.Errors.Count; i++)
                {
                    Console.WriteLine($"{e.Errors[i]}");
                }
            }

            
            

                for (int i = 0; i < users.Count; i++)
                {
                    if (i == (users.Count - 1) && (!userNameInput.Equals(users[i].userDataList.tkey, StringComparison.Ordinal)
                       || !stringHashPass.Equals(dataHash, StringComparison.Ordinal) || !stringCodeSum.Equals(dataHashSecond,StringComparison.Ordinal)))

                    {

                        Console.WriteLine("Incorrect UserName or PassWord.");

                    }

                    else if (userNameInput.Equals(users[i].userDataList.tkey, StringComparison.Ordinal) && stringHashPass.Equals(dataHash, StringComparison.Ordinal) && stringCodeSum.Equals(dataHashSecond, StringComparison.Ordinal))
                    {
                        Console.WriteLine($"Welcome User {users[i].userDataList.tkey}.");
                        userInCharge = users[i].userDataList.tkey.ToString();




                        if (users[i].userDataList.taccess == 2)
                        {

                            users[i].MenuLoggedInBaseLevel();
                            i = users.Count;
                        }


                        else if (users[i].userDataList.taccess == 3)
                        {

                            users[i].MenuLoggedinLevelThree();
                            i = users.Count;
                        }


                        else if (users[i].userDataList.taccess == 4)
                        {

                            users[i].MenuLoggedinLevelFour();
                            i = users.Count;

                        }



                        else if (users[i].userDataList.taccess == 5)
                        {

                            users[i].MenuLoggedinLevelFive();
                            i = users.Count;
                        }



                        else if (users[i].userDataList.taccess == 9)
                        {
                            AdminMenu();
                            i = users.Count;


                        }

                    }




                }

        }




        public void AdminMenu()
        {


            int menuAccess;



            do
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Admin Menu-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 to Create a Message");
                Console.WriteLine("Choose 2 to Read or Delete Messages");
                Console.WriteLine("Choose 3 to Print Users");
                Console.WriteLine("Choose 4 to SignUp a User");
                Console.WriteLine("Choose 5 to Remove a user");
                Console.WriteLine("Choose 6 to Change user Privileges");
                Console.WriteLine("Choose 7 to Exit");

                string userMenuInput = Console.ReadLine();
                do
                {
                    while ((int.TryParse(userMenuInput, out menuAccess) == false))
                    {

                        Console.WriteLine("Incorrect Input, use a Number from 1 to 7 ");

                        userMenuInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuInput);
                    userMenuInput = "";
                } while (menuAccess <= 0 || menuAccess > 7);





                switch (menuAccess)
                {
                    case 1:
                        messageCreation();
                        break;
                    case 2:
                        ReadMessages();
                        break;
                    case 3:
                        PrintUsersAdmin();
                        break;
                    case 4:
                        SignUp();
                        break;
                    case 5:
                        RemoveUser();
                        break;
                    case 6:
                        GiveUserPrivilages();
                        break;
                                           
                       
                    default:
                        break;
                }
            } while (menuAccess != 7);


        }





        private void RemoveUser()
        {
            int i;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Ok Master! Enter UserName you wish to Terminate: ");
            string toTerminate = Console.ReadLine();
            for ( i = 0; i < users.Count && !toTerminate.Equals("exit", StringComparison.Ordinal)&&!toTerminate.Equals("admin",StringComparison.Ordinal); i++)
            {
                if (i == (users.Count-1 ) && (!toTerminate.Equals(users[i].userDataList.tkey, StringComparison.Ordinal)))

                {

                    Console.WriteLine($"There is no user {toTerminate}. ");
                    Console.WriteLine("Type in a new user or type in \"exit\" to exit");
                    toTerminate = Console.ReadLine();
                    i = 0;
                }

                

                

                if (toTerminate.Equals(users[i].userDataList.tkey, StringComparison.Ordinal) && !toTerminate.Equals("admin", StringComparison.Ordinal))
                {
                   // there may be a mistake, ask teacher
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM UserData WHERE UserName = @name", conn))
                            {
                                cmd.Parameters.Add(new SqlParameter("name", toTerminate));
                                cmd.ExecuteNonQuery();

                                Console.WriteLine($"User {users[i].userDataList.tkey} is past. ");
                                users.Remove(users[i]);
                                
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine($"Exception! {e.Message}");
                        for (int j = 0; j < e.Errors.Count; j++)
                        {
                            Console.WriteLine($"{e.Errors[j]}");
                        }
                    }

                   
                }

            }


            if (toTerminate.Equals("admin", StringComparison.Ordinal))
            {
                Console.WriteLine("You are the System's Slave, you can not be removed.");

            }
            

        }



        


       

        private void GiveUserPrivilages()
        {
            int i = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Enter a username to change Privileges: ");
            string userToChangeRole = Console.ReadLine();
            for (i = 0; i < users.Count && !userToChangeRole.Equals("exit", StringComparison.Ordinal) && !userToChangeRole.Equals("admin", StringComparison.Ordinal); i++)
            {
                if (i == (users.Count - 1) && (!userToChangeRole.Equals(users[i].userDataList.tkey, StringComparison.Ordinal)))

                {

                    Console.WriteLine($"There is no user {userToChangeRole}. ");
                    Console.WriteLine("Type in a new user or type in \"exit\" to exit");
                    userToChangeRole = Console.ReadLine();
                    i = 0;
                }



                if (userToChangeRole.Equals(users[i].userDataList.tkey, StringComparison.Ordinal) && !userToChangeRole.Equals("admin", StringComparison.Ordinal))
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("User will have no extra Privileges, enter: 2");
                    Console.WriteLine("User will be able to view transacted data between users,enter: 3 ");
                    Console.WriteLine("User will be able to View and Edit the transacted data between the users: 4 ");
                    Console.WriteLine("User will be able to View, Edit and Delete the transacted data between the users: 5 ");
                    Console.WriteLine($"Enter the apropriate number for the Role you wish {users[i].userDataList.tkey} to have. ");
                    string adminInputPrivileges = Console.ReadLine();
                    int inputPrivilege;

                    do
                    {
                        while ((int.TryParse(adminInputPrivileges, out inputPrivilege) == false))
                        {

                            Console.WriteLine("incorect input, use a number from 2 to 5: ");

                            adminInputPrivileges = Console.ReadLine();
                        }

                        inputPrivilege = int.Parse(adminInputPrivileges);
                        adminInputPrivileges = "";
                    } while (inputPrivilege <= 1 || inputPrivilege > 5);

                    if (!userToChangeRole.Equals("admin", StringComparison.Ordinal))
                    {
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand("UPDATE UserData set UserAccess = @role WHERE UserName = @name", conn))
                                {

                                    cmd.Parameters.Add(new SqlParameter("role", inputPrivilege));
                                    cmd.Parameters.Add(new SqlParameter("name", userToChangeRole));
                                    cmd.ExecuteNonQuery();

                                    users[i].userDataList.taccess = inputPrivilege;
                                    Console.WriteLine($"User {users[i].userDataList.tkey} Privilages Changed. ");
                                    userToChangeRole = "exit";

                                }
                            }
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine($"Exception! {e.Message}");
                            for (int j = 0; j < e.Errors.Count; j++)
                            {
                                Console.WriteLine($"{e.Errors[j]}");
                            }
                        }
                        

                    }
                    

                }

            }


            if (userToChangeRole.Equals("admin", StringComparison.Ordinal))
            {
                Console.WriteLine("You are the System's Slave, you can not Change your Privileges.");

            }


        }






        


        public void PrintUsersAdmin()
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"UserName:[-{users[i].userDataList.tkey}-], UserAccess [-{users[i].userDataList.taccess}-] ");
            }
        }









        public void PrintUsers()
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"UserName:[-{users[i].userDataList.tkey}-]");
            }
        }





        public void messageCreation()
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"-you are about to create a message {userInCharge}-");
            Console.WriteLine($"-Available Users are-");
            for (int i = 0; i < users.Count; i++)
            {
                Console.Write($" [{users[i].userDataList.tkey}],");
            }
            Console.WriteLine("Please type the UserName you wiss to send message to or type \"exit\" to exit");
            string toSendMessageInput = Console.ReadLine();

            for (int i = 0; i < users.Count && (!toSendMessageInput.Equals("exit", StringComparison.Ordinal))&& !toSendMessageInput.Equals(users[i].userDataList.tkey); i++)
            {
                while ((!toSendMessageInput.Equals(users[i].userDataList.tkey, StringComparison.Ordinal)) && i==users.Count-1 && !toSendMessageInput.Equals("exit",StringComparison.Ordinal))
                {
                    Console.WriteLine($"UserName {toSendMessageInput} is not in use.");
                    Console.WriteLine("please type a new UserName or type \"exit\" to exit to the menu.");
                    toSendMessageInput = Console.ReadLine();
                    i = 0;

                }
            }


            if (!toSendMessageInput.Equals("exit", StringComparison.Ordinal))
            {
                Console.WriteLine("-type a Title for your Message-");
                string titleMessageUserInput = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("-Now Type your Message-");
                string messageUserInput = Console.ReadLine();
                int timeadd=0;
                int i;
                
                for (i = 0; i < messagesList.Count /*&& i.Equals(messagesList[i].messageData.tadd)*/; i++)
                {
                    continue;
                }
               
                
                timeadd = i + 1;

                try
                {
                    using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO MessageData(Date,Senter,Receiver,Title,Message) VALUES(@date,@senter,@receiver,@title,@message)", conn))
                        {

                            cmd.Parameters.Add(new SqlParameter("date", DateTime.Now.ToLongDateString()));
                            cmd.Parameters.Add(new SqlParameter("senter", userInCharge));
                            cmd.Parameters.Add(new SqlParameter("receiver", toSendMessageInput));
                            cmd.Parameters.Add(new SqlParameter("title", titleMessageUserInput));
                            cmd.Parameters.Add(new SqlParameter("message", messageUserInput));
                            cmd.ExecuteNonQuery();
                            messagesList.Add(new Messages(timeadd, DateTime.Now.ToLongDateString(), userInCharge, toSendMessageInput, titleMessageUserInput, messageUserInput));
                            Console.WriteLine($"Message to [{toSendMessageInput}] created.");
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"Exception! {e.Message}");
                    for (int j = 0; j < e.Errors.Count; j++)
                    {
                        Console.WriteLine($"{e.Errors[j]}");
                    }
                }


               




               
            }


        }









        public void ReadMessages()
        {
            int menuAccess;
            do
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("(-Read Messages Menu-)");
                Console.WriteLine();
                Console.WriteLine("Choose 1 to Read Inbox Messages");
                Console.WriteLine("Choose 2 to Read Sented Messages");
                Console.WriteLine("Choose 3 to Delete an Inbox Message");
                Console.WriteLine("Choose 4 to Delete a Sented Message");
                Console.WriteLine("Choose 5 to Exit");


                string userMenuInput = Console.ReadLine();
                do
                {
                    while ((int.TryParse(userMenuInput, out menuAccess) == false))
                    {

                        Console.WriteLine("incorect input, use a number from 1 to 5 ");

                        userMenuInput = Console.ReadLine();
                    }

                    menuAccess = int.Parse(userMenuInput);
                    userMenuInput = "";
                } while (menuAccess <= 0 || menuAccess > 5);





                switch (menuAccess)
                {
                    case 1:
                        ReadInbox();
                        break;
                    case 2:
                        ReadSentedMessages();
                        break;
                    case 3:
                        InboxDeleteMessages();
                        break;
                    case 4:
                        OutboxDeleteMessages();
                        break;
                    

                    default:
                        break;
                }
            } while (menuAccess != 5);

        }







        public void ReadInbox()
        {
            
            int catchMaxtAdd=0;
            int k = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("-Available Messages-");
            Console.WriteLine();
            for (int i = 0; i < messagesList.Count; i++)
            {
                
                if (messagesList[i].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal))
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number [{messagesList[i].messageData.tadd}], date [{messagesList[i].messageData.tdate}]");
                    Console.WriteLine($"sent by user [{messagesList[i].messageData.tsenter}], Title [{messagesList[i].messageData.ttitle}]");
                    k++;
                    if (catchMaxtAdd<messagesList[i].messageData.tadd)
                    {
                        catchMaxtAdd = messagesList[i].messageData.tadd;
                    }
                }
              
            }

            if (k==0)
            {
                Console.WriteLine("You have No Messages,");
            }
            else
            {
                Console.WriteLine("Please type in the Reference Number of the Message you wish to Read");
                string userInputReference = Console.ReadLine();
                int inputReference=0;
                string exitIndicator = "";
                
                int l=0;
                do
                {
                    if (l>0)
                    {
                        userInputReference = "";
                    }

                    while (((int.TryParse(userInputReference, out inputReference) == false && !exitIndicator.Equals("exit", StringComparison.Ordinal) ) ||( userInputReference==""||(l == messagesList.Count) || inputReference > catchMaxtAdd)))
                    {

                        
                        Console.WriteLine("Incorrect Reference number try again or type \"exit\" to exit ");
                        userInputReference = Console.ReadLine();
                        

                        if (userInputReference.Equals("exit", StringComparison.Ordinal))
                        {
                            exitIndicator = "exit";
                            userInputReference = "-1";
                        }
                        
                        
                    }


                

                    inputReference = int.Parse(userInputReference);
                    for (l = 0; l < messagesList.Count && !userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) 
                        && (((inputReference != messagesList[l].messageData.tadd
                        || !messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal)) || (inputReference != messagesList[l].messageData.tadd
                        && !messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal)))); l++) 
                        
                    {
                        if (l == messagesList.Count - 1 && (!messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal) || messagesList[l].messageData.tadd != inputReference))
                        {
                            userInputReference = "";
                            l = 0;
                        }

                        if (l < messagesList.Count && messagesList[l].messageData.tadd == inputReference && !messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal))
                        {
                            userInputReference = "";
                        }
                    }


                } while (userInputReference.Equals("",StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && (inputReference != messagesList[l].messageData.tadd || !userInCharge.Equals(messagesList[l].messageData.treceiver,StringComparison.Ordinal )));


                if (messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && userInCharge == messagesList[l].messageData.treceiver)
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number[{messagesList[l].messageData.tadd}], date [{messagesList[l].messageData.tdate}]");
                    Console.WriteLine($"sent by user {messagesList[l].messageData.tsenter}, Title [{messagesList[l].messageData.ttitle}]");
                    Console.WriteLine($"{messagesList[l].messageData.tmessage}");
                }

               
            }
            



        }







        public void ReadSentedMessages()
        {
            int catchMaxtAdd = 0;
            int k = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("-Available Messages-");
            Console.WriteLine();
            for (int i = 0; i < messagesList.Count; i++)
            {

                if (messagesList[i].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal))
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number [{messagesList[i].messageData.tadd}], date [{messagesList[i].messageData.tdate}]");
                    Console.WriteLine($"sented to user [{messagesList[i].messageData.treceiver}], Title [{messagesList[i].messageData.ttitle}]");
                    k++;
                    if (catchMaxtAdd < messagesList[i].messageData.tadd)
                    {
                        catchMaxtAdd = messagesList[i].messageData.tadd;
                    }
                }

            }

            if (k == 0)
            {
                Console.WriteLine("You have sented No Messages,");
            }
            else
            {
                Console.WriteLine("Please type in the Reference Number of the Message you wish to Read");
                string userInputReference = Console.ReadLine();
                int inputReference = 0;
                string exitIndicator = "";
                
                int l = 0;
                do
                {
                    if (l > 0)
                    {
                        userInputReference = "";
                    }

                    while (((int.TryParse(userInputReference, out inputReference) == false && !exitIndicator.Equals("exit", StringComparison.Ordinal)) || (userInputReference == "" || (l == messagesList.Count) || inputReference > catchMaxtAdd)))
                    {


                        Console.WriteLine("Incorrect Reference number try again or type \"exit\" to exit ");
                        userInputReference = Console.ReadLine();


                        if (userInputReference.Equals("exit", StringComparison.Ordinal))
                        {
                            exitIndicator = "exit";
                            userInputReference = "-1";
                        }


                    }




                    inputReference = int.Parse(userInputReference);
                    for (l = 0; l < messagesList.Count && !userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal)
                        && (((inputReference != messagesList[l].messageData.tadd
                        || !messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal)) || (inputReference != messagesList[l].messageData.tadd
                        && !messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal)))); l++)

                    {
                        if (l == messagesList.Count - 1 && (!messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal) || messagesList[l].messageData.tadd != inputReference))
                        {
                            userInputReference = "";
                            l = 0;
                        }

                        if (l < messagesList.Count && messagesList[l].messageData.tadd == inputReference && !messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal))
                        {
                            userInputReference = "";
                        }
                    }


                } while (userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && (inputReference != messagesList[l].messageData.tadd || userInCharge != messagesList[l].messageData.tsenter));


                if (messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && userInCharge.Equals(messagesList[l].messageData.tsenter,StringComparison.Ordinal))
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number[{messagesList[l].messageData.tadd}], Date [{messagesList[l].messageData.tdate}]");
                    Console.WriteLine($"Sented to user {messagesList[l].messageData.treceiver}, Title [{messagesList[l].messageData.ttitle}]");
                    Console.WriteLine($"{messagesList[l].messageData.tmessage}");
                }


            }




        }








        public void InboxDeleteMessages()
        {

            

            int catchMaxtAdd = 0;
            int k = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("-Available Messages-");
            Console.WriteLine();
            for (int i = 0; i < messagesList.Count; i++)
            {

                if (messagesList[i].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal))
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number [{messagesList[i].messageData.tadd}], date [{messagesList[i].messageData.tdate}]");
                    Console.WriteLine($"sent by user [{messagesList[i].messageData.tsenter}], Title [{messagesList[i].messageData.ttitle}]");
                    k++;
                    if (catchMaxtAdd < messagesList[i].messageData.tadd)
                    {
                        catchMaxtAdd = messagesList[i].messageData.tadd;
                    }
                }

            }

            if (k == 0)
            {
                Console.WriteLine("You have No Messages,");
            }
            else
            {
                Console.WriteLine("Please type in the Reference Number of the Message you wish to Delete");
                string userInputReference = Console.ReadLine();
                int inputReference = 0;
                string exitIndicator = "";
                
                int l = 0;
                do
                {
                    if (l > 0)
                    {
                        userInputReference = "";
                    }

                    while (((int.TryParse(userInputReference, out inputReference) == false && !exitIndicator.Equals("exit", StringComparison.Ordinal)) || (userInputReference == "" || (l == messagesList.Count) || inputReference > catchMaxtAdd)))
                    {


                        Console.WriteLine("Incorrect Reference number try again or type \"exit\" to exit ");
                        userInputReference = Console.ReadLine();


                        if (userInputReference.Equals("exit", StringComparison.Ordinal))
                        {
                            exitIndicator = "exit";
                            userInputReference = "-1";
                        }


                    }




                    inputReference = int.Parse(userInputReference);
                    for (l = 0; l < messagesList.Count && !userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal)
                        && (((inputReference != messagesList[l].messageData.tadd
                        || !messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal)) || (inputReference != messagesList[l].messageData.tadd
                        && !messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal)))); l++)

                    {
                        if (l == messagesList.Count - 1 && (!messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal) || messagesList[l].messageData.tadd != inputReference))
                        {
                            userInputReference = "";
                            l = 0;
                        }

                        if (l < messagesList.Count && messagesList[l].messageData.tadd == inputReference && !messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal))
                        {
                            userInputReference = "";
                        }
                    }


                } while (userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && (inputReference != messagesList[l].messageData.tadd || !userInCharge.Equals(messagesList[l].messageData.treceiver, StringComparison.Ordinal)));


                if (messagesList[l].messageData.treceiver.Equals(userInCharge, StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && userInCharge == messagesList[l].messageData.treceiver)
                {

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("UPDATE MessageData set Receiver = @receiver WHERE Reference = @ref", conn))
                            {

                                cmd.Parameters.Add(new SqlParameter("receiver", messagesList[l].messageData.treceiver + "[Deleted]"));
                                cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                cmd.ExecuteNonQuery();

                                messagesList[l].messageData.treceiver = messagesList[l].messageData.treceiver + " [Deleted]";
                                Console.WriteLine($"Message Deleted, Reference Number:[-{messagesList[l].messageData.tadd}-]");

                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine($"Exception! {e.Message}");
                        for (int j = 0; j < e.Errors.Count; j++)
                        {
                            Console.WriteLine($"{e.Errors[j]}");
                        }
                    }


                    

                    


                }


            }




        }















        public void OutboxDeleteMessages()
        {
            int catchMaxtAdd = 0;
            int k = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("-Available Messages-");
            Console.WriteLine();
            for (int i = 0; i < messagesList.Count; i++)
            {

                if (messagesList[i].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal))
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number [{messagesList[i].messageData.tadd}], date [{messagesList[i].messageData.tdate}]");
                    Console.WriteLine($"sented to user [{messagesList[i].messageData.treceiver}], Title [{messagesList[i].messageData.ttitle}]");
                    k++;
                    if (catchMaxtAdd < messagesList[i].messageData.tadd)
                    {
                        catchMaxtAdd = messagesList[i].messageData.tadd;
                    }
                }

            }

            if (k == 0)
            {
                Console.WriteLine("You have sented No Messages,");
            }
            else
            {
                Console.WriteLine("Please type in the Reference Number of the Message you wish to Read");
                string userInputReference = Console.ReadLine();
                int inputReference = 0;
                string exitIndicator = "";

                int l = 0;
                do
                {
                    if (l > 0)
                    {
                        userInputReference = "";
                    }

                    while (((int.TryParse(userInputReference, out inputReference) == false && !exitIndicator.Equals("exit", StringComparison.Ordinal)) || (userInputReference == "" || (l == messagesList.Count) || inputReference > catchMaxtAdd)))
                    {


                        Console.WriteLine("Incorrect Reference number try again or type \"exit\" to exit ");
                        userInputReference = Console.ReadLine();


                        if (userInputReference.Equals("exit", StringComparison.Ordinal))
                        {
                            exitIndicator = "exit";
                            userInputReference = "-1";
                        }


                    }




                    inputReference = int.Parse(userInputReference);
                    for (l = 0; l < messagesList.Count && !userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal)
                        && (((inputReference != messagesList[l].messageData.tadd
                        || !messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal)) || (inputReference != messagesList[l].messageData.tadd
                        && !messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal)))); l++)

                    {
                        if (l == messagesList.Count - 1 && (!messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal) || messagesList[l].messageData.tadd != inputReference))
                        {
                            userInputReference = "";
                            l = 0;
                        }

                        if (l < messagesList.Count && messagesList[l].messageData.tadd == inputReference && !messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal))
                        {
                            userInputReference = "";
                        }
                    }


                } while (userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && (inputReference != messagesList[l].messageData.tadd || userInCharge != messagesList[l].messageData.tsenter));


                if (messagesList[l].messageData.tsenter.Equals(userInCharge, StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && userInCharge.Equals(messagesList[l].messageData.tsenter, StringComparison.Ordinal))
                {


                    try
                    {
                        using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("UPDATE MessageData set Senter = @senter WHERE Reference = @ref", conn))
                            {

                                cmd.Parameters.Add(new SqlParameter("senter", messagesList[l].messageData.tsenter + "[Deleted]"));
                                cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                cmd.ExecuteNonQuery();
                                messagesList[l].messageData.tsenter = messagesList[l].messageData.tsenter + "[Deleted]";
                                Console.WriteLine($"Message Deleted, Reference Number:[-{messagesList[l].messageData.tadd}-]");



                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine($"Exception! {e.Message}");
                        for (int j = 0; j < e.Errors.Count; j++)
                        {
                            Console.WriteLine($"{e.Errors[j]}");
                        }
                    }






                    
                   



                }


            }
        }







        public void Level3AccsessFeature()
        {
            int catchMaxtAdd = 0;
            int k = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("-Available Messages-");
            Console.WriteLine();
            for (int i = 2; i < messagesList.Count; i++)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine($"Reference Number [{messagesList[i].messageData.tadd}], Date [{messagesList[i].messageData.tdate}]");
                Console.WriteLine($"Sent by user [{messagesList[i].messageData.tsenter}], Title [{messagesList[i].messageData.ttitle}]");
                Console.WriteLine($"WAS Sented To [{messagesList[i].messageData.treceiver}]");
                k++;
                if (catchMaxtAdd < messagesList[i].messageData.tadd)
                {
                    catchMaxtAdd = messagesList[i].messageData.tadd;
                }
                

            }

            if (k == 0)
            {
                Console.WriteLine("You have No Messages,");
            }
            else
            {
                Console.WriteLine("Please type in the Reference Number of the Message you wish to Read");
                string userInputReference = Console.ReadLine();
                int inputReference = 0;
                string exitIndicator = "";

                int l = 0;
                do
                {
                    if (l > 0)
                    {
                        userInputReference = "";
                    }

                    while (((int.TryParse(userInputReference, out inputReference) == false && !exitIndicator.Equals("exit", StringComparison.Ordinal)) || (userInputReference == "" || (l == messagesList.Count) || inputReference > catchMaxtAdd)))
                    {


                        Console.WriteLine("Incorrect Reference number try again or type \"exit\" to exit ");
                        userInputReference = Console.ReadLine();


                        if (userInputReference.Equals("exit", StringComparison.Ordinal))
                        {
                            exitIndicator = "exit";
                            userInputReference = "-1";
                        }


                    }




                    inputReference = int.Parse(userInputReference);
                    for (l = 0; l < messagesList.Count && !userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal)
                        && inputReference != messagesList[l].messageData.tadd ; l++)

                    {
                        if (l == messagesList.Count - 1 && ( messagesList[l].messageData.tadd != inputReference))
                        {
                            userInputReference = "";
                            l = 0;
                        }

                        if (l < messagesList.Count && messagesList[l].messageData.tadd == inputReference )
                        {
                            userInputReference = "";
                        }
                    }


                } while (userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && inputReference != messagesList[l].messageData.tadd );


                if ( !exitIndicator.Equals("exit", StringComparison.Ordinal) && inputReference == messagesList[l].messageData.tadd)
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number[{messagesList[l].messageData.tadd}], date [{messagesList[l].messageData.tdate}]");
                    Console.WriteLine($"sent by user [{messagesList[l].messageData.tsenter}], Title [{messagesList[l].messageData.ttitle}]");
                    Console.WriteLine($"WAS Sented To [{messagesList[l].messageData.treceiver}]");
                    Console.WriteLine($"{messagesList[l].messageData.tmessage}");
                }


            }

        }






       
        public void Level4AccsessFeature()
        {
            int catchMaxtAdd = 0;
            int k = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("-Available Messages-");
            Console.WriteLine();
            for (int i = 2; i < messagesList.Count; i++)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine($"Reference Number [{messagesList[i].messageData.tadd}], Date [{messagesList[i].messageData.tdate}]");
                Console.WriteLine($"Was Sent by User [{messagesList[i].messageData.tsenter}], Title [{messagesList[i].messageData.ttitle}]");
                Console.WriteLine($"Sented To User [{messagesList[i].messageData.treceiver}]");
                k++;
                if (catchMaxtAdd < messagesList[i].messageData.tadd)
                {
                    catchMaxtAdd = messagesList[i].messageData.tadd;
                }


            }

            if (k == 0)
            {
                Console.WriteLine("There are no Messages,");
            }
            else
            {
                Console.WriteLine("Please type in the Reference Number of the Message you wish to Read");
                string userInputReference = Console.ReadLine();
                int inputReference = 0;
                string exitIndicator = "";

                int l = 0;
                do
                {
                    if (l > 0)
                    {
                        userInputReference = "";
                    }

                    while (((int.TryParse(userInputReference, out inputReference) == false && !exitIndicator.Equals("exit", StringComparison.Ordinal)) || (userInputReference == "" || (l == messagesList.Count) || inputReference > catchMaxtAdd)))
                    {


                        Console.WriteLine("Incorrect Reference number try again or type \"exit\" to exit ");
                        userInputReference = Console.ReadLine();


                        if (userInputReference.Equals("exit", StringComparison.Ordinal))
                        {
                            exitIndicator = "exit";
                            userInputReference = "-1";
                        }


                    }




                    inputReference = int.Parse(userInputReference);
                    for (l = 0; l < messagesList.Count && !userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal)
                        && inputReference != messagesList[l].messageData.tadd; l++)

                    {
                        if (l == messagesList.Count - 1 && (messagesList[l].messageData.tadd != inputReference))
                        {
                            userInputReference = "";
                            l = 0;
                        }

                        if (l < messagesList.Count && messagesList[l].messageData.tadd == inputReference)
                        {
                            userInputReference = "";
                        }
                    }


                } while (userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && inputReference != messagesList[l].messageData.tadd);


                if (!exitIndicator.Equals("exit", StringComparison.Ordinal) && inputReference == messagesList[l].messageData.tadd)
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Reference Number[{messagesList[l].messageData.tadd}], date [{messagesList[l].messageData.tdate}]");
                    Console.WriteLine($"Was sent by user {messagesList[l].messageData.tsenter}, Title [{messagesList[l].messageData.ttitle}]");
                    Console.WriteLine($"Sented To User [{messagesList[l].messageData.treceiver}]");
                    Console.WriteLine($"{messagesList[l].messageData.tmessage}");


                    int menuAccess;



                    do
                    {

                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine("(-Edit Message Menu-)");
                        Console.WriteLine();
                        Console.WriteLine("Choose 1 to Edit Senter's UserName");
                        Console.WriteLine("Choose 2 to Edit Receiver's UserName");
                        Console.WriteLine("Choose 3 to Change Message Date to Today");
                        Console.WriteLine("Choose 4 to Edit Message Title");
                        Console.WriteLine("Choose 5 to Edit Message");
                        Console.WriteLine("Choose 6 to Exit");

                        string userMenuInput = Console.ReadLine();
                        do
                        {
                            while ((int.TryParse(userMenuInput, out menuAccess) == false))
                            {

                                Console.WriteLine("Incorrect Input, use a Number from 1 to 6 ");

                                userMenuInput = Console.ReadLine();
                            }

                            menuAccess = int.Parse(userMenuInput);
                            userMenuInput = "";
                        } while (menuAccess <= 0 || menuAccess > 6);





                        switch (menuAccess)
                        {
                            case 1:

                                Console.WriteLine("---------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine($"Edit [{messagesList[l].messageData.tsenter}]");
                                Console.WriteLine("Choose a new Senter's UserName ");
                                string newUserNameSenter = Console.ReadLine();
                                try
                                {
                                    using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                                    {
                                        conn.Open();
                                        using (SqlCommand cmd = new SqlCommand("UPDATE MessageData set Senter = @senter WHERE Reference = @ref", conn))
                                        {

                                            cmd.Parameters.Add(new SqlParameter("senter", newUserNameSenter));
                                            cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                            cmd.ExecuteNonQuery();
                                            messagesList[l].messageData.tsenter = newUserNameSenter;
                                            Console.WriteLine($"Message Reference Number:[-{messagesList[l].messageData.tadd}-] Edited");



                                        }
                                    }
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine($"Exception! {e.Message}");
                                    for (int j = 0; j < e.Errors.Count; j++)
                                    {
                                        Console.WriteLine($"{e.Errors[j]}");
                                    }
                                }

                               
                                break;
                            case 2:
                                Console.WriteLine("---------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine($"Edit [-{messagesList[l].messageData.treceiver}-]");
                                Console.WriteLine("Choose a new Receivers's UserName ");
                                string newUserNameReceiver = Console.ReadLine();
                                try
                                {
                                    using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                                    {
                                        conn.Open();
                                        using (SqlCommand cmd = new SqlCommand("UPDATE MessageData set Receiver = @receiver WHERE Reference = @ref", conn))
                                        {

                                            cmd.Parameters.Add(new SqlParameter("receiver", newUserNameReceiver));
                                            cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                            cmd.ExecuteNonQuery();
                                            messagesList[l].messageData.treceiver = newUserNameReceiver;
                                            Console.WriteLine($"Message Reference Number:[-{messagesList[l].messageData.tadd}-] Edited");



                                        }
                                    }
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine($"Exception! {e.Message}");
                                    for (int j = 0; j < e.Errors.Count; j++)
                                    {
                                        Console.WriteLine($"{e.Errors[j]}");
                                    }
                                }

                                break;
                            case 3:
                                Console.WriteLine("---------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine($"Edit [-{messagesList[l].messageData.tdate}-]");
                                
                                string userDateEdit = DateTime.Now.ToLongDateString();
                                try
                                {
                                    using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                                    {
                                        conn.Open();
                                        using (SqlCommand cmd = new SqlCommand("UPDATE MessageData set Date = @date WHERE Reference = @ref", conn))
                                        {

                                            cmd.Parameters.Add(new SqlParameter("receiver", userDateEdit));
                                            cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                            cmd.ExecuteNonQuery();
                                            messagesList[l].messageData.tdate = userDateEdit;
                                            Console.WriteLine($"Message Reference Number:[-{messagesList[l].messageData.tadd}-] Edited");



                                        }
                                    }
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine($"Exception! {e.Message}");
                                    for (int j = 0; j < e.Errors.Count; j++)
                                    {
                                        Console.WriteLine($"{e.Errors[j]}");
                                    }
                                }
                                
                                break;
                            case 4:
                                Console.WriteLine("---------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine($"Edit [-{messagesList[l].messageData.ttitle}-]");
                                Console.WriteLine("Choose a new Title");
                                string newTitle = Console.ReadLine();
                                try
                                {
                                    using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                                    {
                                        conn.Open();
                                        using (SqlCommand cmd = new SqlCommand("UPDATE MessageData set Title = @title WHERE Reference = @ref", conn))
                                        {

                                            cmd.Parameters.Add(new SqlParameter("title", newTitle));
                                            cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                            cmd.ExecuteNonQuery();
                                            messagesList[l].messageData.ttitle = newTitle;
                                            Console.WriteLine($"Message Reference Number:[-{messagesList[l].messageData.tadd}-] Edited");



                                        }
                                    }
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine($"Exception! {e.Message}");
                                    for (int j = 0; j < e.Errors.Count; j++)
                                    {
                                        Console.WriteLine($"{e.Errors[j]}");
                                    }
                                }
                                
                                break;
                            case 5:
                                Console.WriteLine("---------------------------------------------------------------------");
                                Console.WriteLine();
                                Console.WriteLine($"Edit [-{messagesList[l].messageData.tmessage}-]");
                                Console.WriteLine("Choose a new Message");
                                string newMessage = Console.ReadLine();
                                try
                                {
                                    using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                                    {
                                        conn.Open();
                                        using (SqlCommand cmd = new SqlCommand("UPDATE MessageData set Message = @message WHERE Reference = @ref", conn))
                                        {

                                            cmd.Parameters.Add(new SqlParameter("title", newMessage));
                                            cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                            cmd.ExecuteNonQuery();
                                            messagesList[l].messageData.tmessage = newMessage;
                                            Console.WriteLine($"Message Reference Number:[-{messagesList[l].messageData.tadd}-] Edited");



                                        }
                                    }
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine($"Exception! {e.Message}");
                                    for (int j = 0; j < e.Errors.Count; j++)
                                    {
                                        Console.WriteLine($"{e.Errors[j]}");
                                    }
                                }
                                
                                break;


                            default:
                                break;
                        }
                    } while (menuAccess != 6);




                    




                }

               


            }



        }





        






        public void Level5AccsessFeature()
        {
            int catchMaxtAdd = 0;
            int k = 0;
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("-Available Messages-");
            Console.WriteLine();
            for (int i = 2; i < messagesList.Count; i++)
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine($"Reference Number [{messagesList[i].messageData.tadd}], Date [{messagesList[i].messageData.tdate}]");
                Console.WriteLine($"Was Sent by user [{messagesList[i].messageData.tsenter}], Title [{messagesList[i].messageData.ttitle}]");
                Console.WriteLine($"Sented To User [{messagesList[i].messageData.treceiver}]");
                k++;
                if (catchMaxtAdd < messagesList[i].messageData.tadd)
                {
                    catchMaxtAdd = messagesList[i].messageData.tadd;
                }


            }

            if (k == 0)
            {
                Console.WriteLine("There are No Messages,");
            }
            else
            {
                Console.WriteLine("Please type in the Reference Number of the Message you wish to Delete");
                string userInputReference = Console.ReadLine();
                int inputReference = 0;
                string exitIndicator = "";

                int l = 0;
                do
                {
                    if (l > 0)
                    {
                        userInputReference = "";
                    }

                    while (((int.TryParse(userInputReference, out inputReference) == false && !exitIndicator.Equals("exit", StringComparison.Ordinal)) || (userInputReference == "" || (l == messagesList.Count) || inputReference > catchMaxtAdd)))
                    {


                        Console.WriteLine("Incorrect Reference number try again or type \"exit\" to exit ");
                        userInputReference = Console.ReadLine();


                        if (userInputReference.Equals("exit", StringComparison.Ordinal))
                        {
                            exitIndicator = "exit";
                            userInputReference = "-1";
                        }


                    }




                    inputReference = int.Parse(userInputReference);
                    for (l = 0; l < messagesList.Count && !userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal)
                        && inputReference != messagesList[l].messageData.tadd; l++)

                    {
                        if (l == messagesList.Count - 1 && (messagesList[l].messageData.tadd != inputReference))
                        {
                            userInputReference = "";
                            l = 0;
                        }

                        if (l < messagesList.Count && messagesList[l].messageData.tadd == inputReference)
                        {
                            userInputReference = "";
                        }
                    }


                } while (userInputReference.Equals("", StringComparison.Ordinal) && !exitIndicator.Equals("exit", StringComparison.Ordinal) && inputReference != messagesList[l].messageData.tadd);


                if (!exitIndicator.Equals("exit", StringComparison.Ordinal) && inputReference == messagesList[l].messageData.tadd && inputReference>1 )
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-VMG3RJP1\SQLEXPRESS;Initial Catalog=CB_4_First_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("DELETE FROM MessageData WHERE Reference = @ref", conn))
                            {

                                
                                cmd.Parameters.Add(new SqlParameter("ref", inputReference));
                                cmd.ExecuteNonQuery();

                                Console.WriteLine($"Message Deleted! Reference Number [-{messagesList[l].messageData.tadd}-]");
                                messagesList.Remove(messagesList[l]);



                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine($"Exception! {e.Message}");
                        for (int j = 0; j < e.Errors.Count; j++)
                        {
                            Console.WriteLine($"{e.Errors[j]}");
                        }
                    }
                   
                    

                }

                if (inputReference < 2)
                {
                    Console.WriteLine("This Message is Used by the System and can not be Erased");
                }
            }
        }







     }











     
}
