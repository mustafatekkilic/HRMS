using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public Users Users { get; set; }
    }
}
