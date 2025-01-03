
using System;
using System.Collections.Generic;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Frontend.Models;

public static class Links
{
   private static readonly List<LinkItem> _links = [];
   private static string _transfersUrl;
   public static string TransfersUrl => _transfersUrl;

   private static bool _isApplicationDocumentsEnabled;
   public static bool IsApplicationDocumentsEnabled => _isApplicationDocumentsEnabled;

   public static LinkItem AddLinkItem(string page, string backText = "Back")
   {
      LinkItem item = new() { Page = page, BackText = backText };
      _links.Add(item);
      return item;
   }
   public static void InitializeTransfersUrl(string transfersUrl)
   {
      _transfersUrl = transfersUrl;
   }

   public static void InializeProjectDocumentsEnabled(bool isApplicationDocumentsEnabled) {
      _isApplicationDocumentsEnabled = isApplicationDocumentsEnabled;
   }
   public static LinkItem ByPage(string page)
   {
      return _links.Find(x => string.Equals(page, x.Page, StringComparison.InvariantCultureIgnoreCase));
   }
   
   
   public static class SupportProject
   {
      public static readonly LinkItem WhichSchoolNeedsHelp = AddLinkItem(backText: "Back", page: "/which-school-needs-help");
   }
   
   public static class ProjectList
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/Index");
   }
   
   public static class AboutTheSchool
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/AboutTheSchool/Index");
   }
   
   public static class TaskList
   {
      public static readonly LinkItem Index = AddLinkItem(backText: "Back", page: "/TaskList/Index");
      public static readonly LinkItem ContactTheSchool = AddLinkItem(backText: "Back", page: "/TaskList/ContactTheSchool/ContactTheSchool");
   }
   
   public static class NewProject
   {
      public static readonly LinkItem NewConversionInformation = AddLinkItem(page: "/NewProject/NewConversionInformation");
      public static readonly LinkItem WhichSchoolNeedsHelp = AddLinkItem(page: "WhichSchoolNeedsHelp");
      public static readonly LinkItem Summary = AddLinkItem(page: "/Summary");
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
