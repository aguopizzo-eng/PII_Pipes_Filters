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

            PipeSerial pipePublish = new PipeSerial(new PublishFilter(@"beer3.jpg"), pipeNull);
            PipeSerial pipeSave2 = new PipeSerial(new SaveFilter(@"beer3.jpg"), pipePublish);
            // PipeSerial pipeSerial2 = new PipeSerial(new FilterNegative(), pipeSave2);

            PipeSerial pipeSharpen = new PipeSerial(new FilterSharpenConvolution(new int[3,3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } }, 0, 1), pipeSave2);

            PipeSerial pipeSave1 = new PipeSerial(new SaveFilter(@"beer2.jpg"), pipeSharpen);

            PipeSerial pipeSerialNotSend = new PipeSerial(new FilterBlurConvolution(new int[3,3], 0, 9), pipeSave1);
            PipeSerial pipeSerialSend = new PipeSerial(new PublishFilter(@"imagen_r_.png"), pipeNull);

            PipeConditionalFork pipeConditional = new PipeConditionalFork(new ConditionalFilter(), pipeSerialSend, pipeSerialNotSend);
            PipeSerial pipeSerial1 = new PipeSerial(new FilterGrayscale(), pipeConditional);

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            IPicture result = pipeSerial1.Send(picture);
        }
    }
}
