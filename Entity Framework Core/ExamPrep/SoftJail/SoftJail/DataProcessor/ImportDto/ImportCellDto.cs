namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static ModelValidator.CellValidator;

    public class ImportCellDto
    {
        [Range(CellNumberMinRange, CellNumberMaxRange)]
        public int CellNumber { get; set; }

        public bool HasWindow { get; set; }
    }
}
