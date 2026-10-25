using System;
using System.Drawing;
using CompAndDel;

namespace CompAndDel.Filters
{
  /// <summary>
  /// Un filtro de convolución que retorna la imagen recibida con los bordes resaltados, lo que la
  /// hace parecer más nítida. Ver https://en.wikipedia.org/wiki/Kernel_(image_processing).
  /// </summary>
  public class FilterSharpenConvolution : FilterGenericConvolution
  {
    protected int[,] kernel;
    protected int complement, divider;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="FilterSharpenConvolution"/> asignando el kernel, el
    /// complemento y el divisor.
    /// </summary>
    /// <param name="kernel">La matriz de 3x3 con los coeficientes del filtro.</param>
    /// <param name="complement">El valor que se suma al resultado de cada componente de color.</param>
    /// <param name="divider">El valor por el cual se divide la suma de los productos.</param>
    public FilterSharpenConvolution(int[,] kernel, int complement, int divider) : base(kernel, complement, divider)
    {
    }
  }
}