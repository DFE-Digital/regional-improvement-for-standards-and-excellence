namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.ViewModels;

public class RadioButtonViewModel
{
    public string? Hint { get; set; } = null;
    public string? Name { get; set; }= null;
    public IList<RadioButtonsLabelViewModel> RadioButtons { get; set; } = [];
    public string? Value { get; set; } = null;
}
public class RadioButtonsLabelViewModel
{
    public required string Name { get; set; }
    public required string Id { get; set; }
    public string? Value { get; set; }
}
