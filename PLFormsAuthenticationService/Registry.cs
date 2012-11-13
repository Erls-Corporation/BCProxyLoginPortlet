using mobile.Infrastructure.Security;

namespace PLFormsAuthenticationService
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            For<IFormsAuthenticationService>().Use<FormsAuthenticationService>();
        }
    }
}
