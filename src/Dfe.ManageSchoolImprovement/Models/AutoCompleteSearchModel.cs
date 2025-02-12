namespace Dfe.ManageSchoolImprovement.Frontend.Models;

public class AutoCompleteSearchModel(string label, string searchQuery, string searchEndpoint)
{
    public string Label { get; set; } = label;

    public string SearchQuery { get; set; } = searchQuery?.Replace("'", "\\'")!;

    public string SearchEndpoint { get; set; } = searchEndpoint;
}
