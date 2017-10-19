using OBID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.FEIGNotificationSample
{
    public class NotificaitonTaskListener : FedmTaskListener
    {
        #region Private Attributes

        private FedmIscReader _reader;

        #endregion

        #region Constructors

        public NotificaitonTaskListener(FedmIscReader reader)
        {
            _reader = reader;
        }
        #endregion

        //Executes when a new notificaiton is recieved from the reader.
        public void OnNewNotification(int iError, string ip, uint portNr)
        {
            //Get Array of BRM Items from BRM Table in reader object
            //  FedmBrmTableItem[] tagReads = (_reader.GetTable(FedmIscReaderConst.BRM_TABLE) as FedmBrmTableItem[]);

            FedmBrmTableItem[] tagReads;
            tagReads = (FedmBrmTableItem[])_reader.GetTable(FedmIscReaderConst.BRM_TABLE);

            if (tagReads != null && tagReads.Length > 0)
            {
                Console.WriteLine($"------------------------");
                Console.WriteLine($"New Notifiaiton Recieved");


                foreach (FedmBrmTableItem item in tagReads)
                {
                    Console.WriteLine($"Tag ID: {item.GetUid()}");
                    //Check if Time Information is present
                    if (item.isTimer)
                    {
                        FeIscReaderTime timer = item.GetReaderTime();

                        int ms = (timer.MilliSecond % 1000);
                        int second = (timer.MilliSecond - ms) / 1000;

                        Console.WriteLine($"Time: {timer.Year}-{timer.Month}-{timer.Day}T{timer.Hour}:{timer.Minute}:{second}:{ms}");
                    }

                    Console.Write($"Antenna Values: ");

                    //Check if RSSI Data is available (this includes the antenna port number on the MUX
                    if (item.isRSSI)
                    {
                        foreach (var rssi in item.GetRSSI())
                        {
                            Console.Write($"{rssi.Value.antennaNumber} ");
                        }

                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine($"End of Notification");
                    Console.WriteLine($"-------------------");
                    Console.WriteLine();

                }
            }
        }











        #region Unused Method Implementations from FedmTaskListener
        public void OnNewApduResponse(int iError)
        {
            throw new NotImplementedException();
        }



        public void onNewPeopleCounterEvent(uint counter1, uint counter2, uint counter3, uint counter4, string ip, uint portNr, uint busAddress)
        {
            throw new NotImplementedException();
        }

        public void OnNewQueueResponse(int iError)
        {
            throw new NotImplementedException();
        }

        public void OnNewReaderDiagnostic(int iError, string ip, uint portNr)
        {
            throw new NotImplementedException();
        }

        public void OnNewSAMResponse(int iError, byte[] responseData)
        {
            throw new NotImplementedException();
        }

        public void OnNewTag(int iError)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
