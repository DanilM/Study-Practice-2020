using System.Collections.Generic;


namespace Practice
{
  class Parser
  {
    public class ObjectAddress
    {
      public string District { get; set; }
      public string Address { get; set; }
    }
    public class PubliPhone
    {
      public List<string> PublicPhone { get; set; }
    }
    public class WorkinH
    {
      public string WorkHours { get; set; }
      public string DayWeek { get; set; }
    }
    public class Theatre
    {
      public string WebSite { get; set; }
      public double MainHallCapacity { get; set; }
      public double AdditionalHallCapacity { get; set; }
      public string Category { get; set; }
      public string ShortName { get; set; }
      public WorkinH[] WorkingHours { get; set; }
      public PubliPhone[] PublicPhone { get; set; }
      public ObjectAddress[] ObjectAddress { get; set; }
      public string CommonName { get; set; }
    }
  }
}
