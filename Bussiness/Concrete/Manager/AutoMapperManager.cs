using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Business.Abstract.Service;
using Business.Concrete.Utilities.AutoMapperProfiles;

namespace Business.Concrete.Manager
{
    public class AutoMapperManager : IMapperService
    {
        private Mapper _mapper;
        public AutoMapperManager()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

                cfg.AddProfile(new UserProfile());

            });

            config.CreateMapper();

            _mapper = new Mapper(config);
        }

        public THedef Map<TKaynak, THedef>(TKaynak kaynak)
        {
            
            return _mapper.Map<THedef>(kaynak);
        }

        public THedef Map<TKaynak, THedef>(TKaynak kaynak, THedef hedef)
        {
            return _mapper.Map(kaynak, hedef);
        }
    }
}
