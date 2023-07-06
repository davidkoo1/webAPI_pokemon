using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _dataContext;

        public PokemonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Pokemon GetPokemon(int pokemonId) => _dataContext.Pokemon.Where(p => p.Id == pokemonId).FirstOrDefault();

        public Pokemon GetPokemon(string name) => _dataContext.Pokemon.Where(p => p.Name == name).FirstOrDefault();

        public decimal GetPokemonRating(int pokemonId)
        {
            var review = _dataContext.Reviews.Where(p => p.Id == pokemonId);
            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons() =>  _dataContext.Pokemon.OrderBy(p => p.Id).ToList();


        public bool PokemonExist(int pokemonId) => _dataContext.Pokemon.Any(p => p.Id == pokemonId);
    }
}
