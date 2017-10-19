// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SampleEphReceiver
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Azure.EventHubs.Processor;

    public class Program
    {
        private const string EhConnectionString = "Endpoint=sb://smartshelfevents.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Yjc6nskl2ptqBF/w0keE/GiZrPShRMX2MpsCGPiNFlw=";
        private const string EhEntityPath = "staffshopshelves";
        private const string StorageContainerName = "staffshop";
        private const string StorageAccountName = "smartshelfstorage";
        private const string StorageAccountKey = "4yx8WyDeEu0mjOrDh4HoM5+HimR47YVcoHvM1BVQCWO6Tej6+nLeSx21vHG1Z16aPw38+c0qmoJQ0FlbgOExFA==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EhEntityPath,
                PartitionReceiver.DefaultConsumerGroupName,
                EhConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
