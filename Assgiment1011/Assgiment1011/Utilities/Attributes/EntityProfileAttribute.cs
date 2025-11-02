using System;
namespace Assgiment1011.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple= false)]
    public class EntityProfileAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EntityClientAttribute: Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ReadonlyAttribute: Attribute
    {

    }
}
