﻿using App.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base()
        {
            UserProfile();
            BookProfile();
        }
        protected void UserProfile()
        {
            CreateMap<User, UserViewModel>();
        }

        protected void BookProfile()
        {
            CreateMap<Book, BookListItemViewModel>();
            CreateMap<Book, BookViewModel>();
        }
    }

    
}
