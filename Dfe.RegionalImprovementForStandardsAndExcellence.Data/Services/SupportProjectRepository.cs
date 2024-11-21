using System.Net;
using Dfe.RegionalImprovementForStandardsAndExcellence.Data.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Data.Services;

public class SupportProjectRepository : ISupportProjectRepository
{
    public async Task<ApiResponse<IEnumerable<SupportProject>>> GetAllSupportProjects()
    {
        
        
        return new ApiResponse<IEnumerable<SupportProject>>((HttpStatusCode)200,SupportProjects());
    }

    public async Task<ApiResponse<SupportProject>> CreateSupportProject(CreateNewSupportProject newProject)
    {
        
        return new ApiResponse<SupportProject>((HttpStatusCode)200,
            
            new SupportProject
            {
                Id = 4,
                SchoolName = "Dog View College",
                SchoolUrn = "789312"
            });
    }
    
    public async Task<ApiResponse<SupportProject>> GetSupportProject(string urn)
    {
        
        
        var supportProjects = SupportProjects();
        var supportProject = supportProjects.FirstOrDefault(sp => sp.SchoolUrn == urn);
        
        return new ApiResponse<SupportProject>((HttpStatusCode)200,supportProject);
    }


    private IEnumerable<SupportProject> SupportProjects()
    {
        IEnumerable<SupportProject> supportProjects = new List<SupportProject>
        {
            new SupportProject
            {
                Id = 1,
                SchoolName = "Greenwood High School",
                SchoolUrn = "123456"
            },
            new SupportProject
            {
                Id = 2,
                SchoolName = "Riverside Academy",
                SchoolUrn = "654321",
                Region = "Shoe Town"
                
            },
            new SupportProject
            {
                Id = 3,
                SchoolName = "Mountain View College",
                SchoolUrn = "789012"
            }
        };

        return supportProjects;
    }
}