using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PipeNull pipeNull = new PipeNull();

            // PipeSerial pipePublish = new PipeSerial(new PublishFilter(@"luke-filtro1.jpg"), pipeNull);
            // PipeSerial pipeSave2 = new PipeSerial(new SaveFilter(@"luke-filtro2.jpg"), pipePublish);
            // PipeSerial pipeSerial2 = new PipeSerial(new FilterNegative(), pipeSave2);

            // PipeSerial pipeSave1 = new PipeSerial(new SaveFilter(@"luke-filtro1.jpg"), pipeSerial2);

            PipeSerial pipeSerialNotSend = new PipeSerial(new FilterNegative(), pipeNull);
            PipeSerial pipeSerialSend = new PipeSerial(new PublishFilter(@"imagen_r_.png"), pipeNull);

            PipeConditionalFork pipeConditional = new PipeConditionalFork(new ConditionalFilter(), pipeSerialSend, pipeSerialNotSend);
            PipeSerial pipeSerial1 = new PipeSerial(new FilterGrayscale(), pipeConditional);

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");

            IPicture result = pipeSerial1.Send(picture);
        }
    }
}
