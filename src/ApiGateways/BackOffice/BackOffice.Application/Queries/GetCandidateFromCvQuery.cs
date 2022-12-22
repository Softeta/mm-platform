using API.WebClients.Clients.FormRecognizer.Models;
using BackOffice.Application.Contracts.Responses;
using MediatR;

namespace BackOffice.Application.Queries;

public record GetCandidateFromCvQuery(string? FileUri, Guid? FileCacheId, CvDocumentSource Source) : IRequest<CvCandidateResponse?>;