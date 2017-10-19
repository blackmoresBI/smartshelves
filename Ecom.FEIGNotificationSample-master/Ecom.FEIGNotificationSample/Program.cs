using OBID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.FEIGNotificationSample
{
    class Program
    {

        private static FedmIscReader _reader;

        static void Main(string[] args)
        {

            _reader = new FedmIscReader();

            //Initilize internal table size (default is 0, without initilization, there is no table to place data coming in)
            _reader.SetTableSize(FedmIscReaderConst.BRM_TABLE, 255);


            //Create a new FedmTaskListener object, this Interface implements the OnNewNotificaiton() method, which will fire when the reader sends through a new notificaiton
            FedmTaskListener listener = new NotificaitonTaskListener(_reader);

            //Set up FedmTaskOption, this is where you configure the port to listen to, as well as the option to reply with an acknoledgement
            FedmTaskOption taskOpt = new FedmTaskOption()
            {
                IPPort = 10005,
                NotifyWithAck = 0 // 0 = Off, 1 = Off
            };

            Console.WriteLine("Begin Listening");
            Console.WriteLine();

            //Initialize notificaiotn listening, by starting a new background task
            _reader.StartAsyncTask(FedmTaskOption.ID_NOTIFICATION,listener,taskOpt);


            listener.OnNewNotification(1, "172.20.43.67", 10005);



            Console.ReadKey();
        }

    }
}
