using Cornhacks2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CornHacks2019.Interfaces.AccessorInterfaces
{
    interface IGithubAccessor
    {
        Task<List<Repository>> GetPublicRepositoriesAsync();
    }
}