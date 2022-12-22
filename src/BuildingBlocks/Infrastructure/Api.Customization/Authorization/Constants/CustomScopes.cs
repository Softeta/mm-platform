using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;

namespace API.Customization.Authorization.Constants
{
    public static class CustomScopes
    {
        public static class BackOffice
        {
            public const string User = "BackOffice.User";
        }

        public static class FrontOffice
        {
            public const string Company = "FrontOffice.Company";
            public const string Candidate = "FrontOffice.Candidate";
        }

        public static Scope GetScopeEnumValue(string scope)
        {
            switch (scope)
            {
                case BackOffice.User:
                    return Scope.BackOffice;
                case FrontOffice.Candidate:
                    return Scope.Candidate;
                case FrontOffice.Company:
                    return Scope.Company;
                default:
                    throw new NotFoundException("Unknown scope");
            }
        }
    }
}
