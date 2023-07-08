using AutoMapper;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _dataContext;
        public CountryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        public bool CountryExists(int id) => _dataContext.Countries.Any(c => c.Id == id);

        public bool CreateCountry(Country country)
        {
            _dataContext.Add(country);

            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _dataContext.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries() => _dataContext.Countries.ToList();

        public Country GetCountry(int id) => _dataContext.Countries.Where(c => c.Id == id).FirstOrDefault();

        public Country GetCountryByOwner(int ownerId) => _dataContext.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();

        public ICollection<Owner> GetOwnersFromCountry(int countryId) => _dataContext.Owners.Where(c => c.Country.Id == countryId).ToList();

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool UpdateCountry(Country country)
        {
            _dataContext.Update(country);
            return Save();
        }
    }
}
