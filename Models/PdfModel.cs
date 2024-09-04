namespace ReportPOC.Models
{
    public class PdfModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Image { get; set; }
        public List<UserInfo> Users { get; set; }

        public PdfModel()
        {
            Users = new List<UserInfo>();
        }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}
