
namespace BL.Models
{
    public class ClientShortInfo
    {
        public ClientShortInfo(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }
    }
}
