using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_CB_4_1st_Project_V_1
{
    class UserDataList<Tkey, Taccess>
    {
        public  Tkey tkey;
        public Taccess taccess;



        public UserDataList(Tkey tkey, Taccess taccess)
        {
            this.tkey = tkey;
            
            this.taccess = taccess;
        }





    }








    class MessageDataList<Tadd,Tdate,Tsenter,Treceiver,Ttitle,Tmessage>
    {
        public Tadd tadd;
        public Tdate tdate;
        public Tsenter tsenter;
        public Treceiver treceiver;
        public Ttitle ttitle;
        public Tmessage tmessage;
        


        public MessageDataList(Tadd tadd,Tdate tdate,Tsenter tsenter, Treceiver treceiver,Ttitle ttitle, Tmessage tmessage )
        {
            this.tadd = tadd;
            this.tdate = tdate;
            this.tsenter = tsenter;
            this.treceiver = treceiver;
            this.ttitle = ttitle;
            this.tmessage = tmessage;
        }




    }



}
