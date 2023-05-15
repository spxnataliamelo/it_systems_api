using AnbimaFeedMVC.Models;

namespace AnbimaFeedMVC.Services
{
    public interface IDebenturesMercadoSecundarioApiServices
    {
        Task<List<DebenturesMercadoSecundarioModel>> GetDebenturesMercadoSecundario(DateTime data);
    }

}
