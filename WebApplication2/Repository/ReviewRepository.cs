﻿using AutoMapper;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ReviewRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public bool CreateReview(Review review)
        {
            _dataContext.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _dataContext.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _dataContext.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId) => _dataContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();

        public ICollection<Review> GetReviews() => _dataContext.Reviews.ToList();

        public ICollection<Review> GetReviewsOfPokemon(int pokemonId) => _dataContext.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();

        public bool ReviewExists(int reviewId) => _dataContext.Reviews.Any(x => x.Id == reviewId);

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool UpdateReview(Review review)
        {
            _dataContext.Update(review);
            return Save();
        }
    }
}
