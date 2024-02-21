
namespace Business.Abstract.Service
{
    public interface IMapperService
    {
        THedef Map<TKaynak, THedef>(TKaynak kaynak);
        THedef Map<TKaynak, THedef>(TKaynak kaynak, THedef hedef);
    }
}
