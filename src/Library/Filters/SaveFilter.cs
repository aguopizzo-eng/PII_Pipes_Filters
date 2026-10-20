namespace CompAndDel.Filters
{
    public class SaveFilter : IFilter
    {
        private string path;
        public SaveFilter(string path)
        {
            this.path = path;
        }
        public IPicture Filter(IPicture image)
        {
            PictureProvider provider = new PictureProvider();
            provider.SavePicture(image,this.path);
            return image;
        }
    }
}
