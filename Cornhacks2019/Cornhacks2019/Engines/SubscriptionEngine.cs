using Cornhacks2019.Accessors;
using Cornhacks2019.Models;
using Cornhacks2019.Interfaces.EngineInterfaces;

namespace Cornhacks2019.Engines
{
    public class SubscriptionEngine : ISubscriptionEngine
    {
        private UserAccessor _userAccessor; 

        public SubscriptionEngine(UserAccessor userAccessor)
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