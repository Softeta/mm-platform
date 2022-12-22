using Domain.Seedwork.Shared.Entities;

namespace Contracts.Shared
{
    public class Hobby
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public static Hobby FromDomain(HobbyBase candidateHobby)
        {
            return new Hobby
            {
                Id = candidateHobby.HobbyId,
                Code = candidateHobby.Code,
            };
        }
    }
}
