
using System;
using System.Collections.Generic;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Models;

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


  



 



   public static class NewProject
   {
      public static readonly LinkItem NewConversionInformation = AddLinkItem(page: "/NewProject/NewConversionInformation");
      public static readonly LinkItem WhichSchoolNeedsHelp = AddLinkItem(page: "WhichSchoolNeedsHelp");
      public static readonly LinkItem Summary = AddLinkItem(page: "/Summary");
   }
   
}

public class LinkItem
{
   public string Page { get; set; }
   public string BackText { get; set; } = "Back";
}
