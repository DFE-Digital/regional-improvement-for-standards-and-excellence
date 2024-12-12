using AutoMapper;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Interfaces.Repositories;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Commands.UpdateSupportProject;

public record SetAdviserCommand(
    SupportProjectId id,
    string assignedAdviserFullName,
    string assignedAdviserEmail
) : IRequest<bool>;


public class SetAdviser
{
  

    public class SetAdviserCommandHandler(ISupportProjectRepository supportProjectRepository)
        : IRequestHandler<SetAdviserCommand,bool>    {

        public async Task<bool> Handle(SetAdviserCommand request,
            CancellationToken cancellationToken)
        {

            var supportProject = await supportProjectRepository.FindAsync(x => x.Id == request.id, cancellationToken);

            if (supportProject is null)
            {
                return false;
            }

            supportProject.SetAdviser(request.assignedAdviserFullName, request.assignedAdviserEmail);

            await supportProjectRepository.UpdateAsync(supportProject, cancellationToken);
            
            return true;
        }
    }

}