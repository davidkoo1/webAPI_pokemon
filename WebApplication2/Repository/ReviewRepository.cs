using AutoMapper;
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

        public Review GetReview(int reviewId) => _dataContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();

        public ICollection<Review> GetReviews() => _dataContext.Reviews.ToList();

        public ICollection<Review> GetReviewsOfPokemon(int pokemonId) => _dataContext.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();

        public bool ReviewExists(int reviewId) => _dataContext.Reviews.Any(x => x.Id == reviewId);
    }
}
