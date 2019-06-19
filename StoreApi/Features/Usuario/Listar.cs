namespace StoreApi.Features.Usuario
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using StoreApi.Domain;
    using StoreApi.Infra;

    public class Listar
    {
        public class Query : IRequest<Dto[]>
        {
            public int PageSize { get; set; }

            public int PageIndex { get; set; }

            public string Filter { get; set; }
        }

        public class Dto
        {
            public int Id { get; set; }

            public string Nome { get; set; }

            public string Foto { get; set; }

            public string Email { get; set; }

            public bool Administrador { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Dto[]>
        {
            private readonly StoreContext _storeContext;

            public QueryHandler(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }

            public async Task<Dto[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var persons = GetPersons();

                persons = FilterPersons(persons, request);

                persons = PaginatePersons(persons, request);

                return await persons.ToArrayAsync();
            }

            private IQueryable<Dto> GetPersons()
            {
                return _storeContext
                    .Set<Usuario>()
                    .AsNoTracking()
                    .Select(e => new Dto
                    {
                        Id = e.Id,
                        Nome = e.Nome,
                        Email = e.Email,
                        Foto = e.Foto,
                        Administrador = e.Administrador
                    })
                    .AsQueryable();
            }

            private IQueryable<Dto> FilterPersons(IQueryable<Dto> persons, Query request)
            {
                if (string.IsNullOrEmpty(request.Filter))
                    return persons;

                return persons
                    .Where(e => e.Nome.Contains(request.Filter) || e.Email.Contains(request.Filter));
            }

            private IQueryable<Dto> PaginatePersons(IQueryable<Dto> persons, Query request)
            {
                if (!string.IsNullOrEmpty(request.Filter))
                    return persons;

                return persons
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize);
            }
        }
    }
}
