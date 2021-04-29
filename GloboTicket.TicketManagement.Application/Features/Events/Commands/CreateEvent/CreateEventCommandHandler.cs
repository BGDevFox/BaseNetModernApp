using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure.Mail;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @event = _mapper.Map<Event>(request);
            @event = await _eventRepository.AddAsync(@event);

            var email = new Email()
            {
                To = "test@mail.com",
                Subject = "A new event was created!",
                Body = $"A new event was created: {request}"
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                //this should't stop the API from doind else so this can be logged
            }

            return @event.EventId;
        }
    }
}