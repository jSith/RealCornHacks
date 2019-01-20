using Cornhacks2019.Accessors;
using Cornhacks2019.Models;
using CornHacks2019.Interfaces.EngineInterfaces;

namespace Cornhacks2019.Engines
{
    public class SubscriptionEngine : ISubscriptionEngine
    {
        private UserAccessor _userAccessor; 

        public SubscriptionEngine(UserAccessor userAccessor)
        {
            _userAccessor = userAccessor; 
        }
        
        public User Subscribe()
        {
            return _userAccessor.AddUser(User); 

        }

        public User Unsubscribe()
        {
            return _userAccessor.DeleteUser(User); 
        }
    }
}