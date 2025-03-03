// using Dfe.ManageSchoolImprovement.Application.SupportProject.Commands.UpdateSupportProject;
// using Dfe.ManageSchoolImprovement.Application.SupportProject.Queries;
// using Dfe.ManageSchoolImprovement.Domain.ValueObjects;
// using Dfe.ManageSchoolImprovement.Frontend.Models;
// using Dfe.ManageSchoolImprovement.Frontend.Services;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
//
// namespace Dfe.ManageSchoolImprovement.Frontend.Pages.ProjectAssignment;
//
//
// public class AdIndexModel(IUserRepository userRepository, ISupportProjectQueryService supportProjectQueryService, IMediator _mediator) : PageModel
// {
//    public string SchoolName { get; private set; }
//    public int Id { get; set; }
//    public IEnumerable<User> Advisers { get; set; }
//    public string SelectedAdviser { get; set; }
//
//    public async Task<IActionResult> OnGet(int id ,CancellationToken cancellationToken)
//    {
//       var projectResponse = await supportProjectQueryService.GetSupportProject(id,cancellationToken);
//       Id = id;
//       SchoolName = projectResponse.Value?.schoolName!;
//       SelectedAdviser = projectResponse.Value?.assignedAdviserFullName!;
//
//       Advisers = await userRepository.GetAllUsers();
//
//       return Page();
//    }
//
//    public async Task<IActionResult> OnPost(int id, string selectedName, bool unassignAdviser, string adviserInput,CancellationToken cancellationToken)
//    {
//       var projectResponse = await supportProjectQueryService.GetSupportProject(id, cancellationToken);
//       
//       SupportProjectId supportProjectId = new(projectResponse.Value!.id);
//       
//       if (string.IsNullOrWhiteSpace(adviserInput))
//       {
//          selectedName = string.Empty;
//       }
//
//       if (unassignAdviser)
//       {
//          var request = new SetAdviserCommand(supportProjectId, null!, null!);
//          await _mediator.Send(request);
//       }
//       else if (!string.IsNullOrEmpty(selectedName))
//       {
//          IEnumerable<User> deliveryOfficers = await userRepository.GetAllUsers();
//
//          var assignedAdviser = deliveryOfficers.SingleOrDefault(u => u.FullName == selectedName);
//             var request = new SetAdviserCommand(supportProjectId, assignedAdviser?.FullName!, assignedAdviser?.EmailAddress!);
//
//          await _mediator.Send(request);
//       }
//
//       return RedirectToPage(Links.TaskList.Index.Page, new { id });
//    }
// }
