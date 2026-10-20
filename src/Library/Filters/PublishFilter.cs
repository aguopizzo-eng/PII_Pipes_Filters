using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompAndDel;
using Ucu.Poo.Discord;

namespace CompAndDel.Filters
{
  public class PublishFilter : IFilter
  {
    /// <summary>
    /// El cliente utilizado para publicar en Discord.
    /// </summary>
    private DiscordClient discord = new DiscordClient();

    private string path;

    /// <summary>
    /// El token del bot de Discord.
    /// </summary>
    public string BotToken { get; }

    /// <summary>
    /// El identificador del canal de Discord donde se publica.
    /// </summary>
    public ulong ChannelId { get; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="UcuRideShare"/>.
    /// </summary>
    /// <param name="botToken">El token del bot de Discord.</param>
    /// <param name="channelText">El identificador del canal de Discord.</param>
    public PublishFilter(string botToken, ulong channelText,string path)
    {
      this.path = path;
      this.BotToken = botToken;
      this.ChannelId = channelText;
      this.Login();
    }

    /// <summary>
    /// Inicia sesión en Discord. Se debe llamar una sola vez antes de agregar registros.
    /// </summary>
    /// <returns>La tarea asincrónica.</returns>
    public void Login()
    {
      Console.WriteLine("Conectando con Discord...");
      discord.Login(this.BotToken);
    }

    public IPicture Filter(IPicture image)
    {
      this.Publish();
      return image;
    }

    public void Publish()
    {
       discord.SendImage(ChannelId, this.path, "Mira esta imagen");
    }
  }
}
