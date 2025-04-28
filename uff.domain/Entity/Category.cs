namespace UFF.Domain.Entity
{
    public class Category : To
    {
        public string Name { get; set; }
        public string ImgPath { get; set; }

        public Category(string name, string imgPath)
        {
            Name = name;
            ImgPath = imgPath;
        }
    }
}