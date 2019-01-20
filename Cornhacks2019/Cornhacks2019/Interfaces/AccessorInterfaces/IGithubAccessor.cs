using Cornhacks2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cornhacks2019.Interfaces.AccessorInterfaces
{
    public interface IGithubAccessor
    {
        Task<List<Repository>> GetPublicRepositoriesAsync();
    }
}