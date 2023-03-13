namespace IndustryIncident.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string FamillyName { get; set; } = null!;

        public string Title { get; set; }
        public int RoleID { get; set; }
        public int AccesID { get; set; }
        public Role Role { get; set; } = new Role();
        public Zone Acces { get; set; } = new Zone();
    }
}
