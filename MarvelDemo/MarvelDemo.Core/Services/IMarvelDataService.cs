using System.Collections.Generic;
using System.Threading.Tasks;
using MarvelDemo.Core.Models;

namespace MarvelDemo.Core.Services
{
    public interface IMarvelDataService
    {
        Task<IEnumerable<Comic>>GetComicsBySeries(int seriesId);
    }
}
