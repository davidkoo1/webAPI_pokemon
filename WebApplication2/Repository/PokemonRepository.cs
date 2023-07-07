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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _dataContext.Owners.Where(x => x.Id == ownerId).FirstOrDefault();
            var category = _dataContext.Categories.Where(x => x.Id ==  categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };

            _dataContext.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _dataContext.Add(pokemonCategory);

            _dataContext.Add(pokemon);

            return Save();
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

        public bool Save()
        {
            var seved = _dataContext.SaveChanges();
            return seved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _dataContext.Update(pokemon);
            return Save();
        }
    }
}
