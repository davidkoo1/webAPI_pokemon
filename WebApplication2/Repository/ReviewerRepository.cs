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

        public Reviewer GetReviewer(int reviewerId) => _dataContext.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();

        public ICollection<Reviewer> GetReviewers() => _dataContext.Reviewers.ToList();

        public ICollection<Review> GetReviewsByReviewer(int reviewerId) => _dataContext.Reviews.Where(x => x.Reviewer.Id == reviewerId).ToList();

        public bool ReviewerExists(int reviewerId) => _dataContext.Reviewers.Any(r => r.Id == reviewerId);
    }
}
