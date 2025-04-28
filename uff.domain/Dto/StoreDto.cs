using UFF.Domain.Enum;

namespace UFF.Domain.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; private set; }
    }
}