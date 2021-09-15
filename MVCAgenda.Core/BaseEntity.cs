namespace MVCAgenda.Core
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool Hidden { get; set; } = false;
    }
}
