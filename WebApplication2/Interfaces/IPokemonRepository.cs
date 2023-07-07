
using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokemonId);
        bool PokemonExist(int pokemonId);
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool Save();

    }
}
