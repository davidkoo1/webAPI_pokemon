using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _dataContext.Add(reviewer);

            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _dataContext.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId) => _dataContext.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();

        public ICollection<Reviewer> GetReviewers() => _dataContext.Reviewers.ToList();

        public ICollection<Review> GetReviewsByReviewer(int reviewerId) => _dataContext.Reviews.Where(x => x.Reviewer.Id == reviewerId).ToList();

        public bool ReviewerExists(int reviewerId) => _dataContext.Reviewers.Any(r => r.Id == reviewerId);

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _dataContext.Update(reviewer);
            return Save();
        }
    }
}
