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

    public class InsertEdit
    {
        public class Query : IRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest
        {
            public int? Id { get; set; }

            public string Nome { get; set; }

            public string Foto { get; set; }

            public string Email { get; set; }

            public bool Administrador { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly StoreContext _storeContext;

            public QueryHandler(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }

            public async Task<Command> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _storeContext
                    .Set<Usuario>()
                    .AsNoTracking()
                    .Where(e => e.Id == request.Id)
                    .Select(e => new Command
                    {
                        Id = e.Id,
                        Nome = e.Nome,
                        Email = e.Email,
                        Foto = e.Foto,
                        Administrador = e.Administrador
                    })
                    .FirstOrDefaultAsync();
            }
        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            private readonly StoreContext _storeContext;

            public CommandHandler(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var person = await GetPerson(request);

                MapPerson(person, request);

                await _storeContext.SaveChangesAsync();
            }

            private async Task<Usuario> GetPerson(Command request)
            {
                if (!request.Id.HasValue)
                {
                    var person = new Usuario();

                    await _storeContext.AddAsync(person);

                    return person;
                }

                return await _storeContext
                    .Set<Usuario>()
                    .FirstOrDefaultAsync(e => e.Id == request.Id);
            }

            private void MapPerson(Usuario person, Command request)
            {
                person.Nome = request.Nome;
                person.Email = request.Email;
                person.Foto = request.Foto;
                person.Administrador = request.Administrador;
            }
        }
    }
}
