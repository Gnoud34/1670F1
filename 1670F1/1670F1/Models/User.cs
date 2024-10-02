namespace _1670F1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public JobSeeker JobSeeker { get; set; }
        public Employer Employer { get; set; }
    }
}
