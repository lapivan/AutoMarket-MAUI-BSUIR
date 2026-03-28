namespace AutoMarket.UI
{
    public static class ImagePathHelper
    {
        public static string GetImagePath(int id)
        {
            string folder = Path.Combine(FileSystem.AppDataDirectory, "Images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return Path.Combine(folder, $"{id}.jpg");
        }
    }
}
