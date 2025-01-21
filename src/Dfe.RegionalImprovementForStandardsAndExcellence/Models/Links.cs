namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;

public static class Links
{
   private static readonly List<LinkItem> _links = [];

   private static bool _isApplicationDocumentsEnabled;
   public static bool IsApplicationDocumentsEnabled => _isApplicationDocumentsEnabled;

   public static LinkItem AddLinkItem(string page, string backText = "Back")
   {
      LinkItem item = new() { Page = page, BackText = backText };
      _links.Add(item);
      return item;
   }

   public static void InializeProjectDocumentsEnabled(bool isApplicationDocumentsEnabled) {
      _isApplicationDocumentsEnabled = isApplicationDocumentsEnabled;
   }
   public static LinkItem ByPage(string page)
   {
      return _links.Find(x => string.Equals(page, x.Page, StringComparison.InvariantCultureIgnoreCase));
   }
   
   
   public static class AddSchool
   {
      public static readonly LinkItem WhichSchoolNeedsHelp = AddLinkItem(backText: "Back", page: "/AddSchool/WhichSchoolNeedsHelp");
      public static readonly LinkItem Summary = AddLinkItem(page: "/AddSchool/Summary");
    }
   
   public static class SchoolList
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/SchoolList/Index");
   }
   
   public static class AboutTheSchool
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/AboutTheSchool/Index");
   }
   
   public static class TaskList
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/TaskList/Index");
      public static readonly LinkItem ContactTheSchool = AddLinkItem(backText: "Back", page: "/TaskList/ContactTheSchool/ContactTheSchool");
      public static readonly LinkItem RecordTheSchoolResponse = AddLinkItem(backText: "Back", page: "/TaskList/RecordTheSchoolResponse/Index");
      public static readonly LinkItem CheckPotentialAdviserConflictsOfInterest = AddLinkItem(backText: "Back", page: "/TaskList/AdviserConflictOfIntereset/AdviserConflictOfIntereset");
      public static readonly LinkItem SendIntroductoryEmail = AddLinkItem(backText: "Back", page: "/TaskList/SendIntroductoryEmail/Index");
    }
   
   public static class Notes
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/Notes/Index");
      public static readonly LinkItem NewNote = AddLinkItem(backText: "Back", page: "/Notes/NewNote");
      public static readonly LinkItem EditNote = AddLinkItem(backText: "Back", page: "/Notes/EditNote");
   }
   
   public static class ProjectAssignment
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/ProjectAssignment/Index");
      
   }
    public static class Public
    {
        public static readonly LinkItem Accessibility = AddLinkItem(page: "/Public/AccessibilityStatement");
        public static readonly LinkItem CookiePreferences = AddLinkItem(page: "/Public/CookiePreferences");
        public static readonly LinkItem CookiePreferencesURL = AddLinkItem(page: "/public/cookie-Preferences");
    }
}

public class LinkItem
{
   public string Page { get; set; }
   public string BackText { get; set; } = "Back";
}
