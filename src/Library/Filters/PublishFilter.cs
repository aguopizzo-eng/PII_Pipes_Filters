using System;
using Ucu.Poo.Discord;
namespace CompAndDel.Filters
{
  /// <summary>
  /// Un filtro que publica en un canal de Discord la imagen guardada en un archivo y retorna
  /// la imagen recibida sin modificarla. El token y el canal se leen de las variables de entorno
  /// <c>DISCORD_BOT_TOKEN</c> y <c>CHANNEL_ID</c>.
  /// </summary>
  public class PublishFilter : IFilter
  {
    private DiscordClient discord;
    private ulong channelId;
    private string imagePath;
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="PublishFilter"/> e inicia sesión en Discord.
    /// </summary>
    /// <param name="imagePath">El path del archivo con la imagen a publicar. El archivo debe
    /// existir en el momento en que se aplica el filtro.</param>
    public PublishFilter(string imagePath)
    {
      this.imagePath = imagePath;

      string botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
      string channelText = Environment.GetEnvironmentVariable("CHANNEL_ID");
      if (string.IsNullOrWhiteSpace(botToken) || string.IsNullOrWhiteSpace(channelText))
      {
            Console.WriteLine("Faltan las variables de entorno 'DISCORD_BOT_TOKEN' y 'CHANNEL_ID'.");
            return;
      }

      this.channelId = ulong.Parse(channelText);
      this.discord = new DiscordClient();

      discord.Login(botToken);
    }

    /// <summary>
    /// Publica en Discord el archivo de imagen configurado y retorna la imagen recibida.
    /// </summary>
    /// <param name="image">La imagen recibida, que se retorna sin cambios.</param>
    /// <returns>La misma imagen recibida.</returns>
    public IPicture Filter(IPicture image)
    {
      discord.SendImage(this.channelId, this.imagePath, "Mira esta imagen");
      return image;
    }
  }
}