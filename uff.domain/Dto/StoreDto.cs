using uff.Domain.Enum;

namespace uff.Domain.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; private set; }
    }
}