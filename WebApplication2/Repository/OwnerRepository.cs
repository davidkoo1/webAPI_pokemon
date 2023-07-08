using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _dataContext;

        public OwnerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateOwner(Owner owner)
        {
            _dataContext.Add(owner);

            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _dataContext?.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId) => _dataContext.Owners.Where(x => x.Id == ownerId).FirstOrDefault();

        public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId) => _dataContext.PokemonOwners
                                                                                                  .Where(p => p.PokemonId == pokemonId)
                                                                                                  .Select(x => x.Owner).ToList();

        public ICollection<Owner> GetOwners() => _dataContext.Owners.ToList();

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId) => _dataContext.PokemonOwners.Where(o => o.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();

        public bool OwnerExists(int ownerId) => _dataContext.Owners.Any(o => o.Id == ownerId);

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool UpdateOwner(Owner owner)
        {
            _dataContext.Update(owner);
            return Save();
        }
    }
}
