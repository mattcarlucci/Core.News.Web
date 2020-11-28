using AutoMapper;
using Core.News.Entities;
using Crypto.Compare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Compare.Api
{
    public class Bootstrap
    {
        static IMapper mapper;
        public static IMapper Instance => mapper;
        public static class AutoMapperConfig
        {
            /// <summary>
            /// Registers the mappings.
            /// </summary>
            public static void RegisterMappings()
            {
                var config = new MapperConfiguration(
                cfg => 
                {
                    cfg.CreateMap<ItemContent, StoryViewModel>().
                    ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.SmallImage)).
                    ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.CreatedBy)).
                    ForMember(dst => dst.Elapsed, opt => opt.MapFrom(src => src.GetElapsedTime())).
                    ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Title)).
                    ForMember(dst => dst.Body, opt => opt.MapFrom(src => src.Content)).
                    ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.SourceUrl));
                });
                mapper = new Mapper(config);
            }
        }

        public static class Map
        {            
            /// <summary>
            /// Stories the view.
            /// </summary>
            /// <param name="publications">The publications.</param>
            /// <returns>StoryViewModels.</returns>
            public static StoryViewModels StoryView(List<ItemContent> publications)
            {  
                var stories = mapper.Map<List<StoryViewModel>>(publications);

                StoryViewModels model = new StoryViewModels(stories);
                return model;
            }
        }
    }
}
