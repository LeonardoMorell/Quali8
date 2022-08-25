namespace Domain.Models
{
    public class Customer
    {
        protected Customer() { }

        public Customer(int id, string fullname, string company, string email, string cellphone, string commercialPhone)
        {
            Id = id;
            Fullname = fullname;
            Company = company;
            Email = email;
            Cellphone = cellphone;
            CommercialPhone = commercialPhone;
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string CommercialPhone { get; set; }
    }
}