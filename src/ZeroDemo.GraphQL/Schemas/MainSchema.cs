using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using ZeroDemo.Queries.Container;

namespace ZeroDemo.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}