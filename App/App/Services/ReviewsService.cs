using App.Models;
using App.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class ReviewsService : BaseService
    {
        public ReviewsService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Review GetReview(int id)
        {
            var entity = Context.Reviews
                .FirstOrDefault(r => r.ReviewId == id);

            return entity;
        }
    }
}
