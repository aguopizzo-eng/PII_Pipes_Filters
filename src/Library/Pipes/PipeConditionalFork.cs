using CompAndDel.Filters;


namespace CompAndDel.Pipes
{
  /// <summary>
  /// Una cañería condicional con bifurcación: aplica un filtro condicional a la imagen y la envía por
  /// una cañería si el filtro cumple su condición, o por la otra si no la cumple.
  /// </summary>
  public class PipeConditionalFork : IPipe
  {
    protected IConditionalFilter filtro;
    protected IPipe nextPipe;
    protected IPipe pipe1;
    protected IPipe pipe2;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="PipeConditionalFork"/>.
    /// </summary>
    /// <param name="filtro">Filtro condicional que determina por qué cañería se envía la imagen.</param>
    /// <param name="pipe1">Cañería a la que se envía la imagen si el filtro cumple su condición.</param>
    /// <param name="pipe2">Cañería a la que se envía la imagen si el filtro no cumple su condición.</param>
    public PipeConditionalFork(IConditionalFilter filtro, IPipe pipe1, IPipe pipe2)
    {
        this.filtro = filtro;
        this.pipe1 = pipe1;
        this.pipe2 = pipe2;
    }
    
    /// <summary>
    /// Elige la siguiente cañería según el resultado del último filtrado.
    /// </summary>
    /// <returns><see cref="pipe1"/> si se cumple la condición del filtro; de lo contrario, <see cref="pipe2"/>.</returns>
    public IPipe ChooseFilter()
    {
      if (this.filtro.ConditionIsSatisfied)
      {
        return this.nextPipe = pipe1;  
      }

      return this.nextPipe= pipe2;
    }

    /// <summary>
    /// Recibe una imagen, la procesa con el filtro condicional y envía el resultado por la
    /// cañería que corresponda según se cumpla o no la condición del filtro.
    /// </summary>
    /// <param name="picture">La imagen a procesar y enviar.</param>
    /// <returns>La imagen retornada por la cañería elegida.</returns>
    public IPicture Send(IPicture picture)
    {
      IPicture sentPicture = this.filtro.Filter(picture);
      this.ChooseFilter();
      return this.nextPipe.Send(sentPicture);
    }
  }
}