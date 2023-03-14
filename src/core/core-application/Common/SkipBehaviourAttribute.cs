using MediatR;

namespace core_application.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SkipBehaviourAttribute : Attribute
    {
    }  
}
