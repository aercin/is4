using AutoMapper;
using core_application.Common;
using domain.Abstractions;
using domain.Entities;
using MediatR;

namespace application.Features.Queries
{
    public static class GetUsers
    {
        #region Query
        public class Query : QueryPaginationBase<Result>
        {
            public string UserName { get; set; } 
        }
        #endregion

        #region Query Handler
        public class QueryHandler : IRequestHandler<Query, Result>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _uow;
            public QueryHandler(IUnitOfWork uow, IMapper mapper)
            {
                this._uow = uow;
                this._mapper = mapper;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = this._uow.UserRepo.GetAll().WhereIf(!string.IsNullOrEmpty(request.UserName), x => x.UserName.ToLower().Contains(request.UserName.ToLower()));

                var result = await Task.FromResult(query.OrderBy(x => x.Id).QueryResource<User, UserDto>(_mapper, request.PageNumber, request.PageSize));

                return result;
            }
        }
        #endregion

        #region Mapping Profile
        public class GetUsersProfile : Profile
        {
            public GetUsersProfile()
            {
                CreateMap<User, UserDto>();
            }
        }
        #endregion

        #region Response 
        public class UserDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string FamilyName { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
        }
        #endregion
    }
}
