using System;
using System.Drawing;
using CompAndDel;

namespace CompAndDel.Filters
{
  /// <summary>
  /// Un filtro de convolución que retorna la imagen recibida con los bordes suavizados. Basado en
  /// https://en.wikipedia.org/wiki/Box_blur utilizando el kernel
  /// https://wikimedia.org/api/rest_v1/media/math/render/svg/91256bfeece3344f8602e288d445e6422c8b8a1c.
  /// </summary>
  public class FilterBlurConvolution : FilterGenericConvolution
  {
    protected int[,] kernel;
    protected int complement, divider;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="FilterBlurConvolution"/> asignando el kernel, el
    /// complemento y el divisor.
    /// </summary>
    /// <param name="kernel">La matriz de 3x3 con los coeficientes del filtro.</param>
    /// <param name="complement">El valor que se suma al resultado de cada componente de color.</param>
    /// <param name="divider">El valor por el cual se divide la suma de los productos.</param>
    public FilterBlurConvolution(int[,] kernel, int complement, int divider) : base(kernel, complement, divider)
    {
      for (int x = 0; x < 3; x++)
      {
          for (int y = 0; y < 3; y++)
          {
              kernel[x, y] = 1;
          }
      }
    }
  }
}