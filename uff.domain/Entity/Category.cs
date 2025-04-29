using System.Collections.Generic;

namespace UFF.Domain.Entity
{
    public class Category : To
    {
        private Category()
        {
        }

        public string Name { get; set; }
        public string ImgPath { get; set; }
        public virtual ICollection<Store> Stores { get; private set; } = new List<Store>();

        public Category(string name, string imgPath)
        {
            Name = name;
            ImgPath = imgPath;
        }
    }
}