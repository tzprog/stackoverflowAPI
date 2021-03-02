using Microsoft.AspNetCore.Mvc;
using StackoverflowTags.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackoverflowTags.Data
{
    public interface IStackoverflowData
    {
        Task<List<StackoverflowAPI>> GetData();

        Task<List<StackoverflowTypeWWW>> GetAll(int res);
    }
}