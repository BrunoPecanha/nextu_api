using System.Collections.Generic;

namespace UFF.Domain.Entity
{
    public class ServiceCategory : To
    {
        private ServiceCategory()
        {
        }

        public string Name { get; private set; }
        public string ImgPath { get; private set; }
        public string Icon { get; set; }
        public virtual ICollection<Service> Services { get; private set; } = new List<Service>();

        public ServiceCategory(string name, string imgPath)
        {
            Name = name;
            ImgPath = imgPath;
        }
    }
}