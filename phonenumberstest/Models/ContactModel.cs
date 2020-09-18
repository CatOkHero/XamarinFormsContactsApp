using SQLite;

namespace phonenumberstest.Models
{
    public class ContactModel
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
    }
}
