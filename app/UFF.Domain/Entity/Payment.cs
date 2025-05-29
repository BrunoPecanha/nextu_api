namespace UFF.Domain.Entity
{
    public class Payment : To
    {
        private Payment()
        {
        }

        public string Name { get; set; }
        public string ImgPath { get; set; }
        public string Notes { get; set; }
        public string Icon { get; set; }

        public Payment(string name, string imgPath, string icon)
        {
            Name = name;
            ImgPath = imgPath;
            Icon = icon;
        }
    }
}