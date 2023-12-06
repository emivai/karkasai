using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.ProcedureDTOs
{
    public class CreateProcedureDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public static Procedure ToDomain(CreateProcedureDTO procedureDTO)
        {
            return new Procedure
            {
                Name = procedureDTO.Name,
                Description = procedureDTO.Description,
                Price = procedureDTO.Price
            };
        }
    }
}
