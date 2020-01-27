namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("User", Namespace = "")]
    public class UserSoldProductDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public SoldProductDto[] SoldProducts { get; set; }
    }
}
