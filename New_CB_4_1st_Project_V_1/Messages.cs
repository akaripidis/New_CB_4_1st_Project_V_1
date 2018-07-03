using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_CB_4_1st_Project_V_1
{
    class Messages
    {
        public MessageDataList<int,string, string, string,string, string> messageData = new MessageDataList<int, string  , string, string,string, string>(0,"","","","","" );




        public Messages(int tadd, string dateTime, string senter,string receiver,string title,string message)
        {

            messageData.tadd = tadd;
            messageData.tdate = dateTime;
            messageData.tsenter = senter;
            messageData.treceiver = receiver;
            messageData.ttitle = title;
            messageData.tmessage = message;
            
        }
    }
}