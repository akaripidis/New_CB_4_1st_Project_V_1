using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_CB_4_1st_Project_V_1
{
    class User : AdvanceMenu 
    {

        


        
        public UserDataList<string, int> userDataList = new UserDataList<string,  int>("",  0);





        public User(string userName,  int userAccess)
        {
            userDataList.tkey = userName;
            
            userDataList.taccess = userAccess;

        }



               


    }
}
