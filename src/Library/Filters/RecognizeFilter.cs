using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompAndDel;
using Ucu.Poo.Discord;
using Ucu.Poo.Cognitive;

namespace CompAndDel.Filters
{
  public class RecognizeFilter: IFilter
  {
    /// <summary>
    /// El token del bot de Discord.
    /// </summary>
    private CognitiveFace faceApi;

    private string path;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="UcuRideShare"/>.
    /// </summary>
    /// <param name="botToken">El token del bot de Discord.</param>
    /// <param name="channelText">El identificador del canal de Discord.</param>
    public RecognizeFilter(CognitiveFace faceApi, string path)
    {
      this.faceApi = faceApi;   
      this.path = path;
    }

    public IPicture Filter(IPicture image)
    {
      PictureProvider provider = new PictureProvider();
      CognitiveFace.RecognitionResult result = this.faceApi.Recognize(this.path, true);;
      if (result.Success && result.FaceFound) // Recognize procesó la imagen y encontró una cara
      {
            return provider.GetPicture(@"imagen_r_.png"); // Recognize agrega '_r_' como sufijo al archivo
      }
      else // Recognize no pudo procesar la imagen.
      {
            return image;
      }
    }
  }
}