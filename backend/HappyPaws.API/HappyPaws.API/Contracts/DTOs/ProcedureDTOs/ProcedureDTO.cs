using HappyPaws.Core.Entities;

namespace HappyPaws.API.Contracts.DTOs.ProcedureDTOs
{
    public class ProcedureDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public static ProcedureDTO FromDomain(Procedure procedure)
        {
            return new ProcedureDTO
            {
                Id = procedure.Id,
                Name = procedure.Name,
                Description = procedure.Description,
                Price = procedure.Price
            };
        }
    }
}
