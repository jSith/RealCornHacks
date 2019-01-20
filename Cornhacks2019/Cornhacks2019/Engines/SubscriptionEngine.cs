using Cornhacks2019.Models;
using Cornhacks2019.Interfaces.EngineInterfaces;
using Cornhacks2019.Interfaces.AccessorInterfaces;

namespace Cornhacks2019.Engines
{
    public class SubscriptionEngine : ISubscriptionEngine
    {
        private IUserAccessor _userAccessor; 

        public SubscriptionEngine(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor; 
        }
        
        public User Subscribe(User user)
        {
            return _userAccessor.Insert(user); 

        }

        public User Unsubscribe(User user)
        {
            return _userAccessor.Delete(user); 
        }
    }
}