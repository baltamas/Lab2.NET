using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MultimediaCenter.Models;
using MultimediaCenter.ViewModels;

namespace MultimediaCenter
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Movie, MovieWithCommentsViewModels>();

        }
    }
}
