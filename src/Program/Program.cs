using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using System.Threading.Tasks;
using Ucu.Poo.Cognitive;
using System.IO.Pipelines;
using SixLabors.ImageSharp.Processing;

namespace CompAndDel
{
    class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();

        }

        private static async Task MainAsync()
        {
            var botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            var channelText = Environment.GetEnvironmentVariable("CHANNEL_ID");
            if (string.IsNullOrWhiteSpace(botToken) || string.IsNullOrWhiteSpace(channelText))
            {
                Console.WriteLine("Faltan las variables de entorno.");
                return;
            }

            CognitiveFace faceApi;
            string subscriptionKey = Environment.GetEnvironmentVariable("AZURE_FACE_SUBSCRIPTION_KEY");
            if (string.IsNullOrWhiteSpace(subscriptionKey))
            {
            Console.WriteLine("Falta la variable de entorno AZURE_FACE_SUBSCRIPTION_KEY'.");
            return;
            }

            faceApi = new CognitiveFace();

            ulong channelId = ulong.Parse(channelText);




            PipeNull pipeNull = new PipeNull();

            // PipeSerial pipeSerialSend2 = new PipeSerial(new PublishFilter(botToken, channelId, @"luke-filtro2.jpg"),pipeNull);
            // PipeSerial pipeSerial4 = new PipeSerial(new SaveFilter(@"luke-filtro2.jpg"), pipeSerialSend2);


            PipeSerial pipeSerial3 = new PipeSerial(new FilterNegative(), pipeNull);
            
            PipeSerial pipeSerialSend1 = new PipeSerial(new PublishFilter(botToken, channelId, @"imagen_r_.png"), pipeSerial3);

            PipeSerial pipeSerial22 = new PipeSerial(new RecognizeFilter(faceApi, @"imagen.png"), pipeSerialSend1);

            PipeSerial pipeSerial2 = new PipeSerial(new SaveFilter(@"imagen.png"), pipeSerial22);

            PipeFork pipeFork1 = new PipeFork(pipeSerial2, pipeSerial3);

            PipeSerial pipeSerial1 = new PipeSerial(new FilterGrayscale(), pipeFork1);

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");

            IPicture result = pipeSerial1.Send(picture);
        }
    }
}
