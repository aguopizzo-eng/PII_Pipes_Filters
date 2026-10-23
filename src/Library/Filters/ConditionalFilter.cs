           
using System;
using Ucu.Poo.Cognitive;

namespace CompAndDel.Filters
{
  /// <summary>
  /// Un filtro condicional que detecta si la imagen contiene una cara usando el servicio de
  /// reconocimiento. El resultado queda disponible en <see cref="HasFace"/>. La clave de Azure se
  /// lee de la variable de entorno <c>AZURE_FACE_SUBSCRIPTION_KEY</c>.
  /// </summary>
  public class ConditionalFilter : IFilter 
  {
    private PictureProvider provider = new PictureProvider();
    private CognitiveFace faceApi;
    /// <summary>
    /// Obtiene un valor que indica si la última imagen procesada contiene una cara.
    /// </summary>
    public bool HasFace { get; private set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ConditionalFilter"/>.
    /// </summary>
    public ConditionalFilter()
    {
      string subscriptionKey = Environment.GetEnvironmentVariable("AZURE_FACE_SUBSCRIPTION_KEY");
      if (string.IsNullOrWhiteSpace(subscriptionKey))
      {
        Console.WriteLine("Falta la variable de entorno AZURE_FACE_SUBSCRIPTION_KEY'.");
        return;
      }

      this.faceApi = new CognitiveFace();
    }

    /// <summary>
    /// Guarda la imagen, detecta si contiene una cara y actualiza <see cref="HasFace"/>.
    /// </summary>
    /// <param name="image">La imagen a analizar.</param>
    /// <returns>La imagen con la cara marcada si se encontró una cara; de lo contrario, la misma
    /// imagen recibida.</returns>
    public IPicture RecognizeFace(IPicture image)
    {
      this.provider.SavePicture(image, "imagen.png");
      CognitiveFace.RecognitionResult result = faceApi.Recognize(@"imagen.png", true);
      if (result.Success && result.FaceFound) // Recognize procesó la imagen y encontró una cara
      {
        this.HasFace = true;
        return provider.GetPicture(@"imagen_r_.png"); // Recognize agrega '_r_' como sufijo al archivo
      }
      else // Recognize no pudo procesar la imagen.
      {
        this.HasFace = false;
        return image;
      }
    }
    
    /// <summary>
    /// Procesa la imagen detectando si contiene una cara.
    /// </summary>
    /// <param name="image">La imagen a procesar.</param>
    /// <returns>La imagen con la cara marcada si se encontró una cara; de lo contrario, la misma
    /// imagen recibida.</returns>
    public IPicture Filter(IPicture image)
    {
      return this.RecognizeFace(image);
    }
  }
}
