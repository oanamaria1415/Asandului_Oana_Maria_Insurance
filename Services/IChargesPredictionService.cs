using System.Threading.Tasks;
using Asandului_Oana_Maria_Insurance.Models;

namespace Asandului_Oana_Maria_Insurance.Services
{
    public interface IChargesPredictionService
    {
        Task<float?> PredictAsync(ChargesApiRequest request);
    }
}
