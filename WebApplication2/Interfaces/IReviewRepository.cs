using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfPokemon(int pokemonId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool Save();

    }
}
