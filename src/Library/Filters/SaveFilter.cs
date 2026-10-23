namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que guarda en un archivo la imagen que recibe y la retorna sin modificarla.
    /// Permite persistir el resultado de cualquier paso intermedio de una secuencia de pipes.
    /// </summary>
    public class SaveFilter : IFilter 
    {
        private PictureProvider provider = new PictureProvider();
        private string path;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="SaveFilter"/>.
        /// </summary>
        /// <param name="path">El path del archivo donde se guarda la imagen.</param>
        public SaveFilter(string path)
        {
            this.path = path;
        }
        /// <summary>
        /// Guarda la imagen en el archivo indicado y la retorna sin cambios.
        /// </summary>
        /// <param name="image">La imagen a guardar.</param>
        /// <returns>La misma imagen recibida.</returns>
        public IPicture Filter(IPicture image)
        {
            this.provider.SavePicture(image, this.path);
            return image;
        }
    }
}
