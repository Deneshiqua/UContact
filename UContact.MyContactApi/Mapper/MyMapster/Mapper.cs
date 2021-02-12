using Mapster;
using MapsterMapper;
using System;

namespace UContact.MyContactApi.Mapper.MyMapster
{
    public class Mapper : IMapper
    {
        public TypeAdapterConfig Config => throw new NotImplementedException();

        public TypeAdapterBuilder<TSource> From<TSource>(TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }

        public TDestination Map<TDestination>(object source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}
