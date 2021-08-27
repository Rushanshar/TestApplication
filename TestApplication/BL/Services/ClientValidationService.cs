
using System.Linq;
using Bl.Interfaces;

namespace BL.Services
{
    public class ClientValidationService : IClientValidationService
    {
        public const int NameMaxLength = 64;

        public bool ValidateName(string parameter)
        {
            return parameter.Trim().Length <= NameMaxLength 
                   && parameter.All(ch => char.IsLetter(ch) || char.IsWhiteSpace(ch));
        }
    }
}
