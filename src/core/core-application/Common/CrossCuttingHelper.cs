using MediatR;

namespace core_application.Common
{
    public class CrossCuttingHelper
    {
        public static bool ShouldSkip<T>() where T : IBaseRequest
        {
            var result = Attribute.GetCustomAttribute(typeof(T), typeof(SkipBehaviourAttribute)) != null;
            return result;
        }
    }
}
