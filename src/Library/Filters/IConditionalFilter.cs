namespace CompAndDel.Filters
{
  /// <summary>
  /// Un filtro condicional: además de procesar la imagen, deja disponible el resultado de evaluar una
  /// condición sobre ella en <see cref="ConditionIsSatisfied"/>. Permite que una cañería elija por qué
  /// camino enviar la imagen sin conocer cuál es la condición evaluada.
  /// </summary>
  public interface IConditionalFilter : IFilter 
  {
    /// <summary>
    /// Obtiene un valor que indica si la última imagen procesada cumple la condición del filtro.
    /// </summary>
    bool ConditionIsSatisfied { get; }
  }
}
