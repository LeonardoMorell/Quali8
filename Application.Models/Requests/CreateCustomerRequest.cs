namespace Application.Models.Requests
{
    public class CreateCustomerRequest
    {
        public string Fullname { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string CommercialPhone { get; set; }
    }
}