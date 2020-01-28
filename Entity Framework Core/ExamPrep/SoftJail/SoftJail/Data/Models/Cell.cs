namespace SoftJail.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.CellValidator;

    public class Cell
    {
        public Cell()
        {
            this.Prisoners = new HashSet<Prisoner>();
        }

        [Key]
        public int Id { get; set; }

        [Range(CellNumberMinRange, CellNumberMaxRange)]
        public int CellNumber { get; set; }

        public bool HasWindow { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Prisoner> Prisoners { get; set; }
    }
}
